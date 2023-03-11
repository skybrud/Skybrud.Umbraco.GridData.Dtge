using System;
using System.Diagnostics;
using Umbraco.Cms.Core.Semver;

namespace Skybrud.Umbraco.GridData.Dtge {

    /// <summary>
    /// Static class with various information and constants about the package.
    /// </summary>
    public class GridDtgePackage {

        /// <summary>
        /// Gets the alias of the package.
        /// </summary>
        public const string Alias = "Skybrud.Umbraco.GridData";

        /// <summary>
        /// Gets the friendly name of the package.
        /// </summary>
        public const string Name = "Limbo Grid Data DTGE";

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public static readonly Version Version = typeof(GridDtgePackage).Assembly.GetName().Version!;

        /// <summary>
        /// Gets the informational version of the package.
        /// </summary>
        public static readonly string InformationalVersion = FileVersionInfo.GetVersionInfo(typeof(GridDtgePackage).Assembly.Location).ProductVersion!;

        /// <summary>
        /// Gets the semantic version of the package.
        /// </summary>
        public static readonly SemVersion SemVersion = InformationalVersion;

        /// <summary>
        /// Gets the URL of the GitHub repository for this package.
        /// </summary>
        public const string GitHubUrl = "https://github.com/skybrud/Skybrud.Umbraco.GridData.Dtge";

        /// <summary>
        /// Gets the URL of the issue tracker for this package.
        /// </summary>
        public const string IssuesUrl = "https://github.com/skybrud/Skybrud.Umbraco.GridData.Dtge/issues";

        /// <summary>
        /// Gets the website URL of the package.
        /// </summary>
        public const string WebsiteUrl = "https://packages.skybrud.dk/skybrud.umbraco.griddata.dtge/v4/";

        /// <summary>
        /// Gets the URL of the documentation for this package.
        /// </summary>
        public const string DocumentationUrl = "https://packages.skybrud.dk/skybrud.umbraco.griddata.dtge/v4/docs/";

    }

}