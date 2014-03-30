using Class_db;
using kix;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;

namespace Class_db_user
  {

  public class TClass_db_user: TClass_db
    {

    public TClass_db_user() : base()
      {
      }

    public string[] RolesForDisplayOf(string id)
      {
      var role_spec = k.EMPTY;
      var roles_for_display_of_string_collection = new StringCollection();
      //
      var region_spec = k.EMPTY;
      var service_spec = k.EMPTY;
      //
      Open();
      var dr = new MySqlCommand
        (
        "select role.name as role_name"
        + " , IFNULL(concat(region_code_name_map.name,' [region_code=',region_code,']'),'') as region_spec"
        + " , IFNULL(concat(service.name,' [service_id=',service_id,']'),'') as service_spec"
        + " from role"
        +   " join role_member_map on (role_member_map.role_id=role.id)"
        +   " join user_member_map on (user_member_map.member_id=role_member_map.member_id)"
        +   " left join region_code_name_map on (region_code_name_map.code=role_member_map.region_code)"
        +   " left join service on (service.id=role_member_map.service_id)"
        + " where user_member_map.user_id = '" + id + "'",
        connection
        )
        .ExecuteReader();
      while (dr.Read())
        {
        role_spec = dr["role_name"].ToString();
        region_spec = dr["region_spec"].ToString();
        service_spec = dr["service_spec"].ToString();
        if (region_spec.Length + service_spec.Length != 0)
          {
          if (region_spec.Length > 0)
            {
            role_spec += " for " + region_spec;
            }
          else if (service_spec.Length > 0)
            {
            role_spec += " for " + service_spec;
            }
          }
        roles_for_display_of_string_collection.Add(role_spec);
        }
      dr.Close();
      Close();
      var roles_of = new string[roles_for_display_of_string_collection.Count];
      roles_for_display_of_string_collection.CopyTo(roles_of,0);
      return roles_of;
      }

    public string[] RolesOf(string id)
      {
      var roles_of_string_collection = new StringCollection();
      Open();
      var dr = new MySqlCommand
        (
        "select distinct name from role join role_member_map on (role_member_map.role_id=role.id) join user_member_map on (user_member_map.member_id=role_member_map.member_id) where user_member_map.user_id = '" + id + "'",
        connection
        )
        .ExecuteReader();
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
      var dr = new MySqlCommand
        (
        "select " + target_user_table + "group.name as group_name"
        + " from " + target_user_table + "role"
        +   " join " + target_user_table + "group on (" + target_user_table + "group.id=" + target_user_table + "role.group_id)"
        +   " join " + target_user_table + "user on (" + target_user_table + "user.id=" + target_user_table + "role.user_id)"
        + " where " + target_user_table + "user.id = '" + id + "'",
        connection
        )
        .ExecuteReader();
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
