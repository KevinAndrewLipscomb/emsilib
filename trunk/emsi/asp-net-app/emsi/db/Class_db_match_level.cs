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
        private TClass_db_trail db_trail = null;
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
            this.Open();
            ((target) as ListControl).Items.Clear();
            //@ Unsupported property or method(C): 'ExecuteReader'
            dr = new MySqlCommand("SELECT name FROM match_level WHERE name like \"" + partial_name + "%\" order by name", this.connection).ExecuteReader();
            //@ Unsupported property or method(C): 'Read'
            while (dr.Read())
            {
                //@ Unsupported property or method(C): 'tostring'
                //@ Unsupported property or method(C): 'tostring'
                ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["name"].ToString()));
            }
            //@ Unsupported property or method(C): 'Close'
            dr.Close();
            this.Close();
            result = ((target) as ListControl).Items.Count > 0;
            return result;
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            ((target) as ListControl).Items.Clear();
            if (unselected_literal != k.EMPTY)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            Open();
            var dr = new MySqlCommand("SELECT id,concat(name,' (',FORMAT(factor*100,0),'%)') as description FROM match_level order by id", connection).ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["description"].ToString(), dr["id"].ToString()));
            }
            dr.Close();
            Close();
            if (selected_value != k.EMPTY)
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
            this.Open();
            //@ Unsupported property or method(C): 'ExecuteNonQuery'
            new MySqlCommand(db_trail.Saved("delete from match_level where name = " + name), this.connection).ExecuteNonQuery();
            this.Close();
        }

        public bool Get(string name, out Decimal factor)
        {
            bool result;
            MySqlDataReader dr;

            factor = 0;
            result = false;
            this.Open();
            //@ Unsupported property or method(C): 'ExecuteReader'
            dr = new MySqlCommand("select * from match_level where name = \"" + name + "\"", this.connection).ExecuteReader();
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
            this.Close();
            return result;
        }

        public void Set(string name, Decimal factor)
        {
            this.Open();
            //@ Unsupported property or method(C): 'ExecuteNonQuery'
            new MySqlCommand(db_trail.Saved("replace match_level" + " set name = \"" + name + "\"" + " , factor = " + factor.ToString()), this.connection).ExecuteNonQuery();
            this.Close();
        }

    } // end TClass_db_match_level

}

namespace Class_db_match_level.Units
{
    public class Class_db_match_level
    {
    } // end Class_db_match_level

}

