Dicse
=====

A straight-forward translation engine for ASP.NET MVC.

## Installation

Install the NuGet package:

<pre><code>PM&gt; Install-Package Dicse</code></pre>

## Creating a dictionary

Dicse currently supports two dictionary formats: JSON and XML. Each dictionary contains one language,
and multiple contexts.

You'd use the context for translating the same word under different subjects. For instance, the word
`bit` can be interpreted as the basic unit of information in computing or as a tiny piece of something.
Thus, you'd use the global context for the default behaviour, and a different context for the specific
meaning.

Also, contexts allow you to group translation keys by subject or by anything that suits you best.

### The JSON dictionary

<pre><code>{
  "Language": "es-es",
  "Translations": {
    "global": {
      "Hello": "Hola",
      "Goodbye": "Adiós",
      "Hello {0}": "Hola {0}"
    },
    "family": {
      "Hello": "Hey, qué tal?",
      "Goodbye": "Hasta luego!"
    }
  }
}</code></pre>

### The XML dictionary
<pre><code>&lt;?xml version="1.0">
&lt;translations language="es-es">
  &lt;contexts>
    &lt;context id="global">
      &lt;entry key="Hello" translated="Hola"/>
      &lt;entry key="Goodbye" translated="Adiós"/>
      &lt;entry key="Hello {0}" translated="Hola {0}"/>
    &lt;/context>
    &lt;context id="family">
      &lt;entry key="Hello" translated="Hey, qué tal?"/>
      &lt;entry key="Goodbye" translated="Hasta luego!"/>
    &lt;/context>
  &lt;/contexts>
&lt;/translations>
</code></pre>

## Configuring Dicse

In order for Dicse to work, the dictionaries must be loaded first.

A recommended way to do this is to create a `TranslatorConfig` class in the `App_Start` folder
of your MVC project, containing the following:

<pre><code>public class TranslatorConfig
{
    public static void ConfigureTranslations(Translator t)
    {
        // Load a translation file.
        t.LoadFromFile("~/Translations/es-ar.json");
        
        // Set a default language.
        t.DestinationLanguage = "es-ar";
    }
}</code></pre>

Afterwards, you must call the `ConfigureTranslations` method in your `Global.asax` file:

<pre><code>public class MvcApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
        // ... all other inits ...
        
        TranslatorConfig.ConfigureTranslations(JsonTranslator.Default);
        // or
        TranslatorConfig.ConfigureTranslations(XmlTranslator.Default);
    }
}</code></pre>

In the `Application_Start` event, you must define which translator engine to use (JSON or XML). *You cannot mix
between dictionary formats, you must pick one.*

## Localizing your text

To use Dicse in your views, simply use the `Translate` method in the `Html` helper:

<pre><code>@Html.Translate("Hello")</code></pre>

You can also translate with tokens:

<pre><code>@Html.Translate("Hello {0}", "John Doe")</code></pre>

If you need to reference a specifix context, prepend the context name along with a pipe (|) to the translation key:

<pre><code>@Html.Translate("family|Hello")</code></pre>

You must include `@using Dicse.Json;` or `@using Dicse.Xml;` at the top of your view so Razor can recognise the
extension methods of `HtmlHelper`.

### Optional: Register the Dicse namespace in your Razor Views web.config file

If you want to avoid having to add the `@using Dicse.Json;` or `@using Dicse.Xml;` on every view you wish to
translate, you must register the Dicse namespace on the `<pages>` section of `Views/web.config`:

<pre><code>&lt;system.web.webPages.razor>
  &lt;host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
  &lt;pages pageBaseType="System.Web.Mvc.WebViewPage">
    &lt;namespaces>
      &lt;!-- 
      ...
      ...
      other namespaces
      ...
      ... -->
      &lt;add namespace="Dicse.Json" /> &lt;!-- or &lt;add namespace="Dicse.Xml" /> -->
    &lt;/namespaces>
  &lt;/pages>
&lt;/system.web.webPages.razor></code></pre>

**Important:** If you have your view file open while performing the forementioned changes to the Web.config file,
**close** all view files and then reopen them, otherwise Razor won't recognise the `Translate` method.

## License

Dicse is licensed under the MIT License.

## Contribute

Feel free to fork this project, report issues and propose pull requests!
