// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db;
using Class_db_coned_offering_statuses;
using Class_db_trail;
using ConEdLink.component.ss;
using external_data_binding.emsams.ConEdClassInfo;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Class_db_coned_offerings
  {

  public class TClass_db_coned_offerings: TClass_db
    {

    private class coned_offering_summary
      {
      public string id;
      public string class_id;
      public string class_number;
      public string course_number;
      public string course_title;
      public string location;
      public string start_date;
      public string start_other;
      public string end_date;
      public string end_other;
      public string length;
      public string approved;
      public coned_offering_status_enumeration status;
      public string public_contact_email;
      public string sponsor_id;
      public string sponsor_number;
      public string sponsor_name;
      public string sponsor_email;
      public string sponsor_contact_email;
      public string sponsor_public_contact_email;
      public string fr_med_trauma_hours;
      public string fr_other_hours;
      public string emt_med_trauma_hours;
      public string emt_other_hours;
      public string emtp_med_trauma_hours;
      public string emtp_other_hours;
      public string phrn_med_trauma_hours;
      public string phrn_other_hours;
      public string eval_summary_instructional_staff;
      public string eval_summary_time_appropriately_used;
      public string eval_summary_classroom_training_site;
      public string eval_summary_equipment_av;
      public string eval_summary_misc_remarks;
      }

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_coned_offerings() : base()
      {
      db_trail = new TClass_db_trail();
      }

    internal string ApprovedOf(object summary)
      {
      return (summary as coned_offering_summary).approved;
      }

    public bool Bind(string partial_spec, object target)
      {
      var concat_clause = "concat(IFNULL(id,'-'),'|',IFNULL(course_id,'-'),'|',IFNULL(class_number,'-'),'|',IFNULL(created_by,'-'),'|',IFNULL(date_created,'-'),'|',IFNULL(last_edited_by,'-'),'|',IFNULL(date_last_edited,'-'),'|',IFNULL(sponsor_id,'-'),'|',IFNULL(sponsor_number,'-'),'|',IFNULL(document_status,'-'),'|',IFNULL(class_final_status,'-'),'|',IFNULL(course_number,'-'),'|',IFNULL(location,'-'),'|',IFNULL(start_date_time,'-'),'|',IFNULL(end_date_time,'-'),'|',IFNULL(instructors,'-'),'|',IFNULL(public_contact_name,'-'),'|',IFNULL(location_address_1,'-'),'|',IFNULL(location_address_2,'-'),'|',IFNULL(location_city,'-'),'|',IFNULL(location_state,'-'),'|',IFNULL(location_zip,'-'),'|',IFNULL(location_of_registration,'-'),'|',IFNULL(primary_text,'-'),'|',IFNULL(additional_texts,'-'),'|',IFNULL(created_by_first_name,'-'),'|',IFNULL(created_by_last_name,'-'),'|',IFNULL(courses_course_number,'-'),'|',IFNULL(course_title,'-'))";
      Open();
      ((target) as ListControl).Items.Clear();
      using var my_sql_command = new MySqlCommand
        (
        "select id"
        + " , CONVERT(" + concat_clause + " USING utf8) as spec"
        + " from coned_offering"
        + " where " + concat_clause + " like '%" + partial_spec.ToUpper() + "%'"
        + " order by spec",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      return ((target) as ListControl).Items.Count > 0;
      }

    internal void BindBaseDataListPractitionerConedDetail
      (
      string practitioner_id,
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string sponsor_id_filter,
      string range
      )
      {
      var range_condition = k.EMPTY;
      if (range == "LastThreeYears")
        {
        range_condition = " and (end_date_time >= SUBDATE(CURDATE(),INTERVAL 3 YEAR))";
        }
      else if (range == "SinceJan1")
        {
        range_condition = " and (YEAR(end_date_time) = YEAR(CURDATE()))";
        }
      else if (range != "All") // range is a particular year
        {
        range_condition = " and YEAR(end_date_time) = '" + range + "'";
        }
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select coned_offering.id as coned_offering_id"
        + " , class_number"
        + " , course_title"
        + " , location"
        + " , region_code_name_map.name as region"
        + " , concat(start_date_time,' ',IFNULL(start_time,'')) as start"
        + " , concat(end_date_time,' ',IFNULL(end_time,'')) as end"
        + " , concat(sponsor_name,' #',coned_offering.sponsor_number) as sponsor"
        + " , IF(short_description = 'EMR',fr_med_trauma_hours,IF(short_description in ('EMT','EMT-New'),emt_med_trauma_hours,IF(short_description = 'PHRN',phrn_med_trauma_hours,emtp_med_trauma_hours))) as med_trauma_hours"
        + " , IF(short_description = 'EMR',fr_other_hours,IF(short_description in ('EMT','EMT-New'),emt_other_hours,IF(short_description = 'PHRN',phrn_other_hours,emtp_other_hours))) as other_hours"
        + " , instructor_hours"
        + " , coned_offering_roster.id as roster_id"
        + " from coned_offering"
        +   " join coned_offering_roster on (coned_offering_roster.coned_offering_id=coned_offering.id)"
        +   " join coned_offering_status on (coned_offering_status.id=coned_offering.status_id)"
        +   " join region_code_name_map on (region_code_name_map.emsrs_code=coned_offering.region_council_num)"
        +   " join practitioner on (practitioner.id=coned_offering_roster.practitioner_id)"
        +   " join practitioner_level on (practitioner_level.id=practitioner.level_id)"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        + " where practitioner_id = '" + practitioner_id + "'"
        +     range_condition
        +   " and coned_offering_status.description = 'ARCHIVED'"
        +     (sponsor_id_filter.Length > 0 ? " and teaching_entity.id = '" + sponsor_id_filter + "'" : k.EMPTY)
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindClassCatalog
      (
      string coned_sponsor_user_id,
      string range,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      var range_condition = k.EMPTY;
      if (range == "InProcess")
        {
        range_condition = " and (coned_offering_status.description = 'NEEDS_CONED_SPONSOR_FINALIZATION') and (start_date_time <= CURDATE())";
        }
      else if (range == "Future")
        {
        range_condition = " and (coned_offering_status.description = 'NEEDS_CONED_SPONSOR_FINALIZATION') and (start_date_time > CURDATE())";
        }
      else if (range == "ClosedLastThreeYears")
        {
        range_condition = " and (coned_offering_status.description <> 'NEEDS_CONED_SPONSOR_FINALIZATION') and (end_date_time between SUBDATE(CURDATE(),INTERVAL 3 YEAR) and CURDATE())";
        }
      else if (range != "All") // range is a particular year
        {
        range_condition = " and YEAR(start_date_time) = '" + range + "'";
        }
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select coned_offering.id as id"
        + " , class_number"
        + " , course_title"
        + " , location"
        + " , CONCAT(start_date_time,' ',IFNULL(start_time,'')) as start"
        + " , CONCAT(end_date_time,' ',IFNULL(end_time,'')) as end"
        + " , count(coned_offering_roster.id) as num_attendees"
        + " , IF(coned_offering_status.description = 'NEEDS_CONED_SPONSOR_FINALIZATION','Yes','No') as be_in_process"
        + " from coned_offering"
        +   " join region_code_name_map on (region_code_name_map.emsrs_code=coned_offering.region_council_num)"
        +   " join county_code_name_map on (county_code_name_map.emsrs_code=coned_offering.class_county_code)"
        +   " join county_region_map on (county_region_map.county_code=county_code_name_map.code)"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        +   " join coned_offering_status on (coned_offering_status.id=coned_offering.status_id)"
        +   " left join coned_offering_class_final_status on (coned_offering_class_final_status.id=coned_offering.class_final_status_id)"
        +   " left join coned_offering_roster on (coned_offering_roster.coned_offering_id=coned_offering.id)"
        + " where region_code_name_map.be_conedlink_subscriber"
        +     range_condition
        +     (coned_sponsor_user_id.Length > 0 ? " and teaching_entity.id = '" + coned_sponsor_user_id + "'" : k.EMPTY)
        +   " and ((coned_offering_class_final_status.short_description is null) or (coned_offering_class_final_status.short_description <> 'CANCELED'))"
        +   " and coned_offering_status.description not like 'SPONSOR_SAYS_%'"
        + " group by coned_offering.id"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindOutstandingRosters
      (
      string region_code,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select coned_offering.id as id"
        + " , class_number"
        + " , IFNULL(teaching_entity.short_name,teaching_entity.name) as sponsor"
        + " , course_title"
        + " , location"
        + " , CONCAT(start_date_time,' ',IFNULL(start_time,'')) as start"
        + " , CONCAT(end_date_time,' ',IFNULL(end_time,'')) as end"
        + " from coned_offering"
        +   " join region_code_name_map on (region_code_name_map.emsrs_code=coned_offering.region_council_num)"
        +   " join county_code_name_map on (county_code_name_map.emsrs_code=coned_offering.class_county_code)"
        +   " join county_region_map on (county_region_map.county_code=county_code_name_map.code)"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        +   " join coned_offering_status on (coned_offering_status.id=coned_offering.status_id)"
        +   " left join coned_offering_class_final_status on (coned_offering_class_final_status.id=coned_offering.class_final_status_id)"
        + " where region_code_name_map.code = '" + region_code + "'"
        +   " and county_region_map.region_code = '" + region_code + "'"
        +   " and coned_offering_status.description = 'NEEDS_CONED_SPONSOR_FINALIZATION'"
        +   " and end_date_time between CURDATE() - INTERVAL 10 DAY and CURDATE() - INTERVAL 1 DAY"
        +   " and ((coned_offering_class_final_status.short_description is null) or (coned_offering_class_final_status.short_description <> 'CANCELED'))"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal void BindReadyRosters
      (
      string region_code,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select coned_offering.id as id"
        + " , class_number"
        + " , IFNULL(teaching_entity.short_name,teaching_entity.name) as sponsor"
        + " , course_title"
        + " , location"
        + " , CONCAT(start_date_time,' ',IFNULL(start_time,'')) as start"
        + " , CONCAT(end_date_time,' ',IFNULL(end_time,'')) as end"
        + " from coned_offering"
        +   " join region_code_name_map on (region_code_name_map.emsrs_code=coned_offering.region_council_num)"
        +   " join county_code_name_map on (county_code_name_map.emsrs_code=coned_offering.class_county_code)"
        +   " join county_region_map on (county_region_map.county_code=county_code_name_map.code)"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        +   " join coned_offering_status on (coned_offering_status.id=coned_offering.status_id)"
        + " where region_code_name_map.code = '" + region_code + "'"
        +   " and county_region_map.region_code = '" + region_code + "'"
        +   " and coned_offering_status.description = 'NEEDS_REGIONAL_PROCESSING'"
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    public void BindDirectToListControl(object target)
      {
      Open();
      ((target) as ListControl).Items.Clear();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT id"
        + " , CONVERT(concat(IFNULL(id,'-'),'|',IFNULL(course_id,'-'),'|',IFNULL(class_number,'-'),'|',IFNULL(created_by,'-'),'|',IFNULL(date_created,'-'),'|',IFNULL(last_edited_by,'-'),'|',IFNULL(date_last_edited,'-'),'|',IFNULL(sponsor_id,'-'),'|',IFNULL(sponsor_number,'-'),'|',IFNULL(document_status,'-'),'|',IFNULL(class_final_status,'-'),'|',IFNULL(course_number,'-'),'|',IFNULL(location,'-'),'|',IFNULL(start_date_time,'-'),'|',IFNULL(end_date_time,'-'),'|',IFNULL(instructors,'-'),'|',IFNULL(public_contact_name,'-'),'|',IFNULL(location_address_1,'-'),'|',IFNULL(location_address_2,'-'),'|',IFNULL(location_city,'-'),'|',IFNULL(location_state,'-'),'|',IFNULL(location_zip,'-'),'|',IFNULL(location_of_registration,'-'),'|',IFNULL(primary_text,'-'),'|',IFNULL(additional_texts,'-'),'|',IFNULL(created_by_first_name,'-'),'|',IFNULL(created_by_last_name,'-'),'|',IFNULL(courses_course_number,'-'),'|',IFNULL(course_title,'-')) USING utf8) as spec"
        + " FROM coned_offering"
        + " order by spec",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      }

    internal void BindDirectToListControlForCopy
      (
      string id,
      object target
      )
      {
      ((target) as ListControl).Items.Clear();
      ((target) as ListControl).Items.Add(new ListItem("-- Select --",k.EMPTY));
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT t.id"
        + " , CONVERT(concat(IFNULL(t.start_date_time,'-'),'|',IFNULL(concat(SUBSTRING(t.class_number,1,2),'-',SUBSTRING(t.class_number,3,6),'-',SUBSTRING(t.class_number,9,6)),'-'),'|',IFNULL(LEFT(t.course_title,30),'-'),'|',IFNULL(LEFT(t.location,12),'-')) USING utf8) as spec"
        + " FROM coned_offering s"
        +   " join coned_offering t on (t.sponsor_id=s.sponsor_id and t.id<>s.id)"
        +   " join coned_offering_roster on (coned_offering_roster.coned_offering_id=t.id)" //prevents coned_offerings with no attendees from showing up in target list
        + " where s.id = '" + id + "'"
        +   " and t.end_date_time > DATE_ADD(CURDATE(),INTERVAL -18 MONTH)"
        + " group by t.id"  //prevents coned_offerings with no attendees from showing up in target list
        + " order by t.start_date_time desc, t.class_number desc",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      }

    internal void BindDiscardedRosters
      (
      string region_code,
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string filter
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select coned_offering.id as id"
        + " , class_number"
        + " , IFNULL(teaching_entity.short_name,teaching_entity.name) as sponsor"
        + " , course_title"
        + " , location"
        + " , CONCAT(start_date_time,' ',IFNULL(start_time,'')) as start"
        + " , CONCAT(end_date_time,' ',IFNULL(end_time,'')) as end"
        + " , REPLACE(REPLACE(coned_offering_status.description,'SPONSOR_SAYS_',''),'_',' ') as disposition"
        + " from coned_offering"
        +   " join region_code_name_map on (region_code_name_map.emsrs_code=coned_offering.region_council_num)"
        +   " join county_code_name_map on (county_code_name_map.emsrs_code=coned_offering.class_county_code)"
        +   " join county_region_map on (county_region_map.county_code=county_code_name_map.code)"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        +   " join coned_offering_status on (coned_offering_status.id=coned_offering.status_id)"
        + " where region_code_name_map.code = '" + region_code + "'"
        +   " and county_region_map.region_code = '" + region_code + "'"
        +   " and coned_offering_status.description like 'SPONSOR_SAYS_%'"
        +   (filter.Length == 0 ? k.EMPTY : " and YEAR(CURDATE()) in (YEAR(datetime_discarded),YEAR(start_date_time),YEAR(end_date_time))")
        + " order by " + sort_order.Replace("%",(be_sort_order_ascending ? " asc" : " desc")),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    internal string ClassIdOf(object summary)
      {
      return (summary as coned_offering_summary).class_id;
      }

    internal string ClassNumberOf(object summary)
      {
      return (summary as coned_offering_summary).class_number;
      }

    internal string CourseNumberOf(object summary)
      {
      return (summary as coned_offering_summary).course_number;
      }

    internal string CourseTitleOf(object summary)
      {
      return (summary as coned_offering_summary).course_title;
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from coned_offering where id = '" + id + "'"), connection);
        my_sql_command.ExecuteNonQuery();
        }
      catch(System.Exception e)
        {
        if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
          {
          result = false;
          }
        else
          {
          throw;
          }
        }
      Close();
      return result;
      }

    internal string EmtMedTraumaHoursOf(object summary)
      {
      return (summary as coned_offering_summary).emt_med_trauma_hours;
      }

    internal string EmtOtherHoursOf(object summary)
      {
      return (summary as coned_offering_summary).emt_other_hours;
      }

    internal string EmtpMedTraumaHoursOf(object summary)
      {
      return (summary as coned_offering_summary).emtp_med_trauma_hours;
      }

    internal string EmtpOtherHoursOf(object summary)
      {
      return (summary as coned_offering_summary).emtp_other_hours;
      }

    internal string EndDateOf(object summary)
      {
      return (summary as coned_offering_summary).end_date;
      }

    internal string EndOtherOf(object summary)
      {
      return (summary as coned_offering_summary).end_other;
      }

    internal string EvalSummaryInstructionalStaffOf(object summary)
      {
      return (summary as coned_offering_summary).eval_summary_instructional_staff;
      }

    internal string EvalSummaryTimeAppropriatelyUsedOf(object summary)
      {
      return (summary as coned_offering_summary).eval_summary_time_appropriately_used;
      }

    internal string EvalSummaryClassroomTrainingSiteOf(object summary)
      {
      return (summary as coned_offering_summary).eval_summary_classroom_training_site;
      }

    internal string EvalSummaryEquipmentAvOf(object summary)
      {
      return (summary as coned_offering_summary).eval_summary_equipment_av;
      }

    internal string EvalSummaryMiscRemarksOf(object summary)
      {
      return (summary as coned_offering_summary).eval_summary_misc_remarks;
      }

    internal string FrMedTraumaHoursOf(object summary)
      {
      return (summary as coned_offering_summary).fr_med_trauma_hours;
      }

    internal string FrOtherHoursOf(object summary)
      {
      return (summary as coned_offering_summary).fr_other_hours;
      }

    public bool Get
      (
      string id,
      out string class_id,
      out string course_id,
      out string class_number,
      out string created_by,
      out DateTime date_created,
      out string last_edited_by,
      out DateTime date_last_edited,
      out string sponsor_id,
      out string sponsor_number,
      out string document_status,
      out string class_final_status_id,
      out string course_number,
      out string location,
      out string student_cost,
      out string tuition_includes,
      out bool closed,
      out string estimated_students,
      out DateTime start_date_time,
      out DateTime end_date_time,
      out string start_time,
      out string end_time,
      out string other_dates_and_times,
      out string instructors,
      out string instructor_qualifications,
      out string public_contact_name,
      out string public_contact_phone,
      out string public_contact_email,
      out string public_contact_website,
      out string public_contact_notes,
      out DateTime date_submitted_to_region,
      out DateTime date_received_by_region,
      out DateTime date_sponsor_notified,
      out DateTime date_registration_sent_to_state,
      out DateTime date_cards_sent_to_sponsor,
      out DateTime date_materials_to_be_returned,
      out bool approved,
      out string region_comments,
      out string region_council_num,
      out string class_county_code,
      out string total_class_hours,
      out string location_address_1,
      out string location_address_2,
      out string location_city,
      out string location_state,
      out string location_zip,
      out string location_zip_plus_4,
      out string location_phone,
      out string location_email,
      out string location_of_registration,
      out string primary_text,
      out string additional_texts,
      out DateTime final_registration_date,
      out bool offered_as_college_credit,
      out DateTime practical_exam_date,
      out DateTime written_exam_date,
      out string disapproval_reason_id,
      out DateTime date_final_paperwork_received,
      out bool signed_hardcopy,
      out string created_by_first_name,
      out string created_by_last_name,
      out string class_disapproval_reason_description,
      out string sponsor_name,
      out string courses_course_number,
      out string course_title,
      out string status_id,
      out string fr_med_trauma_hours,
      out string fr_other_hours,
      out string emt_med_trauma_hours,
      out string emt_other_hours,
      out string emtp_med_trauma_hours,
      out string emtp_other_hours,
      out string phrn_med_trauma_hours,
      out string phrn_other_hours,
      out string eval_summary_instructional_staff,
      out string eval_summary_time_appropriately_used,
      out string eval_summary_classroom_training_site,
      out string eval_summary_equipment_av,
      out string eval_summary_misc_remarks
      )
      {
      class_id = k.EMPTY;
      course_id = k.EMPTY;
      class_number = k.EMPTY;
      created_by = k.EMPTY;
      date_created = DateTime.MinValue;
      last_edited_by = k.EMPTY;
      date_last_edited = DateTime.MinValue;
      sponsor_id = k.EMPTY;
      sponsor_number = k.EMPTY;
      document_status = k.EMPTY;
      class_final_status_id = k.EMPTY;
      course_number = k.EMPTY;
      location = k.EMPTY;
      student_cost = k.EMPTY;
      tuition_includes = k.EMPTY;
      closed = false;
      estimated_students = k.EMPTY;
      start_date_time = DateTime.MinValue;
      end_date_time = DateTime.MinValue;
      start_time = k.EMPTY;
      end_time = k.EMPTY;
      other_dates_and_times = k.EMPTY;
      instructors = k.EMPTY;
      instructor_qualifications = k.EMPTY;
      public_contact_name = k.EMPTY;
      public_contact_phone = k.EMPTY;
      public_contact_email = k.EMPTY;
      public_contact_website = k.EMPTY;
      public_contact_notes = k.EMPTY;
      date_submitted_to_region = DateTime.MinValue;
      date_received_by_region = DateTime.MinValue;
      date_sponsor_notified = DateTime.MinValue;
      date_registration_sent_to_state = DateTime.MinValue;
      date_cards_sent_to_sponsor = DateTime.MinValue;
      date_materials_to_be_returned = DateTime.MinValue;
      approved = false;
      region_comments = k.EMPTY;
      region_council_num = k.EMPTY;
      class_county_code = k.EMPTY;
      total_class_hours = k.EMPTY;
      location_address_1 = k.EMPTY;
      location_address_2 = k.EMPTY;
      location_city = k.EMPTY;
      location_state = k.EMPTY;
      location_zip = k.EMPTY;
      location_zip_plus_4 = k.EMPTY;
      location_phone = k.EMPTY;
      location_email = k.EMPTY;
      location_of_registration = k.EMPTY;
      primary_text = k.EMPTY;
      additional_texts = k.EMPTY;
      final_registration_date = DateTime.MinValue;
      offered_as_college_credit = false;
      practical_exam_date = DateTime.MinValue;
      written_exam_date = DateTime.MinValue;
      disapproval_reason_id = k.EMPTY;
      date_final_paperwork_received = DateTime.MinValue;
      signed_hardcopy = false;
      created_by_first_name = k.EMPTY;
      created_by_last_name = k.EMPTY;
      class_disapproval_reason_description = k.EMPTY;
      sponsor_name = k.EMPTY;
      courses_course_number = k.EMPTY;
      course_title = k.EMPTY;
      status_id = k.EMPTY;
      fr_med_trauma_hours = k.EMPTY;
      fr_other_hours = k.EMPTY;
      emt_med_trauma_hours = k.EMPTY;
      emt_other_hours = k.EMPTY;
      emtp_med_trauma_hours = k.EMPTY;
      emtp_other_hours = k.EMPTY;
      phrn_med_trauma_hours = k.EMPTY;
      phrn_other_hours = k.EMPTY;
      eval_summary_instructional_staff = k.EMPTY;
      eval_summary_time_appropriately_used = k.EMPTY;
      eval_summary_classroom_training_site = k.EMPTY;
      eval_summary_equipment_av = k.EMPTY;
      eval_summary_misc_remarks = k.EMPTY;
      var result = false;
      //
      Open();
      using var my_sql_command = new MySqlCommand("select * from coned_offering where CAST(id AS CHAR) = '" + id + "'", connection);
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        id = dr["id"].ToString();
        class_id = dr["class_id"].ToString();
        course_id = dr["course_id"].ToString();
        class_number = dr["class_number"].ToString();
        created_by = dr["created_by"].ToString();
        if (dr["date_created"] != DBNull.Value) {date_created = DateTime.Parse(dr["date_created"].ToString());}
        last_edited_by = dr["last_edited_by"].ToString();
        if (dr["date_last_edited"] != DBNull.Value) {date_last_edited = DateTime.Parse(dr["date_last_edited"].ToString());}
        sponsor_id = dr["sponsor_id"].ToString();
        sponsor_number = dr["sponsor_number"].ToString();
        document_status = dr["document_status"].ToString();
        class_final_status_id = dr["class_final_status_id"].ToString();
        course_number = dr["course_number"].ToString();
        location = dr["location"].ToString();
        student_cost = dr["student_cost"].ToString();
        tuition_includes = dr["tuition_includes"].ToString();
        closed = (dr["closed"].ToString() == "1");
        estimated_students = dr["estimated_students"].ToString();
        if (dr["start_date_time"] != DBNull.Value) {start_date_time = DateTime.Parse(dr["start_date_time"].ToString());}
        if (dr["end_date_time"] != DBNull.Value) {end_date_time = DateTime.Parse(dr["end_date_time"].ToString());}
        start_time = dr["start_time"].ToString();
        end_time = dr["end_time"].ToString();
        other_dates_and_times = dr["other_dates_and_times"].ToString();
        instructors = dr["instructors"].ToString();
        instructor_qualifications = dr["instructor_qualifications"].ToString();
        public_contact_name = dr["public_contact_name"].ToString();
        public_contact_phone = dr["public_contact_phone"].ToString();
        public_contact_email = dr["public_contact_email"].ToString();
        public_contact_website = dr["public_contact_website"].ToString();
        public_contact_notes = dr["public_contact_notes"].ToString();
        if (dr["date_submitted_to_region"] != DBNull.Value) {date_submitted_to_region = DateTime.Parse(dr["date_submitted_to_region"].ToString());}
        if (dr["date_received_by_region"] != DBNull.Value) {date_received_by_region = DateTime.Parse(dr["date_received_by_region"].ToString());}
        if (dr["date_sponsor_notified"] != DBNull.Value) {date_sponsor_notified = DateTime.Parse(dr["date_sponsor_notified"].ToString());}
        if (dr["date_registration_sent_to_state"] != DBNull.Value) {date_registration_sent_to_state = DateTime.Parse(dr["date_registration_sent_to_state"].ToString());}
        if (dr["date_cards_sent_to_sponsor"] != DBNull.Value) {date_cards_sent_to_sponsor = DateTime.Parse(dr["date_cards_sent_to_sponsor"].ToString());}
        if (dr["date_materials_to_be_returned"] != DBNull.Value) {date_materials_to_be_returned = DateTime.Parse(dr["date_materials_to_be_returned"].ToString());}
        approved = (dr["approved"].ToString() == "1");
        region_comments = dr["region_comments"].ToString();
        region_council_num = dr["region_council_num"].ToString();
        class_county_code = dr["class_county_code"].ToString();
        total_class_hours = dr["total_class_hours"].ToString();
        location_address_1 = dr["location_address_1"].ToString();
        location_address_2 = dr["location_address_2"].ToString();
        location_city = dr["location_city"].ToString();
        location_state = dr["location_state"].ToString();
        location_zip = dr["location_zip"].ToString();
        location_zip_plus_4 = dr["location_zip_plus_4"].ToString();
        location_phone = dr["location_phone"].ToString();
        location_email = dr["location_email"].ToString();
        location_of_registration = dr["location_of_registration"].ToString();
        primary_text = dr["primary_text"].ToString();
        additional_texts = dr["additional_texts"].ToString();
        if (dr["final_registration_date"] != DBNull.Value) {final_registration_date = DateTime.Parse(dr["final_registration_date"].ToString());}
        offered_as_college_credit = (dr["offered_as_college_credit"].ToString() == "1");
        if (dr["practical_exam_date"] != DBNull.Value) {practical_exam_date = DateTime.Parse(dr["practical_exam_date"].ToString());}
        if (dr["written_exam_date"] != DBNull.Value) {written_exam_date = DateTime.Parse(dr["written_exam_date"].ToString());}
        disapproval_reason_id = dr["disapproval_reason_id"].ToString();
        if (dr["date_final_paperwork_received"] != DBNull.Value) {date_final_paperwork_received = DateTime.Parse(dr["date_final_paperwork_received"].ToString());}
        signed_hardcopy = (dr["signed_hardcopy"].ToString() == "1");
        created_by_first_name = dr["created_by_first_name"].ToString();
        created_by_last_name = dr["created_by_last_name"].ToString();
        sponsor_name = dr["sponsor_name"].ToString();
        courses_course_number = dr["courses_course_number"].ToString();
        course_title = dr["course_title"].ToString();
        status_id = dr["status_id"].ToString();
        fr_med_trauma_hours = dr["fr_med_trauma_hours"].ToString();
        fr_other_hours = dr["fr_other_hours"].ToString();
        emt_med_trauma_hours = dr["emt_med_trauma_hours"].ToString();
        emt_other_hours = dr["emt_other_hours"].ToString();
        emtp_med_trauma_hours = dr["emtp_med_trauma_hours"].ToString();
        emtp_other_hours = dr["emtp_other_hours"].ToString();
        phrn_med_trauma_hours = dr["phrn_med_trauma_hours"].ToString();
        phrn_other_hours = dr["phrn_other_hours"].ToString();
        eval_summary_instructional_staff = dr["eval_summary_instructional_staff"].ToString();
        eval_summary_time_appropriately_used = dr["eval_summary_time_appropriately_used"].ToString();
        eval_summary_classroom_training_site = dr["eval_summary_classroom_training_site"].ToString();
        eval_summary_equipment_av = dr["eval_summary_equipment_av"].ToString();
        eval_summary_misc_remarks = dr["eval_summary_misc_remarks"].ToString();
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    internal void GetForTrainingCertificates
      (
      string id,
      out string class_number,
      out string sponsor_number,
      out string sponsor_name,
      out string course_title,
      out string date_final,
      ref k.decimal_nonnegative fr_med_trauma_ceus,
      ref k.decimal_nonnegative fr_other_ceus,
      ref k.decimal_nonnegative emt_med_trauma_ceus,
      ref k.decimal_nonnegative emt_other_ceus,
      ref k.decimal_nonnegative emtp_med_trauma_ceus,
      ref k.decimal_nonnegative emtp_other_ceus,
      ref k.decimal_nonnegative phrn_med_trauma_ceus,
      ref k.decimal_nonnegative phrn_other_ceus
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select class_number"
        + " , sponsor_number"
        + " , sponsor_name"
        + " , course_title"
        + " , end_date_time"
        + " , fr_med_trauma_hours"
        + " , fr_other_hours"
        + " , emt_med_trauma_hours"
        + " , emt_other_hours"
        + " , emtp_med_trauma_hours"
        + " , emtp_other_hours"
        + " , phrn_med_trauma_hours"
        + " , phrn_other_hours"
        + " from coned_offering"
        + " where CAST(id AS CHAR) = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      class_number = dr["class_number"].ToString();
      sponsor_number = dr["sponsor_number"].ToString();
      sponsor_name = dr["sponsor_name"].ToString();
      course_title = dr["course_title"].ToString();
      date_final = dr["end_date_time"].ToString();
      fr_med_trauma_ceus.val = decimal.Parse(dr["fr_med_trauma_hours"].ToString());
      fr_other_ceus.val = decimal.Parse(dr["fr_other_hours"].ToString());
      emt_med_trauma_ceus.val = decimal.Parse(dr["emt_med_trauma_hours"].ToString());
      emt_other_ceus.val = decimal.Parse(dr["emt_other_hours"].ToString());
      emtp_med_trauma_ceus.val = decimal.Parse(dr["emtp_med_trauma_hours"].ToString());
      emtp_other_ceus.val = decimal.Parse(dr["emtp_other_hours"].ToString());
      phrn_med_trauma_ceus.val = decimal.Parse(dr["phrn_med_trauma_hours"].ToString());
      phrn_other_ceus.val = decimal.Parse(dr["phrn_other_hours"].ToString());
      dr.Close();
      Close();
      }

    internal string IdOf(object summary)
      {
      return (summary as coned_offering_summary).id;
      }

    internal void ImportLatestFromEmsrsAvailableConedClassesList(ArrayList recs)
      {
      if (recs.Count > 0)
        {
        MySqlTransaction transaction;
        var class_city_state_index_of_comma = -1;
        var class_number = k.EMPTY;
        var childless_field_assignments_clause = k.EMPTY;
        Open();
        foreach (var rec in recs)
          {
          class_number = k.EMPTY
          + (rec as Class_ss_emsams.ConedOffering).class_region_code.PadLeft(2,'0')
          + (rec as Class_ss_emsams.ConedOffering).course_number.PadLeft(6,'0')
          + (rec as Class_ss_emsams.ConedOffering).class_number.PadLeft(6,'0');
          class_city_state_index_of_comma = (rec as Class_ss_emsams.ConedOffering).class_city_state.IndexOf(k.COMMA);
          childless_field_assignments_clause = " course_number = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).course_number + "','')"
          + " , course_title = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).course_title + "','')"
          + " , start_date_time = STR_TO_DATE(NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_start_date + "',''),'%m/%d/%Y')"
          + " , start_time = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_start_time + "','')"
          + " , end_date_time = STR_TO_DATE(NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_end_date + "',''),'%m/%d/%Y')"
          + " , end_time = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_end_time + "','')"
          + " , total_class_hours = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','')"
          + " , fr_med_trauma_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).fr_ce_trauma + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).fr_ce_trauma + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          + " , fr_other_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).fr_ce_other + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).fr_ce_other + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          + " , emt_med_trauma_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).emt_ce_trauma + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).emt_ce_trauma + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          + " , emt_other_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).emt_ce_other + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).emt_ce_other + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          //
          // Commented out because values reported by EMSRS for these columns are unreliable.  They appear to be double the correct values.
          //
          //+ " , emtp_other_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).als_ce_other + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).als_ce_other + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          //+ " , phrn_other_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).als_ce_other + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).als_ce_other + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          //+ " , emtp_med_trauma_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).als_ce_trauma + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).als_ce_trauma + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          //+ " , phrn_med_trauma_hours = NULLIF(IF(0+'" + (rec as Class_ss_emsams.ConedOffering).als_ce_trauma + "' <= 0+'" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "','" + (rec as Class_ss_emsams.ConedOffering).als_ce_trauma + "','" + (rec as Class_ss_emsams.ConedOffering).total_ceus + "'),'')"
          //
          + " , student_cost = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).tuition + "','')"
          + " , sponsor_id = (select emsrs_id from teaching_entity where REPLACE(sponsor_number,'-','') = '" + (rec as Class_ss_emsams.ConedOffering).sponsor_number + "')"
          + " , sponsor_name = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).sponsor_name + "','')"
          + " , sponsor_number = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).sponsor_number + "','')"
          + " , instructors = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).instructor_name + "','')"
          + " , public_contact_name = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).contact_name + "','')"
          + " , location = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).location + "','')"
          + " , location_address_1 = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_location + "','')"
          + " , location_city = NULLIF('" + (class_city_state_index_of_comma > -1 ? (rec as Class_ss_emsams.ConedOffering).class_city_state.Substring(0,class_city_state_index_of_comma) : k.EMPTY) + "','')"
          + " , location_state = NULLIF('" + (class_city_state_index_of_comma > -1 ? k.Safe((rec as Class_ss_emsams.ConedOffering).class_city_state.Substring(class_city_state_index_of_comma + 1),k.safe_hint_type.ALPHA) : k.EMPTY) + "','')"
          + " , location_zip = NULLIF('" + k.Safe((rec as Class_ss_emsams.ConedOffering).class_city_state,k.safe_hint_type.NUM) + "','')"
          + " , class_county_code = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_county_code + "','')"
          + " , region_council_num = NULLIF('" + (rec as Class_ss_emsams.ConedOffering).class_region_code + "','')"
          + " , approved = 1"
          + k.EMPTY;
          transaction = connection.BeginTransaction();
          try
            {
            using var my_sql_command_1 = new MySqlCommand("select 1 from coned_offering where class_number = '" + class_number + "'",connection,transaction);
            if (my_sql_command_1.ExecuteScalar() == null)
              {
              using var my_sql_command_2 = new MySqlCommand("insert coned_offering set class_number = NULLIF('" + class_number + "',''), " + childless_field_assignments_clause,connection,transaction);
              my_sql_command_2.ExecuteNonQuery();
              }
            else
              {
              using var my_sql_command_3 = new MySqlCommand("update coned_offering set " + childless_field_assignments_clause + " where class_number = '" + class_number + "'",connection,transaction);
              my_sql_command_3.ExecuteNonQuery();
              }
            transaction.Commit();
            }
          catch
            {
            transaction.Rollback();
            throw;
            }
          }
        Close();
        }
      }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types")]
    internal void ImportLatestFromEmsrs(ArrayList recs)
      {
      if (recs.Count > 0)
        {
        var application_name = ConfigurationManager.AppSettings["application_name"];
        var be_production_instance = !(application_name.ToLower().EndsWith("_d") || application_name.ToLower().EndsWith("_x"));
        //
        MySqlTransaction transaction;
        var course_title = k.EMPTY;
        var class_number = k.EMPTY;
        var public_contact_email = k.EMPTY;
        var location_email = k.EMPTY;
        //var length = k.EMPTY;
        var childless_field_assignments_clause = k.EMPTY;
        Open();
        foreach (var rec in recs)
          {
          if (((rec as ClassInfoClass).CourseNumber > 0) && ((rec as ClassInfoClass).ClassNumber > 0) && ((rec as ClassInfoClass).SponsorID > 0))
            {
            if (((rec as ClassInfoClass).CourseTitle == null) || ((rec as ClassInfoClass).CourseTitle.Length == 0))
              {
              course_title = "Course " + k.Safe((rec as ClassInfoClass).CourseNumber.ToString(),k.safe_hint_type.NUM).PadLeft(6,'0') + " [Title unavailable possibly due to punctuation]";
              }
            else
              {
              course_title = k.Safe((rec as ClassInfoClass).CourseTitle,k.safe_hint_type.PUNCTUATED).Trim();
              }
            class_number = k.Safe((rec as ClassInfoClass).ClassNumber.ToString(),k.safe_hint_type.NUM).PadLeft(14,'0');
            //
            public_contact_email = k.EMPTY;
            if (be_production_instance)
              {
              if ((rec as ClassInfoClass).PublicContactEmail != null)
                {
                var safe_email = k.Safe((rec as ClassInfoClass).PublicContactEmail,k.safe_hint_type.EMAIL_ADDRESS);
                if (k.BeValidFormatEmailAddress(safe_email))
                  {
                  public_contact_email = safe_email;
                  }
                }
              }
            else
              {
              public_contact_email = "CeOffering" + class_number + "Pce@frompaper2web.com";
              }
            //
            location_email = k.EMPTY;
            if (be_production_instance)
              {
              if ((rec as ClassInfoClass).LocationEmail != null)
                {
                var safe_email = k.Safe((rec as ClassInfoClass).LocationEmail,k.safe_hint_type.EMAIL_ADDRESS);
                if (k.BeValidFormatEmailAddress(safe_email))
                  {
                  location_email = safe_email;
                  }
                }
              }
            else
              {
              location_email = "CeOffering" + class_number + "Le@frompaper2web.com";
              }
            //length = ((rec as ClassInfoClass).Length == null ? k.EMPTY : k.Safe((rec as ClassInfoClass).Length.Replace("&nbsp;hrs.",k.EMPTY),k.safe_hint_type.REAL_NUM));  //no longer used
            //
            childless_field_assignments_clause = k.EMPTY
            + "class_id = NULLIF('" + (rec as ClassInfoClass).ClassID + "','')"
            + " , sponsor_id = NULLIF('" + (rec as ClassInfoClass).SponsorID + "','')"
            + " , sponsor_number = NULLIF('" + ((rec as ClassInfoClass).SponsorNumber == null ? k.EMPTY : k.Safe((rec as ClassInfoClass).SponsorNumber,k.safe_hint_type.HYPHENATED_NUM)) + "','')"
            + " , class_final_status_id = null"
            + " , course_number = NULLIF('" + (rec as ClassInfoClass).CourseNumber + "','')"
            + " , location = NULLIF('" + ((rec as ClassInfoClass).Location == null ? k.EMPTY : k.Safe((rec as ClassInfoClass).Location,k.safe_hint_type.POSTAL_STREET_ADDRESS).Trim()) + "','')"
            + " , start_date_time = STR_TO_DATE(NULLIF('" + k.Safe((rec as ClassInfoClass).StartDateTime,k.safe_hint_type.DATE_TIME) + "',''),'%m/%d/%Y %H:%i:%s')"
            + " , end_date_time = STR_TO_DATE(NULLIF('" + k.Safe((rec as ClassInfoClass).EndDateTime,k.safe_hint_type.DATE_TIME) + "',''),'%m/%d/%Y %H:%i:%s')"
            + " , start_time = NULLIF('" + ((rec as ClassInfoClass).StartTime == null ? k.EMPTY : k.Safe((rec as ClassInfoClass).StartTime,k.safe_hint_type.PUNCTUATED)).Trim() + "','')"
            + " , end_time = NULLIF('" + ((rec as ClassInfoClass).EndTime == null ? k.EMPTY : k.Safe((rec as ClassInfoClass).EndTime,k.safe_hint_type.PUNCTUATED)).Trim() + "','')"
            + " , public_contact_email = NULLIF('" + public_contact_email + "','')"
            + " , approved = NULLIF('" + (rec as ClassInfoClass).Approved + "','')"
            + " , region_council_num = IFNULL(NULLIF('" + (rec as ClassInfoClass).RegionCouncilNum + "',''),0)"
            + " , class_county_code = NULLIF('" + (rec as ClassInfoClass).ClassCountyCode + "','')"
            + " , location_email = NULLIF('" + location_email + "','')"
            + " , sponsor_name = NULLIF('" + ((rec as ClassInfoClass).SponsorName == null ? k.EMPTY : k.Safe((rec as ClassInfoClass).SponsorName,k.safe_hint_type.ORG_NAME).Trim()) + "','')"
            + " , courses_course_number = NULLIF('" + (rec as ClassInfoClass).CourseNumber + "','')"
            + " , course_title = '" + course_title + "'"
            + " , fr_med_trauma_hours = NULLIF('" + (rec as ClassInfoClass).FR_MedTrauma_Hrs + "','')"
            + " , fr_other_hours = NULLIF('" + (rec as ClassInfoClass).FR_Other_Hrs + "','')"
            + " , emt_med_trauma_hours = NULLIF('" + (rec as ClassInfoClass).EMT_MedTrauma_Hrs + "','')"
            + " , emt_other_hours = NULLIF('" + (rec as ClassInfoClass).EMT_Other_Hrs + "','')"
            + " , emtp_med_trauma_hours = NULLIF('" + (rec as ClassInfoClass).EMTP_MedTrauma_Hrs + "','')"
            + " , emtp_other_hours = NULLIF('" + (rec as ClassInfoClass).EMTP_Other_Hrs + "','')"
            + " , phrn_med_trauma_hours = NULLIF('" + (rec as ClassInfoClass).PHRN_MedTrauma_Hrs + "','')"
            + " , phrn_other_hours = NULLIF('" + (rec as ClassInfoClass).PHRN_Other_Hrs + "','')"
            //+ " , length = NULLIF('" + length + "','')"  //unreliable
            + " , be_stale = false"
            + k.EMPTY;
            transaction = connection.BeginTransaction();
            try
              {
              using var my_sql_command_1 = new MySqlCommand("select 1 from coned_offering where class_number = '" + class_number + "'",connection,transaction);
              if (my_sql_command_1.ExecuteScalar() == null)
                {
                using var my_sql_command_2 = new MySqlCommand
                  ("insert coned_offering set class_number = NULLIF('" + class_number + "',''), " + childless_field_assignments_clause,connection,transaction)
                  ;
                my_sql_command_2.ExecuteNonQuery();
                }
              else
                {
                using var my_sql_command_3 = new MySqlCommand("update coned_offering set " + childless_field_assignments_clause + " where class_number = '" + class_number + "'",connection,transaction);
                my_sql_command_3.ExecuteNonQuery();
                }
              transaction.Commit();
              }
            catch (Exception e)
              {
              transaction.Rollback();
              if(
                  !ConfigurationManager.AppSettings["class_number_skip_list"].Contains(class_number)
                &&
                  !ConfigurationManager.AppSettings["sponsor_number_skip_list"].Contains((rec as ClassInfoClass).SponsorNumber)
                )
              //then
                {
                k.EscalatedException(e,"#noninteractive#" + k.NEW_LINE + "class_number = [" + class_number + "]" + k.NEW_LINE + "childless_field_assignments_clause = [" + childless_field_assignments_clause + "]");
                }
              }
            }
          }
        Close();
        }
      }

    internal string LengthOf(object summary)
      {
      return (summary as coned_offering_summary).length;
      }

    internal string LocationOf(object summary)
      {
      return (summary as coned_offering_summary).location;
      }

    public void MarkUntouchedAsStale()
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "update coned_offering"
        + " set be_stale = TRUE"
        + " where status_id = (select id from coned_offering_status where description = 'NEEDS_CONED_SPONSOR_FINALIZATION')"
        +   " and (select count(practitioner_id) from coned_offering_roster where coned_offering_roster.coned_offering_id = coned_offering.id) = 0",
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void MarkCanceled(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("update coned_offering set class_final_status_id = (select id from coned_offering_class_final_status where short_description = 'CANCELED') where id = '" + id + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void MarkSponsorSaysAlreadySubmitted(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("update coned_offering set status_id = (select id from coned_offering_status where description = 'SPONSOR_SAYS_ALREADY_SUBMITTED'), datetime_discarded = NOW() where id = '" + id + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void MarkSponsorSaysCanceled(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("update coned_offering set status_id = (select id from coned_offering_status where description = 'SPONSOR_SAYS_CANCELED'), datetime_discarded = NOW() where id = '" + id + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void MarkSponsorSaysRanNoCe(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("update coned_offering set status_id = (select id from coned_offering_status where description = 'SPONSOR_SAYS_RAN_NO_CE'), datetime_discarded = NOW() where id = '" + id + "'",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public void MarkStaleAsCanceled()
      {
      Open();
      using var my_sql_command = new MySqlCommand("update coned_offering set class_final_status_id = (select id from coned_offering_class_final_status where short_description = 'CANCELED') where be_stale",connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal string PhrnMedTraumaHoursOf(object summary)
      {
      return (summary as coned_offering_summary).phrn_med_trauma_hours;
      }

    internal string PhrnOtherHoursOf(object summary)
      {
      return (summary as coned_offering_summary).phrn_other_hours;
      }

    internal string PublicContactEmailOf(object summary)
      {
      return (summary as coned_offering_summary).public_contact_email;
      }

    internal void PurgeStaleUnused()
      {
      Open();
      using var my_sql_command = new MySqlCommand
        ("delete from coned_offering where be_stale and start_date_time < SUBDATE(CURDATE(),INTERVAL 6 MONTH) and (select count(*) from coned_offering_roster where coned_offering_id = coned_offering.id) = 0",connection)
        ;
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal struct RosterDue
      {
      internal k.int_nonnegative days_after;
      internal string class_number;
      internal string sponsor_number;
      internal string location;
      internal string start_date_time;
      internal string start_time;
      internal string end_date_time;
      internal string end_time;
      internal string coned_offering_public_contact_email;
      internal string county;
      internal string sponsor_name;
      internal string course_title;
      internal string sponsor_email;
      }
    internal Queue<RosterDue> RosterDueQueue(k.int_nonnegative days_after)
      {
      var roster_due_q = new Queue<RosterDue>();
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT class_number"
        + " , coned_offering.sponsor_number as sponsor_number"
        + " , location"
        + " , DATE_FORMAT(start_date_time,'%a %d %b %Y') as start_date_time"
        + " , start_time"
        + " , DATE_FORMAT(end_date_time,'%a %d %b %Y') as end_date_time"
        + " , end_time"
        + " , coned_offering.public_contact_email as coned_offering_public_contact_email"
        + " , county_code_name_map.name as county"
        + " , sponsor_name"
        + " , course_title"
        + " , teaching_entity.email as sponsor_email"
        + " FROM coned_offering"
        +   " join coned_offering_status on (coned_offering_status.id=coned_offering.status_id)"
        +   " join county_code_name_map on (county_code_name_map.emsrs_code=coned_offering.class_county_code)"
        +   " join region_code_name_map on (region_code_name_map.emsrs_code=coned_offering.region_council_num)"
        +   " left join coned_offering_class_final_status on (coned_offering_class_final_status.id=coned_offering.class_final_status_id)"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        + " where region_code_name_map.be_conedlink_subscriber"
        +   " and coned_offering_status.description = 'NEEDS_CONED_SPONSOR_FINALIZATION'"
        +   " and ((coned_offering_class_final_status.short_description is null) or (coned_offering_class_final_status.short_description <> 'CANCELED'))"
        +   " and end_date_time + '" + days_after.val + "' = CURDATE()",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        roster_due_q.Enqueue
          (
          new RosterDue
            {
            days_after = days_after,
            class_number = dr["class_number"].ToString(),
            sponsor_number = dr["sponsor_number"].ToString(),
            location = dr["location"].ToString(),
            start_date_time = dr["start_date_time"].ToString(),
            start_time = dr["start_time"].ToString(),
            end_date_time = dr["end_date_time"].ToString(),
            end_time = dr["end_time"].ToString(),
            coned_offering_public_contact_email = dr["coned_offering_public_contact_email"].ToString(),
            county = dr["county"].ToString(),
            sponsor_name = dr["sponsor_name"].ToString(),
            course_title = dr["course_title"].ToString(),
            sponsor_email = dr["sponsor_email"].ToString()
            }
          );
        }
      Close();
      return roster_due_q;
      }

    public void Set
      (
      string id,
      string class_id,
      string course_id,
      string class_number,
      string created_by,
      DateTime date_created,
      string last_edited_by,
      DateTime date_last_edited,
      string sponsor_id,
      string sponsor_number,
      string document_status,
      string class_final_status_id,
      string course_number,
      string location,
      string student_cost,
      string tuition_includes,
      bool closed,
      string estimated_students,
      DateTime start_date_time,
      DateTime end_date_time,
      string start_time,
      string end_time,
      string other_dates_and_times,
      string instructors,
      string instructor_qualifications,
      string public_contact_name,
      string public_contact_phone,
      string public_contact_email,
      string public_contact_website,
      string public_contact_notes,
      DateTime date_submitted_to_region,
      DateTime date_received_by_region,
      DateTime date_sponsor_notified,
      DateTime date_registration_sent_to_state,
      DateTime date_cards_sent_to_sponsor,
      DateTime date_materials_to_be_returned,
      bool approved,
      string region_comments,
      string region_council_num,
      string class_county_code,
      string total_class_hours,
      string location_address_1,
      string location_address_2,
      string location_city,
      string location_state,
      string location_zip,
      string location_zip_plus_4,
      string location_phone,
      string location_email,
      string location_of_registration,
      string primary_text,
      string additional_texts,
      DateTime final_registration_date,
      bool offered_as_college_credit,
      DateTime practical_exam_date,
      DateTime written_exam_date,
      string disapproval_reason_id,
      DateTime date_final_paperwork_received,
      bool signed_hardcopy,
      string created_by_first_name,
      string created_by_last_name,
      string class_disapproval_reason_description,
      string sponsor_name,
      string courses_course_number,
      string course_title,
      string status_id,
      string fr_med_trauma_hours,
      string fr_other_hours,
      string emt_med_trauma_hours,
      string emt_other_hours,
      string emtp_med_trauma_hours,
      string emtp_other_hours,
      string phrn_med_trauma_hours,
      string phrn_other_hours,
      string eval_summary_instructional_staff,
      string eval_summary_time_appropriately_used,
      string eval_summary_classroom_training_site,
      string eval_summary_equipment_av,
      string eval_summary_misc_remarks
      )
      {
      var childless_field_assignments_clause = k.EMPTY
      + "class_id = NULLIF('" + class_id + "','')"
      + " , course_id = NULLIF('" + course_id + "','')"
      + " , class_number = NULLIF('" + class_number + "','')"
      + " , created_by = NULLIF('" + created_by + "','')"
      + " , date_created = '" + date_created.ToString("yyyy-MM-dd") + "'"
      + " , last_edited_by = NULLIF('" + last_edited_by + "','')"
      + " , date_last_edited = '" + date_last_edited.ToString("yyyy-MM-dd") + "'"
      + " , sponsor_id = NULLIF('" + sponsor_id + "','')"
      + " , sponsor_number = NULLIF('" + sponsor_number + "','')"
      + " , document_status = NULLIF('" + document_status + "','')"
      + " , class_final_status_id = NULLIF(IFNULL((select id from coned_offering_class_final_status where short_description = '" + class_final_status_id + "'),''),'')"
      + " , course_number = NULLIF('" + course_number + "','')"
      + " , location = NULLIF('" + location + "','')"
      + " , student_cost = NULLIF('" + student_cost + "','')"
      + " , tuition_includes = NULLIF('" + tuition_includes + "','')"
      + " , closed = NULLIF('" + closed.ToString() + "','')"
      + " , estimated_students = NULLIF('" + estimated_students + "','')"
      + " , start_date_time = '" + start_date_time.ToString("yyyy-MM-dd") + "'"
      + " , end_date_time = '" + end_date_time.ToString("yyyy-MM-dd") + "'"
      + " , start_time = NULLIF('" + start_time + "','')"
      + " , end_time = NULLIF('" + end_time + "','')"
      + " , other_dates_and_times = NULLIF('" + other_dates_and_times + "','')"
      + " , instructors = NULLIF('" + instructors + "','')"
      + " , instructor_qualifications = NULLIF('" + instructor_qualifications + "','')"
      + " , public_contact_name = NULLIF('" + public_contact_name + "','')"
      + " , public_contact_phone = NULLIF('" + public_contact_phone + "','')"
      + " , public_contact_email = NULLIF('" + public_contact_email + "','')"
      + " , public_contact_website = NULLIF('" + public_contact_website + "','')"
      + " , public_contact_notes = NULLIF('" + public_contact_notes + "','')"
      + " , date_submitted_to_region = '" + date_submitted_to_region.ToString("yyyy-MM-dd") + "'"
      + " , date_received_by_region = '" + date_received_by_region.ToString("yyyy-MM-dd") + "'"
      + " , date_sponsor_notified = '" + date_sponsor_notified.ToString("yyyy-MM-dd") + "'"
      + " , date_registration_sent_to_state = '" + date_registration_sent_to_state.ToString("yyyy-MM-dd") + "'"
      + " , date_cards_sent_to_sponsor = '" + date_cards_sent_to_sponsor.ToString("yyyy-MM-dd") + "'"
      + " , date_materials_to_be_returned = '" + date_materials_to_be_returned.ToString("yyyy-MM-dd") + "'"
      + " , approved = NULLIF('" + approved.ToString() + "','')"
      + " , region_comments = NULLIF('" + region_comments + "','')"
      + " , region_council_num = NULLIF('" + region_council_num + "','')"
      + " , class_county_code = NULLIF('" + class_county_code + "','')"
      + " , total_class_hours = NULLIF('" + total_class_hours + "','')"
      + " , location_address_1 = NULLIF('" + location_address_1 + "','')"
      + " , location_address_2 = NULLIF('" + location_address_2 + "','')"
      + " , location_city = NULLIF('" + location_city + "','')"
      + " , location_state = NULLIF('" + location_state + "','')"
      + " , location_zip = NULLIF('" + location_zip + "','')"
      + " , location_zip_plus_4 = NULLIF('" + location_zip_plus_4 + "','')"
      + " , location_phone = NULLIF('" + location_phone + "','')"
      + " , location_email = NULLIF('" + location_email + "','')"
      + " , location_of_registration = NULLIF('" + location_of_registration + "','')"
      + " , primary_text = NULLIF('" + primary_text + "','')"
      + " , additional_texts = NULLIF('" + additional_texts + "','')"
      + " , final_registration_date = '" + final_registration_date.ToString("yyyy-MM-dd") + "'"
      + " , offered_as_college_credit = NULLIF('" + offered_as_college_credit.ToString() + "','')"
      + " , practical_exam_date = '" + practical_exam_date.ToString("yyyy-MM-dd") + "'"
      + " , written_exam_date = '" + written_exam_date.ToString("yyyy-MM-dd") + "'"
      + " , disapproval_reason_id = NULLIF('" + disapproval_reason_id + "','')"
      + " , date_final_paperwork_received = '" + date_final_paperwork_received.ToString("yyyy-MM-dd") + "'"
      + " , signed_hardcopy = NULLIF('" + signed_hardcopy.ToString() + "','')"
      + " , created_by_first_name = NULLIF('" + created_by_first_name + "','')"
      + " , created_by_last_name = NULLIF('" + created_by_last_name + "','')"
      + " , class_disapproval_reason_description = NULLIF('" + class_disapproval_reason_description + "','')"
      + " , sponsor_name = NULLIF('" + sponsor_name + "','')"
      + " , courses_course_number = NULLIF('" + courses_course_number + "','')"
      + " , course_title = NULLIF('" + course_title + "','')"
      + " , status_id = NULLIF('" + status_id + "','')"
      + " , fr_med_trauma_hours = NULLIF(" + fr_med_trauma_hours + "','')"
      + " , fr_other_hours = NULLIF(" + fr_other_hours + "','')"
      + " , emt_med_trauma_hours = NULLIF(" + emt_med_trauma_hours + "','')"
      + " , emt_other_hours = NULLIF(" + emt_other_hours + "','')"
      + " , emtp_med_trauma_hours = NULLIF(" + emtp_med_trauma_hours + "','')"
      + " , emtp_other_hours = NULLIF(" + emtp_other_hours + "','')"
      + " , phrn_med_trauma_hours = NULLIF(" + phrn_med_trauma_hours + "','')"
      + " , phrn_other_hours = NULLIF(" + phrn_other_hours + "','')"
      + " , eval_summary_instructional_staff = NULLIF('" + eval_summary_instructional_staff + "','')"
      + " , eval_summary_time_appropriately_used = NULLIF('" + eval_summary_time_appropriately_used + "','')"
      + " , eval_summary_classroom_training_site = NULLIF('" + eval_summary_classroom_training_site + "','')"
      + " , eval_summary_equipment_av = NULLIF('" + eval_summary_equipment_av + "','')"
      + " , eval_summary_misc_remarks = NULLIF('" + eval_summary_misc_remarks + "','')"
      + k.EMPTY;
      Open();
      using var my_sql_command = new MySqlCommand
        (
        db_trail.Saved
          (
          "insert coned_offering"
          + " set id = NULLIF('" + id + "','')"
          + " , " + childless_field_assignments_clause
          + " on duplicate key update "
          + childless_field_assignments_clause
          ),
          connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void SetEvalSummary
      (
      string id,
      string eval_summary_instructional_staff,
      string eval_summary_time_appropriately_used,
      string eval_summary_classroom_training_site,
      string eval_summary_equipment_av,
      string eval_summary_misc_remarks
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        db_trail.Saved
          (
          "update coned_offering set eval_summary_instructional_staff = NULLIF('" + eval_summary_instructional_staff + "','')"
          + " , eval_summary_time_appropriately_used = NULLIF('" + eval_summary_time_appropriately_used + "','')"
          + " , eval_summary_classroom_training_site = NULLIF('" + eval_summary_classroom_training_site + "','')"
          + " , eval_summary_equipment_av = NULLIF('" + eval_summary_equipment_av + "','')"
          + " , eval_summary_misc_remarks = NULLIF('" + eval_summary_misc_remarks + "','')"
          + " where id = '" + id + "'"
          ),
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal void SetStatus
      (
      string id,
      coned_offering_status_enumeration status,
      bool be_submitted_to_emsrs_directly = false
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        db_trail.Saved
          (
          "update coned_offering"
          + " set status_id = NULLIF('" + status.ToString("D") + "','')"
          + (status == coned_offering_status_enumeration.ARCHIVED ? " , region_comments = CONCAT(IFNULL(region_comments,''),' ## Roster submission to EMSRS handled " + (be_submitted_to_emsrs_directly ? "directly by " + ConfigurationManager.AppSettings["application_name"] : "indirectly by regional staff") + "')" : k.EMPTY)
          + " where id = '" + id + "'"
          ),
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    internal string SponsorContactEmailOf(object summary)
      {
      return (summary as coned_offering_summary).sponsor_contact_email;
      }

    internal string SponsorEmailOf(object summary)
      {
      return (summary as coned_offering_summary).sponsor_email;
      }

    internal string SponsorIdOf(object summary)
      {
      return (summary as coned_offering_summary).sponsor_id;
      }

    internal string SponsorNameOf(object summary)
      {
      return (summary as coned_offering_summary).sponsor_name;
      }

    internal string SponsorNumberOf(object summary)
      {
      return (summary as coned_offering_summary).sponsor_number;
      }

    internal string SponsorPublicContactEmailOf(object summary)
      {
      return (summary as coned_offering_summary).sponsor_public_contact_email;
      }

    internal string StartDateOf(object summary)
      {
      return (summary as coned_offering_summary).start_date;
      }

    internal string StartOtherOf(object summary)
      {
      return (summary as coned_offering_summary).start_other;
      }

    internal coned_offering_status_enumeration StatusOf(object summary)
      {
      return (summary as coned_offering_summary).status;
      }

    public object Summary(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT class_id"
        + " , class_number"
        + " , course_number"
        + " , course_title"
        + " , location"
        + " , CONCAT(start_date_time) as start_date_time"
        + " , start_time"
        + " , CONCAT(end_date_time) as end_date_time"
        + " , end_time"
        + " , IFNULL(length,'') as length"
        + " , approved"
        + " , status_id"
        + " , sponsor_id"
        + " , coned_offering.sponsor_number as sponsor_number"
        + " , IFNULL(name,IFNULL(short_name,'')) as sponsor_name"
        + " , IFNULL(email,'') as sponsor_email"
        + " , IFNULL(contact_email,'') as sponsor_contact_email"
        + " , IFNULL(teaching_entity.public_contact_email,'') as sponsor_public_contact_email"
        + " , IFNULL(coned_offering.public_contact_email,'') as public_contact_email"
        + " , fr_med_trauma_hours"
        + " , fr_other_hours"
        + " , emt_med_trauma_hours"
        + " , emt_other_hours"
        + " , emtp_med_trauma_hours"
        + " , emtp_other_hours"
        + " , phrn_med_trauma_hours"
        + " , phrn_other_hours"
        + " , eval_summary_instructional_staff"
        + " , eval_summary_time_appropriately_used"
        + " , eval_summary_classroom_training_site"
        + " , eval_summary_equipment_av"
        + " , eval_summary_misc_remarks"
        + " FROM coned_offering"
        +   " join teaching_entity on (teaching_entity.emsrs_id=coned_offering.sponsor_id)"
        + " where coned_offering.id = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new coned_offering_summary()
        {
        id = id,
        class_id = dr["class_id"].ToString(),
        class_number = dr["class_number"].ToString(),
        course_number = dr["course_number"].ToString(),
        course_title = dr["course_title"].ToString(),
        location = dr["location"].ToString(),
        start_date = dr["start_date_time"].ToString(),
        start_other = dr["start_time"].ToString(),
        end_date = dr["end_date_time"].ToString(),
        end_other = dr["end_time"].ToString(),
        length = dr["length"].ToString(),
        approved = dr["approved"].ToString(),
        status = (coned_offering_status_enumeration)Enum.Parse(typeof(coned_offering_status_enumeration),dr["status_id"].ToString()),
        sponsor_id = dr["sponsor_id"].ToString(),
        sponsor_number = dr["sponsor_number"].ToString(),
        sponsor_name = dr["sponsor_name"].ToString(),
        sponsor_email = dr["sponsor_email"].ToString(),
        sponsor_contact_email = dr["sponsor_contact_email"].ToString(),
        sponsor_public_contact_email = dr["sponsor_public_contact_email"].ToString(),
        public_contact_email = dr["public_contact_email"].ToString(),
        fr_med_trauma_hours = dr["fr_med_trauma_hours"].ToString(),
        fr_other_hours = dr["fr_other_hours"].ToString(),
        emt_med_trauma_hours = dr["emt_med_trauma_hours"].ToString(),
        emt_other_hours = dr["emt_other_hours"].ToString(),
        emtp_med_trauma_hours = dr["emtp_med_trauma_hours"].ToString(),
        emtp_other_hours = dr["emtp_other_hours"].ToString(),
        phrn_med_trauma_hours = dr["phrn_med_trauma_hours"].ToString(),
        phrn_other_hours = dr["phrn_other_hours"].ToString(),
        eval_summary_instructional_staff = dr["eval_summary_instructional_staff"].ToString(),
        eval_summary_time_appropriately_used = dr["eval_summary_time_appropriately_used"].ToString(),
        eval_summary_classroom_training_site = dr["eval_summary_classroom_training_site"].ToString(),
        eval_summary_equipment_av = dr["eval_summary_equipment_av"].ToString(),
        eval_summary_misc_remarks = dr["eval_summary_misc_remarks"].ToString(),
        };
      Close();
      return the_summary;
      }

    } // end TClass_db_coned_offerings

  }
