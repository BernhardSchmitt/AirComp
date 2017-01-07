
namespace Anberkada.Comp.MidiAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Midi;

    using Contracts;

    /// <summary>
    /// Adapter for using a MIDI out device.
    /// </summary>
    internal class MidiOutAdapter : IMidiOutAdapter
    {
        private static readonly object Lock = new object();
        private readonly IList<int> _noteOnList = new List<int>();
        private OutputDevice _currentOutputDevice;
        private int _currentChannel;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiOutAdapter"/> class.
        /// </summary>
        public MidiOutAdapter()
        {
            MonoMode = true;
        }

        /// <summary>
        /// Gets the current MIDI out device anme.
        /// </summary>
        /// <value>
        /// The name of the MIDI out device.
        /// </value>
        public string CurrentMidiDeviceName 
        { 
            get
            {
                lock (Lock)
                {
                    if (_currentOutputDevice != null)
                    {
                        return _currentOutputDevice.Name;
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the MIDI out channel.
        /// </summary>
        /// <value>
        /// The MIDI out channel; 1..16; 0 for unspecified channel.
        /// </value>
        public int CurrentMidiChannel 
        {
            get { return _currentChannel; }
            set { _currentChannel = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether monophonic mode is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mono mode; otherwise, <c>false</c>.
        /// </value>
        public bool MonoMode { get; set; }

        /// <summary>
        /// Gets the count of current note ons.
        /// </summary>
        /// <value>
        /// The count of current note ons.
        /// </value>
        public int CurrentNoteOnCount 
        {
            get { return _noteOnList.Count; } 
        }

        /// <summary>
        /// Gets the current output device.
        /// </summary>
        /// <value>
        /// The current output device.
        /// </value>
        private OutputDevice CurrentOutputDevice
        {
            get
            {
                return _currentOutputDevice;
            }
        }

        /// <summary>
        /// Gets the available MIDI out devices.
        /// </summary>
        /// <returns>The list of device names.</returns>
        public IEnumerable<string> GetMidiOutDevices()
        {
            lock (Lock)
            {
                var deviceList = OutputDevice.InstalledDevices;

                return deviceList.Select(device => device.Name);
            }
        }

        /// <summary>
        /// Gets the available MIDI out channels.
        /// </summary>
        /// <returns>The list of channels.</returns>
        public IEnumerable<int> GetMidiOutChannels()
        {
            lock (Lock)
            {
                for (int channel = 1; channel <= 16; channel++)
                {
                    yield return channel;
                }
            }
        }

        /// <summary>
        /// Opens the device.
        /// </summary>
        /// <param name="deviceName">Name of the device.</param>
        public void OpenDevice(string deviceName)
        {
            lock (Lock)
            {
                var deviceList = OutputDevice.InstalledDevices;

                foreach (var device in deviceList)
                {
                    if (string.Equals(device.Name, deviceName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!device.IsOpen)
                        {
                            device.Open();
                        }
                        _currentOutputDevice = device;

                        return;
                    }
                }

                throw new NotSupportedException("Unknown MIDI out device");
            }
        }

        /// <summary>
        /// Closes the device.
        /// </summary>
        public void CloseDevice()
        {
            lock (Lock)
            {
                if (_currentOutputDevice != null && _currentOutputDevice.IsOpen)
                {
                    _currentOutputDevice.Close();
                }
                else
                {
                    throw new NotSupportedException("Can't close MIDI output device");
                }
            }
        }

        /// <summary>
        /// Sends the note on.
        /// </summary>
        /// <param name="pitchValue">The pitch value.</param>
        /// <param name="velocity">The velocity; 0..127.</param>
        public void SendNoteOn(int pitchValue, int velocity)
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    // Trigger Note off in MonoMode
                    if (MonoMode && _noteOnList.Count > 0)
                    {
                        CurrentOutputDevice.SendNoteOff(GetChannel(CurrentMidiChannel), GetPitch(_noteOnList[0]), 127);
                        _noteOnList[0] = pitchValue;
                    }
                    else
                    {
                        _noteOnList.Add(pitchValue);
                    }
                    
                    CurrentOutputDevice.SendNoteOn(GetChannel(CurrentMidiChannel), GetPitch(pitchValue), velocity);
                }
            }
        }

        /// <summary>
        /// Sends all notes off.
        /// </summary>
        public void SendAllNotesOff()
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    CurrentOutputDevice.SilenceAllNotes();
                    _noteOnList.Clear();
                }
            }
        }

        /// <summary>
        /// Sends the note off message.
        /// </summary>
        /// <param name="pitchValue">The pitch value.</param>
        /// <param name="velocity">The velocity; 0..127.</param>
        public void SendNoteOff(int pitchValue, int velocity)
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    CurrentOutputDevice.SendNoteOff(GetChannel(CurrentMidiChannel), GetPitch(pitchValue), velocity);
                    _noteOnList.Remove(pitchValue);           
                }
            }
        }

        /// <summary>
        /// Sends the note off message.
        /// </summary>
        /// <param name="pitchValue">The pitch value.</param>
        public void SendNoteOff(int pitchValue)
        {
            SendNoteOff(pitchValue, 127);
        }

        /// <summary>
        /// Sends the pitch bend.
        /// </summary>
        /// <param name="value">The bend value; 0..16383, 8192 is centered.</param>
        public void SendPitchBend(int value)
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    CurrentOutputDevice.SendPitchBend(GetChannel(CurrentMidiChannel), value);
                }
            }
        }

        /// <summary>
        /// Sends the volume change.
        /// </summary>
        /// <param name="value">The volume value; 0..127.</param>
        public void SendVolumeChange(int value)
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    CurrentOutputDevice.SendControlChange(GetChannel(CurrentMidiChannel), Control.Volume, value);
                }
            }
        }

        /// <summary>
        /// Sends the expression change.
        /// </summary>
        /// <param name="value">The expression value; 0..127.</param>
        public void SendExpressionChange(int value)
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    CurrentOutputDevice.SendControlChange(GetChannel(CurrentMidiChannel), Control.Expression, value);
                }
            }
        }

        /// <summary>
        /// Sends the modulation change.
        /// </summary>
        /// <param name="value">The modulation value; 0..127.</param>
        public void SendModulationChange(int value)
        {
            lock (Lock)
            {
                if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
                {
                    CurrentOutputDevice.SendControlChange(GetChannel(CurrentMidiChannel), Control.ModulationWheel, value);
                }
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Auto close the encapsulated device
            if (CurrentOutputDevice != null && CurrentOutputDevice.IsOpen)
            {
                CurrentOutputDevice.Close();
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _currentOutputDevice = null;
            }
            // Free unmanaged resources, if there are any
        }

        #endregion // Implementation of IDisposable

        /// <summary>
        /// Gets the channel from int value.
        /// </summary>
        /// <param name="value">The value [1..16].</param>
        /// <returns>The channel [Channel1...Channel16].</returns>
        private Channel GetChannel(int value)
        {
            return (Channel)(value -1);
        }

        /// <summary>
        /// Gets the pitch from .
        /// </summary>
        /// <param name="value">The value [0..127].</param>
        /// <returns>The pitch [C-1..G9]</returns>
        private Pitch GetPitch(int value)
        {
            // TODO check with Pitch mapping in AirComp.Biz.Contracts!
            return (Pitch)value;
        }
    }
}
