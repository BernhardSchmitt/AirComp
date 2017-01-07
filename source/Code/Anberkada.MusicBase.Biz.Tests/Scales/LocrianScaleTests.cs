
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class LocrianScaleTests
    {
        [TestMethod]
        public void LocrianMajorScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new LocrianScale(Tone.B);

            // Assert
            Assert.AreEqual(Tone.B, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void LocrianBFromB0ToC2_GetScaleMembers_Test()
        {
            // Arrange
            var target = new LocrianScale(Tone.B);

            // Act
            var scaleInB = target.GetMembers(new Pitch(0, Tone.B), new Pitch(2, Tone.C));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInB[0]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInB[1]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInB[2]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInB[3]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInB[4]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInB[5]);
            Assert.AreEqual(new Pitch(1, Tone.A), scaleInB[6]);
            Assert.AreEqual(new Pitch(1, Tone.B), scaleInB[7]);
            Assert.AreEqual(new Pitch(2, Tone.C), scaleInB[8]);
        }

        #endregion // GetMembers
    }
}
