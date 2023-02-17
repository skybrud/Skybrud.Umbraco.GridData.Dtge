using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Config;

namespace Skybrud.Umbraco.GridData.Dtge.Models {

    /// <summary>
    /// Class representing the configuration of a DTGE grid editor.
    /// </summary>
    public class GridEditorDtgeConfig : GridEditorConfigBase {

        #region Properties

        /// <summary>
        /// Gets an array of doc type aliases of which should be allowed to be selected in the grid editor.
        /// </summary>
        public IReadOnlyList<string> AllowedDocTypes { get; }

        /// <summary>
        /// Gets the naming template of the grid editor.
        /// </summary>
        public string? NameTemplate { get; }

        /// <summary>
        /// Gets whether rendering a preview of the grid cell in the grid editor is enabled.
        /// </summary>
        public bool EnablePreview { get; }

        /// <summary>
        /// Gets the view path of the editor.
        /// </summary>
        public string? ViewPath { get; }

        /// <summary>
        /// Gets the preview view path of the editor.
        /// </summary>
        public string? PreviewViewPath { get; }

        /// <summary>
        /// Gets the preview CSS file path of the grid editor.
        /// </summary>
        public string? PreviewCssFilePath { get; }

        /// <summary>
        /// Gets the preview JS file path of the grid editor.
        /// </summary>
        public string? PreviewJsFilePath { get; }

        #endregion

        #region Constructors

        private GridEditorDtgeConfig(GridEditor editor, JObject obj) : base(obj, editor) {
            AllowedDocTypes = obj.GetStringArray("allowedDocTypes");
            NameTemplate = obj.GetString("nameTemplate");
            EnablePreview = obj.GetBoolean("enablePreview");
            ViewPath = obj.GetString("viewPath");
            PreviewViewPath = obj.GetString("previewViewPath");
            PreviewCssFilePath = obj.GetString("previewCssFilePath");
            PreviewJsFilePath = obj.GetString("previewJsFilePath");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorDtgeConfig"/> from the specified <paramref name="editor"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        [return: NotNullIfNotNull(nameof(editor))]
        public static GridEditorDtgeConfig? Parse(GridEditor? editor) {
            return editor == null ? null : new GridEditorDtgeConfig(editor, editor.JObject.GetObject("config")!);
        }

        #endregion

    }

}