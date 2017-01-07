
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class MixolydianScaleTests
    {
        [TestMethod]
        public void MixolydianScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new MixolydianScale(Tone.F);

            // Assert
            Assert.AreEqual(Tone.F, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void MixolydianGFromA0ToA1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new MixolydianScale(Tone.G);

            // Act
            var scaleInG = target.GetMembers(new Pitch(0, Tone.A), new Pitch(1, Tone.A));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInG[1]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInG[2]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[3]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInG[4]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInG[5]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInG[6]);
            Assert.AreEqual(new Pitch(1, Tone.A), scaleInG[7]);
        }

        #endregion // GetMembers
    }
}
