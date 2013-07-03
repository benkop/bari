﻿using System.Collections.Generic;
using System.Threading;
using System.Xml;
using Bari.Core.Model;

namespace Bari.Plugins.VCpp.Model
{
    /// <summary>
    /// Parameters for the linker (Link MSBuild task)
    /// </summary>
    public class VCppProjectLinkerParameters : IProjectParameters
    {
        public string[] AdditionalDependencies { get; set; }
        public string[] AdditionalLibraryDirectories { get; set; }
        public string[] AdditionalManifestDependencies { get; set; }
        public string[] AdditionalOptions { get; set; }
        public string[] AddModuleNamesToAssembly { get; set; }
        public bool AllowIsolation { get; set; }
        public bool AssemblyDebug { get; set; }
        public string BaseAddress { get; set; }
        public CLRImageType CLRImageType { get; set; }
        public CLRSupportLastError CLRSupportLastError { get; set; }
        public ApartmentState CLRThreadAttribute { get; set; }
        public bool CLRUnmanagedCodeCheck { get; set; }
        public LinkerHotPatchingOption CreateHotPatchableImage { get; set; }
        public bool DataExecutionPrevention { get; set; }
        public string[] DelayLoadDLLs { get; set; }
        public bool DelaySign { get; set; }
        public LinkerDriverOption Driver { get; set; }
        public bool EnableCOMDATFolding { get; set; }
        public bool EnableUAC { get; set; }
        public string EntryPointSymbol { get; set; }
        public bool FixedBaseAddress { get; set; }
        public LinkerForceOption ForceFileOutput { get; set; }
        public string[] ForceSymbolReferences { get; set; }
        public string FunctionOrder { get; set; }
        public bool GenerateDebugInformation { get; set; }
        public bool GenerateManifest { get; set; }
        public bool GenerateMapFile { get; set; }
        public int? HeapCommitSize { get; set; }
        public int? HeapReserveSize { get; set; }
        public bool IgnoreAllDefaultLibraries { get; set; }
        public bool IgnoreEmbeddedIDL { get; set; }
        public bool IgnoreImprotLibrary { get; set; }
        public string[] IgnoreSpecificDefaultLibraries { get; set; }
        public bool ImageHasSafeExceptionHandlers { get; set; }
        public string ImportLibrary { get; set; }
        public string KeyContainer { get; set; }
        public string KeyFile { get; set; }
        public bool LargeAddressAware { get; set; }
        public bool LinkDLL { get; set; }
        public bool LinkIncremental { get; set; }
        public bool MapExports { get; set; }
        public string MapFileName { get; set; }
        public string MergedIDLBaseFileName { get; set; }
        public IDictionary<string, string> MergeSections { get; set; }
        public int? MinimumRequiredVersion { get; set; }
        public bool NoEntryPoint { get; set; }
        public bool OptimizeReferences { get; set; }
        public bool PreventDllBinding { get; set; }
        public bool RandomizedBaseAddress { get; set; }
        public int? SectionAlignment { get; set; }
        public bool SetChecksum { get; set; }
        public int? StackCommitSize { get; set; }
        public int? StackReserveSize { get; set; }
        public LinkerSubSystemOption SubSystem { get; set; }
        public bool SupportNobindOfDelayLoadedDLL { get; set; }
        public bool SupportUnloadOfDelayLoadedDLL { get; set; }
        public bool SwapRunFromCD { get; set; }
        public bool SwapRunFromNet { get; set; }
        public LinkerTargetMachine TargetMachine { get; set; }
        public bool TerminalServerAware { get; set; }
        public bool TreatLinkerWarningAsErrors { get; set; }
        public bool TurnOffAssemblyGeneration { get; set; }
        public int? TypeLibraryResourceID { get; set; }
        public UACExecutionLevel UACExecutionLevel { get; set; }
        public bool UACUIAccess { get; set; }

        public VCppProjectLinkerParameters(Suite suite)
        {
        }

        public void FillProjectSpecificMissingInfo(Project project)
        {
            LinkDLL = project.Type == ProjectType.Library;
        }

        public void ToVcxprojProperties(XmlWriter writer)
        {
        }
    }
}