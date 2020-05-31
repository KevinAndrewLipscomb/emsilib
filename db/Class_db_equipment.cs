using Class_db;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Class_db_equipment
  {
  public class TClass_db_equipment: TClass_db
    {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible")]
    public struct serial_indicator_rec_type
      {
      #pragma warning disable CA1051 // Do not declare visible instance fields
      public uint year;
      public uint value;
      #pragma warning restore CA1051 // Do not declare visible instance fields
      }

        //Constructor  Create()
        public TClass_db_equipment() : base()
        {
        }

        public decimal AllowableCostOf(string code)
        {
            decimal result;
            object allowable_cost_obj;
            Open();
            using var my_sql_command = new MySqlCommand("select allowable_cost" + " from eligible_provider_equipment_list" + " where code = " + code, connection);
            allowable_cost_obj = my_sql_command.ExecuteScalar();
            if (allowable_cost_obj != DBNull.Value)
            {
                result = (decimal)(allowable_cost_obj);
            }
            else
            {
                result = decimal.MaxValue;
            }
            Close();
            return result;
        }

        public void BindDescriptionsOnlyToListControl(object target)
        {
            Open();
            using var my_sql_command = new MySqlCommand("select description" + " from eligible_provider_equipment_list" + " group by description" + " order by description", connection);
            ((target) as ListControl).DataSource = my_sql_command.ExecuteReader();
            ((target) as ListControl).DataValueField = "description";
            ((target) as ListControl).DataTextField = "description";
            ((target) as ListControl).DataBind();
            Close();
        }

        public void BindListControl(string fy_id, object target)
        {
            Open();
            using var my_sql_command = new MySqlCommand("select code,description" + " from eligible_provider_equipment_list" + " where fiscal_year_id = \"" + fy_id + "\"" + " order by description", connection);
            ((target) as ListControl).DataSource = my_sql_command.ExecuteReader();
            ((target) as ListControl).DataValueField = "code";
            ((target) as ListControl).DataTextField = "description";
            ((target) as ListControl).DataBind();
            Close();
        }

        public decimal FundingLevelNonRuralOf(string code)
        {
            decimal result;
            object funding_level_obj;
            Open();
            using var my_sql_command = new MySqlCommand("select funding_level_nonrural" + " from eligible_provider_equipment_list" + " where code = " + code, connection);
            funding_level_obj = my_sql_command.ExecuteScalar();
            if (funding_level_obj != DBNull.Value)
            {
                result = (decimal)(funding_level_obj);
            }
            else
            {
                result = decimal.MaxValue - (decimal)(0.01);
            // Prevents null funding level from equaling null allowable cost, which is tested in biz_equipment.BeMatchExempt.
            }
            Close();
            return result;
        }

        public decimal FundingLevelRuralOf(string code)
        {
            decimal result;
            object funding_level_obj;
            Open();
            using var my_sql_command = new MySqlCommand("select funding_level_rural" + " from eligible_provider_equipment_list" + " where code = " + code, connection);
            funding_level_obj = my_sql_command.ExecuteScalar();
            if (funding_level_obj != DBNull.Value)
            {
                result = (decimal)(funding_level_obj);
            }
            else
            {
                result = 0;
            }
            Close();
            return result;
        }

        public Queue SerialIndicatorData(string indicator, string description)
        {
            Queue result;
            MySqlDataReader dr;
            serial_indicator_rec_type serial_indicator_rec;
            Queue serial_indicator_rec_q;
            serial_indicator_rec_q = new Queue();
            Open();
            using var my_sql_command = new MySqlCommand("select YEAR(fy_calendar.value) as year,quantity" + " from indicator_" + indicator + " join fy_calendar using (fiscal_year_id)" + " join milestone_code_name_map on (milestone_code_name_map.code=fy_calendar.milestone_code)" + " where description = \"" + description + "\"" + " and milestone_code_name_map.name = \"emsof-end-of-cycle\"", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                serial_indicator_rec.year = uint.Parse(dr["year"].ToString());
                serial_indicator_rec.value = uint.Parse(dr["quantity"].ToString());
                serial_indicator_rec_q.Enqueue(serial_indicator_rec);
            }
            dr.Close();
            Close();
            result = serial_indicator_rec_q;

            return result;
        }

        public Queue SpecialRuleNames(string code)
        {
            Queue result;
            MySqlDataReader dr;
            Queue special_rule_names_q;
            special_rule_names_q = new Queue();
            Open();
            using var my_sql_command = new MySqlCommand("select name" + " from special_rule" + " join epel_special_rule_map on (epel_special_rule_map.special_rule_id=special_rule.id)" + " join eligible_provider_equipment_list on (eligible_provider_equipment_list.code=epel_special_rule_map.epel_code)" + " where code = \"" + code + "\"", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                special_rule_names_q.Enqueue(dr["name"].ToString());
            }
            dr.Close();
            Close();
            result = special_rule_names_q;

            return result;
        }

    } // end TClass_db_equipment

}
