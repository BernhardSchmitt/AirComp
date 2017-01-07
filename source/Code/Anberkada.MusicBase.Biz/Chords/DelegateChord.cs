
namespace Anberkada.MusicBase.Biz.Chords
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Contracts;

    /// <summary>
    /// Delegate class to represent a chord.
    /// </summary>
    public class DelegateChord : ChordBase
    {
        private readonly Chords _chordId;
        private readonly ReadOnlyCollection<Degrees> _degrees;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateChord"/> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="chordId">The chord identifier.</param>
        /// <param name="degrees">The list of degrees.</param>
        public DelegateChord(Tone baseTone, Chords chordId, IList<Degrees> degrees) : base(baseTone)
        {
            _chordId = chordId;
            _degrees = new ReadOnlyCollection<Degrees>(degrees);
        }

        /// <summary>
        /// Creates the degrees of one octave collection.
        /// </summary>
        /// <returns>
        /// The created collection.
        /// </returns>
        protected override ReadOnlyCollection<Degrees> CreateDegreesOfOneOctaveCollection()
        {
            return _degrees;
        }

        /// <summary>
        /// Gets the chord.
        /// </summary>
        /// <value>
        /// The chord.
        /// </value>
        public override Chords Chord
        {
            get { return _chordId; }
        }
    }
}