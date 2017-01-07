

namespace Anberkada.Comp.MidiAdapter.Contracts
{
    using System;

    /// <summary>
    /// Factory for MidiOutAdapter.
    /// </summary>
    public class MidiOutAdapterFactory
    {
        private static readonly Object Lock = new object();
        private static IMidiOutAdapter _midiOutAdapter;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        public static IMidiOutAdapter GetInstance()
        {
            lock (Lock)
            {
                return _midiOutAdapter ?? (_midiOutAdapter = new MidiOutAdapter());
            }
        }
    }
}
