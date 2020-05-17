using MySql.Data.MySqlClient;
using System;

using Class_biz_fiscal_years;
using Class_db;
using Class_db_trail;
namespace Class_db_milestones
{
    public class TClass_db_milestones: TClass_db
    {
        private readonly TClass_biz_fiscal_years biz_fiscal_years = null;
        private readonly TClass_db_trail db_trail = null;

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
            Open();
            using var my_sql_command = new MySqlCommand("select be_processed from fy_calendar" + " where fiscal_year_id = " + biz_fiscal_years.IdOfCurrent() + " and milestone_code = " + code.ToString(), connection);
            result = "1" == my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public void Check(uint code, out bool be_processed, out DateTime value)
        {
            be_processed = true;
            value = DateTime.MaxValue;
            Open();
            using var my_sql_command = new MySqlCommand("select be_processed,value from fy_calendar where fiscal_year_id = '" + biz_fiscal_years.IdOfCurrent() + "' and milestone_code = '" + code.ToString() + "'", connection);
            var dr = my_sql_command.ExecuteReader();
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
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved(cmdText), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

    } // end TClass_db_milestones

}
