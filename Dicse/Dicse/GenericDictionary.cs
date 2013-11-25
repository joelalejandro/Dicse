using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicse
{
    internal class GenericDictionary : ITranslationDictionary
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

        public GenericDictionary(string language)
        {
            Language = language;
            Translations = new Dictionary<string, Dictionary<string, string>>();
        }
    }
}
