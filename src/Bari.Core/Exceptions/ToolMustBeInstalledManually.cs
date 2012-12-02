﻿using System;

namespace Bari.Core.Exceptions
{
    /// <summary>
    /// Exception thrown by the tool system if an external tool cannot be automatically
    /// installed
    /// </summary>
    public class ToolMustBeInstalledManually: Exception
    {
        private readonly string toolName;
        private readonly Uri startingUri;

        /// <summary>
        /// Gets the name of the tool
        /// </summary>
        public string ToolName
        {
            get { return toolName; }
        }

        /// <summary>
        /// Gets the URI where the user can start solving the problem
        /// </summary>
        public Uri StartingUri
        {
            get { return startingUri; }
        }

        /// <summary>
        /// Constructs the exception
        /// </summary>
        /// <param name="toolName">Name of the missing tool</param>
        /// <param name="startingUri">URI where the user can start solving the problem</param>
        public ToolMustBeInstalledManually(string toolName, Uri startingUri)
        {
            this.toolName = toolName;
            this.startingUri = startingUri;
        }

        public override string ToString()
        {
            return String.Format(
                "The tool {0} is not available and cannot be installed automatically.\n\nPlease check {1} for more information.",
                toolName, startingUri);
        }
    }
}