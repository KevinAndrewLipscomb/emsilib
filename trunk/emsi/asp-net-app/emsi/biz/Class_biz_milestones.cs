using Class_biz_accounts;
using Class_biz_emsof_requests;
using Class_biz_services;
using Class_db_milestones;
using kix;
using System;
using System.Collections;

namespace Class_biz_milestones
{
    public struct reminder_control_record_type
    {
        public uint num_reminders;
        public uint[] relative_day_num_array;
    } // end reminder_control_record_type

    public enum milestone_type
    {
        COUNTY_DICTATED_APPROPRIATION_DEADLINE_MILESTONE = 1,
        SERVICE_PURCHASE_COMPLETION_DEADLINE_MILESTONE = 2,
        SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE = 3,
        SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE = 4,
        END_OF_CYCLE_MILESTONE = 5,
        SERVICE_ANNUAL_SURVEY_SUBMISSION_DEADLINE = 6
    } // end milestone_type

    public static class Class_biz_milestones_Static
    {
        // REMINDER_CONTROL_TABLE
        public const int MAX_NUM_REMINDERS = 6;
        public static reminder_control_record_type[] REMINDER_CONTROL_TABLE =
          {
            // COUNTY_DICTATED_APPROPRIATION_DEADLINE_MILESTONE
            new reminder_control_record_type {num_reminders = 6, relative_day_num_array = new uint[] {1, 3, 7, 14, 30, 90}} , 
            // SERVICE_PURCHASE_COMPLETION_DEADLINE_MILESTONE
            new reminder_control_record_type {num_reminders = 6, relative_day_num_array = new uint[] {1, 3, 7, 14, 30, 90}} , 
            // SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE
            new reminder_control_record_type {num_reminders = 6, relative_day_num_array = new uint[] {1, 3, 7, 14, 30, 90}} , 
            // SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE
            new reminder_control_record_type {num_reminders = 6, relative_day_num_array = new uint[] {1, 3, 7, 14, 30, 90}} , 
            // END_OF_CYCLE_MILESTONE
            new reminder_control_record_type {num_reminders = 0} ,
            // SERVICE_ANNUAL_SURVEY_SUBMISSION_DEADLINE
            new reminder_control_record_type {num_reminders = 6, relative_day_num_array = new uint[] {1, 3, 7, 14, 30, 90}}
          };
    } // end Class_biz_milestones

    public class TClass_biz_milestones
    {
        private TClass_db_milestones db_milestones = null;
        public bool BeProcessed(milestone_type milestone)
        {
            bool result;
            result = db_milestones.BeProcessed((uint)(milestone));
            return result;
        }

        //Constructor  Create()
        public TClass_biz_milestones() : base()
        {
            // TODO: Add any constructor code here
            db_milestones = new TClass_db_milestones();
        }
        public void Sweep()
        {
            bool be_handled;
            bool be_processed;
            TClass_biz_accounts biz_accounts;
            TClass_biz_emsof_requests biz_emsof_requests;
            TClass_biz_services biz_services;
            DateTime deadline;
            uint i;
            uint j;
            string master_id;
            Queue master_id_q;
            uint relative_day_num;
            DateTime today;
            biz_accounts = new TClass_biz_accounts();
            biz_emsof_requests = new TClass_biz_emsof_requests();
            biz_services = new TClass_biz_services();
            master_id_q = null;
            today = DateTime.Today;
            foreach (milestone_type milestone in Enum.GetValues(typeof(milestone_type)))
            {
                db_milestones.Check((uint)(milestone), out be_processed, out deadline);
                if (!be_processed)
                {
                    if ((today > deadline))
                    {
                        switch(milestone)
                        {
                            case milestone_type.COUNTY_DICTATED_APPROPRIATION_DEADLINE_MILESTONE:
                                master_id_q = biz_emsof_requests.FailUnfinalized();
                                break;
                            case milestone_type.SERVICE_PURCHASE_COMPLETION_DEADLINE_MILESTONE:
                                master_id_q = new Queue();
                                break;
                            case milestone_type.SERVICE_INVOICE_SUBMISSION_DEADLINE_MILESTONE:
                                master_id_q = new Queue();
                                break;
                            case milestone_type.SERVICE_CANCELED_CHECK_SUBMISSION_DEADLINE_MILESTONE:
                                master_id_q = new Queue();
                                break;
                            case milestone_type.END_OF_CYCLE_MILESTONE:
                                biz_emsof_requests.DeployCompleted();
                                master_id_q = biz_emsof_requests.FailUncompleted();
                                biz_emsof_requests.ArchiveMatured();
                                biz_services.MarkProfilesStale();
                                break;
                            case milestone_type.SERVICE_ANNUAL_SURVEY_SUBMISSION_DEADLINE:
                                master_id_q = new Queue();
                                break;
                        }
                        uint master_id_q_count = (uint)(master_id_q.Count);
                        for (i = 1; i <= master_id_q_count; i ++ )
                        {
                            master_id = master_id_q.Dequeue().ToString();
                            biz_accounts.MakeDeadlineFailureNotification(milestone, biz_emsof_requests.ServiceIdOfMasterId(master_id), biz_emsof_requests.CountyCodeOfMasterId(master_id));
                        }
                        db_milestones.MarkProcessed((uint)(milestone));
                    }
                    else
                    {
                        be_handled = false;
                        i = 0;
                        while (!be_handled && (i < Class_biz_milestones_Static.REMINDER_CONTROL_TABLE[(int)(milestone) - 1].num_reminders))
                        {
                            relative_day_num = Class_biz_milestones_Static.REMINDER_CONTROL_TABLE[(int)(milestone) - 1].relative_day_num_array[i];
                            if (today == deadline.AddDays( -relative_day_num).Date)
                            {
                              if (milestone == milestone_type.SERVICE_ANNUAL_SURVEY_SUBMISSION_DEADLINE)
                                {
                                var service_id = k.EMPTY;
                                var service_id_q = biz_services.SusceptibleTo(milestone);
                                uint service_id_q_count = (uint)(service_id_q.Count);
                                for (j = 1; j <= service_id_q_count; j ++ )
                                  {
                                    service_id = service_id_q.Dequeue().ToString();
                                    biz_accounts.Remind(milestone, relative_day_num, deadline, service_id);
                                    be_handled = true;
                                  }
                                }
                              else
                                {
                                master_id_q = biz_emsof_requests.SusceptibleTo(milestone);
                                uint master_id_q_count = (uint)(master_id_q.Count);
                                for (j = 1; j <= master_id_q_count; j ++ )
                                  {
                                    master_id = master_id_q.Dequeue().ToString();
                                    biz_accounts.Remind(milestone, relative_day_num, deadline, biz_emsof_requests.ServiceIdOfMasterId(master_id));
                                    be_handled = true;
                                  }
                                }
                            }
                            i = i + 1;
                        }
                    }
                }
            }
        }

    } // end TClass_biz_milestones

}

