
namespace Anberkada.MusicBase.Biz.Tests.Chords
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Chords;
    using Contracts;

    [TestClass]
    public class ChordFactoryTests
    {
        private IChordFactory _target;

        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _target = new ChordFactory();
        }

        /// <summary>
        /// Cleanups the test.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            _target = null;
        }

        #region Major chords

        /// <summary>
        /// Test for the major chord.
        /// </summary>
        [TestMethod]
        public void CreateMajorChordTest()
        {
            // Arrange
            const Tone baseTone = Tone.C;
            const Chords chordId = Chords.Major;

            // Act
            var myChord = _target.CreateChord(baseTone, chordId);

            // Assert
            Assert.IsNotNull(myChord);
            Assert.AreEqual(chordId, myChord.Chord);
            Assert.AreEqual(baseTone, myChord.BaseTone);
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.I_perfectPrime));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.III_majorThird));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.V_perfectFifth));
        }

        /// <summary>
        /// Test for the major b7 chord.
        /// </summary>
        [TestMethod]
        public void CreateMajor_b7ChordTest()
        {
            // Arrange
            const Tone baseTone = Tone.C;
            const Chords chordId = Chords.Major_b7;

            // Act
            var myChord = _target.CreateChord(baseTone, chordId);

            // Assert
            Assert.IsNotNull(myChord);
            Assert.AreEqual(chordId, myChord.Chord);
            Assert.AreEqual(baseTone, myChord.BaseTone);
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.I_perfectPrime));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.III_majorThird));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.V_perfectFifth));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.VII_minorSeventh));
        }

        /// <summary>
        /// Test for the major j7 chord.
        /// </summary>
        [TestMethod]
        public void CreateMajor_j7ChordTest()
        {
            // Arrange
            const Tone baseTone = Tone.C;
            const Chords chordId = Chords.Major_j7;

            // Act
            var myChord = _target.CreateChord(baseTone, chordId);

            // Assert
            Assert.IsNotNull(myChord);
            Assert.AreEqual(chordId, myChord.Chord);
            Assert.AreEqual(baseTone, myChord.BaseTone);
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.I_perfectPrime));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.III_majorThird));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.V_perfectFifth));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.VII_majorSeventh));
        }

        #endregion // Major chords

        #region Minor chord

        /// <summary>
        /// Test for the minor chord.
        /// </summary>
        [TestMethod]
        public void CreateMinorChordTest()
        {
            // Arrange
            const Tone baseTone = Tone.C;
            const Chords chordId = Chords.Minor;

            // Act
            var myChord = _target.CreateChord(baseTone, chordId);

            // Assert
            Assert.IsNotNull(myChord);
            Assert.AreEqual(chordId, myChord.Chord);
            Assert.AreEqual(baseTone, myChord.BaseTone);
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.I_perfectPrime));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.III_minorThird));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.V_perfectFifth));
        }

        /// <summary>
        /// Test for the minor b7 chord.
        /// </summary>
        [TestMethod]
        public void CreateMinor_b7ChordTest()
        {
            // Arrange
            const Tone baseTone = Tone.C;
            const Chords chordId = Chords.Minor_b7;

            // Act
            var myChord = _target.CreateChord(baseTone, chordId);

            // Assert
            Assert.IsNotNull(myChord);
            Assert.AreEqual(chordId, myChord.Chord);
            Assert.AreEqual(baseTone, myChord.BaseTone);
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.I_perfectPrime));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.III_minorThird));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.V_perfectFifth));
            Assert.IsTrue(myChord.DegreesOfOneOctave.Contains(Degrees.VII_minorSeventh));
        }

        #endregion // Minor chord
    }
}
