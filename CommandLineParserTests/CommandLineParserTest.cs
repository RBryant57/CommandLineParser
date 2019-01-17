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

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count == 4);
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void CompactedCommandLineShouldParseCorrectly()
        {
            var stringUnderTest = "--key--anotherKey=value--yetAnotherKey--StillAnotherKey=anotherValue";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count == 4);
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void ExpandedCommandLineShouldParseCorrectly()
        {
            var stringUnderTest = "--key          --anotherKey=value    --   yetAnotherKey           --StillAnotherKey=anotherValue";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count == 4);
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void KeysShouldContainNoDashes()
        {
            var stringUnderTest = "--key --anotherKey=value --yetAnotherKey";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsFalse(result.Keys.Any(k => k.Contains("--")));
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void KeysShouldBeParsedCorrectly()
        {
            var stringUnderTest = "--key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.AreEqual(result.Keys.First(), "key");
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void ValuesShouldBeParsedCorrectly()
        {
            var stringUnderTest = "--key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.AreEqual(result.Values.First(), "value");
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void ValidCommandsShouldBeParsedCorrectly()
        {
            var stringUnderTest = "--key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void ValidParametersShouldBeParsedCorrectly()
        {
            var stringUnderTest = "--key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual(message, "Success.");
        }

        [TestMethod]
        public void DuplicateKeysShouldNotParseCorrectly()
        {
            var stringUnderTest = "--key=value --key=value";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count == 0);
            Assert.AreEqual(message, "The command: key has already been invoked.");
        }

        [TestMethod]
        public void InvalidCommandLineShouldNotParseCorrectly()
        {
            var stringUnderTest = "keyvalue=valuesomestuff";
            var commandLineParser = new CommandLineParser.CommandLineParser();

            var result = commandLineParser.Parse(stringUnderTest, out var message);
            Assert.IsTrue(result.Count == 0);
            Assert.AreEqual(message, "Invalid Format. Commands should be in the format of:\n --command name for uniary commands\n --command=parameter for commands that take parameters");
        }
    }
}
