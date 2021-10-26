using ChessApplication.Logic;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChessApplication.Tests
{
    [TestFixture]
    class LogicTests
    {
        LogicUpdater logic;
        [SetUp]
        public void Setup()
        {
            logic = new LogicUpdater();
        }

        [TestCase]
        public void ValidMoveCountFromStartingPositionTest()
        {

        }
    }
}