using Class_db_members;
using kix;
using System;

namespace Class_biz_members
  {

  public class TClass_biz_members
    {

    private TClass_db_members db_members = null;

    public TClass_biz_members() : base()
      {
      db_members = new TClass_db_members();
      }

        public bool Add()
        {
            bool result;
            result = false;
            // if not db_members.BeKnown(first_name,last_name,cad_num) then begin
            // db_members.Add
            // (
            // attributes..
            // );
            // biz_notifications.IssueForMemberAdded
            // (
            // attributes..
            // );
            // Add := TRUE;
            // end;

            return result;
        }

        public bool BeRoleHolderBySharedSecret
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
          return db_members.BeRoleHolderBySharedSecret
            (
            certification_number,
            level_id,
            regional_council_code,
            birth_date,
            out claimed_role_name,
            out claimed_member_name,
            out claimed_member_id,
            out claimed_member_email_address
            );
          }

        public bool BeValidProfile(string id)
        {
            bool result;
            result = db_members.BeValidProfile(id);
            return result;
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            db_members.BindDirectToListControl(target, unselected_literal, selected_value);
        }

    public bool Bind
      (
      string partial_spec,
      object target
      )
      {
      return db_members.Bind(partial_spec, target);
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
      bool do_limit_to_21_yoa_or_older
      )
      {
      db_members.BindDirectToListControlForRoster(target,region_code,starting_with,limit,do_limit_to_21_yoa_or_older);
      }
    public void BindDirectToListControlForRoster(object target,string region_code,string starting_with,k.int_positive limit)
      {
      BindDirectToListControlForRoster(target,region_code,starting_with,limit,do_limit_to_21_yoa_or_older:false);
      }

    public string CertificationNumberOf(object summary)
      {
      return db_members.CertificationNumberOf(summary);
      }

    public bool Delete(string id)
      {
      return db_members.Delete(id);
      }

        public string EmailAddressOf(string member_id)
        {
            string result;
            result = db_members.EmailAddressOf(member_id);
            return result;
        }

    public string FirstNameOf(object summary)
      {
      return db_members.FirstNameOf(summary);
      }

        public string FirstNameOfMemberId(string member_id)
        {
            string result;
            result = db_members.FirstNameOfMemberId(member_id);
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
      return db_members.Get
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

        public string IdOfUserId(string user_id)
        {
            string result;
            result = db_members.IdOfUserId(user_id);
            return result;
        }

    public string LastNameOf(object summary)
      {
      return db_members.LastNameOf(summary);
      }

        public string LastNameOfMemberId(string member_id)
        {
            string result;
            result = db_members.LastNameOfMemberId(member_id);
            return result;
        }

    public string LevelOf(object summary)
      {
      return db_members.LevelOf(summary);
      }

    public k.int_nonnegative MaxSpecLength
      (
      string region_code,
      string starting_with
      )
      {
      return db_members.MaxSpecLength(region_code,starting_with);
      }

    public void SetFieldsNotImportedFromState
      (
      string id,
      DateTime birth_date,
      string email_address
      )
      {
      db_members.SetFieldsNotImportedFromState
        (
        id,
        birth_date,
        email_address
        );
      }

    public object Summary(string id)
      {
      return db_members.Summary(id);
      }

        public string UserIdOf(string member_id)
        {
            string result;
            result = db_members.UserIdOf(member_id);
            return result;
        }

    } // end TClass_biz_members

  }
