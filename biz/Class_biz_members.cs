using kix;
using System;
using Class_db_members;
namespace Class_biz_members
{
    // Class_biz_notifications,
    public class TClass_biz_members
    {
        private TClass_db_members db_members = null;
        //Constructor  Create()
        public TClass_biz_members() : base()
        {
            // TODO: Add any constructor code here
            db_members = new TClass_db_members();
            // biz_notifications := TClass_biz_notifications.Create;

        }
        // (
        // attributes..
        // )
        // (
        // attributes..
        // )
        public bool Add()
        {
            bool result;
            result = false;
            // if not db_members.BeKnown(first_name,last_name,cad_num) then begin
            // db_members.Add
            // (
            // attributes..
            // );
            // biz_notifications.IssueForMemberAdded
            // (
            // attributes..
            // );
            // Add := TRUE;
            // end;

            return result;
        }

        public bool BeValidProfile(string id)
        {
            bool result;
            result = db_members.BeValidProfile(id);
            return result;
        }

        public void BindDirectToListControl(object target, string unselected_literal, string selected_value)
        {
            db_members.BindDirectToListControl(target, unselected_literal, selected_value);
        }

        public void BindDirectToListControl(object target)
        {
            BindDirectToListControl(target, "-- Member --");
        }

        public void BindDirectToListControl(object target, string unselected_literal)
        {
            BindDirectToListControl(target, unselected_literal, k.EMPTY);
        }

        public string EmailAddressOf(string member_id)
        {
            string result;
            result = db_members.EmailAddressOf(member_id);
            return result;
        }

        public string FirstNameOfMemberId(string member_id)
        {
            string result;
            result = db_members.FirstNameOfMemberId(member_id);
            return result;
        }

        public string IdOfUserId(string user_id)
        {
            string result;
            result = db_members.IdOfUserId(user_id);
            return result;
        }

        public string LastNameOfMemberId(string member_id)
        {
            string result;
            result = db_members.LastNameOfMemberId(member_id);
            return result;
        }

        public string UserIdOf(string member_id)
        {
            string result;
            result = db_members.UserIdOf(member_id);
            return result;
        }

    } // end TClass_biz_members

}
