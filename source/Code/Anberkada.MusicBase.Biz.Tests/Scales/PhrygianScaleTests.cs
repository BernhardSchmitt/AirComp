
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class PhrygianScaleTests
    {
        [TestMethod]
        public void PhrygianScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new PhrygianScale(Tone.E);

            // Assert
            Assert.AreEqual(Tone.E, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void PhrygianEFromE0ToD1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new PhrygianScale(Tone.E);

            // Act
            var scaleInE = target.GetMembers(new Pitch(0, Tone.E), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.E), scaleInE[0]);
            Assert.AreEqual(new Pitch(0, Tone.F), scaleInE[1]);
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInE[2]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInE[3]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInE[4]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInE[5]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInE[6]);
        }

        [TestMethod]
        public void PhrygianEFromF0ToF1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new PhrygianScale(Tone.E);

            // Act
            var scaleInE = target.GetMembers(new Pitch(0, Tone.F), new Pitch(1, Tone.F));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.F), scaleInE[0]);
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInE[1]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInE[2]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInE[3]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInE[4]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInE[5]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInE[6]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInE[7]);
        }

        #endregion // GetMembers
    }
}
