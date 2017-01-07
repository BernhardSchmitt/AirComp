
namespace Anberkada.MusicBase.Biz.Scales
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    /// <summary>
    /// Creation factory for modes / scales.
    /// </summary>
    public class ScaleFactory
    {
        /// <summary>
        /// Creates the scale.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="scaleName">Name of the scale.</param>
        /// <returns>The requested scale.</returns>
        public static IScale CreateScale(Tone baseTone, Scales scaleName)
        {
            switch (scaleName)
            {
                case Scales.Chromatic:
                    return new ChromaticScale(baseTone);

                case Scales.MinorPentatonic:
                    return new MinorPentatonicScale(baseTone);

                case Scales.Blues:
                    return new BluesScale(baseTone);

                case Scales.MajorPentatonic:
                    return new MajorPentatonicScale(baseTone);

                case Scales.IonianMajor:
                    return new IonianMajorScale(baseTone);

                case Scales.Dorian:
                    return new DorianScale(baseTone);

                case Scales.Phrygian:
                    return new PhrygianScale(baseTone);

                case Scales.Lydian:
                    return new LydianScale(baseTone);

                case Scales.Mixolydian:
                    return new MixolydianScale(baseTone);

                case Scales.AeolianNaturalMinor:
                    return new AeolianNaturalMinorScale(baseTone);

                case Scales.Locrian:
                    return new LocrianScale(baseTone);

                default:
                    throw new NotSupportedException("Not supported scale");
            }
        }

        /// <summary>
        /// Gets the supported scales.
        /// </summary>
        /// <returns>List of supported scale names.</returns>
        public static IEnumerable<Scales> GetSupportedScales()
        {
            var result = new List<Scales>
            {
                Scales.Chromatic,
                Scales.MinorPentatonic,
                Scales.Blues,
                Scales.MajorPentatonic,
                Scales.IonianMajor,
                Scales.Dorian,
                Scales.Phrygian,
                Scales.Lydian,
                Scales.Mixolydian,
                Scales.AeolianNaturalMinor,
                Scales.Locrian
            };

            return result;
        }
    }
}
