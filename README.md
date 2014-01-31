Essential Templating
=========

Essential.Templating is a wrapper library for [RazorEngine by Antaris]

  - Provides easy to use template engine interface with C# 5.0 async methods.
  - Supports layouts out of the box.
  - Localization support.

Example
------------
Assume that we have a following template in "Hello.cshtml" file inside "Templates" directory:
```html
@inherits Essential.Templating.Template
Hello, @ViewBag.Name!
```
How to render the template?
```csharp
var engine = new TemplateEngineBuilder()
                .FindTemplatesInDirectory("Templates")
                .CacheExpiresIn(TimeSpan.FromSeconds(20))
                .UseSharedCache()
                .Build();
var template = engine.Render(path: "Hello.cshtml", viewBag: new {Name = "John"});
```
What will we get as a result?

`Hello, John!` 

*To be continued...*
[RazorEngine by Antaris]:https://github.com/Antaris/RazorEngine