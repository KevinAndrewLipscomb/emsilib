using MySql.Data.MySqlClient;
using kix;
using System;

using System.Web.UI.WebControls;
using Class_db;
using Class_db_trail;
namespace Class_db_accounts
{
    public class TClass_db_accounts: TClass_db
    {
        private TClass_db_trail db_trail = null;
        //Constructor  Create()
        public TClass_db_accounts() : base()
        {
            // TODO: Add any constructor code here
            db_trail = new TClass_db_trail();
        }

    public bool BeOkForConedSponsorToInputRosterByBatch(string id)
      {
      Open();
      var be_ok_for_coned_sponsor_to_input_roster_by_batch = ("1" == new MySqlCommand("select be_ok_to_input_roster_by_batch from coned_sponsor_user where id = '" + id + "'",connection).ExecuteScalar().ToString());
      Close();
      return be_ok_for_coned_sponsor_to_input_roster_by_batch;
      }

    public bool BeOkForConedSponsorToInputRosterByCopy(string id)
      {
      Open();
      var be_ok_for_coned_sponsor_to_input_roster_by_copy = ("1" == new MySqlCommand("select be_ok_to_input_roster_by_copy from coned_sponsor_user where id = '" + id + "'",connection).ExecuteScalar().ToString());
      Close();
      return be_ok_for_coned_sponsor_to_input_roster_by_copy;
      }

        public bool BeStalePassword(string user_kind, string user_id)
        {
            bool result;
            this.Open();
            result = "1" == new MySqlCommand("SELECT be_stale_password FROM " + user_kind + "_user where id=\"" + user_id + "\"", this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        internal void BindConedSponsorsInRegion
          (
          string region_code,
          object target
          )
          {
          Open();
          ((target) as ListControl).Items.Clear();
          ((target) as ListControl).Items.Add(new ListItem("-- Select --", "0"));
          var dr = new MySqlCommand
            (
            "SELECT DISTINCT coned_sponsor_user.id"
            + " , teaching_entity.name as name"
            + " FROM coned_sponsor_user"
            +   " JOIN teaching_entity using (id)"
            +   " JOIN region_code_name_map on (region_code_name_map.emsrs_code=teaching_entity.region)"
            +   " left join coned_offering on (coned_offering.sponsor_id=teaching_entity.emsrs_id)"
            + " WHERE (region_code_name_map.code = '" + region_code + "' and be_active) or (LEFT(coned_offering.class_number,2) = LPAD('" + region_code + "',2,'0'))"
            + " ORDER BY name",
            connection
            )
            .ExecuteReader();
          while (dr.Read())
            {
            ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), "coned_sponsor_" + dr["id"].ToString()));
            }
          dr.Close();
          Close();
          }

        public void BindCountiesInRegion
          (
          string region_code,
          object target
          )
          {
          Open();
          ((target) as ListControl).Items.Clear();
          ((target) as ListControl).Items.Add(new ListItem("-- Select --", "0"));
          var dr = new MySqlCommand
            (
            "SELECT county_user.id as id"
            + " , name"
            + " FROM county_user"
            +   " JOIN county_code_name_map on (county_code_name_map.code=county_user.id)"
            +   " JOIN county_region_map on (county_region_map.county_code=county_code_name_map.code)"
            + " WHERE region_code = '" + region_code + "' and be_active"
            + " ORDER BY name",
            connection
            )
            .ExecuteReader();
          while (dr.Read())
            {
            ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), "county_" + dr["id"].ToString()));
            }
          dr.Close();
          Close();
          }

        public void BindRegionalStaffersInRegion
          (
          string region_code,
          string group_name_list,
          object target
          )
          {
          Open();
          ((target) as ListControl).Items.Clear();
          ((target) as ListControl).Items.Add(new ListItem("-- Select --", "0"));
          var dr = new MySqlCommand
            (
            "SELECT distinct regional_staffer_user.id,last_name,first_name"
            + " FROM regional_staffer_user JOIN regional_staffer using (id)"
            +   " join regional_staffer_role on (regional_staffer_role.user_id=regional_staffer_user.id)"
            +   " join regional_staffer_group on (regional_staffer_group.id=regional_staffer_role.group_id)"
            + " WHERE region_code = '" + region_code + "'"
            +   " and be_active"
            +   " and regional_staffer_group.name in (" + group_name_list + ")"
            + " ORDER BY last_name,first_name",
            connection
            )
            .ExecuteReader();
          while (dr.Read())
            {
            ((target) as ListControl).Items.Add(new ListItem(dr["last_name"].ToString() + k.COMMA_SPACE + dr["first_name"].ToString(), "regional_staffer_" + dr["id"].ToString()));
            }
          dr.Close();
          Close();
          }

        public void BindServicesInRegion
          (
          string region_code,
          object target
          )
          {
          Open();
          ((target) as ListControl).Items.Clear();
          ((target) as ListControl).Items.Add(new ListItem("-- Select --", "0"));
          var dr = new MySqlCommand
            (
            "SELECT service_user.id as id"
            + " , name"
            + " FROM service_user"
            +   " JOIN service using (id)"
            +   " JOIN county_region_map on (county_region_map.county_code=service.county_code)"
            + " WHERE region_code = '" + region_code + "' and be_active"
            + " ORDER BY name",
            connection
            )
            .ExecuteReader();
          while (dr.Read())
            {
            ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), "service_" + dr["id"].ToString()));
            }
          dr.Close();
          Close();
          }

        public string EmailAddressByKindId(string user_kind, string user_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select password_reset_email_address from " + user_kind + "_user where id = " + user_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public string EmailTargetByRegionAndRole
          (
          string region_code,
          string role
          )
          {
          var email_target = k.EMPTY;
          Open();
          var dr = new MySqlCommand
            (
            "select password_reset_email_address"
            + " from regional_staffer_user"
            +   " join regional_staffer_role on (regional_staffer_role.user_id=regional_staffer_user.id)"
            +   " join regional_staffer_group on (regional_staffer_group.id=regional_staffer_role.group_id)"
            +   " join regional_staffer on (regional_staffer.id=regional_staffer_user.id)"
            + " where regional_staffer.region_code = '" + region_code + "'"
            +   " and regional_staffer_group.name = '" + role + "'",
            connection
            )
            .ExecuteReader();
          while (dr.Read())
            {
            email_target = email_target + dr["password_reset_email_address"].ToString() + k.COMMA;
            }
          dr.Close();
          Close();
          return (email_target.Length > 0 ? email_target.Substring(0, email_target.Length - 1) : email_target);
          }

        public bool Exists(string user_kind, string user_id, string encoded_password)
        {
            bool result;
            this.Open();
            result = null != new MySqlCommand("SELECT 1 FROM " + user_kind + "_user" + " where id = " + user_id + " and encoded_password_hash = SHA1('" + encoded_password + "')", this.connection).ExecuteScalar();
            this.Close();
            return result;
        }

        internal void SetPasswordResetEmailAddress
          (
          string kind,
          string id,
          string value
          )
          {
          Open();
          new MySqlCommand(db_trail.Saved("UPDATE " + kind + "_user" + " SET password_reset_email_address = '" + value + "'" + " WHERE id = '" + id  + "'"), connection).ExecuteNonQuery();
          Close();
          }

        public void SetTemporaryPassword(string user_kind, string user_id, string encoded_password)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update " + user_kind + "_user" + " set encoded_password_hash = SHA1('" + encoded_password + "')," + " be_stale_password = TRUE " + " where id = " + user_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

    } // end TClass_db_accounts

}
