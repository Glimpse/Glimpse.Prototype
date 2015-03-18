namespace Glimpse.Agent.Web.Options
{
    public class IgnoredContentTypeDescriptor
    {
        public IgnoredContentTypeDescriptor(string contentType)
        {
            ContentType = contentType;
        }

        public string ContentType { get; }
    }
}