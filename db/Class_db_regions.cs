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

    private TClass_db_trail db_trail = null;

    public TClass_db_regions() : base()
      {
      db_trail = new TClass_db_trail();
      }

    internal bool BeConedlinkSubscriber(string code)
      {
      Open();
      var be_conedlink_subscriber = ("1" == new MySqlCommand("select be_conedlink_subscriber from region_code_name_map where code = '" + code + "'",connection).ExecuteScalar().ToString());
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
      var dr = new MySqlCommand
        (
        "SELECT lpad(code,4,'0') as code"
        + " , name"
        + " FROM region_code_name_map"
        + " WHERE concat(lpad(code,4,'0'),' -- ',name) like '%" + partial_spec + "%'"
        + " order by name",
        connection
        )
        .ExecuteReader();
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
      string unselected_literal,
      string selected_value,
      string application_filter
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal != k.EMPTY)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
        }
      Open();
      var dr = new MySqlCommand("SELECT code,name FROM region_code_name_map where name <> '(none specified)'" + (application_filter.Length > 0 ? " and be_" + application_filter + "_subscriber" : k.EMPTY) + " order by code", connection)
        .ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
        }
      dr.Close();
      Close();
      if (selected_value != k.EMPTY)
        {
        ((target) as ListControl).SelectedValue = selected_value;
        }
      }
    public void BindDirectToListControl(object target,string unselected_literal,string selected_value)
      {
      BindDirectToListControl(target, unselected_literal, selected_value, application_filter:"conedlink");
      }
    public void BindDirectToListControl(object target,string unselected_literal)
      {
      BindDirectToListControl(target, unselected_literal, k.EMPTY);
      }
    public void BindDirectToListControl(object target)
      {
      BindDirectToListControl(target, "-- region --");
      }

    public void BindEmsrsToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal != k.EMPTY)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
        }
      Open();
      var dr = new MySqlCommand("SELECT emsrs_code,name FROM region_code_name_map where name <> '(none specified)' order by name", connection).ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["emsrs_code"].ToString()));
        }
      dr.Close();
      Close();
      if (selected_value != k.EMPTY)
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
      object target
      )
      {
      Open();
      ((target) as BaseDataList).DataSource = new MySqlCommand
        (
        "select code"
        + " , name"
        + " , IF(be_pacrat_subscriber,'YES','no') as be_pacrat_subscriber"
        + " from region_code_name_map"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        )
        .ExecuteReader();
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
      ((target) as BaseDataList).DataSource = new MySqlCommand
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
        )
        .ExecuteReader();
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
      var code_of_emsrs_code = new MySqlCommand("select code from region_code_name_map where emsrs_code = '" + emsrs_code + "'",connection).ExecuteScalar().ToString();
      Close();
      return code_of_emsrs_code;
      }

    public bool Delete(string code)
      {
      var result = true;
      Open();
      try
        {
        new MySqlCommand(db_trail.Saved("delete from region_code_name_map where code = '" + code + "'"), connection).ExecuteNonQuery();
        }
      catch(System.Exception e)
        {
        if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
          {
          result = false;
          }
        else
          {
          throw e;
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
      var emsrs_code_of_id = new MySqlCommand("select emsrs_code from region_code_name_map where code = '" + code + "'",connection).ExecuteScalar().ToString();
      Close();
      return emsrs_code_of_id;
      }

    internal string EmsportalPasswordOf(string code)
      {
      Open();
      var emsportal_password_of = new MySqlCommand("select conedlink_emsportal_password from region_code_name_map where code = '" + code + "'",connection).ExecuteScalar().ToString();
      Close();
      return emsportal_password_of;
      }

    internal string EmsportalUsernameOf(string code)
      {
      Open();
      var emsportal_username_of = new MySqlCommand("select conedlink_emsportal_username from region_code_name_map where code = '" + code + "'",connection).ExecuteScalar().ToString();
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
      var dr = new MySqlCommand("select name from region_code_name_map where code = '" + code + "'", connection).ExecuteReader();
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
      var name_of_code = new MySqlCommand("select name from region_code_name_map where code = '" + code + "'",connection).ExecuteScalar().ToString();
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
      new MySqlCommand
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
        )
        .ExecuteNonQuery();
      Close();
      }

    internal void SetPacratSubscriber
      (
      string code,
      bool value
      )
      {
      Open();
      new MySqlCommand("update region_code_name_map set be_pacrat_subscriber = " + value.ToString() + " where code = '" + code + "'",connection).ExecuteNonQuery();
      Close();
      }

    public Queue<string> SubscriberQ()
      {
      var subscriber_q = new Queue<string>();
      Open();
      var dr = new MySqlCommand("select code from region_code_name_map where be_conedlink_subscriber",connection).ExecuteReader();
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
      var dr =
        (
        new MySqlCommand
          (
          "SELECT name"
          + " , emsrs_code"
          + " , emsrs_active_practitioners_name"
          + " , be_pacrat_subscriber"
          + " FROM region_code_name_map"
          + " where code = '" + code + "'",
          connection
          )
          .ExecuteReader()
        );
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
