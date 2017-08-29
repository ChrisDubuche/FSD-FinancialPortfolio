using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSD_FinancialPortal.Startup))]
namespace FSD_FinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
