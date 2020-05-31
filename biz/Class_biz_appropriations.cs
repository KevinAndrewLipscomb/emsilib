using Class_biz_fiscal_years;
using Class_biz_user;
using Class_db_appropriations;
using kix;
using System;

namespace Class_biz_appropriations
{
    public class TClass_biz_appropriations
    {
        private readonly TClass_biz_fiscal_years biz_fiscal_years = null;
        private readonly TClass_db_appropriations db_appropriations = null;

        //Constructor  Create()
        public TClass_biz_appropriations() : base()
        {
            // TODO: Add any constructor code here
            biz_fiscal_years = new TClass_biz_fiscal_years();
            db_appropriations = new TClass_db_appropriations();
        }

        internal void AddRegionDictum
          (
          #pragma warning disable IDE0060 // Remove unused parameter
          string sda_id,
          string county_code,
          string amount,
          string default_match_level_id
          #pragma warning restore IDE0060 // Remove unused parameter
          )
          {
          throw new NotImplementedException();
          }

        public bool BeAnyCurrentToService(string affiliate_num)
        {
            bool result;
            result = db_appropriations.BeAnyCurrentToService(affiliate_num);
            return result;
        }

        public void BindRegionDictums
          (
          string sort_order,
          bool be_sort_order_ascending,
          object target
          )
          {
          db_appropriations.BindRegionDictums(sort_order,be_sort_order_ascending,target);
          }

        public void BindStateDictums
          (
          string sort_order,
          bool be_sda_sort_order_ascending,
          object target
          )
          {
          db_appropriations.BindStateDictums(sort_order,be_sda_sort_order_ascending,target);
          }

        public string CountyCodeOfCountyDictum(string county_dictum_id)
        {
            string result;
            result = db_appropriations.CountyCodeOfCountyDictum(county_dictum_id);
            return result;
        }

        public string CountyCodeOfRegionDictumSummary(object summary)
          {
          return db_appropriations.CountyCodeOfRegionDictumSummary(summary);
          }

        public string CountyNameOfRegionDictumSummary(object region_dictum_summary)
          {
          return db_appropriations.CountyNameOfRegionDictumSummary(region_dictum_summary);
          }

        public bool Delete
          (
          string dictator,
          string id
          )
          {
          return db_appropriations.Delete(dictator,id);
          }

        public uint FundingRoundsGenerated(string regional_staffer_id, string amendment_num_string)
        {
            uint result;
            result = db_appropriations.FundingRoundsGenerated(regional_staffer_id, amendment_num_string);
            return result;
        }

        public uint FundingRoundsGenerated(string regional_staffer_id)
        {
            return FundingRoundsGenerated(regional_staffer_id, "0");
        }

        public decimal MatchFactorOf(string county_dictum_id)
        {
            decimal result;
            result = db_appropriations.MatchFactorOf(county_dictum_id);
            return result;
        }

        public uint MatchLevelIdOf(string county_dictum_id)
        {
            uint result;
            result = db_appropriations.MatchLevelIdOf(county_dictum_id);
            return result;
        }

        public uint MatchLevelIdOfRegionDictum(string region_dictum_id)
        {
            uint result;
            result = db_appropriations.MatchLevelIdOfRegionDictum(region_dictum_id);
            return result;
        }

        public uint NumActiveAmendments(string regional_staffer_id)
        {
            uint result;
            result = db_appropriations.NumActiveAmendments(regional_staffer_id);
            return result;
        }

        public decimal ParentAppropriationOfEmsofRequest(string master_id)
        {
            decimal result;
            result = db_appropriations.ParentAppropriationOfEmsofRequest(master_id);
            return result;
        }

        public void ReduceBy(decimal delta, string county_dictum_id)
        {
            db_appropriations.ReduceBy(delta, county_dictum_id);
        }

        public string RegionCodeOfCountyDictum(string county_dictum_id)
        {
            string result;
            result = db_appropriations.RegionCodeOfCountyDictum(county_dictum_id);
            return result;
        }

        public string RegionDictumIdOf(object summary)
          {
          return db_appropriations.RegionDictumIdOf(summary);
          }

        public object RegionDictumSummary(string id)
          {
          return db_appropriations.RegionDictumSummary(id);
          }

        public void SetNewStateToRegion(decimal amount)
        {
            db_appropriations.SetNewStateToRegion(amount);
        }

        public void SetServiceToCountySubmissionDeadline(string id, DateTime deadline)
        {
            db_appropriations.SetServiceToCountySubmissionDeadline(id, deadline.ToString("yyyyMMdd") + "235959");
        }

        public decimal SumOfAppropriationsFromSpecificParent(string parent_id, string recipient_kind, string recipient_id, string fy_id)
        {
            decimal result;
            TClass_biz_user biz_user;
            biz_user = new TClass_biz_user();
            if (recipient_kind.Length == 0)
            {
                recipient_kind = biz_user.Kind();
            }
            if (recipient_id.Length == 0)
            {
                recipient_id = biz_user.IdNum();
            }
            if (fy_id.Length == 0)
            {
                fy_id = biz_fiscal_years.IdOfCurrent();
            }
            result = db_appropriations.SumOfAppropriationsFromSpecificParent(parent_id, recipient_kind, recipient_id, fy_id);
            return result;
        }

        public decimal SumOfAppropriationsFromSpecificParent(string parent_id)
        {
            return SumOfAppropriationsFromSpecificParent(parent_id, "");
        }

        public decimal SumOfAppropriationsFromSpecificParent(string parent_id, string recipient_kind)
        {
            return SumOfAppropriationsFromSpecificParent(parent_id, recipient_kind, "");
        }

        public decimal SumOfAppropriationsFromSpecificParent(string parent_id, string recipient_kind, string recipient_id)
        {
            return SumOfAppropriationsFromSpecificParent(parent_id, recipient_kind, recipient_id, "");
        }

        public decimal SumOfAppropriationsFromOnlyParent(string recipient_kind, string recipient_id, string fy_id)
        {
            decimal result;
            TClass_biz_user biz_user;
            biz_user = new TClass_biz_user();
            if (recipient_kind.Length == 0)
            {
                recipient_kind = biz_user.Kind();
            }
            if (recipient_id.Length == 0)
            {
                recipient_id = biz_user.IdNum();
            }
            if (fy_id.Length == 0)
            {
                fy_id = biz_fiscal_years.IdOfCurrent();
            }
            result = db_appropriations.SumOfAppropriationsFromOnlyParent(recipient_kind, recipient_id, fy_id);
            return result;
        }

        public decimal SumOfAppropriationsFromOnlyParent()
        {
            return SumOfAppropriationsFromOnlyParent("");
        }

        public decimal SumOfAppropriationsFromOnlyParent(string recipient_kind)
        {
            return SumOfAppropriationsFromOnlyParent(recipient_kind, "");
        }

        public decimal SumOfAppropriationsFromOnlyParent(string recipient_kind, string recipient_id)
        {
            return SumOfAppropriationsFromOnlyParent(recipient_kind, recipient_id, "");
        }

        public decimal SumOfAppropriationsToServicesInRegion(string region_id, string fy_id)
        {
            decimal result;
            result = db_appropriations.SumOfAppropriationsToServicesInRegion(region_id, fy_id);
            return result;
        }

        public decimal SumOfSelfDictatedAppropriations(string fy_id)
        {
            decimal result;
            TClass_biz_user biz_user;
            biz_user = new TClass_biz_user();
            if (fy_id.Length == 0)
            {
                result = db_appropriations.SumOfSelfDictatedAppropriations(biz_user.Kind(), biz_user.IdNum(), biz_fiscal_years.IdOfCurrent());
            }
            else
            {
                result = db_appropriations.SumOfSelfDictatedAppropriations(biz_user.Kind(), biz_user.IdNum(), fy_id);
            }
            return result;
        }

        public decimal SumOfSelfDictatedAppropriations()
        {
            return SumOfSelfDictatedAppropriations("");
        }

        public decimal SumOfSubAppropriations
          (
          string parent_kind,
          string parent_appropriation_id
          )
          {
          return db_appropriations.SumOfSubAppropriations(parent_kind,parent_appropriation_id);
          }

    } // end TClass_biz_appropriations

}
