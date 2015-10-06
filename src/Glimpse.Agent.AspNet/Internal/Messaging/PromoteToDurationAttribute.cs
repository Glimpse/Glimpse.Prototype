using Glimpse;

namespace Glimpse.Internal
{
    public class PromoteToDurationAttribute : PromoteToAttribute
    {
        public PromoteToDurationAttribute() : base("request-duration")
        {
        }
    }
}
