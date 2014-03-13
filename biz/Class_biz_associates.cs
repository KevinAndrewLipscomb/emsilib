// Derived from KiAspdotnetFramework/component/biz/Class~biz~~template~kicrudhelped~item.cs~template

using Class_biz_notifications;
using Class_biz_practitioners;
using Class_db_associates;
using kix;
using System;

namespace Class_biz_associates
  {
  public class TClass_biz_associates: TClass_biz_practitioners
    {

    private TClass_biz_notifications biz_notifications = null;
    private TClass_db_associates db_associates = null;

    public TClass_biz_associates() : base()
      {
      biz_notifications = new TClass_biz_notifications();
      db_associates = new TClass_db_associates();
      }

    public bool Add
      (
      string last_name,
      string first_name,
      string middle_initial,
      DateTime birth_date,
      string email_address,
      string street_address_1,
      string street_address_2,
      string city_state_zip
      )
      {
      var add = false;
      if (!db_associates.BeKnown(first_name,last_name,birth_date))
        {
        db_associates.Add(first_name,last_name,middle_initial,birth_date,email_address,street_address_1,street_address_2,city_state_zip);
        //biz_notifications.IssueForMemberAdded(db_members.IdOfFirstnameLastnameCadnum(first_name, last_name, cad_num), first_name, last_name, cad_num, biz_medical_release_levels.DescriptionOf(medical_release_code), be_driver_qualified, biz_agencies.MediumDesignatorOf(agency_id) + k.SPACE_HYPHEN_SPACE + biz_agencies.LongDesignatorOf(agency_id), email_address, enrollment_date.ToString("dd MMMM yyyy"), biz_enrollment.DescriptionOf(enrollment_level), phone_num, section_num);
        add = true;
        }
      return add;
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
      db_associates.Set
        (
        id,
        last_name,
        first_name,
        middle_initial,
        certification_number,
        level_id,
        regional_council_code,
        birth_date,
        email_address,
        be_stale,
        residence_county_code,
        be_birth_date_confirmed,
        street_address_1,
        street_address_2,
        city_state_zip,
        be_instructor,
        be_past
        );
      }

    } // end TClass_biz_associates

  }
