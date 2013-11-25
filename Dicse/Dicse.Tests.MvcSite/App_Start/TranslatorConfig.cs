using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dicse.Json;

namespace Dicse.Tests.MvcSite
{
    public class TranslatorConfig
    {
        public static void ConfigureTranslations(Translator t)
        {
            t.LoadFromFile("~/Translations/es-ar.json");
            t.DestinationLanguage = "es-ar";
        }
    }
}