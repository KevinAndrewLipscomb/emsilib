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

        private string tier_2_match_field = String.Empty;
        private string tier_3_match_field = String.Empty;

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
            if (unselected_literal != k.EMPTY)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            this.Open();
            dr = new MySqlCommand("select notification.id as notification_id" + " , name as notification_name" + " from notification" + " order by notification_name", this.connection).ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["notification_name"].ToString(), dr["notification_id"].ToString()));
            }
            dr.Close();
            this.Close();
            if (selected_value != k.EMPTY)
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
      dr = new MySqlCommand
        (
        Static.COMMON_SELECT_EMAIL_ADDRESS_FROM_MEMBER_ROLE_NOTIFICATION_WHERE_TIER_ID_EQUALS + "1"
        + common_notification_name_condition_clause,
        connection
        )
        .ExecuteReader();
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
      dr = new MySqlCommand
        (
        Static.COMMON_SELECT_EMAIL_ADDRESS_FROM_MEMBER_ROLE_NOTIFICATION_WHERE_TIER_ID_EQUALS + "2 and region_code = (select regional_council_code from member where id = '" + member_id + "')"
        + common_notification_name_condition_clause,
        connection
        )
        .ExecuteReader();
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
