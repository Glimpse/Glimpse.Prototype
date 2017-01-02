using System.Linq;
using System.Text;
using Glimpse.Agent.Inspectors;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using Glimpse.Agent.Messages;

namespace Glimpse.Agent.Internal.Inspectors
{
    public class BodyInsepector : IInspectorFunction
    {
        private const string RawRequestBodyKey = "__GlimpseRawRequestBody";
        private const string RawResponseBodyKey = "__GlimpseRawResponseBody";
        private const int MaxBodySize = 132000;
        private static Regex[] MineTypesToCapture = new[] {
            new Regex(@"^text\/"),
            new Regex(@"^application\/.*?xml"),
            new Regex(@"^application\/json"),
            new Regex(@"^application\/javascript")
        };
        private readonly IAgentBroker _broker;
        
        public BodyInsepector(IAgentBroker broker)
        {
            _broker = broker;
        }

        public void Configure(IInspectorFunctionBuilder builder)
        {
            builder.Use(async (httpContext, next) =>
            { 
                var request = httpContext.Request;
                var response = httpContext.Response;

                // request
                var requestContent = (string)null;
                var requestHeaders = request.GetTypedHeaders();
                var requestContentType = requestHeaders.ContentType;
                var requestShouldReadBody = ShouldReadBody(requestContentType, request.Body);

                // request setup/read - don't wrap unless we need to
                if (requestShouldReadBody && !request.Body.CanSeek)
                {
                    var newRequestBodyStream = new MemoryStream();
                    await request.Body.CopyToAsync(newRequestBodyStream);

                    httpContext.Items.Add(RawRequestBodyKey, request.Body);
                    request.Body = newRequestBodyStream;

                    request.Body.Seek(0, SeekOrigin.Begin);
                    requestContent = await new StreamReader(request.Body).ReadToEndAsync();
                    request.Body.Seek(0, SeekOrigin.Begin);
                }

                // request write 
                RequestWrite(request, requestContentType, requestContent);

                // response setup - we don't yet know the content type
                if (!response.Body.CanSeek)
                {
                    httpContext.Items.Add(RawResponseBodyKey, response.Body);
                    response.Body = new MemoryStream();
                }

                await next();

                // response
                var responseContent = (string)null;
                var responseHeaders = response.GetTypedHeaders();
                var responseContentType = responseHeaders.ContentType;
                var responseShouldReadBody = ShouldReadBody(responseContentType, response.Body);

                // response read/teardown
                if (response.Body.CanSeek)
                {
                    if (responseShouldReadBody)
                    {
                        response.Body.Seek(0, SeekOrigin.Begin);
                        responseContent = await new StreamReader(response.Body).ReadToEndAsync();
                        response.Body.Seek(0, SeekOrigin.Begin);
                    }

                    var rawResponseBodyStream = (object)null;
                    if (httpContext.Items.TryGetValue(RawResponseBodyKey, out rawResponseBodyStream))
                    {
                        await response.Body.CopyToAsync((Stream)rawResponseBodyStream);
                    }
                }

                // response write 
                ResponseWrite(response, responseContentType, responseContent);
            });
        }

        private void RequestWrite(HttpRequest request, MediaTypeHeaderValue contentType, string content)
        {
            var webBody = new WebRequestBody();
            ProcessBody(webBody, contentType, content);

            if (request.HasFormContentType)
            {
                webBody.Form = request.Form;
            }

            // TODO: need to build message
            //_broker.SendMessage(message);
        }

        private void ResponseWrite(HttpResponse response, MediaTypeHeaderValue contentType, string content)
        {
            var webBody = new WebBody();
            ProcessBody(webBody, contentType, content);

            // TODO: need to build message
            //_broker.SendMessage(message);
        }

        private void ProcessBody(WebBody webBody, MediaTypeHeaderValue contentType, string content)
        {
            webBody.Encoding = contentType?.Encoding?.WebName;
            webBody.Content = content;
            
            if (webBody.Content != null)
            {
                webBody.Size = webBody.Content.Length;

                if (webBody.Content != string.Empty
                    && webBody.Content.Length > MaxBodySize)
                {
                    webBody.Content = webBody.Content.Substring(0, MaxBodySize);
                    webBody.IsTruncated = true;
                }
            }
        }

        private bool ShouldReadBody(MediaTypeHeaderValue contentType, Stream body)
        {
            return (body != null
                    && contentType != null
                    && (contentType.Encoding == Encoding.UTF8
                        || MineTypesToCapture.Any(regex => regex.IsMatch(contentType.MediaType))));
        }
    }
}
