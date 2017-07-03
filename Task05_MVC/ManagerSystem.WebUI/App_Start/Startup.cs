using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Services;
using ManagerSystem.DAL.Interfaces;
using ManagerSystem.DAL.Repositories;
using ManagerSystem.WebUI.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(ManagerSystem.WebUI.Startup))]
namespace ManagerSystem.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<UserService>()
                .WithConstructorArgument(ConstantStorage.ConnectionString);

            return kernel.TryGet<IUserService>();
        }
    }
}