using Skybrud.Umbraco.GridData.Dtge.Converters;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.Umbraco.GridData.Dtge.Composers {

    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    internal class DtgeGridComposer : IUserComposer {
        
        public void Compose(Composition composition) {
            
            GridContext.Current.Converters.Add(new DtgeGridConverter());

        }

    }

}