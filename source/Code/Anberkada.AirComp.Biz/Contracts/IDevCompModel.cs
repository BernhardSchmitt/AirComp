

namespace Anberkada.AirComp.Biz.Contracts
{
    using System;

    /// <summary>
    /// Interface for the device specific model of the composer.
    /// </summary>
    public interface IDevCompModel<in TDeviceFrame> : ICompModelBase, IDisposable
    {
        /// <summary>
        /// Initializes the controller device of this instance.
        /// </summary>
        void InitializeControllerDevice();

        /// <summary>
        /// Updates the model state from the controller device.
        /// </summary>
        /// <param name="deviceFrame">The device specific frame with the source data.</param>
        void UpdateFromControllerDevice(TDeviceFrame deviceFrame);
    }
}
