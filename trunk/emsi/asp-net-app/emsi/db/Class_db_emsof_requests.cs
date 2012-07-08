using Class_biz_milestones;
using Class_db;
using Class_db_fiscal_years;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Class_db_emsof_requests
{
    public enum approval_timestamp_column_type
      {
      NONE,
      COUNTY,
      REGIONAL_PLANNER,
      REGIONAL_DIRECTOR,
      STATE,
      }

    public class Class_db_emsof_requests_Static
      {
      // column 0
      // column 1
      // column 2
      // column 3
      // column 4
      // column 5
      // column 6
      // column 7
      // column 8
      // column 9
      // column 10
      // column 11
      // column 12
      // column 13
      public const string FULL_REQUEST_REVIEW_APPROVE_SELECT_FROM_EXPRESSION = k.EMPTY
      + "select emsof_request_master.id"
      + " , service.id as service_id"
      + " , service.affiliate_num"
      + " , service.name as service_name"
      + " , county_code_name_map.code as county_code"
      + " , county_code_name_map.name as sponsor_county"
      + " , county_user.password_reset_email_address as county_email_address"
      + " , fiscal_year.designator as fiscal_year_designator"
      + " , if(emsof_request_master.value > emsof_request_master.actual_value,emsof_request_master.value,emsof_request_master.actual_value) as emsof_ante"
      + " , county_dictated_appropriation.amount as appropriation"
      + " , if((county_dictated_appropriation.amount > emsof_request_master.value),(county_dictated_appropriation.amount - emsof_request_master.value),(-emsof_request_master.shortage)) as leftover_or_shortage"
      + " , if(has_wish_list,\"YES\",\"no\") as has_wish_list"
      + " , service_user.password_reset_email_address as password_reset_email_address"
      + " , status_code"
      + " , request_status_code_description_map.description as status_description"
      + " from emsof_request_master"
      +   " join county_dictated_appropriation on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)"
      +   " join region_dictated_appropriation on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)"
      +   " join county_code_name_map on (county_code_name_map.code=region_dictated_appropriation.county_code)"
      +   " join service on (service.id=county_dictated_appropriation.service_id)"
      +   " join service_user on (service_user.id=service.id)"
      +   " join state_dictated_appropriation on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)"
      +   " join fiscal_year on (fiscal_year.id=state_dictated_appropriation.fiscal_year_id)"
      +   " join request_status_code_description_map on (request_status_code_description_map.code=status_code)"
      +   " join county_user on (county_user.id=region_dictated_appropriation.county_code)";
      public const int MAX_FULL_REQUEST_REVIEW_APPROVE_DGI_INDEX = 13;
      public const int TCCI_ID = 0;
      public const int TCCI_SERVICE_ID = 1;
      public const int TCCI_AFFILIATE_NUM = 2;
      public const int TCCI_SERVICE_NAME = 3;
      public const int TCCI_SPONSOR_COUNTY_ID = 4;
      public const int TCCI_SPONSOR_COUNTY_NAME = 5;
      public const int TCCI_SPONSOR_COUNTY_EMAIL_ADDRESS = 6;
      public const int TCCI_FISCAL_YEAR_DESIGNATOR = 7;
      public const int TCCI_EMSOF_ANTE = 8;
      public const int TCCI_APPROPRIATION = 9;
      public const int TCCI_LEFTOVER_OR_SHORTAGE = 10;
      public const int TCCI_HAS_WISH_LIST = 11;
      public const int TCCI_PASSWORD_RESET_EMAIL_ADDRESS = 12;
      public const int TCCI_STATUS_CODE = 13;
      public const int TCCI_STATUS_DESCRIPTION = 14;
      public const int TCCI_FULL_REQUEST_PRIORITY = 0;
      public const int TCCI_FULL_REQUEST_ALLOWABLE_COST = 1;
      public const int TCCI_FULL_REQUEST_DETAIL = 2;
      public const int TCCI_FULL_REQUEST_ACTUALS = 3;
      public const int TCCI_PROOF_OF_PAYMENT_ID = 0;
      public const int TCCI_SRR_REPLACEMENT_ROW_INDICATOR = 1;
      }

    public class TClass_db_emsof_requests: TClass_db
    {
        private TClass_db_fiscal_years db_fiscal_years = null;
        private TClass_db_trail db_trail = null;
        private void BindOverview(string order_by_field_name, bool be_order_ascending, object target, string where_parm, string and_parm)
        {
            string cmdText;
            string where_clause;
            if (where_parm == k.EMPTY)
            {
                where_clause = k.EMPTY;
            }
            else
            {
                where_clause = "where " + where_parm;
                if (and_parm != k.EMPTY)
                {
                    where_clause = where_clause + " and " + and_parm;
                }
            }
            cmdText = Class_db_emsof_requests_Static.FULL_REQUEST_REVIEW_APPROVE_SELECT_FROM_EXPRESSION + where_clause + " order by " + order_by_field_name;
            if (be_order_ascending)
            {
                cmdText = cmdText + " asc";
            }
            else
            {
                cmdText = cmdText + " desc";
            }
            this.Open();
            ((target) as DataGrid).DataSource = new MySqlCommand(cmdText, this.connection).ExecuteReader();
            ((target) as DataGrid).DataBind();
            ((MySqlDataReader)(((DataGrid)(target)).DataSource)).Close();
            this.Close();
        }

        private void BindOverview(string order_by_field_name, bool be_order_ascending, object target)
        {
            BindOverview(order_by_field_name, be_order_ascending, target, "");
        }

        private void BindOverview(string order_by_field_name, bool be_order_ascending, object target, string where_parm)
        {
            BindOverview(order_by_field_name, be_order_ascending, target, where_parm, "");
        }

        //Constructor  Create()
        public TClass_db_emsof_requests() : base()
        {
            // TODO: Add any constructor code here
            db_trail = new TClass_db_trail();
            db_fiscal_years = new TClass_db_fiscal_years();
        }
        public decimal ActualValueOf(string master_id)
        {
            decimal result;
            this.Open();
            result = (decimal)(new MySqlCommand("select actual_value from emsof_request_master where id = " + master_id, this.connection).ExecuteScalar());
            this.Close();
            return result;
        }

        public void AddProofOfPayment(string request_id, DateTime date_of_payment, string method_code, decimal amount, string note)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("insert into emsof_purchase_payment" + " set master_id = " + request_id + " , date_of = \"" + date_of_payment.ToString("yyyy-MM-dd") + "\"" + " , method_code = " + method_code + " , amount = " + amount.ToString() + " , note = \"" + note + "\""), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public string AffiliateNumOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_AFFILIATE_NUM].Text, k.safe_hint_type.NUM);
            return result;
        }

        public void Approve(string master_id, uint next_status, string user_kind, approval_timestamp_column_type approval_timestamp_column, decimal amount_to_return_to_county)
        {
            string cmdText;
            if (approval_timestamp_column != approval_timestamp_column_type.NONE)
            {
                cmdText = k.EMPTY;
                if ((amount_to_return_to_county > 0) || (user_kind == "state-staffer") || (next_status == 8))
                {
                    cmdText = "START TRANSACTION; ";
                }
                cmdText = cmdText + " update emsof_request_master" + " set status_code = " + next_status.ToString() + " , " + (approval_timestamp_column).ToString() + "_approval_timestamp = now()";
                if (next_status == 8)
                {
                    cmdText = cmdText + " , be_reopened_after_going_to_state = FALSE";
                }
                cmdText = cmdText + " where id = " + master_id + "; ";
                if (next_status == 8)
                {
                    cmdText = cmdText + "update emsof_request_detail" + " set status_code = 2" + " where master_id = " + master_id + "; ";
                }
                if (amount_to_return_to_county > 0)
                {
                    cmdText = cmdText + " update county_dictated_appropriation" + " set amount = amount - " + amount_to_return_to_county.ToString() + " where id = " + " (" + " select county_dictated_appropriation_id" + " from emsof_request_master" + " where emsof_request_master.id = " + master_id + " ); ";
                }
                if ((amount_to_return_to_county > 0) || (user_kind == "state-staffer") || (next_status == 8))
                {
                    cmdText = cmdText + " COMMIT;";
                }
                this.Open();
                new MySqlCommand(db_trail.Saved(cmdText), this.connection).ExecuteNonQuery();
                this.Close();
            }
        }

        public void Approve(string master_id, uint next_status, string user_kind, approval_timestamp_column_type approval_timestamp_column)
        {
            Approve(master_id, next_status, user_kind, approval_timestamp_column, 0);
        }

        public Queue ArchiveMatured()
        {
            Queue result;
            MySqlDataReader dr;
            Queue id_q;
            string target_id_clause;
            id_q = new Queue();
            target_id_clause = k.EMPTY;
            this.Open();
            dr = new MySqlCommand("select emsof_request_master.id as master_id" + " from emsof_request_master" + " join emsof_request_detail on" + " (emsof_request_detail.master_id=emsof_request_master.id)" + " join eligible_provider_equipment_list on" + " (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)" + " where emsof_request_master.status_code = 14" + " group by emsof_request_master.id" + " having max((state_approval_timestamp + INTERVAL life_expectancy_years YEAR) - NOW()) < 0", this.connection).ExecuteReader();
            while (dr.Read())
            {
                target_id_clause = target_id_clause + dr["master_id"].ToString() + k.COMMA;
            }
            target_id_clause = target_id_clause.Trim(new char[] {Convert.ToChar(k.COMMA)});
            dr.Close();
            if (target_id_clause != k.EMPTY)
            {
                new MySqlCommand(db_trail.Saved("update emsof_request_master" + " set emsof_request_master.status_code = 15" + " where id in (" + target_id_clause + ")"), this.connection).ExecuteNonQuery();
            }
            this.Close();
            result = id_q;

            return result;
        }

        public bool BeDeadlineExempt(string master_id)
        {
            bool result;
            this.Open();
            result = "1" == new MySqlCommand("select be_deadline_exempt from emsof_request_master where id = " + master_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public bool BeRequestsEligibleForUnrejectionByRegionDictatedAppropriation(string region_dictated_appropriation_id)
        {
            bool result;
            this.Open();
            result = "0".CompareTo(new MySqlCommand("select count(*) from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " where status_code = 11" + " and region_dictated_appropriation_id = " + region_dictated_appropriation_id, this.connection).ExecuteScalar().ToString()) < 0;
            this.Close();
            return result;
        }

        public bool BeValidCountyApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            MySqlDataReader dr;

            timestamp = DateTime.MinValue;
            this.Open();
            dr = new MySqlCommand("select county_approval_timestamp from emsof_request_master where id = " + master_id, this.connection).ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                result = false;
            }
            else
            {
                result = true;
                timestamp = (DateTime)(dr["county_approval_timestamp"]);
            }
            dr.Close();
            this.Close();
            return result;
        }

        public bool BeValidRegionalExecDirApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            MySqlDataReader dr;

            timestamp = DateTime.MinValue;
            this.Open();
            dr = new MySqlCommand("select regional_director_approval_timestamp from emsof_request_master where id = " + master_id, this.connection).ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                result = false;
            }
            else
            {
                result = true;
                timestamp = (DateTime)(dr["regional_director_approval_timestamp"]);
            }
            dr.Close();
            this.Close();
            return result;
        }

        public bool BeValidRegionalPlannerApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            MySqlDataReader dr;

            timestamp = DateTime.MinValue;
            this.Open();
            dr = new MySqlCommand("select regional_planner_approval_timestamp from emsof_request_master where id = " + master_id, this.connection).ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                result = false;
            }
            else
            {
                result = true;
                timestamp = (DateTime)(dr["regional_planner_approval_timestamp"]);
            }
            dr.Close();
            this.Close();
            return result;
        }

        public bool BeValidStateApprovalTimestampOf(string master_id, out DateTime timestamp)
        {
            bool result;
            MySqlDataReader dr;

            timestamp = DateTime.MinValue;
            this.Open();
            dr = new MySqlCommand("select state_approval_timestamp from emsof_request_master where id = " + master_id, this.connection).ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                result = false;
            }
            else
            {
                result = true;
                timestamp = (DateTime)(dr["state_approval_timestamp"]);
            }
            dr.Close();
            this.Close();
            return result;
        }

        public void BindDetail(string master_id, object target)
        {
            Open();
            ((target) as DataGrid).DataSource = new MySqlCommand
              (
              "select priority"
              + " , make_model"
              + " , if(be_refurbished,\"(refurbished)\",\"\") as be_refurbished"
              + " , description as category"
              + " , place_kept"
              + " , quantity"
              + " , unit_cost"
              + " , (quantity*unit_cost) as subtotal"
              + " , allowable_cost"
              + " , emsof_ante"
              + " , invoice_designator"
              + " , actual_quantity"
              + " , actual_subtotal_cost"
              + " , actual_emsof_ante"
              + " , attachment_key"
              + " , allowable_cost as second_allowable_cost"
              + " from emsof_request_detail"
              +   " join eligible_provider_equipment_list"
              +     " on (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)"
              +   " join emsof_request_master on (emsof_request_master.id=emsof_request_detail.master_id)"
              + " where master_id = '" + master_id + "'"
              + " order by priority",
              connection
              )
              .ExecuteReader();
            ((target) as DataGrid).DataBind();
            Close();
        }

        public void BindEquipmentProcurementDetail(string fy_id, string equipment_code, string sort_order, bool be_order_ascending, object target)
        {
            this.Open();
            if (be_order_ascending)
            {
                sort_order = sort_order.Replace("%", " asc");
            }
            else
            {
                sort_order = sort_order.Replace("%", " desc");
            }
            ((target) as GridView).DataSource = new MySqlCommand("select name" + " , make_model" + " , place_kept" + " , IF(be_refurbished,\"Y\",\"N\") as be_refurbished" + " , actual_quantity" + " , actual_subtotal_cost" + " , actual_emsof_ante" + " from emsof_request_detail" + " join emsof_request_master on" + " (emsof_request_master.id=emsof_request_detail.master_id)" + " join county_dictated_appropriation on" + " (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join service on" + " (service.id=county_dictated_appropriation.service_id)" + " join eligible_provider_equipment_list on" + " (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)" + " join fiscal_year on" + " (fiscal_year.id=eligible_provider_equipment_list.fiscal_year_id)" + " where fiscal_year_id = \"" + fy_id + "\"" + " and eligible_provider_equipment_list.code = \"" + equipment_code + "\"" + " and actual_quantity > 0" + " group by name" + " order by " + sort_order, this.connection).ExecuteReader();
            ((target) as GridView).DataBind();
            this.Close();
        }

        public void BindEquipmentProcurementOverview(string fy_id, string sort_order, bool be_order_ascending, object target, bool do_log)
        {
            string metric_from_where_clause;
            metric_from_where_clause = k.EMPTY + " from emsof_request_detail" + " join eligible_provider_equipment_list on" + " (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)" + " join fiscal_year on" + " (fiscal_year.id=eligible_provider_equipment_list.fiscal_year_id)" + " where fiscal_year_id = \"" + fy_id + "\"";
            this.Open();
            if (do_log)
            {
                new MySqlCommand(db_trail.Saved("replace indicator_equipment_quantities (fiscal_year_id,description,quantity)" + " select fiscal_year_id" + " , description" + " , sum(actual_quantity) as quantity" + metric_from_where_clause + " group by eligible_provider_equipment_list.description"), this.connection).ExecuteNonQuery();
            }
            if (be_order_ascending)
            {
                sort_order = sort_order.Replace("%", " asc");
            }
            else
            {
                sort_order = sort_order.Replace("%", " desc");
            }
            ((target) as DataGrid).DataSource = new MySqlCommand("select code,description" + " , sum(actual_quantity) as quantity" + " , sum(actual_emsof_ante) as emsof_part" + " , sum(actual_subtotal_cost) as overall_cost" + " , (sum(actual_subtotal_cost)/sum(actual_quantity)) as avg_unit_cost" + metric_from_where_clause + " group by eligible_provider_equipment_list.code" + " order by " + sort_order, this.connection).ExecuteReader();
            ((target) as DataGrid).DataBind();
            this.Close();
        }

        public void BindEquipmentProcurementOverview(string fy_id, string sort_order, bool be_order_ascending, object target)
        {
            BindEquipmentProcurementOverview(fy_id, sort_order, be_order_ascending, target, true);
        }

        public void BindOverviewAll(string order_by_field_name, bool be_order_ascending, object target)
        {
            BindOverview(order_by_field_name, be_order_ascending, target, "fiscal_year.id = \"" + db_fiscal_years.IdOfCurrent() + "\"");
        }

        public void BindOverviewByRegionDictatedAppropriation(string region_dictated_appropriation_id, string order_by_field_name, bool be_order_ascending, object target)
        {
            BindOverview(order_by_field_name, be_order_ascending, target, "region_dictated_appropriation_id = " + region_dictated_appropriation_id);
        }

        public void BindOverviewByRegionDictatedAppropriationAndStatus(string region_dictated_appropriation_id, uint status, string order_by_field_name, bool be_order_ascending, object target)
        {
            BindOverview(order_by_field_name, be_order_ascending, target, "region_dictated_appropriation_id = " + region_dictated_appropriation_id, "status_code = " + status.ToString());
        }

        public void BindOverviewByStatus
          (
          uint status,
          string order_by_field_name,
          bool be_order_ascending,
          object target,
          k.int_sign_range scope
            // -1 for current cycle only
            //  0 for all cycles
            //  1 for past cycles only
          )
          {
          var and_parm = k.EMPTY;
          if (scope.val == -1)
            {
            and_parm = "fiscal_year.id = '" + db_fiscal_years.IdOfCurrent() + "'";
            }
          else if (scope.val == 1)
            {
            and_parm = "fiscal_year.id < '" + db_fiscal_years.IdOfCurrent() + "'";
            }
          BindOverview(order_by_field_name, be_order_ascending, target, "status_code = " + status.ToString(), and_parm);
          }
        public void BindOverviewByStatus(uint status, string order_by_field_name, bool be_order_ascending, object target)
          {
          BindOverviewByStatus(status, order_by_field_name, be_order_ascending, target, scope:new k.int_sign_range());
          }

        public void BindProofsOfPayment(string request_id, object target)
        {
            this.Open();
            // column 0
            // column 1
            // column 2
            // column 3
            // column 4
            ((target) as DataGrid).DataSource = new MySqlCommand("select id" + " , DATE_FORMAT(date_of,\"%Y-%m-%d\") as date_of" + " , description as method" + " , amount" + " , note" + " from emsof_purchase_payment" + " join payment_proof_method_code_description_map" + " on (payment_proof_method_code_description_map.code=emsof_purchase_payment.method_code)" + " where master_id = " + request_id + " order by date_of asc", this.connection).ExecuteReader();
            ((target) as DataGrid).DataBind();
            ((MySqlDataReader)(((DataGrid)(target)).DataSource)).Close();
            this.Close();
        }

        public void BindStateExportBatch(object target, uint status, string amendment_num_string, string region_code)
        {
            Open();
            ((target) as DataGrid).DataSource = new MySqlCommand
              (
              "select concat('W',master_id) as w_num"
              + " , if(be_reopened_after_going_to_state,'*','') as be_reopened_after_going_to_state"
              + " , service.name as service_name"
              + " , if((be_als_amb or be_als_squad or be_air_amb),'ALS',if(be_bls_amb,'BLS',if(be_rescue,'RESCUE','QRS')))" + " as life_support_level"
              + " , description as equipment_description"
              + " , make_model"
              + " , quantity"
              + " , unit_cost"
              + " , IF(emsof_request_master.status_code = 6,quantity*unit_cost,0) as total_cost"
              + " , IF(emsof_request_master.status_code = 6,emsof_ante,0) as emsof_ante"
              + " , IF(emsof_request_master.status_code = 6,quantity*unit_cost - emsof_ante,0) as provider_match"
              + " , if(emsof_request_master.status_code = 6,'X','') as recommendation"
              + " , if(emsof_request_master.status_code is null,'e/5',if(emsof_request_master.status_code = 16,'c/3','')) as discouragement_code"
              + " from emsof_request_master"
              +   " left join emsof_request_detail on (emsof_request_detail.master_id=emsof_request_master.id)"
              +   " join county_dictated_appropriation on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)"
              +   " join region_dictated_appropriation on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)"
              +   " join state_dictated_appropriation on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)"
              +   " left join eligible_provider_equipment_list on (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)"
              +   " right join service on (service.id=county_dictated_appropriation.service_id)"
              + " where (emsof_request_master.status_code is null)"
              +     " or"
              +       " ("
              +       " emsof_request_master.status_code = '" + status.ToString() + "'"
              +       " and quantity > 0"
              +       " and amendment_num = " + amendment_num_string
              +       " and region_code = '" + region_code + "'"
              +       " )"
              +     " or"
              +       " ("
              +       " emsof_request_master.status_code = 16"
              +       " and state_dictated_appropriation.fiscal_year_id = '" + db_fiscal_years.IdOfCurrent() + "'"
              +       " and region_code = '" + region_code + "'"
              +       " )"
              + " order by service_name",
              connection
              )
              .ExecuteReader();
            ((target) as DataGrid).DataBind();
            Close();
        }

        public void BindValuesToRegion(string sort_order, bool be_order_ascending, object target)
        {
            Open();
            if (be_order_ascending)
            {
                sort_order = sort_order.Replace("%", " asc");
            }
            else
            {
                sort_order = sort_order.Replace("%", " desc");
            }
            ((target) as GridView).DataSource = new MySqlCommand
              (
              "SELECT designator as fiscal_year"
              + " , (state_dictated_appropriation.amount - sum(actual_emsof_ante)) as emsof"
              + " FROM emsof_request_detail"
              +   " JOIN emsof_request_master on (emsof_request_master.id=emsof_request_detail.master_id)"
              +   " JOIN county_dictated_appropriation ON (county_dictated_appropriation.id=emsof_request_detail.master_id)"
              +   " JOIN region_dictated_appropriation ON (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)"
              +   " JOIN state_dictated_appropriation ON (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)"
              +   " JOIN fiscal_year ON (fiscal_year.id=state_dictated_appropriation.fiscal_year_id)"
              + " WHERE emsof_request_master.status_code in (10,13,14)"
              + " GROUP BY fiscal_year.designator"
              + " order by " + sort_order,
              connection
              )
              .ExecuteReader();
            ((target) as GridView).DataBind();
            Close();
        }

        public void BindValuesToServices(string sort_order, bool be_order_ascending, object target)
        {
            this.Open();
            if (be_order_ascending)
            {
                sort_order = sort_order.Replace("%", " asc");
            }
            else
            {
                sort_order = sort_order.Replace("%", " desc");
            }
            ((target) as GridView).DataSource = new MySqlCommand("SELECT" + " designator as fiscal_year" + " ,sum(actual_subtotal_cost) as costs" + " ,sum(actual_emsof_ante) as emsof" + " ,sum(actual_subtotal_cost - actual_emsof_ante)" + "   as service_contributions" + " FROM emsof_request_detail" + " JOIN emsof_request_master on" + " (emsof_request_master.id=emsof_request_detail.master_id)" + " JOIN county_dictated_appropriation ON" + " (county_dictated_appropriation.id=emsof_request_detail.master_id)" + " JOIN region_dictated_appropriation ON" + " (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " JOIN state_dictated_appropriation ON" + " (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " JOIN fiscal_year ON" + " (fiscal_year.id=state_dictated_appropriation.fiscal_year_id)" + " WHERE emsof_request_master.status_code in" + " (10,13,14)" + " GROUP BY fiscal_year.designator" + " order by " + sort_order, this.connection).ExecuteReader();
            ((target) as GridView).DataBind();
            this.Close();
        }

        public DateTime CountyApprovalTimestampOf(string master_id)
        {
            DateTime result;
            this.Open();
            result = (DateTime)(new MySqlCommand("select county_approval_timestamp from emsof_request_master where id = " + master_id, this.connection).ExecuteScalar());
            this.Close();
            return result;
        }

        public string CountyCodeOfMasterId(string master_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select county_code" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " where emsof_request_master.id = " + master_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public string CountyDictumIdOf(string master_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select county_dictated_appropriation_id" + " from emsof_request_master" + " where id = " + master_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public void DeleteProofOfPayment(string id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("delete from emsof_purchase_payment where id = " + id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void Demote(string master_id, uint next_status)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " set emsof_request_master.status_code = " + next_status.ToString() + " where emsof_request_master.id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public string DetailText(string master_id)
        {
            string result;
            MySqlDataReader dr;
            string detail_text;
            detail_text = k.EMPTY;
            this.Open();
            dr = new MySqlCommand("select priority" + " , description" + " , make_model" + " , place_kept" + " , if(be_refurbished,\"REFURB\",\"NEW\") as new_or_refurb" + " , quantity" + " , concat(\"$\",format(unit_cost,2)) as formatted_unit_cost" + " , concat(\"$\",format(emsof_ante,2)) as formatted_emsof_ante" + " from emsof_request_detail" + " join eligible_provider_equipment_list on (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)" + " where master_id = " + master_id + " order by priority", this.connection).ExecuteReader();
            while (dr.Read())
            {
                detail_text = detail_text + "Priority:          " + dr["priority"].ToString() + k.NEW_LINE + "Description:       " + dr["description"].ToString() + k.NEW_LINE + "Make/model:        " + dr["make_model"].ToString() + k.NEW_LINE + "Place to be kept:  " + dr["place_kept"].ToString() + k.NEW_LINE + "New/refurb:        " + dr["new_or_refurb"].ToString() + k.NEW_LINE + "Quantity:          " + dr["quantity"].ToString() + k.NEW_LINE + "Unit cost:         " + dr["formatted_unit_cost"].ToString() + k.NEW_LINE + "Max EMSOF amount:  " + dr["formatted_emsof_ante"].ToString() + k.NEW_LINE + k.NEW_LINE;
            }
            dr.Close();
            this.Close();
            result = detail_text;
            return result;
        }

        public Queue DeployCompleted()
        {
            Queue result;
            Queue id_q;
            id_q = new Queue();
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master set status_code = 14 where status_code = 13"), this.connection).ExecuteNonQuery();
            this.Close();
            result = id_q;
            return result;
        }

        public decimal EmsofAnteOf(string master_id)
        {
            decimal result;
            this.Open();
            result = (decimal)(new MySqlCommand("select value from emsof_request_master where id = " + master_id, this.connection).ExecuteScalar());
            this.Close();
            return result;
        }

        public decimal EmsofAnteOfItem(string master_id, string priority)
        {
            decimal result;
            this.Open();
            result = (decimal)(new MySqlCommand("select emsof_ante" + " from emsof_request_detail" + " where master_id = " + master_id + " and priority = " + priority, this.connection).ExecuteScalar());
            this.Close();
            return result;
        }

        public string EquipmentCodeOf(string master_id, string priority)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select equipment_code" + " from emsof_request_detail" + " where master_id = " + master_id + " and priority = " + priority, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public Queue FailUncompleted()
        {
            Queue result;
            MySqlDataReader dr;
            Queue id_q;
            MySqlTransaction transaction;
            id_q = new Queue();
            this.Open();
            transaction = this.connection.BeginTransaction();
            try {
                dr = new MySqlCommand("select id from emsof_request_master where status_code < 10", this.connection, transaction).ExecuteReader();
                while (dr.Read())
                {
                    id_q.Enqueue(dr["id"]);
                }
                dr.Close();
                new MySqlCommand(db_trail.Saved("update emsof_request_master set status_code = 16 where status_code < 10"), this.connection, transaction).ExecuteNonQuery();
                transaction.Commit();
            }
            catch {
                transaction.Rollback();
                throw new Exception();
            }
            this.Close();
            result = id_q;
            return result;
        }

        public Queue FailUnfinalized()
        {
            Queue result;
            MySqlDataReader dr;
            Queue id_q;
            MySqlTransaction transaction;
            id_q = new Queue();
            this.Open();
            transaction = this.connection.BeginTransaction();
            try {
                dr = new MySqlCommand("select id from emsof_request_master where status_code < 3", this.connection, transaction).ExecuteReader();
                while (dr.Read())
                {
                    id_q.Enqueue(dr["id"]);
                }
                dr.Close();
                new MySqlCommand(db_trail.Saved("update emsof_request_master set status_code = 16 where status_code < 3"), this.connection, transaction).ExecuteNonQuery();
                transaction.Commit();
            }
            catch {
                transaction.Rollback();
                throw new Exception();
            }
            this.Close();
            result = id_q;
            return result;
        }

        public void Finalize(string master_id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master set status_code = 3 where id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void ForceClosed(string master_id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master set be_deadline_exempt = FALSE where id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void ForceOpen
          (
          string master_id,
          bool be_reopened_after_going_to_state
          )
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("START TRANSACTION;" + " update emsof_request_master" + " set status_code = 2" + " , be_deadline_exempt = TRUE" + " , be_reopened_after_going_to_state = (" + be_reopened_after_going_to_state.ToString() + " or be_reopened_after_going_to_state) where id = " + master_id + ";" + " update emsof_request_detail" + " set status_code = 1" + " , invoice_designator = \"\"" + " , actual_quantity = 0" + " , actual_subtotal_cost = 0" + " , actual_emsof_ante = 0" + " where master_id = " + master_id + ";" + " delete from emsof_purchase_payment where master_id = " + master_id + ";" + " COMMIT"), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public string FyDesignatorOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_FISCAL_YEAR_DESIGNATOR].Text, k.safe_hint_type.ALPHANUM);
            return result;
        }

        public bool HasWishList(string master_id)
        {
            bool result;
            this.Open();
            result = "1" == new MySqlCommand("select has_wish_list from emsof_request_master where id = " + master_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public string IdOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_ID].Text, k.safe_hint_type.NUM);
            return result;
        }

        public string IdOfProofOfPayment(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_PROOF_OF_PAYMENT_ID].Text, k.safe_hint_type.NUM);
            return result;
        }

        public decimal LeftoverOrShortageOf(object e_item)
        {
            decimal result;
            string safe_string;
            safe_string = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_LEFTOVER_OR_SHORTAGE].Text, k.safe_hint_type.REAL_NUM_INCLUDING_NEGATIVE);
            if (safe_string == k.EMPTY)
            {
                result = 0;
            }
            else
            {
                result = decimal.Parse(safe_string);
            }
            return result;
        }

        public void MarkDone(string master_id, uint next_status, string user_kind)
        {
            string cmdText;
            cmdText = "update emsof_request_master set status_code = " + next_status.ToString() + " where id = " + master_id;
            if ((user_kind == "state-staffer"))
            {
                cmdText = "START TRANSACTION; " + cmdText + ";" + " update emsof_request_detail set status_code = 2 where master_id = " + master_id + " and status_code = 1;" + " COMMIT";
            }
            this.Open();
            new MySqlCommand(db_trail.Saved(cmdText), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void MarkFailed(string master_id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master set status_code = 16 where id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void MarkSubmittedToState(string region_code, string amendment_num_string, uint current_status, uint next_status)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " set status_code = " + next_status.ToString() + " where region_code = " + region_code + " and amendment_num = " + amendment_num_string + " and status_code = " + current_status.ToString()), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public uint NumCompetingEquipmentItems(string code, string service_id, string master_id, string priority)
        {
            uint result;
            object num_competing_equipment_items_obj;
            this.Open();
            num_competing_equipment_items_obj = new MySqlCommand("select sum(quantity)" + " from emsof_request_detail" + " join emsof_request_master on (emsof_request_master.id=emsof_request_detail.master_id)" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join request_status_code_description_map on (request_status_code_description_map.code=emsof_request_master.status_code)" + " join eligible_provider_equipment_list on (eligible_provider_equipment_list.code=emsof_request_detail.equipment_code)" + " where eligible_provider_equipment_list.description = (select description from eligible_provider_equipment_list where code = \"" + code + "\")" + " and request_status_code_description_map.description not in (\"Rejected\",\"Withdrawn by service\",\"Archived\",\"Failed deadline\")" + " and service_id = \"" + service_id + "\"" + " and not (master_id = \"" + master_id + "\" and priority = \"" + priority + "\")", this.connection).ExecuteScalar();
            this.Close();
            if (num_competing_equipment_items_obj == DBNull.Value)
            {
                result = 0;
            }
            else
            {
                result = uint.Parse(num_competing_equipment_items_obj.ToString());
            }
            return result;
        }

        public uint NumRequestsInStateExportBatch(uint status, string amendment_num_string, string region_code)
        {
            uint result;
            this.Open();
            result = (uint)(new MySqlCommand("select count(*)" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where status_code = " + status.ToString() + " and value > 0" + " and amendment_num = " + amendment_num_string + " and region_code = " + region_code, this.connection).ExecuteScalar().GetHashCode());
            this.Close();
            return result;
        }

        public string PasswordResetEmailAddressOf(object e_item)
          {
          return k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_PASSWORD_RESET_EMAIL_ADDRESS].Text, k.safe_hint_type.EMAIL_ADDRESS);
          }

        public string PropertyNameOfActualEmsofAnte()
          {
          return "actual_emsof_ante";
          }

        public string PropertyNameOfAppropriation()
        {
            string result;
            result = "appropriation";
            return result;
        }

        public string PropertyNameOfEmsofAnte()
        {
            string result;
            result = "emsof_ante";
            return result;
        }

        public DateTime ReworkDeadline(object e_item)
        {
            DateTime result;
            this.Open();
            result = (DateTime)(new MySqlCommand("select service_to_county_submission_deadline" + " from region_dictated_appropriation" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.region_dictated_appropriation_id=region_dictated_appropriation.id)" + " join emsof_request_master on (emsof_request_master.county_dictated_appropriation_id=county_dictated_appropriation.id)" + " where emsof_request_master.id = " + IdOf(e_item), this.connection).ExecuteScalar());
            this.Close();
            return result;
        }

        public void RollUpActualValue(string master_id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master" + " set actual_value = (select sum(actual_emsof_ante) from emsof_request_detail where master_id = " + master_id + ")" + " where id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public string ServiceIdOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_SERVICE_ID].Text, k.safe_hint_type.NUM);
            return result;
        }

        public string ServiceIdOfMasterId(string master_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select service_id" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " where emsof_request_master.id = " + master_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public string ServiceNameOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_SERVICE_NAME].Text, k.safe_hint_type.ORG_NAME);
            return result;
        }

        public void SetActuals(string master_id, string priority, string invoice_designator, string quantity, string subtotal_cost, decimal emsof_ante, uint item_status_code)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_detail" + " set invoice_designator = \"" + invoice_designator + "\"" + " , actual_quantity = " + quantity + " , actual_subtotal_cost = " + subtotal_cost + " , actual_emsof_ante = " + emsof_ante.ToString() + " , status_code = " + item_status_code.ToString() + " where master_id = " + master_id + " and priority = " + priority), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void SetHasWishList(string master_id, bool value)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master" + " set has_wish_list = " + value.ToString() + " where id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public string SponsorCountyCodeOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_SPONSOR_COUNTY_ID].Text, k.safe_hint_type.NUM);
            return result;
        }

        public string SponsorCountyEmailAddressOf(object e_item)
          {
          return k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_SPONSOR_COUNTY_EMAIL_ADDRESS].Text, k.safe_hint_type.EMAIL_ADDRESS);
          }

        public string SponsorCountyNameOf(object e_item)
        {
            string result;
            result = k.Safe(((e_item) as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_SPONSOR_COUNTY_NAME].Text, k.safe_hint_type.ALPHA);
            return result;
        }

        public string SponsorRegionNameOf(string master_id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("select name" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join region_code_name_map on (region_code_name_map.code=state_dictated_appropriation.region_code)" + " where emsof_request_master.id = " + master_id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public uint StatusCodeOf(object e_item)
        {
            Open();
            var status_code_of = uint.Parse
              (new MySqlCommand("select status_code from emsof_request_master where id = '" + k.Safe((e_item as DataGridItem).Cells[Class_db_emsof_requests_Static.TCCI_ID].Text, k.safe_hint_type.NUM) + "'",connection).ExecuteScalar().ToString());
            Close();
            return status_code_of;
        }

        public decimal SumOfActualCostsOfRequestItems(string request_id)
        {
            decimal result;
            object sum_of_actual_costs_of_request_items_obj;
            this.Open();
            sum_of_actual_costs_of_request_items_obj = new MySqlCommand("select sum(actual_subtotal_cost) from emsof_request_detail where master_id = " + request_id, this.connection).ExecuteScalar();
            this.Close();
            if (sum_of_actual_costs_of_request_items_obj != DBNull.Value)
            {
                result = (decimal)(sum_of_actual_costs_of_request_items_obj);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public decimal SumOfActualEmsofAntesOfOtherRequestItems
          (
          string request_id,
          string priority
          )
          {
          decimal result;
          object sum_of_actual_emsof_antes_of_request_items_obj;
          Open();
          sum_of_actual_emsof_antes_of_request_items_obj = new MySqlCommand("select sum(actual_emsof_ante) from emsof_request_detail where master_id = '" + request_id + "' and priority <> '" + priority + "'", connection).ExecuteScalar();
          Close();
          if (sum_of_actual_emsof_antes_of_request_items_obj != DBNull.Value)
          {
              result = (decimal)(sum_of_actual_emsof_antes_of_request_items_obj);
          }
          else
          {
              result = 0;
          }
          return result;
          }

        public decimal SumOfActualValues(string user_kind, string user_id, string fy_id)
        {
            decimal result;
            string cmdText = k.EMPTY;
            object sum_obj;
            if (user_kind == "regional_staffer")
            {
                cmdText = "select sum(actual_value)" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join regional_staffer on (regional_staffer.region_code=state_dictated_appropriation.region_code)" + " where regional_staffer.id = " + user_id + " and fiscal_year_id = " + fy_id + " and status_code not in (11,12,16)";
            }
            else if (user_kind == "county")
            {
                cmdText = "select sum(actual_value)" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where county_code = " + user_id + " and fiscal_year_id = " + fy_id + " and status_code not in (11,12,16)";
            }
            this.Open();
            sum_obj = new MySqlCommand(cmdText, this.connection).ExecuteScalar();
            this.Close();
            if (sum_obj != DBNull.Value)
            {
                result = (decimal)(sum_obj);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public decimal SumOfProvenPaymentsOfRequest(string request_id)
        {
            decimal result;
            object sum_of_proven_payments_of_request_obj;
            this.Open();
            sum_of_proven_payments_of_request_obj = new MySqlCommand("select sum(amount) from emsof_purchase_payment where master_id = " + request_id, this.connection).ExecuteScalar();
            this.Close();
            if (sum_of_proven_payments_of_request_obj != DBNull.Value)
            {
                result = (decimal)(sum_of_proven_payments_of_request_obj);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public decimal SumOfRequestValues(string user_kind, string user_id, string fy_id)
        {
            decimal result;
            string cmdText = k.EMPTY;
            object sum_obj;
            if (user_kind == "regional_staffer")
            {
                cmdText = "select sum(value)" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join regional_staffer on (regional_staffer.region_code=state_dictated_appropriation.region_code)" + " where regional_staffer.id = " + user_id + " and fiscal_year_id = " + fy_id + " and ((status_code between 2 and 10) or (status_code = 13))";
            }
            else if (user_kind == "county")
            {
                cmdText = "select sum(value)" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " where county_code = " + user_id + " and fiscal_year_id = " + fy_id + " and ((status_code between 2 and 10) or (status_code = 13))";
            }
            this.Open();
            sum_obj = new MySqlCommand(cmdText, this.connection).ExecuteScalar();
            this.Close();
            if (sum_obj != DBNull.Value)
            {
                result = (decimal)(sum_obj);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public Queue SusceptibleTo(milestone_type milestone)
        {
            Queue result;
            MySqlDataReader dr;
            Queue id_q;
            string where_clause = k.EMPTY;
            id_q = new Queue();
            switch(milestone)
            {
                case Class_biz_milestones.milestone_type.COUNTY_DICTATED_APPROPRIATION_DEADLINE_MILESTONE:
                    where_clause = "status_code < 3";
                    break;
                case Class_biz_milestones.milestone_type.SERVICE_PURCHASE_COMPLETION_DEADLINE_MILESTONE:
                    where_clause = "status_code = 8";
                    break;
                case Class_biz_milestones.milestone_type.SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE:
                    where_clause = "status_code = 8";
                    break;
                case Class_biz_milestones.milestone_type.SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE:
                    where_clause = "status_code in (8,9)";
                    break;
            }
            this.Open();
            dr = new MySqlCommand("select id from emsof_request_master where " + where_clause, this.connection).ExecuteReader();
            while (dr.Read())
            {
                id_q.Enqueue(dr["id"]);
            }
            this.Close();
            result = id_q;
            return result;
        }

        public uint TallyByStatus
          (
          uint status,
          bool be_for_prior_cycles
          )
          {
          uint result;
          var sql = "select count(*) as count" + " from emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " join region_dictated_appropriation" + " on (region_dictated_appropriation.id=county_dictated_appropriation.region_dictated_appropriation_id)" + " join state_dictated_appropriation" + " on (state_dictated_appropriation.id=region_dictated_appropriation.state_dictated_appropriation_id)" + " join fiscal_year on (fiscal_year.id=state_dictated_appropriation.fiscal_year_id)" + " where status_code = " + status.ToString();
          if (!be_for_prior_cycles)
            {
            if (!(new ArrayList(new uint[] {14, 15}).Contains(status)))
              {
              // Unless tallying DEPLOYED or ARCHIVED statuses, limit tally to requests belonging to the current cycle.
              sql += " and fiscal_year.id = (select max(id) from fiscal_year)";
              }
            }
          else
            {
            sql += " and fiscal_year.id < (select max(id) from fiscal_year)";
            }
          Open();
          result = (uint)(new MySqlCommand(sql,connection).ExecuteScalar().GetHashCode());
          Close();
          return result;
          }
        public uint TallyByStatus(uint status)
          {
          return TallyByStatus(status,be_for_prior_cycles:false);
          }

        public uint TcciOfAppropriation()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_APPROPRIATION;
            return result;
        }

        public uint TcciOfCountyEmailAddress()
          {
          return Class_db_emsof_requests_Static.TCCI_SPONSOR_COUNTY_EMAIL_ADDRESS;
          }

        public uint TcciOfId()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_ID;
            return result;
        }

        public uint TcciOfEmsofAnte()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_EMSOF_ANTE;
            return result;
        }

        public uint TcciOfFullRequestActuals()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_FULL_REQUEST_ACTUALS;
            return result;
        }

        public uint TcciOfFullRequestAllowableCost()
          {
          return Class_db_emsof_requests_Static.TCCI_FULL_REQUEST_ALLOWABLE_COST;
          }

        public uint TcciOfFullRequestDetail()
          {
          return Class_db_emsof_requests_Static.TCCI_FULL_REQUEST_DETAIL;
          }

        public uint TcciOfFullRequestPriority()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_FULL_REQUEST_PRIORITY;
            return result;
        }

        public uint TcciOfPasswordResetEmailAddress()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_PASSWORD_RESET_EMAIL_ADDRESS;
            return result;
        }

        public uint TcciOfServiceName()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_SERVICE_NAME;
            return result;
        }

        public uint TcciOfLeftoverOrShortage()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_LEFTOVER_OR_SHORTAGE;
            return result;
        }

        public uint TcciOfSrrReplacementRowIndicator()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_SRR_REPLACEMENT_ROW_INDICATOR;
            return result;
        }

        public uint TcciOfStatusCode()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_STATUS_CODE;
            return result;
        }

        public uint TcciOfStatusDescription()
        {
            uint result;
            result = Class_db_emsof_requests_Static.TCCI_STATUS_DESCRIPTION;
            return result;
        }

        public void Regress(string master_id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master" + " set status_code = status_code - 1" + " where id = \"" + master_id + "\""), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public void Unreject(string master_id)
        {
            this.Open();
            new MySqlCommand(db_trail.Saved("update emsof_request_master" + " join county_dictated_appropriation" + " on (county_dictated_appropriation.id=emsof_request_master.county_dictated_appropriation_id)" + " set emsof_request_master.status_code = 2" + " where emsof_request_master.id = " + master_id), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public Queue Withdraw(string master_id)
          {
          var attachment_key_q = new Queue();
          this.Open();
          var dr = new MySqlCommand("select attachment_key from emsof_request_detail where master_id = '" + master_id + "' and attachment_key is not null",connection).ExecuteReader();
          while (dr.Read())
            {
            attachment_key_q.Enqueue(dr["attachment_key"].ToString());
            }
          dr.Close();
          new MySqlCommand(db_trail.Saved("START TRANSACTION;" + "delete from emsof_request_detail where master_id = " + master_id + ";" + "update emsof_request_master" + " set status_code = 12" + " , value = 0" + " , num_items = 0" + " , shortage = 0" + " , has_wish_list = FALSE" + " , actual_value = 0" + " , be_deadline_exempt = FALSE" + " , be_reopened_after_going_to_state = FALSE" + " where id = " + master_id + ";" + "COMMIT;"), this.connection).ExecuteNonQuery();
          this.Close();
          return attachment_key_q;
          }

    } // end TClass_db_emsof_requests

}

