using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Class_db_practitioner_levels
  {

  public class TClass_db_practitioner_levels: TClass_db
    {
        private readonly TClass_db_trail db_trail = null;

        public TClass_db_practitioner_levels() : base()
        {
            db_trail = new TClass_db_trail();
        }

        public bool Bind(string partial_spec, object target)
        {
            bool result;
            MySqlDataReader dr;
            Open();
            ((target) as ListControl).Items.Clear();
            using var my_sql_command = new MySqlCommand("SELECT lpad(id,4,\"0\") as id" + " , description" + " FROM practitioner_level" + " WHERE concat(lpad(id,4,\"0\"),\" -- \",description) like \"%" + partial_spec + "%\"" + " order by description", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["id"].ToString() + k.SPACE_HYPHENS_SPACE + dr["description"].ToString(), dr["id"].ToString()));
            }
            dr.Close();
            Close();
            result = ((target) as ListControl).Items.Count > 0;
            return result;
        }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal = "-- practitioner level --",
      string selected_value = k.EMPTY,
      bool be_short_description_desired = false
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal.Length > 0)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal,k.EMPTY));
        }
      var description_field = (be_short_description_desired ? "short_description" : "emsrs_practitioner_level_description");
      Open();
      using var my_sql_command = new MySqlCommand("SELECT id," + description_field + " FROM practitioner_level where emsrs_practitioner_level_description <> \"(none specified)\" order by id",connection);
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr[description_field].ToString(),dr["id"].ToString()));
        }
      dr.Close();
      Close();
      if (selected_value.Length > 0)
        {
        ((target) as ListControl).SelectedValue = selected_value;
        }
      }

        public bool Delete(string id)
        {
            bool result;
            result = true;
            Open();
            try {
                using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from practitioner_level where id = " + id), connection);
                my_sql_command.ExecuteNonQuery();
            }
            catch(System.Exception e) {
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

        public bool Get(string id, out string description)
        {
            bool result;
            MySqlDataReader dr;

            description = k.EMPTY;
            result = false;
            Open();
            using var my_sql_command = new MySqlCommand("select description from practitioner_level where id = \"" + id + "\"", connection);
            dr = my_sql_command.ExecuteReader();
            if (dr.Read())
            {
                description = dr["description"].ToString();
                result = true;
            }
            dr.Close();
            Close();
            return result;
        }

        public void Set(string id, string description)
        {
            string childless_field_assignments_clause;
            childless_field_assignments_clause = "description = \"" + description + "\"";
            Open();
            using var my_sql_command = new MySqlCommand(db_trail.Saved("insert practitioner_level" + " set id = NULLIF(\"" + id + "\",\"\")" + " , " + childless_field_assignments_clause + " on duplicate key update " + childless_field_assignments_clause), connection);
            my_sql_command.ExecuteNonQuery();
            Close();

        }

    } // end TClass_db_practitioner_levels

  }
