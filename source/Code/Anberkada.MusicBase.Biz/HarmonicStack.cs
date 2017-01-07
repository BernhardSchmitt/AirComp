
namespace Anberkada.MusicBase.Biz
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    
    using Contracts;
    using Scales;

    /// <summary>
    /// A harmonic stack as base for chords and scales.
    /// </summary>
    public abstract class HarmonicStack : IHarmonicStack
    {
        private Tone _baseTone;
        private ReadOnlyCollection<Degrees> _degreesOfOneOctave;
        private ReadOnlyCollection<Tone> _tonesOfOneOctave;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleBase" /> class.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        protected HarmonicStack(Tone baseTone)
        {
            _baseTone = baseTone;
        }

        /// <summary>
        /// Gets the tonal base of the scale.
        /// </summary>
        /// <value>
        /// The tonal base.
        /// </value>
        public Tone BaseTone
        {
            get { return _baseTone; }
        }

        /// <summary>
        /// Gets the degrees of one octave.
        /// </summary>
        /// <value>
        /// The degrees of one octave.
        /// </value>
        public ReadOnlyCollection<Degrees> DegreesOfOneOctave 
        { 
            get
            {
                if (_degreesOfOneOctave == null)
                {
                    _degreesOfOneOctave = CreateDegreesOfOneOctaveCollection();
                }

                return _degreesOfOneOctave;
            }
        }

        /// <summary>
        /// Gets the tones of one octave.
        /// </summary>
        /// <value>
        /// The tones of one octave.
        /// </value>
        protected ReadOnlyCollection<Tone> TonesOfOneOctave
        {
            get
            {
                if (_tonesOfOneOctave == null)
                {
                    _tonesOfOneOctave = CreateTonesOfOneOctaveCollection();
                }

                return _tonesOfOneOctave;
            }
        }

        /// <summary>
        /// Gets the members of the scale.
        /// </summary>
        /// <param name="minPitch">The minimum pitch.</param>
        /// <param name="maxPitch">The maximum pitch.</param>
        /// <returns>
        /// The scale members.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// maxPitch.OctaveNumber
        /// or
        /// minPitch.ToneName
        /// or
        /// maxPitch.ToneName
        /// </exception>
        public IList<Pitch> GetMembers(Pitch minPitch, Pitch maxPitch)
        {
            if (maxPitch < minPitch)
            {
                throw new ArgumentOutOfRangeException("maxPitch");
            }

            // Find start pitch and ensure, that it is part of the scale
            var startPitch = minPitch;
            if (!IsPitchPartOfScale(startPitch))
            {
                startPitch = GetNextScalePitch(startPitch);
            }

            // Create result scale
            var result = new List<Pitch>();
            result.Add(startPitch);
            for (var nextPitch = GetNextScalePitch(startPitch); nextPitch <= maxPitch; nextPitch = GetNextScalePitch(nextPitch))
            {
                result.Add(nextPitch);
            }

            return result;
        }

        /// <summary>
        /// Creates the degrees of one octave collection.
        /// </summary>
        /// <returns>The created collection.</returns>
        protected abstract ReadOnlyCollection<Degrees> CreateDegreesOfOneOctaveCollection();

        /// <summary>
        /// Gets the next pitch of the scale.
        /// </summary>
        /// <param name="source">The source pitch.</param>
        /// <returns>The next pitch.</returns>
        private Pitch GetNextScalePitch(Pitch source)
        {
            var nextPitch = new Pitch(source.Value + 1);

            while (!IsPitchPartOfScale(nextPitch))
            {
                nextPitch = new Pitch(nextPitch.Value + 1);

                if (nextPitch.Value == 128)
                {
                    return null;
                }
            }

            return nextPitch;
        }

        /// <summary>
        /// Determines whether the specified pitch is part of the scale.
        /// </summary>
        /// <param name="pitch">The pitch.</param>
        /// <returns>true, if part of the scale; otherwise false;</returns>
        private bool IsPitchPartOfScale(Pitch pitch)
        {
            return TonesOfOneOctave.Contains(pitch.ToneName);
        }

        /// <summary>
        /// Creates the tones of one octave collection.
        /// </summary>
        /// <returns>The created collection.</returns>
        private ReadOnlyCollection<Tone> CreateTonesOfOneOctaveCollection()
        {
            var result = new List<Tone>();
            foreach (var degree in DegreesOfOneOctave)
            {
                int toneValue = (int)BaseTone + (int)degree;
                // tone value as circular buffer of 12 halftones
                result.Add((Tone)(toneValue % 12));
            }

            return new ReadOnlyCollection<Tone>(result);
        }


        /// <summary>
        /// Gets the priorized degrees of one octave.
        /// </summary>
        /// <value>
        /// The priorized degrees of one octave.
        /// </value>
        /// <remarks>The first element is the most important / distinctive one.</remarks>
        public virtual ReadOnlyCollection<Degrees> PriorizedDegreesOfOneOctave
        {
            get
            {
                var result = new List<Degrees>();

                // 3rd is the most distinctive element
                var element = Degrees.III_minorThird;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }
                element = Degrees.III_majorThird;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                // 7th is also very dinstinctive
                element = Degrees.VII_minorSeventh;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }
                element = Degrees.VII_majorSeventh;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                // 6th
                element = Degrees.VI_minorSixth;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }
                element = Degrees.VI_majorSixth;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                // 2nd
                element = Degrees.II_minorSecond;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }
                element = Degrees.II_majorSecond;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                // 4th
                element = Degrees.IV_augmentedFourth;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }
                element = Degrees.IV_perfectFourth;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                // 5th
                element = Degrees.V_perfectFifth;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                // 1st
                element = Degrees.I_perfectPrime;
                if (DegreesOfOneOctave.Contains(element))
                {
                    result.Add(element);
                }

                Debug.Assert(result.Count == DegreesOfOneOctave.Count);

                return new ReadOnlyCollection<Degrees>(result);
            }
        }
    }
}