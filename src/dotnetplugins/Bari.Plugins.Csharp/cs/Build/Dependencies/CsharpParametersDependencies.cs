﻿using System.Collections.Generic;
using Bari.Core.Build;
using Bari.Core.Build.Dependencies;
using Bari.Core.Model;

namespace Bari.Plugins.Csharp.Build.Dependencies
{
    public class CsharpParametersDependencies: ProjectParametersDependencies
    {
        CsharpParametersDependencies(Project project) : base(project, "csharp")
        {            
        }

        public static void Add(Project project, ICollection<IDependencies> target)
        {
            if (project.HasParameters("csharp"))
                target.Add(new CsharpParametersDependencies(project));
        }
    }
}