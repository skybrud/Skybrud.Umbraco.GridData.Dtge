﻿using Newtonsoft.Json.Linq;
using Our.Umbraco.DocTypeGridEditor.Helpers;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Dtge.Models;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Config;
using Skybrud.Umbraco.GridData.Models.Values;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Skybrud.Umbraco.GridData.Dtge.Converters {

    /// <summary>
    /// Grid converter for the DocTypeGridEditor.
    /// </summary>
    public class DtgeGridConverter : GridConverterBase {

        private readonly DocTypeGridEditorHelper _dtgeHelper;

        /// <inheritdoc/>
        public DtgeGridConverter(DocTypeGridEditorHelper dtgeHelper) {
            _dtgeHelper = dtgeHelper;
        }

        /// <summary>
        /// Converts the specified <paramref name="token"/> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">A reference to the parent <see cref="GridControl"/>.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted control value.</param>
        public override bool TryConvertControlValue(GridControl control, JToken token, [NotNullWhen(true)] out IGridControlValue? value) {
            value = null;
            if (IsDocTypeGridEditor(control.Editor)) {
                value = GridControlDtgeValue.Parse(control, _dtgeHelper);
            }
            return value != null;
        }

        /// <inheritdoc/>
        public override bool TryConvertEditorConfig(GridEditor editor, JToken token, [NotNullWhen(true)] out IGridEditorConfig? config) {
            config = null;
            if (IsDocTypeGridEditor(editor)) {
                config = GridEditorDtgeConfig.Parse(editor);
            }
            return config != null;
        }

        /// <inheritdoc/>
        public override bool TryGetValueType(GridControl control, [NotNullWhen(true)] out Type? type) {
            type = null;
            if (IsDocTypeGridEditor(control.Editor)) {
                type = typeof(GridControlDtgeValue);
            }
            return type != null;
        }

        /// <inheritdoc/>
        public override bool TryGetConfigType(GridEditor editor, [NotNullWhen(true)] out Type? type) {
            type = null;
            if (IsDocTypeGridEditor(editor)) {
                type = typeof(GridEditorDtgeConfig);
            }
            return type != null;
        }

        private bool IsDocTypeGridEditor(GridEditor? editor) {

            // The editor may be NULL if it no longer exists in a package.manifest file
            if (editor?.View == null) return false;

            const string view = "/App_Plugins/DocTypeGridEditor/Views/doctypegrideditor.html";

            return ContainsIgnoreCase(editor.View.Split('?')[0], view);

        }

    }

}