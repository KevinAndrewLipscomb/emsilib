using MySql.Data.MySqlClient;
using System;

using Class_biz_fiscal_years;
using Class_db;
using Class_db_trail;
namespace Class_db_milestones
{
    public class TClass_db_milestones: TClass_db
    {
        private TClass_biz_fiscal_years biz_fiscal_years = null;
        private TClass_db_trail db_trail = null;

        //Constructor  Create()
        public TClass_db_milestones() : base()
        {
            // TODO: Add any constructor code here
            biz_fiscal_years = new TClass_biz_fiscal_years();
            db_trail = new TClass_db_trail();
        }
        public bool BeProcessed(uint code)
        {
            bool result;
            this.Open();
            result = "1" == new MySqlCommand("select be_processed from fy_calendar" + " where fiscal_year_id = " + biz_fiscal_years.IdOfCurrent() + " and milestone_code = " + code.ToString(), this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public void Check(uint code, out bool be_processed, out DateTime value)
        {
            be_processed = true;
            value = DateTime.MaxValue;
            Open();
            var dr = new MySqlCommand("select be_processed,value from fy_calendar where fiscal_year_id = '" + biz_fiscal_years.IdOfCurrent() + "' and milestone_code = '" + code.ToString() + "'", connection).ExecuteReader();
            if (dr.Read())
              {
              be_processed = (dr["be_processed"].ToString() == "1");
              value = DateTime.Parse(dr["value"].ToString());
              }
            dr.Close();
            Close();
        }

        public void MarkProcessed(uint code)
        {
            string cmdText;
            cmdText = "update fy_calendar" + " set be_processed = TRUE" + " where fiscal_year_id = " + biz_fiscal_years.IdOfCurrent() + " and milestone_code = " + code.ToString();
            this.Open();
            new MySqlCommand(db_trail.Saved(cmdText), this.connection).ExecuteNonQuery();
            this.Close();
        }

    } // end TClass_db_milestones

}
