using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParser
{
    public class CommandLineParser
    {
        /// <summary>
        /// In a production application these would be stored in a content management system and set through configuration.
        /// </summary>
        private const string FORMAT_MESSAGE =
            "Invalid Format. Commands should be in the format of:\n --command name for uniary commands\n --command=parameter for commands that take parameters";

        private const string SUCCESS_MESSAGE = "Success.";
        
        public Dictionary<string, string> Parse(string commandLine, out string message)
        {
            commandLine = commandLine.Trim();
            var result = new Dictionary<string, string>();

            if (commandLine[0] != '-' && commandLine[1] != '-')
            {
                message = FORMAT_MESSAGE;
                return result;
            }
            
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
                var key = String.Empty;

                try
                {
                    if (command.Contains("="))
                    {
                        var index = command.IndexOf("=");
                        key = command.Substring(0, index);
                        var value = command.Substring(index + 1);

                        if (!ValidateCommand(key))
                        {
                            result.Clear();
                            message = "The command: " + key + " is not recognized.";
                            return result;
                        }

                        if (!ValidateParameter(key, value))
                        {
                            result.Clear();
                            message = "The parameter: " + value + " is not recognized as a parameter for the command: " + key + ".";
                            return result;
                        }

                        result.Add(key.Trim(), value.Trim());
                    }
                    else
                    {
                        result.Add(command.Trim(), String.Empty);
                    }
                }
                catch (ArgumentException ex)
                {
                    message = "The command: " + key + " has already been invoked.";
                    result.Clear();
                    return result;
                }
            }

            message = SUCCESS_MESSAGE;
            return result;
        }

        private string TrimKey(string key)
        {
            return key.Substring(2, key.Length - 2);
        }

        /// <summary>
        /// In a production application, this would validate a command against a
        /// set of accepted values.
        /// </summary>
        /// <param name="command">The command to be validated.</param>
        /// <returns>Indicates whether or not the command is valid.</returns>
        private bool ValidateCommand(string command)
        {
            return true;
        }

        /// <summary>
        /// In a production application, this would validate a command parameter against a
        /// set of accepted values.
        /// </summary>
        /// <param name="command">The command against which to validate the parameter.</param>
        /// <param name="parameter">The parameter to be validated.</param>
        /// <returns>Indicates whether or not the parameter is valid.</returns>
        private bool ValidateParameter(string command, string parameter)
        {
            return true;
        }
    }

}
