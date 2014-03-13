// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db_practitioners;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;

namespace Class_db_associates
  {

  public class TClass_db_associates: TClass_db_practitioners
    {

    private TClass_db_trail db_trail = null;

    public TClass_db_associates() : base()
      {
      db_trail = new TClass_db_trail();
      }

    new public void Set
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
      + " , be_practitioner = FALSE"
      + k.EMPTY;
      Open();
      new MySqlCommand
        (
        db_trail.Saved
          (
          "insert associate"
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

    } // end TClass_db_associates

  }
