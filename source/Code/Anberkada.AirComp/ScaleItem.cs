
namespace Anberkada.AirComp
{
    using Biz.Contracts;

    /// <summary>
    /// Represents a scale item.
    /// </summary>
    public class ScaleItem
    {
        /// <summary>
        /// Gets or sets the scale identifier.
        /// </summary>
        /// <value>
        /// The scale identifier.
        /// </value>
        public ScaleModeNames ScaleId { get; set; }

        /// <summary>
        /// Gets or sets the friendly name.
        /// </summary>
        /// <value>
        /// The friendly name.
        /// </value>
        public string FriendlyName { get; set;}
    }
}
