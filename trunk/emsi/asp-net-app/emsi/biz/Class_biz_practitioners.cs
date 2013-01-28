// Derived from KiAspdotnetFramework/component/biz/Class~biz~~template~kicrudhelped~item.cs~template

using Class_db_practitioners;
using Class_db_regions;
using ConEdLink.component.ss;
using kix;
using System;
using System.Collections;

namespace Class_biz_practitioners
  {
  public class TClass_biz_practitioners
    {
    private TClass_db_practitioners db_practitioners = null;
    private TClass_db_regions db_regions = null;
    private Class_ss_emsams ss_emsams = null;

    public TClass_biz_practitioners() : base()
      {
      db_practitioners = new TClass_db_practitioners();
      db_regions = new TClass_db_regions();
      ss_emsams = new Class_ss_emsams();
      }

    public bool Bind(string partial_spec, object target)
      {
      return db_practitioners.Bind(partial_spec, target);
      }

    public void BindDirectToListControl(object target)
      {
      db_practitioners.BindDirectToListControl(target);
      }

    public void BindDirectToListControlForRoster
      (
      object target,
      string region_code,
      string starting_with,
      k.int_positive limit
      )
      {
      db_practitioners.BindDirectToListControlForRoster(target,region_code,starting_with,limit);
      }

    public bool Delete(string id)
      {
      return db_practitioners.Delete(id);
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
      return db_practitioners.Get
        (
        id,
        out last_name,
        out first_name,
        out middle_initial,
        out certification_number,
        out level_id,
        out regional_council_code,
        out birth_date,
        out email_address,
        out be_stale,
        out residence_county_code,
        out be_birth_date_confirmed,
        out street_address_1,
        out street_address_2,
        out city_state_zip,
        out be_instructor,
        out be_past
        );
      }

    public void ImportLatestFromEmsrs()
      {
      db_practitioners.MarkAllStale();
      //
      var context = new Class_ss_emsams.DetailedCurrentPractitionersContext();
      do
        {
        db_practitioners.ImportLatestFromEmsrs(ss_emsams.DetailedCurrentPractitioners(context));
        }
      while (context.disposition.val == 0);
      //
      if (context.disposition.val == 1)
        {
        db_practitioners.RemoveStale();
        }
      }

    public void ImportLatestInstructorsFromEmsrs()
      {
      db_practitioners.ClearBeInstructorFlagsInSubscriberRegions();
      var subscriber_region_code_q = db_regions.SubscriberQ();
      while (subscriber_region_code_q.Count > 0)
        {
        var subscriber_region_code = subscriber_region_code_q.Dequeue();
        if (!new ArrayList() {"15"}.Contains(subscriber_region_code))  //Don't attempt when EMSRS login access to associated regions has been lost.
          {
          db_practitioners.ImportLatestInstructorsFromEmsrs(ss_emsams.EmsInstructorsList(subscriber_region_code));
          }
        }
      }

    public k.int_nonnegative MaxSpecLength
      (
      string region_code,
      string starting_with
      )
      {
      return db_practitioners.MaxSpecLength(region_code,starting_with);
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
      db_practitioners.Set
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

    public void SetFieldsNotImportedFromState
      (
      string id,
      DateTime birth_date,
      string county_code,
      string email_address
      )
      {
      db_practitioners.SetFieldsNotImportedFromState
        (
        id,
        birth_date,
        county_code,
        email_address
        );
      }

    } // end TClass_biz_practitioners

  }
