
namespace Anberkada.AirComp.Biz
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    using Contracts;

    /// <summary>
    /// Configuration for composer.
    /// </summary>
    public class CompConfig : ICompConfig
    {
        private const double MinPositionY = 20f;
        private const double MaxPositionY = 300f;
        private const double MinVelocity = 300;
        private const double MaxVelocity = 1200;
        private const double MinValue = 0f;
        private const double MaxValue = 1f;

        #region Mode control

        /// <summary>
        /// Gets the threshold for the y direction to enter mode control.
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        public double ModeControlYDirectionThreshold 
        {
            get { return GetOption(0.8f); }
        }

        #endregion // Mode control

        #region Amplitude

        /// <summary>
        /// Gets the minimum position for the amplitude.
        /// </summary>
        /// <value>
        /// The minimum position.
        /// </value>
        public double AmplitudeMinPosition
        {
            get { return GetOption(MinPositionY); }
        }

        /// <summary>
        /// Gets the maximum position for the amplitude.
        /// </summary>
        /// <value>
        /// The maximum position.
        /// </value>
        public double AmplitudeMaxPosition
        {
            get { return GetOption(MaxPositionY); }
        }

        /// <summary>
        /// Gets the minimum value of the amplitude.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public double AmplitudeMinValue
        {
            get { return GetOption(MinValue); }
        }

        /// <summary>
        /// Gets the maximum value of the amplitude.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public double AmplitudeMaxValue
        {
            get { return GetOption(MaxValue); }
        }

        /// <summary>
        /// Gets the default value of the amplitude.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public double AmplitudeDefaultValue
        {
            get { return GetOption(0.8 * MaxValue); }
        }

        #endregion // Amplitude

        #region Note on/off

        /// <summary>
        /// Gets the minimum velocity for the touch response.
        /// </summary>
        /// <value>
        /// The minimum velocity.
        /// </value>
        public double TouchResponseMinVelocity
        {
            get { return GetOption(MinVelocity); }
        }

        /// <summary>
        /// Gets the maximum velocity for the touch response.
        /// </summary>
        /// <value>
        /// The maximum velocity.
        /// </value>
        public double TouchResponseMaxVelocity
        {
            get { return GetOption(MaxVelocity); }
        }

        /// <summary>
        /// Gets the minimum value of the touch response.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public double TouchResponseMinValue
        {
            get { return GetOption(MinValue); }
        }

        /// <summary>
        /// Gets the maximum value of the touch response.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public double TouchResponseMaxValue
        {
            get { return GetOption(MaxValue); }
        }

        /// <summary>
        /// Gets the minimum time span to retrigger the next note on event.
        /// </summary>
        /// <value>
        /// The minimum time span.
        /// </value>
        /// <remarks>
        /// Used to avoid chatter effects.
        /// </remarks>
        public TimeSpan NoteOnRetriggerMinTimeSpan
        {
            get
            {
                return TimeSpan.FromMilliseconds(GetOption(100));
            }
        }

        /// <summary>
        /// Gets the default distance between note on and -off position as threshold.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public float DefaultDistanceForNoteOffTrigger
        {
            get
            {
                return 100.0f;
            }
        }

        #endregion // Note on/off

        #region Expression

        /// <summary>
        /// Gets the minimum position for the expression.
        /// </summary>
        /// <value>
        /// The minimum position.
        /// </value>
        public double ExpressionMinPosition
        {
            get { return GetOption(- 200f); }
        }

        /// <summary>
        /// Gets the maximum position for the expression.
        /// </summary>
        /// <value>
        /// The maximum position.
        /// </value>
        public double ExpressionMaxPosition
        {
            get { return GetOption(- 100f); }
        }

        /// <summary>
        /// Gets the minimum value of the expression.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public double ExpressionMinValue
        {
            get { return GetOption(MinValue); }
        }

        /// <summary>
        /// Gets the maximum value of the expression.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public double ExpressionMaxValue
        {
            get { return GetOption(MaxValue); }
        }

        public double ExpressionDefaultValue
        {
            get { return GetOption(MaxValue); }
        }

        #endregion // expression

        #region Pitch bend

        /// <summary>
        /// Gets the maximum range for the pitch bend position.
        /// </summary>
        /// <value>
        /// The maximum range.
        /// </value>
        public double PitchBendMaxRange
        {
            get { return GetOption(800f); }
        }

        /// <summary>
        /// Gets the minimum value of the pitch bend.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public double PitchBendMinValue
        {
            get { return GetOption(- MaxValue); }
        }

        /// <summary>
        /// Gets the maximum value of the pitch bend.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public double PitchBendMaxValue
        {
            get { return GetOption(MaxValue); }
        }

        /// <summary>
        /// Gets the default value of the pitch bend.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public double PitchBendDefaultValue
        {
            get { return GetOption(0f); }
        }

        #endregion // Pitch bend
        
        #region Pitch

        /// <summary>
        /// Gets the minimum position for the pitch.
        /// </summary>
        /// <value>
        /// The minimum position.
        /// </value>
        public double PitchMinPosition
        {
            get { return GetOption(MinPositionY); }
        }

        /// <summary>
        /// Gets the maximum position for the pitch.
        /// </summary>
        /// <value>
        /// The maximum position.
        /// </value>
        public double PitchMaxPosition
        {
            get { return GetOption(MaxPositionY); }
        }

        /// <summary>
        /// Gets the minimum value of the pitch.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public int PitchMinValue
        {
            get 
            { 
                // MIDI value for C1
                return GetOption(36); 
            }
        }

        /// <summary>
        /// Gets the maximum value of the pitch.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int PitchMaxValue
        {
            get 
            {
                // MIDI value for C5
                return GetOption(84); 
            }
        }

        #endregion // Pitch

        #region MIDI

        /// <summary>
        /// Gets the name of the midi out device.
        /// </summary>
        /// <value>
        /// The name of the midi out device.
        /// </value>
        public string MidiOutDeviceName
        {
            get
            {
                return GetOption("Microsoft GS Wavetable Synth");
            }
        }

        /// <summary>
        /// Gets the midi out channel.
        /// </summary>
        /// <value>
        /// The midi out channel.
        /// </value>
        public int MidiOutChannel
        {
            get
            {
                return GetOption(1);
            }
        }

        #endregion // MIDI

        #region Private methods

        /// <summary>
        /// Gets the section of the composer settings.
        /// </summary>
        /// <returns>The requested section.</returns>
        private NameValueCollection GetCompSettings()
        {
            return (NameValueCollection)ConfigurationManager.GetSection("composerSettings");
        }

        /// <summary>
        /// Gets the option with the specified key.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value of the option.</returns>
        private int GetOption(int defaultValue, [CallerMemberName] string key = "")
        {
            var result = defaultValue;
            var section = GetCompSettings();

            if (section != null)
            {
                var value = section[key];
                if (!string.IsNullOrEmpty(value))
                {
                    int.TryParse(value, out result);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the option with the specified key.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value of the option.</returns>
        private double GetOption(double defaultValue, [CallerMemberName] string key = "")
        {
            var result = defaultValue;
            var section = GetCompSettings();

            if (section != null)
            {
                var value = section[key];
                if (!string.IsNullOrEmpty(value))
                {
                    double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the option with the specified key.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value of the option.</returns>
        private string GetOption(string defaultValue, [CallerMemberName] string key = "")
        {
            var result = defaultValue;
            var section = GetCompSettings();

            if (section != null)
            {
                var value = section[key];
                if (!string.IsNullOrEmpty(value))
                {
                    result = value;
                }
            }

            return result;
        }

        #endregion
    }
}
