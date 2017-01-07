

namespace Anberkada.MusicBase.Biz.Scales
{
    using System;
    using System.Diagnostics;

    using Contracts;

    /// <summary>
    /// The base class for all scale implementations.
    /// </summary>
    public abstract class ScaleBase : HarmonicStack, IScale
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleBase" /> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        protected ScaleBase(Tone baseTone) : base(baseTone)
        {
        }

        /// <summary>
        /// Gets the name of the scale / mode.
        /// </summary>
        /// <value>
        /// The name of the scale / mode.
        /// </value>
        public abstract Scales Scale { get; }

        /// <summary>
        /// Gets the pitch by position.
        /// </summary>
        /// <param name="normalizedPosition">The normalized position, range [0..1,0].</param>
        /// <param name="minPitch">The minimum pitch.</param>
        /// <param name="maxPitch">The maximum pitch.</param>
        /// <returns>The calculated pitch.</returns>
        public Pitch GetPitchByPosition(double normalizedPosition, Pitch minPitch, Pitch maxPitch)
        {
            if (normalizedPosition > 1.0 || normalizedPosition < 0.0)
            {
                throw new ArgumentOutOfRangeException("normalizedPosition");
            }

            var scaleMembers = GetMembers(minPitch, maxPitch);
            var scaleMembersCount = scaleMembers.Count;
            var scalePosition = (int)((scaleMembersCount - 1) * normalizedPosition);

            Debug.Assert(scalePosition >= 0);
            Debug.Assert(scalePosition < scaleMembersCount);

            return scaleMembers[scalePosition];
        }
    }
}
