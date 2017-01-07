
namespace Anberkada.AirComp.Biz.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Scales;
    using Contracts;

    [TestClass]
    public class LydianScaleTests
    {
        [TestMethod]
        public void LydianScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new LydianScale(Tone.F);

            // Assert
            Assert.AreEqual(Tone.F, target.BaseTone);
        }

        #region GetScaleMembers

        [TestMethod]
        public void LydianFFromF0ToE1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new LydianScale(Tone.F);

            // Act
            var scaleInE = target.GetScaleMembers(new Pitch(0, Tone.F), new Pitch(1, Tone.E));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.F), scaleInE[0]);
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInE[1]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInE[2]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInE[3]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInE[4]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInE[5]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInE[6]);
        }

        [TestMethod]
        public void LydianFFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new LydianScale(Tone.F);

            // Act
            var scaleInE = target.GetScaleMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInE[0]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInE[1]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInE[2]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInE[3]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInE[4]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInE[5]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInE[6]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInE[7]);
        }

        #endregion // GetScaleMembers
    }
}
