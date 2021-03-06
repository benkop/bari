﻿using System;
using System.Diagnostics.Contracts;
using Bari.Core.Exceptions;
using Bari.Core.Model;
using Bari.Core.UI;

namespace Bari.Core.Commands
{
    /// <summary>
    /// Implements the <c>help</c> command, which either gives a list of all the registered commands
    /// or the detailed help string of one particular command.
    /// </summary>
    public class HelpCommand: ICommand
    {
        private readonly ICommandEnumerator commandEnumerator;
        private readonly ICommandFactory commandFactory;
        private readonly IUserOutput output;

        /// <summary>
        /// Gets the name of the command. This is the string which can be used on the command line interface
        /// to access the particular command.
        /// </summary>
        public string Name
        {
            get { return "help"; }
        }

        /// <summary>
        /// Gets a short, one-liner description of the command
        /// </summary>
        public string Description
        {
            get { return "displays the available commands and their usage"; }
        }

        /// <summary>
        /// Gets a detailed, multiline description of the command and its possible parameters, usage examples
        /// </summary>
        public string Help
        {
            get { return 
@"=Help command=

When used without parameter, it shows all the available commands in the current suite.
Example: `bari help`

When used with a command name as parameter, it prints detailed usage help for the given command.
Example: `bari help clean`
"; 
            }
        }

        /// <summary>
        /// If <c>true</c>, the target goal is important for this command and must be explicitly specified by the user 
        /// (if the available goal set is not the default)
        /// </summary>
        public bool NeedsExplicitTargetGoal
        {
            get { return false; }
        }

        private const string GlobalParameters =
@"=Global parameters=

Global parameters must be placed before the command name. 
 `-v` enables verbose logging
 `--target X` sets the target goal to X. Default goals are `debug` and `release`
";

        /// <summary>
        /// Constructs the help command
        /// </summary>
        /// <param name="commandFactory">Factory to create command instances</param>
        /// <param name="commandEnumerator">Interface to get all the available commands</param>
        /// <param name="output">The user output interface where help content will be printed</param>
        public HelpCommand(ICommandFactory commandFactory, ICommandEnumerator commandEnumerator, IUserOutput output)
        {
            Contract.Requires(commandFactory != null);
            Contract.Requires(commandEnumerator != null);
            Contract.Requires(output != null);

            this.commandEnumerator = commandEnumerator;
            this.commandFactory = commandFactory;
            this.output = output;
        }

        /// <summary>
        /// Runs the command
        /// </summary>
        /// <param name="suite">The current suite model the command is applied to</param>
        /// <param name="parameters">Parameters given to the command (in unprocessed form)</param>
        public bool Run(Suite suite, string[] parameters)
        {
            if (parameters.Length == 0)
                PrintCommandList();
            else if (parameters.Length == 1)
                PrintCommandHelp(parameters[0]);
            else
                throw new InvalidCommandParameterException("help",
                                                           "The 'help' command must be called with zero or one parameters!");

            return true;
        }

        /// <summary>
        /// Tries to get the command given by its name and prints its help string to the output
        /// </summary>
        /// <param name="cmdName">Name of the command</param>
        private void PrintCommandHelp(string cmdName)
        {
            var cmd = commandFactory.CreateCommand(cmdName);
            if (cmd == null)
                throw new InvalidCommandParameterException(
                    "help",
                    string.Format("The given command ({0}) does no exist", cmdName));
            else
            {
                output.Message(cmd.Help);
                output.Message(GlobalParameters);
            }
        }

        /// <summary>
        /// Gets every registered <see cref="ICommand"/> implementation and prints their name and short
        /// description to the output
        /// </summary>
        private void PrintCommandList()
        {
            output.Message("The following commands can be used for this suite:");

            foreach (var cmdName in commandEnumerator.AvailableCommands)
            {
                var cmd = commandFactory.CreateCommand(cmdName);
                output.Describe(cmdName, cmd.Description);
            }
        }
    }
}