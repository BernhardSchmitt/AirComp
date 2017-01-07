
namespace Anberkada.MusicBase.Biz.Chords
{
    using Contracts;

    /// <summary>
    /// Base class for chords.
    /// </summary>
    public abstract class ChordBase : HarmonicStack, IChord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChordBase"/> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        protected ChordBase(Tone baseTone) : base(baseTone)
        {
        }

        /// <summary>
        /// Gets the chord.
        /// </summary>
        /// <value>
        /// The chord.
        /// </value>
        public abstract Chords Chord { get; }
    }
}
