using Class_db_regions;
using kix;

namespace Class_biz_regions
  {

  public class TClass_biz_regions
    {

    private readonly TClass_db_regions db_regions = null;

    public TClass_biz_regions() : base()
      {      
      db_regions = new TClass_db_regions();
      }

    public bool BeConedlinkSubscriber(string code)
      {
      return db_regions.BeConedlinkSubscriber(code);
      }

    public bool BePacratSubscriberOf(object summary)
      {
      return db_regions.BePacratSubscriberOf(summary);
      }

    public bool Bind
      (
      string partial_code,
      object target
      )
      {
      return db_regions.Bind(partial_code, target);
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal = "-- region --",
      string selected_value = k.EMPTY,
      string application_filter = "conedlink"
      )
      {
      db_regions.BindDirectToListControl(target, unselected_literal, selected_value, application_filter);
      }

    public void BindEmsrsToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      db_regions.BindEmsrsToListControl(target, unselected_literal, selected_value);
      }
    public void BindEmsrsToListControl(object target, string unselected_literal)
      {
      BindEmsrsToListControl(target, unselected_literal, k.EMPTY);
      }
    public void BindEmsrsToListControl(object target)
      {
      BindEmsrsToListControl(target, "-- region --");
      }

    internal string EmsportalPasswordOf(string code)
      {
      return db_regions.EmsportalPasswordOf(code);
      }

    internal string EmsportalUsernameOf(string code)
      {
      return db_regions.EmsportalUsernameOf(code);
      }

    public void BindPacratManagementBaseDataList
      (
      string sort_order,
      bool be_sort_order_ascending,
      object target,
      bool do_include_all_regions
      )
      {
      db_regions.BindPacratManagementBaseDataList(sort_order,be_sort_order_ascending,target,do_include_all_regions);
      }

    public void BindStrikeTeamAffiliationBaseDataList
      (
      string member_id,
      string sort_order,
      bool be_sort_order_ascending,
      object target
      )
      {
      db_regions.BindStrikeTeamAffiliationBaseDataList(member_id,sort_order,be_sort_order_ascending,target);
      }

    public string CodeOf(object summary)
      {
      return db_regions.CodeOf(summary);
      }

    public string CodeOfEmsrsCode(string emsrs_code)
      {
      return db_regions.CodeOfEmsrsCode(emsrs_code);
      }

    public string ConedlinkEvalSummaryModeDescriptionOf(string code)
      {
      return db_regions.ConedlinkEvalSummaryModeDescriptionOf(code);
      }

    public string ConedlinkEvalSummaryModeIdOf(string code)
      {
      return db_regions.ConedlinkEvalSummaryModeIdOf(code);
      }

    public bool Delete(string code)
      {
      return db_regions.Delete(code);
      }

    public string EmsrsCodeOf(object summary)
      {
      return db_regions.EmsrsCodeOf(summary);
      }

    internal string EmsrsCodeOfCode(string code)
      {
      return db_regions.EmsrsCodeOfCode(code);
      }

    public bool Get
      (
      string code,
      out string description
      )
      {
      return db_regions.Get(code,out description);
      }

    public string EmsrsActivePractitionersNameOf(object summary)
      {
      return db_regions.EmsrsActivePractitionersNameOf(summary);
      }

    public string NameOf(object summary)
      {
      return db_regions.NameOf(summary);
      }

    internal string NameOfCode(string code)
      {
      return db_regions.NameOfCode(code);
      }

    public void Set
      (
      string code,
      string description
      )
      {
      db_regions.Set(code,description);
      }

    public void SetConedlinkEvalSummaryModeId
      (
      string code,
      string id
      )
      {
      db_regions.SetConedlinkEvalSummaryModeId(code,id);
      }

    public void SetPacratSubscriber
      (
      string code,
      bool value
      )
      {
      db_regions.SetPacratSubscriber(code,value);
      }

    public object Summary(string code)
      {
      return db_regions.Summary(code);
      }

    } // end TClass_biz_regions

  }