using Newtonsoft.Json.Linq;
using Our.Umbraco.DocTypeGridEditor.Helpers;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Values;
using Umbraco.Core.Models;

namespace Skybrud.Umbraco.GridData.Dtge{

    /// <summary>
    /// Class representing the value of a DocTypeGridEditor grid control. 
    /// </summary>
    public class GridControlDtgeValue : GridControlValueBase {

        #region Properties

        /// <summary>
        /// Gets a reference <see cref="IPublishedContent"/> of grid control.
        /// </summary>
        public IPublishedContent Content { get; }

        #endregion

        #region Constructors


        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        protected GridControlDtgeValue(GridControl control) : base(control, control.JObject) {
            
            JObject value = control.JObject.GetObject("value");

            string docTypeAlias = value.GetString("dtgeContentTypeAlias");

            string contentValue = value.GetObject("value").ToString();

            Content = DocTypeGridEditorHelper.ConvertValueToContent("0", docTypeAlias, contentValue);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a media value from the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        public static GridControlDtgeValue Parse(GridControl control) {
            return control == null ? null : new GridControlDtgeValue(control);
        }

        #endregion

    }

}