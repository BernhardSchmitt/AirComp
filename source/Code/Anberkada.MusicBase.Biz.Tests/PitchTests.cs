
namespace Anberkada.MusicBase.Biz.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Contracts;

    [TestClass]
    public class PitchTests
    {
        private Pitch _target;

        [TestMethod]
        public void PitchConstructorByValue()
        {
            // Arrange

            // Act
            _target = new Pitch(60);

            // Assert
            Assert.AreEqual(Tone.C, _target.ToneName);
            Assert.AreEqual(3, _target.OctaveNumber);
        }

        [TestMethod]
        public void ValueForCMinus2ShouldBe0Test()
        {
            // Arrange
            _target = new Pitch(-2, Tone.C);
            
            // Act
            var value = _target.Value;

            // Assert
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void ValueForC3ShouldBe60Test()
        {
            // Arrange
            _target = new Pitch(3, Tone.C);
            
            // Act
            var value = _target.Value;

            // Assert
            Assert.AreEqual(60, value);
        }

        [TestMethod]
        public void ToStringForNotSetTest()
        {
            // Arrange
            _target = new Pitch(0, Tone.Undefined);

            // Act
            var name = _target.ToString();

            // Assert
            Assert.AreEqual("n/a", name);
        }

        [TestMethod]
        public void ToStringForC1Test()
        {
            // Arrange
            _target = new Pitch(1, Tone.C);
            
            // Act
            var name = _target.ToString();

            // Assert
            Assert.AreEqual("C 1", name);
        }

        [TestMethod]
        public void ToStringForCSharp2Test()
        {
            // Arrange
            _target = new Pitch(2, Tone.Csharp);
            
            // Act
            var name = _target.ToString();

            // Assert
            Assert.AreEqual("C# / Db 2", name);
        }

        [TestMethod]
        public void ToStringForD3Test()
        {
            // Arrange
            _target = new Pitch(3, Tone.D);
            
            // Act
            var name = _target.ToString();

            // Assert
            Assert.AreEqual("D 3", name);
        }
    }
}
