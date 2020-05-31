using Class_biz_accounts;
using Class_biz_coned_offerings;
using Class_biz_members;
using Class_biz_regions;
using Class_biz_roles;
using Class_biz_services;
using Class_biz_user;
using Class_biz_users;
using Class_db_coned_offerings;
using Class_db_notifications;
using kix;
using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Class_biz_notifications
  {

  public class TClass_biz_notifications
    {

    private class Static
      {
      private static char[] breakChars = new char[3 + 1];
      public static char[] BreakChars
        {
        get => breakChars;
        set => breakChars = value;
        }
      static Static()
        {
        BreakChars[1] = Convert.ToChar(k.SPACE);
        BreakChars[2] = Convert.ToChar(k.TAB);
        BreakChars[3] = Convert.ToChar(k.HYPHEN);
        }
      }

        private readonly string application_name = k.EMPTY;
        private readonly string host_domain_name = k.EMPTY;
        private readonly TClass_db_notifications db_notifications = null;
        private readonly string runtime_root_fullspec = k.EMPTY;

        public TClass_biz_notifications() : base()
        {
            application_name = ConfigurationManager.AppSettings["application_name"];
            db_notifications = new TClass_db_notifications();
            host_domain_name = ConfigurationManager.AppSettings["host_domain_name"];
            runtime_root_fullspec = ConfigurationManager.AppSettings["runtime_root_fullspec"];
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            db_notifications.BindDirectToListControl(target, unselected_literal, selected_value);
        }

    public void BindDirectToListControl(object target)
        {
            BindDirectToListControl(target, "-- Notification --");
        }

        public void BindDirectToListControl(object target, string unselected_literal)
        {
            BindDirectToListControl(target, unselected_literal, k.EMPTY);
        }

    private delegate string IssueCorruptionNotification_Merge(string s);
    internal void IssueCorruptionNotification(string user_kind, string detection_phase)
      {
      IssueCorruptionNotification_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>", application_name)
          .Replace("<detection_phase/>", detection_phase)
          .Replace("<user_kind/>", user_kind);
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/corruption_notification.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:ConfigurationManager.AppSettings["sender_email_address"],
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd())
        );
      template_reader.Close();
      }

    private delegate string IssueForClassCanceled_Merge(string s);
    internal void IssueForClassCanceled
      (
      string sponsor_number,
      string sponsor_name,
      string sponsor_email,
      string sponsor_contact_email,
      string sponsor_public_contact_email,
      string coned_offering_public_contact_email,
      string class_number,
      string course_title,
      string start,
      string end,
      string length,
      string location,
      k.int_nonnegative num_attendees,
      string status_description
      )
      {
      var biz_accounts = new TClass_biz_accounts();
      var biz_coned_offerings = new TClass_biz_coned_offerings();

      IssueForClassCanceled_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<class_number/>",biz_coned_offerings.StandardSafeRenditionOf(class_number))
          .Replace("<sponsor_number/>",sponsor_number)
          .Replace("<sponsor_name/>",sponsor_name)
          .Replace("<status_description/>",status_description)
          .Replace("<course_title/>",course_title)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          .Replace("<length/>",length)
          .Replace("<num_attendees/>",num_attendees.val.ToString())
          .Replace("<location/>",location)
          .Replace("<coned_offering_public_contact_email/>",coned_offering_public_contact_email)
          .Replace("<sponsor_email/>",sponsor_email)
          .Replace("<sponsor_contact_email/>",sponsor_contact_email)
          .Replace("<sponsor_public_contact_email/>",sponsor_public_contact_email)
          ;
        };

      var region_code = biz_coned_offerings.RegionCodeOf(class_number);
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/class_canceled.txt"));
      var education_specialist_target = biz_accounts.EmailTargetByRegionAndRole(region_code,"education-specialist");
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:biz_accounts.EmailTargetByRegionAndRole(region_code,"education-coordinator") + (education_specialist_target.Length > 0 ? k.COMMA + education_specialist_target : k.EMPTY),
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:Regex.Replace
          (
            (
              k.EmptyIfInvalidEmailAddress(coned_offering_public_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_public_contact_email)
            )
            .Trim(new char[] {Convert.ToChar(k.COMMA)}),
            ",,+",
            k.COMMA
          ),
        bcc:k.EMPTY,
        reply_to:k.EmptyIfInvalidEmailAddress(sponsor_email)
        );
      template_reader.Close();
      }

    private delegate string IssueForClassClosed_Merge(string s);
    internal void IssueForClassClosed
      (
      string sponsor_number,
      string sponsor_name,
      string sponsor_email,
      string sponsor_contact_email,
      string sponsor_public_contact_email,
      string coned_offering_public_contact_email,
      string class_number,
      string course_title,
      string start,
      string end,
      string length,
      string location,
      k.int_nonnegative num_attendees,
      string status_description
      )
      {
      var biz_accounts = new TClass_biz_accounts();
      var biz_coned_offerings = new TClass_biz_coned_offerings();

      IssueForClassClosed_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<class_number/>",biz_coned_offerings.StandardSafeRenditionOf(class_number))
          .Replace("<sponsor_number/>",sponsor_number)
          .Replace("<sponsor_name/>",sponsor_name)
          .Replace("<status_description/>",status_description)
          .Replace("<course_title/>",course_title)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          .Replace("<length/>",length)
          .Replace("<num_attendees/>",num_attendees.val.ToString())
          .Replace("<location/>",location)
          .Replace("<coned_offering_public_contact_email/>",coned_offering_public_contact_email)
          .Replace("<sponsor_email/>",sponsor_email)
          .Replace("<sponsor_contact_email/>",sponsor_contact_email)
          .Replace("<sponsor_public_contact_email/>",sponsor_public_contact_email)
          ;
        };

      var region_code = biz_coned_offerings.RegionCodeOf(class_number);
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/roster_ready.txt"));
      var education_specialist_target = biz_accounts.EmailTargetByRegionAndRole(region_code,"education-specialist");
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:biz_accounts.EmailTargetByRegionAndRole(region_code,"education-coordinator") + (education_specialist_target.Length > 0 ? k.COMMA + education_specialist_target : k.EMPTY),
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:Regex.Replace
          (
            (
              k.EmptyIfInvalidEmailAddress(coned_offering_public_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_public_contact_email)
            )
            .Trim(new char[] {Convert.ToChar(k.COMMA)}),
            ",,+",
            k.COMMA
          ),
        bcc:k.EMPTY,
        reply_to:k.EmptyIfInvalidEmailAddress(sponsor_email)
        );
      template_reader.Close();
      }

    private delegate string IssueForDeploymentMemberDemobilization_Merge(string s);
    public void IssueForDeploymentMemberDemobilization
      (
      string target,
      string actual_vs_drill_indicator,
      string deployment_name
      )
      {
      IssueForDeploymentMemberDemobilization_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<actual_vs_drill_indicator/>",actual_vs_drill_indicator)
          .Replace("<deployment_name/>",deployment_name)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member-demobilization-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

    private delegate string IssueForDeploymentMemberMobilization_Merge(string s);
    public void IssueForDeploymentMemberMobilization
      (
      string target,
      string actual_vs_drill_indicator,
      string deployment_name
      )
      {
      IssueForDeploymentMemberMobilization_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<actual_vs_drill_indicator/>",actual_vs_drill_indicator)
          .Replace("<deployment_name/>",deployment_name)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member-mobilization-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

    private delegate string IssueForDeploymentMemberTagAssignment_Merge(string s);
    public void IssueForDeploymentMemberTagAssignment
      (
      string target,
      string tag_num
      )
      {
      IssueForDeploymentMemberTagAssignment_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<tag_num/>",tag_num)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member-tag-assignment-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

    private delegate string IssueForDeploymentMemberTagDeassignment_Merge(string s);
    public void IssueForDeploymentMemberTagDeassignment(string target)
      {
      IssueForDeploymentMemberTagDeassignment_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member-tag-deassignment-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

    public void IssueForExpiringDriversLicense
      (
      string email_address,
      string last_name,
      string first_name,
      string certification_number,
      string service_strike_team_primary_manager,
      k.int_nonnegative days_left
      )
      {
      IssueForUpcomingDecredentialing
        (
        email_address:email_address,
        last_name:last_name,
        first_name:first_name,
        certification_number:certification_number,
        service_strike_team_primary_manager:service_strike_team_primary_manager,
        days_left:days_left,
        template_spec:"expiring_drivers_license"
        );
      }

        private delegate string IssueForExplicitRegionRoleChange_Merge(string s);
        public void IssueForExplicitRegionRoleChange
          (
          string member_id,
          string role_id,
          bool be_granted,
          string region_code
          )
          {
          string actor = k.EMPTY;
          string actor_email_address = k.EMPTY;
          string actor_member_id;
          TClass_biz_members biz_members;
          TClass_biz_regions biz_regions;
          TClass_biz_roles biz_roles;
          TClass_biz_user biz_user;
          TClass_biz_users biz_users;
          string changed = k.EMPTY;
          string first_name = k.EMPTY;
          string last_name = k.EMPTY;
          string region = k.EMPTY;
          string role_name = k.EMPTY;
          StreamReader template_reader;
          string to_or_from = k.EMPTY;

          IssueForRoleChange_Merge Merge = delegate (string s)
            {
            return s
              .Replace("<application_name/>", application_name)
              .Replace("<host_domain_name/>", host_domain_name)
              .Replace("<actor/>", actor)
              .Replace("<actor_email_address/>", actor_email_address)
              .Replace("<changed/>", changed)
              .Replace("<to_or_from/>", to_or_from)
              .Replace("<first_name/>", first_name)
              .Replace("<last_name/>", last_name)
              .Replace("<region/>", region)
              .Replace("<role_name/>", role_name)
              .Replace("<runtime_root_fullspec/>", runtime_root_fullspec);
            };

          biz_members = new TClass_biz_members();
          biz_regions = new TClass_biz_regions();
          biz_roles = new TClass_biz_roles();
          biz_user = new TClass_biz_user();
          biz_users = new TClass_biz_users();
          actor_member_id = biz_members.IdOfUserId(biz_user.IdNum());
          actor = biz_user.Roles()[0] + k.SPACE + biz_members.FirstNameOfMemberId(actor_member_id) + k.SPACE + biz_members.LastNameOfMemberId(actor_member_id);
          actor_email_address = biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum());
          if (be_granted)
            {
            changed = "granted";
            to_or_from = "to";
            }
          else
            {
            changed = "removed";
            to_or_from = "from";
            }
          first_name = biz_members.FirstNameOfMemberId(member_id);
          last_name = biz_members.LastNameOfMemberId(member_id);
          region = biz_regions.NameOfCode(region_code);
          role_name = biz_roles.NameOfId(role_id);
          template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/explicit_region_role_change.txt"));
          k.SmtpMailSend
            (
            from:ConfigurationManager.AppSettings["sender_email_address"],
            to:biz_members.EmailAddressOf(member_id) + k.COMMA + actor_email_address + k.COMMA + db_notifications.TargetOf("role-change", member_id),
            subject:Merge(template_reader.ReadLine()),
            message_string:Merge(template_reader.ReadToEnd()),
            be_html:false,
            cc:k.EMPTY,
            bcc:k.EMPTY,
            reply_to:actor_email_address
            );
          template_reader.Close();
          }

        private delegate string IssueForExplicitServiceRoleChange_Merge(string s);
        public void IssueForExplicitServiceRoleChange
          (
          string member_id,
          string role_id,
          bool be_granted,
          string service_id
          )
          {
          string actor = k.EMPTY;
          string actor_email_address = k.EMPTY;
          string actor_member_id;
          TClass_biz_members biz_members;
          TClass_biz_roles biz_roles;
          TClass_biz_services biz_services;
          TClass_biz_user biz_user;
          TClass_biz_users biz_users;
          string changed = k.EMPTY;
          string first_name = k.EMPTY;
          string last_name = k.EMPTY;
          string service = k.EMPTY;
          string role_name = k.EMPTY;
          StreamReader template_reader;
          string to_or_from = k.EMPTY;

          IssueForRoleChange_Merge Merge = delegate (string s)
            {
            return s
              .Replace("<application_name/>", application_name)
              .Replace("<host_domain_name/>", host_domain_name)
              .Replace("<actor/>", actor)
              .Replace("<actor_email_address/>", actor_email_address)
              .Replace("<changed/>", changed)
              .Replace("<to_or_from/>", to_or_from)
              .Replace("<first_name/>", first_name)
              .Replace("<last_name/>", last_name)
              .Replace("<service/>", service)
              .Replace("<role_name/>", role_name)
              .Replace("<runtime_root_fullspec/>", runtime_root_fullspec);
            };

          biz_members = new TClass_biz_members();
          biz_roles = new TClass_biz_roles();
          biz_services = new TClass_biz_services();
          biz_user = new TClass_biz_user();
          biz_users = new TClass_biz_users();
          actor_member_id = biz_members.IdOfUserId(biz_user.IdNum());
          actor = biz_user.Roles()[0] + k.SPACE + biz_members.FirstNameOfMemberId(actor_member_id) + k.SPACE + biz_members.LastNameOfMemberId(actor_member_id);
          actor_email_address = biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum());
          if (be_granted)
            {
            changed = "granted";
            to_or_from = "to";
            }
          else
            {
            changed = "removed";
            to_or_from = "from";
            }
          first_name = biz_members.FirstNameOfMemberId(member_id);
          last_name = biz_members.LastNameOfMemberId(member_id);
          service = biz_services.NameOf(service_id);
          role_name = biz_roles.NameOfId(role_id);
          template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/explicit_service_role_change.txt"));
          k.SmtpMailSend
            (
            from:ConfigurationManager.AppSettings["sender_email_address"],
            to:biz_members.EmailAddressOf(member_id) + k.COMMA + actor_email_address + k.COMMA + db_notifications.TargetOf("role-change", member_id),
            subject:Merge(template_reader.ReadLine()),
            message_string:Merge(template_reader.ReadToEnd()),
            be_html:false,
            cc:k.EMPTY,
            bcc:k.EMPTY,
            reply_to:actor_email_address
            );
          template_reader.Close();
          }

        private delegate string IssueForForgottenUsername_Merge(string s);
        public void IssueForForgottenUsername(string email_address, string username, string client_host_name)
        {
            TClass_biz_user biz_user;
            TClass_biz_users biz_users;
            StreamReader template_reader;

            IssueForForgottenUsername_Merge Merge = delegate (string s)
              {
              return s
                .Replace("<application_name/>", application_name)
                .Replace("<host_domain_name/>", host_domain_name)
                .Replace("<client_host_name/>", client_host_name)
                .Replace("<email_address/>", client_host_name)
                .Replace("<username/>", username);
              };

            biz_user = new TClass_biz_user();
            biz_users = new TClass_biz_users();
            template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/username_reminder.txt"));
            // from
            // to
            // subject
            // body
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], email_address, Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()));
            template_reader.Close();
        }

    private delegate string IssueForMemberAdded_Merge(string s);
    internal void IssueForMemberAdded
      (
      string member_id,
      string last_name,
      string first_name,
      string practitioner_level_short_description,
      string regional_council_name,
      DateTime birth_date,
      string email_address,
      string residence_county_name,
      string street_address_1,
      string street_address_2,
      string city_state_zip
      )
      {
      var actor = k.EMPTY;
      var actor_email_address = k.EMPTY;
      var actor_member_id = k.EMPTY;

      IssueForMemberAdded_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>", application_name)
          .Replace("<host_domain_name/>", host_domain_name)
          .Replace("<actor/>", actor)
          .Replace("<actor_email_address/>", actor_email_address)
          .Replace("<first_name/>", first_name)
          .Replace("<last_name/>", last_name)
          .Replace("<practitioner_level/>", practitioner_level_short_description)
          .Replace("<region_name/>", regional_council_name)
          .Replace("<birth_date/>", birth_date.ToString("yyyy-MM-dd"))
          .Replace("<email_address/>", email_address)
          .Replace("<residence_county_name/>", residence_county_name)
          .Replace("<street_address_1/>", street_address_1)
          .Replace("<street_address_2/>", street_address_2)
          .Replace("<city_state_zip/>", city_state_zip)
          ;
        };

      var biz_members = new TClass_biz_members();
      var biz_user = new TClass_biz_user();
      var biz_users = new TClass_biz_users();
      actor_member_id = biz_members.IdOfUserId(biz_user.IdNum());
      actor = biz_user.Roles()[0] + k.SPACE + biz_members.FirstNameOfMemberId(actor_member_id) + k.SPACE + biz_members.LastNameOfMemberId(actor_member_id);
      actor_email_address = biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum());
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member_added.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:email_address + k.COMMA + actor_email_address + k.COMMA + db_notifications.TargetOf("member-added",member_id),
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:actor_email_address
        );
      template_reader.Close();
      }

        private delegate string IssueForMembershipEstablishmentBlocked_Merge(string s);
        public void IssueForMembershipEstablishmentBlocked
          (
          string username,
          string user_id,
          string claimed_role_name,
          string claimed_member_name,
          string claimed_member_id,
          string claimed_member_email_address
          )
          {
          var user_email_address = k.EMPTY;
          IssueForMembershipEstablishmentBlocked_Merge Merge = delegate (string s)
            {
            return s
              .Replace("<application_name/>",application_name)
              .Replace("<host_domain_name/>",host_domain_name)
              .Replace("<username/>",username)
              .Replace("<user_id/>",user_id)
              .Replace("<user_email_address/>",user_email_address)
              .Replace("<claimed_role_name/>",claimed_role_name)
              .Replace("<claimed_member_name/>",claimed_member_name)
              .Replace("<claimed_member_id/>",claimed_member_id)
              .Replace("<claimed_member_email_address/>",claimed_member_email_address);
            };
          var biz_user = new TClass_biz_user();
          var template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/membership_establishment_blocked.txt"));
          user_email_address = biz_user.EmailAddress();
          k.SmtpMailSend
            (
            ConfigurationManager.AppSettings["sender_email_address"],
            ConfigurationManager.AppSettings["application_name"] + "-appadmin@" + host_domain_name + k.COMMA + ConfigurationManager.AppSettings["sysadmin_sms_address"],
            Merge(template_reader.ReadLine()),
            Merge(template_reader.ReadToEnd()),
            false,
            k.EMPTY,
            k.EMPTY,
            user_email_address
            );
          template_reader.Close();
          }

        private delegate string IssueForMembershipEstablishmentTrouble_Merge(string s);
        public void IssueForMembershipEstablishmentTrouble(string full_name, string explanation)
        {
            string user_email_address = k.EMPTY;
            TClass_biz_user biz_user;
            StreamReader template_reader;

            IssueForMembershipEstablishmentTrouble_Merge Merge = delegate (string s)
              {
              return s
                .Replace("<full_name/>", full_name.ToUpper())
                .Replace("<user_email_address/>", user_email_address)
                .Replace("<application_name/>", application_name)
                .Replace("<explanation/>", k.WrapText(explanation, (k.NEW_LINE + "   "), Static.BreakChars, short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])))
                .Replace("<host_domain_name/>", host_domain_name);
              };

            biz_user = new TClass_biz_user();
            template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/membership_establishment_trouble.txt"));
            user_email_address = biz_user.EmailAddress();
            // from
            // to
            // subject
            // body
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], ConfigurationManager.AppSettings["membership_establishment_liaison"] + k.COMMA + ConfigurationManager.AppSettings["application_name"] + "-appadmin@" + host_domain_name, Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()));
            template_reader.Close();
        }

    private delegate string IssueMobilizationAnnouncementEmail_Merge(string s);
    public void IssueMobilizationAnnouncementEmail
      (
      string target,
      string deployment_name,
      string service_name,
      string actual_vs_drill_indicator,
      string supplemental_message,
      string deployment_coordinator_target
      )
      {
      IssueMobilizationAnnouncementEmail_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<deployment_name/>",deployment_name)
          .Replace("<service_name/>",service_name)
          .Replace("<actual_vs_drill_indicator/>",actual_vs_drill_indicator)
          .Replace("<supplemental_message/>",k.WrapText(supplemental_message,k.NEW_LINE + new string(Convert.ToChar(k.SPACE),6),Static.BreakChars,short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])))
          .Replace("<deployment_coordinator_target/>",deployment_coordinator_target)
          ;
        };

      var biz_user = new TClass_biz_user();
      var biz_users = new TClass_biz_users();
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/mobilization-announcement-email.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:deployment_coordinator_target,
        bcc:k.EMPTY,
        reply_to:biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum())
        );
      template_reader.Close();
      }

    private delegate string IssueMobilizationAnnouncementSms_Merge(string s);
    public void IssueMobilizationAnnouncementSms
      (
      string target,
      string actual_vs_drill_indicator
      )
      {
      IssueMobilizationAnnouncementSms_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<actual_vs_drill_indicator/>",actual_vs_drill_indicator)
          ;
        };

      var biz_user = new TClass_biz_user();
      var biz_users = new TClass_biz_users();
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/mobilization-announcement-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum())
        );
      template_reader.Close();
      }

    public void IssueForNearlyStaleClearance
      (
      string email_address,
      string last_name,
      string first_name,
      string certification_number,
      string service_strike_team_primary_manager,
      k.int_nonnegative days_left,
      k.int_positive num_years_clearances_considered_valid,
      string clearance_forms_web_address,
      string clearance_description
      )
      {
      IssueForUpcomingDecredentialing
        (
        email_address:email_address,
        last_name:last_name,
        first_name:first_name,
        certification_number:certification_number,
        service_strike_team_primary_manager:service_strike_team_primary_manager,
        days_left:days_left,
        template_spec:"stale_clearance",
        num_years_clearances_considered_valid:num_years_clearances_considered_valid,
        clearance_forms_web_address:clearance_forms_web_address,
        clearance_description:clearance_description
        );
      }

    private delegate string IssueForOperationalPeriodAssignment_Merge(string s);
    public void IssueForOperationalPeriodAssignment
      (
      string target,
      string vehicle_designator,
      string nature,
      string start,
      string end
      )
      {
      IssueForOperationalPeriodAssignment_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<vehicle_designator/>",vehicle_designator)
          .Replace("<nature/>",nature)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member-op-period-assignment-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

    private delegate string IssueForOperationalPeriodDeassignment_Merge(string s);
    public void IssueForOperationalPeriodDeassignment
      (
      string target,
      string nature,
      string start,
      string end
      )
      {
      IssueForOperationalPeriodDeassignment_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<nature/>",nature)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/member-op-period-deassignment-sms.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:target,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

        private delegate string IssueForRoleChange_Merge(string s);
        public void IssueForRoleChange(string member_id, string role_id, bool be_granted)
        {
            string actor = k.EMPTY;
            string actor_email_address = k.EMPTY;
            string actor_member_id;
            TClass_biz_members biz_members;
            TClass_biz_roles biz_roles;
            TClass_biz_user biz_user;
            TClass_biz_users biz_users;
            string changed = k.EMPTY;
            string first_name = k.EMPTY;
            string last_name = k.EMPTY;
            string role_name = k.EMPTY;
            StreamReader template_reader;
            string to_or_from = k.EMPTY;

            IssueForRoleChange_Merge Merge = delegate (string s)
              {
              return s
                .Replace("<application_name/>", application_name)
                .Replace("<host_domain_name/>", host_domain_name)
                .Replace("<actor/>", actor)
                .Replace("<actor_email_address/>", actor_email_address)
                .Replace("<changed/>", changed)
                .Replace("<to_or_from/>", to_or_from)
                .Replace("<first_name/>", first_name)
                .Replace("<last_name/>", last_name)
                .Replace("<role_name/>", role_name)
                .Replace("<runtime_root_fullspec/>", runtime_root_fullspec);
              };

            biz_members = new TClass_biz_members();
            biz_roles = new TClass_biz_roles();
            biz_user = new TClass_biz_user();
            biz_users = new TClass_biz_users();
            actor_member_id = biz_members.IdOfUserId(biz_user.IdNum());
            actor = biz_user.Roles()[0] + k.SPACE + biz_members.FirstNameOfMemberId(actor_member_id) + k.SPACE + biz_members.LastNameOfMemberId(actor_member_id);
            actor_email_address = biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum());
            if (be_granted)
            {
                changed = "granted";
                to_or_from = "to";
            }
            else
            {
                changed = "removed";
                to_or_from = "from";
            }
            first_name = biz_members.FirstNameOfMemberId(member_id);
            last_name = biz_members.LastNameOfMemberId(member_id);
            role_name = biz_roles.NameOfId(role_id);
            template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/role_change.txt"));
            // from
            // to
            // subject
            // body
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], biz_members.EmailAddressOf(member_id) + k.COMMA + actor_email_address + k.COMMA + db_notifications.TargetOf("role-change", member_id), Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()), false, k.EMPTY, k.EMPTY, actor_email_address);
            template_reader.Close();
        }

        private delegate string IssueForClassUnclosed_Merge(string s);
        public void IssueForClassUnclosed
          (
          string sponsor_email_target,
          string class_number,
          string course_title,
          string start,
          string end,
          string length,
          string num_attendees,
          string location,
          string actor,
          string actor_email_address,
          string status_description,
          string reason
          )
          {

          IssueForClassUnclosed_Merge Merge = delegate (string s)
            {
            return s
              .Replace("<application_name/>",application_name)
              .Replace("<host_domain_name/>",host_domain_name)
              .Replace("<class_number/>",new TClass_biz_coned_offerings().StandardSafeRenditionOf(class_number))
              .Replace("<course_title/>",course_title)
              .Replace("<start/>",start)
              .Replace("<end/>",end)
              .Replace("<length/>",length)
              .Replace("<num_attendees/>",num_attendees)
              .Replace("<location/>",location)
              .Replace("<actor/>",actor)
              .Replace("<actor_email_address/>",actor_email_address)
              .Replace("<status_description/>",status_description)
              .Replace("<reason/>",k.WrapText(t:reason,insert_string:(k.NEW_LINE + "   "),break_char_array:Static.BreakChars,max_line_len:short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])))
              ;
            };

          var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/class_unclosed.txt"));
          k.SmtpMailSend
            (
            from:ConfigurationManager.AppSettings["sender_email_address"],
            to:sponsor_email_target,
            subject:Merge(template_reader.ReadLine()),
            message_string:Merge(template_reader.ReadToEnd()),
            be_html:false,
            cc:actor_email_address,
            bcc:k.EMPTY,
            reply_to:actor_email_address
            );
          template_reader.Close();
          }

    private delegate string IssueForRosterDue_Merge(string s);
    internal void IssueForRosterDue(TClass_db_coned_offerings.RosterDue roster_due)
      {
      IssueForRosterDue_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<class_number/>",new TClass_biz_coned_offerings().StandardSafeRenditionOf(roster_due.class_number))
          .Replace("<sponsor_number/>",roster_due.sponsor_number)
          .Replace("<sponsor_name/>",roster_due.sponsor_name)
          .Replace("<days_left/>",(int.Parse(ConfigurationManager.AppSettings["num_roster_due_days_after_coned_offering_end"]) - roster_due.days_after.val).ToString())
          .Replace("<course_title/>",roster_due.course_title)
          .Replace("<start_date_time/>",roster_due.start_date_time)
          .Replace("<end_date_time/>",roster_due.end_date_time)
          .Replace("<start_time/>",roster_due.start_time)
          .Replace("<end_time/>",roster_due.end_time)
          .Replace("<location/>",roster_due.location)
          .Replace("<county/>",roster_due.county)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/roster_due.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:roster_due.coned_offering_public_contact_email + k.COMMA + roster_due.sponsor_email,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:ConfigurationManager.AppSettings["bouncer_email_address"]
        );
      template_reader.Close();
      }

    private delegate string IssueForSponsorSaysAlreadySubmitted_Merge(string s);
    internal void IssueForSponsorSaysAlreadySubmitted
      (
      string sponsor_number,
      string sponsor_name,
      string sponsor_email,
      string sponsor_contact_email,
      string sponsor_public_contact_email,
      string coned_offering_public_contact_email,
      string class_number,
      string course_title,
      string start,
      string end,
      string length,
      string location,
      k.int_nonnegative num_attendees,
      string status_description
      )
      {
      var biz_accounts = new TClass_biz_accounts();
      var biz_coned_offerings = new TClass_biz_coned_offerings();

      IssueForSponsorSaysAlreadySubmitted_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<class_number/>",biz_coned_offerings.StandardSafeRenditionOf(class_number))
          .Replace("<sponsor_number/>",sponsor_number)
          .Replace("<sponsor_name/>",sponsor_name)
          .Replace("<status_description/>",status_description)
          .Replace("<course_title/>",course_title)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          .Replace("<length/>",length)
          .Replace("<num_attendees/>",num_attendees.val.ToString())
          .Replace("<location/>",location)
          .Replace("<coned_offering_public_contact_email/>",coned_offering_public_contact_email)
          .Replace("<sponsor_email/>",sponsor_email)
          .Replace("<sponsor_contact_email/>",sponsor_contact_email)
          .Replace("<sponsor_public_contact_email/>",sponsor_public_contact_email)
          ;
        };

      var region_code = biz_coned_offerings.RegionCodeOf(class_number);
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/sponsor_says_already_submitted.txt"));
      var education_specialist_target = biz_accounts.EmailTargetByRegionAndRole(region_code,"education-specialist");
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:biz_accounts.EmailTargetByRegionAndRole(region_code,"education-coordinator") + (education_specialist_target.Length > 0 ? k.COMMA + education_specialist_target : k.EMPTY),
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:Regex.Replace
          (
            (
              k.EmptyIfInvalidEmailAddress(coned_offering_public_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_public_contact_email)
            )
            .Trim(new char[] {Convert.ToChar(k.COMMA)}),
            ",,+",
            k.COMMA
          ),
        bcc:k.EMPTY,
        reply_to:k.EmptyIfInvalidEmailAddress(sponsor_email)
        );
      template_reader.Close();
      }

    private delegate string IssueForSponsorSaysCanceled_Merge(string s);
    internal void IssueForSponsorSaysCanceled
      (
      string sponsor_number,
      string sponsor_name,
      string sponsor_email,
      string sponsor_contact_email,
      string sponsor_public_contact_email,
      string coned_offering_public_contact_email,
      string class_number,
      string course_title,
      string start,
      string end,
      string length,
      string location,
      k.int_nonnegative num_attendees,
      string status_description
      )
      {
      var biz_accounts = new TClass_biz_accounts();
      var biz_coned_offerings = new TClass_biz_coned_offerings();

      IssueForSponsorSaysCanceled_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<class_number/>",biz_coned_offerings.StandardSafeRenditionOf(class_number))
          .Replace("<sponsor_number/>",sponsor_number)
          .Replace("<sponsor_name/>",sponsor_name)
          .Replace("<status_description/>",status_description)
          .Replace("<course_title/>",course_title)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          .Replace("<length/>",length)
          .Replace("<num_attendees/>",num_attendees.val.ToString())
          .Replace("<location/>",location)
          .Replace("<coned_offering_public_contact_email/>",coned_offering_public_contact_email)
          .Replace("<sponsor_email/>",sponsor_email)
          .Replace("<sponsor_contact_email/>",sponsor_contact_email)
          .Replace("<sponsor_public_contact_email/>",sponsor_public_contact_email)
          ;
        };

      var region_code = biz_coned_offerings.RegionCodeOf(class_number);
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/sponsor_says_canceled.txt"));
      var education_specialist_target = biz_accounts.EmailTargetByRegionAndRole(region_code,"education-specialist");
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:biz_accounts.EmailTargetByRegionAndRole(region_code,"education-coordinator") + (education_specialist_target.Length > 0 ? k.COMMA + education_specialist_target : k.EMPTY),
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:Regex.Replace
          (
            (
              k.EmptyIfInvalidEmailAddress(coned_offering_public_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_public_contact_email)
            )
            .Trim(new char[] {Convert.ToChar(k.COMMA)}),
            ",,+",
            k.COMMA
          ),
        bcc:k.EMPTY,
        reply_to:k.EmptyIfInvalidEmailAddress(sponsor_email)
        );
      template_reader.Close();
      }

    private delegate string IssueForSponsorSaysRanNoCe_Merge(string s);
    internal void IssueForSponsorSaysRanNoCe
      (
      string sponsor_number,
      string sponsor_name,
      string sponsor_email,
      string sponsor_contact_email,
      string sponsor_public_contact_email,
      string coned_offering_public_contact_email,
      string class_number,
      string course_title,
      string start,
      string end,
      string length,
      string location,
      k.int_nonnegative num_attendees,
      string status_description
      )
      {
      var biz_accounts = new TClass_biz_accounts();
      var biz_coned_offerings = new TClass_biz_coned_offerings();

      IssueForSponsorSaysRanNoCe_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<class_number/>",biz_coned_offerings.StandardSafeRenditionOf(class_number))
          .Replace("<sponsor_number/>",sponsor_number)
          .Replace("<sponsor_name/>",sponsor_name)
          .Replace("<status_description/>",status_description)
          .Replace("<course_title/>",course_title)
          .Replace("<start/>",start)
          .Replace("<end/>",end)
          .Replace("<length/>",length)
          .Replace("<num_attendees/>",num_attendees.val.ToString())
          .Replace("<location/>",location)
          .Replace("<coned_offering_public_contact_email/>",coned_offering_public_contact_email)
          .Replace("<sponsor_email/>",sponsor_email)
          .Replace("<sponsor_contact_email/>",sponsor_contact_email)
          .Replace("<sponsor_public_contact_email/>",sponsor_public_contact_email)
          ;
        };

      var region_code = biz_coned_offerings.RegionCodeOf(class_number);
      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/sponsor_says_ran_no_ce.txt"));
      var education_specialist_target = biz_accounts.EmailTargetByRegionAndRole(region_code,"education-specialist");
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:biz_accounts.EmailTargetByRegionAndRole(region_code,"education-coordinator") + (education_specialist_target.Length > 0 ? k.COMMA + education_specialist_target : k.EMPTY),
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:Regex.Replace
          (
            (
              k.EmptyIfInvalidEmailAddress(coned_offering_public_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_contact_email) + k.COMMA
              + k.EmptyIfInvalidEmailAddress(sponsor_public_contact_email)
            )
            .Trim(new char[] {Convert.ToChar(k.COMMA)}),
            ",,+",
            k.COMMA
          ),
        bcc:k.EMPTY,
        reply_to:k.EmptyIfInvalidEmailAddress(sponsor_email)
        );
      template_reader.Close();
      }

    private delegate string IssueStrikeTeamMemberStatusStatement_Merge(string s);
    public void IssueStrikeTeamMemberStatusStatement
      (
      string email_address,
      string last_name,
      string first_name,
      string certification_number,
      string phone_number,
      string carrier_name,
      string emergency_contact_1_name,
      string emergency_contact_1_phone_number,
      string emergency_contact_2_name,
      string emergency_contact_2_phone_number,
      bool be_immune_hepatitis_b,
      bool be_immune_diptheria_tetanus,
      string drivers_license_expiration,
      string nims_is_100_date,
      string nims_is_200_date,
      string nims_is_700_date,
      string act_1985_33_date,
      string act_1985_34_date,
      string act_1994_151_date,
      string credentialed_as_member_clause,
      string credentialed_as_leader_clause,
      string service_strike_team_affiliation,
      string service_strike_team_primary_manager
      )
      {
      IssueStrikeTeamMemberStatusStatement_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<first_name/>",first_name)
          .Replace("<last_name/>",last_name)
          .Replace("<certification_number/>",certification_number)
          .Replace("<phone_number/>",k.FormatAsNanpPhoneNum(phone_number))
          .Replace("<carrier_name/>",carrier_name)
          .Replace("<emergency_contact_1_name/>",emergency_contact_1_name)
          .Replace("<emergency_contact_1_phone_number/>",k.FormatAsNanpPhoneNum(emergency_contact_1_phone_number))
          .Replace("<emergency_contact_2_name/>",emergency_contact_2_name)
          .Replace("<emergency_contact_2_phone_number/>",k.FormatAsNanpPhoneNum(emergency_contact_2_phone_number))
          .Replace("<be_immune_hepatitis_b/>",k.YesNoOf(be_immune_hepatitis_b))
          .Replace("<be_immune_diptheria_tetanus/>",k.YesNoOf(be_immune_diptheria_tetanus))
          .Replace("<drivers_license_expiration/>",drivers_license_expiration)
          .Replace("<nims_is_100_date/>",nims_is_100_date)
          .Replace("<nims_is_200_date/>",nims_is_200_date)
          .Replace("<nims_is_700_date/>",nims_is_700_date)
          .Replace("<act_1985_33_date/>",act_1985_33_date)
          .Replace("<act_1985_34_date/>",act_1985_34_date)
          .Replace("<act_1994_151_date/>",act_1994_151_date)
          .Replace("<credentialed_as_member_clause/>",credentialed_as_member_clause)
          .Replace("<credentialed_as_leader_clause/>",credentialed_as_leader_clause)
          .Replace("<service_strike_team_affiliation/>",service_strike_team_affiliation)
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/strike_team_member_status_statement.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:email_address,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:service_strike_team_primary_manager
        );
      template_reader.Close();
      }

        private delegate string IssueForTemporaryPassword_Merge(string s);
        public void IssueForTemporaryPassword(string username, string client_host_name, string temporary_password)
        {
            TClass_biz_user biz_user;
            TClass_biz_users biz_users;
            StreamReader template_reader;

            IssueForTemporaryPassword_Merge Merge = delegate (string s)
              {
              return s
                .Replace("<application_name/>", application_name)
                .Replace("<host_domain_name/>", host_domain_name)
                .Replace("<client_host_name/>", client_host_name)
                .Replace("<temporary_password/>", temporary_password);
              };

            biz_user = new TClass_biz_user();
            biz_users = new TClass_biz_users();
            template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/temporary_password.txt"));
            // from
            // to
            // subject
            // body
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], biz_users.PasswordResetEmailAddressOfUsername(username), Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()));
            template_reader.Close();
        }

    private delegate string IssueForUpcomingDecredentialing_Merge(string s);
    public void IssueForUpcomingDecredentialing
      (
      string email_address,
      string last_name,
      string first_name,
      string certification_number,
      string service_strike_team_primary_manager,
      k.int_nonnegative days_left,
      string template_spec,
      k.int_positive num_years_clearances_considered_valid = null,
      string clearance_forms_web_address = k.EMPTY,
      string clearance_description = k.EMPTY
      )
      {
      IssueForUpcomingDecredentialing_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<first_name/>",first_name)
          .Replace("<last_name/>",last_name)
          .Replace("<certification_number/>",certification_number)
          .Replace("<num_days_left/>",days_left.val.ToString())
          .Replace("<num_years_clearances_considered_valid/>",(num_years_clearances_considered_valid == null ? "???" : num_years_clearances_considered_valid.val.ToString()))
          .Replace("<nearly_stale_clearance_list/>",(clearance_description.Length > 0 ? clearance_description : "???"))
          .Replace("<clearance_forms_web_address/>",(clearance_forms_web_address.Length > 0 ? clearance_forms_web_address : "???"))
          ;
        };

      var template_reader = File.OpenText(HttpContext.Current.Server.MapPath("template/notification/" + template_spec + "_warning.txt"));
      k.SmtpMailSend
        (
        from:ConfigurationManager.AppSettings["sender_email_address"],
        to:email_address,
        subject:Merge(template_reader.ReadLine()),
        message_string:Merge(template_reader.ReadToEnd()),
        be_html:false,
        cc:k.EMPTY,
        bcc:k.EMPTY,
        reply_to:service_strike_team_primary_manager
        );
      template_reader.Close();
      }

    } // end TClass_biz_notifications

  }
