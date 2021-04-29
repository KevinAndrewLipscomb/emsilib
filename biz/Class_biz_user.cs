using Class_db_user;
using Class_db_users;
using kix;
using System;
using System.Configuration;
using System.Web;

namespace Class_biz_user
  {

  public class TClass_biz_user
    {

    private static class Static
      {
      public const string KIND_THAT_HAS_ROLES = "regional_staffer";
      }

    private readonly TClass_db_user db_user = null;
    private readonly TClass_db_users db_users = null;

    public TClass_biz_user() : base()
      {
      db_user = new TClass_db_user();
      db_users = new TClass_db_users();
      }

    public void BindNotificationsToBaseDataList(object target)
      {
      db_user.BindNotificationsToBaseDataList(IdNum(),target);
      }

    public void BindPrivilegesToBaseDataList(object target)
      {
      db_user.BindPrivilegesToBaseDataList(IdNum(),target);
      }

    public void BindRolesToBaseDataList(object target)
      {
      db_user.BindRolesToBaseDataList(IdNum(),target);
      }

    public string EmailAddress()
      {
      return db_users.PasswordResetEmailAddressOfId(IdNum());
      }

    public string IdNum()
      {
      var id_num = k.EMPTY;
      if (ConfigurationManager.AppSettings["application_name"].Contains("WebEMSOF") || ConfigurationManager.AppSettings["application_name"].Contains("ConEdLink"))
        {
        var name = HttpContext.Current.User.Identity.Name;
        id_num = name.Substring(name.LastIndexOf("_") + 1);
        }
      else
        {
        id_num = db_users.IdOf(HttpContext.Current.User.Identity.Name);
        }
      return id_num;
      }

    public string Kind()
      {
      var name = HttpContext.Current.User.Identity.Name;
      return name.Substring(0, name.LastIndexOf("_"));
      }

    public DateTime LastLoginTime()
      {
      return db_user.LastLoginTime(IdNum());
      }

    public string[] Privileges()
      {
      return db_users.PrivilegesOf(IdNum());
      }

    public string[] Roles()
      {
      var result = new string[] {k.EMPTY};
      if (ConfigurationManager.AppSettings["application_name"].Contains("PACRAT"))
        {
        result = db_user.RolesOf(IdNum());
        }
      else
        {
        if (Kind() == Static.KIND_THAT_HAS_ROLES)
          {
          result = new TClass_db_user().RolesOf("regional_staffer_", IdNum());
          }
        }
      return result;
      }

    }

  }
