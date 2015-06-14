using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI.WebControls;

namespace Class_db_members
  {

  public class TClass_db_members: TClass_db
    {

    private class member_summary
      {
      public string id;
      public string last_name;
      public string first_name;
      public string middle_initial;
      public string level_description;
      public string certification_number;
      public string birth_date;
      public string email_address;
      }

    private TClass_db_trail db_trail = null;

    public TClass_db_members() : base()
      {
      db_trail = new TClass_db_trail();
      }

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

    public bool Bind
      (
      string partial_spec,
      object target
      )
      {
      var concat_clause = "concat(IFNULL(last_name,'-'),'|',IFNULL(first_name,'-'),'|',IFNULL(certification_number,'nocert'),'|',IFNULL(birth_date,'-'))";
      Open();
      ((target) as ListControl).Items.Clear();
      var dr = new MySqlCommand
        (
        "select id"
        + " , CONVERT(" + concat_clause + " USING utf8) as spec"
        + " from member"
        + " where " + concat_clause + " like '%" + partial_spec.ToUpper() + "%'"
        + " order by spec",
        connection
        )
        .ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      return ((target) as ListControl).Items.Count > 0;
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

    public void BindDirectToListControlForRoster
      (
      object target,
      string region_code,
      string starting_with,
      k.int_positive limit,
      bool do_limit_to_21_yo_or_older
      )
      {
      Open();
      ((target) as ListControl).Items.Clear();
      var matching_clause = k.EMPTY;
      if (starting_with.Length > 0)
        {
        if ((starting_with[0].CompareTo('0') > -1) && (starting_with[0].CompareTo('9') < 1))
          {
          matching_clause = " and certification_number like '" + starting_with + "%'";
          }
        else
          {
          matching_clause = " and CONVERT(concat(last_name,', ',first_name,' ',middle_initial,', ',IFNULL(certification_number,'nocert'),', ',IFNULL(birth_date,'-')) USING utf8) like '" + starting_with + "%'";
          }
        }
      if (do_limit_to_21_yo_or_older)
        {
        matching_clause += " and ADDDATE(birth_date,INTERVAL 21 YEAR) <= CURDATE()";
        }
      var dr = new MySqlCommand
        (
        "SELECT id"
        + " , CONVERT(concat(last_name,', ',first_name,' ',middle_initial,', ',IFNULL(certification_number,'nocert'),', ',IFNULL(DATE_FORMAT(birth_date,'%m/%d/%Y'),'-')) USING utf8) as spec"
        + " FROM member"
        + " where not be_past" + matching_clause
        + " order by spec"
        + " limit " + limit.val,
        connection
        )
        .ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      }
    public void BindDirectToListControlForRoster(object target,string region_code,string starting_with,k.int_positive limit)
      {
      BindDirectToListControlForRoster(target,region_code,starting_with,limit,do_limit_to_21_yo_or_older:false);
      }

    internal string CertificationNumberOf(object summary)
      {
      return (summary as member_summary).certification_number;
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        new MySqlCommand(db_trail.Saved("delete from member where id = '" + id + "'"), connection).ExecuteNonQuery();
        }
      catch(System.Exception e)
        {
        if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
          {
          result = false;
          }
        else
          {
          throw e;
          }
        }
      Close();
      return result;
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

    internal string FirstNameOf(object summary)
      {
      return (summary as member_summary).first_name;
      }

        public string FirstNameOfMemberId(string member_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select first_name from member where id = \"" + member_id + "\"", this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

    public bool Get
      (
      string id,
      out string last_name,
      out string first_name,
      out string middle_initial,
      out string certification_number,
      out string level_id,
      out string regional_council_code,
      out DateTime birth_date,
      out string email_address,
      out bool be_stale,
      out string residence_county_code,
      out bool be_birth_date_confirmed,
      out string street_address_1,
      out string street_address_2,
      out string city_state_zip,
      out bool be_instructor,
      out bool be_past
      )
      {
      last_name = k.EMPTY;
      first_name = k.EMPTY;
      middle_initial = k.EMPTY;
      certification_number = k.EMPTY;
      level_id = k.EMPTY;
      regional_council_code = k.EMPTY;
      birth_date = DateTime.MinValue;
      email_address = k.EMPTY;
      be_stale = true;
      residence_county_code = k.EMPTY;
      be_birth_date_confirmed = true;
      street_address_1 = k.EMPTY;
      street_address_2 = k.EMPTY;
      city_state_zip = k.EMPTY;
      be_instructor = false;
      be_past = false;
      var result = false;
      //
      Open();
      var dr = new MySqlCommand("select * from member where CAST(id AS CHAR) = '" + id + "'", connection).ExecuteReader();
      if (dr.Read())
        {
        last_name = dr["last_name"].ToString();
        first_name = dr["first_name"].ToString();
        middle_initial = dr["middle_initial"].ToString();
        certification_number = dr["certification_number"].ToString();
        level_id = dr["level_id"].ToString();
        regional_council_code = dr["regional_council_code"].ToString();
        birth_date = DateTime.Parse(dr["birth_date"].ToString());
        email_address = dr["email_address"].ToString();
        be_stale = (dr["be_stale"].ToString() == "1");
        residence_county_code = dr["residence_county_code"].ToString();
        be_birth_date_confirmed = (dr["be_birth_date_confirmed"].ToString() == "1");
        street_address_1 = dr["street_address_1"].ToString();
        street_address_2 = dr["street_address_2"].ToString();
        city_state_zip = dr["city_state_zip"].ToString();
        be_instructor = (dr["be_instructor"].ToString() == "1");
        be_past = (dr["be_past"].ToString() == "1");
        result = true;
        }
      dr.Close();
      Close();
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

    internal string LastNameOf(object summary)
      {
      return (summary as member_summary).last_name;
      }

        public string LastNameOfMemberId(string member_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select last_name from member where id = \"" + member_id + "\"", this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

    internal string LevelOf(object summary)
      {
      return (summary as member_summary).level_description;
      }

    public k.int_nonnegative MaxSpecLength
      (
      string region_code,
      string starting_with
      )
      {
      var max_spec_length = new k.int_nonnegative();
      Open();
      max_spec_length.val = int.Parse
        (
        new MySqlCommand
          (
          "SELECT IFNULL(max(length(CONVERT(concat(last_name,', ',first_name,' ',middle_initial,', ',IFNULL(certification_number,'nocert'),', ',IFNULL(birth_date,'-')) USING utf8))),0)"
          + " FROM member"
          + (region_code.Length > 0 ? " where regional_council_code = '" + region_code + "'" : k.EMPTY),
          connection
          )
          .ExecuteScalar().ToString()
        );
      Close();
      return max_spec_length;
      }

        public void SetEmailAddress(string id, string email_address)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("UPDATE member" + " SET email_address = \"" + email_address + "\"" + " WHERE id = " + id), this.connection).ExecuteNonQuery();
            this.Close();
        }

    public void SetFieldsNotImportedFromState
      (
      string id,
      DateTime birth_date,
      string email_address
      )
      {
      Open();
      new MySqlCommand
        (
        db_trail.Saved
          (
          "update member set birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'"
          + " , email_address = NULLIF('" + email_address + "','')"
          + " where id = '" + id + "'"
          ),
          connection
        )
        .ExecuteNonQuery();
      Close();
      }

    internal object Summary(string id)
      {
      Open();
      var dr =
        (
        new MySqlCommand
          (
          "SELECT email_address"
          + " , last_name"
          + " , first_name"
          + " , middle_initial"
          + " , IFNULL(practitioner_level.short_description,'') as level_description"
          + " , IFNULL(certification_number,'nocert') as certification_number"
          + " , DATE_FORMAT(birth_date,'%Y-%m-%d') as birth_date"
          + " FROM member"
          +   " left join practitioner_level on (practitioner_level.id=member.level_id)"
          + " where member.id = '" + id + "'",
          connection
          )
          .ExecuteReader()
        );
      dr.Read();
      var the_summary = new member_summary()
        {
        id = id,
        email_address = dr["email_address"].ToString(),
        last_name = dr["last_name"].ToString(),
        first_name = dr["first_name"].ToString(),
        middle_initial = dr["middle_initial"].ToString(),
        level_description = dr["level_description"].ToString(),
        certification_number = dr["certification_number"].ToString(),
        birth_date = dr["birth_date"].ToString()
        };
      Close();
      return the_summary;
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
