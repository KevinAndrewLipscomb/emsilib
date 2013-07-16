using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI.WebControls;

namespace Class_db_members
{
    // const
    // TCCI_ID = 0;
    // TCCI_LAST_NAME = 1;
    // TCCI_FIRST_NAME = 2;
    // TCCI_CAD_NUM = 3;
    // TCCI_SECTION_NUM = 4;
    // TCCI_MEDICAL_RELEASE_PECK_CODE = 5;
    // TCCI_MEDICAL_RELEASE_LEVEL = 6;
    // TCCI_BE_DRIVER_QUALIFIED = 7;
    // TCCI_ENROLLMENT = 8;
    // TCCI_ENROLLMENT_OBLIGATION = 9;
    // TCCI_LEAVE = 10;
    // TCCI_OBLIGED_SHIFTS = 11;
    // TCCI_EMAIL_ADDRESS = 12;
    public class TClass_db_members: TClass_db
    {
        private TClass_db_trail db_trail = null;
        //Constructor  Create()
        public TClass_db_members() : base()
        {
            // TODO: Add any constructor code here
            db_trail = new TClass_db_trail();
        }
        // procedure Add
        // (
        // attributes..
        // );
        // function BeKnown
        // (
        // attributes..
        // )
        // : boolean;

        internal bool BeRoleHolderBySharedSecret
          (
          string certification_number,
          string level_id,
          string regional_council_code,
          DateTime birth_date,
          out string claimed_role_name,
          out string claimed_member_name,
          out string claimed_member_id,
          out string claimed_member_email_address
          )
          {
          var be_role_holder_by_shared_secret = false;
          claimed_role_name = k.EMPTY;
          claimed_member_name = k.EMPTY;
          claimed_member_id = k.EMPTY;
          claimed_member_email_address = k.EMPTY;
          Open();
          var dr = new MySqlCommand
            (
            "select role.name as role_name"
            + " , concat(member.first_name,' ',member.last_name) as member_name"
            + " , member.id as member_id"
            + " , IFNULL(email_address,'') as email_address"
            + " from member"
            +   " join role_member_map on (role_member_map.member_id=member.id)"
            +   " join role on (role.id=role_member_map.role_id)"
            +   " join user_member_map on (user_member_map.member_id=member.id)"
            + " where certification_number = '" + certification_number + "'"
            +   " and level_id = '" + level_id + "'"
            +   " and regional_council_code = '" + regional_council_code + "'"
            +   " and birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'"
            + " order by role.pecking_order"
            + " limit 1",
            connection
            )
            .ExecuteReader();
          if (dr.Read())
            {
            claimed_role_name = dr["role_name"].ToString();
            claimed_member_name = dr["member_name"].ToString();
            claimed_member_id = dr["member_id"].ToString();
            claimed_member_email_address = dr["email_address"].ToString();
            be_role_holder_by_shared_secret = true;
            }
          dr.Close();
          Close();
          return be_role_holder_by_shared_secret;
          }

        public bool BeValidProfile(string id)
        {
            bool result;
            this.Open();
            result = ("1" == new MySqlCommand("select be_valid_profile from member where id = " + id, this.connection).ExecuteScalar().ToString());
            this.Close();
            return result;
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            MySqlDataReader dr;
            ((target) as ListControl).Items.Clear();
            if (unselected_literal != k.EMPTY)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            this.Open();
            dr = new MySqlCommand("select member.id as member_id" + " , concat(last_name,\", \",first_name) as member_designator" + " from member" + " order by member_designator", this.connection).ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["member_designator"].ToString(), dr["member_id"].ToString()));
            }
            dr.Close();
            this.Close();
            if (selected_value != k.EMPTY)
            {
                ((target) as ListControl).SelectedValue = selected_value;
            }

        }

        public void BindDirectToListControl(object target)
        {
            BindDirectToListControl(target, "-- Member --");
        }

        public void BindDirectToListControl(object target, string unselected_literal)
        {
            BindDirectToListControl(target, unselected_literal, k.EMPTY);
        }

        public string EmailAddressOf(string member_id)
        {
            string result;
            object email_address_obj;
            this.Open();
            email_address_obj = new MySqlCommand("select email_address from member where id = " + member_id, this.connection).ExecuteScalar().ToString();
            if (email_address_obj != null)
            {
                result = email_address_obj.ToString();
            }
            else
            {
                result = k.EMPTY;
            }
            this.Close();
            return result;
        }

        public string FirstNameOfMemberId(string member_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select first_name from member where id = \"" + member_id + "\"", this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public string IdOfUserId(string user_id)
        {
            string result;
            object member_id_obj;
            this.Open();
            member_id_obj = new MySqlCommand("select member_id from user_member_map where user_id = " + user_id, this.connection).ExecuteScalar();
            if (member_id_obj != null)
            {
                result = member_id_obj.ToString();
            }
            else
            {
                result = k.EMPTY;
            }
            this.Close();
            return result;
        }

        public string LastNameOfMemberId(string member_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select last_name from member where id = \"" + member_id + "\"", this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public void SetEmailAddress(string id, string email_address)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("UPDATE member" + " SET email_address = \"" + email_address + "\"" + " WHERE id = " + id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public string UserIdOf(string member_id)
        {
            string result;
            object user_id_obj;
            this.Open();
            user_id_obj = new MySqlCommand("select user_id from user_member_map where member_id = " + member_id, this.connection).ExecuteScalar();
            if (user_id_obj != null)
            {
                result = user_id_obj.ToString();
            }
            else
            {
                result = k.EMPTY;
            }
            this.Close();
            return result;
        }

    } // end TClass_db_members

}
