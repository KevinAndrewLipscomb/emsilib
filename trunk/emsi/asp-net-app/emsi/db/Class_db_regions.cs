using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Class_db_regions
  {

  public class TClass_db_regions: TClass_db
    {

    private class region_summary
      {
      public string code;
      public string name;
      public string emsrs_code;
      public string emsrs_active_practitioners_name;
      public bool be_pacrat_subscriber;
      }

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_regions() : base()
      {
      db_trail = new TClass_db_trail();
      }

    internal bool BeConedlinkSubscriber(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select be_conedlink_subscriber from region_code_name_map where code = '" + code + "'",connection);
      var be_conedlink_subscriber = ("1" == my_sql_command.ExecuteScalar().ToString());
      Close();
      return be_conedlink_subscriber;
      }

    internal bool BePacratSubscriberOf(object summary)
      {
      return (summary as region_summary).be_pacrat_subscriber;
      }

    public bool Bind
      (
      string partial_spec,
      object target
      )
      {
      Open();
      ((target) as ListControl).Items.Clear();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT lpad(code,4,'0') as code"
        + " , name"
        + " FROM region_code_name_map"
        + " WHERE concat(lpad(code,4,'0'),' -- ',name) like '%" + partial_spec + "%'"
        + " order by name",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["code"].ToString() + k.SPACE_HYPHENS_SPACE + dr["name"].ToString(), dr["code"].ToString()));
        }
      dr.Close();
      Close();
      return ((target) as ListControl).Items.Count > 0;
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal = "-- region --",
      string selected_value = k.EMPTY,
      string application_filter = "conedlink"
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal.Length > 0)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
        }
      Open();
      using var my_sql_command = new MySqlCommand("SELECT code,name FROM region_code_name_map where name <> '(none specified)'" + (application_filter.Length > 0 ? " and be_" + application_filter + "_subscriber" : k.EMPTY) + " order by code", connection)
        ;
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

    public void BindEmsrsToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal.Length > 0)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
        }
      Open();
      using var my_sql_command = new MySqlCommand("SELECT emsrs_code,name FROM region_code_name_map where name <> '(none specified)' order by name", connection);
      var dr = my_sql_command.ExecuteReader();
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
    public void BindEmsrsToListControl(object target,string unselected_literal)
      {
      BindEmsrsToListControl(target, unselected_literal, k.EMPTY);
      }
    public void BindEmsrsToListControl(object target)
      {
      BindEmsrsToListControl(target, "-- region --");
      }

    internal void BindPacratManagementBaseDataList
      (
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      bool do_include_all_regions
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select code"
        + " , region_code_name_map.name"
        + " , IF(be_pacrat_subscriber,'YES','no') as be_pacrat_subscriber"
        + " , GROUP_CONCAT(email_address) as email_target"
        + " , concat(phone_number,'@',sms_gateway.hostname) as sms_target"
        + " from region_code_name_map"
        +   " left join role_member_map on (role_member_map.region_code=region_code_name_map.code)"
        +   " left join role on (role.id=role_member_map.role_id and role.name = 'Region Strike Team Manager')"
        +   " left join practitioner on (practitioner.id=role_member_map.member_id)"
        +   " left join practitioner_strike_team_detail on (practitioner_strike_team_detail.practitioner_id=practitioner.id)"
        +   " left join sms_gateway on (sms_gateway.id=practitioner_strike_team_detail.phone_service_id)"
        + (do_include_all_regions ? k.EMPTY : " where be_pacrat_subscriber")
        + " group by region_code_name_map.code"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindStrikeTeamAffiliationBaseDataList
      (
      string member_id,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select region_code_name_map.code as region_code"
        + " , region_code_name_map.emsrs_code as region_emsrs_code"
        + " , region_code_name_map.name as region_name"
        + " from role_member_map"
        +   " join region_code_name_map on (region_code_name_map.code=role_member_map.region_code)"
        + " where member_id = '" + member_id + "'"
        +   " and role_id = (select id from role where name = 'Region Strike Team Manager')"
        +   " and be_pacrat_subscriber"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal string CodeOf(object summary)
      {
      return (summary as region_summary).code;
      }

    internal string CodeOfEmsrsCode(string emsrs_code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select code from region_code_name_map where emsrs_code = '" + emsrs_code + "'",connection);
      var code_of_emsrs_code = my_sql_command.ExecuteScalar().ToString();
      Close();
      return code_of_emsrs_code;
      }

    internal string ConedlinkEvalSummaryModeDescriptionOf(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select eval_summary_mode.description"
        + " from region_code_name_map join eval_summary_mode on (eval_summary_mode.id=region_code_name_map.conedlink_eval_summary_mode_id)"
        + " where region_code_name_map.code = '" + code + "'",
        connection
        );
      var conedlink_eval_summary_mode_description_of = my_sql_command.ExecuteScalar().ToString();
      Close();
      return conedlink_eval_summary_mode_description_of;
      }

    internal string ConedlinkEvalSummaryModeIdOf(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select eval_summary_mode.id"
        + " from region_code_name_map join eval_summary_mode on (eval_summary_mode.id=region_code_name_map.conedlink_eval_summary_mode_id)"
        + " where region_code_name_map.code = '" + code + "'",
        connection
        );
      var conedlink_eval_summary_mode_id_of = my_sql_command.ExecuteScalar().ToString();
      Close();
      return conedlink_eval_summary_mode_id_of;
      }

    public bool Delete(string code)
      {
      var result = true;
      Open();
      try
        {
        using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from region_code_name_map where code = '" + code + "'"), connection);
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

    internal string EmsrsCodeOf(object summary)
      {
      return (summary as region_summary).emsrs_code;
      }

    internal string EmsrsCodeOfCode(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select emsrs_code from region_code_name_map where code = '" + code + "'",connection);
      var emsrs_code_of_id = my_sql_command.ExecuteScalar().ToString();
      Close();
      return emsrs_code_of_id;
      }

    internal string EmsportalPasswordOf(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select conedlink_emsportal_password from region_code_name_map where code = '" + code + "'",connection);
      var emsportal_password_of = my_sql_command.ExecuteScalar().ToString();
      Close();
      return emsportal_password_of;
      }

    internal string EmsportalUsernameOf(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select conedlink_emsportal_username from region_code_name_map where code = '" + code + "'",connection);
      var emsportal_username_of = my_sql_command.ExecuteScalar().ToString();
      Close();
      return emsportal_username_of;
      }

    public bool Get
      (
      string code,
      out string name
      )
      {
      name = k.EMPTY;
      var result = false;
      Open();
      using var my_sql_command = new MySqlCommand("select name from region_code_name_map where code = '" + code + "'", connection);
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        name = dr["name"].ToString();
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    internal string EmsrsActivePractitionersNameOf(object summary)
      {
      return (summary as region_summary).emsrs_active_practitioners_name;
      }

    internal string NameOf(object summary)
      {
      return (summary as region_summary).name;
      }

    internal string NameOfCode(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select name from region_code_name_map where code = '" + code + "'",connection);
      var name_of_code = my_sql_command.ExecuteScalar().ToString();
      Close();
      return name_of_code;
      }

    public void Set
      (
      string code,
      string name
      )
      {
      var childless_field_assignments_clause = "name = '" + name + "'";
      Open();
      using var my_sql_command = new MySqlCommand
        (
        db_trail.Saved
          (
          "insert region_code_name_map"
          + " set code = NULLIF('" + code + "','')"
          + " , " + childless_field_assignments_clause
          + " on duplicate key update "
          + childless_field_assignments_clause
          ),
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void SetConedlinkEvalSummaryModeId
      (
      string code,
      string id
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand("update region_code_name_map set conedlink_eval_summary_mode_id = '" + id + "' where code = '" + code + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void SetPacratSubscriber
      (
      string code,
      bool value
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand("update region_code_name_map set be_pacrat_subscriber = " + value.ToString() + " where code = '" + code + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public Queue<string> PacratSubscriberEmsrsCodeQ()
      {
      var pacrat_subscriber_emsrs_code_q = new Queue<string>();
      Open();
      using var my_sql_command = new MySqlCommand("select emsrs_code from region_code_name_map where be_pacrat_subscriber",connection);
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        pacrat_subscriber_emsrs_code_q.Enqueue(dr["emsrs_code"].ToString());
        }
      Close();
      return pacrat_subscriber_emsrs_code_q;
      }

    public Queue<string> SubscriberQ()
      {
      var subscriber_q = new Queue<string>();
      Open();
      using var my_sql_command = new MySqlCommand("select code from region_code_name_map where be_conedlink_subscriber",connection);
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        subscriber_q.Enqueue(dr["code"].ToString());
        }
      dr.Close();
      Close();
      return subscriber_q;
      }

    internal object Summary(string code)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT name"
        + " , emsrs_code"
        + " , emsrs_active_practitioners_name"
        + " , be_pacrat_subscriber"
        + " FROM region_code_name_map"
        + " where code = '" + code + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new region_summary()
        {
        code = code,
        name = dr["name"].ToString(),
        emsrs_code = dr["emsrs_code"].ToString(),
        emsrs_active_practitioners_name = dr["emsrs_active_practitioners_name"].ToString(),
        be_pacrat_subscriber = (dr["be_pacrat_subscriber"].ToString() == "1")
        };
      Close();
      return the_summary;
      }

    } // end TClass_db_regions

  }
