using MySql.Data.MySqlClient;
using System;
using Class_db;
namespace Class_db_regional_staffers
{
    public class TClass_db_regional_staffers: TClass_db
    {
        //Constructor  Create()
        public TClass_db_regional_staffers() : base()
        {
            // TODO: Add any constructor code here

        }
        public string RegionCodeOf(string id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("SELECT region_code FROM regional_staffer WHERE id = " + id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

        public string RegionNameOf(string id)
        {
            string result;
            this.Open();
            result = new MySqlCommand("SELECT name" + " FROM regional_staffer join region_code_name_map on (region_code_name_map.code=regional_staffer.region_code)" + " WHERE id = " + id, this.connection).ExecuteScalar().ToString();
            this.Close();
            return result;
        }

    } // end TClass_db_regional_staffers

}
