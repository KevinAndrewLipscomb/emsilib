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

    internal void Add
      (
      string first_name,
      string last_name,
      string middle_initial,
      DateTime birth_date,
      string email_address,
      string street_address_1,
      string street_address_2,
      string city_state_zip
      )
      {
      Set
        (
        id:k.EMPTY,
        last_name:last_name,
        first_name:first_name,
        middle_initial:middle_initial,
        certification_number:k.EMPTY,
        level_id:k.EMPTY,
        regional_council_code:k.EMPTY,
        birth_date:birth_date,
        email_address:email_address,
        be_stale:false,
        residence_county_code:k.EMPTY,
        be_birth_date_confirmed:false,
        street_address_1:street_address_1,
        street_address_2:street_address_2,
        city_state_zip:city_state_zip,
        be_instructor:false,
        be_past:false
        );
      }

    public bool BeKnown
      (
      string first_name,
      string last_name,
      DateTime birth_date
      )
      {
      Open();
      var be_known = null !=
        new MySqlCommand("select 1 from member where first_name = '" + first_name + "' and last_name = '" + last_name + "' and birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'",connection).ExecuteScalar();
      Close();
      return be_known;
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
      + "last_name = NULLIF('" + last_name.Trim().ToUpper() + "','')"
      + " , first_name = NULLIF('" + first_name.Trim().ToUpper() + "','')"
      + " , middle_initial = '" + (middle_initial.Length > 0 ? middle_initial.ToUpper() : k.SPACE) + "'"
      + " , certification_number = ''"
      + " , level_id = (select id from practitioner_level where short_description = '[Associate]')"
      + " , regional_council_code = (select code from region_code_name_map where name = 'PA DOH EMSB')"
      + " , birth_date = '" + birth_date.ToString("yyyy-MM-dd") + "'"
      + " , email_address = NULLIF('" + email_address + "','')"
      + " , residence_county_code = NULLIF('" + residence_county_code + "','')"
      + " , street_address_1 = NULLIF('" + street_address_1.Trim().ToUpper() + "','')"
      + " , street_address_2 = NULLIF('" + street_address_2.Trim().ToUpper() + "','')"
      + " , city_state_zip = NULLIF('" + city_state_zip.Trim().ToUpper() + "','')"
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
