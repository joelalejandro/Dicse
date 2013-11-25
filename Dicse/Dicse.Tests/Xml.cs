using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dicse.Xml;

namespace Dicse.Tests
{
    [TestClass]
    public class Xml
    {
        [TestMethod]
        public void TestXmlFromString()
        {
            var xmlDicString = @"<?xml version=""1.0""?>
<translations language=""es-ar"">
    <contexts>
        <context id=""global"">
            <entry key=""new"" translated=""Nuevo""/>
            <entry key=""remove"" translated=""Eliminar""/>
            <entry key=""new {0}"" translated=""Nuevo {0}""/>
            <entry key=""action {0} has been {1}"" translated=""La acción {0} ha sido {1}""/>
        </context>
        <context id=""shopping"">
            <entry key=""new"" translated=""Novedoso""/>
        </context>
    </contexts>
</translations>";
            var xmlTranslator = new XmlTranslator();
            xmlTranslator.LoadFromString(xmlDicString);
            xmlTranslator.DestinationLanguage = "es-ar";
            Assert.AreEqual(xmlTranslator.Translate("new"), "Nuevo");
            Assert.AreEqual(xmlTranslator.Translate("shopping|new"), "Novedoso");
            Assert.AreEqual(xmlTranslator.Translate("new {0}", "movimiento"), "Nuevo movimiento");
            Assert.AreEqual(xmlTranslator.Translate("action {0} has been {1}", "Destruir el mundo", "Finalizada"), "La acción Destruir el mundo ha sido Finalizada");
        }
    }
}
