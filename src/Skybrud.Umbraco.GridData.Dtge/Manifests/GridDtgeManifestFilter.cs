using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace Skybrud.Umbraco.GridData.Dtge.Manifests {

    /// <inheritdoc />
    public class GridDtgeManifestFilter : IManifestFilter {

        /// <inheritdoc />
        public void Filter(List<PackageManifest> manifests) {
            manifests.Add(new PackageManifest {
                AllowPackageTelemetry = true,
                PackageName = GridDtgePackage.Name,
                Version = GridDtgePackage.InformationalVersion
            });
        }

    }

}