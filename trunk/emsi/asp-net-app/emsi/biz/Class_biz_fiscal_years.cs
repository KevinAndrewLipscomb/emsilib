using Class_db_fiscal_years;
using System;

namespace Class_biz_fiscal_years
{
    public class TClass_biz_fiscal_years
    {
        private readonly TClass_db_fiscal_years db_fiscal_years = null;
        private string new_designator = String.Empty;

        //Constructor  Create()
        public TClass_biz_fiscal_years() : base()
        {
            string next_year_string;
            uint this_year_cardinal;
            string this_year_string;
            db_fiscal_years = new TClass_db_fiscal_years();
            this_year_cardinal = (uint)(DateTime.Now.Year);
            this_year_string = this_year_cardinal.ToString();
            next_year_string = ((uint)(this_year_cardinal + 1)).ToString();
            new_designator = "FY" + this_year_string.Substring(this_year_string.Length - 2) + next_year_string.Substring(next_year_string.Length - 2);

        }

        public void BindAllocationsReimbursementsReturns
          (
          string id,
          string sort_order,
          bool be_order_ascending,
          object target
          )
          {
          db_fiscal_years.BindAllocationsReimbursementsReturns(id,sort_order,be_order_ascending,target);
          }

        public void BindListControl(object target)
        {
            db_fiscal_years.BindListControl(target);
        }

        public string DesignatorOfCurrent()
        {
            string result;
            result = db_fiscal_years.DesignatorOfCurrent();
            return result;
        }

        public string IdOfCurrent()
        {
            string result;
            result = db_fiscal_years.IdOfCurrent();
            return result;
        }

        public string IdOfDesignator(string designator)
        {
            string result;
            result = db_fiscal_years.IdOfDesignator(designator);
            return result;
        }

        public string NewDesignator()
        {
            string result;
            result = new_designator;
            return result;
        }

        public void StartNew()
        {
            db_fiscal_years.StartNew(new_designator);
        }

    } // end TClass_biz_fiscal_years

}
