
namespace Anberkada.MusicBase.Biz.Scales
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Contracts;

    /// <summary>
    /// Represents the aeolian mode or natural minor scale, associated with minor 7 ♭13 chord.
    /// </summary>
    public class AeolianNaturalMinorScale : ScaleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IonianMajorScale" /> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        public AeolianNaturalMinorScale(Tone baseTone)
            : base(baseTone)
        {
            
        }

        /// <summary>
        /// Gets the name of the scale / mode.
        /// </summary>
        /// <value>
        /// The name of the scale / mode.
        /// </value>
        public override Scales Scale { get { return Scales.AeolianNaturalMinor; } }

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
                Degrees.III_minorThird,
                Degrees.IV_perfectFourth,
                Degrees.V_perfectFifth,
                Degrees.VI_minorSixth,
                Degrees.VII_minorSeventh
            };

            return new ReadOnlyCollection<Degrees>(result);
        }
    }
}
