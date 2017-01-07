
namespace Anberkada.AirComp.Biz.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Scales;
    using Contracts;

    [TestClass]
    public class MinorPentatonicScaleTests
    {
        [TestMethod]
        public void MinorPentatonicScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new MinorPentatonicScale(Tone.C);

            // Assert
            Assert.AreEqual(Tone.C, target.BaseTone);
        }

        #region GetScaleMembers

        [TestMethod]
        public void MinorPentatonicGFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new MinorPentatonicScale(Tone.G);

            // Act
            var scaleInG = target.GetScaleMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.Asharp), scaleInG[1]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInG[2]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[3]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInG[4]);
        }

        #endregion // GetScaleMembers
    }
}
