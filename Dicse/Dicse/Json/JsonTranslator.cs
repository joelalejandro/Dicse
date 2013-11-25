using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Dicse.Json
{
    public class JsonTranslator : Translator
    {
        public static JsonTranslator Default = new JsonTranslator();

        public JsonTranslator()
        {
         
        }

        protected override void ParseAndRegister(string data)
        {
            var json = JsonConvert.DeserializeObject(data);
            var lang = ((dynamic)json).Language.Value;
            var dictionary = new JsonDictionary(lang);

            var translations = ((dynamic)json).Translations;

            foreach (var item in (dynamic)translations)
            {
                var contextDic = new Dictionary<string, string>();
                foreach (var tx in (dynamic)item)
                {
                    foreach (var sx in tx.Properties())
                    {
                        contextDic.Add(sx.Name, sx.Value.ToString());
                    }
                }
                dictionary.Translations.Add(item.Name, contextDic);
            }

            RegisterDictionary(dictionary);
        }


        public override void RegisterDictionary(ITranslationDictionary dictionary)
        {
            if (dictionary is JsonDictionary)
            {
                base.RegisterDictionary(dictionary);
            }
        }
    }
}
