using Class_biz_milestones;
using Class_db_appropriations;
using Class_db_services;
using kix;
using System.Collections;
using System.Web;

namespace Class_biz_services
  {
  public class TClass_biz_services
    {
        private readonly TClass_db_appropriations db_appropriations = null;
        private readonly TClass_db_services db_services = null;

        //Constructor  Create()
        public TClass_biz_services() : base()
        {
            db_appropriations = new TClass_db_appropriations();
            db_services = new TClass_db_services();
        }

        public string AffiliateNumOf(object summary)
          {
          return db_services.AffiliateNumOf(summary);
          }

        public string AffiliateNumOfId(string id)
        {
            string result;
            result = db_services.AffiliateNumOfId(id);
            return result;
        }

        public bool BeAdded(string service_name, string affiliate_num, string password_reset_email_address, string county_code)
        {
            bool result;
            result = db_services.BeAdded(service_name, affiliate_num, password_reset_email_address, county_code);
            return result;
        }

        public bool BeDistressed(string id)
        {
            bool result;
            result = db_services.BeDistressed(id);
            return result;
        }

        public bool BeKnown(string affiliate_num)
        {
            bool result;
            result = db_services.BeKnown(affiliate_num);
            return result;
        }

        public bool BeOkToDelete()
        {
            bool result;
            result = HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("emsof-planner");
            return result;
        }

        public bool BeValidAndParticipating(string id)
        {
            bool result;
            result = db_services.BeValidAndParticipating(id);
            return result;
        }

        public bool Bind(string partial_affiliate_num, object target)
        {
            bool result;
            result = db_services.Bind(partial_affiliate_num, target);
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
          db_services.BindAnnualRespondents(sort_order, be_order_ascending, target, region_code, county_code);
          }
        public void BindAnnualRespondents(string sort_order, bool be_order_ascending, object target, string region_code)
          {
          BindAnnualRespondents(sort_order,be_order_ascending,target,region_code,county_code:k.EMPTY);
          }

    public void BindBaseDataListOfAdHocStrikeTeams
      (
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string service_strike_team_management_footprint
      )
      {
      db_services.BindBaseDataListOfAdHocStrikeTeams(sort_order,be_sort_order_ascending,target,service_strike_team_management_footprint);
      }

        public void BindListControl(string county_user_id, object target, bool be_unfiltered, bool be_inclusive_of_invalids_and_nonparticipants)
        {
            db_services.BindListControl(county_user_id, target, be_unfiltered, be_inclusive_of_invalids_and_nonparticipants);
        }

        public void BindListControl(string county_user_id, object target)
        {
            BindListControl(county_user_id, target, false);
        }

        public void BindListControl(string county_user_id, object target, bool be_unfiltered)
        {
            BindListControl(county_user_id, target, be_unfiltered, false);
        }

        public void BindPacratManagementBaseDataList
          (
          string sort_order,
          bool be_sort_order_ascending,
          object target,
          string region_code,
          bool do_include_all_services
          )
          {
          db_services.BindPacratManagementBaseDataList(sort_order,be_sort_order_ascending,target,region_code,do_include_all_services);
          }

    public void BindStrikeTeamAffiliationBaseDataList
      (
      string member_id,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      db_services.BindStrikeTeamAffiliationBaseDataList(member_id,sort_order,be_sort_order_ascending,target);
      }

    public void BindStrikeTeamMobilizationAnnouncementListControl
      (
      string region_code,
      object target
      )
      {
      db_services.BindStrikeTeamMobilizationAnnouncementListControl(region_code,target);
      }

    public void DeactivateAdHocParticipants(string region_code)
      {
      db_services.DeactivateAdHocServices(region_code);
      }

        public bool Delete(string affiliate_num)
        {
            bool result;
            result = db_services.Delete(affiliate_num);
            return result;
        }

    public string DesignatorWithCompetingShortName
      (
      string affiliate_num,
      string short_name
      )
      {
      return db_services.DesignatorWithCompetingShortName(affiliate_num,short_name);
      }

        public string EmailTargetForCounty(string county_id, bool be_filtered_by_valid_profile, bool be_valid_profile, bool be_filtered_by_emsof_participation, bool be_emsof_participant)
          {
          return db_services.EmailTargetForCounty(county_id, be_filtered_by_valid_profile, be_valid_profile, be_filtered_by_emsof_participation, be_emsof_participant);
          }
        public string EmailTargetForCounty(string county_id, bool be_filtered_by_valid_profile, bool be_valid_profile)
          {
          return EmailTargetForCounty(county_id, be_filtered_by_valid_profile, be_valid_profile, false, false);
          }
        public string EmailTargetForCounty(string county_id)
          {
          return EmailTargetForCounty(county_id, false, false);
          }

        public string EmsofCoordinatorNameOf(string id)
          {
          return db_services.EmsofCoordinatorNameOf(id);
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
          return db_services.Get
            (
            affiliate_num,
            out id,
            out name,
            out county_code,
            out business_phone_num,
            out business_fax_num,
            out website_address,
            out charter_kind,
            out corpadmin_contact_name,
            out corpadmin_primary_phone_num,
            out corpadmin_secondary_phone_num,
            out corpadmin_email_address,
            out be_emsof_participant,
            out emsof_nonparticipation_reason,
            out emsof_contact_name,
            out emsof_contact_primary_phone_num,
            out emsof_contact_sms_phone_num,
            out coo_name,
            out coo_work_phone_num,
            out coo_home_phone_num,
            out coo_email_address,
            out coo_mobile_phone_or_pager_num,
            out md_name,
            out md_office_phone_num,
            out md_home_phone_num,
            out md_email_address,
            out md_mobile_phone_or_pager_num,
            out physical_street_address_line_1,
            out physical_street_address_line_2,
            out physical_city,
            out physical_state,
            out physical_zip_code,
            out mail_address_line_1,
            out mail_address_line_2,
            out mail_city,
            out mail_state,
            out mail_zip_code,
            out be_qrs,
            out be_bls_amb,
            out be_als_amb,
            out be_als_squad,
            out be_air_amb,
            out be_rescue,
            out be_pa_turnpike_contractor,
            out num_doh_licensed_vehicles,
            out num_ambulances,
            out be_dera,
            out charter_other_kind,
            out be_valid_profile,
            out federal_tax_id,
            out be_qrs_unrecognized,
            out be_rescue_unrecognized,
            out be_distressed,
            out num_staff_typical_als_08_16,
            out num_staff_typical_als_16_00,
            out num_staff_typical_als_00_08,
            out num_staff_typical_bls_08_16,
            out num_staff_typical_bls_16_00,
            out num_staff_typical_bls_00_08,
            out num_units_typical_als_08_16,
            out num_units_typical_als_16_00,
            out num_units_typical_als_00_08,
            out num_units_typical_bls_08_16,
            out num_units_typical_bls_16_00,
            out num_units_typical_bls_00_08,
            out num_n95_masks,
            out num_scba,
            out num_pfd,
            out num_portable_vents,
            out num_infusion_pumps,
            out num_bariatric_stretchers,
            out num_golf_carts,
            out num_gators,
            out num_quads,
            out num_mci_special_event_trailers,
            out num_bariatric_equipped_units,
            out num_wheelchair_vans,
            out num_opensky_radios,
            out num_uhf_portables,
            out num_uhf_mobiles,
            out num_vhf_portables,
            out num_vhf_mobiles,
            out num_trunked_radios,
            out num_gps_receivers,
            out num_mark_1_kits,
            out num_cases_potable_h2o,
            out num_recognized_rescue_units,
            out num_4wd_ambulances,
            out num_recognized_qrs_units,
            out num_unrecognized_qrs_units,
            out num_boats,
            out num_ballistic_vests,
            out num_cases_mre,
            out inventory_memo,
            out key_cell_phone_1_num,
            out key_cell_phone_1_assignee,
            out key_cell_phone_2_num,
            out key_cell_phone_2_assignee,
            out key_cell_phone_3_num,
            out key_cell_phone_3_assignee,
            out key_cell_phone_4_num,
            out key_cell_phone_4_assignee,
            out key_cell_phone_5_num,
            out key_cell_phone_5_assignee,
            out key_cell_phone_6_num,
            out key_cell_phone_6_assignee,
            out radio_channel_1,
            out radio_channel_2,
            out radio_channel_3,
            out radio_channel_4,
            out radio_channel_5,
            out radio_channel_6,
            out primary_response_area,
            out short_name
            );
          }

        public string IdOf(object summary)
          {
          return db_services.IdOf(summary);
          }

        public string IdOfAffiliateNum(string affiliate_num)
          {
          return db_services.IdOfAffiliateNum(affiliate_num);
          }

        public void MarkProfilesStale()
        {
            db_services.MarkProfilesStale();
        }

        public string NameOf(string service_id)
        {
            string result;
            result = db_services.NameOf(service_id);
            return result;
        }

        public string NameOfSummary(object summary)
          {
          return db_services.NameOfSummary(summary);
          }

        public string RegionCodeOf(string service_id)
          {
          return db_services.RegionCodeOf(service_id);
          }

        public string ServiceStrikeTeamManagementFootprintOf(string member_id)
          {
          return db_services.ServiceStrikeTeamManagementFootprintOf(member_id);
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
          db_services.Set
            (
            affiliate_num,
            name,
            county_code,
            business_phone_num,
            business_fax_num,
            website_address,
            charter_kind,
            corpadmin_contact_name,
            corpadmin_primary_phone_num,
            corpadmin_secondary_phone_num,
            corpadmin_email_address,
            be_emsof_participant,
            emsof_nonparticipation_reason,
            emsof_contact_name,
            emsof_contact_primary_phone_num,
            emsof_contact_sms_phone_num,
            coo_name,
            coo_work_phone_num,
            coo_home_phone_num,
            coo_email_address,
            coo_mobile_phone_or_pager_num,
            md_name,
            md_office_phone_num,
            md_home_phone_num,
            md_email_address,
            md_mobile_phone_or_pager_num,
            physical_street_address_line_1,
            physical_street_address_line_2,
            physical_city,
            physical_state,
            physical_zip_code,
            mail_address_line_1,
            mail_address_line_2,
            mail_city,
            mail_state,
            mail_zip_code,
            be_qrs,
            be_bls_amb,
            be_als_amb,
            be_als_squad,
            be_air_amb,
            be_rescue,
            be_pa_turnpike_contractor,
            num_doh_licensed_vehicles,
            num_ambulances,
            be_dera,
            charter_other_kind,
            be_valid_profile,
            federal_tax_id,
            be_qrs_unrecognized,
            be_rescue_unrecognized,
            be_distressed,
            num_staff_typical_als_08_16,
            num_staff_typical_als_16_00,
            num_staff_typical_als_00_08,
            num_staff_typical_bls_08_16,
            num_staff_typical_bls_16_00,
            num_staff_typical_bls_00_08,
            num_units_typical_als_08_16,
            num_units_typical_als_16_00,
            num_units_typical_als_00_08,
            num_units_typical_bls_08_16,
            num_units_typical_bls_16_00,
            num_units_typical_bls_00_08,
            num_n95_masks,
            num_scba,
            num_pfd,
            num_portable_vents,
            num_infusion_pumps,
            num_bariatric_stretchers,
            num_golf_carts,
            num_gators,
            num_quads,
            num_mci_special_event_trailers,
            num_bariatric_equipped_units,
            num_wheelchair_vans,
            num_opensky_radios,
            num_uhf_portables,
            num_uhf_mobiles,
            num_vhf_portables,
            num_vhf_mobiles,
            num_trunked_radios,
            num_gps_receivers,
            num_mark_1_kits,
            num_cases_potable_h2o,
            num_recognized_rescue_units,
            num_4wd_ambulances,
            num_recognized_qrs_units,
            num_unrecognized_qrs_units,
            num_boats,
            num_ballistic_vests,
            num_cases_mre,
            inventory_memo,
            key_cell_phone_1_num,
            key_cell_phone_1_assignee,
            key_cell_phone_2_num,
            key_cell_phone_2_assignee,
            key_cell_phone_3_num,
            key_cell_phone_3_assignee,
            key_cell_phone_4_num,
            key_cell_phone_4_assignee,
            key_cell_phone_5_num,
            key_cell_phone_5_assignee,
            key_cell_phone_6_num,
            key_cell_phone_6_assignee,
            radio_channel_1,
            radio_channel_2,
            radio_channel_3,
            radio_channel_4,
            radio_channel_5,
            radio_channel_6,
            primary_response_area,
            short_name
            );
          db_appropriations.ApplyToExisting(affiliate_num, be_distressed);
          }

    public void SetShortName
      (
      string id,
      string value
      )
      {
      db_services.SetShortName(id,value);
      }

    public void SetStrikeTeamParticipation
      (
      string id,
      string level_id
      )
      {
      db_services.SetStrikeTeamParticipation(id,level_id);
      }

    public string ShortNameOf(string service_id)
      {
      return db_services.ShortNameOf(service_id);
      }

    public string ShortNameOfSummary(object summary)
      {
      return db_services.ShortNameOfSummary(summary);
      }

    public string StrikeTeamParticipationLevelIdOf(object summary)
      {
      return db_services.StrikeTeamParticipationLevelIdOf(summary);
      }

        public object Summary(string id)
          {
          return db_services.Summary(id);
          }

        internal Queue SusceptibleTo(milestone_type milestone)
          {
          return db_services.SusceptibleTo(milestone);
          }

    } // end TClass_biz_services

}
