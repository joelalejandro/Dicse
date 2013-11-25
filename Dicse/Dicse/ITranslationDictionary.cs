using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicse
{
    public interface ITranslationDictionary
    {
        string Language { get; set; }
        Dictionary<string, Dictionary<string, string>> Translations { get; set; }
    }
}
