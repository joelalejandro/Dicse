using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dicse
{
    public abstract class Translator
    {
        protected string _DestinationLanguage;

        /// <summary>
        /// Language to use for the translation.
        /// </summary>
        public string DestinationLanguage
        {
            get
            {
                return _DestinationLanguage;
            }
            set
            {
                if (ExistsDictionaryForLanguage(value))
                {
                    _DestinationLanguage = value;
                }
                else
                {
                    throw new NoDictionariesForRequestedLanguageException();
                }
            }
        }

        protected List<ITranslationDictionary> _Dictionaries = new List<ITranslationDictionary>();

        /// <summary>
        /// Checks if there is a dictionary that can handle a specified language.
        /// </summary>
        /// <param name="language">Language code</param>
        /// <returns>true if exists, false otherwise</returns>
        protected bool ExistsDictionaryForLanguage(string language)
        {
            return !(Dictionaries.Count(p => p.Language == language) == 0);
        }

        /// <summary>
        /// Collection of registered dictionaries.
        /// </summary>
        public List<ITranslationDictionary> Dictionaries { get { return _Dictionaries; } }

        /// <summary>
        /// Registers a format-agnostic dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary object (can be JsonDictionary, XmlDictionary, or any implementation of ITranslationDictionary)</param>
        public virtual void RegisterDictionary(ITranslationDictionary dictionary)
        {
            if (ExistsDictionaryForLanguage(dictionary.Language))
            {
                throw new DictionaryForLanguageAlreadyRegisteredException();
            }
            Dictionaries.Add(dictionary);
        }

        /// <summary>
        /// Gets the first available dictionary for handling 
        /// </summary>
        /// <returns></returns>
        protected ITranslationDictionary GetDestinationLanguageDictionary()
        {
            try
            {
                return Dictionaries.Where(p => p.Language == DestinationLanguage).First();
            }
            catch
            {
                throw new NoDestinationLanguageSpecifiedException();
            }
        }

        /// <summary>
        /// Combines all translations for a language in a single GenericDictionary. (internal use only)
        /// </summary>
        /// <returns></returns>
        private GenericDictionary JoinTranslations()
        {
            var gd = new GenericDictionary(DestinationLanguage);
            foreach (var dictionary in Dictionaries.Where(p => p.Language == DestinationLanguage))
            {
                foreach (var translation in dictionary.Translations)
                {
                    gd.Translations.Add(translation.Key, translation.Value);
                }
            }
            return gd;
        }

        /// <summary>
        /// Registers multiple dictionaries.
        /// </summary>
        /// <param name="dictionaries"></param>
        public void RegisterDictionaries(params ITranslationDictionary[] dictionaries)
        {
            foreach (var dictionary in dictionaries)
            {
                RegisterDictionary(dictionary);
            }
        }

        /// <summary>
        /// Performs a translation.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Translate(string key)
        {
            try
            {
                if (key.Contains("|"))
                {
                    var contextKey = key.Split('|');
                    return JoinTranslations().Translations[contextKey[0]][contextKey[1]];
                }
                else
                {
                    return JoinTranslations().Translations["global"][key];
                }
            }
            catch (KeyNotFoundException)
            {
                return key;
            }
        }

        /// <summary>
        /// Performs a translation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Translate(string key, params object[] args)
        {
            try
            {
                if (key.Contains("|"))
                {
                    var contextKey = key.Split('|');
                    return String.Format(JoinTranslations().Translations[contextKey[0]][contextKey[1]], args);
                }
                else
                {
                    return String.Format(JoinTranslations().Translations["global"][key], args);
                }
            }
            catch (KeyNotFoundException)
            {
                return String.Format(key, args);
            }
        }

        protected abstract void ParseAndRegister(string data);

        /// <summary>
        /// Loads a dictionary from a file.
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFromFile(string filename)
        {
            ParseAndRegister(File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(filename)));
        }

        /// <summary>
        /// Loads a dictionary from a string.
        /// </summary>
        /// <param name="data"></param>
        public void LoadFromString(string data)
        {
            ParseAndRegister(data);
        }

        /// <summary>
        /// Loads a dictionary from a TextReader.
        /// </summary>
        /// <param name="tr"></param>
        public void LoadFromTextReader(TextReader tr)
        {
            ParseAndRegister(tr.ReadToEnd());
        }
    }
}
