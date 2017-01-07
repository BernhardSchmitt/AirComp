
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class DorianScaleTests
    {
        [TestMethod]
        public void DorianMajorScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new DorianScale(Tone.D);

            // Assert
            Assert.AreEqual(Tone.D, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void DorianDFromE0ToE1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new DorianScale(Tone.D);

            // Act
            var scaleInD = target.GetMembers(new Pitch(0, Tone.E), new Pitch(1, Tone.E));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.E), scaleInD[0]);
            Assert.AreEqual(new Pitch(0, Tone.F), scaleInD[1]);
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInD[2]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInD[3]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInD[4]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInD[5]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInD[6]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInD[7]);
        }

        #endregion // GetMembers
    }
}
