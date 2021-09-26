using System;
using Newtonsoft.Json.Linq;
using Our.Umbraco.DocTypeGridEditor.Helpers;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Values;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Dtge.Models{

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
        protected GridControlDtgeValue(GridControl control) : base(control, control.JObject) {
            
            JObject value = control.JObject.GetObject("value");

            Id = value.GetGuid("id");

            DtgeContentTypeAlias = value.GetString("dtgeContentTypeAlias");

            string contentValue = value.GetObject("value").ToString();

            Element = DocTypeGridEditorHelper.ConvertValueToContent(Id.ToString(), DtgeContentTypeAlias, contentValue);

        }
        
        /// <summary>
        /// Initializes a new instance based on the specified DTGE <paramref name="value"/>.
        /// </summary>
        /// <param name="value">An instance of <see cref="GridControlDtgeValue"/> representing the value to wrap.</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        protected GridControlDtgeValue(GridControlDtgeValue value, GridControl control) : base(control, control.JObject) {
            Id = value.Id;
            DtgeContentTypeAlias = value.DtgeContentTypeAlias;
            Element = value.Element;
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override string GetSearchableText() {
            return Element == null ? Environment.NewLine : GridContext.Current.GetSearchableText(Element);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a media value from the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        public static GridControlDtgeValue Parse(GridControl control) {

            if (control == null) return null;

            GridControlDtgeValue value = new GridControlDtgeValue(control);
            if (value.Element == null) return value;

            // Get the generic type that we wish to instantiate
            Type type = typeof(GridControlDtgeValue<>).MakeGenericType(value.Element.GetType());

            // Create a generic DTGE value instance
            return (GridControlDtgeValue) Activator.CreateInstance(type, value, control);

        }

        #endregion

    }

}