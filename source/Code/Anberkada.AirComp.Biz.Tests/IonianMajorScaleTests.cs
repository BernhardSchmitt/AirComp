
namespace Anberkada.AirComp.Biz.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Scales;
    using Contracts;

    [TestClass]
    public class IonianMajorScaleTests
    {
        [TestMethod]
        public void IonianMajorScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new IonianMajorScale(Tone.C);

            // Assert
            Assert.AreEqual(Tone.C, target.BaseTone);
        }

        #region GetScaleMembers

        [TestMethod]
        public void IonianMajorDFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new IonianMajorScale(Tone.D);

            // Act
            var scaleInD = target.GetScaleMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInD[0]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInD[1]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInD[2]);
            Assert.AreEqual(new Pitch(1, Tone.Csharp), scaleInD[3]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInD[4]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInD[5]);
            Assert.AreEqual(new Pitch(1, Tone.Fsharp), scaleInD[6]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInD[7]);
        }

        #endregion // GetScaleMembers
    }
}
