using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Dicse.Xml
{
    public class XmlTranslator : Translator
    {
        public static XmlTranslator Default = new XmlTranslator();

        protected override void ParseAndRegister(string data)
        {
            var xd = XDocument.Parse(data);
            var lang = xd.Root.Attribute("language").Value;
            var dictionary = new XmlDictionary(lang);

            foreach (XElement item in xd.Root.Elements("contexts").Elements("context"))
            {
                var contextDic = new Dictionary<string, string>();
                foreach (XElement tx in item.Elements("entry"))
                {
                    contextDic.Add(tx.Attribute("key").Value, tx.Attribute("translated").Value);
                }
                dictionary.Translations.Add(item.Attribute("id").Value, contextDic);
            }

            RegisterDictionary(dictionary);
        }

        public override void RegisterDictionary(ITranslationDictionary dictionary)
        {
            if (dictionary is XmlDictionary)
            {
                base.RegisterDictionary(dictionary);
            }
        }
    }
}
