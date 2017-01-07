
namespace Anberkada.MusicBase.Biz.Tests.Scales
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Biz.Scales;
    using Contracts;

    [TestClass]
    public class ChromaticScaleTests
    {
        [TestMethod]
        public void ChromaticScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new ChromaticScale(Tone.C);

            // Assert
            Assert.AreEqual(Tone.C, target.BaseTone);
        }

        #region GetMembers

        [TestMethod]
        public void ChromaticGFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new ChromaticScale(Tone.G);

            // Act
            var scaleInG = target.GetMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.Gsharp), scaleInG[1]);
            Assert.AreEqual(new Pitch(0, Tone.A), scaleInG[2]);
            Assert.AreEqual(new Pitch(0, Tone.Asharp), scaleInG[3]);
            Assert.AreEqual(new Pitch(0, Tone.B), scaleInG[4]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInG[5]);
            Assert.AreEqual(new Pitch(1, Tone.Csharp), scaleInG[6]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[7]);
            Assert.AreEqual(new Pitch(1, Tone.Dsharp), scaleInG[8]);
            Assert.AreEqual(new Pitch(1, Tone.E), scaleInG[9]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInG[10]);
            Assert.AreEqual(new Pitch(1, Tone.Fsharp), scaleInG[11]);
            Assert.AreEqual(new Pitch(1, Tone.G), scaleInG[12]);
        }

        #endregion // GetMembers
    }
}
