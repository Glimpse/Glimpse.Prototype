using Glimpse;

namespace Glimpse.Internal
{
    public class PromoteToMethodAttribute : PromoteToAttribute
    {
        public PromoteToMethodAttribute() : base("request-method")
        {
        }
    }
}
