using Class_biz_counties;
using Class_biz_milestones;
using Class_biz_services;
using Class_biz_user;
using Class_db_accounts;
using Class_db_emsof_requests;
using kix;
using System;
using System.Configuration;

namespace Class_biz_accounts
{
    public class TClass_biz_accounts
    {
        private readonly TClass_biz_counties biz_counties = null;
        private readonly TClass_biz_services biz_services = null;
        private readonly TClass_biz_user biz_user = null;
        private readonly TClass_db_accounts db_accounts = null;
        private readonly TClass_db_emsof_requests db_emsof_requests = null;

        public TClass_biz_accounts() : base()
        {
            // TODO: Add any constructor code here
            biz_counties = new TClass_biz_counties();
            biz_services = new TClass_biz_services();
            biz_user = new TClass_biz_user();
            db_accounts = new TClass_db_accounts();
            db_emsof_requests = new TClass_db_emsof_requests();
        }

    public bool BeOkForConedSponsorToInputRosterByBatch()
      {
      return ((biz_user.Kind() == "coned_sponsor") && db_accounts.BeOkForConedSponsorToInputRosterByBatch(biz_user.IdNum()));
      }

    public bool BeOkForConedSponsorToInputRosterByCopy()
      {
      return ((biz_user.Kind() == "coned_sponsor") && db_accounts.BeOkForConedSponsorToInputRosterByCopy(biz_user.IdNum()));
      }

        public bool BeStalePassword(string user_kind, string user_id)
        {
            bool result;
            result = db_accounts.BeStalePassword(user_kind, user_id);
            return result;
        }

    public bool BeValidSysAdminCredentials(string encoded_password)
      {
      return (encoded_password == ConfigurationManager.AppSettings["sysadmin_encoded_password"]);
      }

        public void BindConedSponsorsInRegion
          (
          string region_code,
          object target
          )
          {
          db_accounts.BindConedSponsorsInRegion(region_code,target);
          }

        public void BindCountiesInRegion
          (
          string region_code,
          object target
          )
          {
          db_accounts.BindCountiesInRegion(region_code,target);
          }

        public void BindRegionalStaffersInRegionForConEdLink
          (
          string region_code,
          object target
          )
          {
          db_accounts.BindRegionalStaffersInRegion(region_code,"'director','education-coordinator','education-specialist','education-reservist'",target);
          }

        public void BindRegionalStaffersInRegionForWebEMSOF
          (
          string region_code,
          object target
          )
          {
          db_accounts.BindRegionalStaffersInRegion(region_code,"'director','emsof-coordinator','emsof-planner','emsof-accountant'",target);
          }

        public void BindServicesInRegion
          (
          string region_code,
          object target
          )
          {
          db_accounts.BindServicesInRegion(region_code,target);
          }

        public string EmailAddressByKindId(string user_kind, string user_id)
        {
            string result;
            result = db_accounts.EmailAddressByKindId(user_kind, user_id);
            return result;
        }

        public string EmailTargetByRegionAndRole
          (
          string region_code,
          string role
          )
          {
          return db_accounts.EmailTargetByRegionAndRole(region_code,role);
          }

        public bool Exists(string user_kind, string user_id, string encoded_password)
        {
            bool result;
            result = db_accounts.Exists(user_kind, user_id, encoded_password);
            return result;
        }

        public void IssueForcedOpenNotice(string service_id, string sponsor_region_name, string county_code)
        {
            string emsof_coordinator_email_address;
            string service_name;
            emsof_coordinator_email_address = EmailTargetByRegionAndRole(biz_counties.RegionCodeOf(county_code),"emsof-coordinator");
            service_name = biz_services.NameOf(service_id);
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailAddressByKindId("service", service_id) + k.COMMA + EmailAddressByKindId("county", county_code) + k.COMMA + emsof_coordinator_email_address, "Special re-opening of EMSOF request", sponsor_region_name + " has re-opened an EMSOF request from " + service_name + " for additional rework.  " + "The re-opened request is temporarily exempt from normal submission deadlines but " + sponsor_region_name + " reserves the " + "right to rescind this exemption without notice." + k.NEW_LINE + k.NEW_LINE + service_name + " can rework this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + sponsor_region_name + " can be contacted as follows:" + k.NEW_LINE + k.NEW_LINE + "   " + emsof_coordinator_email_address + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, emsof_coordinator_email_address);
        }

        public void IssueNoticeToProceed
          (
          string service_id,
          string service_name,
          string fy_designator,
          string sponsor_region_name,
          string sponsor_county_name,
          string sponsor_county_email_address,
          string master_id
          )
          {
            string emsof_coord_email_address;
            string message_text;
            // Get the next approver's email address.
            emsof_coord_email_address = EmailTargetByRegionAndRole(db_emsof_requests.RegionCodeOfMasterId(master_id),"emsof-coordinator");
            // Generate the base message.
            message_text = "Recently, " + service_name + " submitted a request to conduct an EMSOF Category 4 Provider Equipment project.  " + "This message documents the approval by all the necessary parties of that request as recorded in " + ConfigurationManager.AppSettings["application_name"] + k.PERIOD + k.NEW_LINE + k.NEW_LINE + "Congratulations!  YOU MAY NOW PROCEED WITH THE PROJECT SPECIFIED BELOW.  Please print this message and keep it for your " + "records." + k.NEW_LINE + k.NEW_LINE + "To complete this project and receive proper reimbursement, please do the following:" + k.NEW_LINE + k.NEW_LINE + "1. Purchase the items specified below." + k.NEW_LINE + k.NEW_LINE + "2. FAX OR SEND COPIES OF YOUR INVOICES AND/OR RECEIPTS TO THE REGIONAL" + k.NEW_LINE + "   COUNCIL." + k.NEW_LINE + k.NEW_LINE + "3. Take delivery of the specified equipment." + k.NEW_LINE + k.NEW_LINE + "4. Send proof of payment to the regional council.  Documents acceptable" + k.NEW_LINE + "   for this purpose include:" + k.NEW_LINE + k.NEW_LINE + "   -  Copy of canceled check" + k.NEW_LINE + "   -  Copy of bank draft" + k.NEW_LINE + "   -  Copy of bank statement" + k.NEW_LINE + "   -  Copy of wire transfer slip" + k.NEW_LINE + "   -  Copy of credit card statement" + k.NEW_LINE + k.NEW_LINE + "5. Watch your mailbox for the reimbursement check!" + k.NEW_LINE + k.NEW_LINE + k.NEW_LINE + "=== PROJECT DETAILS ===" + k.NEW_LINE + "This Notice To Proceed only applies to the following project:" + k.NEW_LINE + k.NEW_LINE + "--- Master data ---" + k.NEW_LINE + "Fiscal year:     " + fy_designator + k.NEW_LINE + "Sponsor region:  " + sponsor_region_name + k.NEW_LINE + "Sponsor county:  " + sponsor_county_name + k.NEW_LINE + k.NEW_LINE + "--- Detail data ---" + k.NEW_LINE + db_emsof_requests.DetailText(master_id) + k.NEW_LINE + "You can review your EMSOF requests by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact your Regional EMSOF Coordinator at:" + k.NEW_LINE + k.NEW_LINE + "   " + emsof_coord_email_address + "  (mailto:" + emsof_coord_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"];
            // Send notification to service.
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailAddressByKindId("service", service_id), "NOTICE TO PROCEED with EMSOF project", message_text, false, sponsor_county_email_address, k.EMPTY, emsof_coord_email_address);
            // Send notification to region.
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], emsof_coord_email_address, ConfigurationManager.AppSettings["application_name"] + " has issued a Notice To Proceed", ConfigurationManager.AppSettings["application_name"] + " has issued the following Notice To Proceed message to " + service_name + ".  You should expect this service to submit invoices and proof of payment." + k.NEW_LINE + k.NEW_LINE + "... BEGIN COPY OF NOTICE TO PROCEED ..." + k.NEW_LINE + k.NEW_LINE + message_text + k.NEW_LINE + k.NEW_LINE + "... END COPY OF NOTICE TO PROCEED ...");
          }

        public void IssueRegressionNotice(string service_id, string sponsor_region_name, string county_code)
        {
            string emsof_coordinator_email_address;
            string service_name;
            emsof_coordinator_email_address = EmailTargetByRegionAndRole(biz_counties.RegionCodeOf(county_code),"emsof-coordinator");
            service_name = biz_services.NameOf(service_id);
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailAddressByKindId("service", service_id) + k.COMMA + EmailAddressByKindId("county", county_code) + k.COMMA + emsof_coordinator_email_address, "Administrative regression of EMSOF request", sponsor_region_name + " has regressed the status of an EMSOF request from " + service_name + ".  This change will allow " + sponsor_region_name + " to revise the request\'s invoice or canceled check data." + k.NEW_LINE + k.NEW_LINE + "Unless otherwise advised by " + sponsor_region_name + ", no action is required from you at this time." + k.NEW_LINE + k.NEW_LINE + service_name + " can review this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + sponsor_region_name + " can be contacted as follows:" + k.NEW_LINE + k.NEW_LINE + "   " + emsof_coordinator_email_address + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, emsof_coordinator_email_address);
        }

        public void IssueWithdrawalNotice(string master_id, string service_id, string county_code)
        {
            var service_email_address = EmailAddressByKindId("service", service_id);
            var service_name = biz_services.NameOf(service_id);
            k.SmtpMailSend
              (
              from:ConfigurationManager.AppSettings["sender_email_address"],
              to:EmailTargetByRegionAndRole(biz_counties.RegionCodeOf(county_code),"emsof-request-withdrawal-stakeholder") + k.COMMA + EmailAddressByKindId("county", county_code),
              subject:"Service is WITHDRAWING an entire EMSOF request",
              message_string:service_name + " is withdrawing EMSOF request W#" + master_id + " in its totality.  The associated sponsor county is " + biz_counties.NameOf(county_code) + k.PERIOD + k.NEW_LINE + k.NEW_LINE + service_name + " is aware that this action effectively surrenders " + (db_emsof_requests.EmsofAnteOf(master_id)).ToString("C") + " of EMSOF matching funds for use by others." + k.NEW_LINE + k.NEW_LINE + "You can see the effect of this action by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact the " + service_name + " EMSOF Coordinator at:" + k.NEW_LINE + k.NEW_LINE + "   " + service_email_address + "  (mailto:" + service_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"],
              be_html:false,
              cc:k.EMPTY,
              bcc:k.EMPTY,
              reply_to:service_email_address
              );
        }

        internal void MakeDeadlineFailureNotification(milestone_type milestone, string service_id, string county_code)
        {
            bool be_advice_valuable;
            string county_coord_email_address;
            string county_name;
            string deadline_description = k.EMPTY;
            string message_text;
            string service_email_address;
            string service_name;
            be_advice_valuable = false;
            county_coord_email_address = EmailAddressByKindId("county", county_code);
            county_name = biz_counties.NameOf(county_code);
            service_email_address = EmailAddressByKindId("service", service_id);
            service_name = biz_services.NameOf(service_id);
            switch(milestone)
            {
                case Class_biz_milestones.milestone_type.COUNTY_DICTATED_APPROPRIATION_DEADLINE_MILESTONE:
                    deadline_description = "finalize its EMSOF request";
                    be_advice_valuable = true;
                    break;
                case Class_biz_milestones.milestone_type.SERVICE_PURCHASE_COMPLETION_DEADLINE_MILESTONE:
                    deadline_description = "purchase EMSOF one or more requested items";
                    break;
                case Class_biz_milestones.milestone_type.SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE:
                    deadline_description = "submit invoices for one or more requested items";
                    break;
                case Class_biz_milestones.milestone_type.SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE:
                    deadline_description = "submit proof of payment for one or more requested items";
                    break;
                case Class_biz_milestones.milestone_type.END_OF_CYCLE_MILESTONE:
                    deadline_description = "complete the EMSOF cycle";
                    break;
            }
            message_text = "Sorry, but " + service_name + " has missed the regional deadline to " + deadline_description + k.SPACE + "associated with an allocation from " + county_name + " County." + k.NEW_LINE + k.NEW_LINE;
            if (be_advice_valuable)
            {
                message_text = message_text + "Please DO NOT PROCEED with any activity related to that request." + k.NEW_LINE + k.NEW_LINE;
            }
            message_text = message_text + "You can review your EMSOF requests by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact your County EMSOF Coordinator at:" + k.NEW_LINE + k.NEW_LINE + "   " + county_coord_email_address + "  (mailto:" + county_coord_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"];
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], service_email_address, "Missed deadline", message_text, false, k.EMPTY, k.EMPTY, county_coord_email_address);
            message_text = service_name + " has missed the regional deadline to " + deadline_description + k.SPACE + "associated with an allocation from " + county_name + " County." + k.NEW_LINE + k.NEW_LINE;
            if (be_advice_valuable)
            {
                message_text = message_text + "WebEMSOF has advised the service not to proceed with any activity related to that request." + k.NEW_LINE + k.NEW_LINE;
            }
            message_text = message_text + "You can use WebEMSOF by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact the affected service at:" + k.NEW_LINE + k.NEW_LINE + "   " + service_email_address + "  (mailto:" + service_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"];
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], county_coord_email_address + k.COMMA + EmailTargetByRegionAndRole(biz_counties.RegionCodeOf(county_code),"emsof-coordinator"), "Service missed deadline", message_text);
        }

        public void MakeDemotionNotification(string role, string reviewer_descriptor, string service_id, string service_name, string fy_designator, bool be_ok_to_rework, string reason, string county_code, string emsof_ante)
        {
            char[] BreakChars = new char[3 + 1];
            string other_stakeholder_email_address = k.EMPTY;
            string self_email_address;
            string service_email_address;
            // Get service's email address of record.
            service_email_address = EmailAddressByKindId("service", service_id);
            self_email_address = SelfEmailAddress();
            BreakChars[1] = Convert.ToChar(k.SPACE);
            BreakChars[2] = Convert.ToChar(k.TAB);
            BreakChars[3] = Convert.ToChar(k.HYPHEN);
            if (be_ok_to_rework)
            {
                // Send notification to service.
                // be_html
                // cc
                // bcc
                // reply_to
                k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], service_email_address, "Return of EMSOF request for rework", reviewer_descriptor + " did NOT approve " + service_name + "\'s " + fy_designator + " EMSOF request.  Instead, the request " + "has been returned to you for rework." + k.NEW_LINE + k.NEW_LINE + reviewer_descriptor + " gave this reason for this action:" + k.NEW_LINE + k.NEW_LINE + "   " + k.WrapText(reason, (k.NEW_LINE + "   "), BreakChars, short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])) + k.NEW_LINE + k.NEW_LINE + "You can rework this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
                if (role != "county")
                {
                    // Send notification to county.
                    // be_html
                    // cc
                    // bcc
                    // reply_to
                    k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailAddressByKindId("county", county_code), "Return of EMSOF request to " + service_name, reviewer_descriptor + " did NOT approve " + service_name + "\'s " + fy_designator + " EMSOF request.  Instead, the request " + "has been returned to the service for rework." + k.NEW_LINE + k.NEW_LINE + reviewer_descriptor + " gave this reason for this action:" + k.NEW_LINE + k.NEW_LINE + "   " + k.WrapText(reason, (k.NEW_LINE + "   "), BreakChars, short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])) + k.NEW_LINE + k.NEW_LINE + "You can review this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "You can contact " + service_name + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + service_email_address + "  (mailto:" + service_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
                }
            }
            else
            {
                // Get other stakeholder's email address.
                if (biz_user.Kind() == "county")
                {
                    other_stakeholder_email_address = EmailTargetByRegionAndRole(biz_counties.RegionCodeOf(county_code),"emsof-coordinator");
                }
                else if (biz_user.Kind() == "regional_staffer")
                {
                    other_stakeholder_email_address = EmailAddressByKindId("county", county_code);
                }
                // Send notifications to service and region.
                // be_html
                // cc
                // bcc
                // reply_to
                k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], service_email_address, "REJECTION of EMSOF request", reviewer_descriptor + " has REJECTED " + service_name + "\'s " + fy_designator + " EMSOF request.  " + k.NEW_LINE + k.NEW_LINE + reviewer_descriptor + " gave this reason for this action:" + k.NEW_LINE + k.NEW_LINE + "   " + k.WrapText(reason, (k.NEW_LINE + "   "), BreakChars, short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])) + k.NEW_LINE + k.NEW_LINE + "You can review your EMSOF requests by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
                // be_html
                // cc
                // bcc
                // reply_to
                k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], other_stakeholder_email_address, "REJECTION of EMSOF request", reviewer_descriptor + " has REJECTED " + service_name + "\'s " + fy_designator + " EMSOF request.  " + k.NEW_LINE + k.NEW_LINE + reviewer_descriptor + " gave this reason for this action:" + k.NEW_LINE + k.NEW_LINE + "   " + k.WrapText(reason, (k.NEW_LINE + "   "), BreakChars, short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])) + k.NEW_LINE + k.NEW_LINE + "This action effectively returns " + emsof_ante + " of EMSOF funds for use by others." + k.NEW_LINE + k.NEW_LINE + "If the regional request submission deadline has not already passed, you can UN-reject this request by moving your county " + "deadline from the past to the future." + k.NEW_LINE + k.NEW_LINE + "To review this EMSOF request or to adjust your county submission deadline, visit:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
            }
        }

        public void MakePromotionNotification
          (
          string next_reviewer_role,
          string reviewer_descriptor,
          string new_status_description,
          string service_id,
          string service_name,
          string sponsor_county_email_address,
          string fy_designator,
          bool be_warning_required
          )
          {
            string messageText;
            string next_reviewer_email_target;
            string self_email_address;
            // Get the next approver's email address.
            if (next_reviewer_role.Length > 0)
            {
                next_reviewer_email_target = EmailTargetByRegionAndRole(biz_services.RegionCodeOf(service_id),next_reviewer_role);
            }
            else
            {
                next_reviewer_email_target = k.EMPTY;
            }
            self_email_address = SelfEmailAddress();
            messageText = reviewer_descriptor + " has promoted " + service_name + "\'s " + fy_designator + " EMSOF request." + k.NEW_LINE + k.NEW_LINE + "The status of this EMSOF request is now \"" + new_status_description + "\".  ";
            if (next_reviewer_email_target.Length > 0)
            {
                messageText = messageText + "The next reviewer\'s email address is <" + next_reviewer_email_target + ">.";
            }
            if (be_warning_required)
            {
                messageText = messageText + k.NEW_LINE + k.NEW_LINE + "NOTE that this message is NOT a \"Notice To Proceed\", so you must NOT purchase any of the items in your EMSOF request " + "yet.";
            }
            messageText = messageText + k.NEW_LINE + k.NEW_LINE + "You can review your EMSOF requests by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"];
            // Send notification to service.
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailAddressByKindId("service", service_id), "Promotion of EMSOF request", messageText, false, sponsor_county_email_address, k.EMPTY, self_email_address);
            // Send notification to region.
            if (next_reviewer_email_target.Length > 0)
            {
                // be_html
                // cc
                // bcc
                // reply_to
                k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], next_reviewer_email_target, "Promotion of EMSOF request", reviewer_descriptor + " has promoted " + service_name + "\'s " + fy_designator + " EMSOF request." + k.NEW_LINE + k.NEW_LINE + "Your action is now required.  The status of this EMSOF request is \"" + new_status_description + "\"." + k.NEW_LINE + k.NEW_LINE + "You can review this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
            }
          }

        public void MakePromotionNotification
          (
          string next_reviewer_role,
          string reviewer_descriptor,
          string new_status_description,
          string service_id,
          string service_name,
          string sponsor_county_email_address,
          string fy_designator
          )
          {
          MakePromotionNotification(next_reviewer_role, reviewer_descriptor, new_status_description, service_id, service_name, sponsor_county_email_address, fy_designator, true);
          }

        public void MakeUnrejectionNotification(string role, string reviewer_descriptor, string service_id, string service_name, string fy_designator, string county_code)
        {
            string self_email_address;
            string service_email_address;
            // Get service's email address of record.
            service_email_address = EmailAddressByKindId("service", service_id);
            self_email_address = SelfEmailAddress();
            // Send notification to service.
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], service_email_address, "UN-Rejection of EMSOF request", reviewer_descriptor + " has UN-rejected " + service_name + "\'s " + fy_designator + " EMSOF request.  The request " + "has been returned to you for rework." + k.NEW_LINE + k.NEW_LINE + "You can rework this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
            if (role != "county")
            {
                // Send notification to county.
                // be_html
                // cc
                // bcc
                // reply_to
                k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailAddressByKindId("county", county_code), "UN-rejection of " + service_name + "\'s EMSOF request", reviewer_descriptor + " has UN-rejected " + service_name + "\'s " + fy_designator + " EMSOF request.  The request " + "has been returned to the service for rework." + k.NEW_LINE + k.NEW_LINE + "You can review this EMSOF request by visiting:" + k.NEW_LINE + k.NEW_LINE + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE + k.NEW_LINE + "You can contact " + reviewer_descriptor + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + self_email_address + "  (mailto:" + self_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "You can contact " + service_name + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + service_email_address + "  (mailto:" + service_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, self_email_address);
            }
        }

        public string SelfEmailAddress()
          {
          return EmailAddressByKindId(biz_user.Kind(), biz_user.IdNum());
          }

        public void SetPasswordResetEmailAddress
          (
          string kind,
          string id,
          string value
          )
          {
          db_accounts.SetPasswordResetEmailAddress(kind,id,value);
          }

        public void NotifyRegionOfServicePocAffirmation(string service_id, string service_name, string contact_person_name)
        {
            string poc_email_address;
            poc_email_address = EmailAddressByKindId("service", service_id);
            // be_html
            // cc
            // bcc
            // reply_to
            k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], EmailTargetByRegionAndRole(biz_services.RegionCodeOf(service_id),"emsof-coordinator"), "POC has assumed EMSOF resposibility for Service", "Dear Regional Council EMSOF Coordinator," + k.NEW_LINE + k.NEW_LINE + contact_person_name + " has successfully logged into WebEMSOF and has agreed to assume EMSOF Point Of Contact responsibilities " + "for " + service_name + ".  Furthermore, " + contact_person_name + " has agreed to give " + "reasonable advance notice to both the service and the regional council if it becomes necessary to relinquish such " + "responsibilities." + k.NEW_LINE + k.NEW_LINE + "You can contact " + contact_person_name + " at:" + k.NEW_LINE + k.NEW_LINE + "   " + poc_email_address + "  (mailto:" + poc_email_address + ")" + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"], false, k.EMPTY, k.EMPTY, poc_email_address);
        }

        internal void Remind(milestone_type milestone, uint num_days_left, DateTime deadline_date, string service_id)
        {
            var task_description = k.EMPTY;
            var service_email_address = EmailAddressByKindId("service", service_id);
            var service_name = biz_services.NameOf(service_id);
            if (milestone == milestone_type.COUNTY_DICTATED_APPROPRIATION_DEADLINE_MILESTONE)
              {
              task_description = "finalize your EMSOF request and submit it to your county EMSOF coordinator";
              }
            else if (milestone == milestone_type.SERVICE_PURCHASE_COMPLETION_DEADLINE_MILESTONE)
              {
              task_description = "purchase all the items in your EMSOF request(s)";
              }
            else if (milestone == milestone_type.SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE)
              {
              task_description = "finish submitting invoices for items in your EMSOF request(s) to the regional EMS council";
              }
            else if (milestone == milestone_type.SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE)
              {
              task_description = "finish submitting proof of payment for items in your EMSOF request(s) to the regional EMS council";
              }
            else if (milestone == milestone_type.SERVICE_ANNUAL_SURVEY_SUBMISSION_DEADLINE)
              {
              task_description = "use WebEMSOF to submit your Annual Survey to the regional EMS council";
              }
            k.SmtpMailSend
              (
              ConfigurationManager.AppSettings["sender_email_address"],
              service_email_address,
              "Reminder of approaching deadline",
              "This is an automated reminder from WebEMSOF." + k.NEW_LINE
              + k.NEW_LINE
              + "You have " + num_days_left.ToString() + " days to " + task_description + ".  The deadline is " + deadline_date.ToString("HH:mm:ss dddd, MMMM d, yyyy") + k.PERIOD + k.NEW_LINE
              + k.NEW_LINE
              + "You can access WebEMSOF by visiting:" + k.NEW_LINE
              + k.NEW_LINE
              + "   http://" + ConfigurationManager.AppSettings["host_domain_name"] + "/" + ConfigurationManager.AppSettings["application_name"] + k.NEW_LINE
              + k.NEW_LINE
              + "-- " + ConfigurationManager.AppSettings["application_name"]
              );
        }

        public void SetTemporaryPassword(string user_kind, string user_id, string encoded_password)
        {
            db_accounts.SetTemporaryPassword(user_kind, user_id, encoded_password);
        }

    } // end TClass_biz_accounts

}
