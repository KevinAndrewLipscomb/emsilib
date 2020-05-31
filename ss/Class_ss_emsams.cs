using Class_biz_regions;
using Class_ss;
using HtmlAgilityPack;
using kix;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Web;

namespace ConEdLink.component.ss
  {
  public static class Class_ss_emsams_Static
    {
    }

  public class Class_ss_emsams : TClass_ss
    {

    private readonly TClass_biz_regions biz_regions = null;

    public Class_ss_emsams() : base()
      {      
      biz_regions = new TClass_biz_regions();
      }

    //--
    //
    // BEGIN code generated initially by Fiddler extension "Request to Code"
    //
    #pragma warning disable CA1031 // Do not catch general exception types
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
    //
    //--

    private static bool Request_ems_health_state_pa_us_Emsportal
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsportal/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "http://www.emsi.org/";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"loginid=Rekaufman; __utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_Emsportal_Login
      (
      CookieContainer cookie_container,
      string view_state,
      string event_validation,
      string username,
      string password,
      out HttpWebResponse response
      )
      {
	    response = null;
	    try
	      {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsportal/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        //
		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsportal/";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=dh4f2wnm3ei4m1xogh0yp3lb");
        //
		    request.Method = "POST";
        //
		    string postString = @"__LASTFOCUS=&__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation) + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtUserName=" + HttpUtility.UrlEncode(username) + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtPassword=" + HttpUtility.UrlEncode(password) + "&ctl00%24ctl00%24SessionLinkBar%24Content%24cmdLoginUser=Login";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();
        //
		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	      }
	    catch (Exception)
	      {
		    if(response != null) response.Close();
		    return false;
	      }
	    return true;
      }

    private static bool Request_ems_health_state_pa_us_EmsportalApplicationlist
    (
    CookieContainer cookie_container,
    out HttpWebResponse response
    )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsportal/ApplicationList.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsportal/";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"loginid=Rekaufman; ASP.NET_SessionId=yals2q1pqdsx0xgye24t2d2u; __utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	      return true;
      }

    private static bool Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	      {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/EMSPortal/ApplicationTransfers/TransferToConEd.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/EMSPortal/ApplicationList.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=hymy3yujgf1gaopjfodpcqfn");

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
  	    }
	    catch (Exception)
	      {
		    if(response != null) response.Close();
		    return false;
	      }

	    return true;
      }

    private static bool Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	      {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/EMSPortal/ApplicationTransfers/TransferToEMSREG.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/EMSPortal/ApplicationList.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=hymy3yujgf1gaopjfodpcqfn");

		    response = (HttpWebResponse)request.GetResponse();
	      }
	    catch (WebException e)
	      {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
  	    }
	    catch (Exception)
	      {
		    if(response != null) response.Close();
		    return false;
	      }

	    return true;
      }

    private static bool Request_ems_health_state_pa_us_EmsregDefault
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/default.aspx?Version=2.2.25");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/EMSPortal/ApplicationList.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=gra35ekc4fbgr4yn5umh2oii; loginid=Rekaufman");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    private static string Request_ems_health_state_pa_us_EmsregPractitionerHome
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/practitioner/home.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/EMSPortal/ApplicationList.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=gra35ekc4fbgr4yn5umh2oii; loginid=Rekaufman; EMSREG=A91AB0CB7AB6A388CBC1664AEFB890AA8816CBEF1748B3D6D49C1D92F6988868DA7ACAE842B48004CF52E8A9A91CD3A0582CBFDF8C87D1BAEE31B4894F684484455FE1FBA0CFDAA855AA3117CF2CBA903CAABFCE2759417082B51258F468F133082D8EE27AA3F45534ABAE3E553C522DF79BF19ED36EEC805F45A016EBB9B72FF0144E83DF6B6A8AF60F63FD9B0012744A1E62C36164DA09E965405523E79B0A0ABEDEAAA2267E88F86F7D8BCE167CC2381F138948BC25B53589445951D7CB344A95F6DE4A9B9793EC89779E5C70E076F5957E0DF3EFAF9BB0E34828207FCA9E449E0FFED42543F03E198D16131C9B67");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Message + k.NEW_LINE + e.StackTrace;
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message + k.NEW_LINE + e.StackTrace;
	    }

	    return k.EMPTY;
    }

    private static string Request_ems_health_state_pa_us_EmsregReportsEmsinstructorlistsearch
      (
      CookieContainer cookie_container,
      string region_code,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Reports/EMSInstructorListSearch.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Reports/ReportList.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=3nniwgkydva5klmzj4qfwdva; loginid=rekaufman; EMSREG=C90DF9762916149C733B4F536D25D83E247198F04399FC17422F2CAFF95E772FF9E809F85D2171CDF6EB45658845D72C6D8623EE0B8F00E6319640AB0267276F4E1936ADC022B95EBCE9714DBB9F7CDF96C48BF8E9C624AFDADB108EC30D1836FD394C83FCB90BAFAB9AFE3432F51C0409D3795EDBFB79996B3786FA1272A35F9E9DB152537F859CDDA933858514D4A1D6F14838332C070979EE632612A630B41DA59CC2198BD71E0330976AC8D890DFE4BF162194E570A5E60460F63957410E0A33DD15F3D2A3B39849F5FE2EACC3A08F1678D83D94360EAC525C5360E275EFFA3DDF03BC9C16ED742A37EDE0DB1A74");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Message + k.NEW_LINE + e.StackTrace + k.NEW_LINE + k.NEW_LINE + "region_code = '" + region_code + "'" + k.NEW_LINE;
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message + k.NEW_LINE + e.StackTrace + k.NEW_LINE + k.NEW_LINE + "region_code = '" + region_code + "'" + k.NEW_LINE;
	    }

	    return k.EMPTY;
    }

    private static bool Request_ems_health_state_pa_us_EmsregReportsEmsinstructorlistsearch_ExcelGeneratereport
      (
      CookieContainer cookie_container,
      string view_state,
      string event_validation,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Reports/EMSInstructorListSearch.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Reports/EMSInstructorListSearch.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=3nniwgkydva5klmzj4qfwdva; loginid=rekaufman; EMSREG=C90DF9762916149C733B4F536D25D83E247198F04399FC17422F2CAFF95E772FF9E809F85D2171CDF6EB45658845D72C6D8623EE0B8F00E6319640AB0267276F4E1936ADC022B95EBCE9714DBB9F7CDF96C48BF8E9C624AFDADB108EC30D1836FD394C83FCB90BAFAB9AFE3432F51C0409D3795EDBFB79996B3786FA1272A35F9E9DB152537F859CDDA933858514D4A1D6F14838332C070979EE632612A630B41DA59CC2198BD71E0330976AC8D890DFE4BF162194E570A5E60460F63957410E0A33DD15F3D2A3B39849F5FE2EACC3A08F1678D83D94360EAC525C5360E275EFFA3DDF03BC9C16ED742A37EDE0DB1A74");

		    request.Method = "POST";

		    string postString = @"__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation) + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AlstCounty=All&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AReportFormatControl1%3ArdlFormat=2&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AbtnGenerateReport=Generate+Report";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static string Request_ems_health_state_pa_us_EmsregReportsReportlist
      (
      CookieContainer cookie_container,
      string region_code,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Reports/ReportList.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/practitioner/home.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=gra35ekc4fbgr4yn5umh2oii; loginid=Rekaufman; EMSREG=A91AB0CB7AB6A388CBC1664AEFB890AA8816CBEF1748B3D6D49C1D92F6988868DA7ACAE842B48004CF52E8A9A91CD3A0582CBFDF8C87D1BAEE31B4894F684484455FE1FBA0CFDAA855AA3117CF2CBA903CAABFCE2759417082B51258F468F133082D8EE27AA3F45534ABAE3E553C522DF79BF19ED36EEC805F45A016EBB9B72FF0144E83DF6B6A8AF60F63FD9B0012744A1E62C36164DA09E965405523E79B0A0ABEDEAAA2267E88F86F7D8BCE167CC2381F138948BC25B53589445951D7CB344A95F6DE4A9B9793EC89779E5C70E076F5957E0DF3EFAF9BB0E34828207FCA9E449E0FFED42543F03E198D16131C9B67");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Message + k.NEW_LINE + e.StackTrace + k.NEW_LINE + k.NEW_LINE + "region_code = '" + region_code + "'" + k.NEW_LINE;
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message + k.NEW_LINE + e.StackTrace + k.NEW_LINE + k.NEW_LINE + "region_code = '" + region_code + "'" + k.NEW_LINE;;
	    }

	    return k.EMPTY;
    }

    private static bool Request_ems_health_state_pa_us_emsregPractitionerSearch
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Practitioner/Search.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/practitioner/home.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=btnrbmjo5ihgflf00aqbxlyl; loginid=rekaufman; EMSREG=B1BF1B5210B9C7326C2C0D2B2C566A56A406A7E2CD432CC141C424A6E197A883CF957FE0B233FC685E6150505D2FC3941597E7605CAAA778960081568FB39AA636C60BD0390BE888BBC8098B1A5108E444BA03748AC45D01E5EC1A698A9C3784D6E373BCA46BDC8421DD7E6F4A9CFAAFE892663C71D1707412D958B0CBD158AA2BF625B630BAD5F562BA2D648F07A26220AC497369FDACCC6AFD9B566C2F8DC26C938E9467FC39C409C32FC420F6A0E7250F1F69E2C78A89827B84FACCB5E398D376C7C929D4726DFB8392D2A5D7825CB19C0E27D0A66C62DE7DB0698317218CB9C4AC63A6E29AF370FE9AA10F5D260C");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_EmsregPractitionerSearchresults_1000recordsperpage
      (
      CookieContainer cookie_container,
      string view_state,
      string event_validation,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Practitioner/SearchResults.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Practitioner/Search.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=btnrbmjo5ihgflf00aqbxlyl; loginid=rekaufman; EMSREG=C6E92B9D3CBD2E228B7028CD94B52A6436B9E5EC4EFAF747CDBED5FA2E8498C03A68B334239783CF452EC81BC18E4987FCB5AB36E5539428ACA5C5156A8DB2199E42BA163632E976E84E6A34F9A987D68F33C5C40FE0C5900C8A97433945851B50C07A4AC3BC02B95E6F0ADDD44E28548E54482E26C54088CBFC8CB0045301A6668D7084091BC0A497532FC06C76FC1A568D9345F9D683AD4AE4355DD8ADEB1C9EA68A9260D3130CAA52934632D2C344F2B39A4F3C8717947D60A794A79CE1FA271EC4C1EB452334D806FA38E850F8C9E6E36A7BA3F9B407DE31748591C88FE412B31C2677ED67676599135B45CA6297");

		    request.Method = "POST";

		    string postString = @"__EVENTTARGET=_ctl0%24_ctl0%24SessionLinkBar%24Content%24ddlPageSize&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation) + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlLabelOutputFormat=Avery5161&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AhdnURL=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlPageSize=1000";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static string Request_ems_health_state_pa_us_EmsregPractitionerSearchresults_Next
      (
      CookieContainer cookie_container,
      string view_state,
      string event_validation,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Practitioner/SearchResults.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Practitioner/SearchResults.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=uba1m11szlslr032q0ezipu5; loginid=rekaufman; EMSREG=0783FC16E1C8604CE576855A7756621CDAD8E0D16C47B10DB2325A72EEDE0DF61096302540CB48EB863D9ED97C2E645F3E733D2F7CB8A16237735A0753B2102818AAFA65A5FCF027D2982B7AE52AE50E77A4A2E29F7174F45E2CC76A2D6C805925AA473DC68EC2DD8097E738FE8CE4C2EF3F60A6E0B1CEEE7303940E6C9D0D4C9B957BD046F5D0920A8860DDC2CE2876C92A803481861CA47A9FFCDA474C154E47C6E586E88301D61CDF9E1E1457A4DB64FDA186B64F08424C6871BF5B7FD7B445AB75BCC29D5573B2FCC4BE5E01C5BDAD4A780A472BCCA8F8F007F8819E3717EE22A53D73157A089D631B9B44D81E5E");
        //
        // The following line prevents "The operation has timed out" errors in later calls to StreamReader.ReadToEnd() via Class_ss.ConsumedStreamOf().
        //
        request.KeepAlive = false;

		    request.Method = "POST";

		    string postString = @"__EVENTTARGET=_ctl0%24_ctl0%24SessionLinkBar%24Content%24lbtnNext&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation) + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlLabelOutputFormat=Avery5161&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AhdnURL=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlPageSize=1000";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Message + k.NEW_LINE + e.StackTrace;
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message + k.NEW_LINE + e.StackTrace;
	    }

	    return k.EMPTY;
    }

    private static bool Request_ems_health_state_pa_us_EmsregReportsAvailablececlasseslistsearch
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Reports/AvailableCEClassesListSearch.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Reports/ReportList.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=gra35ekc4fbgr4yn5umh2oii; loginid=Rekaufman; EMSREG=A91AB0CB7AB6A388CBC1664AEFB890AA8816CBEF1748B3D6D49C1D92F6988868DA7ACAE842B48004CF52E8A9A91CD3A0582CBFDF8C87D1BAEE31B4894F684484455FE1FBA0CFDAA855AA3117CF2CBA903CAABFCE2759417082B51258F468F133082D8EE27AA3F45534ABAE3E553C522DF79BF19ED36EEC805F45A016EBB9B72FF0144E83DF6B6A8AF60F63FD9B0012744A1E62C36164DA09E965405523E79B0A0ABEDEAAA2267E88F86F7D8BCE167CC2381F138948BC25B53589445951D7CB344A95F6DE4A9B9793EC89779E5C70E076F5957E0DF3EFAF9BB0E34828207FCA9E449E0FFED42543F03E198D16131C9B67");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_EmsregPractitionerSearch_PractitionerstatusActiveSuspendedExpiredProbation_Submit
      (
      CookieContainer cookie_container,
      string view_state,
      string event_validation,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Practitioner/Search.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Practitioner/Search.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=btnrbmjo5ihgflf00aqbxlyl; loginid=rekaufman; EMSREG=B1BF1B5210B9C7326C2C0D2B2C566A56A406A7E2CD432CC141C424A6E197A883CF957FE0B233FC685E6150505D2FC3941597E7605CAAA778960081568FB39AA636C60BD0390BE888BBC8098B1A5108E444BA03748AC45D01E5EC1A698A9C3784D6E373BCA46BDC8421DD7E6F4A9CFAAFE892663C71D1707412D958B0CBD158AA2BF625B630BAD5F562BA2D648F07A26220AC497369FDACCC6AFD9B566C2F8DC26C938E9467FC39C409C32FC420F6A0E7250F1F69E2C78A89827B84FACCB5E398D376C7C929D4726DFB8392D2A5D7825CB19C0E27D0A66C62DE7DB0698317218CB9C4AC63A6E29AF370FE9AA10F5D260C");

		    request.Method = "POST";

		    string postString = @"__EVENTTARGET=&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation) + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxLastName=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxFirstName=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlGender=0&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlBirthdateOperator=%3E&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxBirthDateStart=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxBirthDateEnd=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxCertificationNumber=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AlbxPractitionerStatus=1&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AlbxPractitionerStatus=4&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AlbxPractitionerStatus=6&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AlbxPractitionerStatus=9&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlCertificationDateOperator=%3E&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxCertificationDate=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlExpirationDateOperator=%3E&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxExpirationDate=&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlRegion=-1&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AbtnSubmit=Submit";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_EmsregReportsAvailablececlasseslistsearch_From
      (
      CookieContainer cookie_container,
      string view_state,
      string event_validation,
      string date_from,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/emsreg/Reports/AvailableCEClassesListSearch.aspx");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/emsreg/Reports/AvailableCEClassesListSearch.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=gra35ekc4fbgr4yn5umh2oii; loginid=Rekaufman; EMSREG=4704B228D3FD0BE7891F4D9E004D4B0C01479A4F7ECABE552DBFA33BD000CD98A02F9006BC7346ED4C01BBB33971EBF624EAC2F2EEF7B7B51C8A1A8277F571873D68F7C8737E88744502676CCE7B48054AEF794125C22CF09C09A0DBF5748571226E3257FAB6C806DE42238B4CF164E92164B32555DC4FE2EF3F0046F2F58B1651FF0FB897AAFB97D1677A543A87C9A0ED61D4A8F443CE1AA6F2D6DCFDC51CE8F53E939F3C967938B134B3F1D9936A3425E46EFAC1CBDDC6AD66F6625CB91B4152CE7213B980F91BEC0F1AFC2406DFB96D5602571BA911A5E48F479B1F201D62BDE149B176BA064DA34FCB58F05E47B2");

		    request.Method = "POST";

		    string postString = @"__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation) + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3ADateRangeControl1%3AtbxDateFrom=" + HttpUtility.UrlEncode(date_from) + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3ADateRangeControl1%3AtbxDateTo=12%2F31%2F9999&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AReportFormat%3ArdlFormat=2&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AbtnGenerateReport=Generate+Report";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedClassreg_Classid
      (
      CookieContainer cookie_container,
      string class_id,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ClassReg.asp?ClassID=" + class_id);
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ClassSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=v2254ib3hqii4mn4rjusga1w; ConEd_EMSOUserID=40491; ASPSESSIONIDASSCBSDR=NJGJEOADMPMJGCDOMGJELBJD");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedClassreg_ClasswascanceledClassmaintenance
      (
      CookieContainer cookie_container,
      string class_id,
      string created_by,
      string date_created,
      string date_last_edited,
      string date_submitted_to_region,
      string document_status,
      string last_edited_by,
      string region_council_num,
      string sponsor_id,
      string course_id,
      string initial_value_approved,
      string initial_value_disapproval_reason_id,
      string debug_session_emso_user_id,
      string debug_session_user_role,
      string debug_session_sponsor_id,
      string debug_sponsor_info_editable,
      string debug_region_info_editable,
      string sponsor_name,
      string sponsor_number,
      string training_ins_accred_num,
      string sponsor_county,
      string course_title,
      string not_valid_after_date,
      string course_number,
      string location,
      string location_of_registration,
      string location_address_1,
      string location_address_2,
      string location_city,
      string location_state,
      string location_zip,
      string location_zip_plus_4,
      string county_code,
      string county_name,
      string regional_council_name,
      string location_phone,
      string location_email,
      string public_contact_name,
      string public_contact_email,
      string public_contact_phone,
      string public_contact_website,
      string public_contact_notes,
      string student_cost,
      string total_class_hours,
      string total_class_hours_chk,
      string length_of_course_in_hours,
      string tuition_includes,
      string closed,
      string estimated_students,
      string start_date_time,
      string start_date_time_chk,
      string start_time,
      string end_date_time,
      string end_date_time_chk,
      string end_time,
      string final_registration_date,
      string instructors,
      string instructor_qualifications,
      string class_coordinator,
      string primary_text,
      string college_credit_awarded,
      string held_on_sun,
      string held_on_mon,
      string held_on_tue,
      string held_on_wed,
      string held_on_thu,
      string held_on_fri,
      string held_on_sat,
      string other_dates_and_times,
      string date_received_by_region,
      string ret_to_applicant_comment,
      string date_sponsor_notified,
      string approved,
      string class_number,
      string date_registration_sent_to_state,
      string date_cards_sent_to_sponsor,
      string date_materials_to_be_returned,
      string disapproval_reason_id,
      string region_comments,
      string practical_exam_date,
      string practical_exam_time,
      string written_exam_date,
      string written_exam_time,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ClassReg.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ClassReg.asp?ClassID=" + class_id;
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASP.NET_SessionId=htmiojbhdxbtnsxptnp4o2pj; ConEd_EMSOUserID=40491; ASPSESSIONIDCSRBBQBT=JJOFNIIAOBLKPDHCJBFAOBBK");

		    request.Method = "POST";

		    string postString = @"ClassID=" + HttpUtility.UrlEncode(class_id)
        + "&CreatedBy=" + HttpUtility.UrlEncode(created_by)
        + "&DateCreated=" + HttpUtility.UrlEncode(date_created)
        + "&DateLastEdited=" + HttpUtility.UrlEncode(date_last_edited)
        + "&DateSubmittedToRegion=" + HttpUtility.UrlEncode(date_submitted_to_region)
        + "&DocumentStatus=" + HttpUtility.UrlEncode(document_status)
        + "&LastEditedBy=" + HttpUtility.UrlEncode(last_edited_by)
        + "&RegionCouncilNum=" + HttpUtility.UrlEncode(region_council_num)
        + "&SponsorID=" + HttpUtility.UrlEncode(sponsor_id)
        + "&CourseID=" + HttpUtility.UrlEncode(course_id)
        + "&InitialValue_Approved=" + HttpUtility.UrlEncode(initial_value_approved)
        + "&InitialValue_DisapprovalReasonID=" + HttpUtility.UrlEncode(initial_value_disapproval_reason_id)
        + "&debug_session_emsoUserid=" + HttpUtility.UrlEncode(debug_session_emso_user_id)
        + "&debug_session_userRole=" + HttpUtility.UrlEncode(debug_session_user_role)
        + "&debug_session_SponsorID=" + HttpUtility.UrlEncode(debug_session_sponsor_id)
        + "&debug_SponsorInfoEditable=" + HttpUtility.UrlEncode(debug_sponsor_info_editable)
        + "&debug_RegionInfoEditable=" + HttpUtility.UrlEncode(debug_region_info_editable)
        + "&cmdMove=" + HttpUtility.UrlEncode("Class Maintenance")
        + "&SponsorName=" + HttpUtility.UrlEncode(sponsor_name)
        + "&SponsorNumber=" + HttpUtility.UrlEncode(sponsor_number)
        + "&TrainingInsAccredNum=" + HttpUtility.UrlEncode(training_ins_accred_num)
        + "&SponsorCounty=" + HttpUtility.UrlEncode(sponsor_county)
        + "&CourseTitle=" + HttpUtility.UrlEncode(course_title)
        + "&NotValidAfterDate=" + HttpUtility.UrlEncode(not_valid_after_date)
        + "&CourseNumber=" + HttpUtility.UrlEncode(course_number)
        + "&Location=" + HttpUtility.UrlEncode(location)
        + "&LocationOfRegistration=" + HttpUtility.UrlEncode(location_of_registration)
        + "&LocationAddress1=" + HttpUtility.UrlEncode(location_address_1)
        + "&LocationAddress2=" + HttpUtility.UrlEncode(location_address_2)
        + "&LocationCity=" + HttpUtility.UrlEncode(location_city)
        + "&LocationState=" + HttpUtility.UrlEncode(location_state)
        + "&LocationZIP=" + HttpUtility.UrlEncode(location_zip)
        + "&LocationZipPlus4=" + HttpUtility.UrlEncode(location_zip_plus_4)
        + "&ClassCountyCode=" + HttpUtility.UrlEncode(county_code)
        + "&CountyCode=" + HttpUtility.UrlEncode(county_code)
        + "&CountyName=" + HttpUtility.UrlEncode(county_name)
        + "&RegionalCouncilName=" + HttpUtility.UrlEncode(regional_council_name)
        + "&LocationPhone=" + HttpUtility.UrlEncode(location_phone)
        + "&LocationEmail=" + HttpUtility.UrlEncode(location_email)
        + "&PublicContactName=" + HttpUtility.UrlEncode(public_contact_name)
        + "&PublicContactEmail=" + HttpUtility.UrlEncode(public_contact_email)
        + "&PublicContactPhone=" + HttpUtility.UrlEncode(public_contact_phone)
        + "&PublicContactWebsite=" + HttpUtility.UrlEncode(public_contact_website)
        + "&PublicContactNotes=" + HttpUtility.UrlEncode(public_contact_notes)
        + "&StudentCost=" + HttpUtility.UrlEncode(student_cost)
        + "&TotalClassHours=" + HttpUtility.UrlEncode(total_class_hours)
        + "&TotalClassHoursChk=" + HttpUtility.UrlEncode(total_class_hours_chk)
        + "&LengthOfCourseInHours=" + HttpUtility.UrlEncode(length_of_course_in_hours)
        + "&TuitionIncludes=" + HttpUtility.UrlEncode(tuition_includes)
        + "&Closed=" + HttpUtility.UrlEncode(closed)
        + "&EstimatedStudents=" + HttpUtility.UrlEncode(estimated_students)
        + "&StartDateTime=" + HttpUtility.UrlEncode(start_date_time)
        + "&StartDateTimeChk=" + HttpUtility.UrlEncode(start_date_time_chk)
        + "&StartTime=" + HttpUtility.UrlEncode(start_time)
        + "&EndDateTime=" + HttpUtility.UrlEncode(end_date_time)
        + "&EndDateTimeChk=" + HttpUtility.UrlEncode(end_date_time_chk)
        + "&EndTime=" + HttpUtility.UrlEncode(end_time)
        + "&FinalRegistrationDate=" + HttpUtility.UrlEncode(final_registration_date)
        + "&Instructors=" + HttpUtility.UrlEncode(instructors)
        + "&InstructorQualifications=" + HttpUtility.UrlEncode(instructor_qualifications)
        + "&ClassCoordinator=" + HttpUtility.UrlEncode(class_coordinator)
        + "&PrimaryText=" + HttpUtility.UrlEncode(primary_text)
        + "&CollegeCreditAwarded=" + HttpUtility.UrlEncode(college_credit_awarded)
        + (held_on_sun.Length > 0 ? "&HeldOnSun=" + HttpUtility.UrlEncode(held_on_sun) : k.EMPTY)
        + (held_on_mon.Length > 0 ? "&HeldOnMon=" + HttpUtility.UrlEncode(held_on_mon) : k.EMPTY)
        + (held_on_tue.Length > 0 ? "&HeldOnTue=" + HttpUtility.UrlEncode(held_on_tue) : k.EMPTY)
        + (held_on_wed.Length > 0 ? "&HeldOnWed=" + HttpUtility.UrlEncode(held_on_wed) : k.EMPTY)
        + (held_on_thu.Length > 0 ? "&HeldOnThu=" + HttpUtility.UrlEncode(held_on_thu) : k.EMPTY)
        + (held_on_fri.Length > 0 ? "&HeldOnFri=" + HttpUtility.UrlEncode(held_on_fri) : k.EMPTY)
        + (held_on_sat.Length > 0 ? "&HeldOnSat=" + HttpUtility.UrlEncode(held_on_sat) : k.EMPTY)
        + "&OtherDatesAndTimes=" + HttpUtility.UrlEncode(other_dates_and_times)
        + "&DateReceivedByRegion=" + HttpUtility.UrlEncode(date_received_by_region)
        + "&RetToApplicantComment=" + HttpUtility.UrlEncode(ret_to_applicant_comment)
        + "&DateSponsorNotified=" + HttpUtility.UrlEncode(date_sponsor_notified)
        + "&Approved=" + HttpUtility.UrlEncode(approved)
        + "&ClassNumber=" + HttpUtility.UrlEncode(class_number)
        + "&DateRegistrationSentToState=" + HttpUtility.UrlEncode(date_registration_sent_to_state)
        + "&DateCardsSentToSponsor=" + HttpUtility.UrlEncode(date_cards_sent_to_sponsor)
        + "&DateMaterialsToBeReturned=" + HttpUtility.UrlEncode(date_materials_to_be_returned)
        + "&DisapprovalReasonID=" + HttpUtility.UrlEncode(disapproval_reason_id)
        + "&RegionComments=" + HttpUtility.UrlEncode(region_comments + (region_comments.Length > 0 ? " . . . " : k.EMPTY) + "=Class CANCELED via ConEdLink at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "=")
        + "&PracticalExamDate=" + HttpUtility.UrlEncode(practical_exam_date)
        + "&PracticalExamTime=" + HttpUtility.UrlEncode(practical_exam_time)
        + "&WrittenExamDate=" + HttpUtility.UrlEncode(written_exam_date)
        + "&WrittenExamTime=" + HttpUtility.UrlEncode(written_exam_time)
        + "&ClassFinalStatus=CANCELED"
        ;
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedClasssearch
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ClassSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/MainMenu.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=hymy3yujgf1gaopjfodpcqfn; ConEd_EMSOUserID=345; ASPSESSIONIDSCCDSBCQ=JDKNGNNCEFNLKBKABEAGCCPJ; ASPSESSIONIDQCBBRDCQ=MFMNIHOCGKDFKAECCINFODBN; EMS_EMSOUserID=KALipscomb; EMS_EMSOType=QRS; ASPSESSIONIDSCDBSADR=BAMDPJOCCGMIMJLGHPPCFEIG; ASPSESSIONIDQCDARDDQ=OABNCNOCAOGCDDDIGLEBHGFH; ASPSESSIONIDSABBRDDR=AKIJMAPCAGNFPBAIGBJEHGJN");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedClasssearch_Coned_Filedelimited_Searchnow
      (
      CookieContainer cookie_container,
      string date_from,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ClassSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ClassSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=drkxrxkrje0i1irpscgw35g3; ConEd_EMSOUserID=345; ASPSESSIONIDSCDATADQ=NLGPKEICGOFDPEKCGMIBHCHH");

		    request.Method = "POST";

		    string postString = @"SearchMode=&cmdMove=Search+Now&ClassType=CONED&CourseNumber=&CourseTitle=&CourseCode=&StartDate_Low=" + HttpUtility.UrlEncode(date_from) + "&StartDate_High=&ClassCountyCode=&RegionCouncilNum=&ClassNumber=&IncludeDisapprovedClasses=0&OutputFormat=FileDelimited";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedClasssearch_Coned_Searchnow
      (
      CookieContainer cookie_container,
      string date_from,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ClassSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ClassSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=drkxrxkrje0i1irpscgw35g3; ConEd_EMSOUserID=345; ASPSESSIONIDSCDATADQ=NLGPKEICGOFDPEKCGMIBHCHH");

		    request.Method = "POST";

		    string postString = @"SearchMode=&cmdMove=Search+Now&ClassType=CONED&CourseNumber=&CourseTitle=&CourseCode=&StartDate_Low=" + HttpUtility.UrlEncode(date_from) + "&StartDate_High=&ClassCountyCode=&RegionCouncilNum=&ClassNumber=&IncludeDisapprovedClasses=0&OutputFormat=Screen";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedExportClasssearchtxt
      (
      CookieContainer cookie_container,
      string user_id,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/Export/ClassSearch_" + user_id + ".txt");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "text/html, application/xhtml+xml, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ClassSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=eftwud551xujf0rdibjcz4re");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedExportSponsorsearchtxt
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/Export/SponsorSearch_345.txt");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/SponsorSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=jcydtlzcjxtbyawd4zjsbg3w; ConEd_EMSOUserID=345; ASPSESSIONIDQCBBQCCR=HCGDCOMCLNEJMAGGPPNHCAFM");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedListclassnumbers
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ListClassNumbers.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "*/*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ClassSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=hymy3yujgf1gaopjfodpcqfn; ConEd_EMSOUserID=345; ASPSESSIONIDSCCDSBCQ=JDKNGNNCEFNLKBKABEAGCCPJ; ASPSESSIONIDQCBBRDCQ=MFMNIHOCGKDFKAECCINFODBN; EMS_EMSOUserID=KALipscomb; EMS_EMSOType=QRS; ASPSESSIONIDSCDBSADR=BAMDPJOCCGMIMJLGHPPCFEIG; ASPSESSIONIDQCDARDDQ=OABNCNOCAOGCDDDIGLEBHGFH; ASPSESSIONIDSABBRDDR=AKIJMAPCAGNFPBAIGBJEHGJN");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedListClassNumbers_Coned_2011_999999_ShowNumbers
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/ListClassNumbers.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/ListClassNumbers.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=hymy3yujgf1gaopjfodpcqfn; ConEd_EMSOUserID=345; ASPSESSIONIDSCCDSBCQ=JDKNGNNCEFNLKBKABEAGCCPJ; ASPSESSIONIDQCBBRDCQ=MFMNIHOCGKDFKAECCINFODBN; EMS_EMSOUserID=KALipscomb; EMS_EMSOType=QRS; ASPSESSIONIDSCDBSADR=BAMDPJOCCGMIMJLGHPPCFEIG; ASPSESSIONIDQCDARDDQ=OABNCNOCAOGCDDDIGLEBHGFH; ASPSESSIONIDSABBRDDR=AKIJMAPCAGNFPBAIGBJEHGJN");

		    request.Method = "POST";

		    string postString = @"ClassType=CONED&ClassYear=2011&HowMany=999999&cmdMove=Show+Numbers";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_Coned_Mainmenu_Logout
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/MainMenu.asp?cmdMove=logout");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/MainMenu.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=5rk44se5bzjgtspmizipbpwp; ConEd_EMSOUserID=345; ASPSESSIONIDQCCATACQ=IPDLADIDOFONKEFNONAJIAEK");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedSponsorsearch
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/SponsorSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/MainMenu.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=jcydtlzcjxtbyawd4zjsbg3w; ConEd_EMSOUserID=345; ASPSESSIONIDQCBBQCCR=HCGDCOMCLNEJMAGGPPNHCAFM");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedSponsorsearch_Filedelimited_Searchnow
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/SponsorSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/SponsorSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=jcydtlzcjxtbyawd4zjsbg3w; ConEd_EMSOUserID=345; ASPSESSIONIDQCBBQCCR=HCGDCOMCLNEJMAGGPPNHCAFM");

		    request.Method = "POST";

		    string postString = @"cmdMove=Search+Now&name=&number=&Region=&CountyCode=&ExpirationDate_Low=&ExpirationDate_High=&Status=&ApplicationDate_Low=&ApplicationDate_High=&ProcessDate_Low=&ProcessDate_High=&OutputFormat=FileDelimited";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_ConedSponsorsearch_StatusSearchnow
      (
      CookieContainer cookie_container,
      string status,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/SponsorSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/SponsorSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=106443904.163291999.1326547990.1326547990.1326547990.1; __utmz=106443904.1326547990.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); ASPSESSIONIDACBATCSR=GAFPJGNBECCHBGLEJILLKFCL");

		    request.Method = "POST";

		    string postString = @"cmdMove=Search+Now&name=&number=&Region=&CountyCode=&ExpirationDate_Low=&ExpirationDate_High=&Status=" + status + "&ApplicationDate_Low=&ApplicationDate_High=&ProcessDate_Low=&ProcessDate_High=";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedUsersearch
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/UserSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/MainMenu.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=5gecqtoycvwyikzjp3kz0mtz; ConEd_EMSOUserID=345; ASPSESSIONIDQCACSBCQ=NJAJLPMAEMPCLKMBIIEGHPLA");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_ConedUsersearch_RegionalCouncilId_SearchNow
      (
      CookieContainer cookie_container,
      string regional_council_id,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/ConEd/UserSearch.asp");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

		    request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, */*";
		    request.Referer = "https://ems.health.state.pa.us/ConEd/UserSearch.asp";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=5gecqtoycvwyikzjp3kz0mtz; ConEd_EMSOUserID=345; ASPSESSIONIDQCACSBCQ=NJAJLPMAEMPCLKMBIIEGHPLA");

		    request.Method = "POST";

		    string postString = @"SponsorName=&SponsorID=&cmdMove=Search+Now&FirstName=&LastName=&CertNumber=&RegionalCouncilID=" + regional_council_id + "&CountyCode=";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    private static bool Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

        request.Timeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private static bool Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_ByCounty
      (
      CookieContainer cookie_container,
      string view_state,
      string view_state_generator,
      string event_validation,
      string county,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/registry/registry/activepractitioners.aspx");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		    request.Referer = "https://ems.health.state.pa.us/registry/registry/activepractitioners.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Add("DNT", @"1");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=wfl34zp4bwnt01mgli5w2lwo");

		    request.Method = "POST";
		    request.ServicePoint.Expect100Continue = false;

		    string body = @"__VIEWSTATE=" + HttpUtility.UrlEncode(view_state)
        + "&__VIEWSTATEGENERATOR=" + HttpUtility.UrlEncode(view_state_generator)
        + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation)
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlCertType=-1"
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxLName="
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxFName="
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxCertNum="
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlRegionalCouncil=-1"
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlCounty=" + county
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxCity="
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AddlState=-1"
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AtbxZip="
        + "&_ctl0%3A_ctl0%3ASessionLinkBar%3AContent%3AbtnSearch=Search";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    private static string Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_NextNumericalPage
      (
      CookieContainer cookie_container,
      string view_state,
      string view_state_generator,
      string event_validation,
      k.subtype<int> target_next_page_button_num,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		    request.Referer = "https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=qz4fif2xqgthlxvg34ndnghh");

		    request.Method = "POST";
		    request.ServicePoint.Expect100Continue = false;
        request.Timeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);
        request.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);
        request.KeepAlive = false;

	      string body = @"__EVENTTARGET=ctl00%24ctl00%24SessionLinkBar%24Content%24gvPractitionerSearchResults%24ctl103%24lbtnPage" + target_next_page_button_num.val.ToString()
        + "&__EVENTARGUMENT="
        + "&__LASTFOCUS="
        + "&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state)
        + "&__VIEWSTATEGENERATOR=" + HttpUtility.UrlEncode(view_state_generator)
        + "&__VIEWSTATEENCRYPTED="
        + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation)
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24gvPractitionerSearchResults%24ctl103%24ddlPerPage=100";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Status.ToString();
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message;
	    }

	    return k.EMPTY;
      }

    private static string Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Perpage100
      (
      CookieContainer cookie_container,
      string view_state,
      string view_state_generator,
      string event_validation,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		    request.Referer = "https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=qz4fif2xqgthlxvg34ndnghh");

		    request.Method = "POST";
		    request.ServicePoint.Expect100Continue = false;
        request.Timeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);
        request.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);

		    string body = @"__EVENTTARGET=ctl00%24ctl00%24SessionLinkBar%24Content%24gvPractitionerSearchResults%24ctl13%24ddlPerPage"
        + "&__EVENTARGUMENT="
        + "&__LASTFOCUS="
        + "&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state)
        + "&__VIEWSTATEGENERATOR=" + HttpUtility.UrlEncode(view_state_generator)
        + "&__VIEWSTATEENCRYPTED="
        + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation)
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24gvPractitionerSearchResults%24ctl13%24ddlPerPage=100";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Status.ToString();
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message;
	    }

	    return k.EMPTY;
      }

    private static string Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search
      (
      CookieContainer cookie_container,
      string view_state,
      string view_state_generator,
      string event_validation,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		    request.Referer = "https://ems.health.state.pa.us/Registry/Registry/ActivePractitioners.aspx";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"ASP.NET_SessionId=hifdcolbktrih0ybwyj4b3h3");

		    request.Method = "POST";
		    request.ServicePoint.Expect100Continue = false;
        request.Timeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);
        request.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search_timeout_milliseconds"]);

		    string body = @"__EVENTTARGET="
        + "&__EVENTARGUMENT="
        + "&__VIEWSTATE=" + HttpUtility.UrlEncode(view_state)
        + "&__VIEWSTATEGENERATOR=" + HttpUtility.UrlEncode(view_state_generator)
        + "&__VIEWSTATEENCRYPTED="
        + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(event_validation)
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24ddlCertification=0"
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtLastName="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24tbwmeLastName_ClientState="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtFistName="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24tbwmeFirstName_ClientState="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtCertNumber="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtCertNumber_MaskedEditExtender_ClientState="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24ddlRegion=0"
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24ddlCounty=0"
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtCity="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24ddlState=0"
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtZIP="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24txtZIP_MaskedEditExtender_ClientState="
        + "&ctl00%24ctl00%24SessionLinkBar%24Content%24btnSearch=Search";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return e.Status.ToString();
	    }
	    catch (Exception e)
	    {
		    if(response != null) response.Close();
		    return e.Message;
	    }

	    return k.EMPTY;
      }

    //--
    //
    #pragma warning restore CA1031 // Do not catch general exception types
    #pragma warning restore CA2234 // Pass system uri objects instead of strings
    //
    // END code generated initially by Fiddler extension "Request to Code"
    //
    //--

    internal class EmsInstructor
      {
      internal string practitioner_name = k.EMPTY;
      internal string certification_level = k.EMPTY;
      internal string certification_number = k.EMPTY;
      internal string instructor_address = k.EMPTY;
      internal string instructor_city_state = k.EMPTY;
      internal string home_phone = k.EMPTY;
      internal string work_phone = k.EMPTY;
      internal string instructor_expiration_date = k.EMPTY;
      internal string instructor_type = k.EMPTY;
      internal string county_code = k.EMPTY;
      internal string region_code = k.EMPTY;
      }
    public ArrayList EmsInstructorsList(string region_code)
      {
      var ems_instructors_list = new ArrayList();
      var cookie_container = new CookieContainer();
      //
      Login(region_code,cookie_container);
      //
      HttpWebResponse response;
      if (!Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_EmsregDefault(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregDefault() returned FALSE.");
        }
      response.Close();
      var request_ems_health_state_pa_us_emsregpractitionerhome = Request_ems_health_state_pa_us_EmsregPractitionerHome(cookie_container,out response);
      if (request_ems_health_state_pa_us_emsregpractitionerhome.Length > 0)
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregPractitionerHome() returned [" + request_ems_health_state_pa_us_emsregpractitionerhome + "]");
        }
      response.Close();
      var request_ems_health_state_pa_us_emsregreportsreportlist = Request_ems_health_state_pa_us_EmsregReportsReportlist(cookie_container,region_code,out response);
      if (request_ems_health_state_pa_us_emsregreportsreportlist.Length > 0)
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregReportsReportlist() returned [" + request_ems_health_state_pa_us_emsregreportsreportlist + "]");
        }
      response.Close();
      var request_ems_health_state_pa_us_emsregreportsemsinstructorlistsearch = Request_ems_health_state_pa_us_EmsregReportsEmsinstructorlistsearch(cookie_container,region_code,out response);
      if (request_ems_health_state_pa_us_emsregreportsemsinstructorlistsearch.Length > 0)
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregReportsEmsinstructorlistsearch() returned [" + request_ems_health_state_pa_us_emsregreportsemsinstructorlistsearch + "]");
        }
      var html_document = HtmlDocumentOf(ConsumedStreamOf(response));
      if (!Request_ems_health_state_pa_us_EmsregReportsEmsinstructorlistsearch_ExcelGeneratereport(cookie_container,ViewstateOf(html_document),EventValidationOf(html_document),out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregReportsEmsinstructorlistsearch_ExcelGeneratereport() returned FALSE.");
        }
      //
      var hn_target_table = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.SelectSingleNode("table"); 
      //
      var hnc_practitioner_name = hn_target_table.SelectNodes("tr/td[1]");
      var hnc_certification_level = hn_target_table.SelectNodes("tr/td[2]");
      var hnc_certification_number = hn_target_table.SelectNodes("tr/td[3]");
      var hnc_instructor_address = hn_target_table.SelectNodes("tr/td[4]");
      var hnc_instructor_city_state = hn_target_table.SelectNodes("tr/td[5]");
      var hnc_home_phone = hn_target_table.SelectNodes("tr/td[6]");
      var hnc_work_phone = hn_target_table.SelectNodes("tr/td[7]");
      var hnc_instructor_expiration_date = hn_target_table.SelectNodes("tr/td[8]");
      var hnc_instructor_type = hn_target_table.SelectNodes("tr/td[9]");
      var hnc_county_code = hn_target_table.SelectNodes("tr/td[10]");
      var hnc_region_code = hn_target_table.SelectNodes("tr/td[11]");
      //
      for (var i = new k.subtype<int>(1,hnc_practitioner_name.Count); i.val < i.LAST; i.val++)  //limits take into account non-data header
        {
        var ems_instructor = new EmsInstructor();
        ems_instructor.practitioner_name = k.Safe(hnc_practitioner_name[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME_CSV);
        ems_instructor.certification_level = k.Safe(hnc_certification_level[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
        ems_instructor.certification_number = k.Safe(hnc_certification_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        ems_instructor.instructor_address = k.Safe(hnc_instructor_address[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_STREET_ADDRESS);
        ems_instructor.instructor_city_state = k.Safe(hnc_instructor_city_state[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_STREET_ADDRESS); // safely captures alphas, nums, and spaces
        ems_instructor.home_phone = k.Safe(hnc_home_phone[i.val].InnerText.Trim(),k.safe_hint_type.PHONE_NUM);
        ems_instructor.work_phone = k.Safe(hnc_work_phone[i.val].InnerText.Trim(),k.safe_hint_type.PHONE_NUM);
        ems_instructor.instructor_expiration_date = k.Safe(hnc_instructor_expiration_date[i.val].InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        ems_instructor.instructor_type = k.Safe(hnc_instructor_type[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        ems_instructor.county_code = k.Safe(hnc_county_code[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_STREET_ADDRESS); // safely captures alphas, nums, and spaces
        ems_instructor.region_code = k.Safe(hnc_region_code[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        ems_instructors_list.Add(ems_instructor);
        }
      return ems_instructors_list;
      }

    internal void Login
      (
      string region_code,
      CookieContainer cookie_container
      )
      {
      HttpWebResponse response;
      if (!Request_ems_health_state_pa_us_Emsportal(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_Emsportal() returned FALSE.");
        }
      var html_document = HtmlDocumentOf(ConsumedStreamOf(response));
      if(!Request_ems_health_state_pa_us_Emsportal_Login
          (
          cookie_container,
          ViewstateOf(html_document),
          EventValidationOf(html_document),
          biz_regions.EmsportalUsernameOf(region_code),
          biz_regions.EmsportalPasswordOf(region_code),
          out response
          )
        )
        {
        throw new Exception("Request_ems_health_state_pa_us_Emsportal_Login() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_EmsportalApplicationlist(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationlist() returned FALSE.");
        }
      if (TitleOf(HtmlDocumentOf(ConsumedStreamOf(response))) != "EMS Login | Application List")
        {
        throw new Exception("Unexpected response from Request_ems_health_state_pa_us_EmsportalApplicationlist().");
        }
      }

    private static void ScrapeConedSponsors
      (
      string status,
      ArrayList teaching_entity_array_list,
      HttpWebResponse response
      )
      {
      var hn_target_table = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.SelectSingleNode("html/body/font/center[2]/center/table");
      //
      var hnc_name = hn_target_table.SelectNodes("tr/td[1]/a");
      var hnc_location_column = hn_target_table.SelectNodes("tr/td[2]");
      var hnc_county_name = hn_target_table.SelectNodes("tr/td[2]/table/tr[2]/td[2]");
      var hnc_teaching_entity_kind = hn_target_table.SelectNodes("tr/td[2]/table/tr[4]/td[1]");
      var hnc_sponsor_number = hn_target_table.SelectNodes("tr/td[2]/table/tr[4]/td[2]");
      var hnc_public_contact_name = hn_target_table.SelectNodes("tr/td[3]/table/tr[1]/td[2]");
      var hnc_public_contact_phone = hn_target_table.SelectNodes("tr/td[3]/table/tr[2]/td[2]");
      var hnc_public_contact_email = hn_target_table.SelectNodes("tr/td[3]/table/tr[3]/td[2]");
      var hnc_public_contact_website_cell = hn_target_table.SelectNodes("tr/td[3]/table/tr[4]/td[2]");
      var hnc_public_contact_notes = hn_target_table.SelectNodes("tr/td[3]/table/tr[5]/td[2]");
      //
      for (var i = new k.subtype<int>(0, hnc_name.Count); i.val < i.LAST; i.val++)
        {
        var county_name = k.Safe(hnc_county_name[i.val].InnerText.Trim(), k.safe_hint_type.ALPHA_WORDS);
        var teaching_entity_kind = k.Safe(hnc_teaching_entity_kind[i.val].InnerText,k.safe_hint_type.ALPHA);
        if (teaching_entity_kind == "Sponsor")
          {
          var teaching_entity = new TeachingEntity();
          var hn_name = hnc_name[i.val];
          var name_href = hn_name.Attributes["href"].Value;
          var name_href_index_of_ampersand = name_href.IndexOf("&");
          teaching_entity.id = k.Safe(name_href.Substring(0, name_href_index_of_ampersand), k.safe_hint_type.NUM);
          teaching_entity.region = k.Safe(name_href.Substring(name_href_index_of_ampersand), k.safe_hint_type.NUM);
          teaching_entity.name = k.Safe(hn_name.InnerText.Trim(), k.safe_hint_type.ORG_NAME);
          var hnc_location_column_children = hnc_location_column[i.val].ChildNodes;
          var hnc_location_column_children_count = hnc_location_column_children.Count;
          teaching_entity.address_1 = (hnc_location_column_children_count == 7 ? k.Safe(hnc_location_column_children[0].InnerText.Trim(), k.safe_hint_type.POSTAL_STREET_ADDRESS) : k.EMPTY);
          teaching_entity.address_2 = (hnc_location_column_children_count == 9 ? k.Safe(hnc_location_column_children[2].InnerText.Trim(), k.safe_hint_type.POSTAL_STREET_ADDRESS) : k.EMPTY);
          var city_state_zip = hnc_location_column_children[hnc_location_column_children_count - 5].InnerText.Replace("&nbsp;",k.SPACE).Trim();
          if (city_state_zip.Length > 0)
            {
            var city_state_zip_index_of_comma = city_state_zip.IndexOf(k.COMMA);
            if (city_state_zip_index_of_comma > -1)
              {
              teaching_entity.city = k.Safe(city_state_zip.Substring(0, city_state_zip_index_of_comma), k.safe_hint_type.POSTAL_CITY);
              teaching_entity.state = k.Safe(city_state_zip.Substring(city_state_zip_index_of_comma), k.safe_hint_type.ALPHA);
              teaching_entity.zip = k.Safe(city_state_zip.Substring(city_state_zip_index_of_comma), k.safe_hint_type.HYPHENATED_NUM);
              }
            }
          teaching_entity.county_name = county_name;
          teaching_entity.sponsor_number = k.Safe(hnc_sponsor_number[i.val].InnerText.Trim(), k.safe_hint_type.HYPHENATED_NUM);
          teaching_entity.public_contact_name = k.Safe(hnc_public_contact_name[i.val].InnerText.Trim(), k.safe_hint_type.HUMAN_NAME);
          teaching_entity.public_contact_phone = k.Safe(hnc_public_contact_phone[i.val].InnerText.Trim(), k.safe_hint_type.PHONE_NUM);
          var hn_public_contact_email = hnc_public_contact_email[i.val].SelectSingleNode("a");
          if (hn_public_contact_email != null)
            {
            teaching_entity.public_contact_email = k.Safe(hn_public_contact_email.InnerText.Trim(), k.safe_hint_type.EMAIL_ADDRESS);
            }
          var hn_public_contact_website = hnc_public_contact_website_cell[i.val].SelectSingleNode("a");
          if (hn_public_contact_website != null)
            {
            teaching_entity.public_contact_website = k.Safe(hn_public_contact_website.InnerText.Trim(), k.safe_hint_type.HTTP_TARGET);
            }
          teaching_entity.public_contact_notes = k.Safe(hnc_public_contact_notes[i.val].InnerText.Trim(), k.safe_hint_type.PUNCTUATED);
          teaching_entity.sponsor_status_description = status;
          teaching_entity_array_list.Add(teaching_entity);
          }
        }
      }

    internal class ConedOffering
      {
      internal string class_id_1 = k.EMPTY;
      internal string class_type = k.EMPTY;
      internal string course_id = k.EMPTY;
      internal string class_number = k.EMPTY;
      internal string course_code = k.EMPTY;
      internal string created_by = k.EMPTY;
      internal string date_created = k.EMPTY;
      internal string last_edited_by = k.EMPTY;
      internal string date_last_edited = k.EMPTY;
      internal string sponsor_id = k.EMPTY;
      internal string sponsor_number = k.EMPTY;
      internal string training_ins_accred_num = k.EMPTY;
      internal string document_status = k.EMPTY;
      internal string class_final_status = k.EMPTY;
      internal string course_number = k.EMPTY;
      internal string location = k.EMPTY;
      internal string student_cost = k.EMPTY;
      internal string tuition_includes = k.EMPTY;
      internal string closed = k.EMPTY;
      internal string estimated_students = k.EMPTY;
      internal string start_date_time = k.EMPTY;
      internal string end_date_time = k.EMPTY;
      internal string start_time = k.EMPTY;
      internal string end_time = k.EMPTY;
      internal string other_dates_and_times = k.EMPTY;
      internal string instructors = k.EMPTY;
      internal string instructor_qualifications = k.EMPTY;
      internal string verification_name = k.EMPTY;
      internal string contact_name = k.EMPTY;
      internal string contact_address_1 = k.EMPTY;
      internal string contact_address_2 = k.EMPTY;
      internal string contact_city = k.EMPTY;
      internal string contact_state = k.EMPTY;
      internal string contact_zip = k.EMPTY;
      internal string contact_daytime_phone = k.EMPTY;
      internal string contact_evening_phone = k.EMPTY;
      internal string contact_email = k.EMPTY;
      internal string public_contact_name = k.EMPTY;
      internal string public_contact_phone = k.EMPTY;
      internal string public_contact_email = k.EMPTY;
      internal string public_contact_website = k.EMPTY;
      internal string public_contact_notes = k.EMPTY;
      internal string date_submitted_to_region = k.EMPTY;
      internal string date_received_by_region = k.EMPTY;
      internal string date_sponsor_notified = k.EMPTY;
      internal string date_registration_sent_to_state = k.EMPTY;
      internal string date_cards_sent_to_sponsor = k.EMPTY;
      internal string date_materials_to_be_returned = k.EMPTY;
      internal string approved = k.EMPTY;
      internal string region_comments = k.EMPTY;
      internal string region_council_num = k.EMPTY;
      internal string class_county_code = k.EMPTY;
      internal string total_class_hours = k.EMPTY;
      internal string location_address_1 = k.EMPTY;
      internal string location_address_2 = k.EMPTY;
      internal string location_city = k.EMPTY;
      internal string location_state = k.EMPTY;
      internal string location_zip = k.EMPTY;
      internal string location_zip_plus_4 = k.EMPTY;
      internal string location_phone = k.EMPTY;
      internal string location_email = k.EMPTY;
      internal string location_of_registration = k.EMPTY;
      internal string primary_text = k.EMPTY;
      internal string additional_texts = k.EMPTY;
      internal string final_registration_date = k.EMPTY;
      internal string offered_as_college_credit = k.EMPTY;
      internal string practical_exam_date = k.EMPTY;
      internal string written_exam_date = k.EMPTY;
      internal string disapproval_reason_id = k.EMPTY;
      internal string date_final_paperwork_received = k.EMPTY;
      internal string signed_hard_copy = k.EMPTY;
      internal string created_by_first_name = k.EMPTY;
      internal string created_by_last_name = k.EMPTY;
      internal string class_disapproval_reason_description = k.EMPTY;
      internal string class_final_status_description = k.EMPTY;
      internal string sponsor_name = k.EMPTY;
      internal string courses_course_number = k.EMPTY;
      internal string course_title = k.EMPTY;
      internal string cert_course_code = k.EMPTY;
      internal string cert_course_description = k.EMPTY;
      internal string class_id_2 = k.EMPTY;
      //
      internal string fr_med_trauma_hours = k.EMPTY;
      internal string fr_other_hours = k.EMPTY;
      internal string emt_med_trauma_hours = k.EMPTY;
      internal string emt_other_hours = k.EMPTY;
      internal string emtp_med_trauma_hours = k.EMPTY;
      internal string emtp_other_hours = k.EMPTY;
      internal string phrn_med_trauma_hours = k.EMPTY;
      internal string phrn_other_hours = k.EMPTY;
      internal string length = k.EMPTY;
      internal string region_council_name = k.EMPTY;
      internal string class_county_name = k.EMPTY;
      //
      internal string class_start_date = k.EMPTY;
      internal string class_start_time = k.EMPTY;
      internal string class_end_date = k.EMPTY;
      internal string class_end_time = k.EMPTY;
      internal string total_ceus = k.EMPTY;
      internal string fr_ce_trauma = k.EMPTY;
      internal string fr_ce_other = k.EMPTY;
      internal string fr_ce_total = k.EMPTY;
      internal string emt_ce_trauma = k.EMPTY;
      internal string emt_ce_other = k.EMPTY;
      internal string emt_ce_total = k.EMPTY;
      internal string als_ce_total = k.EMPTY;
      internal string als_ce_trauma = k.EMPTY;
      internal string als_ce_other = k.EMPTY;
      internal string tuition = k.EMPTY;
      internal string instructor_name = k.EMPTY;
      internal string contact_name_1 = k.EMPTY;
      internal string class_location = k.EMPTY;
      internal string class_city_state = k.EMPTY;
      internal string class_region_code = k.EMPTY;
      }
    internal ArrayList AvailableConedClassesList(string region_code)
      {
      var available_coned_classes_list = new ArrayList();
      var cookie_container = new CookieContainer();
      //
      Login(region_code,cookie_container);
      //
      HttpWebResponse response;
      if (!Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_EmsregDefault(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregDefault() returned FALSE.");
        }
      response.Close();
      var request_ems_health_state_pa_us_emsregpractitionerhome = Request_ems_health_state_pa_us_EmsregPractitionerHome(cookie_container,out response);
      if (request_ems_health_state_pa_us_emsregpractitionerhome.Length > 0)
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregPractitionerHome() returned [" + request_ems_health_state_pa_us_emsregpractitionerhome + "]");
        }
      response.Close();
      var request_ems_health_state_pa_us_emsregreportsreportlist = Request_ems_health_state_pa_us_EmsregReportsReportlist(cookie_container,region_code,out response);
      if (request_ems_health_state_pa_us_emsregreportsreportlist.Length > 0)
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregReportsReportlist() returned [" + request_ems_health_state_pa_us_emsregreportsreportlist + "]");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_EmsregReportsAvailablececlasseslistsearch(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregReportsAvailablececlasseslistsearch() returned FALSE.");
        }
      var html_document = HtmlDocumentOf(ConsumedStreamOf(response));
      if(!Request_ems_health_state_pa_us_EmsregReportsAvailablececlasseslistsearch_From
          (
          cookie_container,
          ViewstateOf(html_document),
          EventValidationOf(html_document),
          DateTime.Today.AddMonths(-2).ToString(),
          out response
          )
        )
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsregReportsAvailablececlasseslistsearch_FromToFormatGenerateReport() returned FALSE.");
        }
      var stream_string = ConsumedStreamOf(response).Replace("&nbsp;",k.EMPTY);
      var hn_target_table = HtmlDocumentOf(stream_string).DocumentNode.SelectSingleNode("table");
      //
      var hnc_course_number = hn_target_table.SelectNodes("tr/td[1]");
      var hnc_course_title = hn_target_table.SelectNodes("tr/td[2]");
      var hnc_class_number = hn_target_table.SelectNodes("tr/td[3]");
      var hnc_class_start_date = hn_target_table.SelectNodes("tr/td[4]");
      var hnc_class_start_time = hn_target_table.SelectNodes("tr/td[5]");
      var hnc_class_end_date = hn_target_table.SelectNodes("tr/td[6]");
      var hnc_class_end_time = hn_target_table.SelectNodes("tr/td[7]");
      var hnc_total_ceus = hn_target_table.SelectNodes("tr/td[8]");
      var hnc_fr_ce_trauma = hn_target_table.SelectNodes("tr/td[9]");
      var hnc_fr_ce_other = hn_target_table.SelectNodes("tr/td[10]");
      var hnc_fr_ce_total = hn_target_table.SelectNodes("tr/td[11]");
      var hnc_emt_ce_trauma = hn_target_table.SelectNodes("tr/td[12]");
      var hnc_emt_ce_other = hn_target_table.SelectNodes("tr/td[13]");
      var hnc_emt_ce_total = hn_target_table.SelectNodes("tr/td[14]");
      //
      // Values that EMSRS reports in the following columns are unreliable.  They appear to be double the appropriate values.
      //
      //var hnc_als_ce_total = hn_target_table.SelectNodes("tr/td[15]");
      //var hnc_als_ce_other = hn_target_table.SelectNodes("tr/td[16]");
      //var hnc_als_ce_trauma = hn_target_table.SelectNodes("tr/td[17]");
      //
      var hnc_tuition = hn_target_table.SelectNodes("tr/td[18]");
      var hnc_sponsor_name = hn_target_table.SelectNodes("tr/td[19]");
      var hnc_sponsor_number = hn_target_table.SelectNodes("tr/td[20]");
      var hnc_instructor_name = hn_target_table.SelectNodes("tr/td[21]");
      var hnc_contact_name = hn_target_table.SelectNodes("tr/td[22]");
      var hnc_contact_name_1 = hn_target_table.SelectNodes("tr/td[23]");
      var hnc_location = hn_target_table.SelectNodes("tr/td[24]");
      var hnc_class_location = hn_target_table.SelectNodes("tr/td[25]");
      var hnc_class_city_state = hn_target_table.SelectNodes("tr/td[26]");
      var hnc_class_county_code = hn_target_table.SelectNodes("tr/td[27]");
      var hnc_class_region_code = hn_target_table.SelectNodes("tr/td[28]");
      //
      for (var i = new k.subtype<int>(1,hnc_course_number.Count); i.val < i.LAST; i.val++)
        {
        var coned_offering = new ConedOffering();
        coned_offering.course_number = k.Safe(hnc_course_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        coned_offering.course_title = k.Safe(hnc_course_title[i.val].InnerText.Trim(),k.safe_hint_type.PUNCTUATED);
        coned_offering.class_number = k.Safe(hnc_class_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        coned_offering.class_start_date = k.Safe(hnc_class_start_date[i.val].InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        coned_offering.class_start_time = k.Safe(hnc_class_start_time[i.val].InnerText.Trim(),k.safe_hint_type.PUNCTUATED);
        coned_offering.class_end_date = k.Safe(hnc_class_end_date[i.val].InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        coned_offering.class_end_time = k.Safe(hnc_class_end_time[i.val].InnerText.Trim(),k.safe_hint_type.PUNCTUATED);
        coned_offering.total_ceus = k.Safe(hnc_total_ceus[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.fr_ce_trauma = k.Safe(hnc_fr_ce_trauma[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.fr_ce_other = k.Safe(hnc_fr_ce_other[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.fr_ce_total = k.Safe(hnc_fr_ce_total[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emt_ce_trauma = k.Safe(hnc_emt_ce_trauma[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emt_ce_other = k.Safe(hnc_emt_ce_other[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emt_ce_total = k.Safe(hnc_emt_ce_total[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        //coned_offering.als_ce_total = k.Safe(hnc_als_ce_total[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        //coned_offering.als_ce_trauma = k.Safe(hnc_als_ce_trauma[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        //coned_offering.als_ce_other = k.Safe(hnc_als_ce_other[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.tuition = k.Safe(hnc_tuition[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.sponsor_name = k.Safe(hnc_sponsor_name[i.val].InnerText.Trim(),k.safe_hint_type.ORG_NAME);
        coned_offering.sponsor_number = k.Safe(hnc_sponsor_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        coned_offering.instructor_name = k.Safe(hnc_instructor_name[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME_CSV);
        coned_offering.contact_name = k.Safe(hnc_contact_name[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME);
        coned_offering.contact_name_1 = k.Safe(hnc_contact_name_1[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME);
        coned_offering.location = k.Safe(hnc_location[i.val].InnerText.Trim(),k.safe_hint_type.ORG_NAME);
        coned_offering.class_location = k.Safe(hnc_class_location[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_STREET_ADDRESS);
        coned_offering.class_city_state = k.Safe(hnc_class_city_state[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_STREET_ADDRESS);
        coned_offering.class_county_code = k.Safe(hnc_class_county_code[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        coned_offering.class_region_code = k.Safe(hnc_class_region_code[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        //
        available_coned_classes_list.Add(coned_offering);
        }
      return available_coned_classes_list;
      }

    internal ArrayList ClassSearchScreen()
      {
      var class_search_screen = new ArrayList();
      var cookie_container = new CookieContainer();
      //
      Login(region_code:"1",cookie_container:cookie_container);
      //
      HttpWebResponse response;
      if (!Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_ConedClasssearch(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClasssearch() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_ConedClasssearch_Coned_Searchnow(cookie_container,DateTime.Today.AddMonths(-2).ToString(),out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClasssearch_Coned_Searchnow() returned FALSE.");
        }
      //
      var hn_target_table = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.SelectSingleNode("html/body/font/center/table");
      //
      var hnc_start_date = hn_target_table.SelectNodes("tr/td[1]"); // This will also catch the horizontal line separator cells that occur every other row, so there'll be twice as many of these as of the rest.
      var hnc_course_title = hn_target_table.SelectNodes("tr/td[2]/table/tr[1]/td[2]/a");
      var hnc_course_number = hn_target_table.SelectNodes("tr/td[2]/table/tr[2]/td[2]");
      var hnc_fr_med_trauma_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[2]/td[4]");
      var hnc_fr_other_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[2]/td[5]");
      var hnc_emt_med_trauma_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[3]/td[4]");
      var hnc_emt_other_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[3]/td[5]");
      var hnc_emtp_med_trauma_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[4]/td[4]");
      var hnc_emtp_other_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[4]/td[5]");
      var hnc_phrn_med_trauma_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[5]/td[4]");
      var hnc_phrn_other_hours = hn_target_table.SelectNodes("tr/td[2]/table/tr[5]/td/table/tr[5]/td[5]");
      var hnc_length = hn_target_table.SelectNodes("tr/td[2]/table/tr[7]/td[2]");
      var hnc_closed = hn_target_table.SelectNodes("tr/td[2]/table/tr[8]/td[2]");
      var hnc_student_cost = hn_target_table.SelectNodes("tr/td[2]/table/tr[9]/td[2]");
      var hnc_class_number = hn_target_table.SelectNodes("tr/td[3]/table/tr[2]/td[2]/a");
      var hnc_sponsor_name = hn_target_table.SelectNodes("tr/td[3]/table/tr[4]/td[2]");
      var hnc_sponsor_id = hn_target_table.SelectNodes("tr/td[3]/table/tr[4]/td[2]/input");
      var hnc_region_council_name = hn_target_table.SelectNodes("tr/td[3]/table/tr[5]/td[2]");
      var hnc_region_council_num = hn_target_table.SelectNodes("tr/td[3]/table/tr[5]/td[2]/input");
      var hnc_class_county_name = hn_target_table.SelectNodes("tr/td[3]/table/tr[6]/td[2]");
      var hnc_location = hn_target_table.SelectNodes("tr/td[3]/table/tr[7]/td[2]");
      var hnc_public_contact_name = hn_target_table.SelectNodes("tr/td[3]/table/tr[9]/td[2]");
      var hnc_public_contact_phone = hn_target_table.SelectNodes("tr/td[3]/table/tr[10]/td[2]");
      //var hnc_public_contact_email = hn_target_table.SelectNodes("tr/td[3]/table/tr[11]/td[2]/a");
      var hnc_public_contact_website = hn_target_table.SelectNodes("tr/td[3]/table/tr[12]/td[2]");
      var hnc_public_contact_notes = hn_target_table.SelectNodes("tr/td[3]/table/tr[13]/td[2]");
      //
      for (var i = new k.subtype<int>(0,hnc_course_title.Count); i.val < i.LAST; i.val++)
        {
        var class_county_name = k.Safe(hnc_class_county_name[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
        var coned_offering = new ConedOffering();
        coned_offering.approved = "1";
        coned_offering.start_date_time = k.Safe(hnc_start_date[i.val*2].FirstChild.InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        coned_offering.course_id = k.Safe(hnc_course_title[i.val].Attributes["href"].Value,k.safe_hint_type.NUM);
        coned_offering.course_title = k.Safe(hnc_course_title[i.val].InnerText.Trim(),k.safe_hint_type.PUNCTUATED);
        coned_offering.course_number = k.Safe(hnc_course_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        coned_offering.fr_med_trauma_hours = k.Safe(hnc_fr_med_trauma_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.fr_other_hours = k.Safe(hnc_fr_other_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emt_med_trauma_hours = k.Safe(hnc_emt_med_trauma_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emt_other_hours = k.Safe(hnc_emt_other_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emtp_med_trauma_hours = k.Safe(hnc_emtp_med_trauma_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.emtp_other_hours = k.Safe(hnc_emtp_other_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.phrn_med_trauma_hours = k.Safe(hnc_phrn_med_trauma_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.phrn_other_hours = k.Safe(hnc_phrn_other_hours[i.val].InnerText.Trim(),k.safe_hint_type.REAL_NUM);
        coned_offering.length = k.Safe(hnc_length[i.val].InnerText.Trim().Replace("&nbsp;hrs.",k.EMPTY),k.safe_hint_type.REAL_NUM);
        coned_offering.closed = k.Safe(hnc_closed[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
        coned_offering.student_cost = k.Safe(hnc_student_cost[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
        coned_offering.class_id_1 = k.Safe(hnc_class_number[i.val].Attributes["href"].Value,k.safe_hint_type.NUM);
        coned_offering.class_number = k.Safe(hnc_class_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        coned_offering.sponsor_name = k.Safe(hnc_sponsor_name[i.val].InnerText.Trim(),k.safe_hint_type.ORG_NAME);
        coned_offering.sponsor_id = k.Safe(hnc_sponsor_id[i.val].Attributes["value"].Value,k.safe_hint_type.NUM);
        coned_offering.region_council_name = k.Safe(hnc_region_council_name[i.val].InnerText.Trim(),k.safe_hint_type.ORG_NAME);
        coned_offering.region_council_num = k.Safe(hnc_region_council_num[i.val].Attributes["value"].Value,k.safe_hint_type.NUM);
        coned_offering.class_county_name = class_county_name.Replace("Northhampton","Northampton");
        coned_offering.location = k.Safe(hnc_location[i.val].FirstChild.InnerText.Trim(),k.safe_hint_type.POSTAL_STREET_ADDRESS);
        coned_offering.public_contact_name = k.Safe(hnc_public_contact_name[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME);
        coned_offering.public_contact_phone = k.Safe(hnc_public_contact_phone[i.val].InnerText.Trim(),k.safe_hint_type.PHONE_NUM);
        //coned_offering.public_contact_email = k.Safe(hnc_public_contact_email[i.val].InnerText.Trim(),k.safe_hint_type.EMAIL_ADDRESS);
        coned_offering.public_contact_website = k.Safe(hnc_public_contact_website[i.val].InnerText.Trim(),k.safe_hint_type.HTTP_TARGET);
        coned_offering.public_contact_notes = k.Safe(hnc_public_contact_notes[i.val].InnerText.Trim(),k.safe_hint_type.PUNCTUATED);
        class_search_screen.Add(coned_offering);
        }
      return class_search_screen;
      }

    internal ArrayList ClassSearchTabDelimited()
      {
      var class_search_tab_delimited = new ArrayList();
      var cookie_container = new CookieContainer();
      //
      Login(region_code:"1",cookie_container:cookie_container);
      //
      HttpWebResponse response;
      if (!Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_ConedClasssearch(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClasssearch() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_ConedClasssearch_Coned_Filedelimited_Searchnow(cookie_container,DateTime.Today.AddMonths(-2).ToString(),out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClasssearch_Coned_Filedelimited_Searchnow() returned FALSE.");
        }
      var dn = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode;
      if(!Request_ems_health_state_pa_us_ConedExportClasssearchtxt
          (
          cookie_container,
          k.Safe
            (
            dn.SelectSingleNode("/html/body/font[2]/a[1]").Attributes["href"].Value.Replace("Export/ClassSearch_",k.EMPTY).Replace(".txt",k.EMPTY),
            k.safe_hint_type.NUM
            ),
          out response
          )
        )
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedExportClasssearchtxt() returned FALSE.");
        }
      var stream_reader = new StreamReader(response.GetResponseStream());
      stream_reader.ReadLine();  //Discard the first line.  It only contains column headings.
      while (!stream_reader.EndOfStream)
        {
        var field_array = stream_reader.ReadLine().Split(new string[] {k.TAB},StringSplitOptions.None);
        var course_title = k.Safe(field_array[77],k.safe_hint_type.PUNCTUATED);
        if (course_title.Trim().Length > 0)
          {
          var coned_offering = new ConedOffering();
          coned_offering.class_id_1 = k.Safe(field_array[0],k.safe_hint_type.NUM);
          coned_offering.class_type = k.Safe(field_array[1],k.safe_hint_type.ALPHA);
          coned_offering.course_id = k.Safe(field_array[2],k.safe_hint_type.NUM);
          coned_offering.class_number = k.Safe(field_array[3],k.safe_hint_type.NUM);
          coned_offering.course_code = k.Safe(field_array[4],k.safe_hint_type.NUM);
          coned_offering.created_by = k.Safe(field_array[5],k.safe_hint_type.NUM);
          coned_offering.date_created = k.Safe(field_array[6],k.safe_hint_type.DATE_TIME);
          coned_offering.last_edited_by = k.Safe(field_array[7],k.safe_hint_type.NUM);
          coned_offering.date_last_edited = k.Safe(field_array[8],k.safe_hint_type.DATE_TIME);
          coned_offering.sponsor_id = k.Safe(field_array[9],k.safe_hint_type.NUM);
          coned_offering.sponsor_number = k.Safe(field_array[10],k.safe_hint_type.HYPHENATED_NUM);
          coned_offering.training_ins_accred_num = k.Safe(field_array[11],k.safe_hint_type.NUM);
          coned_offering.document_status = k.Safe(field_array[12],k.safe_hint_type.ALPHA);
          coned_offering.class_final_status = k.Safe(field_array[13],k.safe_hint_type.ALPHA);
          coned_offering.course_number = k.Safe(field_array[14],k.safe_hint_type.NUM);
          coned_offering.location = k.Safe(field_array[15],k.safe_hint_type.POSTAL_STREET_ADDRESS);
          coned_offering.student_cost = k.Safe(field_array[16],k.safe_hint_type.CURRENCY_USA);
          coned_offering.tuition_includes = k.Safe(field_array[17],k.safe_hint_type.PUNCTUATED);
          coned_offering.closed = k.Safe(field_array[18],k.safe_hint_type.ALPHA);
          coned_offering.estimated_students = k.Safe(field_array[19],k.safe_hint_type.NUM);
          coned_offering.start_date_time = k.Safe(field_array[20],k.safe_hint_type.DATE_TIME);
          coned_offering.end_date_time = k.Safe(field_array[21],k.safe_hint_type.DATE_TIME);
          coned_offering.start_time = k.Safe(field_array[22],k.safe_hint_type.PUNCTUATED);
          coned_offering.end_time = k.Safe(field_array[23],k.safe_hint_type.PUNCTUATED);
          coned_offering.other_dates_and_times = k.Safe(field_array[24],k.safe_hint_type.PUNCTUATED);
          coned_offering.instructors = k.Safe(field_array[25],k.safe_hint_type.PUNCTUATED);
          coned_offering.instructor_qualifications = k.Safe(field_array[26],k.safe_hint_type.PUNCTUATED);
          coned_offering.verification_name = k.Safe(field_array[27],k.safe_hint_type.HUMAN_NAME);
          coned_offering.contact_name = k.Safe(field_array[28],k.safe_hint_type.HUMAN_NAME);
          coned_offering.contact_address_1 = k.Safe(field_array[29],k.safe_hint_type.POSTAL_STREET_ADDRESS);
          coned_offering.contact_address_2 = k.Safe(field_array[30],k.safe_hint_type.POSTAL_STREET_ADDRESS);
          coned_offering.contact_city = k.Safe(field_array[31],k.safe_hint_type.POSTAL_CITY);
          coned_offering.contact_state = k.Safe(field_array[32],k.safe_hint_type.ALPHA);
          coned_offering.contact_zip = k.Safe(field_array[33],k.safe_hint_type.NUM);
          coned_offering.contact_daytime_phone = k.Safe(field_array[34],k.safe_hint_type.PHONE_NUM);
          coned_offering.contact_evening_phone = k.Safe(field_array[35],k.safe_hint_type.PHONE_NUM);
          coned_offering.contact_email = k.Safe(field_array[36],k.safe_hint_type.EMAIL_ADDRESS);
          coned_offering.public_contact_name = k.Safe(field_array[37],k.safe_hint_type.HUMAN_NAME);
          coned_offering.public_contact_phone = k.Safe(field_array[38],k.safe_hint_type.PHONE_NUM);
          coned_offering.public_contact_email = k.Safe(field_array[39],k.safe_hint_type.EMAIL_ADDRESS);
          coned_offering.public_contact_website = k.Safe(field_array[40],k.safe_hint_type.HTTP_TARGET);
          coned_offering.public_contact_notes = k.Safe(field_array[41],k.safe_hint_type.PUNCTUATED);
          coned_offering.date_submitted_to_region = k.Safe(field_array[42],k.safe_hint_type.DATE_TIME);
          coned_offering.date_received_by_region = k.Safe(field_array[43],k.safe_hint_type.DATE_TIME);
          coned_offering.date_sponsor_notified = k.Safe(field_array[44],k.safe_hint_type.DATE_TIME);
          coned_offering.date_registration_sent_to_state = k.Safe(field_array[45],k.safe_hint_type.DATE_TIME);
          coned_offering.date_cards_sent_to_sponsor = k.Safe(field_array[46],k.safe_hint_type.DATE_TIME);
          coned_offering.date_materials_to_be_returned = k.Safe(field_array[47],k.safe_hint_type.DATE_TIME);
          coned_offering.approved = k.Safe(field_array[48],k.safe_hint_type.NUM);
          coned_offering.region_comments = k.Safe(field_array[49],k.safe_hint_type.PUNCTUATED);
          coned_offering.region_council_num = k.Safe(field_array[50],k.safe_hint_type.NUM);
          coned_offering.class_county_code = k.Safe(field_array[51],k.safe_hint_type.NUM);
          coned_offering.total_class_hours = k.Safe(field_array[52],k.safe_hint_type.REAL_NUM);
          coned_offering.location_address_1 = k.Safe(field_array[53],k.safe_hint_type.POSTAL_STREET_ADDRESS);
          coned_offering.location_address_2 = k.Safe(field_array[54],k.safe_hint_type.POSTAL_STREET_ADDRESS);
          coned_offering.location_city = k.Safe(field_array[55],k.safe_hint_type.POSTAL_CITY);
          coned_offering.location_state = k.Safe(field_array[56],k.safe_hint_type.ALPHA);
          coned_offering.location_zip = k.Safe(field_array[57],k.safe_hint_type.NUM);
          coned_offering.location_zip_plus_4 = k.Safe(field_array[58],k.safe_hint_type.NUM);
          coned_offering.location_phone = k.Safe(field_array[59],k.safe_hint_type.PHONE_NUM);
          coned_offering.location_email = k.Safe(field_array[60],k.safe_hint_type.EMAIL_ADDRESS);
          coned_offering.location_of_registration = k.Safe(field_array[61],k.safe_hint_type.PUNCTUATED);
          coned_offering.primary_text = k.Safe(field_array[62],k.safe_hint_type.PUNCTUATED);
          coned_offering.additional_texts = k.Safe(field_array[63],k.safe_hint_type.NUM);
          coned_offering.final_registration_date = k.Safe(field_array[64],k.safe_hint_type.DATE_TIME);
          coned_offering.offered_as_college_credit = k.Safe(field_array[65],k.safe_hint_type.ALPHA);
          coned_offering.practical_exam_date = k.Safe(field_array[66],k.safe_hint_type.DATE_TIME);
          coned_offering.written_exam_date = k.Safe(field_array[67],k.safe_hint_type.DATE_TIME);
          coned_offering.disapproval_reason_id = k.Safe(field_array[68],k.safe_hint_type.NUM);
          coned_offering.date_final_paperwork_received = k.Safe(field_array[69],k.safe_hint_type.DATE_TIME);
          coned_offering.signed_hard_copy = k.Safe(field_array[70],k.safe_hint_type.ALPHA);
          coned_offering.created_by_first_name = k.Safe(field_array[71],k.safe_hint_type.HUMAN_NAME);
          coned_offering.created_by_last_name = k.Safe(field_array[72],k.safe_hint_type.HUMAN_NAME);
          coned_offering.class_disapproval_reason_description = k.Safe(field_array[73],k.safe_hint_type.PUNCTUATED);
          coned_offering.class_final_status_description = k.Safe(field_array[74],k.safe_hint_type.ALPHA_WORDS);
          coned_offering.sponsor_name = k.Safe(field_array[75],k.safe_hint_type.ORG_NAME);
          coned_offering.courses_course_number = k.Safe(field_array[76],k.safe_hint_type.NUM);
          coned_offering.course_title = course_title;
          coned_offering.cert_course_code = k.Safe(field_array[78],k.safe_hint_type.NUM);
          coned_offering.cert_course_description = k.Safe(field_array[79],k.safe_hint_type.NUM);
          coned_offering.class_id_2 = k.Safe(field_array[80],k.safe_hint_type.NUM);
          class_search_tab_delimited.Add(coned_offering);
          }
        }
      stream_reader.Close();
      return class_search_tab_delimited;
      }

    private class TeachingEntity
      {
      public string id = k.EMPTY;
      public string date_created = k.EMPTY;
      public string date_last_edited = k.EMPTY;
      public string sponsor_number = k.EMPTY;
      public string training_ins_accred_num = k.EMPTY;
      public string name = k.EMPTY;
      public string short_name = k.EMPTY;
      public string address_1 = k.EMPTY;
      public string address_2 = k.EMPTY;
      public string city = k.EMPTY;
      public string state = k.EMPTY;
      public string zip = k.EMPTY;
      public string county_code = k.EMPTY;
      public string region = k.EMPTY;
      public string email = k.EMPTY;
      public string website = k.EMPTY;
      public string daytime_phone = k.EMPTY;
      public string evening_phone = k.EMPTY;
      public string fax = k.EMPTY;
      public string business_type_id = k.EMPTY;
      public string con_ed_level = k.EMPTY;
      public string certification_level = k.EMPTY;
      public string contact_prefix = k.EMPTY;
      public string contact_first_name = k.EMPTY;
      public string contact_last_name = k.EMPTY;
      public string contact_suffix = k.EMPTY;
      public string contact_title = k.EMPTY;
      public string contact_address_1 = k.EMPTY;
      public string contact_address_2 = k.EMPTY;
      public string contact_city = k.EMPTY;
      public string contact_state = k.EMPTY;
      public string contact_zip = k.EMPTY;
      public string contact_daytime_phone = k.EMPTY;
      public string contact_evening_phone = k.EMPTY;
      public string contact_fax = k.EMPTY;
      public string contact_email = k.EMPTY;
      public string public_contact_name = k.EMPTY;
      public string public_contact_phone = k.EMPTY;
      public string public_contact_email = k.EMPTY;
      public string public_contact_website = k.EMPTY;
      public string public_contact_notes = k.EMPTY;
      public string application_date = k.EMPTY;
      public string application_received = k.EMPTY;
      public string sponsor_status = k.EMPTY;
      public string sponsor_status_description = k.EMPTY;
      public string training_inst_status = k.EMPTY;
      public string training_inst_status_description = k.EMPTY;
      public string issue_date = k.EMPTY;
      public string prev_expiration_date = k.EMPTY;
      public string expiration_date_sponsor = k.EMPTY;
      public string expiration_date_training_inst = k.EMPTY;
      public string process_date = k.EMPTY;
      public string corrective_action = k.EMPTY;
      public string compliance_by_date = k.EMPTY;
      public string initial_accred_date = k.EMPTY;
      public string accepted_provisional_date = k.EMPTY;
      public string completed_provisional_date = k.EMPTY;
      public string withdrawal_challenge_due_date = k.EMPTY;
      public string letter_for_expiration = k.EMPTY;
      public string letter_f_for_ppwk_non_compliance = k.EMPTY;
      public string other_letter = k.EMPTY;
      public string history = k.EMPTY;
      public string business_type_description = k.EMPTY;
      public string county_name = k.EMPTY;
      }
    public ArrayList TeachingEntitySearch()
      {
      var teaching_entity_search = new ArrayList();
      //
      HttpWebResponse response;
      var cookie_container = new CookieContainer();
      if (Request_ems_health_state_pa_us_ConedSponsorsearch_StatusSearchnow(cookie_container,"S1",out response))
        {
        ScrapeConedSponsors("Approved",teaching_entity_search,response);
        }
      if (Request_ems_health_state_pa_us_ConedSponsorsearch_StatusSearchnow(cookie_container,"S3",out response))
        {
        ScrapeConedSponsors("Provisional",teaching_entity_search,response);
        }
      return teaching_entity_search;
      }

    internal class Practitioner
      {
      internal string name = k.EMPTY;
      internal string certification_number = k.EMPTY;
      internal string level = k.EMPTY;
      internal string status = k.EMPTY;
      internal string county = k.EMPTY;
      internal string region = k.EMPTY;
      }
    internal class PractitionersContext
      {
      internal CookieContainer cookie_container = null;
      internal string view_state = k.EMPTY;
      internal string view_state_generator = k.EMPTY;
      internal string event_validation = k.EMPTY;
      internal HtmlNodeCollection county_option_collection = null;
      internal k.subtype<int> county_option_index;
      internal k.int_sign_range disposition = null;
      //
      public PractitionersContext()
        {
        HttpWebResponse response;
        cookie_container = new CookieContainer();
        disposition = new k.int_sign_range();
        if(!Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners
            (
            cookie_container:cookie_container,
            response:out response
            )
          )
        //then
          {
          throw new Exception("Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners returned FALSE.");
          }
        var html_document = HtmlDocumentOf(ConsumedStreamOf(response));
        view_state = ViewstateOf(html_document);
        view_state_generator = ViewstateGeneratorOf(html_document);
        event_validation = EventValidationOf(html_document);
        county_option_collection = html_document.DocumentNode.SelectNodes("//select[@id='SessionLinkBar_Content_ddlCounty']/option/@value");
        county_option_index = new k.subtype<int>(the_first:0,the_last:county_option_collection.Count); // Change the_first value to 1 to skip the first "Select county" / -1 select option if actually pulling data county-by-county.
        }
      }
    internal ArrayList Practitioners
      (
      PractitionersContext context
      )
      {
      var active_practitioners = new ArrayList();
      //
      HtmlDocument html_document;
      HttpWebResponse response;
      var result = k.EMPTY;
      //
      var log = new StreamWriter(path:HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["scratch_folder"] + "/Class_ss_emsams.Practitioners.log"),append:true);
      log.AutoFlush = true;
      log.WriteLine(DateTime.Now.ToString("s") + ": NEW JOB STARTING");
      //
      log.WriteLine(DateTime.Now.ToString("s") + ": Calling Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search");
      result = Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search
        (
        cookie_container: context.cookie_container,
        view_state: context.view_state,
        view_state_generator: context.view_state_generator,
        event_validation: context.event_validation,
        response: out response
        );
      if (result.Length > 0)
        {
        log.WriteLine(DateTime.Now.ToString("s") + ": EXCEPTION - Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search() returned '" + result + "'.");
        throw new Exception("Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Search() returned '" + result + "'.");
        }
      html_document = HtmlDocumentOf(ConsumedStreamOf(response));
      context.view_state = ViewstateOf(html_document);
      context.view_state_generator = ViewstateGeneratorOf(html_document);
      context.event_validation = EventValidationOf(html_document);
      //
      log.WriteLine(DateTime.Now.ToString("s") + ": Calling Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Perpage100");
      result = Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Perpage100
        (
        cookie_container:context.cookie_container,
        view_state:context.view_state,
        view_state_generator:context.view_state_generator,
        event_validation:context.event_validation,
        response:out response
        );
      if(result.Length > 0)
        {
        log.WriteLine(DateTime.Now.ToString("s") + ": EXCEPTION - Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Perpage100() returned '" + result + "'.");
        throw new Exception("Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_Perpage100() returned '" + result + "'.");
        }
      var target_next_page_button_num = new k.subtype<int>(1,5);
      target_next_page_button_num.val = 2;
      var page_index = new k.int_positive();
      var row_index = new k.int_nonnegative();
      do
        {
        html_document = HtmlDocumentOf(ConsumedStreamOf(response));
        context.view_state = ViewstateOf(html_document);
        context.view_state_generator = ViewstateGeneratorOf(html_document);
        context.event_validation = EventValidationOf(html_document);
        //
        // The initial XPaths are determined by visiting the page in Google Chrome and activating the XPath Helper Wizard and/or Relative XPath Helper extensions.
        //
        var hn_target_table = html_document.GetElementbyId("SessionLinkBar_Content_gvPractitionerSearchResults"); 
        var saved_practitioner_name = k.EMPTY;
        //
        try
          {
          var hnc_name = hn_target_table.SelectNodes("tr/td[1]");
          var hnc_certification_number = hn_target_table.SelectNodes("tr/td[2]");
          var hnc_level = hn_target_table.SelectNodes("tr/td[3]");
          var hnc_status = hn_target_table.SelectNodes("tr/td[4]");
          var hnc_county = hn_target_table.SelectNodes("tr/td[6]");
          var hnc_region = hn_target_table.SelectNodes("tr/td[7]");
          //
          var certification_number = k.EMPTY;
          for (var i = new k.subtype<int>(1,hnc_name.Count - 1); i.val < i.LAST; i.val++)  //limits take into account non-data header
            {
            row_index.val = i.val;
            certification_number = k.Safe(hnc_certification_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
            if (certification_number.Length > 0)
              {
              var practitioner = new Practitioner();
              practitioner.name = k.Safe(hnc_name[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME_CSV);
              saved_practitioner_name = practitioner.name;
              practitioner.certification_number = certification_number;
              practitioner.level = k.Safe(hnc_level[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME);
              practitioner.status = k.Safe(hnc_status[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
              practitioner.county = k.Safe(hnc_county[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_CITY);
              practitioner.region = k.Safe(hnc_region[i.val].InnerText.Trim(),k.safe_hint_type.ORG_NAME);
              active_practitioners.Add(practitioner);
              }
            row_index.val = 0;
            }
          }
        catch (Exception e)
          {
          log.WriteLine(DateTime.Now.ToString("s") + ": EXCEPTION - " + e.StackTrace + k.NEW_LINE + k.NEW_LINE + "At page_index.val " + page_index.val + " row_index.val " + row_index.val + " the received html_document was: " + k.NEW_LINE + html_document.DocumentNode.InnerHtml);
          throw new Exception(e.StackTrace + k.NEW_LINE + k.NEW_LINE + "At page_index.val " + page_index.val + " row_index.val " + row_index.val + " the received html_document was: " + k.NEW_LINE + html_document.DocumentNode.InnerHtml);
          }
        if (html_document.GetElementbyId("SessionLinkBar_Content_gvPractitionerSearchResults_lbtnPage" + target_next_page_button_num.val.ToString()) == null)
          {
          context.disposition.val = 1;
          }
        else
          {
          context.disposition.val = 0;
          do
            {
            log.WriteLine(DateTime.Now.ToString("s") + ": Calling Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_NextNumericalPage");
            result = Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_NextNumericalPage
              (
              cookie_container:context.cookie_container,
              view_state:context.view_state,
              view_state_generator:context.view_state_generator,
              event_validation:context.event_validation,
              target_next_page_button_num:target_next_page_button_num,
              response:out response
              );
            if (result.Length > 0)
              {
              log.WriteLine
                (
                DateTime.Now.ToString("s") + ":"
                + " Request_ems_health_state_pa_us_RegistryRegistryActivepractitioners_NextNumericalPage() returned '" + result + "'"
                + " with saved_practitioner_name = '" + saved_practitioner_name + "'"
                + " and target_next_page_button_num.val = '" + target_next_page_button_num.val + "'"
                + " and page_index.val '" + page_index.val + "'"
                + " and row_index.val '" + row_index.val + "'"
                );
              Thread.Sleep(millisecondsTimeout:5000);
              }
            }
          while (result.Length > 0);
          target_next_page_button_num.val = (target_next_page_button_num.val < target_next_page_button_num.LAST ? target_next_page_button_num.val + 1 : 1);
          }
        page_index.val++;
        }
      while (context.disposition.val == 0);
      log.WriteLine(DateTime.Now.ToString("s") + ": DONE");
      log.Close();
      return active_practitioners;
      }

    internal class DetailedPractitioner
      {
      internal string certification_number = k.EMPTY;
      internal string name = k.EMPTY;
      internal string level = k.EMPTY;
      internal string issue_date = k.EMPTY;
      internal string expiration_date = k.EMPTY;
      internal string region = k.EMPTY;
      internal string county = k.EMPTY;
      internal string birth_date = k.EMPTY;
      internal string gender = k.EMPTY;
      internal string status = k.EMPTY;
      }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible")]
    public class DetailedCurrentPractitionersContext
      {
      internal CookieContainer cookie_container = null;
      internal k.int_sign_range disposition = null;
      internal string event_validation = k.EMPTY;
      internal string view_state = k.EMPTY;
      //
      public DetailedCurrentPractitionersContext()
        {
        cookie_container = new CookieContainer();
        disposition = new k.int_sign_range();
        }
      }
    public ArrayList DetailedCurrentPractitioners
      (
      DetailedCurrentPractitionersContext context
      )
      {
      var detailed_current_practitioners = new ArrayList();
      HtmlDocument html_document;
      HttpWebResponse response;
      //
      if (context.disposition.val == -1)
        {
        Login(region_code:"1",cookie_container:context.cookie_container);
        //
        if (!Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg(context.cookie_container,out response))
          {
          throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoemsreg() returned FALSE.");
          }
        response.Close();
        if (!Request_ems_health_state_pa_us_EmsregDefault(context.cookie_container,out response))
          {
          throw new Exception("Request_ems_health_state_pa_us_EmsregDefault() returned FALSE.");
          }
        response.Close();
        var request_ems_health_state_pa_us_emsregpractitionerhome = Request_ems_health_state_pa_us_EmsregPractitionerHome(context.cookie_container,out response);
        if (request_ems_health_state_pa_us_emsregpractitionerhome.Length > 0)
          {
          throw new Exception("Request_ems_health_state_pa_us_EmsregPractitionerHome() returned [" + request_ems_health_state_pa_us_emsregpractitionerhome + "]");
          }
        response.Close();
        if (!Request_ems_health_state_pa_us_emsregPractitionerSearch(context.cookie_container,out response))
          {
          throw new Exception("Request_ems_health_state_pa_us_emsregPractitionerSearch() returned FALSE.");
          }
        html_document = HtmlDocumentOf(ConsumedStreamOf(response));
        context.view_state = ViewstateOf(html_document);
        context.event_validation = EventValidationOf(html_document);
        if (!Request_ems_health_state_pa_us_EmsregPractitionerSearch_PractitionerstatusActiveSuspendedExpiredProbation_Submit(context.cookie_container,context.view_state,context.event_validation,out response))
          {
          throw new Exception("Request_ems_health_state_pa_us_EmsregPractitionerSearch_PractitionerstatusActiveSuspendedExpiredProbation_Submit() returned FALSE.");
          }
        html_document = HtmlDocumentOf(ConsumedStreamOf(response));
        context.view_state = ViewstateOf(html_document);
        context.event_validation = EventValidationOf(html_document);
        if (!Request_ems_health_state_pa_us_EmsregPractitionerSearchresults_1000recordsperpage(context.cookie_container,context.view_state,context.event_validation,out response))
          {
          throw new Exception("Request_ems_health_state_pa_us_EmsregPractitionerSearchresults_1000recordsperpage() returned FALSE.");
          }
        }
      else
        {
        var request_ems_health_state_pa_us_emsregpractitionersearchresults_next = Request_ems_health_state_pa_us_EmsregPractitionerSearchresults_Next(context.cookie_container,context.view_state,context.event_validation,out response);
        if (request_ems_health_state_pa_us_emsregpractitionersearchresults_next.Length > 0)
          {
          throw new Exception("Request_ems_health_state_pa_us_EmsregPractitionerSearchresults_Next() returned [" + request_ems_health_state_pa_us_emsregpractitionersearchresults_next + "]");
          }
        }
      html_document = HtmlDocumentOf(ConsumedStreamOf(response));
      //
      // The initial XPaths are determined by visiting the page in IE9, selecting "F12 developer tools", setting Document Mode to IE9 Standards, navigating to the node of interest, and disregarding any form or tbody tags.
      //
      var hn_target_table = html_document.GetElementbyId("_ctl0__ctl0_SessionLinkBar_Content_dgSearchResults"); 
      //
      var hnc_certification_number = hn_target_table.SelectNodes("tr/td[1]/a");
      var hnc_name = hn_target_table.SelectNodes("tr/td[2]");
      var hnc_level = hn_target_table.SelectNodes("tr/td[3]");
      var hnc_issue_date = hn_target_table.SelectNodes("tr/td[4]");
      var hnc_expiration_date = hn_target_table.SelectNodes("tr/td[5]");
      var hnc_region = hn_target_table.SelectNodes("tr/td[6]");
      var hnc_county = hn_target_table.SelectNodes("tr/td[7]");
      var hnc_birth_date = hn_target_table.SelectNodes("tr/td[8]");
      var hnc_gender = hn_target_table.SelectNodes("tr/td[9]");
      var hnc_status = hn_target_table.SelectNodes("tr/td[10]");
      //
      for (var i = new k.subtype<int>(1,hnc_certification_number.Count); i.val < i.LAST; i.val++)  //limits take into account non-data header & page index rows
        {
        var detailed_practitioner = new DetailedPractitioner();
        detailed_practitioner.certification_number = k.Safe(hnc_certification_number[i.val].InnerText.Trim(),k.safe_hint_type.NUM);
        detailed_practitioner.name = k.Safe(hnc_name[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME_CSV);
        detailed_practitioner.level = k.Safe(hnc_level[i.val].InnerText.Trim(),k.safe_hint_type.HUMAN_NAME);
        detailed_practitioner.issue_date = k.Safe(hnc_issue_date[i.val].InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        detailed_practitioner.expiration_date = k.Safe(hnc_expiration_date[i.val].InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        detailed_practitioner.region = k.Safe(hnc_region[i.val].InnerText.Trim(),k.safe_hint_type.ORG_NAME);
        detailed_practitioner.county = k.Safe(hnc_county[i.val].InnerText.Trim(),k.safe_hint_type.POSTAL_CITY);
        detailed_practitioner.birth_date = k.Safe(hnc_birth_date[i.val].InnerText.Trim(),k.safe_hint_type.DATE_TIME);
        detailed_practitioner.gender = k.Safe(hnc_gender[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
        detailed_practitioner.status = k.Safe(hnc_status[i.val].InnerText.Trim(),k.safe_hint_type.ALPHA);
        detailed_current_practitioners.Add(detailed_practitioner);
        }
      //
      context.view_state = ViewstateOf(html_document);
      context.event_validation = EventValidationOf(html_document);
      //
      var hn_Next = html_document.GetElementbyId("_ctl0__ctl0_SessionLinkBar_Content_lbtnNext");
      context.disposition.val = (hn_Next.Attributes["disabled"] == null ? 0 : 1);
      return detailed_current_practitioners;
      }

    internal void MarkClassCanceled
      (
      string class_id,
      string region_code
      )
      {
      var cookie_container = new CookieContainer();
      //
      Login(region_code:region_code,cookie_container:cookie_container);
      //
      HttpWebResponse response;
      if (!Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_EmsportalApplicationtransfersTransfertoconed() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_ConedClasssearch(cookie_container,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClasssearch() returned FALSE.");
        }
      response.Close();
      if (!Request_ems_health_state_pa_us_ConedClassreg_Classid(cookie_container,class_id,out response))
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClassreg_Classid() returned FALSE.");
        }
      var hdn = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode;
      //
      // The initial XPaths are determined by visiting the page in IE9, selecting "F12 developer tools", setting Document Mode to IE9 Standards, navigating to the node of interest, and disregarding any form or tbody tags.
      //
      var hn_disapproval_reason_id = hdn.SelectSingleNode("//select[@name='DisapprovalReasonID']/option[@selected]");
      var hn_college_credit_awarded = hdn.SelectSingleNode("//input[@name='CollegeCreditAwarded' and @checked]");
      //
      var created_by = hdn.SelectSingleNode("//input[@name='CreatedBy']").Attributes["value"].Value;
      var date_created = hdn.SelectSingleNode("//input[@name='DateCreated']").Attributes["value"].Value;
      var date_last_edited = hdn.SelectSingleNode("//input[@name='DateLastEdited']").Attributes["value"].Value;
      var date_submitted_to_region = hdn.SelectSingleNode("//input[@name='DateSubmittedToRegion']").Attributes["value"].Value;
      var document_status = hdn.SelectSingleNode("//input[@name='DocumentStatus']").Attributes["value"].Value;
      var last_edited_by = hdn.SelectSingleNode("//input[@name='LastEditedBy']").Attributes["value"].Value;
      var region_council_num = hdn.SelectSingleNode("//input[@name='RegionCouncilNum']").Attributes["value"].Value;
      var sponsor_id = hdn.SelectSingleNode("//input[@name='SponsorID']").Attributes["value"].Value;
      var course_id = hdn.SelectSingleNode("//input[@name='CourseID']").Attributes["value"].Value;
      var initial_value_approved = hdn.SelectSingleNode("//input[@name='InitialValue_Approved']").Attributes["value"].Value;
      var initial_value_disapproval_reason_id = hdn.SelectSingleNode("//input[@name='InitialValue_DisapprovalReasonID']").Attributes["value"].Value;
      var debug_session_emso_user_id = hdn.SelectSingleNode("//input[@name='debug_session_emsoUserid']").Attributes["value"].Value;
      var debug_session_user_role = hdn.SelectSingleNode("//input[@name='debug_session_userRole']").Attributes["value"].Value;
      var debug_session_sponsor_id = hdn.SelectSingleNode("//input[@name='debug_session_SponsorID']").Attributes["value"].Value;
      var debug_sponsor_info_editable = hdn.SelectSingleNode("//input[@name='debug_SponsorInfoEditable']").Attributes["value"].Value;
      var debug_region_info_editable = hdn.SelectSingleNode("//input[@name='debug_RegionInfoEditable']").Attributes["value"].Value;
      var sponsor_name = hdn.SelectSingleNode("//input[@name='SponsorName']").Attributes["value"].Value;
      var sponsor_number = hdn.SelectSingleNode("//input[@name='SponsorNumber']").Attributes["value"].Value;
      var training_ins_accred_num = hdn.SelectSingleNode("//input[@name='TrainingInsAccredNum']").Attributes["value"].Value;
      var sponsor_county = hdn.SelectSingleNode("//input[@name='SponsorCounty']").Attributes["value"].Value;
      var course_title = hdn.SelectSingleNode("//input[@name='CourseTitle']").Attributes["value"].Value;
      var not_valid_after_date = hdn.SelectSingleNode("//input[@name='NotValidAfterDate']").Attributes["value"].Value;
      var course_number = hdn.SelectSingleNode("//input[@name='CourseNumber']").Attributes["value"].Value;
      var location = hdn.SelectSingleNode("//input[@name='Location']").Attributes["value"].Value;
      var location_of_registration = hdn.SelectSingleNode("//input[@name='LocationOfRegistration']").Attributes["value"].Value;
      var location_address_1 = hdn.SelectSingleNode("//input[@name='LocationAddress1']").Attributes["value"].Value;
      var location_address_2 = hdn.SelectSingleNode("//input[@name='LocationAddress2']").Attributes["value"].Value;
      var location_city = hdn.SelectSingleNode("//input[@name='LocationCity']").Attributes["value"].Value;
      var location_state = hdn.SelectSingleNode("//input[@name='LocationState']").Attributes["value"].Value;
      var location_zip = hdn.SelectSingleNode("//input[@name='LocationZIP']").Attributes["value"].Value;
      var location_zip_plus_4 = hdn.SelectSingleNode("//input[@name='LocationZipPlus4']").Attributes["value"].Value;
      var county_code = hdn.SelectSingleNode("//input[@name='CountyCode']").Attributes["value"].Value;
      var county_name = hdn.SelectSingleNode("//input[@name='CountyName']").Attributes["value"].Value;
      var regional_council_name = hdn.SelectSingleNode("//input[@name='RegionalCouncilName']").Attributes["value"].Value;
      var location_phone = hdn.SelectSingleNode("//input[@name='LocationPhone']").Attributes["value"].Value;
      var location_email = hdn.SelectSingleNode("//input[@name='LocationEmail']").Attributes["value"].Value;
      var public_contact_name = hdn.SelectSingleNode("//input[@name='PublicContactName']").Attributes["value"].Value;
      var public_contact_email = hdn.SelectSingleNode("//input[@name='PublicContactEmail']").Attributes["value"].Value;
      var public_contact_phone = hdn.SelectSingleNode("//input[@name='PublicContactPhone']").Attributes["value"].Value;
      var public_contact_website = hdn.SelectSingleNode("//input[@name='PublicContactWebsite']").Attributes["value"].Value;
      var public_contact_notes = hdn.SelectSingleNode("//textarea[@name='PublicContactNotes']").InnerText;
      var student_cost = hdn.SelectSingleNode("//input[@name='StudentCost']").Attributes["value"].Value;
      var total_class_hours = hdn.SelectSingleNode("//input[@name='TotalClassHours']").Attributes["value"].Value;
      var total_class_hours_chk = hdn.SelectSingleNode("//input[@name='TotalClassHoursChk']").Attributes["value"].Value;
      var length_of_course_in_hours = hdn.SelectSingleNode("//input[@name='LengthOfCourseInHours']").Attributes["value"].Value;
      var tuition_includes = hdn.SelectSingleNode("//textarea[@name='TuitionIncludes']").InnerText;
      var closed = hdn.SelectSingleNode("//input[@name='Closed']").Attributes["value"].Value;
      var estimated_students = hdn.SelectSingleNode("//input[@name='EstimatedStudents']").Attributes["value"].Value;
      var start_date_time = hdn.SelectSingleNode("//input[@name='StartDateTime']").Attributes["value"].Value;
      var start_date_time_chk = hdn.SelectSingleNode("//input[@name='StartDateTimeChk']").Attributes["value"].Value;
      var start_time = hdn.SelectSingleNode("//input[@name='StartTime']").Attributes["value"].Value;
      var end_date_time = hdn.SelectSingleNode("//input[@name='EndDateTime']").Attributes["value"].Value;
      var end_date_time_chk = hdn.SelectSingleNode("//input[@name='EndDateTimeChk']").Attributes["value"].Value;
      var end_time = hdn.SelectSingleNode("//input[@name='EndTime']").Attributes["value"].Value;
      var final_registration_date = hdn.SelectSingleNode("//input[@name='FinalRegistrationDate']").Attributes["value"].Value;
      var instructors = hdn.SelectSingleNode("//input[@name='Instructors']").Attributes["value"].Value;
      var instructor_qualifications = hdn.SelectSingleNode("//input[@name='InstructorQualifications']").Attributes["value"].Value;
      var class_coordinator = hdn.SelectSingleNode("//input[@name='ClassCoordinator']").Attributes["value"].Value;
      var primary_text = hdn.SelectSingleNode("//input[@name='PrimaryText']").Attributes["value"].Value;
      var college_credit_awarded = (hn_college_credit_awarded == null ? k.EMPTY : hn_college_credit_awarded.Attributes["value"].Value);
      var held_on_sun = (hdn.SelectSingleNode("//input[@name='HeldOnSun' and @checked]") == null ? k.EMPTY : "CHECKED");
      var held_on_mon = (hdn.SelectSingleNode("//input[@name='HeldOnMon' and @checked]") == null ? k.EMPTY : "CHECKED");
      var held_on_tue = (hdn.SelectSingleNode("//input[@name='HeldOnTue' and @checked]") == null ? k.EMPTY : "CHECKED");
      var held_on_wed = (hdn.SelectSingleNode("//input[@name='HeldOnWed' and @checked]") == null ? k.EMPTY : "CHECKED");
      var held_on_thu = (hdn.SelectSingleNode("//input[@name='HeldOnThu' and @checked]") == null ? k.EMPTY : "CHECKED");
      var held_on_fri = (hdn.SelectSingleNode("//input[@name='HeldOnFri' and @checked]") == null ? k.EMPTY : "CHECKED");
      var held_on_sat = (hdn.SelectSingleNode("//input[@name='HeldOnSat' and @checked]") == null ? k.EMPTY : "CHECKED");
      var other_dates_and_times = hdn.SelectSingleNode("//textarea[@name='OtherDatesAndTimes']").InnerText;
      var date_received_by_region = hdn.SelectSingleNode("//input[@name='DateReceivedByRegion']").Attributes["value"].Value;
      var ret_to_applicant_comment = hdn.SelectSingleNode("//input[@name='RetToApplicantComment']").Attributes["value"].Value;
      var date_sponsor_notified = hdn.SelectSingleNode("//input[@name='DateSponsorNotified']").Attributes["value"].Value;
      var approved = hdn.SelectSingleNode("//input[@name='Approved']").Attributes["value"].Value;
      var class_number = hdn.SelectSingleNode("//input[@name='ClassNumber']").Attributes["value"].Value;
      var date_registration_sent_to_state = hdn.SelectSingleNode("//input[@name='DateRegistrationSentToState']").Attributes["value"].Value;
      var date_cards_sent_to_sponsor = hdn.SelectSingleNode("//input[@name='DateCardsSentToSponsor']").Attributes["value"].Value;
      var date_materials_to_be_returned = hdn.SelectSingleNode("//input[@name='DateMaterialsToBeReturned']").Attributes["value"].Value;
      var disapproval_reason_id = (hn_disapproval_reason_id == null ? k.EMPTY : hn_disapproval_reason_id.Attributes["value"].Value);
      var region_comments = hdn.SelectSingleNode("//textarea[@name='RegionComments']").InnerText;
      var practical_exam_date = hdn.SelectSingleNode("//input[@name='PracticalExamDate']").Attributes["value"].Value;
      var practical_exam_time = hdn.SelectSingleNode("//input[@name='PracticalExamTime']").Attributes["value"].Value;
      var written_exam_date = hdn.SelectSingleNode("//input[@name='WrittenExamDate']").Attributes["value"].Value;
      var written_exam_time = hdn.SelectSingleNode("//input[@name='WrittenExamTime']").Attributes["value"].Value;
      //
      var application_name = ConfigurationManager.AppSettings["application_name"];
      if(
          !(application_name.EndsWith("_d") || application_name.EndsWith("_x"))
        &&
          !Request_ems_health_state_pa_us_ConedClassreg_ClasswascanceledClassmaintenance
            (
            cookie_container,
            class_id,
            created_by,
            date_created,
            date_last_edited,
            date_submitted_to_region,
            document_status,
            last_edited_by,
            region_council_num,
            sponsor_id,
            course_id,
            initial_value_approved,
            initial_value_disapproval_reason_id,
            debug_session_emso_user_id,
            debug_session_user_role,
            debug_session_sponsor_id,
            debug_sponsor_info_editable,
            debug_region_info_editable,
            sponsor_name,
            sponsor_number,
            training_ins_accred_num,
            sponsor_county,
            course_title,
            not_valid_after_date,
            course_number,
            location,
            location_of_registration,
            location_address_1,
            location_address_2,
            location_city,
            location_state,
            location_zip,
            location_zip_plus_4,
            county_code,
            county_name,
            regional_council_name,
            location_phone,
            location_email,
            public_contact_name,
            public_contact_email,
            public_contact_phone,
            public_contact_website,
            public_contact_notes,
            student_cost,
            total_class_hours,
            total_class_hours_chk,
            length_of_course_in_hours,
            tuition_includes,
            closed,
            estimated_students,
            start_date_time,
            start_date_time_chk,
            start_time,
            end_date_time,
            end_date_time_chk,
            end_time,
            final_registration_date,
            instructors,
            instructor_qualifications,
            class_coordinator,
            primary_text,
            college_credit_awarded,
            held_on_sun,
            held_on_mon,
            held_on_tue,
            held_on_wed,
            held_on_thu,
            held_on_fri,
            held_on_sat,
            other_dates_and_times,
            date_received_by_region,
            ret_to_applicant_comment,
            date_sponsor_notified,
            approved,
            class_number,
            date_registration_sent_to_state,
            date_cards_sent_to_sponsor,
            date_materials_to_be_returned,
            disapproval_reason_id,
            region_comments,
            practical_exam_date,
            practical_exam_time,
            written_exam_date,
            written_exam_time,
            out response
            )
          //
        )
      // then
        {
        throw new Exception("Request_ems_health_state_pa_us_ConedClassreg_ClasswascanceledClassmaintenance() returned FALSE.");
        }
      response.Close();
      }

    }
  }
