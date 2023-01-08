using Skybrud.Umbraco.GridData.Models;
using Umbraco.Cms.Core.Models.PublishedContent;


namespace Skybrud.Umbraco.GridData.Dtge.Models {
    
    /// <summary>
    /// Class representing a generic version of <see cref="GridControlDtgeValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Element"/>.</typeparam>
    public class GridControlDtgeValue<T> : GridControlDtgeValue where T : IPublishedElement {

        #region Properties

        /// <summary>
        /// Gets a reference <see cref="IPublishedElement"/> of grid control.
        /// </summary>
        public new T Element { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified DTGE <paramref name="value"/>.
        /// </summary>
        /// <param name="value">An instance of <see cref="GridControlDtgeValue"/> representing the value to wrap.</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        public GridControlDtgeValue(GridControlDtgeValue value, GridControl control) : base(value, control) {
            Element = (T) value.Element;
        }

        #endregion

    }

}