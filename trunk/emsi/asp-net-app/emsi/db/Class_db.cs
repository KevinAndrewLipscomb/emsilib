using MySql.Data.MySqlClient;
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

    protected static void ExecuteOneOffProcedureScriptWithTolerance
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
          if (the_exception.Message != "PROCEDURE " + procedure_name + " already exists")
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
