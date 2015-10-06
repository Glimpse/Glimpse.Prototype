using Glimpse;

namespace Glimpse.Internal
{
    public class PromoteToUrlAttribute : PromoteToAttribute
    {
        public PromoteToUrlAttribute() : base("request-url")
        {
        }
    }
}
