using Glimpse.Web;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent.Web
{
    public class MasterRequestProfiler : IRequestRuntime
    {
        private readonly IDiscoverableCollection<IRequestProfiler> _requestProfiliers;

        public MasterRequestProfiler(IDiscoverableCollection<IRequestProfiler> requestProfiliers)
        {
            _requestProfiliers = requestProfiliers;
            _requestProfiliers.Discover();
        }

        public async Task Begin(IHttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    await requestRuntime.Begin(context);
                }
            }
        }

        public async Task End(IHttpContext context)
        {
            if (ShouldProfile(context))
            {
                foreach (var requestRuntime in _requestProfiliers)
                {
                    await requestRuntime.End(context);
                }
            }
        }
        
        public bool ShouldProfile(IHttpContext context)
        {
            // TODO: confirm that we want on by default. I'm
            //       thinking yes since they can easily exclude
            //       Glimpse middleware if they want.

            // TODO: Propbably should be hardcoded to ignore
            //       own requests here.

            // TODO: Hack for favicon atm. Can't be here

            if (context.Request.Path.IndexOf("/Glimpse", StringComparison.InvariantCultureIgnoreCase) > -1
                || context.Request.Path.IndexOf("favicon.ico", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                return false;
            }
            else if (context.Settings.ShouldProfile != null)
            {
                return context.Settings.ShouldProfile(context);
            }

            return true;
        }
    }
}