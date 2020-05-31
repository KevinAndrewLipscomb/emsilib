using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI.WebControls;

namespace Class_db_match_level
{
    public class TClass_db_match_level: TClass_db
    {

        private readonly TClass_db_trail db_trail = null;

        //Constructor  Create()
        public TClass_db_match_level() : base()
        {
            // TODO: Add any constructor code here
            db_trail = new TClass_db_trail();
        }

        public bool Bind(string partial_name, object target)
        {
            bool result;
            MySqlDataReader dr;
            Open();
            ((target) as ListControl).Items.Clear();
            //@ Unsupported property or method(C): 'ExecuteReader'
            using var my_sql_command = new MySqlCommand("SELECT name FROM match_level WHERE name like \"" + partial_name + "%\" order by name", connection);
            dr = my_sql_command.ExecuteReader();
            //@ Unsupported property or method(C): 'Read'
            while (dr.Read())
            {
                //@ Unsupported property or method(C): 'tostring'
                //@ Unsupported property or method(C): 'tostring'
                ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["name"].ToString()));
            }
            //@ Unsupported property or method(C): 'Close'
            dr.Close();
            Close();
            result = ((target) as ListControl).Items.Count > 0;
            return result;
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            ((target) as ListControl).Items.Clear();
            if (unselected_literal.Length > 0)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            Open();
            using var my_sql_command = new MySqlCommand("SELECT id,concat(name,' (',FORMAT(factor*100,0),'%)') as description FROM match_level order by id", connection);
            var dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["description"].ToString(), dr["id"].ToString()));
            }
            dr.Close();
            Close();
            if (selected_value.Length > 0)
            {
                ((target) as ListControl).SelectedValue = selected_value;
            }

        }

        public void BindDirectToListControl(object target)
        {
            BindDirectToListControl(target, "-- match_level --");
        }

        public void BindDirectToListControl(object target, string unselected_literal)
        {
            BindDirectToListControl(target, unselected_literal, k.EMPTY);
        }

        public void Delete(string name)
        {
            Open();
            //@ Unsupported property or method(C): 'ExecuteNonQuery'
            using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from match_level where name = " + name), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

        public bool Get(string name, out Decimal factor)
        {
            bool result;
            MySqlDataReader dr;

            factor = 0;
            result = false;
            Open();
            //@ Unsupported property or method(C): 'ExecuteReader'
            using var my_sql_command = new MySqlCommand("select * from match_level where name = \"" + name + "\"", connection);
            dr = my_sql_command.ExecuteReader();
            //@ Unsupported property or method(C): 'Read'
            if (dr.Read())
            {
                //@ Unsupported property or method(C): 'tostring'
                name = dr["name"].ToString();
                //@ Unsupported property or method(C): 'tostring'
                factor = Decimal.Parse(dr["factor"].ToString());
                result = true;
            }
            //@ Unsupported property or method(C): 'Close'
            dr.Close();
            Close();
            return result;
        }

        public void Set(string name, Decimal factor)
        {
            Open();
            //@ Unsupported property or method(C): 'ExecuteNonQuery'
            using var my_sql_command = new MySqlCommand(db_trail.Saved("replace match_level" + " set name = \"" + name + "\"" + " , factor = " + factor.ToString()), connection);
            my_sql_command.ExecuteNonQuery();
            Close();
        }

    } // end TClass_db_match_level

}
