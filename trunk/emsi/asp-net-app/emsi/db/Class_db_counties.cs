using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Class_db_counties
{
    public class TClass_db_counties: TClass_db
    {

    private class county_summary
      {
      public string code;
      public string default_match_level_id;
      public string email_address;
      public string name;
      }

        private readonly TClass_db_trail db_trail;

        //Constructor  Create()
        public TClass_db_counties() : base()
          {
          db_trail = new TClass_db_trail();
          }

        public void BindDirectToListControl
          (
          object target,
          string unselected_literal,
          string selected_value,
          string region_code
          )
          {
          ((target) as ListControl).Items.Clear();
          if (unselected_literal.Length > 0)
            {
            ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
          Open();
          using var my_sql_command = new MySqlCommand
            (
            "SELECT code,name"
            + " FROM county_code_name_map"
            +     (region_code.Length > 0 ? " join county_region_map on (county_region_map.county_code=county_code_name_map.code) where region_code = '" + region_code + "'" : k.EMPTY)
            + " order by name",
            connection
            );
          var dr = my_sql_command.ExecuteReader();
          while (dr.Read())
            {
            ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
            }
          dr.Close();
          Close();
          if (selected_value.Length > 0)
            {
            ((target) as ListControl).SelectedValue = selected_value;
            }
          }

        public void BindEmsrsToListControl(object target, string unselected_literal, string selected_value)
        {
            MySqlDataReader dr;
            ((target) as ListControl).Items.Clear();
            if (unselected_literal.Length > 0)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            Open();
            using var my_sql_command = new MySqlCommand("SELECT emsrs_code,name FROM county_code_name_map order by name", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["emsrs_code"].ToString()));
            }
            dr.Close();
            Close();
            if (selected_value.Length > 0)
            {
                ((target) as ListControl).SelectedValue = selected_value;
            }
        }
        public void BindEmsrsToListControl(object target, string unselected_literal)
        {
            BindEmsrsToListControl(target, unselected_literal, k.EMPTY);
        }
        public void BindEmsrsToListControl(object target)
        {
            BindEmsrsToListControl(target, "-- County --");
        }

        public void BindGrid(string sort_order, bool be_sort_order_ascending, object target)
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
            "select code"
            + " , county_code_name_map.name as name"
            + " , password_reset_email_address as email_address"
            + " , concat(match_level.name,' (',FORMAT(factor*100,0),'%)') as match_level_description"
            + " , ("
            +   " select region_dictated_appropriation.amount - sum(county_dictated_appropriation.amount)"
            +   " from fiscal_year"
            +     " join state_dictated_appropriation on"
            +       " (state_dictated_appropriation.fiscal_year_id=fiscal_year.id)"
            +     " join region_dictated_appropriation on"
            +       " (region_dictated_appropriation.state_dictated_appropriation_id=state_dictated_appropriation.id)"
            +     " join county_dictated_appropriation on"
            +       " (county_dictated_appropriation.region_dictated_appropriation_id=region_dictated_appropriation.id)"
            +   " where region_dictated_appropriation.county_code = county_code_name_map.code"
            +     " and fiscal_year.id = (select max(id) from fiscal_year)"
            +   " ) as unallocated"
            + " from county_code_name_map"
            +   " join county_user on (county_user.id=county_code_name_map.code)"
            +   " join match_level on (match_level.id=county_code_name_map.default_match_level_id)"
            + " order by " + sort_order,
            connection
            );
          ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
          ((target) as BaseDataList).DataBind();
          Close();
          }

    internal string CodeOfSummary(object summary)
      {
      return (summary as county_summary).code;
      }

    internal string DefaultMatchLevelIdOfCode(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select default_match_level_id from county_code_name_map where code = '" + code + "'",connection);
      var default_match_level_code = my_sql_command.ExecuteScalar().ToString();
      Close();
      return default_match_level_code;
      }

    internal string DefaultMatchLevelIdOfSummary(object summary)
      {
      return (summary as county_summary).default_match_level_id;
      }

    internal string EmailAddressOfSummary(object summary)
      {
      return (summary as county_summary).email_address;
      }

        public string NameOf(string code)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select name from county_code_name_map where code = " + code, connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

    internal string NameOfSummary(object summary)
      {
      return (summary as county_summary).name;
      }

    public string RegionCodeOf(string county_code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select region_code from county_region_map where county_code = '" + county_code + "'",connection);
      var region_code_of = my_sql_command.ExecuteScalar().ToString();
      Close();
      return region_code_of;
      }

    internal void Set
      (
      string code,
      string email_address,
      string default_match_level_id
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        db_trail.Saved("update county_code_name_map join county_user on (county_user.id=county_code_name_map.code) set password_reset_email_address = '" + email_address + "', default_match_level_id = '" + default_match_level_id + "' where code = '" + code + "'"),
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public object Summary(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select name"
        + " , password_reset_email_address"
        + " , default_match_level_id"
        + " from county_code_name_map"
        +   " join county_user on (county_user.id=county_code_name_map.code)"
        + " where code = '" + code + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new county_summary()
        {
        code = code,
        default_match_level_id = dr["default_match_level_id"].ToString(),
        email_address = dr["password_reset_email_address"].ToString(),
        name = dr["name"].ToString()
        };
      Close();
      return the_summary;
      }

    } // end TClass_db_counties

}
