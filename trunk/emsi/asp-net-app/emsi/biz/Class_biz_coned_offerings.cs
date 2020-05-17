// Derived from KiAspdotnetFramework/component/biz/Class~biz~~template~kicrudhelped~item.cs~template

using Class_biz_accounts;
using Class_biz_notifications;
using Class_db_coned_offering_statuses;
using Class_db_coned_offerings;
using Class_db_practitioners;
using Class_db_regions;
using emsi.ServiceReference_emsams_ConEd;
using external_data_binding.emsams.ConEdClassInfo;
using external_data_binding.emsams.EmptyRequest;
using kix;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Class_biz_coned_offerings
  {

  public class TClass_biz_coned_offerings
    {
    private readonly TClass_biz_accounts biz_accounts = null;
    private readonly TClass_biz_notifications biz_notifications = null;
    private readonly TClass_db_coned_offerings db_coned_offerings = null;
    private readonly TClass_db_practitioners db_practitioners = null;
    private readonly TClass_db_regions db_regions = null;

    public TClass_biz_coned_offerings() : base()
      {
      biz_accounts = new TClass_biz_accounts();
      biz_notifications = new TClass_biz_notifications();
      db_coned_offerings = new TClass_db_coned_offerings();
      db_practitioners = new TClass_db_practitioners();
      db_regions = new TClass_db_regions();
      }

    public void Archive
      (
      object summary,
      bool be_submitted_to_emsrs_directly
      )
      {
      if (db_coned_offerings.StatusOf(summary) == coned_offering_status_enumeration.NEEDS_REGIONAL_PROCESSING)
        {
        var id = db_coned_offerings.IdOf(summary);
        db_practitioners.MarkDobsConfirmed(id);
        db_coned_offerings.SetStatus
          (
          id:id,
          status:coned_offering_status_enumeration.ARCHIVED,
          be_submitted_to_emsrs_directly:be_submitted_to_emsrs_directly
          );
        }
      }

    public bool BeApprovedOf(object summary)
      {
      return (db_coned_offerings.ApprovedOf(summary) == "1");
      }

    public bool BeCeuBreakdownValid(object summary)
      {
      return true
      && (FrMedTraumaHoursOf(summary).Length > 0)
      && (FrOtherHoursOf(summary).Length > 0)
      && (EmtMedTraumaHoursOf(summary).Length > 0)
      && (EmtOtherHoursOf(summary).Length > 0)
      && (EmtpMedTraumaHoursOf(summary).Length > 0)
      && (EmtpOtherHoursOf(summary).Length > 0)
      && (PhrnMedTraumaHoursOf(summary).Length > 0)
      && (PhrnOtherHoursOf(summary).Length > 0)
      ;
      }

    public bool BeOkToCloseClassAndSubmitForCredit
      (
      object summary,
      k.int_nonnegative num_attendees,
      k.int_nonnegative num_attendees_with_known_birth_dates
      )
      {
      return BeApprovedOf(summary)
      && (DateTime.Now > DateTime.Parse(db_coned_offerings.StartDateOf(summary)).AddHours((double)FullHourDurationOf(summary)))
      && (num_attendees.val > 0)
      && (num_attendees_with_known_birth_dates.val == num_attendees.val);
      }

    public bool BeOkToEditRoster(object summary)
      {
      return (db_coned_offerings.StatusOf(summary) == coned_offering_status_enumeration.NEEDS_CONED_SPONSOR_FINALIZATION);
      }

    public bool Bind(string partial_spec, object target)
      {
      return db_coned_offerings.Bind(partial_spec, target);
      }

    public void BindBaseDataListPractitionerConedDetail
      (
      string practitioner_id,
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string sponsor_id_filter,
      string range
      )
      {
      db_coned_offerings.BindBaseDataListPractitionerConedDetail(practitioner_id,sort_order,be_sort_order_ascending,target,sponsor_id_filter,range);
      }

    public void BindClassCatalog
      (
      string coned_sponsor_user_id,
      string range,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      db_coned_offerings.BindClassCatalog(coned_sponsor_user_id,range,sort_order,be_sort_order_ascending,target);
      }

    public void BindDirectToListControlForCopy
      (
      string id,
      object target
      )
      {
      db_coned_offerings.BindDirectToListControlForCopy(id,target);
      }

    public void BindOutstandingRosters
      (
      string region_code,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      db_coned_offerings.BindOutstandingRosters(region_code,sort_order,be_sort_order_ascending,target);
      }

    public void BindReadyRosters
      (
      string region_code,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      db_coned_offerings.BindReadyRosters(region_code,sort_order,be_sort_order_ascending,target);
      }

    public void BindDirectToListControl(object target)
      {
      db_coned_offerings.BindDirectToListControl(target);
      }

    public void BindDiscardedRosters
      (
      string region_code,
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      string filter
      )
      {
      db_coned_offerings.BindDiscardedRosters(region_code,sort_order,be_sort_order_ascending,target,filter);
      }

    public string ClassIdOf(object summary)
      {
      return db_coned_offerings.ClassIdOf(summary);
      }

    public string ClassNumberOf(object summary)
      {
      return db_coned_offerings.ClassNumberOf(summary);
      }

    public void CloseAndSubmit
      (
      object summary,
      k.int_nonnegative num_attendees,
      string eval_summary_instructional_staff,
      string eval_summary_time_appropriately_used,
      string eval_summary_classroom_training_site,
      string eval_summary_equipment_av,
      string eval_summary_misc_remarks
      )
      {
      if (db_coned_offerings.StatusOf(summary) == coned_offering_status_enumeration.NEEDS_CONED_SPONSOR_FINALIZATION)
        {
        var id = db_coned_offerings.IdOf(summary);
        db_coned_offerings.SetEvalSummary(id,eval_summary_instructional_staff,eval_summary_time_appropriately_used,eval_summary_classroom_training_site,eval_summary_equipment_av,eval_summary_misc_remarks);
        db_coned_offerings.SetStatus(id,coned_offering_status_enumeration.NEEDS_REGIONAL_PROCESSING);
        biz_notifications.IssueForClassClosed
          (
          sponsor_number:db_coned_offerings.SponsorNumberOf(summary),
          sponsor_name:db_coned_offerings.SponsorNameOf(summary),
          sponsor_email:db_coned_offerings.SponsorEmailOf(summary),
          sponsor_contact_email:db_coned_offerings.SponsorContactEmailOf(summary),
          sponsor_public_contact_email:db_coned_offerings.SponsorPublicContactEmailOf(summary),
          coned_offering_public_contact_email:db_coned_offerings.PublicContactEmailOf(summary),
          class_number:db_coned_offerings.ClassNumberOf(summary),
          course_title:db_coned_offerings.CourseTitleOf(summary),
          start:db_coned_offerings.StartDateOf(summary) + k.SPACE + db_coned_offerings.StartOtherOf(summary),
          end:db_coned_offerings.EndDateOf(summary) + k.SPACE + db_coned_offerings.EndOtherOf(summary),
          length:db_coned_offerings.LengthOf(summary),
          location:db_coned_offerings.LocationOf(summary),
          num_attendees:num_attendees,
          status_description:coned_offering_status_enumeration.NEEDS_REGIONAL_PROCESSING.ToString()
          );
        }
      }

    public string CourseNumberOf(object summary)
      {
      return db_coned_offerings.CourseNumberOf(summary);
      }

    public string CourseTitleOf(object summary)
      {
      return db_coned_offerings.CourseTitleOf(summary);
      }

    public bool Delete(string id)
      {
      return db_coned_offerings.Delete(id);
      }

    public string EmtMedTraumaHoursOf(object summary)
      {
      return db_coned_offerings.EmtMedTraumaHoursOf(summary);
      }

    public string EmtOtherHoursOf(object summary)
      {
      return db_coned_offerings.EmtOtherHoursOf(summary);
      }

    public string EmtpMedTraumaHoursOf(object summary)
      {
      return db_coned_offerings.EmtpMedTraumaHoursOf(summary);
      }

    public string EmtpOtherHoursOf(object summary)
      {
      return db_coned_offerings.EmtpOtherHoursOf(summary);
      }

    public string EndDateOf(object summary)
      {
      return db_coned_offerings.EndDateOf(summary);
      }

    public string EndOtherOf(object summary)
      {
      return db_coned_offerings.EndOtherOf(summary);
      }

    public string EvalSummaryInstructionalStaffOf(object summary)
      {
      return db_coned_offerings.EvalSummaryInstructionalStaffOf(summary);
      }

    public string EvalSummaryTimeAppropriatelyUsedOf(object summary)
      {
      return db_coned_offerings.EvalSummaryTimeAppropriatelyUsedOf(summary);
      }

    public string EvalSummaryClassroomTrainingSiteOf(object summary)
      {
      return db_coned_offerings.EvalSummaryClassroomTrainingSiteOf(summary);
      }

    public string EvalSummaryEquipmentAvOf(object summary)
      {
      return db_coned_offerings.EvalSummaryEquipmentAvOf(summary);
      }

    public string EvalSummaryMiscRemarksOf(object summary)
      {
      return db_coned_offerings.EvalSummaryMiscRemarksOf(summary);
      }

    public string FrMedTraumaHoursOf(object summary)
      {
      return db_coned_offerings.FrMedTraumaHoursOf(summary);
      }

    public string FrOtherHoursOf(object summary)
      {
      return db_coned_offerings.FrOtherHoursOf(summary);
      }

    public decimal FullHourDurationOf(object summary)
      {
      var fr_med_trauma_hours_string = FrMedTraumaHoursOf(summary);
      var fr_other_hours_string = FrOtherHoursOf(summary);
      var emt_med_trauma_hours_string = EmtMedTraumaHoursOf(summary);
      var emt_other_hours_string = EmtOtherHoursOf(summary);
      var emtp_med_trauma_hours_string = EmtpMedTraumaHoursOf(summary);
      var emtp_other_hours_string = EmtpOtherHoursOf(summary);
      var phrn_med_trauma_hours_string = PhrnMedTraumaHoursOf(summary);
      var phrn_other_hours_string = PhrnOtherHoursOf(summary);
      //
      var fr_total_hours = (fr_med_trauma_hours_string.Length > 0 ? decimal.Parse(fr_med_trauma_hours_string) : 0) + (fr_other_hours_string.Length > 0 ? decimal.Parse(fr_other_hours_string) : 0);
      var emt_total_hours = (emt_med_trauma_hours_string.Length > 0 ? decimal.Parse(emt_med_trauma_hours_string) : 0) + (emt_other_hours_string.Length > 0 ? decimal.Parse(emt_other_hours_string) : 0);
      var emtp_total_hours = (emtp_med_trauma_hours_string.Length > 0 ? decimal.Parse(emtp_med_trauma_hours_string) : 0) + (emtp_other_hours_string.Length > 0 ? decimal.Parse(emtp_other_hours_string) : 0);
      var phrn_total_hours = (phrn_med_trauma_hours_string.Length > 0 ? decimal.Parse(phrn_med_trauma_hours_string) : 0) + (phrn_other_hours_string.Length > 0 ? decimal.Parse(phrn_other_hours_string) : 0);
      //
      return Math.Max(Math.Max(Math.Max(fr_total_hours,emt_total_hours),emtp_total_hours),phrn_total_hours);
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
      return db_coned_offerings.Get
        (
        id,
        out class_id,
        out course_id,
        out class_number,
        out created_by,
        out date_created,
        out last_edited_by,
        out date_last_edited,
        out sponsor_id,
        out sponsor_number,
        out document_status,
        out class_final_status_id,
        out course_number,
        out location,
        out student_cost,
        out tuition_includes,
        out closed,
        out estimated_students,
        out start_date_time,
        out end_date_time,
        out start_time,
        out end_time,
        out other_dates_and_times,
        out instructors,
        out instructor_qualifications,
        out public_contact_name,
        out public_contact_phone,
        out public_contact_email,
        out public_contact_website,
        out public_contact_notes,
        out date_submitted_to_region,
        out date_received_by_region,
        out date_sponsor_notified,
        out date_registration_sent_to_state,
        out date_cards_sent_to_sponsor,
        out date_materials_to_be_returned,
        out approved,
        out region_comments,
        out region_council_num,
        out class_county_code,
        out total_class_hours,
        out location_address_1,
        out location_address_2,
        out location_city,
        out location_state,
        out location_zip,
        out location_zip_plus_4,
        out location_phone,
        out location_email,
        out location_of_registration,
        out primary_text,
        out additional_texts,
        out final_registration_date,
        out offered_as_college_credit,
        out practical_exam_date,
        out written_exam_date,
        out disapproval_reason_id,
        out date_final_paperwork_received,
        out signed_hardcopy,
        out created_by_first_name,
        out created_by_last_name,
        out class_disapproval_reason_description,
        out sponsor_name,
        out courses_course_number,
        out course_title,
        out status_id,
        out fr_med_trauma_hours,
        out fr_other_hours,
        out emt_med_trauma_hours,
        out emt_other_hours,
        out emtp_med_trauma_hours,
        out emtp_other_hours,
        out phrn_med_trauma_hours,
        out phrn_other_hours,
        out eval_summary_instructional_staff,
        out eval_summary_time_appropriately_used,
        out eval_summary_classroom_training_site,
        out eval_summary_equipment_av,
        out eval_summary_misc_remarks
        );
      }

    public void GetForTrainingCertificates
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
      db_coned_offerings.GetForTrainingCertificates
        (
        id,
        out class_number,
        out sponsor_number,
        out sponsor_name,
        out course_title,
        out date_final,
        ref fr_med_trauma_ceus,
        ref fr_other_ceus,
        ref emt_med_trauma_ceus,
        ref emt_other_ceus,
        ref emtp_med_trauma_ceus,
        ref emtp_other_ceus,
        ref phrn_med_trauma_ceus,
        ref phrn_other_ceus
        );
      }

    public string IdOf(object summary)
      {
      return db_coned_offerings.IdOf(summary);
      }

    public void ImportLatestFromEmsrs()
      {
      var client = new ConedClient();
      var empty_request_obj = new EmptyRequest();
      empty_request_obj.GUID = ConfigurationManager.AppSettings["emsams_service_references_guid"];
      //
      db_coned_offerings.MarkUntouchedAsStale();
      using var empty_request = new StringWriter();
      new XmlSerializer(typeof(EmptyRequest)).Serialize(empty_request,empty_request_obj);
      var response = client.GetClassInfo(emptyRequest:empty_request.ToString());
      using var xml_reader = XmlReader.Create(input:new StringReader(response));
      db_coned_offerings.ImportLatestFromEmsrs(recs:ArrayList.Adapter(((ClassInfo)new XmlSerializer(typeof(ClassInfo)).Deserialize(xml_reader)).Class));
      db_coned_offerings.MarkStaleAsCanceled();
      client.Close();
      }

    public k.decimal_nonnegative LengthOf(object summary)
      {
      var length = new k.decimal_nonnegative();
      var length_string = db_coned_offerings.LengthOf(summary);
      length.val = (length_string.Length > 0 ? decimal.Parse(length_string) : 0);
      return length;
      }

    public string LocationOf(object summary)
      {
      return db_coned_offerings.LocationOf(summary);
      }

    public void MakeRosterDueNotifications()
      {
      Queue<TClass_db_coned_offerings.RosterDue> roster_due_q;
      foreach (int days_after in new int[]{1,4,7,9,10})
        {
        roster_due_q = db_coned_offerings.RosterDueQueue(new k.int_nonnegative(days_after));
        while (roster_due_q.Count > 0)
          {
          biz_notifications.IssueForRosterDue(roster_due_q.Dequeue());
          }
        }
      }

    public void MarkRosterAlreadySubmitted
      (
      object summary
      )
      {
      biz_notifications.IssueForSponsorSaysAlreadySubmitted
        (
        sponsor_number:db_coned_offerings.SponsorNumberOf(summary),
        sponsor_name:db_coned_offerings.SponsorNameOf(summary),
        sponsor_email:db_coned_offerings.SponsorEmailOf(summary),
        sponsor_contact_email:db_coned_offerings.SponsorContactEmailOf(summary),
        sponsor_public_contact_email:db_coned_offerings.SponsorPublicContactEmailOf(summary),
        coned_offering_public_contact_email:db_coned_offerings.PublicContactEmailOf(summary),
        class_number:db_coned_offerings.ClassNumberOf(summary),
        course_title:db_coned_offerings.CourseTitleOf(summary),
        start:db_coned_offerings.StartDateOf(summary) + k.SPACE + db_coned_offerings.StartOtherOf(summary),
        end:db_coned_offerings.EndDateOf(summary) + k.SPACE + db_coned_offerings.EndOtherOf(summary),
        length:db_coned_offerings.LengthOf(summary),
        location:db_coned_offerings.LocationOf(summary),
        num_attendees:new k.int_nonnegative(0),
        status_description:coned_offering_status_enumeration.SPONSOR_SAYS_ALREADY_SUBMITTED.ToString()
        );
      db_coned_offerings.MarkSponsorSaysAlreadySubmitted(IdOf(summary));
      }

    public void RequestCancellation
      (
      object summary
      )
      {
      biz_notifications.IssueForSponsorSaysCanceled
        (
        sponsor_number:db_coned_offerings.SponsorNumberOf(summary),
        sponsor_name:db_coned_offerings.SponsorNameOf(summary),
        sponsor_email:db_coned_offerings.SponsorEmailOf(summary),
        sponsor_contact_email:db_coned_offerings.SponsorContactEmailOf(summary),
        sponsor_public_contact_email:db_coned_offerings.SponsorPublicContactEmailOf(summary),
        coned_offering_public_contact_email:db_coned_offerings.PublicContactEmailOf(summary),
        class_number:db_coned_offerings.ClassNumberOf(summary),
        course_title:db_coned_offerings.CourseTitleOf(summary),
        start:db_coned_offerings.StartDateOf(summary) + k.SPACE + db_coned_offerings.StartOtherOf(summary),
        end:db_coned_offerings.EndDateOf(summary) + k.SPACE + db_coned_offerings.EndOtherOf(summary),
        length:db_coned_offerings.LengthOf(summary),
        location:db_coned_offerings.LocationOf(summary),
        num_attendees:new k.int_nonnegative(0),
        status_description:coned_offering_status_enumeration.SPONSOR_SAYS_CLASS_CANCELED.ToString()
        );
      db_coned_offerings.MarkSponsorSaysCanceled(IdOf(summary));
      }

    public void RequestRanNoCe
      (
      object summary
      )
      {
      biz_notifications.IssueForSponsorSaysRanNoCe
        (
        sponsor_number:db_coned_offerings.SponsorNumberOf(summary),
        sponsor_name:db_coned_offerings.SponsorNameOf(summary),
        sponsor_email:db_coned_offerings.SponsorEmailOf(summary),
        sponsor_contact_email:db_coned_offerings.SponsorContactEmailOf(summary),
        sponsor_public_contact_email:db_coned_offerings.SponsorPublicContactEmailOf(summary),
        coned_offering_public_contact_email:db_coned_offerings.PublicContactEmailOf(summary),
        class_number:db_coned_offerings.ClassNumberOf(summary),
        course_title:db_coned_offerings.CourseTitleOf(summary),
        start:db_coned_offerings.StartDateOf(summary) + k.SPACE + db_coned_offerings.StartOtherOf(summary),
        end:db_coned_offerings.EndDateOf(summary) + k.SPACE + db_coned_offerings.EndOtherOf(summary),
        length:db_coned_offerings.LengthOf(summary),
        location:db_coned_offerings.LocationOf(summary),
        num_attendees:new k.int_nonnegative(0),
        status_description:coned_offering_status_enumeration.SPONSOR_SAYS_RAN_NO_CE.ToString()
        );
      db_coned_offerings.MarkSponsorSaysRanNoCe(IdOf(summary));
      }

    public string PhrnMedTraumaHoursOf(object summary)
      {
      return db_coned_offerings.PhrnMedTraumaHoursOf(summary);
      }

    public string PhrnOtherHoursOf(object summary)
      {
      return db_coned_offerings.PhrnOtherHoursOf(summary);
      }

    public string PublicContactEmailOf(object summary)
      {
      return db_coned_offerings.PublicContactEmailOf(summary);
      }

    public void PurgeStaleUnused()
      {
      db_coned_offerings.PurgeStaleUnused();
      }

    public string RegionCodeOf(string class_number)
      {
      return db_regions.CodeOfEmsrsCode(class_number.Substring(0,2));
      }

    public void ReturnToSponsor
      (
      object summary,
      k.int_nonnegative num_attendees,
      string actor,
      string disapproval_reason
      )
      {
      db_coned_offerings.SetStatus(IdOf(summary),coned_offering_status_enumeration.NEEDS_CONED_SPONSOR_FINALIZATION);
      biz_notifications.IssueForClassUnclosed
        (
        sponsor_email_target:(PublicContactEmailOf(summary) + k.COMMA + SponsorContactEmailOf(summary) + k.COMMA + SponsorEmailOf(summary) + k.COMMA + SponsorPublicContactEmailOf(summary)).Replace(k.COMMA + k.COMMA,k.COMMA),
        class_number:ClassNumberOf(summary),
        course_title:CourseTitleOf(summary),
        start:StartDateOf(summary) + k.SPACE + StartOtherOf(summary),
        end:EndDateOf(summary) + k.SPACE + EndOtherOf(summary),
        length:LengthOf(summary).val.ToString(),
        num_attendees:num_attendees.val.ToString(),
        location:LocationOf(summary),
        actor:actor,
        actor_email_address:biz_accounts.SelfEmailAddress(),
        status_description:coned_offering_status_enumeration.NEEDS_CONED_SPONSOR_FINALIZATION.ToString(),
        reason:disapproval_reason
        );
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
      db_coned_offerings.Set
        (
        id,
        class_id,
        course_id,
        class_number,
        created_by,
        date_created,
        last_edited_by,
        date_last_edited,
        sponsor_id,
        sponsor_number,
        document_status,
        class_final_status_id,
        course_number,
        location,
        student_cost,
        tuition_includes,
        closed,
        estimated_students,
        start_date_time,
        end_date_time,
        start_time,
        end_time,
        other_dates_and_times,
        instructors,
        instructor_qualifications,
        public_contact_name,
        public_contact_phone,
        public_contact_email,
        public_contact_website,
        public_contact_notes,
        date_submitted_to_region,
        date_received_by_region,
        date_sponsor_notified,
        date_registration_sent_to_state,
        date_cards_sent_to_sponsor,
        date_materials_to_be_returned,
        approved,
        region_comments,
        region_council_num,
        class_county_code,
        total_class_hours,
        location_address_1,
        location_address_2,
        location_city,
        location_state,
        location_zip,
        location_zip_plus_4,
        location_phone,
        location_email,
        location_of_registration,
        primary_text,
        additional_texts,
        final_registration_date,
        offered_as_college_credit,
        practical_exam_date,
        written_exam_date,
        disapproval_reason_id,
        date_final_paperwork_received,
        signed_hardcopy,
        created_by_first_name,
        created_by_last_name,
        class_disapproval_reason_description,
        sponsor_name,
        courses_course_number,
        course_title,
        status_id,
        fr_med_trauma_hours,
        fr_other_hours,
        emt_med_trauma_hours,
        emt_other_hours,
        emtp_med_trauma_hours,
        emtp_other_hours,
        phrn_med_trauma_hours,
        phrn_other_hours,
        eval_summary_instructional_staff,
        eval_summary_time_appropriately_used,
        eval_summary_classroom_training_site,
        eval_summary_equipment_av,
        eval_summary_misc_remarks
        );
      }

    public string SponsorContactEmailOf(object summary)
      {
      return db_coned_offerings.SponsorContactEmailOf(summary);
      }

    public string SponsorEmailOf(object summary)
      {
      return db_coned_offerings.SponsorEmailOf(summary);
      }

    public string SponsorNumberOf(object summary)
      {
      return db_coned_offerings.SponsorNumberOf(summary);
      }

    public string SponsorPublicContactEmailOf(object summary)
      {
      return db_coned_offerings.SponsorPublicContactEmailOf(summary);
      }

    public string StandardSafeRenditionOf(string class_number)
      {
      return k.Safe(class_number,k.safe_hint_type.NUM).Insert(8,k.HYPHEN).Insert(2,k.HYPHEN);
      }

    public string StartDateOf(object summary)
      {
      return db_coned_offerings.StartDateOf(summary);
      }

    public string StartOtherOf(object summary)
      {
      return db_coned_offerings.StartOtherOf(summary);
      }

    public string StatusOf(object summary)
      {
      return db_coned_offerings.StatusOf(summary).ToString();
      }

    public object Summary(string id)
      {
      return db_coned_offerings.Summary(id);
      }

    } // end TClass_biz_coned_offerings

  }
