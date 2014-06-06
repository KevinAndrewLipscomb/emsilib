using MySql.Data.MySqlClient;
using System.Collections;
using System.Configuration;
using System.Data;

namespace Class_db
  {

  public class TClass_db
    // The class should be abstract once all db operations in WebEMSOF are performed via descendants of this class.
    {

    public MySqlConnection connection = null;
      // The connection member should be protected rather than public once all db operations in WebEMSOF are performed via descendants of this class.

    public TClass_db() : base()
      {
      connection = new MySqlConnection(connectionString:ConfigurationManager.AppSettings["db_connection_string"]);
      }

    public void Close()
      // This routine should be protected rather than public once all db operations in WebEMSOF are performed via descendants of this class.
      {
      connection.Close();
      }

    protected void ExecuteOneOffProcedureScriptWithTolerance
      (
      string procedure_name,
      MySqlScript my_sql_script
      )
      {
      var done = false;
      while (!done)
        {
        try
          {
          my_sql_script.Execute();
          done = true;
          }
        catch (MySqlException the_exception)
          {
          if (!new ArrayList() {"PROCEDURE " + procedure_name + " already exists","PROCEDURE " + connection.Database + "." + procedure_name + " does not exist"}.Contains(the_exception.Message))
            {
            throw;
            }
          }
        }
      }

    public void Open()
      // This routine should be protected rather than public once all db operations in WebEMSOF are performed via descendants of this class.
      {
      if (connection.State != ConnectionState.Open)
        {
        connection.Open();
        }
      }

    } // end TClass_db

  }
