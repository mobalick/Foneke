using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FoNeke.Web.Castle.Installers
{

    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("EMM.FoNeke.Common")
                                      .Where(type => type.Name.EndsWith("Repository"))
                                      .WithServiceAllInterfaces()
                                      .Configure(c => c.LifestyleTransient()));

            container.Register(Classes.FromAssemblyNamed("FoNeke.Web.Repositories")
                                      .Where(type => type.Name.EndsWith("Repository"))
                                      .WithServiceAllInterfaces()
                                      .Configure(c => c.LifestyleTransient()));


        }
    }

    public class BuildersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("FoNeke.Web.Controllers")
                                      .Where(type => type.Name.EndsWith("Builder"))
                                      .WithServiceAllInterfaces()
                                      .Configure(c => c.LifestyleTransient()));

            container.Register(Classes.FromAssemblyNamed("FoNeke.Web.Controllers")
                                      .Where(type => type.Name.EndsWith("ViewModel"))
                                      .WithServiceAllInterfaces()
                                      .Configure(c => c.LifestyleTransient()));


            //container.Register(Classes.FromAssemblyNamed("Cit.Common")
            //                          .Where(type => type.Name.EndsWith("Builder"))
            //                          .WithServiceAllInterfaces()
            //                          .Configure(c => c.LifestyleTransient()));

            //container.Register(Classes.FromAssemblyNamed("Cit.Common")
            //                      .Where(type => type.Name.EndsWith("ViewModel"))
            //                      .WithServiceAllInterfaces()
            //                      .Configure(c => c.LifestyleTransient()));

            
        }

    }


    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("FoNeke.Services")
                                      .Where(type => type.Name.EndsWith("Service"))
                                      .WithServiceAllInterfaces()
                                      .Configure(c => c.LifestyleTransient()));

        }

    }


    public class ControllersInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("FoNeke.Web.Controllers")
                                .BasedOn<IController>()
                                .LifestyleTransient());
        }
    }

   
}

