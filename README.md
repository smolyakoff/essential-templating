Essential Templating [![Analytics](https://ga-beacon.appspot.com/UA-61414371-1/smolyakoff/essential-templating)](https://github.com/igrigorik/ga-beacon)
=========

Essential Templating is a set of libraries to render templated objects (like web pages or emails).
  - Provides easy to use template engine interface with C# 5.0 async methods.
  - Unified template location (now you can use  files or .resx resources).
  - Localization support.

Essential.Templating.Razor
------------
Template engine implementation based on [RazorEngine by Antaris]

**Basic usage**
Template is located in "Hello.cshtml" file under "Templates" directory.

```html
@inherits Essential.Templating.Razor.Template
Hello, @ViewBag.Name!
```

How to render the template?

```csharp
ITemplateEngine engine = new RazorTemplateEngineBuilder()
                .FindTemplatesInDirectory("Templates")
                .CacheExpiresIn(TimeSpan.FromSeconds(20))
                .UseSharedCache()
                .Build();
string template = engine.Render(path: "Hello.cshtml", viewBag: new {Name = "John"});
```

What will we get as a result?
`Hello, John!` 

Essential.Templating.Razor.Email
---------------------------------------
Render razor pages as emails. See more at [wiki](https://github.com/smolyakoff/essential-templating/wiki/Email-Template-with-Razor).

*To be continued...*
[RazorEngine by Antaris]:https://github.com/Antaris/RazorEngine
