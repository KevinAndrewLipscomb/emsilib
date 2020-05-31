using Class_db_regional_staffers;

namespace Class_biz_regional_staffers
{
    public class TClass_biz_regional_staffers
    {
        private readonly TClass_db_regional_staffers db_regional_staffers = null;

        //Constructor  Create()
        public TClass_biz_regional_staffers() : base()
        {
            // TODO: Add any constructor code here
            db_regional_staffers = new TClass_db_regional_staffers();
        }
        public string RegionCodeOf(string id)
        {
            string result;
            result = db_regional_staffers.RegionCodeOf(id);
            return result;
        }

        public string RegionNameOf(string id)
        {
            string result;
            result = db_regional_staffers.RegionNameOf(id);
            return result;
        }

    } // end TClass_biz_regional_staffers

}
