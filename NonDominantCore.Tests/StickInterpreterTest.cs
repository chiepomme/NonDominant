using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NonDominant.Tests
{
    [TestClass]
    public class StickInterpreterTest
    {
        [TestMethod]
        public void 正しくスティックの判定ができる()
        {
            Assert.AreEqual(StickDirection.Neutral, StickInterpreter.Interpret(0, 0));
            Assert.AreEqual(StickDirection.Neutral, StickInterpreter.Interpret(2000, 2000));

            Assert.AreEqual(StickDirection.UpRight, StickInterpreter.Interpret(2500, 2500));
            Assert.AreEqual(StickDirection.Up, StickInterpreter.Interpret(2000, 2500));
            Assert.AreEqual(StickDirection.UpLeft, StickInterpreter.Interpret(1500, 2500));

            Assert.AreEqual(StickDirection.DownLeft, StickInterpreter.Interpret(1500, 1500));
            Assert.AreEqual(StickDirection.Down, StickInterpreter.Interpret(2000, 1500));
            Assert.AreEqual(StickDirection.DownRight, StickInterpreter.Interpret(2500, 1500));
        }
    }
}