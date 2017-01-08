
namespace Anberkada.AirComp.Ui.Stubs
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Biz.Contracts;
    using MusicBase.Biz.Contracts;
    using MusicBase.Biz.Scales;

    class AirCompModelStub : ICompModelBase, IOutDeviceModel
    {
        public AirCompModelStub()
        {
            CurrentAmplitude = 0.5f;
            CurrentExpression = 0.5f;
            CurrentPitch = new Pitch(60);
            PlayingPitch = new Pitch(50);
            CurrentPitchBend = 0f;
            CurrentScale = ScaleFactory.CreateScale(Tone.C, Scales.Chromatic);
            AvailableOutDevices = new List<string> { "My MIDI out device" };
            CurrentOutDevice = AvailableOutDevices.First();
            AvailableOutChannels = new List<int> { 1, 2, 3, 4 };
            CurrentOutChannel = AvailableOutChannels.First();
        }

        /// <summary>
        /// Initializes the out device.
        /// </summary>
        public void InitOutDevice()
        {
        }

        public void ResetOutDevice()
        {
        }

        public Pitch GetPitchByNormalizedPosition(double normalizedPosition)
        {
            return new Pitch(0);
        }

        public void ChangeScale(Tone baseTone, Scales scaleName)
        {
        }

        public void ChangeScale(Scales scaleName)
        {
        }

        public double CurrentAmplitude { get; set; }

        public double CurrentExpression { get; set; }

        public ExpressionControlType CurrentExpressionControlType { get; set; }

        public Pitch CurrentPitch { get; set; }

        public Pitch PlayingPitch { get; set; }

        public double CurrentPitchBend { get; set; }

        public IScale CurrentScale { get; set; }

        public event NoteOffEventHandler NoteOff = delegate { };

        public event NoteOnEventHandler NoteOn = delegate { };

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public IEnumerable<string> AvailableOutDevices { get; private set; }
        public string CurrentOutDevice { get; set; }
        public IEnumerable<int> AvailableOutChannels { get; private set; }
        public int CurrentOutChannel { get; set; }
    }
}
