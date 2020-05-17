using Class_db;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Class_db_notifications
{

    public class TClass_db_notifications: TClass_db
    {

    private static class Static
      {
      public const string COMMON_SELECT_EMAIL_ADDRESS_FROM_MEMBER_ROLE_NOTIFICATION_WHERE_TIER_ID_EQUALS = k.EMPTY
      + "select email_address"
      + " from member"
      +   " join role_member_map on (role_member_map.member_id=member.id)"
      +   " join role_notification_map on (role_notification_map.role_id=role_member_map.role_id)"
      +   " join role on (role.id=role_member_map.role_id)"
      +   " join notification on (notification.id=role_notification_map.notification_id)"
      + " where tier_id = ";
      }

        #pragma warning disable IDE0052 // Remove unread private members
        private readonly string tier_2_match_field = String.Empty;
        private readonly string tier_3_match_field = String.Empty;
        #pragma warning restore IDE0052 // Remove unread private members

        //Constructor  Create()
        public TClass_db_notifications() : base()
        {
            tier_2_match_field = ConfigurationManager.AppSettings["tier_2_match_field"];
            tier_3_match_field = ConfigurationManager.AppSettings["tier_3_match_field"];
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            MySqlDataReader dr;
            ((target) as ListControl).Items.Clear();
            if (unselected_literal.Length > 0)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            Open();
            using var my_sql_command = new MySqlCommand("select notification.id as notification_id" + " , name as notification_name" + " from notification" + " order by notification_name", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["notification_name"].ToString(), dr["notification_id"].ToString()));
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
            BindDirectToListControl(target, "-- Notification --");
        }

        public void BindDirectToListControl(object target, string unselected_literal)
        {
            BindDirectToListControl(target, unselected_literal, k.EMPTY);
        }

    public string TargetOf
      (
      string name,
      string member_id
      )
      {
      var common_notification_name_condition_clause = " and notification.name = '" + name + "'";
      var target_of = k.EMPTY;
      //
      MySqlDataReader dr;
      //
      Open();
      //
      // Tier 1 stakeholders
      //
      using var my_sql_command_1 = new MySqlCommand
        (
        Static.COMMON_SELECT_EMAIL_ADDRESS_FROM_MEMBER_ROLE_NOTIFICATION_WHERE_TIER_ID_EQUALS + "1"
        + common_notification_name_condition_clause,
        connection
        );
      dr = my_sql_command_1.ExecuteReader();
      if (dr != null)
        {
        while (dr.Read())
          {
          target_of += dr["email_address"].ToString() + k.COMMA;
          }
        }
      dr.Close();
      //
      // Tier 2 stakeholders
      //
      using var my_sql_command_2 = new MySqlCommand
        (
        Static.COMMON_SELECT_EMAIL_ADDRESS_FROM_MEMBER_ROLE_NOTIFICATION_WHERE_TIER_ID_EQUALS + "2 and region_code = (select regional_council_code from member where id = '" + member_id + "')"
        + common_notification_name_condition_clause,
        connection
        );
      dr = my_sql_command_2.ExecuteReader();
      if (dr != null)
        {
        while (dr.Read())
          {
          target_of += dr["email_address"].ToString() + k.COMMA;
          }
        }
      dr.Close();
      Close();
      return target_of.TrimEnd(new char[] {Convert.ToChar(k.COMMA)});
      }

    } // end TClass_db_notifications

}
