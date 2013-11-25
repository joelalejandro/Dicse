using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dicse.Xml
{
    public static class XmlTranslationHelper
    {
        public static MvcHtmlString Translate(this HtmlHelper helper, string key)
        {
            return MvcHtmlString.Create(XmlTranslator.Default.Translate(key));
        }

        public static MvcHtmlString Translate(this HtmlHelper helper, string key, params object[] args)
        {
            return MvcHtmlString.Create(XmlTranslator.Default.Translate(key, args));
        }
    }
}
