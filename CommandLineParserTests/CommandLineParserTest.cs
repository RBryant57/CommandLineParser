using System;
using CommandLineParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CommandLineParserTests
{
    [TestClass]
    public class CommandLineParserTest
    {
        [TestMethod]
        public void CommandLineShouldParseCorrectly()
        {
            var stringUnderTest = "--key --anotherKey=value --yetAnotherKey --StillAnotherKey=anotherValue";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
            Assert.IsTrue(result.Count == 4);
        }

        [TestMethod]
        public void CompactedCommandLineShouldParseCorrectly()
        {
            var stringUnderTest = "--key--anotherKey=value--yetAnotherKey--StillAnotherKey=anotherValue";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
            Assert.IsTrue(result.Count == 4);
        }

        [TestMethod]
        public void ExpandedCommandLineShouldParseCorrectly()
        {
            var stringUnderTest = "--key          --anotherKey=value    --   yetAnotherKey           --StillAnotherKey=anotherValue";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
            Assert.IsTrue(result.Count == 4);
        }

        [TestMethod]
        public void KeysShouldContainNoDashes()
        {
            var stringUnderTest = "--key --anotherKey=value --yetAnotherKey";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
            Assert.IsFalse(result.Keys.Any(k => k.Contains("--")));
        }

        [TestMethod]
        public void KeysShouldBeParsedCorrectly()
        {
            var stringUnderTest = "--key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
            Assert.AreEqual(result.Keys.First(), "key");
        }

        [TestMethod]
        public void ValuesShouldBeParsedCorrectly()
        {
            var stringUnderTest = "--key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
            Assert.AreEqual(result.Values.First(), "value");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandLineFormatException))]
        public void DuplicateKeysShouldNotParseCorrectly()
        {
            var stringUnderTest = "--key=value --key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandLineFormatException))]
        public void InvalidCommandLineShouldNotParseCorrectly()
        {
            var stringUnderTest = "keyvalue=valuesomestuff";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest);

        }
    }
}
