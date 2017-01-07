
namespace Anberkada.AirComp.Biz
{
    using System;
    
    using Leap;

    using Contracts;

    /// <summary>
    /// The listener for events discovered by touchless input device.
    /// </summary>
    public class TouchlessListener : Listener
    {
        private readonly AirCompModel _model;

        private readonly Object _lock = new Object();

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchlessListener"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public TouchlessListener(ICompModelBase model)
        {
            _model = model as AirCompModel;
		}

        #region Leap overrides

        /// <summary>
        /// Frame handler.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public override void OnFrame(Controller controller)
        {
            lock (_lock)
            {
                // Get the most recent frame and pass it to the model for interpretation
                _model.UpdateFromControllerDevice(controller.Frame());
            }
        }

        #endregion // Leap overrides
    }
}
