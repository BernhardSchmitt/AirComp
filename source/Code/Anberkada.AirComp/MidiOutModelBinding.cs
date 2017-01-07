
using System.Diagnostics;

namespace Anberkada.AirComp
{
    using System;
    using System.ComponentModel;

    using Biz;
    using Biz.Contracts;

    using Comp.MidiAdapter.Contracts;

    /// <summary>
    /// Binds the MIDI out adapter to the AirCompModel.
    /// </summary>
    class MidiOutModelBinding : IDisposable
    {
        private readonly AirCompModel _model;
        private readonly IMidiOutAdapter _midiOutAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiOutModelBinding"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="midiOutAdapter">The midi out adapter.</param>
        public MidiOutModelBinding(AirCompModel model, IMidiOutAdapter midiOutAdapter)
        {
            _model = model;
            _midiOutAdapter = midiOutAdapter;

            _model.NoteOn += NoteOnHandler;
            _model.NoteOff += NoteOffHandler;
            _model.AllNotesOff += AllNotesOffHandler;
            _model.PropertyChanged += PropertyChangedHandler;
        }

        /// <summary>
        /// Handles note on events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
        private void NoteOnHandler(object sender, NoteEventArgs e)
        {
            if (e.Pitch != null)
            {
                _midiOutAdapter.SendNoteOn(e.Pitch.Value, Get7bitMidiRange(e.Velocity));
            }
        }

        /// <summary>
        /// Handles note off events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
        private void NoteOffHandler(object sender, NoteEventArgs e)
        {
            if (e.Pitch != null)
            {
                _midiOutAdapter.SendNoteOff(e.Pitch.Value, Get7bitMidiRange(e.Velocity));
            }
        }
        
        /// <summary>
        /// Handles note off events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NoteEventArgs"/> instance containing the event data.</param>
        private void AllNotesOffHandler(object sender, EventArgs e)
        {
            _midiOutAdapter.SendAllNotesOff();
        }

        /// <summary>
        /// HAndles property changed events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentOutDevice":
                    _midiOutAdapter.OpenDevice(_model.CurrentOutDevice);
                    break;

                case "CurrentOutChannel":
                    _midiOutAdapter.CurrentMidiChannel = _model.CurrentOutChannel;
                    break;

                case "CurrentAmplitude":
                    _midiOutAdapter.SendVolumeChange(Get7bitMidiRange(_model.CurrentAmplitude));
                    break;

                case "CurrentExpression":
                    HandleExpressionChanged(_model.CurrentExpression, _model.CurrentExpressionControlType);
                    break;

                case "CurrentPitchBend":
                    _midiOutAdapter.SendPitchBend(Get14bitMidiRange(_model.CurrentPitchBend));
                    break;
            }
        }

        /// <summary>
        /// Handles the expression changed notification.
        /// </summary>
        /// <param name="normalizedValue">The normalized value.</param>
        /// <param name="controlType">Type of the expression control.</param>
        private void HandleExpressionChanged(double normalizedValue, ExpressionControlType controlType )
        {
            switch (controlType)
            {
                case ExpressionControlType.Expression:
                    _midiOutAdapter.SendExpressionChange(Get7bitMidiRange(normalizedValue));
                    break;
                case ExpressionControlType.ModulationWheel:
                    _midiOutAdapter.SendModulationChange(Get7bitMidiRange(normalizedValue));
                    break;
            }
            
        }

        /// <summary>
        /// Gets the 7 bit MIDI range.
        /// </summary>
        /// <param name="source">The source [0..1].</param>
        /// <returns>The value within [0..127].</returns>
        private int Get7bitMidiRange(double source)
        {
            Debug.Assert(source >=0f && source <=1f);

            return (int) (source*127f);
        }

        /// <summary>
        /// Gets the 14 bit MIDI range (used by pitch bend f. x.).
        /// </summary>
        /// <param name="source">The source [-1..1].</param>
        /// <returns>The value within [0..16383].</returns>
        private int Get14bitMidiRange(double source)
        {
            Debug.Assert(source >= -1f && source <= 1f);

            // 0..16383, 8192 is centered
            return (int)(source * 16383f);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _midiOutAdapter.SendAllNotesOff();

            _model.NoteOn -= NoteOnHandler;
            _model.NoteOff -= NoteOffHandler;
            _model.PropertyChanged -= PropertyChangedHandler;

            if (_model.CurrentOutDevice != null)
            {
                _midiOutAdapter.CloseDevice();
            }
        }
    }
}
