
namespace Anberkada.MusicBase.Biz.Scales
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Contracts;

    /// <summary>
    /// Represents the chromatic scale.
    /// </summary>
    public class ChromaticScale : ScaleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaticScale" /> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        public ChromaticScale(Tone baseTone)
            : base(baseTone)
        {

        }

        /// <summary>
        /// Gets the name of the scale / mode.
        /// </summary>
        /// <value>
        /// The name of the scale / mode.
        /// </value>
        public override Scales Scale { get { return Scales.Chromatic; } }

        /// <summary>
        /// Creates the degrees of one octave collection.
        /// </summary>
        /// <returns>The created collection.</returns>
        protected override ReadOnlyCollection<Degrees> CreateDegreesOfOneOctaveCollection()
        {
            var result = new List<Degrees>
            {
                Degrees.I_perfectPrime,
                Degrees.II_minorSecond,
                Degrees.II_majorSecond,
                Degrees.III_minorThird,
                Degrees.III_majorThird,
                Degrees.IV_perfectFourth,
                Degrees.IV_augmentedFourth,
                Degrees.V_perfectFifth,
                Degrees.VI_minorSixth,
                Degrees.VI_majorSixth,
                Degrees.VII_minorSeventh,
                Degrees.VII_majorSeventh
            };

            return new ReadOnlyCollection<Degrees>(result);
        }
    }
}
