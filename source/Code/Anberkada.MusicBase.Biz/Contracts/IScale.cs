
namespace Anberkada.MusicBase.Biz.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for scales.
    /// </summary>
    public interface IScale : IHarmonicStack
    {
        /// <summary>
        /// Gets the name of the scale / mode.
        /// </summary>
        /// <value>
        /// The name of the scale / mode.
        /// </value>
        Scales Scale { get; }

        /// <summary>
        /// Gets the members of the scale.
        /// </summary>
        /// <param name="minPitch">The minimum pitch.</param>
        /// <param name="maxPitch">The maximum pitch.</param>
        /// <returns>The scale members.</returns>
        IList<Pitch> GetMembers(Pitch minPitch, Pitch maxPitch);

        /// <summary>
        /// Gets the pitch by position.
        /// </summary>
        /// <param name="normalizedPosition">The normalized position, range [0..1,0].</param>
        /// <param name="minPitch">The minimum pitch.</param>
        /// <param name="maxPitch">The maximum pitch.</param>
        /// <returns>The calculated pitch.</returns>
        Pitch GetPitchByPosition(double normalizedPosition, Pitch minPitch, Pitch maxPitch);
    }
}
