using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicse.Json
{
    /// <summary>
    /// 
    /// JSON dictionary format:
    /// 
    /// {
    ///     "Language": "es-ar",
    ///     "Translations": {
    ///         "global": {
    ///             "original": "translated"
    ///         },
    ///         "[context]": {
    ///             "original": "translated"
    ///         }
    ///     }
    /// }
    /// 
    /// </summary>

    public class JsonDictionary : ITranslationDictionary
    {
        public string Language
        {
            get;
            set;
        }

        public Dictionary<string, Dictionary<string, string>> Translations
        {
            get;
            set;
        }

        public JsonDictionary(string language)
        {
            Language = language;
            Translations = new Dictionary<string, Dictionary<string, string>>();
        }
    }
}
