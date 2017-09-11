using Autofac;
using Sample.Core;

namespace Sample.Droid
{
    public class DroidAutofacServiceLocator : AutofacServiceLocator
    {
        public override void RegisterPlatformDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<DroidBackgroundTask>().As<IBackgroundTask>().SingleInstance();
        }
    }
}