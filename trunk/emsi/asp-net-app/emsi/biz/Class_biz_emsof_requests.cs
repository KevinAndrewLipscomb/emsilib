using Class_biz_accounts;
using Class_biz_appropriations;
using Class_biz_equipment;
using Class_biz_fiscal_years;
using Class_biz_match_level;
using Class_biz_milestones;
using Class_biz_regional_staffers;
using Class_biz_user;
using Class_db_appropriations;
using Class_db_emsof_requests;
using kix;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Web;

namespace Class_biz_emsof_requests
{
    public class TClass_biz_emsof_requests
    {
        private TClass_db_appropriations db_appropriations = null;
        private TClass_db_emsof_requests db_emsof_requests = null;
        private TClass_biz_accounts biz_accounts = null;
        private TClass_biz_appropriations biz_appropriations = null;
        private TClass_biz_equipment biz_equipment = null;
        private TClass_biz_fiscal_years biz_fiscal_years = null;
        private TClass_biz_match_level biz_match_level = null;
        private TClass_biz_milestones biz_milestones = null;
        private TClass_biz_regional_staffers biz_regional_staffers = null;
        private TClass_biz_user biz_user = null;
        //Constructor  Create()
        public TClass_biz_emsof_requests() : base()
        {
            // TODO: Add any constructor code here
            db_appropriations = new TClass_db_appropriations();
            db_emsof_requests = new TClass_db_emsof_requests();
            biz_accounts = new TClass_biz_accounts();
            biz_appropriations = new TClass_biz_appropriations();
            biz_equipment = new TClass_biz_equipment();
            biz_fiscal_years = new TClass_biz_fiscal_years();
            biz_match_level = new TClass_biz_match_level();
            biz_milestones = new TClass_biz_milestones();
            biz_regional_staffers = new TClass_biz_regional_staffers();
            biz_user = new TClass_biz_user();
        }
        public decimal ActualValueOf(string master_id)
        {
            decimal result;
            result = db_emsof_requests.ActualValueOf(master_id);
            return result;
        }

        public void AddProofOfPayment(string request_id, DateTime date_of_payment, string method_code, decimal amount, string note)
        {
            db_emsof_requests.AddProofOfPayment(request_id, date_of_payment, method_code, amount, note);
        }

        public string AffiliateNumOf(object e_item)
        {
            string result;
            result = db_emsof_requests.AffiliateNumOf(e_item);
            return result;
        }

        public void Approve(object e_item, string promoter)
        {
            decimal amount_to_return_to_county;
            approval_timestamp_column_type approval_timestamp_column;
            decimal leftover_or_shortage;
            string master_id;
            string next_approver_role = k.EMPTY;
            status_type next_status;
            string reviewer_descriptor = k.EMPTY;
            status_type status;
            amount_to_return_to_county = 0;
            approval_timestamp_column = approval_timestamp_column_type.NONE;
            leftover_or_shortage = 0;
            master_id = IdOf(e_item);
            status = StatusOf(e_item);
            next_status = status;
            switch(status)
            {
                case status_type.NEEDS_COUNTY_APPROVAL:
                    // Better initialize it to something.
                    leftover_or_shortage = LeftoverOrShortageOf(e_item);
                    if (leftover_or_shortage > 0)
                    {
                        amount_to_return_to_county = leftover_or_shortage;
                    }
                    approval_timestamp_column = Class_db_emsof_requests.approval_timestamp_column_type.COUNTY;
                    next_approver_role = "emsof-planner";
                    reviewer_descriptor = "The " + promoter + " EMSOF Coordinator";
                    next_status = status_type.NEEDS_REGIONAL_COMPLIANCE_CHECK;
                    break;
                case status_type.NEEDS_REGIONAL_COMPLIANCE_CHECK:
                    approval_timestamp_column = Class_db_emsof_requests.approval_timestamp_column_type.REGIONAL_PLANNER;
                    next_approver_role = "director";
                    reviewer_descriptor = "Regional planner " + promoter;
                    next_status = status_type.NEEDS_REGIONAL_EXEC_DIR_APPROVAL;
                    break;
                case status_type.NEEDS_REGIONAL_EXEC_DIR_APPROVAL:
                    approval_timestamp_column = Class_db_emsof_requests.approval_timestamp_column_type.REGIONAL_DIRECTOR;
                    next_approver_role = "emsof-coordinator";
                    reviewer_descriptor = "Regional Executive Director " + promoter;
                    next_status = status_type.NEEDS_SENT_TO_PA_DOH_EMSO;
                    break;
                case status_type.NEEDS_PA_DOH_EMSO_APPROVAL:
                    approval_timestamp_column = Class_db_emsof_requests.approval_timestamp_column_type.STATE;
                    next_approver_role = k.EMPTY;
                    reviewer_descriptor = SponsorRegionNameOf(master_id);
                    next_status = status_type.NEEDS_INVOICE_COLLECTION;
                    break;
            }
            if (approval_timestamp_column != approval_timestamp_column_type.NONE)
            {
                db_emsof_requests.Approve(IdOf(e_item), (uint)(next_status), biz_user.Kind(), approval_timestamp_column, amount_to_return_to_county);
                if (next_status != status_type.NEEDS_INVOICE_COLLECTION)
                {
                    biz_accounts.MakePromotionNotification(next_approver_role, reviewer_descriptor, (next_status).ToString(), ServiceIdOf(e_item), ServiceNameOf(e_item), SponsorCountyEmailAddressOf(e_item), FyDesignatorOf(e_item));
                }
                else
                {
                    biz_accounts.IssueNoticeToProceed(ServiceIdOf(e_item), ServiceNameOf(e_item), FyDesignatorOf(e_item), reviewer_descriptor, SponsorCountyNameOf(e_item), SponsorCountyEmailAddressOf(e_item), master_id);
                }
            }

        }

        public Queue ArchiveMatured()
        {
            Queue result;
            result = db_emsof_requests.ArchiveMatured();
            return result;
        }

        public bool BeDeadlineExempt(string master_id)
        {
            bool result;
            result = db_emsof_requests.BeDeadlineExempt(master_id);
            return result;
        }

        public bool BeOkToApproveEmsofRequest(status_type status)
        {
            bool result;
            result = false;
            switch(status)
            {
                case status_type.NEEDS_COUNTY_APPROVAL:
                    result = (new TClass_biz_user().Kind() == "county");
                    break;
                case status_type.NEEDS_REGIONAL_COMPLIANCE_CHECK:
                    result = HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("emsof-planner");
                    break;
                case status_type.NEEDS_REGIONAL_EXEC_DIR_APPROVAL:
                    result = HttpContext.Current.User.IsInRole("director");
                    break;
                case status_type.NEEDS_PA_DOH_EMSO_APPROVAL:
                    result = HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("director");
                    break;
            }
            return result;
        }

        public bool BeOkToDrillDown(status_type status)
        {
            bool result;
            result = false;
            if (HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("emsof-planner"))
            {
                result = (status != status_type.ALLOCATED);
            }
            else if (HttpContext.Current.User.IsInRole("emsof-clerk"))
            {
                result = new ArrayList(new status_type[] {status_type.NEEDS_SENT_TO_PA_DOH_EMSO, status_type.NEEDS_INVOICE_COLLECTION, status_type.NEEDS_CANCELED_CHECK_COLLECTION, status_type.DEPLOYED}).Contains(status);
            }
            else if (HttpContext.Current.User.IsInRole("emsof-accountant"))
            {
                result = (status == status_type.NEEDS_REIMBURSEMENT_ISSUANCE);
            }
            return result;
        }

        public bool BeOkToForceOpen(object e_item)
        {
            bool result;
            result = (!db_emsof_requests.BeDeadlineExempt(db_emsof_requests.IdOf(e_item)))
            && (new ArrayList(new status_type[] {status_type.NEEDS_COUNTY_APPROVAL, status_type.NEEDS_SENT_TO_PA_DOH_EMSO, status_type.NEEDS_PA_DOH_EMSO_APPROVAL, status_type.NEEDS_INVOICE_COLLECTION, status_type.NEEDS_CANCELED_CHECK_COLLECTION, status_type.NEEDS_REIMBURSEMENT_ISSUANCE, status_type.REJECTED, status_type.FAILED_DEADLINE}).Contains(StatusOf(e_item)))
            && (HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator"))
            && (FyDesignatorOf(e_item) == biz_fiscal_years.DesignatorOfCurrent());
            return result;
        }

        public bool BeOkToMarkDone(status_type status)
        {
            bool result;
            result = false;
            switch(status)
            {
                case status_type.NEEDS_SENT_TO_PA_DOH_EMSO:
                    result = HttpContext.Current.User.IsInRole("emsof-clerk") || HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("director");
                    break;
                case status_type.NEEDS_INVOICE_COLLECTION:
                case status_type.NEEDS_CANCELED_CHECK_COLLECTION:
                    result = HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("emsof-accountant") || HttpContext.Current.User.IsInRole("director");
                    break;
                case status_type.NEEDS_REIMBURSEMENT_ISSUANCE:
                    result = HttpContext.Current.User.IsInRole("emsof-accountant") || HttpContext.Current.User.IsInRole("director");
                    break;
            }
            return result;
        }

        public bool BeOkToMarkFailed(status_type status)
        {
            bool result;
            result = false;
            switch(status)
            {
                case status_type.NEEDS_INVOICE_COLLECTION:
                    result = biz_milestones.BeProcessed(Class_biz_milestones.milestone_type.SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE) && (HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("director"));
                    break;
                case status_type.NEEDS_CANCELED_CHECK_COLLECTION:
                    result = biz_milestones.BeProcessed(Class_biz_milestones.milestone_type.SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE) && (HttpContext.Current.User.IsInRole("emsof-coordinator") || HttpContext.Current.User.IsInRole("director"));
                    break;
            }
            return result;
        }

        public bool BeOkToRevokeDeadlineExemption(object e_item)
        {
            bool result;
            result = ((db_emsof_requests.BeDeadlineExempt(db_emsof_requests.IdOf(e_item))) && (HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator")));
            return result;
        }

        public bool BeOkToTrackInvoices(status_type status)
        {
            bool result;
            result = (status == status_type.NEEDS_INVOICE_COLLECTION) && (HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator"));
            return result;
        }

        public bool BeOkToTrackPayments(status_type status)
        {
            bool result;
            result = (status == status_type.NEEDS_CANCELED_CHECK_COLLECTION) && (HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator"));
            return result;
        }

        public bool BeOkToRegress(object e_item)
        {
            bool result;
            result = (new ArrayList(new status_type[] {status_type.NEEDS_REIMBURSEMENT_ISSUANCE, status_type.NEEDS_CANCELED_CHECK_COLLECTION}).Contains(StatusOf(e_item))) && (HttpContext.Current.User.IsInRole("director") || HttpContext.Current.User.IsInRole("emsof-coordinator"));
            return result;
        }

        public bool BeOkToViewInvoices(status_type status)
        {
            bool result;
            result = new ArrayList(new status_type[] {status_type.NEEDS_INVOICE_COLLECTION, status_type.NEEDS_CANCELED_CHECK_COLLECTION, status_type.NEEDS_REIMBURSEMENT_ISSUANCE, status_type.REIMBURSEMENT_ISSUED, status_type.DEPLOYED, status_type.ARCHIVED, status_type.FAILED_DEADLINE, status_type.REJECTED, status_type.WITHDRAWN}).Contains(status);
            return result;
        }

        public bool BeWithdrawable(uint status_code)
        {
            bool result;
            result = (((status_type)(status_code)) >= status_type.NEEDS_COUNTY_APPROVAL && ((status_type)(status_code))<= status_type.NEEDS_REIMBURSEMENT_ISSUANCE);
            return result;
        }

        public void BindEquipmentProcurementDetail(string fy_id, string equipment_code, string sort_order, bool be_order_ascending, object target)
        {
            db_emsof_requests.BindEquipmentProcurementDetail(fy_id, equipment_code, sort_order, be_order_ascending, target);
        }

        public void BindEquipmentProcurementOverview(string fy_id, string sort_order, bool be_order_ascending, object target, bool do_log)
        {
            db_emsof_requests.BindEquipmentProcurementOverview(fy_id, sort_order, be_order_ascending, target, do_log);
        }

        public void BindEquipmentProcurementOverview(string fy_id, string sort_order, bool be_order_ascending, object target)
        {
            BindEquipmentProcurementOverview(fy_id, sort_order, be_order_ascending, target, true);
        }

        public void BindDetail(string master_id, object target)
        {
            db_emsof_requests.BindDetail(master_id, target);
        }

        public void BindOverviewAll(string order_by_field_name, bool be_order_ascending, object target)
        {
            db_emsof_requests.BindOverviewAll(order_by_field_name, be_order_ascending, target);
        }

        public void BindOverviewByRegionDictatedAppropriation(string region_dictated_appropriation_id, string order_by_field_name, bool be_order_ascending, object target)
        {
            db_emsof_requests.BindOverviewByRegionDictatedAppropriation(region_dictated_appropriation_id, order_by_field_name, be_order_ascending, target);
        }

        public void BindOverviewByRegionDictatedAppropriationAndStatus(string region_dictated_appropriation_id, status_type status, string order_by_field_name, bool be_order_ascending, object target)
        {
            db_emsof_requests.BindOverviewByRegionDictatedAppropriationAndStatus(region_dictated_appropriation_id, (uint)(status), order_by_field_name, be_order_ascending, target);
        }

        public void BindOverviewByStatus
          (
          status_type status,
          string order_by_field_name,
          bool be_order_ascending,
          object target,
          k.int_sign_range scope
            // -1 for current cycle only
            //  0 for all cycles
            //  1 for past cycles only
          )
          {
          db_emsof_requests.BindOverviewByStatus((uint)(status), order_by_field_name, be_order_ascending, target, scope);
          }

        public DateTime CountyApprovalTimestampOf(string master_id)
        {
            DateTime result;
            result = db_emsof_requests.CountyApprovalTimestampOf(master_id);
            return result;
        }

        public void BindProofsOfPayment(string request_id, object target)
        {
            db_emsof_requests.BindProofsOfPayment(request_id, target);
        }

        public void BindStateExportBatch(object target, status_type status, string amendment_num_string, string regional_staffer_user_id)
        {
            db_emsof_requests.BindStateExportBatch(target, (uint)(status), amendment_num_string, biz_regional_staffers.RegionCodeOf(regional_staffer_user_id));
        }

        public void BindValuesToRegion(string sort_order, bool be_order_ascending, object target)
        {
            db_emsof_requests.BindValuesToRegion(sort_order, be_order_ascending, target);
        }

        public void BindValuesToServices(string sort_order, bool be_order_ascending, object target)
        {
            db_emsof_requests.BindValuesToServices(sort_order, be_order_ascending, target);
        }

        public string CountyCodeOfMasterId(string master_id)
        {
            string result;
            result = db_emsof_requests.CountyCodeOfMasterId(master_id);
            return result;
        }

        public string CountyDictumIdOf(string master_id)
        {
            string result;
            result = db_emsof_requests.CountyDictumIdOf(master_id);
            return result;
        }

        public void DeleteProofOfPayment(string id)
        {
            db_emsof_requests.DeleteProofOfPayment(id);
        }

        public void Demote(object e_item, string demoter, string reason, string emsof_ante)
        {
            bool be_ok_to_rework;
            status_type next_status;
            string reviewer_descriptor = k.EMPTY;
            string role = k.EMPTY;
            be_ok_to_rework = (DateTime.Now <= ReworkDeadline(e_item));
            switch(StatusOf(e_item))
            {
                case status_type.NEEDS_COUNTY_APPROVAL:
                    role = "county";
                    reviewer_descriptor = "The " + demoter + " County EMSOF Coordinator";
                    break;
                case status_type.NEEDS_REGIONAL_COMPLIANCE_CHECK:
                    role = "regional_planner";
                    reviewer_descriptor = "Regional planner " + demoter;
                    break;
                case status_type.NEEDS_REGIONAL_EXEC_DIR_APPROVAL:
                    role = "regional_director";
                    reviewer_descriptor = "Regional Executive Director " + demoter;
                    break;
                case status_type.NEEDS_PA_DOH_EMSO_APPROVAL:
                    role = "state";
                    reviewer_descriptor = "State staffer " + demoter;
                    break;
            }
            if (be_ok_to_rework)
            {
                next_status = status_type.NEEDS_SERVICE_FINALIZATION;
            }
            else
            {
                next_status = status_type.REJECTED;
            }
            db_emsof_requests.Demote(IdOf(e_item), (uint)(next_status));
            biz_accounts.MakeDemotionNotification(role, reviewer_descriptor, (next_status).ToString(), ServiceIdOf(e_item), ServiceNameOf(e_item), FyDesignatorOf(e_item), be_ok_to_rework, reason, SponsorCountyCodeOf(e_item), emsof_ante);

        }

        public Queue DeployCompleted()
        {
            Queue result;
            result = db_emsof_requests.DeployCompleted();
            return result;
        }

        public decimal EmsofAnteOfItem(string master_id, string priority)
        {
            decimal result;
            result = db_emsof_requests.EmsofAnteOfItem(master_id, priority);
            return result;
        }

        public string EquipmentCodeOf(string master_id, string priority)
        {
            string result;
            result = db_emsof_requests.EquipmentCodeOf(master_id, priority);
            return result;
        }

        public Queue FailUncompleted()
        {
            Queue result;
            result = db_emsof_requests.FailUncompleted();
            return result;
        }

        public Queue FailUnfinalized()
        {
            Queue result;
            result = db_emsof_requests.FailUnfinalized();
            return result;
        }

        public void Finalize(string master_id)
        {
            db_emsof_requests.Finalize(master_id);
        }

        public void ForceClosed(string master_id)
        {
            db_emsof_requests.ForceClosed(master_id);
        }

        public void ForceOpen
          (
          string master_id,
          status_type status
          )
          {
          db_emsof_requests.ForceOpen(master_id,(new ArrayList() {status_type.NEEDS_PA_DOH_EMSO_APPROVAL, status_type.NEEDS_INVOICE_COLLECTION, status_type.NEEDS_CANCELED_CHECK_COLLECTION, status_type.NEEDS_REIMBURSEMENT_ISSUANCE, status_type.REJECTED, status_type.FAILED_DEADLINE}).Contains(status));
          biz_accounts.IssueForcedOpenNotice(ServiceIdOfMasterId(master_id), SponsorRegionNameOf(master_id), CountyCodeOfMasterId(master_id));
          }

        public string FyDesignatorOf(object e_item)
        {
            string result;
            result = db_emsof_requests.FyDesignatorOf(e_item);
            return result;
        }

        public bool HasWishList(string master_id)
        {
            bool result;
            result = db_emsof_requests.HasWishList(master_id);
            return result;
        }

        public string IdOf(object e_item)
        {
            string result;
            result = db_emsof_requests.IdOf(e_item);
            return result;
        }

        public string IdOfProofOfPayment(object e_item)
        {
            string result;
            result = db_emsof_requests.IdOfProofOfPayment(e_item);
            return result;
        }

        public decimal LeftoverOrShortageOf(object e_item)
        {
            decimal result;
            result = db_emsof_requests.LeftoverOrShortageOf(e_item);
            return result;
        }

        public void MarkDone(object e_item, string promoter)
        {
            string master_id;
            status_type next_status;
            string reviewer_descriptor;
            master_id = IdOf(e_item);
            reviewer_descriptor = SponsorRegionNameOf(master_id);
            switch(StatusOf(e_item))
            {
                case status_type.NEEDS_SENT_TO_PA_DOH_EMSO:
                    next_status = status_type.NEEDS_PA_DOH_EMSO_APPROVAL;
                    db_emsof_requests.MarkDone(IdOf(e_item), (uint)(next_status), biz_user.Kind());
                    biz_accounts.MakePromotionNotification(k.EMPTY, reviewer_descriptor, (next_status).ToString(), ServiceIdOf(e_item), ServiceNameOf(e_item), SponsorCountyEmailAddressOf(e_item), FyDesignatorOf(e_item));
                    break;
                case status_type.NEEDS_INVOICE_COLLECTION:
                    next_status = status_type.NEEDS_CANCELED_CHECK_COLLECTION;
                    db_emsof_requests.RollUpActualValue(master_id);
                    db_emsof_requests.MarkDone(master_id, (uint)(next_status), biz_user.Kind());
                    biz_accounts.MakePromotionNotification(k.EMPTY, reviewer_descriptor, (next_status).ToString(), ServiceIdOf(e_item), ServiceNameOf(e_item), SponsorCountyEmailAddressOf(e_item), FyDesignatorOf(e_item), false);
                    break;
                case status_type.NEEDS_CANCELED_CHECK_COLLECTION:
                    next_status = status_type.NEEDS_REIMBURSEMENT_ISSUANCE;
                    db_emsof_requests.MarkDone(master_id, (uint)(next_status), biz_user.Kind());
                    biz_accounts.MakePromotionNotification("emsof-accountant", reviewer_descriptor, (next_status).ToString(), ServiceIdOf(e_item), ServiceNameOf(e_item), SponsorCountyEmailAddressOf(e_item), FyDesignatorOf(e_item), false);
                    break;
                case status_type.NEEDS_REIMBURSEMENT_ISSUANCE:
                    if (biz_milestones.BeProcessed(Class_biz_milestones.milestone_type.END_OF_CYCLE_MILESTONE))
                    {
                        next_status = status_type.DEPLOYED;
                    }
                    else
                    {
                        next_status = status_type.REIMBURSEMENT_ISSUED;
                    }
                    db_emsof_requests.MarkDone(master_id, (uint)(next_status), biz_user.Kind());
                    break;
            }

        }

        public void MarkFailed(object e_item, string failer)
        {
            string master_id;
            master_id = IdOf(e_item);
            db_emsof_requests.MarkFailed(master_id);
            switch(StatusOf(e_item))
            {
                case status_type.NEEDS_INVOICE_COLLECTION:
                    db_emsof_requests.RollUpActualValue(master_id);
                    biz_accounts.MakeDeadlineFailureNotification(Class_biz_milestones.milestone_type.SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE, ServiceIdOf(e_item), CountyCodeOfMasterId(master_id));
                    break;
                case status_type.NEEDS_CANCELED_CHECK_COLLECTION:
                    biz_accounts.MakeDeadlineFailureNotification(Class_biz_milestones.milestone_type.SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE, ServiceIdOf(e_item), CountyCodeOfMasterId(master_id));
                    break;
            }

        }

        public string NextReviewer(status_type status)
        {
            string result = k.EMPTY;
            switch(status)
            {
                case status_type.NEEDS_COUNTY_APPROVAL:
                    result = "Regional Council EMSOF Coordinator";
                    break;
                case status_type.NEEDS_REGIONAL_COMPLIANCE_CHECK:
                    result = "Regional Council Executive Director";
                    break;
                case status_type.NEEDS_REGIONAL_EXEC_DIR_APPROVAL:
                    result = "Regional Council EMSOF Coordinator (for transmittal to PA DOH EMSO)";
                    break;
                case status_type.NEEDS_PA_DOH_EMSO_APPROVAL:
                    result = "Regional Council EMSOF Coordinator (for invoice collection)";
                    break;
            }
            return result;
        }

        public uint NumRequestsInStateExportBatch(status_type status, string amendment_num_string, string regional_staffer_user_id)
        {
            uint result;
            result = db_emsof_requests.NumRequestsInStateExportBatch((uint)(status), amendment_num_string, biz_regional_staffers.RegionCodeOf(regional_staffer_user_id));
            return result;
        }

        public string PropertyNameOfActualEmsofAnte()
          {
          return db_emsof_requests.PropertyNameOfActualEmsofAnte();
          }

        public string PropertyNameOfAppropriation()
        {
            string result;
            result = db_emsof_requests.PropertyNameOfAppropriation();
            return result;
        }

        public string PropertyNameOfEmsofAnte()
        {
            string result;
            result = db_emsof_requests.PropertyNameOfEmsofAnte();
            return result;
        }

        public bool BeRequestsEligibleForUnrejectionByRegionDictatedAppropriation(string region_dictated_appropriation_id)
        {
            bool result;
            result = db_emsof_requests.BeRequestsEligibleForUnrejectionByRegionDictatedAppropriation(region_dictated_appropriation_id);
            return result;
        }

        public bool BeValidCountyApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            result = db_emsof_requests.BeValidCountyApprovalTimestampOf(master_id, out timestamp);
            return result;
        }

        public bool BeValidRegionalExecDirApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            result = db_emsof_requests.BeValidRegionalExecDirApprovalTimestampOf(master_id, out timestamp);
            return result;
        }

        public bool BeValidRegionalPlannerApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            result = db_emsof_requests.BeValidRegionalPlannerApprovalTimestampOf(master_id, out timestamp);
            return result;
        }

        public bool BeValidStateApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            result = db_emsof_requests.BeValidStateApprovalTimestampOf(master_id, out timestamp);
            return result;
        }

        public string PasswordResetEmailAddressOf(object e_item)
          {
          return db_emsof_requests.PasswordResetEmailAddressOf(e_item);
          }

        public DateTime ReworkDeadline(object e_item)
        {
            DateTime result;
            result = db_emsof_requests.ReworkDeadline(e_item);
            return result;
        }

        public string ServiceIdOf(object e_item)
        {
            string result;
            result = db_emsof_requests.ServiceIdOf(e_item);
            return result;
        }

        public string ServiceIdOfMasterId(string master_id)
        {
            string result;
            result = db_emsof_requests.ServiceIdOfMasterId(master_id);
            return result;
        }

        public string ServiceNameOf(object e_item)
        {
            string result;
            result = db_emsof_requests.ServiceNameOf(e_item);
            return result;
        }

        public void SetActuals(string master_id, string priority, string invoice_designator, string quantity, string subtotal_cost, string unused_allocation, string allowable_cost)
          {
          var county_dictum_id = CountyDictumIdOf(master_id);
          var match_factor = biz_appropriations.MatchFactorOf(county_dictum_id);
          var quantity_value = decimal.Parse(quantity);
          var subtotal_cost_value = decimal.Parse(subtotal_cost);
          var effective_emsof_ante = new k.decimal_nonnegative();
          uint item_status_code = (int)(item_status_type.REGION_NEEDS_INVOICE);
          var unused_allocation_value = decimal.Parse(unused_allocation);
          if ((quantity_value > 0) && (subtotal_cost_value > 0))
            {
            decimal match_portion_of_subtotal_cost;
            if (biz_equipment.BeMatchExempt(EquipmentCodeOf(master_id, priority), biz_match_level.EnumOfId(biz_appropriations.MatchLevelIdOf(county_dictum_id))))
              {
              match_portion_of_subtotal_cost = subtotal_cost_value;
              }
            else
              {
              match_portion_of_subtotal_cost = subtotal_cost_value * match_factor;
              }
            item_status_code = (int)(item_status_type.REGION_NEEDS_PROOF_OF_PAYMENT);
            if (unused_allocation_value > 0)
              {
              decimal additional_service_ante = 0;
              var allowable_cost_value = decimal.MaxValue;
              if (allowable_cost != k.EMPTY)
                {
                allowable_cost_value = decimal.Parse(allowable_cost);
                }
              var emsof_ante_string = k.EMPTY;
              var min_service_ante_string = k.EMPTY;
              var total_cost_string = k.EMPTY;
              biz_equipment.CostsAndAntes
                (
                (subtotal_cost_value/quantity_value).ToString(),
                quantity,
                k.EMPTY,
                match_factor,
                allowable_cost_value,
                biz_equipment.FundingLevel(EquipmentCodeOf(master_id,priority),match_factor,allowable_cost_value),
                ref additional_service_ante,
                ref total_cost_string,
                ref emsof_ante_string,
                ref min_service_ante_string
                );
              effective_emsof_ante.val = decimal.Parse(emsof_ante_string);
              }
            else
              {
              effective_emsof_ante.val = Math.Min(EmsofAnteOfItem(master_id, priority), match_portion_of_subtotal_cost);
              }
            }
          db_emsof_requests.SetActuals(master_id, priority, invoice_designator, quantity, subtotal_cost, Math.Min(unused_allocation_value,effective_emsof_ante.val), item_status_code);
          }

        public void SetHasWishList(string master_id, bool value)
        {
            db_emsof_requests.SetHasWishList(master_id, value);
        }

        public string SponsorCountyCodeOf(object e_item)
        {
            string result;
            result = db_emsof_requests.SponsorCountyCodeOf(e_item);
            return result;
        }

        public string SponsorCountyEmailAddressOf(object e_item)
          {
          return db_emsof_requests.SponsorCountyEmailAddressOf(e_item);
          }

        public string SponsorCountyNameOf(object e_item)
        {
            string result;
            result = db_emsof_requests.SponsorCountyNameOf(e_item);
            return result;
        }

        public string SponsorRegionNameOf(string master_id)
        {
            string result;
            result = db_emsof_requests.SponsorRegionNameOf(master_id);
            return result;
        }

        public status_type StatusOf(object e_item)
        {
            status_type result;
            result = (status_type)(db_emsof_requests.StatusCodeOf(e_item));
            return result;
        }

        public void SubmitToState(object Table_report, string request_physical_path, status_type status_of_interest, string regional_staffer_user_id, string amendment_num_string)
        {
            string body;
            string qualifier = k.EMPTY;
            string region_name;
            body = "Dear PA DOH EMSO EMSOF Coordinator," + k.NEW_LINE + k.NEW_LINE + "The attached Excel spreadsheet contains ";
            switch(status_of_interest)
            {
                case status_type.NEEDS_SENT_TO_PA_DOH_EMSO:
                    qualifier = "fresh";
                    body = body + "new";
                    break;
                case status_type.NEEDS_PA_DOH_EMSO_APPROVAL:
                    qualifier = "repeat";
                    body = body + "a RE-TRANSMISSION of";
                    break;
            }
            region_name = biz_regional_staffers.RegionNameOf(regional_staffer_user_id);
            body = body + " EMSOF request items that have been approved by the Executive Director of " + region_name + ".  Please process " + "this report at your earliest convenience." + k.NEW_LINE + k.NEW_LINE + "Replies to this message will be addressed to the " + region_name + " EMSOF Coordinator." + k.NEW_LINE + k.NEW_LINE + "-- " + ConfigurationManager.AppSettings["application_name"];
            k.SendControlAsAttachmentToEmailMessage(Table_report, Path.GetDirectoryName(request_physical_path) + "/" + ConfigurationManager.AppSettings["scratch_folder"] + "/" + "WebEmsofDohExport-" + qualifier + k.HYPHEN + DateTime.Now.ToString("yyyyMMddHHmmssf") + ".xls", biz_accounts.EmailTargetByRole("emsof-coordinator"), ConfigurationManager.AppSettings["state_report_to_target"], ConfigurationManager.AppSettings["state_report_cc_target"], "EMSOF requests from " + region_name + " region", body);
            if (status_of_interest == status_type.NEEDS_SENT_TO_PA_DOH_EMSO)
            {
                db_emsof_requests.MarkSubmittedToState(biz_regional_staffers.RegionCodeOf(regional_staffer_user_id), amendment_num_string, (int)(status_type.NEEDS_SENT_TO_PA_DOH_EMSO), (int)(status_type.NEEDS_PA_DOH_EMSO_APPROVAL));
                db_appropriations.IncFundingRoundsGenerated(regional_staffer_user_id, amendment_num_string);
            }
        }

        public Queue SusceptibleTo(milestone_type milestone)
        {
            Queue result;
            result = db_emsof_requests.SusceptibleTo(milestone);
            return result;
        }

        public decimal SumOfActualValues(string fy_id)
        {
            decimal result;
            if (fy_id == k.EMPTY)
            {
                result = db_emsof_requests.SumOfActualValues(biz_user.Kind(), biz_user.IdNum(), new TClass_biz_fiscal_years().IdOfCurrent());
            }
            else
            {
                result = db_emsof_requests.SumOfActualValues(biz_user.Kind(), biz_user.IdNum(), fy_id);
            }
            return result;
        }

        public decimal SumOfActualValues()
        {
            return SumOfActualValues("");
        }

        public decimal SumOfActualCostsOfRequestItems(string request_id)
        {
            decimal result;
            result = db_emsof_requests.SumOfActualCostsOfRequestItems(request_id);
            return result;
        }

        public decimal SumOfActualEmsofAntesOfOtherRequestItems
          (
          string request_id,
          string priority
          )
          {
          return db_emsof_requests.SumOfActualEmsofAntesOfOtherRequestItems(request_id,priority);
          }

        public decimal SumOfProvenPaymentsOfRequest(string request_id)
        {
            decimal result;
            result = db_emsof_requests.SumOfProvenPaymentsOfRequest(request_id);
            return result;
        }

        public decimal SumOfRequestValues(string fy_id)
        {
            decimal result;
            if (fy_id == k.EMPTY)
            {
                result = db_emsof_requests.SumOfRequestValues(biz_user.Kind(), biz_user.IdNum(), new TClass_biz_fiscal_years().IdOfCurrent());
            }
            else
            {
                result = db_emsof_requests.SumOfRequestValues(biz_user.Kind(), biz_user.IdNum(), fy_id);
            }
            return result;
        }

        public decimal SumOfRequestValues()
        {
            return SumOfRequestValues("");
        }

        public string TallyOfStatus
          (
          status_type status,
          bool be_for_prior_cycles
          )
          {
          return db_emsof_requests.TallyByStatus((uint)(status),be_for_prior_cycles).ToString();
          }
        public string TallyOfStatus(status_type status)
          {
          return TallyOfStatus(status,be_for_prior_cycles:false);
          }

        public uint TcciOfAppropriation()
        {
            uint result;
            result = db_emsof_requests.TcciOfAppropriation();
            return result;
        }

        public uint TcciOfCountyEmailAddress()
          {
          return db_emsof_requests.TcciOfCountyEmailAddress();
          }

        public uint TcciOfId()
        {
            uint result;
            result = db_emsof_requests.TcciOfId();
            return result;
        }

        public uint TcciOfEmsofAnte()
        {
            uint result;
            result = db_emsof_requests.TcciOfEmsofAnte();
            return result;
        }

        public uint TcciOfFullRequestActuals()
        {
            uint result;
            result = db_emsof_requests.TcciOfFullRequestActuals();
            return result;
        }

        public uint TcciOfFullRequestDetail()
          {
          return db_emsof_requests.TcciOfFullRequestDetail();
          }

        public uint TcciOfFullRequestAllowableCost()
          {
          return db_emsof_requests.TcciOfFullRequestAllowableCost();
          }

        public uint TcciOfFullRequestPriority()
        {
            uint result;
            result = db_emsof_requests.TcciOfFullRequestPriority();
            return result;
        }

        public uint TcciOfPasswordResetEmailAddress()
        {
            uint result;
            result = db_emsof_requests.TcciOfPasswordResetEmailAddress();
            return result;
        }

        public uint TcciOfServiceName()
        {
            uint result;
            result = db_emsof_requests.TcciOfServiceName();
            return result;
        }

        public uint TcciOfLeftoverOrShortage()
        {
            uint result;
            result = db_emsof_requests.TcciOfLeftoverOrShortage();
            return result;
        }

        public uint TcciOfSrrReplacementRowIndicator()
        {
            uint result;
            result = db_emsof_requests.TcciOfSrrReplacementRowIndicator();
            return result;
        }

        public uint TcciOfStatusCode()
        {
            uint result;
            result = db_emsof_requests.TcciOfStatusCode();
            return result;
        }

        public uint TcciOfStatusDescription()
        {
            uint result;
            result = db_emsof_requests.TcciOfStatusDescription();
            return result;
        }

        public void Regress(string master_id)
        {
            db_emsof_requests.Regress(master_id);
            biz_accounts.IssueRegressionNotice(ServiceIdOfMasterId(master_id), SponsorRegionNameOf(master_id), CountyCodeOfMasterId(master_id));
        }

        public void Unreject(object e_item, string role, string reviewer_descriptor)
        {
            db_emsof_requests.Unreject(IdOf(e_item));
            biz_accounts.MakeUnrejectionNotification(role, reviewer_descriptor, ServiceIdOf(e_item), ServiceNameOf(e_item), FyDesignatorOf(e_item), SponsorCountyCodeOf(e_item));
        }

        public Queue Withdraw(string master_id)
        {
            biz_accounts.IssueWithdrawalNotice(master_id, ServiceIdOfMasterId(master_id), CountyCodeOfMasterId(master_id));
            return db_emsof_requests.Withdraw(master_id);
        }

    } // end TClass_biz_emsof_requests

    public enum status_type
    {
        ALLOCATED = 1,
        NEEDS_SERVICE_FINALIZATION = 2,
        NEEDS_COUNTY_APPROVAL = 3,
        NEEDS_REGIONAL_COMPLIANCE_CHECK = 4,
        NEEDS_REGIONAL_EXEC_DIR_APPROVAL = 5,
        NEEDS_SENT_TO_PA_DOH_EMSO = 6,
        NEEDS_PA_DOH_EMSO_APPROVAL = 7,
        NEEDS_INVOICE_COLLECTION = 8,
        NEEDS_CANCELED_CHECK_COLLECTION = 9,
        NEEDS_REIMBURSEMENT_ISSUANCE = 10,
        REJECTED = 11,
        WITHDRAWN = 12,
        REIMBURSEMENT_ISSUED = 13,
        DEPLOYED = 14,
        ARCHIVED = 15,
        FAILED_DEADLINE = 16,
    } // end status_type

    public enum item_status_type
    {
        MASTER_NOT_YET_APPROVED = 1,
        REGION_NEEDS_INVOICE = 2,
        REGION_NEEDS_PROOF_OF_PAYMENT = 4,
        ITEM_WITHDRAWN = 6,
    } // end item_status_type

}
