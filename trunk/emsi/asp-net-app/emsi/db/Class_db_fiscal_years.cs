using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI.WebControls;

namespace Class_db_fiscal_years
{
    public class TClass_db_fiscal_years: TClass_db
    {
        private readonly TClass_db_trail db_trail = null;

        //Constructor  Create()
        public TClass_db_fiscal_years() : base()
        {
            db_trail = new TClass_db_trail();
        }

        internal void BindAllocationsReimbursementsReturns
          (
          string id,
          string sort_order,
          bool be_order_ascending,
          object target
          )
          {
          if (be_order_ascending)
            {
            sort_order = sort_order.Replace("%", " asc");
            }
          else
            {
            sort_order = sort_order.Replace("%", " desc");
            }
          var sql = k.EMPTY
          + "select emsof_request_master.id as master_id"
          + " , service.name as service_name"
          + " , county_code_name_map.name as county_name"
          + " , @allocation := county_dictated_appropriation.amount as allocation"
          + " , @reimbursement := IF(status_code not in (11,12,16),emsof_request_master.actual_value,0) as reimbursement"
          + " , @allocation - @reimbursement as amount_returned"
          + " from fiscal_year"
            + " join state_dictated_appropriation on (state_dictated_appropriation.fiscal_year_id=fiscal_year.id)"
            + " join region_dictated_appropriation on (region_dictated_appropriation.state_dictated_appropriation_id=state_dictated_appropriation.id)"
            + " join county_dictated_appropriation on (county_dictated_appropriation.region_dictated_appropriation_id=region_dictated_appropriation.id)"
            + " join emsof_request_master on (emsof_request_master.id=county_dictated_appropriation.id)"
            + " join service on (service.id=county_dictated_appropriation.service_id)"
            + " join county_code_name_map on (county_code_name_map.code=region_dictated_appropriation.county_code)"
          + " where fiscal_year.id = '" + id + "'"
          + " order by " + sort_order;
          Open();
          using var my_sql_command = new MySqlCommand(sql,connection);
          (target as BaseDataList).DataSource = my_sql_command.ExecuteReader();
          (target as BaseDataList).DataBind();
          ((target as BaseDataList).DataSource as MySqlDataReader).Close();
          Close();
          }

        public void BindListControl(object target)
        {
            Open();
            using var my_sql_command = new MySqlCommand("select id,designator from fiscal_year order by id desc", connection);
            ((target) as ListControl).DataSource = my_sql_command.ExecuteReader();
            ((target) as ListControl).DataValueField = "id";
            ((target) as ListControl).DataTextField = "designator";
            ((target) as ListControl).DataBind();
            Close();
        }

        public string DesignatorOfCurrent()
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select designator from fiscal_year order by id desc limit 1", connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public string IdOfCurrent()
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select max(id) from fiscal_year", connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public string IdOfDesignator(string designator)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select id from fiscal_year where designator = \"" + designator + "\"", connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public void StartNew(string designator)
        {
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("insert ignore fiscal_year set designator = \"" + designator + "\""), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

    } // end TClass_db_fiscal_years

}
