namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using Crosscutting.Logging;
    using Crosscutting.NetFramework.Logging;
    using Crosscutting.NetFramework.Validator;
    using Crosscutting.Validator;
    using Infrastructure;

    public class BaseController : Controller
	{
		public const string PromptMessageKey = "message";
		private SupportedLanguage _currentLanguage;

		public BaseController()
		{
			EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
			LoggerFactory.SetCurrent(new Log4NetLogFactory());
		}

		protected void SetPromptMessage(string message)
		{
			TempData[PromptMessageKey] = message;
		}

		public static List<SupportedLanguage> Languages
		{
			get
			{

				if (HttpRuntime.Cache["SupportedLanguages"] == null)
				{
					var ls = System.Configuration.ConfigurationManager.AppSettings["SupportedLanguages"];
					var temp = new List<SupportedLanguage>();
					var parts = ls.Split(';');
					foreach (var part in parts)
					{
						var kv = part.Split(',');
						if (kv.Length == 2)
						{
							temp.Add(new SupportedLanguage { LanguageCode = kv[0], LanguageName = kv[1] });
						}
						if (temp.Count == 0)
						{
							temp.Add(new SupportedLanguage { LanguageCode = "zh-CN", LanguageName = "简体中文" });
						}
					}
					HttpRuntime.Cache.Add("SupportedLanguages", temp, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(365, 0, 0, 0), System.Web.Caching.CacheItemPriority.Normal, null);
				}
				return HttpRuntime.Cache["SupportedLanguages"] as List<SupportedLanguage>;
			}
		}

		/// <summary>
		/// 根据用户设定设置当前语言
		/// </summary>
		/// <param name="request">Http请求</param>
		/// <param name="response">Http反馈</param>
		/// <remarks>
		/// </remarks>
		private void SetCurrentLanguage(HttpRequestBase request, HttpResponseBase response)
		{
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_currentLanguage.LanguageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Thread.CurrentThread.CurrentUICulture.Name);
		}

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);

            var parts = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"].Split(',');

		    _currentLanguage = new SupportedLanguage
		        {
                    LanguageCode = parts[0],
                    LanguageName = parts[1]
		        };

			SetCurrentLanguage(requestContext.HttpContext.Request, requestContext.HttpContext.Response);
		}
	}
}
