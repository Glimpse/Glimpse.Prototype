using Glimpse;

namespace Glimpse.Internal
{
    public class PromoteToContentTypeAttribute : PromoteToAttribute
    {
        public PromoteToContentTypeAttribute() : base("request-content-type")
        {
        }
    }
}
