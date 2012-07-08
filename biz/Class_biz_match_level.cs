using Class_db_match_level;
using kix;
using System;

namespace Class_biz_match_level
{
    public class TClass_biz_match_level
    {
        private TClass_db_match_level db_match_level = null;
        //Constructor  Create()
        public TClass_biz_match_level() : base()
        {
            // TODO: Add any constructor code here
            db_match_level = new TClass_db_match_level();
        }
        public bool Bind(string partial_username, object target)
        {
            bool result;
            result = db_match_level.Bind(partial_username, target);
            return result;
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            db_match_level.BindDirectToListControl(target, unselected_literal, selected_value);
        }

        public void BindDirectToListControl(object target)
        {
            BindDirectToListControl(target, "-- match_level --");
        }

        public void BindDirectToListControl(object target, string unselected_literal)
        {
            BindDirectToListControl(target, unselected_literal, k.EMPTY);
        }

        public void Delete(string username)
        {
            db_match_level.Delete(username);
        }

        public nominal_type EnumOfId(uint id)
        {
            nominal_type result;
            result = ((nominal_type)(id - 1));
            return result;
        }

        public bool Get(string name, out decimal factor)
        {
            bool result;
            result = db_match_level.Get(name, out factor);

            return result;
        }

        public void Set(string name, decimal factor)
        {
            db_match_level.Set(name, factor);

        }

    } // end TClass_biz_match_level

    public enum nominal_type
    {
        STANDARD,
        RURAL,
        DISTRESSED,
    } // end nominal_type

}

namespace Class_biz_match_level.Units
{
    public class Class_biz_match_level
    {
        public const string ID = "$Id";
    } // end Class_biz_match_level

}

