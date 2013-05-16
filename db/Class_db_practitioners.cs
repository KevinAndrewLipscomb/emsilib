// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db;
using Class_db_trail;
using external_data_binding.emsams.PractitionerInfo;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;

namespace Class_db_practitioners
  {
  public class TClass_db_practitioners: TClass_db
    {
    private TClass_db_trail db_trail = null;

    public TClass_db_practitioners() : base()
      {
      db_trail = new TClass_db_trail();
      }

    public bool Bind(string partial_spec, object target)
      {
      var concat_clause = "concat(IFNULL(last_name,'-'),'|',IFNULL(first_name,'-'),'|',IFNULL(certification_number,'-'),'|',IFNULL(level_id,'-'),'|',IFNULL(regional_council_code,'-'),'|',IFNULL(birth_date,'-'))";
      Open();
      ((target) as ListControl).Items.Clear();
      var dr = new MySqlCommand
        (
        "select id"
        + " , CONVERT(" + concat_clause + " USING utf8) as spec"
        + " from practitioner"
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

    public void BindDirectToListControl(object target)
      {
      Open();
      ((target) as ListControl).Items.Clear();
      var dr = new MySqlCommand
        (
        "SELECT id"
        + " , CONVERT(concat(IFNULL(last_name,'-'),'|',IFNULL(first_name,'-'),'|',IFNULL(certification_number,'-'),'|',IFNULL(level_id,'-'),'|',IFNULL(regional_council_code,'-'),'|',IFNULL(birth_date,'-')) USING utf8) as spec"
        + " FROM practitioner"
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
      }

    public void BindDirectToListControlForRoster
      (
      object target,
      string region_code,
      string starting_with,
      k.int_positive limit
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
          matching_clause = " and CONVERT(concat(last_name,', ',first_name,' ',middle_initial,', ',certification_number,', ',IFNULL(birth_date,'-')) USING utf8) like '" + starting_with + "%'";
          }
        }
      var dr = new MySqlCommand
        (
        "SELECT id"
        + " , CONVERT(concat(last_name,', ',first_name,' ',middle_initial,', ',certification_number,', ',IFNULL(DATE_FORMAT(birth_date,'%m/%d/%Y'),'-')) USING utf8) as spec"
        + " FROM practitioner"
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

    public void ClearBeInstructorFlagsInSubscriberRegions()
      {
      Open();
      new MySqlCommand
        (
        "update practitioner"
        + " join region_code_name_map on (region_code_name_map.code=practitioner.regional_council_code)"
        + " set be_instructor = FALSE"
        + " where be_conedlink_subscriber"
        +   " and code not in (15)",  //Don't clear flags when EMSRS login access to associated regions has been lost.
        connection
        )
        .ExecuteNonQuery();
      Close();
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        new MySqlCommand(db_trail.Saved("delete from practitioner where id = \"" + id + "\""), connection).ExecuteNonQuery();
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
      var dr = new MySqlCommand("select * from practitioner where CAST(id AS CHAR) = '" + id + "'", connection).ExecuteReader();
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

    public void GetForAttendanceRec
      (
      string id,
      out string certification_number,
      out string birth_date,
      out string email_address,
      out string first_name,
      out string last_name,
      out string level_emsrs_code,
      out string level_short_description,
      out string middle_initial
      )
      {
      Open();
      var dr = new MySqlCommand
        (
        "select certification_number"
        + " , DATE_FORMAT(birth_date,'%m/%d/%Y') as birth_date"
        + " , email_address"
        + " , first_name"
        + " , last_name"
        + " , middle_initial"
        + " , emsrs_code as level_emsrs_code"
        + " , short_description as level_short_description"
        + " from practitioner"
        +   " join practitioner_level on (practitioner_level.id=practitioner.level_id)"
        + " where CAST(practitioner.id AS CHAR) = '" + id + "'",
        connection
        )
        .ExecuteReader();
      dr.Read();
      certification_number = dr["certification_number"].ToString();
      birth_date = dr["birth_date"].ToString();
      email_address = dr["email_address"].ToString();
      first_name = dr["first_name"].ToString();
      last_name = dr["last_name"].ToString();
      level_emsrs_code = dr["level_emsrs_code"].ToString();
      level_short_description = dr["level_short_description"].ToString();
      middle_initial = dr["middle_initial"].ToString();
      dr.Close();
      Close();
      }

    public void ImportLatestFromEmsrs(ArrayList recs)
      {
      if (recs.Count > 0)
        {
        //
        // The following is the code I really wanted to use, but MySQL implements it in a way that causes the auto_increment id field to advance when the "on duplicate key update" clause gets applied, which should be the
        // dominant case, and which exhausts id numbers faster than I think is prudent.
        //
        //var sb = new StringBuilder("insert practitioner (last_name,first_name,middle_initial,certification_number,level_id,regional_council_code) values ");
        //Open();
        //foreach (var rec in recs)
        //  {
        //  sb.Append
        //    (
        //    "("
        //    + "NULLIF('" + (rec as Practitioner).last_name + "','')"
        //    + ",'" + (rec as Practitioner).first_name + "'"
        //    + ",'" + (rec as Practitioner).middle_initial + "'"
        //    + ",NULLIF('" + (rec as Practitioner).certification_number + "','')"
        //    + ",NULLIF(IFNULL((select id from practitioner_level where emsrs_practitioner_level_description = '" + (rec as Practitioner).level + "'),''),'')"
        //    + ",NULLIF(IFNULL((select code from region_code_name_map where emsrs_active_practitioners_name = '" + (rec as Practitioner).regional_council + "'),''),'')"
        //    + "),"
        //    );
        //  }
        //new MySqlCommand(sb.ToString().TrimEnd(Convert.ToChar(k.COMMA)) + " on duplicate key update be_stale = false",connection).ExecuteNonQuery();
        //Close();
        //
        var combined_name = k.EMPTY;
        var first_name = k.EMPTY;
        var id_obj = new object();
        var last_name = k.EMPTY;
        var middle_initial = k.EMPTY;
        var certification_number = k.EMPTY;
        var level_description = k.EMPTY;
        var region_name = k.EMPTY;
        var birth_date = k.EMPTY;
        var county_name = k.EMPTY;
        var status = k.EMPTY;
        var be_instructor = false;
        Open();
        foreach (var rec in recs)
          {
          last_name = k.Safe((rec as PractitionersPractitioner).LastName,k.safe_hint_type.HUMAN_NAME).Replace(k.SPACE + k.SPACE,k.SPACE).Trim();
          region_name = ((rec as PractitionersPractitioner).RegionalCouncilName == null ? k.EMPTY : k.Safe((rec as PractitionersPractitioner).RegionalCouncilName,k.safe_hint_type.ORG_NAME));
          if ((last_name.Length > 0) && (region_name.Length > 0))
            {
            combined_name = (k.Safe((rec as PractitionersPractitioner).FirstName,k.safe_hint_type.HUMAN_NAME) + k.SPACE + k.Safe((rec as PractitionersPractitioner).MI,k.safe_hint_type.HUMAN_NAME)).Trim();
            first_name = combined_name;
            middle_initial = k.EMPTY;
            if ((combined_name.Contains(k.SPACE)) && (combined_name.LastIndexOf(k.SPACE) == combined_name.Length - 2))
              {
              middle_initial = combined_name.Substring(combined_name.Length - 1);
              first_name = combined_name.Substring(0,combined_name.Length - 2);
              }
            certification_number = (rec as PractitionersPractitioner).CertificationNumber.ToString();
            level_description = k.Safe((rec as PractitionersPractitioner).LevelDescription,k.safe_hint_type.HYPHENATED_ALPHA_WORDS);
            birth_date = ((rec as PractitionersPractitioner).BirthDate == null ? k.EMPTY : k.Safe((rec as PractitionersPractitioner).BirthDate,k.safe_hint_type.DATE_TIME));
            county_name = ((rec as PractitionersPractitioner).CountyName == null ? k.EMPTY : k.Safe((rec as PractitionersPractitioner).CountyName,k.safe_hint_type.POSTAL_CITY));
            status = k.Safe((rec as PractitionersPractitioner).Status,k.safe_hint_type.HYPHENATED_ALPHA_WORDS);
            be_instructor = ((rec as PractitionersPractitioner).IsInstructor == "Y");
            //
            // The following logic will prevent this app from detecting when EMSRS reassigns a certification_number to a different person.  The latest person to hold the certification number will therefore be associated, in
            // this app, with the prior assignee's history of class attendance.  But that doesn't seem to be a concern since EMSRS, not this app, is the authoritative source for a practitioner's historical class attendance.
            //
            id_obj = new MySqlCommand("select id from practitioner where certification_number = '" + certification_number + "'",connection).ExecuteScalar();
            if (id_obj == null)
              {
              new MySqlCommand
                (
                "insert ignore practitioner set last_name = '" + last_name + "'"
                + " , first_name = '" + first_name + "'"
                + " , middle_initial = '" + middle_initial + "'"
                + " , certification_number = '" + certification_number + "'"
                + " , level_id = (select id from practitioner_level where emsrs_practitioner_level_description = '" + level_description + "')"
                + " , regional_council_code = (select code from region_code_name_map where emsrs_active_practitioners_name = '" + region_name + "')"
                + " , birth_date = STR_TO_DATE('" + birth_date + "','%m/%d/%Y')"
                + " , be_birth_date_confirmed = TRUE"
                + " , residence_county_code = (select code from county_code_name_map where name = '" + county_name + "')"
                + " , status_id = (select id from practitioner_status where description = '" + status + "')"
                + " , be_instructor = " + be_instructor.ToString(),
                connection
                )
                .ExecuteNonQuery();
              }
            else
              {
              new MySqlCommand
                (
                "update ignore practitioner"
                + " set last_name = '" + last_name + "'"
                + " , first_name = '" + first_name + "'"
                + " , middle_initial = '" + middle_initial + "'"
                + " , level_id = (select id from practitioner_level where emsrs_practitioner_level_description = '" + level_description + "')"
                + " , regional_council_code = (select code from region_code_name_map where emsrs_active_practitioners_name = '" + region_name + "')"
                + (birth_date.Length > 0 ? " , birth_date = STR_TO_DATE('" + birth_date + "','%m/%d/%Y') , be_birth_date_confirmed = TRUE" : k.EMPTY)
                + (county_name.Length > 0 ? " , residence_county_code = (select code from county_code_name_map where name = '" + county_name + "')" : k.EMPTY)
                + " , status_id = (select id from practitioner_status where description = '" + status + "')"
                + " , be_stale = false"
                + " , be_past = false"
                + " , be_instructor = " + be_instructor.ToString()
                + " where id = '" + id_obj.ToString() + "'",
                connection
                )
                .ExecuteNonQuery();
              }
            }
          }
        Close();
        }
      }

    public void MarkAllStale()
      {
      Open();
      new MySqlCommand("update practitioner set be_stale = TRUE",connection).ExecuteNonQuery();
      Close();
      }

    internal void MarkDobsConfirmed(string id)
      {
      Open();
      new MySqlCommand
        (
        db_trail.Saved
          (
          "update practitioner"
          + " join coned_offering_roster on (coned_offering_roster.practitioner_id=practitioner.id)"
          + " set be_birth_date_confirmed = TRUE"
          + "  where coned_offering_id = '" + id + "'"
          ),
        connection
        )
        .ExecuteNonQuery();
      Close();
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
          "SELECT IFNULL(max(length(CONVERT(concat(last_name,', ',first_name,' ',middle_initial,', ',certification_number,', ',IFNULL(birth_date,'-')) USING utf8))),0)"
          + " FROM practitioner"
          + (region_code.Length > 0 ? " where regional_council_code = '" + region_code + "'" : k.EMPTY),
          connection
          )
          .ExecuteScalar().ToString()
        );
      Close();
      return max_spec_length;
      }

    public void RemoveStale()
      {
      Open();
      new MySqlCommand
        (
        "START TRANSACTION"
        + ";"
        + " delete from practitioner where be_stale and (select count(*) from coned_offering_roster where practitioner_id = practitioner.id) = 0"
        + ";"
        + " update practitioner set be_past = TRUE where be_stale"
        + ";"
        + " COMMIT",
        connection
        )
        .ExecuteNonQuery();
      Close();
      }

    public void Set
      (
      string id,
      string last_name,
      string first_name,
      string middle_initial,
      string certification_number,
      string level_id,
      string regional_council_code,
      DateTime birth_date,
      string email_address,
      bool be_stale,
      string residence_county_code,
      bool be_birth_date_confirmed,
      string street_address_1,
      string street_address_2,
      string city_state_zip,
      bool be_instructor,
      bool be_past
      )
      {
      var childless_field_assignments_clause = k.EMPTY
      + "last_name = NULLIF('" + last_name + "','')"
      + " , first_name = NULLIF('" + first_name + "','')"
      + " , middle_initial = '" + (middle_initial.Length > 0 ? middle_initial : k.SPACE) + "'"
      + " , certification_number = NULLIF('" + certification_number + "','')"
      + " , level_id = NULLIF('" + level_id + "','')"
      + " , regional_council_code = NULLIF('" + regional_council_code + "','')"
      + " , birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'"
      + " , email_address = NULLIF('" + email_address + "','')"
      + " , be_stale = " + be_stale.ToString()
      + " , residence_county_code = NULLIF('" + residence_county_code + "','')"
      + " , be_birth_date_confirmed = " + be_birth_date_confirmed.ToString()
      + " , street_address_1 = NULLIF('" + street_address_1 + "','')"
      + " , street_address_2 = NULLIF('" + street_address_2 + "','')"
      + " , city_state_zip = NULLIF('" + city_state_zip + "','')"
      + " , be_instructor = " + be_instructor.ToString()
      + " , be_past = " + be_past.ToString()
      + k.EMPTY;
      Open();
      new MySqlCommand
        (
        db_trail.Saved
          (
          "insert practitioner"
          + " set id = NULLIF('" + id + "','')"
          + " , " + childless_field_assignments_clause
          + " on duplicate key update "
          + childless_field_assignments_clause
          ),
          connection
        )
        .ExecuteNonQuery();
      Close();
      }

    public void SetFieldsNotImportedFromState
      (
      string id,
      DateTime birth_date,
      string county_code,
      string email_address
      )
      {
      Open();
      new MySqlCommand
        (
        db_trail.Saved
          (
          "update practitioner set birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'"
          + " , residence_county_code = NULLIF('" + county_code + "','')"
          + " , email_address = NULLIF('" + email_address + "','')"
          + " where id = '" + id + "'"
          ),
          connection
        )
        .ExecuteNonQuery();
      Close();
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
          "update practitioner set birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'"
          + " , email_address = NULLIF('" + email_address + "','')"
          + " where id = '" + id + "'"
          ),
          connection
        )
        .ExecuteNonQuery();
      Close();
      }

    } // end TClass_db_practitioners

  }
