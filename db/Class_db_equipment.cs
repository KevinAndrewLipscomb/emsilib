using MySql.Data.MySqlClient;
using System;


using System.Collections;
using System.Web.UI.WebControls;
using Class_db;
namespace Class_db_equipment
{
    public struct serial_indicator_rec_type
    {
        public uint year;
        public uint value;
    } // end serial_indicator_rec_type

    public class TClass_db_equipment: TClass_db
    {
        //Constructor  Create()
        public TClass_db_equipment() : base()
        {
            // TODO: Add any constructor code here

        }
        public decimal AllowableCostOf(string code)
        {
            decimal result;
            object allowable_cost_obj;
            this.Open();
            allowable_cost_obj = new MySqlCommand("select allowable_cost" + " from eligible_provider_equipment_list" + " where code = " + code, this.connection).ExecuteScalar();
            if (allowable_cost_obj != DBNull.Value)
            {
                result = (decimal)(allowable_cost_obj);
            }
            else
            {
                result = decimal.MaxValue;
            }
            this.Close();
            return result;
        }

        public void BindDescriptionsOnlyToListControl(object target)
        {
            this.Open();
            ((target) as ListControl).DataSource = new MySqlCommand("select description" + " from eligible_provider_equipment_list" + " group by description" + " order by description", this.connection).ExecuteReader();
            ((target) as ListControl).DataValueField = "description";
            ((target) as ListControl).DataTextField = "description";
            ((target) as ListControl).DataBind();
            this.Close();
        }

        public void BindListControl(string fy_id, object target)
        {
            this.Open();
            ((target) as ListControl).DataSource = new MySqlCommand("select code,description" + " from eligible_provider_equipment_list" + " where fiscal_year_id = \"" + fy_id + "\"" + " order by description", this.connection).ExecuteReader();
            ((target) as ListControl).DataValueField = "code";
            ((target) as ListControl).DataTextField = "description";
            ((target) as ListControl).DataBind();
            this.Close();
        }

        public decimal FundingLevelNonRuralOf(string code)
        {
            decimal result;
            object funding_level_obj;
            this.Open();
            funding_level_obj = new MySqlCommand("select funding_level_nonrural" + " from eligible_provider_equipment_list" + " where code = " + code, this.connection).ExecuteScalar();
            if (funding_level_obj != DBNull.Value)
            {
                result = (decimal)(funding_level_obj);
            }
            else
            {
                result = decimal.MaxValue - (decimal)(0.01);
            // Prevents null funding level from equaling null allowable cost, which is tested in TClass_biz_equipment.BeMatchExempt.
            }
            this.Close();
            return result;
        }

        public decimal FundingLevelRuralOf(string code)
        {
            decimal result;
            object funding_level_obj;
            this.Open();
            funding_level_obj = new MySqlCommand("select funding_level_rural" + " from eligible_provider_equipment_list" + " where code = " + code, this.connection).ExecuteScalar();
            if (funding_level_obj != DBNull.Value)
            {
                result = (decimal)(funding_level_obj);
            }
            else
            {
                result = 0;
            }
            this.Close();
            return result;
        }

        public Queue SerialIndicatorData(string indicator, string description)
        {
            Queue result;
            MySqlDataReader dr;
            serial_indicator_rec_type serial_indicator_rec;
            Queue serial_indicator_rec_q;
            serial_indicator_rec_q = new Queue();
            this.Open();
            dr = new MySqlCommand("select YEAR(fy_calendar.value) as year,quantity" + " from indicator_" + indicator + " join fy_calendar using (fiscal_year_id)" + " join milestone_code_name_map on (milestone_code_name_map.code=fy_calendar.milestone_code)" + " where description = \"" + description + "\"" + " and milestone_code_name_map.name = \"emsof-end-of-cycle\"", this.connection).ExecuteReader();
            while (dr.Read())
            {
                serial_indicator_rec.year = uint.Parse(dr["year"].ToString());
                serial_indicator_rec.value = uint.Parse(dr["quantity"].ToString());
                serial_indicator_rec_q.Enqueue(serial_indicator_rec);
            }
            dr.Close();
            this.Close();
            result = serial_indicator_rec_q;

            return result;
        }

        public Queue SpecialRuleNames(string code)
        {
            Queue result;
            MySqlDataReader dr;
            Queue special_rule_names_q;
            special_rule_names_q = new Queue();
            this.Open();
            dr = new MySqlCommand("select name" + " from special_rule" + " join epel_special_rule_map on (epel_special_rule_map.special_rule_id=special_rule.id)" + " join eligible_provider_equipment_list on (eligible_provider_equipment_list.code=epel_special_rule_map.epel_code)" + " where code = \"" + code + "\"", this.connection).ExecuteReader();
            while (dr.Read())
            {
                special_rule_names_q.Enqueue(dr["name"].ToString());
            }
            dr.Close();
            this.Close();
            result = special_rule_names_q;

            return result;
        }

    } // end TClass_db_equipment

}
