using Class_db;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace Class_db_user
  {

  public class TClass_db_user: TClass_db
    {

    public TClass_db_user() : base()
      {
      }

    internal void BindRolesToBaseDataList
      (
      string id,
      object target
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select role.name as role"
        + " , IF(service_id is not null,4,IF(region_code is not null,2,1)) as tier"
        + " , IF(service_id is not null,concat('service `',service.name,'`'),IF(region_code is not null,concat('region `',region_code_name_map.name,'`'),'')) as scope"
        + " from user"
        +   " join user_member_map on (user_member_map.user_id=user.id)"
        +   " join role_member_map using (member_id)"
        +   " left join region_code_name_map on (region_code_name_map.code=role_member_map.region_code)"
        +   " left join service on (service.id=role_member_map.service_id)"
        +   " join role on (role.id=role_member_map.role_id)"
        + " where user.id = '" + id + "'"
        + " order by role,tier,scope",
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindPrivilegesToBaseDataList
      (
      string id,
      object target
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select distinct privilege.name as privilege"
        + " , IF(service_id is not null,4,IF(region_code is not null,2,1)) as tier"
        + " , IF(service_id is not null,concat('service `',service.name,'`'),IF(region_code is not null,concat('region `',region_code_name_map.name,'`'),'')) as scope"
        + " from user"
        +   " join user_member_map on (user_member_map.user_id=user.id)"
        +   " join role_member_map using (member_id)"
        +   " left join region_code_name_map on (region_code_name_map.code=role_member_map.region_code)"
        +   " left join service on (service.id=role_member_map.service_id)"
        +   " join role_privilege_map using (role_id)"
        +   " join privilege on (privilege.id=role_privilege_map.privilege_id)"
        + " where user.id = '" + id + "'"
        + " order by privilege,tier,scope",
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindNotificationsToBaseDataList
      (
      string id,
      object target
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select distinct notification.name as notification"
        + " , IF(service_id is not null,4,IF(region_code is not null,2,1)) as tier"
        + " , IF(service_id is not null,concat('service `',service.name,'`'),IF(region_code is not null,concat('region `',region_code_name_map.name,'`'),'')) as scope"
        + " from user"
        +   " join user_member_map on (user_member_map.user_id=user.id)"
        +   " join role_member_map using (member_id)"
        +   " left join region_code_name_map on (region_code_name_map.code=role_member_map.region_code)"
        +   " left join service on (service.id=role_member_map.service_id)"
        +   " join role_notification_map using (role_id)"
        +   " join notification on (notification.id=role_notification_map.notification_id)"
        + " where user.id = '" + id + "'"
        + " order by notification,tier,scope",
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal DateTime LastLoginTime(string id)
      {
      Open();
      using var mysql_command = new MySqlCommand("select last_login from user where id = '" + id + "'",connection);
      var last_login_time_obj = mysql_command.ExecuteScalar();
      Close();
      return (last_login_time_obj == DBNull.Value ? DateTime.MaxValue : DateTime.Parse(last_login_time_obj.ToString()));
      }

    public string[] RolesOf(string id)
      {
      var roles_of_string_collection = new StringCollection();
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select distinct name from role join role_member_map on (role_member_map.role_id=role.id) join user_member_map on (user_member_map.member_id=role_member_map.member_id) where user_member_map.user_id = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        roles_of_string_collection.Add(dr["name"].ToString());
        }
      dr.Close();
      Close();
      var roles_of = new string[roles_of_string_collection.Count];
      roles_of_string_collection.CopyTo(roles_of,0);
      return roles_of;
      }

    public string[] RolesOf(string target_user_table, string id)
      {
      var roles_of_string_collection = new StringCollection();
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select " + target_user_table + "group.name as group_name"
        + " from " + target_user_table + "role"
        +   " join " + target_user_table + "group on (" + target_user_table + "group.id=" + target_user_table + "role.group_id)"
        +   " join " + target_user_table + "user on (" + target_user_table + "user.id=" + target_user_table + "role.user_id)"
        + " where " + target_user_table + "user.id = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        roles_of_string_collection.Add(dr["group_name"].ToString());
        }
      dr.Close();
      Close();
      var roles_of = new string[roles_of_string_collection.Count];
      roles_of_string_collection.CopyTo(roles_of,0);
      return roles_of;
      }

    } // end TClass_db_user

  }
