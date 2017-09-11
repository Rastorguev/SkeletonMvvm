using Autofac;
using Sample.Core;

namespace Sample.iOS
{
    public class iOSAutofacServiceLocator : AutofacServiceLocator
    {
        public override void RegisterPlatformDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<iOSBackgroundTask>().As<IBackgroundTask>().SingleInstance();
        }
    }
}