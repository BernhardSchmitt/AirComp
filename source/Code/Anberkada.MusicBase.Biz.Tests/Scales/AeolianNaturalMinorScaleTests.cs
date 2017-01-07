
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class AeolianNaturalMinorScaleTests
    {
        [TestMethod]
        public void AeolianNaturalMinorScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new AeolianNaturalMinorScale(Tone.A);

            // Assert
            Assert.AreEqual(Tone.A, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void AeolianNaturalMinorAFromC0ToD1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new AeolianNaturalMinorScale(Tone.A);

            // Act
            var scaleInA = target.GetMembers(new Pitch(0, Tone.C), new Pitch(1, Tone.D));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.C), scaleInA[0]);
            Assert.AreEqual(new Pitch(0, Tone.D), scaleInA[1]);
            Assert.AreEqual(new Pitch(0, Tone.E), scaleInA[2]);
            Assert.AreEqual(new Pitch(0, Tone.F), scaleInA[3]);
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInA[4]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInA[5]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInA[6]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInA[7]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInA[8]);
        }

        #endregion // GetMembers
    }
}
