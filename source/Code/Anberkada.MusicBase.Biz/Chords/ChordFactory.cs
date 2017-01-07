
namespace Anberkada.MusicBase.Biz.Chords
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    /// <summary>
    /// Factory to create chords.
    /// </summary>
    public class ChordFactory : IChordFactory
    {
        /// <summary>
        /// Creates the specified chord.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="chordId">The chord identifier.</param>
        /// <returns>
        /// The created object.
        /// </returns>
        public IChord CreateChord(Tone baseTone, Chords chordId)
        {
            IList<Degrees> degrees = null;

            if (chordId.ToString().StartsWith(Chords.Major.ToString()))
            {
                degrees = CreateMajorDegrees();
            }
            else if (chordId.ToString().StartsWith(Chords.Minor.ToString()))
            {
                degrees = CreateMinorDegrees();
            }

            if (degrees == null)
            {
                throw new NotSupportedException("Not supported chord Id");
            }

            switch (chordId)
            {
                case Chords.Major:
                    return CreateChord(baseTone, chordId, degrees);

                case Chords.Major_b7:
                    Add_b7(ref degrees);

                    return CreateChord(baseTone, chordId, degrees);

                case Chords.Major_j7:
                    Add_j7(ref degrees);

                    return CreateChord(baseTone, chordId, degrees);

                case Chords.Minor:
                    return CreateChord(baseTone, chordId, degrees);

                case Chords.Minor_b7:
                    Add_b7(ref degrees);

                    return CreateChord(baseTone, chordId, degrees);

                default:
                    throw new NotSupportedException("Not supported chord Id");
            }
        }

        #region Private methods

        /// <summary>
        /// Creates the chord.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="chordId">The chord identifier.</param>
        /// <param name="degrees">The list of degrees.</param>
        /// <returns>The created chord.</returns>
        private IChord CreateChord(Tone baseTone, Chords chordId, IList<Degrees> degrees)
        {
            return new DelegateChord(baseTone, chordId, degrees);
        }

        #region Base degrees

        /// <summary>
        /// Creates the degrees list for a major chord.
        /// </summary>
        /// <returns>The created list.</returns>
        private IList<Degrees> CreateMajorDegrees()
        {
            return new List<Degrees>
            {
                Degrees.I_perfectPrime,
                Degrees.III_majorThird,
                Degrees.V_perfectFifth
            };
        }

        /// <summary>
        /// Creates the degrees list for a minor chord.
        /// </summary>
        /// <returns>The created list.</returns>
        private IList<Degrees> CreateMinorDegrees()
        {
            return new List<Degrees>
            {
                Degrees.I_perfectPrime,
                Degrees.III_minorThird,
                Degrees.V_perfectFifth
            };
        }

        #endregion // Base degrees

        #region Additions

        /// <summary>
        /// Adds a b7 to the specified degrees list.
        /// </summary>
        /// <param name="degrees">The degrees list.</param>
        private void Add_b7(ref IList<Degrees> degrees)
        {
            if (degrees != null)
            {
                degrees.Add(Degrees.VII_minorSeventh);
            }
        }

        /// <summary>
        /// Adds a major 7 to the specified degrees list.
        /// </summary>
        /// <param name="degrees">The degrees list.</param>
        private void Add_j7(ref IList<Degrees> degrees)
        {
            if (degrees != null)
            {
                degrees.Add(Degrees.VII_majorSeventh);
            }
        }

        #endregion // Additions

        #region Alterations

        #endregion // Alterations

        #endregion // Private methods
    }
}