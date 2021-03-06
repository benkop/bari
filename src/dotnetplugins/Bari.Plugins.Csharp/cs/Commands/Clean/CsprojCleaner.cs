﻿using System;
using System.Linq;
using Bari.Core.Commands.Clean;
using Bari.Core.Model;

namespace Bari.Plugins.Csharp.Commands.Clean
{
    /// <summary>
    /// .csproj files must be generated outside the target directory, into the project source tree otherwise
    /// visual studio does not show the hierarchy only a flat set of file references. This class performs the
    /// additional cleaning step to remove these generated project files.
    /// </summary>
    public class CsprojCleaner : ICleanExtension
    {
        private readonly Suite suite;

        /// <summary>
        /// Constructs the cleaner
        /// </summary>
        /// <param name="suite">Current suite</param>
        public CsprojCleaner(Suite suite)
        {
            this.suite = suite;
        }

        /// <summary>
        /// Performs the additional cleaning step
        /// </summary>
        /// <param name="parameters"></param>
        public void Clean(ICleanParameters parameters)
        {
            foreach (var projectRoot in from module in suite.Modules
                                        from project in module.Projects.Concat(module.TestProjects)
                                        select project.RootDirectory)
            {
                var csRoot = projectRoot.GetChildDirectory("cs");
                if (csRoot != null)
                {
                    foreach (var csproj in csRoot.Files.Where(
                        name => name.EndsWith(".csproj", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        csRoot.DeleteFile(csproj);
                    }

                    foreach (var csproj in csRoot.Files.Where(
                                name => name.EndsWith(".csproj.user", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        csRoot.DeleteFile(csproj);
                    }

                    foreach (var csversion in projectRoot.Files.Where(
                                name => name.Equals("version.cs", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        projectRoot.DeleteFile(csversion);
                    }
                }
            }
        }
    }
}