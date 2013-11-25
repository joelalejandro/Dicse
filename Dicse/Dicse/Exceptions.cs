using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicse
{
    [Serializable]
    public class NoDictionariesForRequestedLanguageException : Exception
    {
        public NoDictionariesForRequestedLanguageException() { }
        public NoDictionariesForRequestedLanguageException(string message) : base(message) { }
        public NoDictionariesForRequestedLanguageException(string message, Exception inner) : base(message, inner) { }
        protected NoDictionariesForRequestedLanguageException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class DictionaryForLanguageAlreadyRegisteredException : Exception
    {
        public DictionaryForLanguageAlreadyRegisteredException() { }
        public DictionaryForLanguageAlreadyRegisteredException(string message) : base(message) { }
        public DictionaryForLanguageAlreadyRegisteredException(string message, Exception inner) : base(message, inner) { }
        protected DictionaryForLanguageAlreadyRegisteredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class NoDestinationLanguageSpecifiedException : Exception
    {
        public NoDestinationLanguageSpecifiedException() { }
        public NoDestinationLanguageSpecifiedException(string message) : base(message) { }
        public NoDestinationLanguageSpecifiedException(string message, Exception inner) : base(message, inner) { }
        protected NoDestinationLanguageSpecifiedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
