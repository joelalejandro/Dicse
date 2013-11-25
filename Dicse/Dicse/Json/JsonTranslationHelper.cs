using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dicse.Json
{
    public static class JsonTranslationHelper
    {
        public static MvcHtmlString Translate(this HtmlHelper helper, string key)
        {
            return MvcHtmlString.Create(JsonTranslator.Default.Translate(key));
        }

        public static MvcHtmlString Translate(this HtmlHelper helper, string key, params object[] args)
        {
            return MvcHtmlString.Create(JsonTranslator.Default.Translate(key, args));
        }
    }
}
