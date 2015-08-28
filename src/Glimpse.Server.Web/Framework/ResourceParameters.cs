using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glimpse.Server.Web
{
    public class ResourceParameters : List<ResourceParameter>
    {
        public ResourceParameters()
        {
        }

        public ResourceParameters(params ResourceParameter[] resourceParameters)
        {
            foreach (var parameter in resourceParameters)
            {
                base.Add(parameter);
            }
        }

        public new void Add(ResourceParameter resourceParameter)
        {
            base.Add(resourceParameter);
        }

        public string GenerateUriTemplate()
        {
            var sb = new StringBuilder();

            var requiredParams = this.Where(p => p.IsRequired).ToArray();
            if (requiredParams.Length > 0)
            {
                sb.AppendFormat(@"?{0}={{{0}}}", requiredParams[0].Name);
                for (int i = 1; i < requiredParams.Length; i++)
                {
                    sb.AppendFormat(@"&{0}={{{0}}}", requiredParams[i].Name);
                }
            }

            var optionalParams = this.Where(p => !p.IsRequired).Select(p => p.Name).ToArray();
            if (optionalParams.Length > 0)
            {
                sb.Append(string.Format("{{{1}{0}}}", string.Join(",", optionalParams), requiredParams.Length > 0 ? "&" : "?"));
            }

            return sb.ToString();
        }
    }
}