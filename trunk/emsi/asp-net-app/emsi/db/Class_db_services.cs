using Class_biz_milestones;
using Class_biz_notifications;
using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Class_db_services
  {
  public class TClass_db_services: TClass_db
    {

    private class service_summary
      {
      public string id;
      public string name;
      public string short_name;
      public string affiliate_num;
      public string strike_team_participation_level_id;
      }

        private readonly TClass_biz_notifications biz_notifications = null;
        private readonly TClass_db_trail db_trail = null;

        public TClass_db_services() : base()
        {
            biz_notifications = new TClass_biz_notifications();
            db_trail = new TClass_db_trail();
        }

        internal string AffiliateNumOf(object summary)
          {
          return (summary as service_summary).affiliate_num;
          }

        public string AffiliateNumOfId(string id)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("SELECT affiliate_num FROM service WHERE id = " + id, connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public bool BeAdded(string service_name, string affiliate_num, string password_reset_email_address, string county_code)
        {
            bool result;
            result = false;
            Open();
            using var my_sql_command_1 = new MySqlCommand("select max(service_user.id) = max(service.id) from service_user, service", connection);
            if ("1" != my_sql_command_1.ExecuteScalar().ToString())
            {
                biz_notifications.IssueCorruptionNotification("SERVICE", "BEFORE");
            }
            else
            {
                using var my_sql_command_2 = new MySqlCommand(db_trail.Saved("SET FOREIGN_KEY_CHECKS=0; SET AUTOCOMMIT=0; START TRANSACTION" + ";" + " insert service" + " set county_code = \"" + county_code + "\"" + " , affiliate_num = \"" + affiliate_num + "\"" + " , name = \"" + service_name + "\"" + " , charter_kind = \"9\"" + " , corpadmin_contact_name =\"\"" + " , corpadmin_primary_phone_num = \"\"" + " , be_emsof_participant = FALSE" + " , coo_name = \"\"" + " , coo_work_phone_num = \"\"" + " , physical_street_address_line_1 = \"\"" + " , physical_city = \"\"" + " , physical_state = \"\"" + " , physical_zip_code = \"\"" + " , mail_address_line_1 = \"\"" + " , mail_city = \"\"" + " , mail_state = \"\"" + " , mail_zip_code = \"\"" + " , num_doh_licensed_vehicles = 0" + " , num_ambulances = 0" + " , be_qrs_unrecognized = FALSE" + " , be_rescue_unrecognized = FALSE" + ";" + " insert service_user set password_reset_email_address = \"" + password_reset_email_address + "\"" + ";" + " SET FOREIGN_KEY_CHECKS=1; COMMIT"), connection);
                my_sql_command_2.ExecuteNonQuery();
                //
                using var my_sql_command_3 = new MySqlCommand("select max(service_user.id) = max(service.id) from service_user, service", connection);
                if ("1" != my_sql_command_3.ExecuteScalar().ToString())
                {
                    biz_notifications.IssueCorruptionNotification("SERVICE", "AFTER");
                }
                else
                {
                    result = true;
                }
            }
            Close();

            return result;
        }

        public bool BeDistressed(string id)
        {
            bool result;
            Open();
            using var my_sql_command = new MySqlCommand("select be_distressed from service where id = \"" + id + "\"", connection);
            result = "1" == my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public bool BeKnown(string affiliate_num)
        {
            bool result;
            Open();
            using var my_sql_command = new MySqlCommand("select 1 from service where affiliate_num = \"" + affiliate_num + "\"", connection);
            result = null != my_sql_command.ExecuteScalar();
            Close();
            return result;
        }

        public bool BeValidAndParticipating(string id)
        {
            bool result;
            Open();
            using var my_sql_command = new MySqlCommand("select be_valid_profile and be_emsof_participant from service where id = \"" + id + "\"", connection);
            result = null != my_sql_command.ExecuteScalar();
            Close();
            return result;
        }

        public bool Bind(string partial_affiliate_num, object target)
        {
            bool result;
            MySqlDataReader dr;
            Open();
            ((target) as ListControl).Items.Clear();
            using var my_sql_command = new MySqlCommand("SELECT affiliate_num" + " , name" + " , concat(affiliate_num,\" - \",name) as descriptor" + " FROM service" + " WHERE concat(affiliate_num,\" - \",name) like \"%" + partial_affiliate_num + "%\"" + " order by name", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["descriptor"].ToString(), dr["affiliate_num"].ToString()));
            }
            dr.Close();
            Close();
            result = ((target) as ListControl).Items.Count > 0;
            return result;
        }

    public void BindAnnualRespondents
          (
          string sort_order,
          bool be_order_ascending,
          object target,
          string region_code,
          string county_code
          )
          {
            Open();
            using var my_sql_command = new MySqlCommand
              (
              "select affiliate_num"
              + " , service.name as service_name"
              + " , county_code_name_map.name as county_name"
              + " , IF(be_valid_profile,IF(be_emsof_participant,'YES','No'),'no response') as be_emsof_participant"
              + " , IF(be_valid_profile,IF(be_emsof_participant,1,0),-1) as participation_pecking_order"
              + " , IFNULL(password_reset_email_address,'') as password_reset_email_address"
              + " , IFNULL(corpadmin_email_address,'') as corpadmin_email_address"
              + " , IFNULL(coo_email_address,'') as coo_email_address"
              + " from service"
              +   " join service_user using (id)"
              +   " join county_code_name_map on (county_code_name_map.code=service.county_code)"
              +   " join county_region_map on (county_region_map.county_code=service.county_code)"
              +   (county_code.Length > 0 ? " left join emsof_extra_service_county_dependency on (emsof_extra_service_county_dependency.service_id=service.id)" : k.EMPTY)
              + " WHERE region_code = '" + region_code + "'"
              +     (county_code.Length > 0 ? " and '" + county_code + "' in (service.county_code,emsof_extra_service_county_dependency.county_code)" : k.EMPTY)
              + " order by " + sort_order.Replace("%",(be_order_ascending ? " asc" : " desc")),
              connection
              );
            ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
            ((target) as BaseDataList).DataBind();
            Close();
          }
        public void BindAnnualRespondents(string sort_order, bool be_order_ascending, object target, string region_code)
          {
          BindAnnualRespondents(sort_order,be_order_ascending,target,region_code,county_code:k.EMPTY);
          }

        public void BindListControl(string county_user_id, object target, bool be_unfiltered, bool be_inclusive_of_invalids_and_nonparticipants)
        {
            MySqlDataReader dr;
            string cmdText;
            Open();
            ((target) as ListControl).Items.Clear();
            ((target) as ListControl).Items.Add(new ListItem("-- Select --", ""));
            cmdText = "SELECT DISTINCT id,name FROM service_user JOIN service using (id)";
            if (!be_unfiltered)
              {
              cmdText += " left join emsof_extra_service_county_dependency on (emsof_extra_service_county_dependency.service_id=service.id)";
              }
            cmdText += " WHERE be_active";
            if (!be_unfiltered)
              {
              cmdText += " and '" + county_user_id + "' in (service.county_code,emsof_extra_service_county_dependency.county_code)";
              }
            if (!be_inclusive_of_invalids_and_nonparticipants)
            {
                cmdText += " and be_valid_profile and be_emsof_participant";
            }
            cmdText += " ORDER BY name";
            using var my_sql_command = new MySqlCommand(cmdText, connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
            dr.Close();
            Close();
        }

    internal void BindBaseDataListOfAdHocStrikeTeams
      (
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string service_strike_team_management_footprint
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select service.id as id"
        + " , region_code_name_map.code as region_code"
        + " , region_code_name_map.name as region_name"
        + " , affiliate_num"
        + " , service.short_name as short_name"
        + " , service.name as name"
        + " , GROUP_CONCAT(email_address) as email_target"
        + " , concat(phone_number,'@',sms_gateway.hostname) as sms_target"
        + " from service"
        +   " join county_region_map on (county_region_map.county_code=service.county_code)"
        +   " join region_code_name_map on (region_code_name_map.code=county_region_map.region_code)"
        +   " left join role_member_map on (role_member_map.service_id=service.id)"
        +   " left join role on (role.id=role_member_map.role_id and role.name = 'Service Strike Team Manager')"
        +   " left join practitioner on (practitioner.id=role_member_map.member_id)"
        +   " left join practitioner_strike_team_detail on (practitioner_strike_team_detail.practitioner_id=practitioner.id)"
        +   " left join sms_gateway on (sms_gateway.id=practitioner_strike_team_detail.phone_service_id)"
        +   " join strike_team_participation_level on (strike_team_participation_level.id=service.strike_team_participation_level_id)"
        + " where strike_team_participation_level.description = 'Ad-hoc'"
        +     (service_strike_team_management_footprint.Length > 0 ? " and service.id in (" + service_strike_team_management_footprint + ")" : k.EMPTY) 
        + " group by service.id"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

        public void BindListControl(string county_user_id, object target)
        {
            BindListControl(county_user_id, target, false);
        }

        public void BindListControl(string county_user_id, object target, bool be_unfiltered)
        {
            BindListControl(county_user_id, target, be_unfiltered, false);
        }

    internal void BindPacratManagementBaseDataList
      (
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string region_code,
      bool do_include_all_services
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select service.id as id"
        + " , affiliate_num"
        + " , service.short_name as short_name"
        + " , service.name as name"
        + " , strike_team_participation_level.description as strike_team_participation"
        + " , (select count(practitioner_id) from strike_team_roster where service_id = service.id) as num_members"
        + " , (select count(id) from vehicle where service_id = service.id) as num_vehicles"
        + " , GROUP_CONCAT(email_address) as email_target"
        + " , concat(phone_number,'@',sms_gateway.hostname) as sms_target"
        + " from service"
        +   " join county_region_map on (county_region_map.county_code=service.county_code)"
        +   " left join role_member_map on (role_member_map.service_id=service.id)"
        +   " left join role on (role.id=role_member_map.role_id and role.name = 'Service Strike Team Manager')"
        +   " left join practitioner on (practitioner.id=role_member_map.member_id)"
        +   " left join practitioner_strike_team_detail on (practitioner_strike_team_detail.practitioner_id=practitioner.id)"
        +   " left join sms_gateway on (sms_gateway.id=practitioner_strike_team_detail.phone_service_id)"
        +   " join strike_team_participation_level on (strike_team_participation_level.id=service.strike_team_participation_level_id)"
        + " where county_region_map.region_code = '" + region_code + "'"
        +     (do_include_all_services ? k.EMPTY : " and strike_team_participation_level.pecking_order > 0")
        + " group by service.id"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindStrikeTeamMobilizationAnnouncementListControl
      (
      string region_code,
      object target
      )
      {
      Open();
      ((target) as ListControl).Items.Clear();
      using var my_sql_command = new MySqlCommand
        (
        "select service.id as id"
        + " , service.short_name as name"
        + " from service"
        +   " join county_region_map on (county_region_map.county_code=service.county_code)"
        +   " left join role_member_map on (role_member_map.service_id=service.id)"
        +   " left join role on (role.id=role_member_map.role_id and role.name = 'Service Strike Team Manager')"
        +   " left join practitioner on (practitioner.id=role_member_map.member_id)"
        +   " left join practitioner_strike_team_detail on (practitioner_strike_team_detail.practitioner_id=practitioner.id)"
        +   " left join sms_gateway on (sms_gateway.id=practitioner_strike_team_detail.phone_service_id)"
        +   " join strike_team_participation_level on (strike_team_participation_level.id=service.strike_team_participation_level_id)"
        + " where county_region_map.region_code = '" + region_code + "'"
        +     " and strike_team_participation_level.pecking_order > 0"
        + " group by service.id"
        + " order by service.name",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      }

        internal void BindStrikeTeamAffiliationBaseDataList
          (
          string member_id,
          string sort_order,
          bool be_sort_order_ascending,
          object target
          )
          {
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select service.id as service_id"
            + " , service.affiliate_num as service_affiliate_num"
            + " , service.short_name as service_name"
            + " from role_member_map"
            +   " join service on (service.id=role_member_map.service_id)"
            +   " join county_region_map on (county_region_map.county_code=service.county_code)"
            +   " join region_code_name_map on (region_code_name_map.code=county_region_map.region_code)"
            + " where member_id = '" + member_id + "'"
            +   " and role_id = (select id from role where name = 'Service Strike Team Manager')"
            +   " and be_pacrat_subscriber"
            + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
            connection
            );
          ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
          ((target) as BaseDataList).DataBind();
          Close();
          }

    internal void DeactivateAdHocServices(string region_code)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "update service"
        + " join county_region_map on (county_region_map.county_code=service.county_code)"
        + " set strike_team_participation_level_id = (select id from strike_team_participation_level where description = 'None')"
        + " where strike_team_participation_level_id = (select id from strike_team_participation_level where description = 'Ad-hoc')"
        +   " and county_region_map.region_code = '" + region_code + "'",
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

        public bool Delete(string affiliate_num)
        {
            bool result;
            result = true;
            Open();
            try {
                using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from service where affiliate_num = " + affiliate_num), connection);
                my_sql_command.ExecuteNonQuery();
            }
            catch(System.Exception e) {
                if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
                {
                    result = false;
                }
                else
                {
                    throw;
                }
            }
            Close();
            return result;
        }

    internal string DesignatorWithCompetingShortName
      (
      string affiliate_num,
      string short_name
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select concat(name,' (affiliate #',affiliate_num,')') from service where short_name = '" + short_name + "' and affiliate_num <> '" + affiliate_num + "'",
        connection
        );
      var designator_with_competing_short_name_obj = my_sql_command.ExecuteScalar();
      Close();
      return (designator_with_competing_short_name_obj == null ? k.EMPTY : designator_with_competing_short_name_obj.ToString());
      }

        public string EmailTargetForCounty(string county_id, bool be_filtered_by_valid_profile, bool be_valid_profile, bool be_filtered_by_emsof_participation, bool be_emsof_participant)
          {
          var email_target = k.EMPTY;
          var sql = "select password_reset_email_address" + " from service_user" + " join service using (id)" + " where county_code = '" + county_id + "'";
          if (be_filtered_by_valid_profile)
            {
            sql += " and be_valid_profile = " + be_valid_profile.ToString();
            if (be_filtered_by_emsof_participation)
              {
              sql += " and be_emsof_participant = " + be_emsof_participant.ToString();
              }
            }
          Open();
          using var my_sql_command = new MySqlCommand(sql, connection);
          var dr = my_sql_command.ExecuteReader();
          while (dr.Read())
            {
            email_target += dr["password_reset_email_address"].ToString() + k.COMMA_SPACE;
            }
          Close();
          return email_target.TrimEnd(new char[] {Convert.ToChar(k.COMMA), Convert.ToChar(k.SPACE)});
          }

        internal string EmsofCoordinatorNameOf(string id)
          {
          Open();
          using var my_sql_command = new MySqlCommand("select emsof_contact_name from service where id = '" + id + "'",connection);
          var emsof_coordinator_name_of = my_sql_command.ExecuteScalar().ToString();
          Close();
          return emsof_coordinator_name_of;
          }

        public bool Get
          (
          string affiliate_num,
          out string id,
          out string name,
          out string county_code,
          out string business_phone_num,
          out string business_fax_num,
          out string website_address,
          out string charter_kind,
          out string corpadmin_contact_name,
          out string corpadmin_primary_phone_num,
          out string corpadmin_secondary_phone_num,
          out string corpadmin_email_address,
          out bool be_emsof_participant,
          out string emsof_nonparticipation_reason,
          out string emsof_contact_name,
          out string emsof_contact_primary_phone_num,
          out string emsof_contact_sms_phone_num,
          out string coo_name,
          out string coo_work_phone_num,
          out string coo_home_phone_num,
          out string coo_email_address,
          out string coo_mobile_phone_or_pager_num,
          out string md_name,
          out string md_office_phone_num,
          out string md_home_phone_num,
          out string md_email_address,
          out string md_mobile_phone_or_pager_num,
          out string physical_street_address_line_1,
          out string physical_street_address_line_2,
          out string physical_city,
          out string physical_state,
          out string physical_zip_code,
          out string mail_address_line_1,
          out string mail_address_line_2,
          out string mail_city,
          out string mail_state,
          out string mail_zip_code,
          out bool be_qrs,
          out bool be_bls_amb,
          out bool be_als_amb,
          out bool be_als_squad,
          out bool be_air_amb,
          out bool be_rescue,
          out bool be_pa_turnpike_contractor,
          out string num_doh_licensed_vehicles,
          out string num_ambulances,
          out bool be_dera,
          out string charter_other_kind,
          out bool be_valid_profile,
          out string federal_tax_id,
          out bool be_qrs_unrecognized,
          out bool be_rescue_unrecognized,
          out bool be_distressed,
          out string num_staff_typical_als_08_16,
          out string num_staff_typical_als_16_00,
          out string num_staff_typical_als_00_08,
          out string num_staff_typical_bls_08_16,
          out string num_staff_typical_bls_16_00,
          out string num_staff_typical_bls_00_08,
          out string num_units_typical_als_08_16,
          out string num_units_typical_als_16_00,
          out string num_units_typical_als_00_08,
          out string num_units_typical_bls_08_16,
          out string num_units_typical_bls_16_00,
          out string num_units_typical_bls_00_08,
          out string num_n95_masks,
          out string num_scba,
          out string num_pfd,
          out string num_portable_vents,
          out string num_infusion_pumps,
          out string num_bariatric_stretchers,
          out string num_golf_carts,
          out string num_gators,
          out string num_quads,
          out string num_mci_special_event_trailers,
          out string num_bariatric_equipped_units,
          out string num_wheelchair_vans,
          out string num_opensky_radios,
          out string num_uhf_portables,
          out string num_uhf_mobiles,
          out string num_vhf_portables,
          out string num_vhf_mobiles,
          out string num_trunked_radios,
          out string num_gps_receivers,
          out string num_mark_1_kits,
          out string num_cases_potable_h2o,
          out string num_recognized_rescue_units,
          out string num_4wd_ambulances,
          out string num_recognized_qrs_units,
          out string num_unrecognized_qrs_units,
          out string num_boats,
          out string num_ballistic_vests,
          out string num_cases_mre,
          out string inventory_memo,
          out string key_cell_phone_1_num,
          out string key_cell_phone_1_assignee,
          out string key_cell_phone_2_num,
          out string key_cell_phone_2_assignee,
          out string key_cell_phone_3_num,
          out string key_cell_phone_3_assignee,
          out string key_cell_phone_4_num,
          out string key_cell_phone_4_assignee,
          out string key_cell_phone_5_num,
          out string key_cell_phone_5_assignee,
          out string key_cell_phone_6_num,
          out string key_cell_phone_6_assignee,
          out string radio_channel_1,
          out string radio_channel_2,
          out string radio_channel_3,
          out string radio_channel_4,
          out string radio_channel_5,
          out string radio_channel_6,
          out string primary_response_area,
          out string short_name
          )
          {
            id = k.EMPTY;
            name = k.EMPTY;
            county_code = k.EMPTY;
            business_phone_num = k.EMPTY;
            business_fax_num = k.EMPTY;
            website_address = k.EMPTY;
            charter_kind = k.EMPTY;
            corpadmin_contact_name = k.EMPTY;
            corpadmin_primary_phone_num = k.EMPTY;
            corpadmin_secondary_phone_num = k.EMPTY;
            corpadmin_email_address = k.EMPTY;
            be_emsof_participant = false;
            emsof_nonparticipation_reason = k.EMPTY;
            emsof_contact_name = k.EMPTY;
            emsof_contact_primary_phone_num = k.EMPTY;
            emsof_contact_sms_phone_num = k.EMPTY;
            coo_name = k.EMPTY;
            coo_work_phone_num = k.EMPTY;
            coo_home_phone_num = k.EMPTY;
            coo_email_address = k.EMPTY;
            coo_mobile_phone_or_pager_num = k.EMPTY;
            md_name = k.EMPTY;
            md_office_phone_num = k.EMPTY;
            md_home_phone_num = k.EMPTY;
            md_email_address = k.EMPTY;
            md_mobile_phone_or_pager_num = k.EMPTY;
            physical_street_address_line_1 = k.EMPTY;
            physical_street_address_line_2 = k.EMPTY;
            physical_city = k.EMPTY;
            physical_state = k.EMPTY;
            physical_zip_code = k.EMPTY;
            mail_address_line_1 = k.EMPTY;
            mail_address_line_2 = k.EMPTY;
            mail_city = k.EMPTY;
            mail_state = k.EMPTY;
            mail_zip_code = k.EMPTY;
            be_qrs = false;
            be_bls_amb = false;
            be_als_amb = false;
            be_als_squad = false;
            be_air_amb = false;
            be_rescue = false;
            be_pa_turnpike_contractor = false;
            num_doh_licensed_vehicles = k.EMPTY;
            num_ambulances = k.EMPTY;
            be_dera = false;
            charter_other_kind = k.EMPTY;
            be_valid_profile = false;
            federal_tax_id = k.EMPTY;
            be_qrs_unrecognized = false;
            be_rescue_unrecognized = false;
            be_distressed = false;
            num_staff_typical_als_08_16 = k.EMPTY;
            num_staff_typical_als_16_00 = k.EMPTY;
            num_staff_typical_als_00_08 = k.EMPTY;
            num_staff_typical_bls_08_16 = k.EMPTY;
            num_staff_typical_bls_16_00 = k.EMPTY;
            num_staff_typical_bls_00_08 = k.EMPTY;
            num_units_typical_als_08_16 = k.EMPTY;
            num_units_typical_als_16_00 = k.EMPTY;
            num_units_typical_als_00_08 = k.EMPTY;
            num_units_typical_bls_08_16 = k.EMPTY;
            num_units_typical_bls_16_00 = k.EMPTY;
            num_units_typical_bls_00_08 = k.EMPTY;
            num_n95_masks = k.EMPTY;
            num_scba = k.EMPTY;
            num_pfd = k.EMPTY;
            num_portable_vents = k.EMPTY;
            num_infusion_pumps = k.EMPTY;
            num_bariatric_stretchers = k.EMPTY;
            num_golf_carts = k.EMPTY;
            num_gators = k.EMPTY;
            num_quads = k.EMPTY;
            num_mci_special_event_trailers = k.EMPTY;
            num_bariatric_equipped_units = k.EMPTY;
            num_wheelchair_vans = k.EMPTY;
            num_opensky_radios = k.EMPTY;
            num_uhf_portables = k.EMPTY;
            num_uhf_mobiles = k.EMPTY;
            num_vhf_portables = k.EMPTY;
            num_vhf_mobiles = k.EMPTY;
            num_trunked_radios = k.EMPTY;
            num_gps_receivers = k.EMPTY;
            num_mark_1_kits = k.EMPTY;
            num_cases_potable_h2o = k.EMPTY;
            num_recognized_rescue_units = k.EMPTY;
            num_4wd_ambulances = k.EMPTY;
            num_recognized_qrs_units = k.EMPTY;
            num_unrecognized_qrs_units = k.EMPTY;
            num_boats = k.EMPTY;
            num_ballistic_vests = k.EMPTY;
            num_cases_mre = k.EMPTY;
            inventory_memo = k.EMPTY;
            key_cell_phone_1_num = k.EMPTY;
            key_cell_phone_1_assignee = k.EMPTY;
            key_cell_phone_2_num = k.EMPTY;
            key_cell_phone_2_assignee = k.EMPTY;
            key_cell_phone_3_num = k.EMPTY;
            key_cell_phone_3_assignee = k.EMPTY;
            key_cell_phone_4_num = k.EMPTY;
            key_cell_phone_4_assignee = k.EMPTY;
            key_cell_phone_5_num = k.EMPTY;
            key_cell_phone_5_assignee = k.EMPTY;
            key_cell_phone_6_num = k.EMPTY;
            key_cell_phone_6_assignee = k.EMPTY;
            radio_channel_1 = k.EMPTY;
            radio_channel_2 = k.EMPTY;
            radio_channel_3 = k.EMPTY;
            radio_channel_4 = k.EMPTY;
            radio_channel_5 = k.EMPTY;
            radio_channel_6 = k.EMPTY;
            primary_response_area = k.EMPTY;
            short_name = k.EMPTY;
            var result = false;
            Open();
            using var my_sql_command = new MySqlCommand("select * from service where CAST(affiliate_num AS CHAR) = '" + affiliate_num + "'", connection);
            MySqlDataReader dr = my_sql_command.ExecuteReader();
            if (dr.Read())
              {
                id = dr["id"].ToString();
                name = dr["name"].ToString();
                county_code = dr["county_code"].ToString();
                business_phone_num = dr["business_phone_num"].ToString();
                business_fax_num = dr["business_fax_num"].ToString();
                website_address = dr["website_address"].ToString();
                charter_kind = dr["charter_kind"].ToString();
                corpadmin_contact_name = dr["corpadmin_contact_name"].ToString();
                corpadmin_primary_phone_num = dr["corpadmin_primary_phone_num"].ToString();
                corpadmin_secondary_phone_num = dr["corpadmin_secondary_phone_num"].ToString();
                corpadmin_email_address = dr["corpadmin_email_address"].ToString();
                be_emsof_participant = (dr["be_emsof_participant"].ToString() == "1");
                emsof_nonparticipation_reason = dr["emsof_nonparticipation_reason"].ToString();
                emsof_contact_name = dr["emsof_contact_name"].ToString();
                emsof_contact_primary_phone_num = dr["emsof_contact_primary_phone_num"].ToString();
                emsof_contact_sms_phone_num = dr["emsof_contact_sms_phone_num"].ToString();
                coo_name = dr["coo_name"].ToString();
                coo_work_phone_num = dr["coo_work_phone_num"].ToString();
                coo_home_phone_num = dr["coo_home_phone_num"].ToString();
                coo_email_address = dr["coo_email_address"].ToString();
                coo_mobile_phone_or_pager_num = dr["coo_mobile_phone_or_pager_num"].ToString();
                md_name = dr["md_name"].ToString();
                md_office_phone_num = dr["md_office_phone_num"].ToString();
                md_home_phone_num = dr["md_home_phone_num"].ToString();
                md_email_address = dr["md_email_address"].ToString();
                md_mobile_phone_or_pager_num = dr["md_mobile_phone_or_pager_num"].ToString();
                physical_street_address_line_1 = dr["physical_street_address_line_1"].ToString();
                physical_street_address_line_2 = dr["physical_street_address_line_2"].ToString();
                physical_city = dr["physical_city"].ToString();
                physical_state = dr["physical_state"].ToString();
                physical_zip_code = dr["physical_zip_code"].ToString();
                mail_address_line_1 = dr["mail_address_line_1"].ToString();
                mail_address_line_2 = dr["mail_address_line_2"].ToString();
                mail_city = dr["mail_city"].ToString();
                mail_state = dr["mail_state"].ToString();
                mail_zip_code = dr["mail_zip_code"].ToString();
                be_qrs = (dr["be_qrs"].ToString() == "1");
                be_bls_amb = (dr["be_bls_amb"].ToString() == "1");
                be_als_amb = (dr["be_als_amb"].ToString() == "1");
                be_als_squad = (dr["be_als_squad"].ToString() == "1");
                be_air_amb = (dr["be_air_amb"].ToString() == "1");
                be_rescue = (dr["be_rescue"].ToString() == "1");
                be_pa_turnpike_contractor = (dr["be_pa_turnpike_contractor"].ToString() == "1");
                num_doh_licensed_vehicles = dr["num_doh_licensed_vehicles"].ToString();
                num_ambulances = dr["num_ambulances"].ToString();
                be_dera = (dr["be_dera"].ToString() == "1");
                charter_other_kind = dr["charter_other_kind"].ToString();
                be_valid_profile = (dr["be_valid_profile"].ToString() == "1");
                federal_tax_id = dr["federal_tax_id_num"].ToString();
                be_qrs_unrecognized = (dr["be_qrs_unrecognized"].ToString() == "1");
                be_rescue_unrecognized = (dr["be_rescue_unrecognized"].ToString() == "1");
                be_distressed = (dr["be_distressed"].ToString() == "1");
                num_staff_typical_als_08_16 = dr["num_staff_typical_als_08_16"].ToString();
                num_staff_typical_als_16_00 = dr["num_staff_typical_als_16_00"].ToString();
                num_staff_typical_als_00_08 = dr["num_staff_typical_als_00_08"].ToString();
                num_staff_typical_bls_08_16 = dr["num_staff_typical_bls_08_16"].ToString();
                num_staff_typical_bls_16_00 = dr["num_staff_typical_bls_16_00"].ToString();
                num_staff_typical_bls_00_08 = dr["num_staff_typical_bls_00_08"].ToString();
                num_units_typical_als_08_16 = dr["num_units_typical_als_08_16"].ToString();
                num_units_typical_als_16_00 = dr["num_units_typical_als_16_00"].ToString();
                num_units_typical_als_00_08 = dr["num_units_typical_als_00_08"].ToString();
                num_units_typical_bls_08_16 = dr["num_units_typical_bls_08_16"].ToString();
                num_units_typical_bls_16_00 = dr["num_units_typical_bls_16_00"].ToString();
                num_units_typical_bls_00_08 = dr["num_units_typical_bls_00_08"].ToString();
                num_n95_masks = dr["num_n95_masks"].ToString();
                num_scba = dr["num_scba"].ToString();
                num_pfd = dr["num_pfd"].ToString();
                num_portable_vents = dr["num_portable_vents"].ToString();
                num_infusion_pumps = dr["num_infusion_pumps"].ToString();
                num_bariatric_stretchers = dr["num_bariatric_stretchers"].ToString();
                num_golf_carts = dr["num_golf_carts"].ToString();
                num_gators = dr["num_gators"].ToString();
                num_quads = dr["num_quads"].ToString();
                num_mci_special_event_trailers = dr["num_mci_special_event_trailers"].ToString();
                num_bariatric_equipped_units = dr["num_bariatric_equipped_units"].ToString();
                num_wheelchair_vans = dr["num_wheelchair_vans"].ToString();
                num_opensky_radios = dr["num_opensky_radios"].ToString();
                num_uhf_portables = dr["num_uhf_portables"].ToString();
                num_uhf_mobiles = dr["num_uhf_mobiles"].ToString();
                num_vhf_portables = dr["num_vhf_portables"].ToString();
                num_vhf_mobiles = dr["num_vhf_mobiles"].ToString();
                num_trunked_radios = dr["num_trunked_radios"].ToString();
                num_gps_receivers = dr["num_gps_receivers"].ToString();
                num_mark_1_kits = dr["num_mark_1_kits"].ToString();
                num_cases_potable_h2o = dr["num_cases_potable_h2o"].ToString();
                num_recognized_rescue_units = dr["num_recognized_rescue_units"].ToString();
                num_4wd_ambulances = dr["num_4wd_ambulances"].ToString();
                num_recognized_qrs_units = dr["num_recognized_qrs_units"].ToString();
                num_unrecognized_qrs_units = dr["num_unrecognized_qrs_units"].ToString();
                num_boats = dr["num_boats"].ToString();
                num_ballistic_vests = dr["num_ballistic_vests"].ToString();
                num_cases_mre = dr["num_cases_mre"].ToString();
                inventory_memo = dr["inventory_memo"].ToString();
                key_cell_phone_1_num = dr["key_cell_phone_1_num"].ToString();
                key_cell_phone_1_assignee = dr["key_cell_phone_1_assignee"].ToString();
                key_cell_phone_2_num = dr["key_cell_phone_2_num"].ToString();
                key_cell_phone_2_assignee = dr["key_cell_phone_2_assignee"].ToString();
                key_cell_phone_3_num = dr["key_cell_phone_3_num"].ToString();
                key_cell_phone_3_assignee = dr["key_cell_phone_3_assignee"].ToString();
                key_cell_phone_4_num = dr["key_cell_phone_4_num"].ToString();
                key_cell_phone_4_assignee = dr["key_cell_phone_4_assignee"].ToString();
                key_cell_phone_5_num = dr["key_cell_phone_5_num"].ToString();
                key_cell_phone_5_assignee = dr["key_cell_phone_5_assignee"].ToString();
                key_cell_phone_6_num = dr["key_cell_phone_6_num"].ToString();
                key_cell_phone_6_assignee = dr["key_cell_phone_6_assignee"].ToString();
                radio_channel_1 = dr["radio_channel_1"].ToString();
                radio_channel_2 = dr["radio_channel_2"].ToString();
                radio_channel_3 = dr["radio_channel_3"].ToString();
                radio_channel_4 = dr["radio_channel_4"].ToString();
                radio_channel_5 = dr["radio_channel_5"].ToString();
                radio_channel_6 = dr["radio_channel_6"].ToString();
                primary_response_area = dr["primary_response_area"].ToString();
                short_name = dr["short_name"].ToString();
                result = true;
              }
            dr.Close();
            Close();
            return result;
          }

        internal string IdOf(object summary)
          {
          return (summary as service_summary).id;
          }

        internal string IdOfAffiliateNum(string affiliate_num)
          {
          Open();
          using var my_sql_command = new MySqlCommand("select id from service where affiliate_num = '" + affiliate_num + "'",connection);
          var id_of_affiliate_num = my_sql_command.ExecuteScalar().ToString();
          Close();
          return id_of_affiliate_num;
          }

        public void MarkProfilesStale()
        {
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("update service set be_valid_profile = FALSE"), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

        public string MdNameOf(string service_id)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select md_name from service where id = " + service_id, connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public string NameOf(string service_id)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select name from service where id = '" + service_id + "'", connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        internal string NameOfSummary(object summary)
          {
          return (summary as service_summary).name;
          }

        public uint NumAmbulancesOf(string service_id)
        {
            uint result;
            Open();
            using var my_sql_command = new MySqlCommand("select num_ambulances from service where id = " + service_id, connection);
            result = uint.Parse(my_sql_command.ExecuteScalar().ToString());
            Close();
            return result;
        }

        public uint NumDohLicensedVehiclesOf(string service_id)
        {
            uint result;
            Open();
            using var my_sql_command = new MySqlCommand("select num_doh_licensed_vehicles from service where id = " + service_id, connection);
            result = uint.Parse(my_sql_command.ExecuteScalar().ToString());
            Close();
            return result;
        }

        internal string RegionCodeOf(string service_id)
          {
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select region_code"
            + " from service join county_region_map on (county_region_map.county_code=service.county_code)"
            + " where service.id = '" + service_id + "'",
            connection
            );
          var region_code_of = my_sql_command.ExecuteScalar().ToString();
          Close();
          return region_code_of;
          }

        internal string ServiceStrikeTeamManagementFootprintOf(string member_id)
          {
          var service_strike_team_management_footprint_of = k.EMPTY;          
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select GROUP_CONCAT(service.id)"
            + " from role_member_map"
            +   " join service on (service.id=role_member_map.service_id)"
            +   " join county_region_map on (county_region_map.county_code=service.county_code)"
            +   " join region_code_name_map on (region_code_name_map.code=county_region_map.region_code)"
            + " where member_id = '" + member_id + "'"
            +   " and role_id = (select id from role where name = 'Service Strike Team Manager')"
            +   " and be_pacrat_subscriber",
            connection
            );
          var service_strike_team_management_footprint_of_obj = my_sql_command.ExecuteScalar();
          if (service_strike_team_management_footprint_of_obj != null)
            {
            service_strike_team_management_footprint_of = service_strike_team_management_footprint_of_obj.ToString();
            }
          Close();
          return service_strike_team_management_footprint_of;
          }

        public void Set
          (
          string affiliate_num,
          string name,
          string county_code,
          string business_phone_num,
          string business_fax_num,
          string website_address,
          string charter_kind,
          string corpadmin_contact_name,
          string corpadmin_primary_phone_num,
          string corpadmin_secondary_phone_num,
          string corpadmin_email_address,
          bool be_emsof_participant,
          string emsof_nonparticipation_reason,
          string emsof_contact_name,
          string emsof_contact_primary_phone_num,
          string emsof_contact_sms_phone_num,
          string coo_name,
          string coo_work_phone_num,
          string coo_home_phone_num,
          string coo_email_address,
          string coo_mobile_phone_or_pager_num,
          string md_name,
          string md_office_phone_num,
          string md_home_phone_num,
          string md_email_address,
          string md_mobile_phone_or_pager_num,
          string physical_street_address_line_1,
          string physical_street_address_line_2,
          string physical_city,
          string physical_state,
          string physical_zip_code,
          string mail_address_line_1,
          string mail_address_line_2,
          string mail_city,
          string mail_state,
          string mail_zip_code,
          bool be_qrs,
          bool be_bls_amb,
          bool be_als_amb,
          bool be_als_squad,
          bool be_air_amb,
          bool be_rescue,
          bool be_pa_turnpike_contractor,
          string num_doh_licensed_vehicles,
          string num_ambulances,
          bool be_dera,
          string charter_other_kind,
          bool be_valid_profile,
          string federal_tax_id,
          bool be_qrs_unrecognized,
          bool be_rescue_unrecognized,
          bool be_distressed,
          string num_staff_typical_als_08_16,
          string num_staff_typical_als_16_00,
          string num_staff_typical_als_00_08,
          string num_staff_typical_bls_08_16,
          string num_staff_typical_bls_16_00,
          string num_staff_typical_bls_00_08,
          string num_units_typical_als_08_16,
          string num_units_typical_als_16_00,
          string num_units_typical_als_00_08,
          string num_units_typical_bls_08_16,
          string num_units_typical_bls_16_00,
          string num_units_typical_bls_00_08,
          string num_n95_masks,
          string num_scba,
          string num_pfd,
          string num_portable_vents,
          string num_infusion_pumps,
          string num_bariatric_stretchers,
          string num_golf_carts,
          string num_gators,
          string num_quads,
          string num_mci_special_event_trailers,
          string num_bariatric_equipped_units,
          string num_wheelchair_vans,
          string num_opensky_radios,
          string num_uhf_portables,
          string num_uhf_mobiles,
          string num_vhf_portables,
          string num_vhf_mobiles,
          string num_trunked_radios,
          string num_gps_receivers,
          string num_mark_1_kits,
          string num_cases_potable_h2o,
          string num_recognized_rescue_units,
          string num_4wd_ambulances,
          string num_recognized_qrs_units,
          string num_unrecognized_qrs_units,
          string num_boats,
          string num_ballistic_vests,
          string num_cases_mre,
          string inventory_memo,
          string key_cell_phone_1_num,
          string key_cell_phone_1_assignee,
          string key_cell_phone_2_num,
          string key_cell_phone_2_assignee,
          string key_cell_phone_3_num,
          string key_cell_phone_3_assignee,
          string key_cell_phone_4_num,
          string key_cell_phone_4_assignee,
          string key_cell_phone_5_num,
          string key_cell_phone_5_assignee,
          string key_cell_phone_6_num,
          string key_cell_phone_6_assignee,
          string radio_channel_1,
          string radio_channel_2,
          string radio_channel_3,
          string radio_channel_4,
          string radio_channel_5,
          string radio_channel_6,
          string primary_response_area,
          string short_name
          )
          {
          var childless_field_assignments_clause = " name = NULLIF('" + name + "','')"
          + " , county_code = NULLIF('" + county_code + "','')"
          + " , business_phone_num = NULLIF('" + business_phone_num + "','')"
          + " , business_fax_num = NULLIF('" + business_fax_num + "','')"
          + " , website_address = NULLIF('" + website_address + "','')"
          + " , charter_kind = NULLIF('" + charter_kind + "','')"
          + " , corpadmin_contact_name = NULLIF('" + corpadmin_contact_name + "','')"
          + " , corpadmin_primary_phone_num = NULLIF('" + corpadmin_primary_phone_num + "','')"
          + " , corpadmin_secondary_phone_num = NULLIF('" + corpadmin_secondary_phone_num + "','')"
          + " , corpadmin_email_address = NULLIF('" + corpadmin_email_address + "','')"
          + " , be_emsof_participant = " + be_emsof_participant.ToString()
          + " , emsof_nonparticipation_reason = NULLIF('" + emsof_nonparticipation_reason + "','')"
          + " , emsof_contact_name = NULLIF('" + emsof_contact_name + "','')"
          + " , emsof_contact_primary_phone_num = NULLIF('" + emsof_contact_primary_phone_num + "','')"
          + " , emsof_contact_sms_phone_num = NULLIF('" + emsof_contact_sms_phone_num + "','')"
          + " , coo_name = NULLIF('" + coo_name + "','')"
          + " , coo_work_phone_num = NULLIF('" + coo_work_phone_num + "','')"
          + " , coo_home_phone_num = NULLIF('" + coo_home_phone_num + "','')"
          + " , coo_email_address = NULLIF('" + coo_email_address + "','')"
          + " , coo_mobile_phone_or_pager_num = NULLIF('" + coo_mobile_phone_or_pager_num + "','')"
          + " , md_name = NULLIF('" + md_name + "','')"
          + " , md_office_phone_num = NULLIF('" + md_office_phone_num + "','')"
          + " , md_home_phone_num = NULLIF('" + md_home_phone_num + "','')"
          + " , md_email_address = NULLIF('" + md_email_address + "','')"
          + " , md_mobile_phone_or_pager_num = NULLIF('" + md_mobile_phone_or_pager_num + "','')"
          + " , physical_street_address_line_1 = NULLIF('" + physical_street_address_line_1 + "','')"
          + " , physical_street_address_line_2 = NULLIF('" + physical_street_address_line_2 + "','')"
          + " , physical_city = NULLIF('" + physical_city + "','')"
          + " , physical_state = NULLIF('" + physical_state + "','')"
          + " , physical_zip_code = NULLIF('" + physical_zip_code + "','')"
          + " , mail_address_line_1 = NULLIF('" + mail_address_line_1 + "','')"
          + " , mail_address_line_2 = NULLIF('" + mail_address_line_2 + "','')"
          + " , mail_city = NULLIF('" + mail_city + "','')"
          + " , mail_state = NULLIF('" + mail_state + "','')"
          + " , mail_zip_code = NULLIF('" + mail_zip_code + "','')"
          + " , be_qrs = " + be_qrs.ToString()
          + " , be_bls_amb = " + be_bls_amb.ToString()
          + " , be_als_amb = " + be_als_amb.ToString()
          + " , be_als_squad = " + be_als_squad.ToString()
          + " , be_air_amb = " + be_air_amb.ToString()
          + " , be_rescue = " + be_rescue.ToString()
          + " , be_pa_turnpike_contractor = " + be_pa_turnpike_contractor.ToString()
          + " , num_doh_licensed_vehicles = NULLIF('" + num_doh_licensed_vehicles + "','')"
          + " , num_ambulances = NULLIF('" + num_ambulances + "','')"
          + " , be_dera = " + be_dera.ToString()
          + " , charter_other_kind = NULLIF('" + charter_other_kind + "','')"
          + " , be_valid_profile = " + be_valid_profile.ToString()
          + " , federal_tax_id_num = NULLIF('" + federal_tax_id + "','')"
          + " , be_qrs_unrecognized = " + be_qrs_unrecognized.ToString()
          + " , be_rescue_unrecognized = " + be_rescue_unrecognized.ToString()
          + " , be_distressed = " + be_distressed.ToString()
          + " , num_staff_typical_als_08_16 = NULLIF('" + num_staff_typical_als_08_16 +  "', '')"
          + " , num_staff_typical_als_16_00 = NULLIF('" + num_staff_typical_als_16_00 +  "', '')"
          + " , num_staff_typical_als_00_08 = NULLIF('" + num_staff_typical_als_00_08 +  "', '')"
          + " , num_staff_typical_bls_08_16 = NULLIF('" + num_staff_typical_bls_08_16 +  "', '')"
          + " , num_staff_typical_bls_16_00 = NULLIF('" + num_staff_typical_bls_16_00 +  "', '')"
          + " , num_staff_typical_bls_00_08 = NULLIF('" + num_staff_typical_bls_00_08 +  "', '')"
          + " , num_units_typical_als_08_16 = NULLIF('" + num_units_typical_als_08_16 +  "', '')"
          + " , num_units_typical_als_16_00 = NULLIF('" + num_units_typical_als_16_00 +  "', '')"
          + " , num_units_typical_als_00_08 = NULLIF('" + num_units_typical_als_00_08 +  "', '')"
          + " , num_units_typical_bls_08_16 = NULLIF('" + num_units_typical_bls_08_16 +  "', '')"
          + " , num_units_typical_bls_16_00 = NULLIF('" + num_units_typical_bls_16_00 +  "', '')"
          + " , num_units_typical_bls_00_08 = NULLIF('" + num_units_typical_bls_00_08 +  "', '')"
          + " , num_n95_masks = NULLIF('" + num_n95_masks +  "', '')"
          + " , num_scba = NULLIF('" + num_scba +  "', '')"
          + " , num_pfd = NULLIF('" + num_pfd +  "', '')"
          + " , num_portable_vents = NULLIF('" + num_portable_vents +  "', '')"
          + " , num_infusion_pumps = NULLIF('" + num_infusion_pumps +  "', '')"
          + " , num_bariatric_stretchers = NULLIF('" + num_bariatric_stretchers +  "', '')"
          + " , num_golf_carts = NULLIF('" + num_golf_carts +  "', '')"
          + " , num_gators = NULLIF('" + num_gators +  "', '')"
          + " , num_quads = NULLIF('" + num_quads +  "', '')"
          + " , num_mci_special_event_trailers = NULLIF('" + num_mci_special_event_trailers +  "', '')"
          + " , num_bariatric_equipped_units = NULLIF('" + num_bariatric_equipped_units +  "', '')"
          + " , num_wheelchair_vans = NULLIF('" + num_wheelchair_vans +  "', '')"
          + " , num_opensky_radios = NULLIF('" + num_opensky_radios +  "', '')"
          + " , num_uhf_portables = NULLIF('" + num_uhf_portables +  "', '')"
          + " , num_uhf_mobiles = NULLIF('" + num_uhf_mobiles +  "', '')"
          + " , num_vhf_portables = NULLIF('" + num_vhf_portables +  "', '')"
          + " , num_vhf_mobiles = NULLIF('" + num_vhf_mobiles +  "', '')"
          + " , num_trunked_radios = NULLIF('" + num_trunked_radios +  "', '')"
          + " , num_gps_receivers = NULLIF('" + num_gps_receivers +  "', '')"
          + " , num_mark_1_kits = NULLIF('" + num_mark_1_kits +  "', '')"
          + " , num_cases_potable_h2o = NULLIF('" + num_cases_potable_h2o +  "', '')"
          + " , num_recognized_rescue_units = NULLIF('" + num_recognized_rescue_units +  "', '')"
          + " , num_4wd_ambulances = NULLIF('" + num_4wd_ambulances +  "', '')"
          + " , num_recognized_qrs_units = NULLIF('" + num_recognized_qrs_units +  "', '')"
          + " , num_unrecognized_qrs_units = NULLIF('" + num_unrecognized_qrs_units +  "', '')"
          + " , num_boats = NULLIF('" + num_boats +  "', '')"
          + " , num_ballistic_vests = NULLIF('" + num_ballistic_vests +  "', '')"
          + " , num_cases_mre = NULLIF('" + num_cases_mre +  "', '')"
          + " , inventory_memo = NULLIF('" + inventory_memo +  "', '')"
          + " , key_cell_phone_1_num = NULLIF('" + key_cell_phone_1_num +  "', '')"
          + " , key_cell_phone_1_assignee = NULLIF('" + key_cell_phone_1_assignee +  "', '')"
          + " , key_cell_phone_2_num = NULLIF('" + key_cell_phone_2_num +  "', '')"
          + " , key_cell_phone_2_assignee = NULLIF('" + key_cell_phone_2_assignee +  "', '')"
          + " , key_cell_phone_3_num = NULLIF('" + key_cell_phone_3_num +  "', '')"
          + " , key_cell_phone_3_assignee = NULLIF('" + key_cell_phone_3_assignee +  "', '')"
          + " , key_cell_phone_4_num = NULLIF('" + key_cell_phone_4_num +  "', '')"
          + " , key_cell_phone_4_assignee = NULLIF('" + key_cell_phone_4_assignee +  "', '')"
          + " , key_cell_phone_5_num = NULLIF('" + key_cell_phone_5_num +  "', '')"
          + " , key_cell_phone_5_assignee = NULLIF('" + key_cell_phone_5_assignee +  "', '')"
          + " , key_cell_phone_6_num = NULLIF('" + key_cell_phone_6_num +  "', '')"
          + " , key_cell_phone_6_assignee = NULLIF('" + key_cell_phone_6_assignee +  "', '')"
          + " , radio_channel_1 = NULLIF('" + radio_channel_1 +  "', '')"
          + " , radio_channel_2 = NULLIF('" + radio_channel_2 +  "', '')"
          + " , radio_channel_3 = NULLIF('" + radio_channel_3 +  "', '')"
          + " , radio_channel_4 = NULLIF('" + radio_channel_4 +  "', '')"
          + " , radio_channel_5 = NULLIF('" + radio_channel_5 +  "', '')"
          + " , radio_channel_6 = NULLIF('" + radio_channel_6 +  "', '')"
          + " , primary_response_area = NULLIF('" + primary_response_area + "','')"
          + " , short_name = NULLIF('" + short_name + "','')"
          ;
          db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate
            (
            target_table_name:"service",
            key_field_name:"affiliate_num",
            key_field_value:affiliate_num,
            childless_field_assignments_clause:childless_field_assignments_clause
            );
          }

    internal void SetShortName
      (
      string id,
      string value
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand("update service set short_name = '" + value + "' where id = '" + id + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void SetStrikeTeamParticipation
      (
      string id,
      string level_id
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand("update service set strike_team_participation_level_id = '" + level_id + "' where id = '" + id + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public string ShortNameOf(string service_id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select short_name from service where id = '" + service_id + "'",connection);
      var short_name_of = my_sql_command.ExecuteScalar().ToString();
      Close();
      return short_name_of;
      }

    internal string ShortNameOfSummary(object summary)
      {
      return (summary as service_summary).short_name;
      }

    public Queue<string> StrikeTeamParticipantIdQ(string included_descriptions_csv_clause = "Standing")
      {
      var strike_team_participant_id_q = new Queue<string>();
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select service.id as id"
        + " from service"
        +   " join county_region_map on (county_region_map.county_code=service.county_code)"
        +   " join region_code_name_map on (region_code_name_map.code=county_region_map.region_code)"
        + " where strike_team_participation_level_id in (select id from strike_team_participation_level where description in ('" + included_descriptions_csv_clause + "'))"
        +   "  and be_pacrat_subscriber",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        strike_team_participant_id_q.Enqueue(dr["id"].ToString());
        }
      Close();
      return strike_team_participant_id_q;
      }

    internal string StrikeTeamParticipationLevelIdOf(object summary)
      {
      return (summary as service_summary).strike_team_participation_level_id;
      }

    internal object Summary(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT name"
        + " , short_name"
        + " , affiliate_num"
        + " , strike_team_participation_level_id"
        + " FROM service"
        + " where id = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new service_summary()
        {
        id = id,
        name = dr["name"].ToString(),
        short_name = dr["short_name"].ToString(),
        affiliate_num = dr["affiliate_num"].ToString(),
        strike_team_participation_level_id = dr["strike_team_participation_level_id"].ToString()
        };
      Close();
      return the_summary;
      }

        internal Queue SusceptibleTo(milestone_type milestone)
          {
          var id_q = new Queue();
          if (milestone == milestone_type.SERVICE_ANNUAL_SURVEY_SUBMISSION_DEADLINE)
            {
            Open();
            using var my_sql_command = new MySqlCommand("select id from service where not be_valid_profile", connection);
            var dr = my_sql_command.ExecuteReader();
            while (dr.Read())
              {
              id_q.Enqueue(dr["id"]);
              }
            Close();
            }
          return id_q;
          }

    } // end TClass_db_services

  }
