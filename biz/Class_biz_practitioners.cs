// Derived from KiAspdotnetFramework/component/biz/Class~biz~~template~kicrudhelped~item.cs~template

using Class_db_practitioners;
using ConEdLink.component.ss;
using kix;
using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace Class_biz_practitioners
  {
  public class TClass_biz_practitioners
    {
    private readonly TClass_db_practitioners db_practitioners = null;
    private readonly Class_ss_emsams ss_emsams = null;

    public TClass_biz_practitioners() : base()
      {
      db_practitioners = new TClass_db_practitioners();
      ss_emsams = new Class_ss_emsams();
      }

    public bool Bind
      (
      string partial_spec,
      object target,
      string sponsor_id_filter
      )
      {
      return db_practitioners.Bind(partial_spec, target, sponsor_id_filter);
      }
    public bool Bind(string partial_spec,object target)
      {
      return Bind(partial_spec,target,sponsor_id_filter:k.EMPTY);
      }

    public void BindDirectToListControl(object target)
      {
      db_practitioners.BindDirectToListControl(target);
      }

    public void BindDirectToListControlForRoster
      (
      object target,
      string starting_with,
      k.int_positive limit,
      bool do_limit_to_21_yoa_or_older = false
      )
      {
      db_practitioners.BindDirectToListControlForRoster(target,starting_with,limit,do_limit_to_21_yoa_or_older);
      }

    public string BirthDateOf(object summary)
      {
      return db_practitioners.BirthDateOf(summary);
      }

    public string CertificationNumberOf(object summary)
      {
      return db_practitioners.CertificationNumberOf(summary);
      }

    public bool Delete(string id)
      {
      return db_practitioners.Delete(id);
      }

    public string EmailAddressOf(object summary)
      {
      return db_practitioners.EmailAddressOfId(summary);
      }

    public string EmailAddressOfId(string id)
      {
      return db_practitioners.EmailAddressOfId(id);
      }

    public string FirstNameOf(object summary)
      {
      return db_practitioners.FirstNameOf(summary);
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

    public string IdOf(object summary)
      {
      return db_practitioners.IdOf(summary);
      }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types")]
    public void ImportLatestFromEmsrs()
      {
//      db_practitioners.MarkAllStale();
      //
      var context = new Class_ss_emsams.PractitionersContext();
      while (context.disposition.val < 1)
        {
        try
          {
          do
            {
            db_practitioners.ImportLatestFromEmsrs(ss_emsams.Practitioners(context));
            }
          while (context.disposition.val == 0);
          }
        catch (Exception e)
          {
          k.SilentAlarm(the_exception:e);
          Thread.Sleep(millisecondsTimeout:new Random().Next(minValue:1800000,maxValue:5400000));
          context = new Class_ss_emsams.PractitionersContext();
          }
        }
      //
//      db_practitioners.RemoveStale();
      }

    public string LastNameOf(object summary)
      {
      return db_practitioners.LastNameOf(summary);
      }

    public string LevelOf(object summary)
      {
      return db_practitioners.LevelOf(summary);
      }

    public k.int_nonnegative MaxSpecLength(string region_code)
      {
      return db_practitioners.MaxSpecLength(region_code);
      }

    public string MiddleInitialOf(object summary)
      {
      return db_practitioners.MiddleInitialOf(summary);
      }

    public void SendClassCompletionCertificateLegacy
      (
      string working_directory,
      string shielded_query_string_of_hashtable,
      string sender_email_address,
      string target_email_address
      )
      {
      var stdout = k.EMPTY;
      var stderr = k.EMPTY;
      k.RunCommandIteratedOverArguments
        (
        "c:\\cygwin\\bin\\wget",
        new ArrayList()
          {
          "--output-document=/dev/null --no-check-certificate"
          + " --post-data"
          +   "=" + shielded_query_string_of_hashtable
          +   "&practitioner_email_address=" + target_email_address
          +   "&sender_email_address=" + sender_email_address
          + k.SPACE
          + "\"" + ConfigurationManager.AppSettings["runtime_root_fullspec"] + "noninteractive/report_commanded_training_certificate_legacy.aspx\""
          },
        working_directory,
        out stdout,
        out stderr
        );
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

    public void SetEmailAddress
      (
      string id,
      string email_address
      )
      {
      db_practitioners.SetEmailAddress(id,email_address);
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

    public void SetFieldsNotImportedFromState
      (
      string id,
      DateTime birth_date,
      string email_address
      )
      {
      db_practitioners.SetFieldsNotImportedFromState
        (
        id,
        birth_date,
        email_address
        );
      }

    public object Summary(string id)
      {
      return db_practitioners.Summary(id);
      }

    } // end TClass_biz_practitioners

  }
