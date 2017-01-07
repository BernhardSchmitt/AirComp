
namespace Anberkada.MusicBase.Biz.Contracts
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interface for a harmonic stack as base for chords and scales.
    /// </summary>
    public interface IHarmonicStack
    {
        /// <summary>
        /// Gets the tonal base of the scale.
        /// </summary>
        /// <value>
        /// The tonal base.
        /// </value>
        Tone BaseTone { get; }

        /// <summary>
        /// Gets the degrees of one octave.
        /// </summary>
        /// <value>
        /// The degrees of one octave.
        /// </value>
        ReadOnlyCollection<Degrees> DegreesOfOneOctave { get; }

        /// <summary>
        /// Gets the priorized degrees of one octave.
        /// </summary>
        /// <value>
        /// The priorized degrees of one octave.
        /// </value>
        /// <remarks>The first element is the most important / distinctive one.</remarks>
        ReadOnlyCollection<Degrees> PriorizedDegreesOfOneOctave { get; }
    }
}