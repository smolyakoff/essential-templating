# Essential Templating

Essential Templating is a set of libraries to render templated objects (like web pages, documents or emails).

- Allows to render complex objects from a single-file template.
- Provides easy to use template engine interface with C# 5.0 async methods.
- Unified template location (you can use files or .resx resources).
- Localization support.

## ⚠️ Deprecation Notice

This package is not actively maintained anymore due to lack of time and the amount of existing libraries that do similar things. There are many alternatives you can consider:

- [RazorEngine](https://github.com/Antaris/RazorEngine)
- [RazorGenerator](https://github.com/RazorGenerator/RazorGenerator)
- [RazorLight](https://github.com/toddams/RazorLight)
- [FluentEmail](https://github.com/lukencode/FluentEmail)
- [Scriban](https://github.com/lunet-io/scriban)

## Render text using Razor templates

The [Essential.Templating.Razor](https://www.nuget.org/packages/Essential.Templating.Razor) package allows you to render text using Razor templates. The implementation is based on [RazorEngine](https://github.com/Antaris/RazorEngine) which makes it easy to render templates without using ASP.NET MVC.

Assuming the template is located in `Hello.cshtml` file under `Templates` directory. Make sure the template is copied to the output directory.

```html
@inherits Essential.Templating.Razor.Template
Hello, @ViewBag.Name!
```

To render a template to a `System.String` create an instance of `ITemplateEngine` and call the `Render` method.

```csharp
ITemplateEngine engine = new RazorTemplateEngineBuilder()
                .FindTemplatesInDirectory("Templates")
                .CacheExpiresIn(TimeSpan.FromSeconds(20))
                .UseSharedCache()
                .Build();
string template = engine.Render(path: "Hello.cshtml", viewBag: new {Name = "John"});
```

The rendered result should look as follows:

```txt
Hello, John!
```

## Render emails using Razor templates

The [Essential.Templating.Razor.Email](https://www.nuget.org/packages/Essential.Templating.Razor) package allows to render `System.Net.MailMessage` instances from Razor templates. Both HTML and text bodies are supported and can be specified using Razor section syntax.

Assuming that you have a following template in `Email.cshtml` file under `Templates` directory.

```html
@inherits Essential.Templating.Razor.Email.EmailTemplate
@using System.Net;
@{
    From = new MailAddress("example@email.com");
    Subject = "Email Subject";
}
@section Html
{
   <html>
      <head>
          <title>Example</title>
      </head>
      <body>
          <h1>HTML part of the email</h1>
      </body>
   </html>
}
@section Text 
{
    Text part of the email.
}
```

To get a `System.Net.MailMessage` object from this template use the following code:

```csharp
ITemplateEngine engine = new RazorTemplateEngineBuilder()
                            .FindTemplatesInDirectory("Templates")
                            .Build();
MailMessage email = engine.RenderEmail("Email.cshtml");
```
