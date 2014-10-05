relation-scaffolding
====================

Relation dependencies in EntityFramework is common taks.
This package will create a select box, or checkboxes depending if you have a One2One or One2Many relationship.

This package hooks up with MVC5.

# Installation
Run the following command
```
Install-Package relation-scaffolding
```

The current package does not setup everything automatically. Please keep reading to understand
what has to be done manually to use this package.

The package will install the following:
 * DLL reference
 * Templates in /Views/Shared/[EditorTemplates|DisplayTemplates]
   * Feel free to modify those templates to your needs, note that this project is in beta and those files will most likely be updated later
   * If you use this inside an Area, you do not need to move those files. They will be picked up automatically.
 * One JavaScript file /Scripts/relation.js

## Manual Steps
Install the JavaScript in your bundles

BUndleConfig.cs
```c#
bundles.Add(new ScriptBundle("~/bundles/relation").Include(
            "~/Scripts/relation.js",
            "~/Scripts/site.js"));
```

_Layout.cshtml
```cshtml
@Scripts.Render("~/bundles/admin")
```

# Usage
After everything is setup, you can name your property that you want to get a relation displayed.

## Classes
```c#
public class Book
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [RelationScaffolding.RelationOne2One(Empty = "")]
    public Member Author { get; set; }

    [RelationScaffolding.RelationOne2Many]
    public virtual ICollection<Member> Followers { get; set; }
}
```

On the Member class, you have to mention which property is the key, and which one you want to display.
```c#
public class Member
{
    [Key]
    public long Id { get; set; }

    [Required]
    [RelationScaffolding.RelationDisplay]
    public string Name { get; set; }
}
```

If you do not set the `[Key]` attribute, we try to find a property that ends with `Id`.

## View
The current scaffolding is not automatically populating the view for relations.
Use the same code as a primite would use to generate relations.

Use something like this:
```cshtml
  @Html.EditorFor(model => model.Author, new { htmlAttributes = new { @class = "form-control" } })
```

If you are dealing with a `RelationOne2Many`, you probably want to get the whole list of items. Use the following:
```cshtml
@Html.EditorFor(model => model.Followers, new { list = ViewBag.AllMembers, htmlAttributes = new { @class = "form-control" } })
```

In your controller:
```c#
ViewBag.AllMembers = dbContext.Members.ToList();
```

## Controller Model Binding
I am actively working on this.


Roadmap
-------
 * Model Binder
 * Make "CanAdd" more generic
 * Support Many 2 Many
 * Add a new Scaffolding template T4
 * Better support of styles
 * I18n
 * Unit Tests
 