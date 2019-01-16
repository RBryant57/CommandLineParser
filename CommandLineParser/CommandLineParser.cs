using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public class CommandLineParser
    {
        public Dictionary<string, string> Parse(string commandLine)
        {
            commandLine = commandLine.Trim();

            if (commandLine[0] != '-' && commandLine[1] != '-')
                throw new InvalidCommandLineFormatException();

            var result = new Dictionary<string, string>();
            var indexArray = new List<int>();

            for (int i = 0; i < commandLine.Length; i++)
            {
                if (commandLine[i] == '-' && commandLine[i + 1] == '-')
                    indexArray.Add(i);
            }

            for (int i = 0; i < indexArray.Count; i++)
            {
                var endIndex = i + 1;

                var command = endIndex > indexArray.Count - 1 ? commandLine.Substring(indexArray[i]) : commandLine.Substring(indexArray[i], (indexArray[endIndex] - indexArray[i]));
                command = TrimKey(command);

                try
                {
                    if (command.Contains("="))
                    {
                        var index = command.IndexOf("=");
                        var key = command.Substring(0, index);
                        var value = command.Substring(index + 1);

                        result.Add(key.Trim(), value.Trim());
                    }
                    else
                    {
                        result.Add(command.Trim(), String.Empty);
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidCommandLineFormatException();
                }
            }

            return result;
        }

        private string TrimKey(string key)
        {
            return key.Substring(2, key.Length - 2);
        }
    }

    public class InvalidCommandLineFormatException : Exception
    {

    }
}
