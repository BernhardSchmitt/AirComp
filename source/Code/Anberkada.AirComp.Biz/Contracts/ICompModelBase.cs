
namespace Anberkada.AirComp.Biz.Contracts
{
    using System;
    using System.ComponentModel;

    using MusicBase.Biz.Contracts;
    using MusicBase.Biz.Scales;

    /// <summary>
    /// Event arguments for note on/off events.
    /// </summary>
    public class NoteEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEventArgs"/> class.
        /// </summary>
        /// <param name="pitch">The pitch.</param>
        /// <param name="velocity">The velocity.</param>
        public NoteEventArgs(Pitch pitch, double velocity)
        {
            Pitch = pitch;
            Velocity = velocity;
        }

        /// <summary>
        /// Gets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        public Pitch Pitch { get; private set; }

        /// <summary>
        /// Gets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public double Velocity { get; private set; }
    }

    /// <summary>
    /// Handler for a note on event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
    public delegate void NoteOnEventHandler(object sender, NoteEventArgs e);

    /// <summary>
    /// Handler for a note off event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
    public delegate void NoteOffEventHandler(object sender, NoteEventArgs e);

    /// <summary>
    /// Handler for an all notes off event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public delegate void AllNotesOffEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Base interface for the logical composer model.
    /// </summary>
    public interface ICompModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the current pitch.
        /// </summary>
        /// <value>
        /// The current pitch.
        /// </value>
        Pitch CurrentPitch { get; set; }

        /// <summary>
        /// Gets the pitch of the actual playing note.
        /// </summary>
        /// <value>
        /// The pitch if note is on; otherwise null.
        /// </value>
        Pitch PlayingPitch { get; }

        /// <summary>
        /// Gets or sets the current scale.
        /// </summary>
        /// <value>
        /// The current scale.
        /// </value>
        IScale CurrentScale { get; set; }

        /// <summary>
        /// Gets or sets the current amplitude.
        /// </summary>
        /// <value>
        /// The current amplitude in the range of 0..1.
        /// </value>
        double CurrentAmplitude { get; set; }

        /// <summary>
        /// Gets or sets the current expression.
        /// </summary>
        /// <value>
        /// The current expression in the range of 0..1.
        /// </value>
        double CurrentExpression { get; set; }

        /// <summary>
        /// Gets or sets the type of the current expression control.
        /// </summary>
        /// <value>
        /// The type of the current expression control.
        /// </value>
        ExpressionControlType CurrentExpressionControlType { get; set; }

        /// <summary>
        /// Gets or sets the current pitch bend.
        /// </summary>
        /// <value>
        /// The current pitch bend in the range of -1..1.
        /// </value>
        /// <remarks>With each note on event, pitch bend value will be initialized to 0.</remarks>
        double CurrentPitchBend { get; set; }

        /// <summary>
        /// Occurs on a [note on] event.
        /// </summary>
        event NoteOnEventHandler NoteOn;

        /// <summary>
        /// Occurs on a [note off] event.
        /// </summary>
        event NoteOffEventHandler NoteOff;

        /// <summary>
        /// Initializes the out device.
        /// </summary>
        void InitOutDevice();

        /// <summary>
        /// Gets the pitch by normalized position.
        /// </summary>
        /// <param name="normalizedPosition">The normalized position.</param>
        /// <returns>The pitch.</returns>
        Pitch GetPitchByNormalizedPosition(double normalizedPosition);

        /// <summary>
        /// Changes the current scale.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="scaleName">Name of the scale.</param>
        void ChangeScale(Tone baseTone, Scales scaleName);
    }
}