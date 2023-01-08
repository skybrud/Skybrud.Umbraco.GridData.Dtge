using Skybrud.Umbraco.GridData.Composers;
using Skybrud.Umbraco.GridData.Dtge.Converters;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Skybrud.Umbraco.GridData.Dtge.Composers
{

    internal class DtgeGridComposer : IComposer
    {

        public void Compose(IUmbracoBuilder builder)
        {

            builder.GridConverters().Append<DtgeGridConverter>();
        }

    }

}