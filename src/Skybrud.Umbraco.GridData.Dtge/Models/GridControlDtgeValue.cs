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

        private readonly GridContext _gridContext;
        private readonly DocTypeGridEditorHelper _dtgeHelper;

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
        /// <param name="context"></param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="dtgeHelper"></param>
        protected GridControlDtgeValue(GridContext context, GridControl control, DocTypeGridEditorHelper dtgeHelper) : base(control, control.JObject) {

            _dtgeHelper = dtgeHelper;

            JObject value = control.JObject.GetObject("value");

            Id = value.GetGuid("id");

            DtgeContentTypeAlias = value.GetString("dtgeContentTypeAlias");

            string contentValue = value.GetObject("value").ToString();

            Element = _dtgeHelper.ConvertValueToContent(Id.ToString(), DtgeContentTypeAlias, contentValue);

            this._gridContext = context;
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
        public override string GetSearchableText(GridContext context) {
            return Element == null ? Environment.NewLine : GetSearchableText(_gridContext);
        }


        #endregion

        #region Static methods

        /// <summary>
        /// Gets a media value from the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="gridContext"></param>
        /// <param name="control">The parent control.</param>
        /// <param name="dtgeHelper"></param>
        public static GridControlDtgeValue Parse(GridContext gridContext, GridControl control, DocTypeGridEditorHelper dtgeHelper) {

            if (control == null) return null;

            GridControlDtgeValue value = new GridControlDtgeValue(gridContext, control, dtgeHelper);
            if (value.Element == null) return value;

            // Get the generic type that we wish to instantiate
            Type type = typeof(GridControlDtgeValue<>).MakeGenericType(value.Element.GetType());

            // Create a generic DTGE value instance
            return (GridControlDtgeValue)Activator.CreateInstance(type, value, control);

        }

        #endregion

    }

}