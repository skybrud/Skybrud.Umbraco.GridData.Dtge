---
sidebar: false
---

# Documentation

The idea with **Doc Type Grid Editor** is that each DTGE grid element is a wrapper for an instance of `IPublishedContent`, which then represents the properties of that grid element. There is good native support for strongly typed views, but there are still scenarious where you're still left with dynamic references - eg. when you need to work with grid elements outside of MVC views.


## Without Skybrud.Umbraco.GridData

When not using **Skybrud.Umbraco.GridData**, your view will typically inherit from `Umbraco.Web.Mvc.UmbracoViewPage<dynamic>`. Doc Type Grid Editor ships wit a main view :

```csharp
@using System.Web.Mvc
@using System.Web.Mvc.Html
@using Microsoft.CSharp.RuntimeBinder
@using Our.Umbraco.DocTypeGridEditor.Helpers
@using Our.Umbraco.DocTypeGridEditor.Web.Extensions
@inherits Umbraco.Web.Mvc.UmbracoViewPage<dynamic>
@if (Model.value != null)
{
    string id = Model.value.id.ToString();
    string editorAlias = Model.editor.alias.ToString();
    string contentTypeAlias = "";
    string value = Model.value.value.ToString();
    string viewPath = Model.editor.config.viewPath.ToString();

    try
    {
        contentTypeAlias = Model.value.dtgeContentTypeAlias.ToString();
    }
    catch (RuntimeBinderException)
    {
        contentTypeAlias = Model.value.docType.ToString();
    }

    if (contentTypeAlias != "")
    {
        var content = DocTypeGridEditorHelper.ConvertValueToContent(id, contentTypeAlias, value);

        @Html.RenderDocTypeGridEditorItem(content, editorAlias, viewPath)
    }
}
```

By using this addon for **Skybrud.Umbraco.GridData**, the same code could look like:

```csharp
@using Skybrud.Umbraco.GridData.Dtge
@using Skybrud.Umbraco.GridData.Dtge.Extensions
@inherits UmbracoViewPage<GridControl<IPublishedContent>>
@Html.RenderDocTypeGridEditorItem(Model)
```