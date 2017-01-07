
namespace Anberkada.AirComp.Ui
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    using Biz.Contracts;
    using MusicBase.Biz.Contracts;
    using MusicBase.Biz.Scales;
    using Stubs;

    /// <summary>
    /// View model of the air composer.
    /// </summary>
    public class AirCompViewModel : ViewModelBase
    {
        private readonly IEnumerable<ScaleItem> _scaleSelectionList;
        private readonly IEnumerable<ExpressionControlItem> _expressionControlItems;
        private ICompModelBase _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirCompViewModel"/> class.
        /// </summary>
        public AirCompViewModel()
        {
            _scaleSelectionList = CreateScaleSelectionList();
            _expressionControlItems = CreateExpressionControlItems();

            ResetCommand = new DelegateCommand(x => OutDeviceModel.ResetOutDevice());
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public ICompModelBase Model
        {
            get
            {
                if (_model == null)
                {
                    // Create stub to support preview in XAML editor
                    _model = new AirCompModelStub();
                }

                return _model;
            }

            set
            {
                if (value != _model)
                {
                    _model = value;
                    if (_model != null)
                    {
                        _model.PropertyChanged += ModelOnPropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current pitch name.
        /// </summary>
        /// <value>
        /// The current pitch name.
        /// </value>
        public string CurrentPitchName
        {
            get
            {
                if (Model.CurrentPitch == null)
                {
                    return string.Empty;
                }

                return Model.CurrentPitch.ToString();
            }
        }

        /// <summary>
        /// Gets the current normalized pitch value in the range of 0..1.0.
        /// </summary>
        /// <value>
        /// The current pitch value.
        /// </value>
        public double CurrentPitchValue
        {
            get
            {
                if (Model.CurrentPitch == null)
                {
                    return double.NaN;
                }

                return Model.CurrentPitch.Value / (double)(Pitch.MaxValue);
            }
            set
            {
                Model.CurrentPitch = Model.GetPitchByNormalizedPosition(value);
            }
        }

        /// <summary>
        /// Gets or sets the current scale.
        /// </summary>
        /// <value>
        /// The current scale.
        /// </value>
        public ScaleItem CurrentScale
        {
            get
            {
                if (Model.CurrentScale == null)
                {
                    return null;
                }

                return GetScaleItem(Model.CurrentScale.Scale);
            }

            set
            {
                if (value != null)
                {
                    Model.ChangeScale(CurrentBaseTone, value.ScaleId);
                }
            }
        }

        /// <summary>
        /// Gets the available scale items.
        /// </summary>
        /// <value>
        /// The available scale items.
        /// </value>
        public IEnumerable<ScaleItem> AvailableScaleItems
        {
            get
            {
                return _scaleSelectionList;
            }
        }

        /// <summary>
        /// Gets the current base tone.
        /// </summary>
        /// <value>
        /// The current base tone.
        /// </value>
        public Tone CurrentBaseTone
        {
            get
            {
                return Model.CurrentScale.BaseTone;
            }

            set
            {
                if (value != Model.CurrentScale.BaseTone)
                {
                    Model.ChangeScale(value, Model.CurrentScale.Scale);
                }
            }
        }

        /// <summary>
        /// Gets the available base tones.
        /// </summary>
        /// <value>
        /// The available base tones.
        /// </value>
        public IEnumerable<Tone> AvailableBaseTones
        {
            get
            {
                for (var i = Tone.C; i <= Tone.B; i++)
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current expression control item.
        /// </summary>
        /// <value>
        /// The current expression control item.
        /// </value>
        public ExpressionControlItem CurrentExpressionControlItem
        {
            get
            {
                return GetExpressionControlItem(Model.CurrentExpressionControlType);
            }
            set
            {
                Model.CurrentExpressionControlType = value.TypeId;
            }
        }

        /// <summary>
        /// Gets the list of available expression control items.
        /// </summary>
        /// <value>
        /// The available expression control items.
        /// </value>
        public IEnumerable<ExpressionControlItem> AvailableExpressionControlItems
        {
            get { return _expressionControlItems; }
        }

        /// <summary>
        /// Gets the current amplitude in the range of 0..1.0.
        /// </summary>
        /// <value>
        /// The current amplitude.
        /// </value>
        public double CurrentAmplitude
        {
            get
            {
                return Model.CurrentAmplitude;
            }
            set
            {
                Model.CurrentAmplitude = value;
            }
        }

        /// <summary>
        /// Gets or sets the current expression.
        /// </summary>
        /// <value>
        /// The current expression.
        /// </value>
        public double CurrentExpression
        {
            get
            {
                return Model.CurrentExpression;
            }
            set
            {
                Model.CurrentExpression = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the current out device.
        /// </summary>
        /// <value>
        /// The name of the current out device.
        /// </value>
        public string CurrentOutDeviceName
        {
            get
            {
                return OutDeviceModel.CurrentOutDevice;
            }
            set
            {
                OutDeviceModel.CurrentOutDevice = value;
            }
        }

        /// <summary>
        /// Gets the list of available out devices.
        /// </summary>
        /// <value>
        /// The available out devices.
        /// </value>
        public IEnumerable<string> AvailableOutDevices
        {
            get
            {
                return OutDeviceModel.AvailableOutDevices;
            }
        }

        /// <summary>
        /// Gets or sets the current out channel.
        /// </summary>
        /// <value>
        /// The current out channel.
        /// </value>
        public int CurrentOutChannel
        {
            get
            {
                return OutDeviceModel.CurrentOutChannel;
            }
            set
            {
                OutDeviceModel.CurrentOutChannel = value;
            }
        }

        /// <summary>
        /// Gets the list of available out channels.
        /// </summary>
        /// <value>
        /// The available out channels.
        /// </value>
        public IEnumerable<int> AvailableOutChannels
        {
            get
            {
                return OutDeviceModel.AvailableOutChannels;
            }
        }

        /// <summary>
        /// Gets the reset command.
        /// </summary>
        /// <value>
        /// The reset command.
        /// </value>
        public ICommand ResetCommand { get; private set; }

        /// <summary>
        /// Gets the out device model.
        /// </summary>
        /// <value>
        /// The out device model.
        /// </value>
        private IOutDeviceModel OutDeviceModel
        {
            get { return (IOutDeviceModel)Model; }
        }

        #region Private methods

        /// <summary>
        /// Handles property changed notification from model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyChangedEventArgs">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "CurrentPitch":
                    RaisePropertyChanged("CurrentPitchName");
                    RaisePropertyChanged("CurrentPitchValue");
                    break;
                case "CurrentScale":
                    RaisePropertyChanged("CurrentScale");
                    RaisePropertyChanged("CurrentBaseTone");
                    break;
                case "CurrentAmplitude":
                    RaisePropertyChanged("CurrentAmplitude");
                    break;
                case "CurrentExpression":
                    RaisePropertyChanged("CurrentExpression");
                    break;
            }
        }

        /// <summary>
        /// Creates the expression control items.
        /// </summary>
        /// <returns>The created list.</returns>
        private IEnumerable<ExpressionControlItem> CreateExpressionControlItems()
        {
            var result = new List<ExpressionControlItem>
            {
                new ExpressionControlItem 
                {
                    TypeId = ExpressionControlType.None, 
                    FriendlyName = "-"
                },
                new ExpressionControlItem
                {
                    TypeId = ExpressionControlType.ModulationWheel,
                    // TODO lookup from resource
                    FriendlyName = "Modulation Wheel"
                },
                new ExpressionControlItem 
                {
                    TypeId = ExpressionControlType.Expression, 
                    FriendlyName = "Expression"
                }
            };
            
            return result;
        }

        /// <summary>
        /// Creates the scale selection list.
        /// </summary>
        /// <returns>The created list.</returns>
        private IEnumerable<ScaleItem> CreateScaleSelectionList()
        {
            var availableScales = ScaleFactory.GetSupportedScales();

            return availableScales.Select(scaleName => new ScaleItem
            {
                ScaleId = scaleName, FriendlyName = scaleName.ToString() // TODO lookup from resource
            }).ToList();
        }

        /// <summary>
        /// Gets the scale item by identifier.
        /// </summary>
        /// <param name="scaleModeId">The scale mode identifier.</param>
        /// <returns>The requested item if found; otherwise null.</returns>
        private ScaleItem GetScaleItem(Scales scaleModeId)
        {
            return _scaleSelectionList.FirstOrDefault(scaleItem => scaleItem.ScaleId == scaleModeId);
        }

        /// <summary>
        /// Gets the expression control item by ExpressionControlType.
        /// </summary>
        /// <param name="expressionControlType">Type of the expression control.</param>
        /// <returns>The requested item if found; otherwise null.</returns>
        private ExpressionControlItem GetExpressionControlItem(ExpressionControlType expressionControlType)
        {
            return _expressionControlItems.FirstOrDefault(item => item.TypeId == expressionControlType);
        }

        #endregion // Private methods
    }
}
