# Skybrud.Umbraco.GridData.Dtge

Add-on for [**Skybrud.Umbraco.GridData**](https://github.com/skybrud/Skybrud.Umbraco.GridData) that adds support for [**Doc Type Grid Editor**](https://github.com/skttl/umbraco-doc-type-grid-editor).

<br /><br />

## Installation

The package is only available via <a href="https://www.nuget.org/packages/Skybrud.Umbraco.GridData.Dtge" target="_blank">NuGet</a>. To install the package, you can use either .NET CLI:

dotnet add package Skybrud.Umbraco.GridData.Dtge --version 4.0.1

or the NuGet Package Manager in Visual Studio:

```
Install-Package Skybrud.Umbraco.GridData.Dtge -Version 4.0.2
```




<br /><br />

## Usage

Skybrud.Umbraco.GridData works by having a list of grid converters. The package contains a default grid converter which handles the editors that is installed with Umbraco, but also allows developers and other packages to add their own conveters to handle custom grid editors.

This package builds on top of that by introducing a `DtgeGridConverter`, that will then handle DTGE grid controls. Also utilizing the generic improvements made in [Skybrud.Umbraco.GridData v3.0.2](https://github.com/skybrud/Skybrud.Umbraco.GridData/releases/tag/v3.0.2), there are now a few different ways to working with strongly typed DTE based gird controls.

For instance in the example below, we can iterate through all the controls in a grid model. `control.Value` is defined as an instance of `IGridControlValue`, but given that this is an interface, it has a more specific type that depends on the underlying grid editor. For DTGE based grid controls, the value will be an instance if `GridControlDtgeValue`, which we can check for in an if statement.

`GridControlDtgeValue` then specifies the element value of the control. The type of the `GridControlDtgeValue.Element` property is `IPublishedElement`, but when using tools like ModelsBuilder, it may have an even more specific type, which we in a similar way can check for in a new if statement or switch case statement (`DtgeTest` is my test element type):

```csharp
@foreach (GridControl control in grid.GetAllControls()) {
    <div style="padding: 50px;">
        <table class="table details">
            @if (control.Value is GridControlDtgeValue dtge) {
                <tr>
                    <th>ID</th>
                    <td>@dtge.Id</td>
                </tr>
                <tr>
                    <th>Alias</th>
                    <td>@dtge.DtgeContentTypeAlias</td>
                </tr>
                <tr>
                    <th>Element</th>
                    <td>@dtge.Element</td>
                </tr>
                switch (dtge.Element) {
                    case DtgeTest test:
                        <tr>
                            <th>DTGE Test</th>
                            <td>@test.Title</td>
                        </tr>
                        break;
                }
            }  
            <tr>
                <th>Value</th>
                <td>@(control.Value?.GetType().ToString() ?? "NULL")</td>
            </tr>
            <tr>
                <th>Control</th>
                <td>@(control.GetType())</td>
            </tr>
        </table>
    </div>
}
```

### Indexing DTGE grid controls

Normally when using the Skybrud.Umbraco.GridData package for indexing grid content, it will work by asking the individual models implementing `IGridControlValue` to return a textual representation of the value of a given grid control (via the [GetSearchableText method](https://github.com/skybrud/Skybrud.Umbraco.GridData/blob/v3/latest/src/Skybrud.Umbraco.GridData/Interfaces/IGridControlValue.cs#L26)). But duye to the nature of DTGE based controls, this package can't know how to return a textual representation for models that it does not know about.

So to still allow this concept for DTGE based grid controls, `GridConverterBase` now contains a virtual `TryGetSearchableText` method that you can override in your own custom grid converter.

The method could be implemented with a switch case statement like en the example below, which then checks for the element types that it should handle - eg. my `DtgeTest` model. 

If the method encounters an element type it does know how to handle, it should return false. This ensures that the Skybrud.Umbraco.GridData will ask the next grid converter in the list of grid converters.

```csharp
using System;
using Skybrud.Umbraco.GridData.Converters;
using Umbraco.Core.Models.PublishedContent;
using WebApplication11.Models.Umbraco.Elements;

namespace WebApplication11.Grid {

    public class MyGridConverter : GridConverterBase {

        public override bool TryGetSearchableText(IPublishedElement element, out string text) {

            switch (element) {
                
                case DtgeTest dtge:
                    text = dtge.Title + Environment.NewLine;
                    return true;

                default:
                    text = null;
                    return false;

            }

        }

    }

}
```
