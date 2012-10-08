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

    public void MimicTraditionalInsertOnDuplicateKeyUpdate
      (
      string target_table_name,
      string key_field_name,
      string key_field_value,
      string childless_field_assignments_clause,
      string additional_match_condition
      )
      //
      // USAGE
      //    var childless_field_assignments_clause = " name = NULLIF('" + name + "','')"
      //    + " , county_code = NULLIF('" + county_code + "','')"
      //    + " , business_phone_num = NULLIF('" + business_phone_num + "','')"
      //    + " , business_fax_num = NULLIF('" + business_fax_num + "','')"
      //    + " , website_address = NULLIF('" + website_address + "','')"
      //    ;
      //    db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate
      //      (
      //      target_table_name:"service",
      //      key_field_name:"affiliate_num",
      //      key_field_value:affiliate_num,
      //      childless_field_assignments_clause:childless_field_assignments_clause
      //      );
      //
      // DESCRIPTION
      //   This routine mimics the use of the INSERT . . . ON DUPLICATE KEY UPDATE construct as if the MySQL server's innodb_autoinc_lock_mode were set to Traditional (0).  In other words, it does its best to prevent gaps from
      //   forming in the assigned auto-increment values.
      //
      //   This routine opens and closes the connection itself, and saves its work in the journal.
      //
      //   The generated code can only be executed by a database user if that user has been granted the CREATE ROUTINE privilege on the database in whose context the execution occurs.
      //
      {
      const string DELIMITER = "~";
      var procedure_name = "MTIODKU_" + DateTime.Now.Ticks.ToString("D19");
      var code = "/* DELIMITER '" + DELIMITER + "' */"
      + " drop procedure if exists " + procedure_name
      + DELIMITER
      + " create procedure " + procedure_name + "() modifies sql data"
      +   " BEGIN"
      +   " start transaction;"
      +   " if (select 1 from " + target_table_name + " where " + key_field_name + " = '" + key_field_value + "'" + (additional_match_condition.Length > 0 ? additional_match_condition : k.EMPTY) + " LOCK IN SHARE MODE) is null"
      +   " then"
      +     " insert " + target_table_name + " set " + key_field_name + " = NULLIF('" + key_field_value + "',''), " + childless_field_assignments_clause + ";"
      +   " else"
      +     " update " + target_table_name + " set " + childless_field_assignments_clause + " where " + key_field_name + " = '" + key_field_value + "';"
      +   " end if;"
      +   " commit;"
      +   " END"
      + DELIMITER
      + " call " + procedure_name + "()"
      + DELIMITER
      + " drop procedure " + procedure_name + "";
      var my_sql_script = new MySqlScript();
      my_sql_script.Connection = connection;
      my_sql_script.Delimiter = DELIMITER;
      my_sql_script.Query = Saved(code);
      Open();
      my_sql_script.Execute();
      Close();
      }
    public void MimicTraditionalInsertOnDuplicateKeyUpdate(string target_table_name,string key_field_name,string key_field_value,string childless_field_assignments_clause)
      {
      MimicTraditionalInsertOnDuplicateKeyUpdate(target_table_name,key_field_name,key_field_value,childless_field_assignments_clause,additional_match_condition:k.EMPTY);
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
