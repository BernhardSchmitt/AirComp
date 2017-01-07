
namespace Anberkada.MusicBase.Biz.Scales
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Contracts;

    /// <summary>
    /// Represents the mixolydian mode, associated with (b)7 chords.
    /// </summary>
    public class MixolydianScale : ScaleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MixolydianScale" /> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        public MixolydianScale(Tone baseTone)
            : base(baseTone)
        {

        }

        /// <summary>
        /// Gets the name of the scale / mode.
        /// </summary>
        /// <value>
        /// The name of the scale / mode.
        /// </value>
        public override Scales Scale { get { return Scales.Mixolydian; } }

        /// <summary>
        /// Creates the degrees of one octave collection.
        /// </summary>
        /// <returns>The created collection.</returns>
        protected override ReadOnlyCollection<Degrees> CreateDegreesOfOneOctaveCollection()
        {
            var result = new List<Degrees>
            {
                Degrees.I_perfectPrime,
                Degrees.II_majorSecond,
                Degrees.III_majorThird,
                Degrees.IV_perfectFourth,
                Degrees.V_perfectFifth,
                Degrees.VI_majorSixth,
                Degrees.VII_minorSeventh                
            };

            return new ReadOnlyCollection<Degrees>(result);
        }
    }
}
