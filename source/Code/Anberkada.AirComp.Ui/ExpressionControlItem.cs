
namespace Anberkada.AirComp.Ui
{
    using Biz.Contracts;

    /// <summary>
    /// Represents a expression control item.
    /// </summary>
    public class ExpressionControlItem
    {
        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The tape identifier.
        /// </value>
        public ExpressionControlType TypeId { get; set; }

        /// <summary>
        /// Gets or sets the friendly name.
        /// </summary>
        /// <value>
        /// The friendly name.
        /// </value>
        public string FriendlyName { get; set;}
    }
}
