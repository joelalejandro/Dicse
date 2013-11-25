using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicse.Xml
{
    /// <summary>
    /// 
    /// XML dictionary format:
    /// 
    /// <?xml version="1.0">
    /// <translations language="es-ar">
    ///    <contexts>
    ///        <context id="global">
    ///             <entry key="..." translated="..."></entry>
    ///        </context>
    ///    </contexts>
    /// </translations>
    /// 
    /// </summary>
    public class XmlDictionary : ITranslationDictionary
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

        public XmlDictionary(string language)
        {
            Language = language;
            Translations = new Dictionary<string, Dictionary<string, string>>();
        }
    }
}
