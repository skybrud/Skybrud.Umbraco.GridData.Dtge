using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Dtge.Models
{
    /// <summary>
    /// Class representing a generic version of <see cref="GridControlDtgeValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Element"/>.</typeparam>
    public class GridControlDtgeValue<T> : GridControlDtgeValue where T : IPublishedElement {

        /// <summary>
        /// Gets a reference <see cref="IPublishedElement"/> of grid control.
        /// </summary>
        public new T Element { get; }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="element"/> and <paramref name="control"/>.
        /// </summary>
        /// <param name="element">An instance of <see cref="IPublishedElement"/> representing the value.</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        public GridControlDtgeValue(T element, GridControl control) : base(element, control) {
            Element = element;
        }

    }
}