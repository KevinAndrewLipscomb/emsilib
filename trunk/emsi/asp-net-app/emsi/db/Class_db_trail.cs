using Class_db;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;

namespace Class_db_trail
  {
  public class TClass_db_trail: TClass_db
    {

    public TClass_db_trail() : base()
      {
      }

    public string Saved(string action)
      {
      //
      // Make a local journal entry for convenient review.
      //
      var imitator_designator = (HttpContext.Current.Session["imitator_designator"] == null ? k.EMPTY : HttpContext.Current.Session["imitator_designator"].ToString());
      var imitator_sql = (imitator_designator.Length == 0 ? k.EMPTY : " , imitator = '" + imitator_designator + "'");
      Open();
      new MySqlCommand
        (
        "insert into journal"
        + " set timestamp = null"
        + imitator_sql
        + " , actor = '" + HttpContext.Current.User.Identity.Name + "'"
        + " , action = \"" + Regex.Replace(action, Convert.ToString(k.QUOTE), k.DOUBLE_QUOTE) + "\"",
        connection
        )
        .ExecuteNonQuery();
      Close();
      //
      // Send a representation of the action offsite as a contingency.
      //
      k.SmtpMailSend
        (
        ConfigurationManager.AppSettings["sender_email_address"],
        ConfigurationManager.AppSettings["failsafe_recipient_email_address"],
        "DB action by " + (imitator_designator.Length == 0 ? k.EMPTY : imitator_designator + " IMITATING ") + HttpContext.Current.User.Identity.Name,
        "/*" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + "*/ " + action
        );
      return action;
      }

    } // end TClass_db_trail

  }
