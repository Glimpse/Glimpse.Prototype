using Glimpse;

namespace Glimpse.Internal
{
    public class PromoteToUserIdAttribute : PromoteToAttribute
    {
        public PromoteToUserIdAttribute() : base("request-userId")
        {
        }
    }
}
