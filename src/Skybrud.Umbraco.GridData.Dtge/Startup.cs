using Umbraco.Core;

namespace Skybrud.Umbraco.GridData.Dtge {

    class Startup : ApplicationEventHandler {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            GridContext.Current.Converters.Add(new DtgeGridConverter());
        }
    
    }

}