
namespace Anberkada.Comp.MidiAdapter.Contracts
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Interface for using a MIDI out device with a specific channel.
    /// </summary>
    public interface IMidiOutAdapter : IDisposable
    {
        /// <summary>
        /// Gets the current MIDI out device.
        /// </summary>
        /// <value>
        /// The name of the MIDI out device.
        /// </value>
        string CurrentMidiDeviceName { get; }

        /// <summary>
        /// Gets or sets the MIDI out channel.
        /// </summary>
        /// <value>
        /// The MIDI out channel; 1..16; 0 for unspecified channel.
        /// </value>
        int CurrentMidiChannel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether monophonic mode is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mono mode; otherwise, <c>false</c>.
        /// </value>
        bool MonoMode { get; set; }

        /// <summary>
        /// Gets the count of current note ons.
        /// </summary>
        /// <value>
        /// The count of current note ons.
        /// </value>
        int CurrentNoteOnCount { get; }

        /// <summary>
        /// Gets the available MIDI out devices.
        /// </summary>
        /// <returns>The list of device names.</returns>
        IEnumerable<string> GetMidiOutDevices();

        /// <summary>
        /// Gets the available MIDI out channels.
        /// </summary>
        /// <returns>The list of channels.</returns>
        IEnumerable<int> GetMidiOutChannels(); 

        /// <summary>
        /// Opens the device.
        /// </summary>
        /// <param name="deviceName">Name of the device.</param>
        void OpenDevice(string deviceName);

        /// <summary>
        /// Closes the device.
        /// </summary>
        void CloseDevice();

        /// <summary>
        /// Sends the note on.
        /// </summary>
        /// <param name="pitchValue">The pitch value.</param>
        /// <param name="velocity">The velocity; 0..127.</param>
        void SendNoteOn(int pitchValue, int velocity);

        /// <summary>
        /// Sends the note off message.
        /// </summary>
        /// <param name="pitchValue">The pitch value.</param>
        /// <param name="velocity">The velocity; 0..127.</param>
        void SendNoteOff(int pitchValue, int velocity);

        /// <summary>
        /// Sends the note off message.
        /// </summary>
        /// <param name="pitchValue">The pitch value.</param>
        void SendNoteOff(int pitchValue);

        /// <summary>
        /// Sends all notes off.
        /// </summary>
        void SendAllNotesOff();

        /// <summary>
        /// Sends the pitch bend.
        /// </summary>
        /// <param name="value">The bend value; 0..16383, 8192 is centered.</param>
        void SendPitchBend(int value);

        /// <summary>
        /// Sends the volume change.
        /// </summary>
        /// <param name="value">The volume value; 0..127.</param>
        void SendVolumeChange(int value);

        /// <summary>
        /// Sends the expression change.
        /// </summary>
        /// <param name="value">The expression value; 0..127.</param>
        void SendExpressionChange(int value);

        /// <summary>
        /// Sends the modulation change.
        /// </summary>
        /// <param name="value">The modulation value; 0..127.</param>
        void SendModulationChange(int value);
    }
}
