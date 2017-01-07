
namespace Anberkada.AirComp.Biz
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Contracts;
    using MusicBase.Biz.Contracts;
    using MusicBase.Biz.Scales;

    /// <summary>
    /// Base implementation of the composer model.
    /// </summary>
    public abstract class CompModelBase : ICompModelBase, IOutDeviceModel
    {
        protected static readonly object Lock = new object();

        private readonly CompConfig _config;
        
        private Pitch _currentPitch;
        private double _currentAmplitude;
        private double _currentExpression;
        private ExpressionControlType _currentExpressionControlType;
        private double _currentPitchBend;
        private IScale _currentScale;
        private string _currentOutDevice;
        private int _currentOutChannel;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="AirCompModel" /> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="outDevices">The list of out devices.</param>
        /// <param name="outChannels">The list of out channels.</param>
        protected CompModelBase(CompConfig config, IEnumerable<string> outDevices, IEnumerable<int> outChannels)
        {
            _config = config;
            AvailableOutDevices = outDevices;
            AvailableOutChannels = outChannels;
            
            CurrentScale = new ChromaticScale(Tone.C);
        }

        #region Implementation of ICompModelBase

        /// <summary>
        /// Gets or sets the current pitch.
        /// </summary>
        /// <value>
        /// The current pitch.
        /// </value>
        public Pitch CurrentPitch
        {
            get
            {
                lock (Lock)
                {
                    return _currentPitch;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetBackingFieldWithNotification(value, ref _currentPitch);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current scale.
        /// </summary>
        /// <value>
        /// The current scale.
        /// </value>
        public IScale CurrentScale
        {
            get
            {
                lock (Lock)
                {
                    return _currentScale;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetBackingFieldWithNotification(value, ref _currentScale);
                }
            }
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
                lock (Lock)
                {
                    return _currentAmplitude;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetBackingFieldWithNotification(value, ref _currentAmplitude, 0.01);
                }
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
                lock (Lock)
                {
                    return _currentExpression;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetBackingFieldWithNotification(value, ref _currentExpression, 0.01);
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the current expression control.
        /// </summary>
        /// <value>
        /// The type of the current expression control.
        /// </value>
        public ExpressionControlType CurrentExpressionControlType
        {
            get
            {
                lock (Lock)
                {
                    return _currentExpressionControlType;
                }
            }
            set
            {
                lock (Lock)
                {
                    // SetBackingFieldWithNotification(int newValue, ref int backingField, [CallerMemberName] string propName = "")
                    var newValue = (int)_currentExpressionControlType;
                    SetBackingFieldWithNotification((int)value, ref newValue);
                    _currentExpressionControlType = (ExpressionControlType)newValue;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current pitch bend.
        /// </summary>
        /// <value>
        /// The current pitch bend in the range of -1..1.
        /// </value>
        /// <remarks>With each note on event, pitch bend value will be initialized to 0.</remarks>
        public double CurrentPitchBend
        {
            get
            {
                lock (Lock)
                {
                    return _currentPitchBend;
                }
            }
            set
            {
                lock (Lock)
                {
                    SetBackingFieldWithNotification(value, ref _currentPitchBend, 0.01);
                }
            }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        protected CompConfig Config
        {
            get
            {
                return _config;
            }
        }

        /// <summary>
        /// Occurs on a [note on] event.
        /// </summary>
        public event NoteOnEventHandler NoteOn = delegate { };

        /// <summary>
        /// Occurs on a [note off] event.
        /// </summary>
        public event NoteOffEventHandler NoteOff = delegate { };

        /// <summary>
        /// Occurs on an [all notes off] event.
        /// </summary>
        public event AllNotesOffEventHandler AllNotesOff = delegate { };

        /// <summary>
        /// Initializes the out device.
        /// </summary>
        public void InitOutDevice()
        {
            // Setup MIDI out
            CurrentOutChannel = Config.MidiOutChannel;
            try
            {
                CurrentOutDevice = Config.MidiOutDeviceName;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception occured while trying to set configured MIDI out device '{0}': {1}", Config.MidiOutDeviceName, e);
            }
        }

        /// <summary>
        /// Resets the out device.
        /// </summary>
        public void ResetOutDevice()
        {
            OnAllNotesOffEvent();
        }

        /// <summary>
        /// Gets the pitch by normalized position.
        /// </summary>
        /// <param name="normalizedPosition">The normalized position.</param>
        /// <returns>The pitch.</returns>
        public virtual Pitch GetPitchByNormalizedPosition(double normalizedPosition)
        {
            return CurrentScale.GetPitchByPosition(normalizedPosition, new Pitch(Config.PitchMinValue), new Pitch(Config.PitchMaxValue));
        }

        /// <summary>
        /// Changes the current scale.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="scaleName">Name of the scale.</param>
        public void ChangeScale(Tone baseTone, Scales scaleName)
        {
            CurrentScale = ScaleFactory.CreateScale(baseTone, scaleName);
        }

        #endregion // Implementation of ICompModelBase

        #region Implementation of IOutDeviceModel

        /// <summary>
        /// Gets or sets the list of available out devices.
        /// </summary>
        /// <value>
        /// The available out devices.
        /// </value>
        public IEnumerable<string> AvailableOutDevices { get; private set; }

        /// <summary>
        /// Gets or sets the current out device.
        /// </summary>
        /// <value>
        /// The current out device.
        /// </value>
        public string CurrentOutDevice
        {
            get
            {
                lock (Lock)
                {
                    return _currentOutDevice;
                }
            }
            set
            {
                lock (Lock)
                {
                    if (AvailableOutDevices.Any(device => string.Equals(value, device, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        SetBackingFieldWithNotification(value, ref _currentOutDevice);
                    }
                    else
                    {
                        throw new NotSupportedException("Can't set unknown out device");    
                    }
                }
            }
        }

        /// <summary>
        /// Gets the list of available out channels.
        /// </summary>
        /// <value>
        /// The available out channels.
        /// </value>
        public IEnumerable<int> AvailableOutChannels { get; private set; } 

        /// <summary>
        /// Gets or sets the current out device channel.
        /// </summary>
        /// <value>
        /// The current out device channel.
        /// </value>
        public int CurrentOutChannel
        {
            get
            {
                lock (Lock)
                {
                    return _currentOutChannel;
                }
            }
            set
            {
                if (AvailableOutChannels.Any(channel => channel == value))
                {
                    SetBackingFieldWithNotification(value, ref _currentOutChannel);
                }
                else
                {
                    throw new NotSupportedException("Can't set unknown out channel");
                }
            }
        }

        #endregion // Implementation of IOutDeviceModel

        #region Protected methods

        /// <summary>
        /// Raises the NoteOnEvent.
        /// </summary>
        /// <param name="eventArgs">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
        protected virtual void OnNoteOnEvent(NoteEventArgs eventArgs)
        {
            NoteOn(this, eventArgs);
        }

        /// <summary>
        /// Raises the NoteOffEvent.
        /// </summary>
        /// <param name="eventArgs">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
        protected virtual void OnNoteOffEvent(NoteEventArgs eventArgs)
        {
            NoteOff(this, eventArgs);
        }

        /// <summary>
        /// Raises the AllNotesOffEvent.
        /// </summary>
        protected virtual void OnAllNotesOffEvent()
        {
            AllNotesOff(this, null);
        }

        /// <summary>
        /// Normalizes the double value.
        /// </summary>
        /// <param name="rawValue">The raw value.</param>
        /// <param name="minBoundary">The minimum boundary.</param>
        /// <param name="maxBoundary">The maximum boundary.</param>
        /// <returns>The normalized value.</returns>
        protected double NormalizeDoubleValue(double rawValue, double minBoundary, double maxBoundary)
        {
            var result = (rawValue - minBoundary ) / (maxBoundary - minBoundary);

            return LimitDoubleValue(result, 0, 1);
        }

        /// <summary>
        /// Limits the double value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The limited double value.</returns>
        protected double LimitDoubleValue(double source, double minValue, double maxValue)
        {
            return (source > maxValue) ? maxValue : (source < minValue) ? minValue : source;
        }

        /// <summary>
        /// Sets the backing field with notification PropertyChanged.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="newValue">The new value.</param>
        /// <param name="backingField">The backing field.</param>
        /// <param name="propName">Name of the property.</param>
        protected void SetBackingFieldWithNotification<TProp>(TProp newValue, ref TProp backingField, [CallerMemberName] string propName = "") where TProp : class
        {
            if (newValue != backingField)
            {
                backingField = newValue;
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// Sets the backing field with notification PropertyChanged.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="backingField">The backing field.</param>
        /// <param name="propName">Name of the property.</param>
        protected void SetBackingFieldWithNotification(int newValue, ref int backingField, [CallerMemberName] string propName = "")
        {
            if (newValue != backingField)
            {
                backingField = newValue;
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// Sets the backing field with notification.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="backingField">The backing field.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <param name="propName">Name of the property.</param>
        protected void SetBackingFieldWithNotification(double newValue, ref double backingField, double tolerance, [CallerMemberName] string propName = "")
        {
            if (Math.Abs(newValue - backingField) > tolerance)
            {
                backingField = newValue;
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion // Protected methods
    }
}