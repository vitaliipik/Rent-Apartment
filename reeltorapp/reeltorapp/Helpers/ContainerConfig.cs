using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using reeltorapp.Services;
using reeltorapp.ViewModels;
using reeltorapp.Views;

namespace reeltorapp.Helpers
{
    public class ContainerConfig:Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
           base.Load(containerBuilder);
           containerBuilder.RegisterType<HouseService>().As<IHouseService>();
           containerBuilder.RegisterType<AddMyHouseDetailsPage>();
           containerBuilder.RegisterType<MyHouseDetailsPage>();
        }
    }
}