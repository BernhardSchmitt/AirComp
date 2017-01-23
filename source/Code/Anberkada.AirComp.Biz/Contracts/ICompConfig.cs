
namespace Anberkada.AirComp.Biz.Contracts
{
    using System;

    /// <summary>
    /// Interface for the configuration of the composer.
    /// </summary>
    public interface ICompConfig
    {
        #region Mode control

        /// <summary>
        /// Gets the threshold for the y direction to enter mode control.
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        double ModeControlYDirectionThreshold { get; }

        #endregion // Mode control

        #region Amplitude

        /// <summary>
        /// Gets the minimum position for the amplitude.
        /// </summary>
        /// <value>
        /// The minimum position.
        /// </value>
        double AmplitudeMinPosition { get; }

        /// <summary>
        /// Gets the maximum position for the amplitude.
        /// </summary>
        /// <value>
        /// The maximum position.
        /// </value>
        double AmplitudeMaxPosition { get; }

        /// <summary>
        /// Gets the minimum value of the amplitude.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        double AmplitudeMinValue { get; }

        /// <summary>
        /// Gets the maximum value of the amplitude.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        double AmplitudeMaxValue { get; }

        /// <summary>
        /// Gets the default value of the amplitude.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        double AmplitudeDefaultValue { get; }

        #endregion // Amplitude

        #region Note on/off

        /// <summary>
        /// Gets the minimum velocity for the touch response.
        /// </summary>
        /// <value>
        /// The minimum velocity.
        /// </value>
        double TouchResponseMinVelocity { get; }

        /// <summary>
        /// Gets the maximum velocity for the touch response.
        /// </summary>
        /// <value>
        /// The maximum velocity.
        /// </value>
        double TouchResponseMaxVelocity { get; }

        /// <summary>
        /// Gets the minimum value of the touch response.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        double TouchResponseMinValue { get; }

        /// <summary>
        /// Gets the maximum value of the touch response.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        double TouchResponseMaxValue { get; }

        /// <summary>
        /// Gets the minimum time span to retrigger the next note on event.
        /// </summary>
        /// <remarks>Used to avoid chatter effects.</remarks>
        /// <value>
        /// The minimum time span.
        /// </value>
        TimeSpan NoteOnRetriggerMinTimeSpan { get; }

        /// <summary>
        /// Gets the default distance between note on and -off position as threshold.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        float DefaultDistanceForNoteOffTrigger { get; }

        #endregion // Note on/off

        #region Expression

        /// <summary>
        /// Gets the minimum position for the expression.
        /// </summary>
        /// <value>
        /// The minimum position.
        /// </value>
        double ExpressionMinPosition { get; }

        /// <summary>
        /// Gets the maximum position for the expression.
        /// </summary>
        /// <value>
        /// The maximum position.
        /// </value>
        double ExpressionMaxPosition { get; }

        /// <summary>
        /// Gets the minimum value of the expression.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        double ExpressionMinValue { get; }

        /// <summary>
        /// Gets the maximum value of the expression.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        double ExpressionMaxValue { get; }

        /// <summary>
        /// Gets the default value of the expression.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        double ExpressionDefaultValue { get; }

        #endregion // Expression

        #region Pitch bend

        /// <summary>
        /// Gets the maximum range for the pitch bend position.
        /// </summary>
        /// <value>
        /// The maximum range.
        /// </value>
        double PitchBendMaxRange { get; }

        /// <summary>
        /// Gets the minimum value of the pitch bend.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        double PitchBendMinValue { get; }

        /// <summary>
        /// Gets the maximum value of the pitch bend.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        double PitchBendMaxValue { get; }

        /// <summary>
        /// Gets the default value of the pitch bend.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        double PitchBendDefaultValue { get; }

        #endregion // Pitch bend

        #region Pitch

        /// <summary>
        /// Gets the minimum position for the pitch.
        /// </summary>
        /// <value>
        /// The minimum position.
        /// </value>
        double PitchMinPosition { get; }

        /// <summary>
        /// Gets the maximum position for the pitch.
        /// </summary>
        /// <value>
        /// The maximum position.
        /// </value>
        double PitchMaxPosition { get; }

        /// <summary>
        /// Gets the minimum value of the pitch.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        int PitchMinValue { get; }

        /// <summary>
        /// Gets the maximum value of the pitch.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        int PitchMaxValue { get; }

        #endregion // Pitch

        #region MIDI

        /// <summary>
        /// Gets the name of the midi out device.
        /// </summary>
        /// <value>
        /// The name of the midi out device.
        /// </value>
        string MidiOutDeviceName { get; }

        /// <summary>
        /// Gets the midi out channel.
        /// </summary>
        /// <value>
        /// The midi out channel.
        /// </value>
        int MidiOutChannel { get; }

        #endregion // MIDI
    }
}