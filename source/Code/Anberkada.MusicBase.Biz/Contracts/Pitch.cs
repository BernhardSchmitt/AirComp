
namespace Anberkada.MusicBase.Biz.Contracts
{
    /// <summary>
    /// Enumeration of tone names.
    /// </summary>
    public enum Tone
    {
        /// <summary>
        /// The tone C 
        /// </summary>
        C,

        /// <summary>
        /// The tone C# / Db
        /// </summary>
        Csharp,

        /// <summary>
        /// The tone D
        /// </summary>
        D,

        /// <summary>
        /// The tone D# / Eb
        /// </summary>
        Dsharp,

        /// <summary>
        /// The tone E
        /// </summary>
        E,

        /// <summary>
        /// The tone F
        /// </summary>
        F,

        /// <summary>
        /// The tone F# / Gb
        /// </summary>
        Fsharp,

        /// <summary>
        /// The tone G
        /// </summary>
        G,

        /// <summary>
        /// The tone G# / Ab
        /// </summary>
        Gsharp,

        /// <summary>
        /// The tone A
        /// </summary>
        A,

        /// <summary>
        /// The tone A# / Bb
        /// </summary>
        Asharp,

        /// <summary>
        /// The tone B
        /// </summary>
        B,

        /// <summary>
        /// The undefined tone
        /// </summary>
        Undefined
    }

    /// <summary>
    /// Data structure for the logical pitch.
    /// </summary>
    public class Pitch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pitch" /> class.
        /// </summary>
        /// <param name="octaveNumber">The octave number.</param>
        /// <param name="toneName">Name of the tone.</param>
        public Pitch(int octaveNumber, Tone toneName)
        {
            OctaveNumber = octaveNumber;
            ToneName = toneName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pitch"/> class.
        /// </summary>
        /// <param name="noteValue">The (MIDI) note value.</param>
        public Pitch(int noteValue)
        {
            OctaveNumber = GetOctave(noteValue);
            ToneName = GetTone(noteValue);
        }

        /// <summary>
        /// Gets or sets the name of the tone.
        /// </summary>
        /// <value>
        /// The name of the tone.
        /// </value>
        public Tone ToneName { get; set; }

        /// <summary>
        /// Gets or sets the octave number.
        /// </summary>
        /// <value>
        /// The octave number.
        /// </value>
        public int OctaveNumber { get; set; }

        /// <summary>
        /// Gets the tone value als MIDI number.
        /// </summary>
        /// <value>
        /// The tone value.
        /// </value>
        /// <remarks>C-2 belongs to MIDI number 0.</remarks>
        public int Value
        {
            get
            {
                return (OctaveNumber + 2) * 12 + (int)ToneName;
            }
        }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public static int MaxValue
        {
            get
            {
                return 127;
            }
        }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public static int MinValue
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string noteName;

            switch (ToneName)
            {
                case Tone.C : noteName = "C"; break;
                case Tone.Csharp: noteName = "C# / Db"; break;
                case Tone.D: noteName = "D"; break;
                case Tone.Dsharp: noteName = "D# / Eb"; break;
                case Tone.E: noteName = "E"; break;
                case Tone.F: noteName = "F"; break;
                case Tone.Fsharp: noteName = "F# / Gb"; break;
                case Tone.G: noteName = "G"; break;
                case Tone.Gsharp: noteName = "G# / Ab"; break;
                case Tone.A: noteName = "A"; break;
                case Tone.Asharp: noteName = "A# / Bb"; break;
                case Tone.B: noteName = "B"; break;
                default: return "n/a";
            }

            return string.Format("{0} {1:d}", noteName, OctaveNumber); 
        }

        #region Equality & comparison

        /// <summary>
        /// Determines whether the specified object is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified object is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Pitch);
        }

        /// <summary>
        /// Checks if the specified pitch is equal to this instance.
        /// </summary>
        /// <param name="pitch">The specified pitch.</param>
        /// <returns><c>true</c> if the specified pitch is equal to this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(Pitch pitch)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(pitch, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (ReferenceEquals(this, pitch))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (GetType() != pitch.GetType())
                return false;

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            return (Value == pitch.Value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value;
        }

        /// <summary>
        /// Equal (==) operator.
        /// </summary>
        /// <param name="lhs">The operator on the left side.</param>
        /// <param name="rhs">The operator on the right side.</param>
        /// <returns>true, if equal; otherwise false.</returns>
        public static bool operator ==(Pitch lhs, Pitch rhs)
        {
            // Check for null on left side. 
            if (ReferenceEquals(lhs, null))
            {
                if (ReferenceEquals(rhs, null))
                {
                    // null == null = true. 
                    return true;
                }

                // Only the left side is null. 
                return false;
            }
            // Equals handles case of null on right side. 
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Unequal (!=) operator.
        /// </summary>
        /// <param name="lhs">The operator on the left side.</param>
        /// <param name="rhs">The operator on the right side.</param>
        /// <returns>true, if NOT equal; otherwise false.</returns>
        public static bool operator !=(Pitch lhs, Pitch rhs)
        {
            // Check for null on left side. 
            if (ReferenceEquals(lhs, null))
            {
                if (ReferenceEquals(rhs, null))
                {
                    // null == null = false. 
                    return false;
                }

                // Only the left side is null. 
                return true;
            }
            // Equals handles case of null on right side. 
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Greater than or equal operator.
        /// </summary>
        /// <param name="lhs">The operator on the left side.</param>
        /// <param name="rhs">The operator on the right side.</param>
        /// <returns>true, if lhs is greater than or equal than rhs; otherwise false.</returns>
        public static bool operator >=(Pitch lhs, Pitch rhs)
        {
            return lhs.GetHashCode() >= rhs.GetHashCode();
        }

        /// <summary>
        /// Greater than operator.
        /// </summary>
        /// <param name="lhs">The operator on the left side.</param>
        /// <param name="rhs">The operator on the right side.</param>
        /// <returns>true, if lhs is greater than rhs; otherwise false.</returns>
        public static bool operator >(Pitch lhs, Pitch rhs)
        {
            return lhs.GetHashCode() > rhs.GetHashCode();
        }

        /// <summary>
        /// Less than or equal operator.
        /// </summary>
        /// <param name="lhs">The operator on the left side.</param>
        /// <param name="rhs">The operator on the right side.</param>
        /// <returns>true, if lhs is less than or equal than rhs; otherwise false.</returns>
        public static bool operator <=(Pitch lhs, Pitch rhs)
        {
            return lhs.GetHashCode() <= rhs.GetHashCode();
        }

        /// <summary>
        /// Less than operator.
        /// </summary>
        /// <param name="lhs">The operator on the left side.</param>
        /// <param name="rhs">The operator on the right side.</param>
        /// <returns>true, if lhs is less than rhs; otherwise false.</returns>
        public static bool operator <(Pitch lhs, Pitch rhs)
        {
            return lhs.GetHashCode() <= rhs.GetHashCode();
        }

        #endregion // Equality & comparison

        /// <summary>
        /// Gets the octave number.
        /// </summary>
        /// <param name="noteValue">The note value.</param>
        /// <returns>The octave number.</returns>
        private int GetOctave(int noteValue)
        {
            return noteValue / 12 - 2;
        }

        /// <summary>
        /// Gets the tone.
        /// </summary>
        /// <param name="noteValue">The note value.</param>
        /// <returns>The tone.</returns>
        private Tone GetTone(int noteValue)
        {
            return (Tone)(noteValue % 12);
        }
    }
}
