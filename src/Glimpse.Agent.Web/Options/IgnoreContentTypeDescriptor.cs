namespace Glimpse.Agent.Web.Options
{
    public class IgnoreContentTypeDescriptor
    {
        public IgnoreContentTypeDescriptor(string contentType)
        {
            ContentType = contentType;
        }

        public string ContentType { get; }
    }
}