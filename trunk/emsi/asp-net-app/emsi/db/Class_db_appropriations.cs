using Class_biz_fiscal_years;
using Class_biz_regional_staffers;
using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI.WebControls;

namespace Class_db_appropriations
{
    public class TClass_db_appropriations: TClass_db
    {

    private class region_dictum_summary
      {
      public string amount;
      public string county_code;
      public string county_name;
      public string id;
      public string match_level_id;
      public string match_level_description;
      public string service_to_county_submission_deadline;
      }

        private readonly TClass_biz_fiscal_years biz_fiscal_years = null;
        private readonly TClass_biz_regional_staffers biz_regional_staffers = null;
        private readonly TClass_db_trail db_trail = null;

        //Constructor  Create()
        public TClass_db_appropriations() : base()
        {
            // TODO: Add any constructor code here
            biz_fiscal_years = new TClass_biz_fiscal_years();
            biz_regional_staffers = new TClass_biz_regional_staffers();
            db_trail = new TClass_db_trail();
        }
        public void ApplyToExisting(string affiliate_num, bool be_distressed)
        {
            string match_level_id_expression;
            if (be_distressed)
            {
                match_level_id_expression = "(select id from match_level where factor = 1)";
            }
            else
            {
                match_level_id_expression = "region_dictated_appropriation.match_level_id";
            }
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("update county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join fiscal_year on (fiscal_year.id=state_dictated_appropriation.fiscal_year_id)" + " join service on (service.id=county_dictated_appropriation.service_id)" + " set county_dictated_appropriation.match_level_id = " + match_level_id_expression + " where service_id = (select id from service where affiliate_num = \"" + affiliate_num + "\")" + " and fiscal_year_id = (select max(id) from fiscal_year)"), connection);
            my_sql_command.ExecuteNonQuery();
            Close();

        }

        public bool BeAnyCurrentToService(string affiliate_num)
        {
            bool result;
            Open();
            using var my_sql_command = new MySqlCommand("select count(*)" + " from county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join service on (service.id=county_dictated_appropriation.service_id)" + " where fiscal_year_id = \"" + biz_fiscal_years.IdOfCurrent() + "\"" + " and affiliate_num = \"" + affiliate_num + "\"", connection);
            result = "0" != my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        internal void BindRegionDictums
          (
          string sort_order,
          bool be_sort_order_ascending,
          object target
          )
          {
          if (be_sort_order_ascending)
            {
            sort_order = sort_order.Replace("%", " asc");
            }
          else
            {
            sort_order = sort_order.Replace("%", " desc");
            }
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select state_dictated_appropriation.id as sda_id"
            + " , region_dictated_appropriation.id as id"
            + " , county_code as code"
            + " , password_reset_email_address as email_address"
            + " , county_code_name_map.name as name"
            + " , region_dictated_appropriation.amount"
            + " , service_to_county_submission_deadline as deadline"
            + " , concat(match_level.name,' (',FORMAT(factor*100,0),'%)') as match_level_description"
            + " from region_dictated_appropriation"
            +   " join county_user on (county_user.id=region_dictated_appropriation.county_code)"
            +   " join county_code_name_map on (county_code_name_map.code=region_dictated_appropriation.county_code)"
            +   " join match_level on (match_level.id=region_dictated_appropriation.match_level_id)"
            +   " join state_dictated_appropriation on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)"
            + " where fiscal_year_id = (select max(id) from fiscal_year)"
            + " order by " + sort_order,
            connection
            );
          ((target) as DataGrid).DataSource = my_sql_command.ExecuteReader();
          ((target) as DataGrid).DataBind();
          Close();
          }

        internal void BindStateDictums
          (
          string sort_order,
          bool be_sort_order_ascending,
          object target
          )
          {
          if (be_sort_order_ascending)
            {
            sort_order = sort_order.Replace("%", " asc");
            }
          else
            {
            sort_order = sort_order.Replace("%", " desc");
            }
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select id"
            + " , amount"
            + " from state_dictated_appropriation"
            + " where fiscal_year_id = (select max(id) from fiscal_year)"
            + " order by " + sort_order,
            connection
            );
          ((target) as DataGrid).DataSource = my_sql_command.ExecuteReader();
          ((target) as DataGrid).DataBind();
          Close();
          }

        public string CountyCodeOfCountyDictum(string county_dictum_id)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select county_code" + " from county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " where county_dictated_appropriation.id = " + county_dictum_id, connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        internal string CountyCodeOfRegionDictumSummary(object summary)
          {
          return (summary as region_dictum_summary).county_code;
          }

        internal string CountyNameOfRegionDictumSummary(object summary)
          {
          return(summary as region_dictum_summary).county_name;
          }

        internal bool Delete
          (
          string dictator,
          string id
          )
          {
          var result = true;
          Open();
          try
            {
            using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from " + dictator + "_dictated_appropriation where id = '" + id + "'"),connection);
            my_sql_command.ExecuteNonQuery();
            }
          catch(System.Exception e)
            {
            if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
              {
              result = false;
              }
            else
              {
              throw;
              }
            }
          Close();
          return result;
          }

        public uint FundingRoundsGenerated(string regional_staffer_id, string amendment_num_string)
        {
            uint result;
            Open();
            using var my_sql_command = new MySqlCommand("select funding_rounds_generated" + " from state_dictated_appropriation" + " where fiscal_year_id = " + biz_fiscal_years.IdOfCurrent() + " and region_code = " + biz_regional_staffers.RegionCodeOf(regional_staffer_id) + " and amendment_num = " + amendment_num_string, connection);
            result = (uint)(my_sql_command.ExecuteScalar().GetHashCode());
            Close();
            return result;
        }

        public void IncFundingRoundsGenerated(string regional_staffer_id, string amendment_num_string)
        {
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("update state_dictated_appropriation" + " set funding_rounds_generated = funding_rounds_generated + 1" + " where fiscal_year_id = " + biz_fiscal_years.IdOfCurrent() + " and region_code = " + biz_regional_staffers.RegionCodeOf(regional_staffer_id) + " and amendment_num = " + amendment_num_string), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

        public decimal MatchFactorOf(string county_dictum_id)
        {
            decimal result;
            Open();
            using var my_sql_command = new MySqlCommand("select factor" + " from county_dictated_appropriation" + " join match_level on (match_level.id=county_dictated_appropriation.match_level_id)" + " where county_dictated_appropriation.id = " + county_dictum_id, connection);
            result = (decimal)(my_sql_command.ExecuteScalar());
            Close();
            return result;
        }

        public uint MatchLevelIdOf(string county_dictum_id)
        {
            uint result;
            Open();
            using var my_sql_command = new MySqlCommand("select match_level_id" + " from county_dictated_appropriation" + " where county_dictated_appropriation.id = " + county_dictum_id, connection);
            result = uint.Parse(my_sql_command.ExecuteScalar().ToString());
            Close();
            return result;
        }

        public uint MatchLevelIdOfRegionDictum(string region_dictum_id)
        {
            uint result;
            Open();
            using var my_sql_command = new MySqlCommand("select match_level_id" + " from region_dictated_appropriation" + " where region_dictated_appropriation.id = " + region_dictum_id, connection);
            result = uint.Parse(my_sql_command.ExecuteScalar().ToString());
            Close();
            return result;
        }

        public uint NumActiveAmendments(string regional_staffer_id)
        {
            uint result;
            Open();
            using var my_sql_command = new MySqlCommand("select count(id)" + " from state_dictated_appropriation" + " where fiscal_year_id = " + biz_fiscal_years.IdOfCurrent() + " and region_code = " + biz_regional_staffers.RegionCodeOf(regional_staffer_id), connection);
            result =  (uint)(-1 + my_sql_command.ExecuteScalar().GetHashCode());
            Close();
            return result;
        }

        public decimal ParentAppropriationOfEmsofRequest(string master_id)
        {
            decimal result;
            Open();
            using var my_sql_command = new MySqlCommand("select county_dictated_appropriation.amount" + " from county_dictated_appropriation" + " join emsof_request_master on (emsof_request_master.county_dictated_appropriation_id=county_dictated_appropriation.id)" + " where emsof_request_master.id = " + master_id, connection);
            result = (decimal)(my_sql_command.ExecuteScalar());
            Close();
            return result;
        }

        public void ReduceBy(decimal delta, string county_dictum_id)
        {
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("update county_dictated_appropriation" + " set amount = amount - " + delta.ToString() + " where id = " + county_dictum_id), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

        public string RegionCodeOfCountyDictum(string county_dictum_id)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select region_code" + " from county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where county_dictated_appropriation.id = " + county_dictum_id, connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        internal string RegionDictumIdOf(object summary)
          {
          return (summary as region_dictum_summary).id;
          }

        internal object RegionDictumSummary(string id)
          {
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select county_code"
            + " , county_code_name_map.name as county_name"
            + " , amount"
            + " , service_to_county_submission_deadline"
            + " , match_level_id"
            + " , match_level.name as match_level_description"
            + " from region_dictated_appropriation"
            +   " join county_code_name_map on (county_code_name_map.code=region_dictated_appropriation.county_code)"
            +   " join match_level on (match_level.id=region_dictated_appropriation.match_level_id)"
            + " where region_dictated_appropriation.id = '" + id + "'",
            connection
            );
          var dr = my_sql_command.ExecuteReader();
          dr.Read();
          var the_summary = new region_dictum_summary()
            {
            amount = dr["amount"].ToString(),
            county_code = dr["county_code"].ToString(),
            county_name = dr["county_name"].ToString(),
            id = id,
            match_level_id = dr["match_level_id"].ToString(),
            match_level_description = dr["match_level_description"].ToString(),
            service_to_county_submission_deadline = dr["service_to_county_submission_deadline"].ToString()
            };
          dr.Close();
          Close();
          return the_summary;
          }

        public void SetNewStateToRegion(decimal amount)
        {
            string settings;
            settings = " region_code = 1" + " , fiscal_year_id = (select max(id) from fiscal_year)" + " , amount = \"" + amount.ToString() + "\"";
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("insert state_dictated_appropriation" + " set " + settings + " on duplicate key update " + settings), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

        public void SetServiceToCountySubmissionDeadline(string id, string deadline)
        {
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("update region_dictated_appropriation" + " set service_to_county_submission_deadline = \"" + deadline + "\"" + " where id = " + id), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

        public decimal SumOfAppropriationsFromSpecificParent(string parent_id, string recipient_kind, string recipient_id, string fy_id)
        {
            decimal result;
            string cmdText = k.EMPTY;
            if (recipient_kind == "service")
            {
                cmdText = "select sum(county_dictated_appropriation.amount)" + " from county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where service_id = " + recipient_id + " and county_code = " + parent_id + " and fiscal_year_id = " + fy_id;
            }
            else if (recipient_kind == "county")
            {
                cmdText = "select sum(region_dictated_appropriation.amount)" + " from region_dictated_appropriation" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.region_code=region_dictated_appropriation.state_dictated_appropriation_id)" + " where county_code = " + recipient_id + " and region_code = " + parent_id + " and fiscal_year_id = " + fy_id;
            }
            Open();
            using var my_sql_command = new MySqlCommand(cmdText, connection);
            result = (decimal)(my_sql_command.ExecuteScalar());
            Close();
            return result;
        }

        public decimal SumOfAppropriationsFromOnlyParent(string recipient_kind, string recipient_id, string fy_id)
        {
            decimal result;
            string cmdText = k.EMPTY;
            if (recipient_kind == "regional_staffer")
            {
                cmdText = "select sum(state_dictated_appropriation.amount)" + " from state_dictated_appropriation" + " join regional_staffer on (regional_staffer.region_code=state_dictated_appropriation.region_code)" + " where regional_staffer.id = " + recipient_id + " and fiscal_year_id = " + fy_id;
            }
            else if (recipient_kind == "county")
            {
                cmdText = "select sum(region_dictated_appropriation.amount)" + " from region_dictated_appropriation" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.region_code=region_dictated_appropriation.state_dictated_appropriation_id)" + " where county_code = " + recipient_id + " and fiscal_year_id = " + fy_id;
            }
            Open();
            using var my_sql_command = new MySqlCommand(cmdText, connection);
            result = (decimal)(my_sql_command.ExecuteScalar());
            Close();
            return result;
        }

        public decimal SumOfAppropriationsToServicesInRegion(string region_id, string fy_id)
        {
            decimal result;
            object sum_obj;
            Open();
            using var my_sql_command = new MySqlCommand("select sum(county_dictated_appropriation.amount)" + " from county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where region_code = " + region_id + " and fiscal_year_id = " + fy_id, connection);
            sum_obj = my_sql_command.ExecuteScalar();
            if (sum_obj == DBNull.Value)
            {
                result = 0;
            }
            else
            {
                result = (decimal)(sum_obj);
            }
            Close();
            return result;
        }

        public decimal SumOfSelfDictatedAppropriations(string self_kind, string self_id, string fy_id)
        {
            decimal result;
            string cmdText = k.EMPTY;
            object sum_obj;
            if (self_kind == "regional_staffer")
            {
                cmdText = "select sum(region_dictated_appropriation.amount)" + " from region_dictated_appropriation" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join regional_staffer on (regional_staffer.region_code=state_dictated_appropriation.region_code)" + " where regional_staffer.id = " + self_id + " and fiscal_year_id = " + fy_id;
            }
            else if (self_kind == "county")
            {
                cmdText = "select sum(county_dictated_appropriation.amount)" + " from county_dictated_appropriation" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where county_code = " + self_id + " and fiscal_year_id = " + fy_id;
            }
            Open();
            using var my_sql_command = new MySqlCommand(cmdText, connection);
            sum_obj = my_sql_command.ExecuteScalar();
            if (sum_obj == DBNull.Value)
            {
                result = 0;
            }
            else
            {
                result = (decimal)(sum_obj);
            }
            Close();

            return result;
        }

        internal decimal SumOfSubAppropriations(string parent_kind, string parent_appropriation_id)
          {
          var child_kind = k.EMPTY;
          if (parent_kind == "region")
            {
            child_kind = "county";
            }
          else if (parent_kind == "state")
            {
            child_kind = "region";
            }
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "select IFNULL(sum(amount),0) from " + child_kind + "_dictated_appropriation where " + child_kind + "_dictated_appropriation." + parent_kind + "_dictated_appropriation_id = '" + parent_appropriation_id + "'",
            connection
            );
          var sum = decimal.Parse(my_sql_command.ExecuteScalar().ToString());
          Close();
          return sum;
          }

    } // end TClass_db_appropriations

}
