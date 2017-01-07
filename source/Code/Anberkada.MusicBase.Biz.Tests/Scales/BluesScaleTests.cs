
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class BluesScaleTests
    {
        [TestMethod]
        public void BluesScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = CreateTarget(Tone.C);

            // Assert
            Assert.AreEqual(Tone.C, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void BluesGFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = CreateTarget(Tone.G);

            // Act
            var scaleInG = target.GetMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.Asharp), scaleInG[1]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInG[2]);
            Assert.AreEqual(new Pitch(1, Tone.Csharp), scaleInG[3]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[4]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInG[5]);
        }

        #endregion // GetMembers

        #region PriorizedDegreesOfOneOctave

        [TestMethod]
        public void PriorizedDegreesOfOneOctave_Test()
        {
            // Arrange

            // Act
            var target = CreateTarget(Tone.C);

            // Assert
            Assert.AreEqual(Degrees.III_minorThird, target.PriorizedDegreesOfOneOctave[0]);
            Assert.AreEqual(Degrees.VII_minorSeventh, target.PriorizedDegreesOfOneOctave[1]);
            Assert.AreEqual(Degrees.IV_augmentedFourth, target.PriorizedDegreesOfOneOctave[2]);
            Assert.AreEqual(Degrees.IV_perfectFourth, target.PriorizedDegreesOfOneOctave[3]);
            Assert.AreEqual(Degrees.V_perfectFifth, target.PriorizedDegreesOfOneOctave[4]);
            Assert.AreEqual(Degrees.I_perfectPrime, target.PriorizedDegreesOfOneOctave[5]);
        }

        #endregion // PriorizedDegreesOfOneOctave

        #region Private helpers

        /// <summary>
        /// Creates the target.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <returns>The created target.</returns>
        private BluesScale CreateTarget(Tone baseTone)
        {
            return new BluesScale(baseTone);
        }

        #endregion // Private helpers
    }
}
