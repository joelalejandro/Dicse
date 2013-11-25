using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dicse.Json;

namespace Dicse.Tests
{
    [TestClass]
    public class Json
    {
        [TestMethod]
        public void TestLoadFromString()
        {
            var jsonDicString = @"{
        ""Language"": ""es-ar"",
        ""Translations"": {
            ""global"": {
                ""new"": ""Nuevo"",
                ""remove"": ""Eliminar"",
                ""new {0}"": ""Nuevo {0}"",
                ""action {0} has been {1}"": ""La acción {0} ha sido {1}""
            },
            ""shopping"": {
                ""new"": ""Novedoso""
            }
        }
    }
";
            var jsonTranslator = new JsonTranslator();
            jsonTranslator.LoadFromString(jsonDicString);
            jsonTranslator.DestinationLanguage = "es-ar";
            Assert.AreEqual(jsonTranslator.Translate("new"), "Nuevo");
            Assert.AreEqual(jsonTranslator.Translate("shopping|new"), "Novedoso");
            Assert.AreEqual(jsonTranslator.Translate("new {0}", "movimiento"), "Nuevo movimiento");
            Assert.AreEqual(jsonTranslator.Translate("action {0} has been {1}", "Destruir el mundo", "Finalizada"), "La acción Destruir el mundo ha sido Finalizada");
        }
    }
}
