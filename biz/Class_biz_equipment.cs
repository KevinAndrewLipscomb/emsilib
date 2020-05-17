using Class_biz_match_level;
using Class_db_emsof_requests;
using Class_db_equipment;
using Class_db_services;
using kix;
using System;
using System.Collections;

namespace Class_biz_equipment
{
    public class TClass_biz_equipment
    {
        private readonly TClass_db_emsof_requests db_emsof_requests = null;
        private readonly TClass_db_equipment db_equipment = null;
        private readonly TClass_db_services db_services = null;

        //Constructor  Create()
        public TClass_biz_equipment() : base()
        {
            // TODO: Add any constructor code here
            db_emsof_requests = new TClass_db_emsof_requests();
            db_equipment = new TClass_db_equipment();
            db_services = new TClass_db_services();
        }

        public bool BeMatchExempt(string code, nominal_type match_level_enum)
        {
            var be_match_exempt = false;
            var allowable_cost = db_equipment.AllowableCostOf(code);
            if (match_level_enum == nominal_type.STANDARD)
              {
              be_match_exempt = (allowable_cost == db_equipment.FundingLevelNonRuralOf(code));
              }
            else if (match_level_enum == nominal_type.RURAL)
              {
              be_match_exempt = (allowable_cost == db_equipment.FundingLevelRuralOf(code));
              }
            else if (match_level_enum == nominal_type.DISTRESSED)
              {
              be_match_exempt = (allowable_cost == db_equipment.FundingLevelNonRuralOf(code)) || (allowable_cost == db_equipment.FundingLevelRuralOf(code));
              }
            return be_match_exempt;
        }

        public void CostsAndAntes
          (
          string unit_cost_string,
          string quantity_string,
          string additional_service_ante_string,
          decimal match_level,
          decimal allowable_cost,
          decimal funding_level,
          ref decimal additional_service_ante,
          ref string total_cost_string,
          ref string emsof_ante_string,
          ref string min_service_ante_string
          )
          {
          decimal effective_emsof_ante;
          decimal max_emsof_ante;
          var quantity = new k.decimal_nonnegative();
          decimal total_cost;
          var unit_cost = new k.decimal_nonnegative();
          if ((unit_cost_string.Length > 0) && (quantity_string.Length > 0))
            {
            unit_cost.val = decimal.Parse(unit_cost_string);
            quantity.val = decimal.Parse(quantity_string);
            if (additional_service_ante_string.Length > 0)
              {
              additional_service_ante = decimal.Parse(additional_service_ante_string);
              }
            else
              {
              additional_service_ante = 0;
              }
            total_cost = unit_cost.val * quantity.val;
            total_cost_string = total_cost.ToString("N2");
            // This is the zebra case where a distressed service wants an item with no specified allowable cost (initially an ambulance
            // or squad/response vehicle).  Basically, they can have whatever they want, up to the limit of the remainder of their
            // appropriation.  Consideration of their appropriation is not within the scope of this form, so we can indicate that the
            // request can be fully funded.
            // This is the case where items in an "equipment category" are always fully funded (up to the limit of a service's
            // appropriation, which is not within the scope of this form).  This initially describes only Data Collection Software and
            // the EMT-P Written Test.  The "Other - with external documentation" category is specifically excluded from this case.
            if ((decimal.Equals(match_level,1.00) && (allowable_cost == decimal.MaxValue)) || ((unit_cost.val <= allowable_cost) && (funding_level == allowable_cost) && (allowable_cost < decimal.MaxValue)))
              {
              max_emsof_ante = total_cost;
              }
            else
              {
              if (unit_cost.val > allowable_cost)
                {
                max_emsof_ante = Math.Max(allowable_cost * match_level, funding_level) * quantity.val;
                }
              else
                {
                max_emsof_ante = total_cost * match_level;
                }
              }
            // A service may elect not to use the max_emsof_ante.  An example would be when they know that doing so, in the context of all
            // their other request items, would draw more EMSOF funds than they were appropriated.  So account for if they want to ante up
            // more of the cost themselves.
            effective_emsof_ante = max_emsof_ante - additional_service_ante;
            emsof_ante_string = effective_emsof_ante.ToString("N2");
            min_service_ante_string = (total_cost - max_emsof_ante).ToString("N2");
            }
          }

        internal decimal FundingLevel
          (
          string code,
          decimal match_level,
          decimal allowable_cost
          )
          {
          decimal funding_level;
          if (decimal.Equals(match_level,0.60))
            {
            funding_level = db_equipment.FundingLevelRuralOf(code);
            }
          else if (decimal.Equals(match_level,1.00))
            {
            funding_level = allowable_cost;
            }
          else
            {
            funding_level = db_equipment.FundingLevelNonRuralOf(code);
            }
          return funding_level;
          }

        public string SpecialRulesViolation(string service_id, string master_id, string priority, string code, string quantity_string)
        {
            string result;
            uint i;
            string name;
            Queue q;
            string special_rules_violation;
            special_rules_violation = k.EMPTY;
            q = db_equipment.SpecialRuleNames(code);
            uint q_count = (uint)(q.Count);
            for (i = 1; i <= q_count; i ++ )
            {
                name = q.Dequeue().ToString();
                // UpTo1PerVehicle
                if ((name == "UpTo1PerVehicle") && (uint.Parse(quantity_string) + db_emsof_requests.NumCompetingEquipmentItems(code, service_id, master_id, priority) > db_services.NumDohLicensedVehiclesOf(service_id)))
                {
                    special_rules_violation = special_rules_violation + "The quantity of the requested items, plus those you\'ve procured from recent EMSOF cycles, exceeds 1 per DOH licensed vehicle." + k.SPACE + k.SPACE;
                // UpTo2PerVehicle
                }
                else if ((name == "UpTo2PerVehicle") && (uint.Parse(quantity_string) + db_emsof_requests.NumCompetingEquipmentItems(code, service_id, master_id, priority) > db_services.NumDohLicensedVehiclesOf(service_id) * 2))
                {
                    special_rules_violation = special_rules_violation + "The quantity of the requested items, plus those you\'ve procured from recent EMSOF cycles, exceeds 2 per DOH licensed vehicle." + k.SPACE + k.SPACE;
                // UpTo5
                }
                else if ((name == "UpTo5") && (uint.Parse(quantity_string) > 5))
                {
                    special_rules_violation = special_rules_violation + "The quantity of the requested items exceeds 5." + k.SPACE + k.SPACE;
                // HasMedicalDirector
                }
                else if ((name == "HasMedicalDirector") && (db_services.MdNameOf(service_id).Length == 0))
                {
                    special_rules_violation = special_rules_violation + "To request this item, your service\'s profile (annual survey) must first specify a Medical Director." + k.SPACE + k.SPACE;
                // UpTo1PerAmbulance
                }
                else if ((name == "UpTo1PerAmbulance") && (uint.Parse(quantity_string) + db_emsof_requests.NumCompetingEquipmentItems(code, service_id, master_id, priority) > db_services.NumAmbulancesOf(service_id)))
                {
                    special_rules_violation = special_rules_violation + "The quantity of the requested items, plus those you\'ve procured from recent EMSOF cycles, exceeds 1 per ambulance." + k.SPACE + k.SPACE;
                }
            }
            result = special_rules_violation;

            return result;
        }

        public void BindDescriptionsOnlyToListControl(object target)
        {
            db_equipment.BindDescriptionsOnlyToListControl(target);
        }

        public void BindListControl(string fy_id, object target)
        {
            db_equipment.BindListControl(fy_id, target);
        }

        public Queue SerialIndicatorData(string indicator, string description)
        {
            Queue result;
            result = db_equipment.SerialIndicatorData(indicator, description);
            return result;
        }

    } // end TClass_biz_equipment

}
