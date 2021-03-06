﻿
namespace Anberkada.AirComp.Biz.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Scales;
    using Contracts;

    [TestClass]
    public class BluesScaleTests
    {
        [TestMethod]
        public void BluesScale_Constructor_Test()
        {
            // Arrange

            // Act
            var target = new BluesScale(Tone.C);

            // Assert
            Assert.AreEqual(Tone.C, target.BaseTone);
        }

        #region GetScaleMembers

        [TestMethod]
        public void BluesGFromG0ToG1_GetScaleMembers_Test()
        {
            // Arrange
            var target = new BluesScale(Tone.G);

            // Act
            var scaleInG = target.GetScaleMembers(new Pitch(0, Tone.G), new Pitch(1, Tone.G));

            // Assert
            Assert.AreEqual(new Pitch(0, Tone.G), scaleInG[0]);
            Assert.AreEqual(new Pitch(0, Tone.Asharp), scaleInG[1]);
            Assert.AreEqual(new Pitch(1, Tone.C), scaleInG[2]);
            Assert.AreEqual(new Pitch(1, Tone.Csharp), scaleInG[3]);
            Assert.AreEqual(new Pitch(1, Tone.D), scaleInG[4]);
            Assert.AreEqual(new Pitch(1, Tone.F), scaleInG[5]);
        }

        #endregion // GetScaleMembers
    }
}
