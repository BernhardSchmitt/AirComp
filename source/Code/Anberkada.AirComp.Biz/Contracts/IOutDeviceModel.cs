
namespace Anberkada.AirComp.Biz.Contracts
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Interface to manage an out device.
    /// </summary>
    public interface IOutDeviceModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the list of available out devices.
        /// </summary>
        /// <value>
        /// The available out devices.
        /// </value>
        IEnumerable<string> AvailableOutDevices { get; }

        /// <summary>
        /// Gets or sets the current out device.
        /// </summary>
        /// <value>
        /// The current out device.
        /// </value>
        string CurrentOutDevice { get; set; }

        /// <summary>
        /// Gets the list of available out channels.
        /// </summary>
        /// <value>
        /// The available out channels.
        /// </value>
        IEnumerable<int> AvailableOutChannels { get; } 

        /// <summary>
        /// Gets or sets the current out device channel.
        /// </summary>
        /// <value>
        /// The current out device channel.
        /// </value>
        int CurrentOutChannel { get; set; }

        /// <summary>
        /// Resets the out device.
        /// </summary>
        void ResetOutDevice();
    }
}