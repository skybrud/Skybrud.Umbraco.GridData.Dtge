using System;
using Newtonsoft.Json.Linq;
using Our.Umbraco.DocTypeGridEditor.Helpers;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Values;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Dtge.Models {

    /// <summary>
    /// Class representing the value of a DocTypeGridEditor grid control.
    /// </summary>
    public class GridControlDtgeValue : GridControlValueBase {

        #region Properties

        /// <summary>
        /// Gets the unique ID of this DTGE value.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the alias of the underlying element type.
        /// </summary>
        public string DtgeContentTypeAlias { get; }

        /// <summary>
        /// Gets a reference <see cref="IPublishedElement"/> of grid control.
        /// </summary>
        public IPublishedElement Element { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="id">The unique ID of the DTGE value.</param>
        /// <param name="contentTypeAlias">The alias of the underlying content type.</param>
        /// <param name="element">The <see cref="IPublishedElement"/> representing the parsed DTGE value.</param>
        protected GridControlDtgeValue(GridControl control, Guid id, string contentTypeAlias, IPublishedElement element) : base(control) {
            Id = id;
            DtgeContentTypeAlias = contentTypeAlias;
            Element = element;
        }

        /// <summary>
        /// Initializes a new instance based on the specified DTGE <paramref name="value"/>.
        /// </summary>
        /// <param name="value">An instance of <see cref="GridControlDtgeValue"/> representing the value to wrap.</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        protected GridControlDtgeValue(GridControlDtgeValue value, GridControl control) : base(control) {
            Id = value.Id;
            DtgeContentTypeAlias = value.DtgeContentTypeAlias;
            Element = value.Element;
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override string GetSearchableText(GridContext context) {
            return context.GetSearchableText(Element);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a media value from the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="dtgeHelper"></param>
        public static GridControlDtgeValue? Parse(GridControl? control, DocTypeGridEditorHelper dtgeHelper) {

            // Get a reference to the grid control value
            JObject? value = control?.JObject.GetObject("value");
            if (value is null) return null;

            // Read and parse the raw DTGE values
            Guid id = value.GetGuid("id");
            string contentTypeAlias = value.GetString("dtgeContentTypeAlias")!;
            string? contentValue = value.GetObject("value")?.ToString();
            if (string.IsNullOrWhiteSpace(contentTypeAlias)) return null;
            if (string.IsNullOrWhiteSpace(contentValue)) return null;

            // Convert the DTGE value to a IPublishedElement
            IPublishedElement? element = dtgeHelper.ConvertValueToContent(id.ToString(), contentTypeAlias, contentValue);
            if (element is null) return null;

            // Initialize a new grid control value
            GridControlDtgeValue dtge = new(control!, id, contentTypeAlias, element);

            // Get the generic type that we wish to instantiate
            Type type = typeof(GridControlDtgeValue<>).MakeGenericType(element.GetType());

            // Create a generic DTGE value instance
            return (GridControlDtgeValue) Activator.CreateInstance(type, dtge, control)!;

        }

        #endregion

    }

}