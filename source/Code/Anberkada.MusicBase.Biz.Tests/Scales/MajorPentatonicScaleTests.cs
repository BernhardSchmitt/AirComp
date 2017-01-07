
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class MajorPentatonicScaleTests
    {
        [TestMethod]
        public void MajorPentatonicScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = CreateTarget(Tone.C);

            // Assert
            Assert.AreEqual(Tone.C, target.BaseTone);
        }

        #region GetScaleMembers

        [TestMethod]
        public void MajorPentatonicGFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.G);

            // Act
            var scaleInG = target.GetMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInG[1]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInG[2]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[3]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInG[4]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInG[5]);
        }

        [TestMethod]
        public void PentatonicGFromF0ToGsharp1_GetScaleMembers_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.G);

            // Act
            var scaleInG = target.GetMembers(new Pitch(0, Tone.F), new Pitch(1, Tone.Gsharp));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInG[1]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInG[2]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[3]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInG[4]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInG[5]);
        }

        #endregion // GetMembers

        #region GetPitchByPosition

        [TestMethod]
        public void MajorPentatonicFFromF0ToD1_GetPitchByPosition_0_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.F);

            // Act
            var pitch = target.GetPitchByPosition(0.0, new Pitch(0, Tone.F), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.F), pitch);
        }

        [TestMethod]
        public void MajorPentatonicFFromF0ToD1_GetPitchByPosition_0_25_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.F);

            // Act
            var pitch = target.GetPitchByPosition(0.25, new Pitch(0, Tone.F), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), pitch);
        }

        [TestMethod]
        public void MajorPentatonicFFromF0ToD1_GetPitchByPosition_0_5_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.F);

            // Act
            var pitch = target.GetPitchByPosition(0.5, new Pitch(0, Tone.F), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.A), pitch);
        }

        [TestMethod]
        public void MajorPentatonicFFromF0ToD1_GetPitchByPosition_0_75_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.F);

            // Act
            var pitch = target.GetPitchByPosition(0.75, new Pitch(0, Tone.F), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(1, Tone.C), pitch);
        }

        [TestMethod]
        public void MajorPentatonicFFromF0ToD1_GetPitchByPosition_1_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.F);

            // Act
            var pitch = target.GetPitchByPosition(1.0, new Pitch(0, Tone.F), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(1, Tone.D), pitch);
        }

        #endregion // GetPitchByPosition

        #region PriorizedDegreesOfOneOctave

        [TestMethod]
        public void PriorizedDegreesOfOneOctave_Test()
        {
            // Arrange

            // Act
            var target = CreateTarget(Tone.C);

            // Assert
            Assert.AreEqual(Degrees.III_majorThird, target.PriorizedDegreesOfOneOctave[0]);
            Assert.AreEqual(Degrees.VI_majorSixth, target.PriorizedDegreesOfOneOctave[1]);
            Assert.AreEqual(Degrees.II_majorSecond, target.PriorizedDegreesOfOneOctave[2]);
            Assert.AreEqual(Degrees.V_perfectFifth, target.PriorizedDegreesOfOneOctave[3]);
            Assert.AreEqual(Degrees.I_perfectPrime, target.PriorizedDegreesOfOneOctave[4]);
        }

        #endregion // PriorizedDegreesOfOneOctave

        #region Private helpers

        /// <summary>
        /// Creates the target.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <returns>The created target.</returns>
        private MajorPentatonicScale CreateTarget(Tone baseTone)
        {
            return new MajorPentatonicScale(baseTone);
        }

        #endregion // Private helpers
    }
}
