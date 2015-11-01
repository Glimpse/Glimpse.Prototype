using System;

namespace Glimpse.Server.Resources
{
    public class ExceptionProblem : Problem
    {
        private readonly Exception _exception;
        public ExceptionProblem(Exception exception)
        {
            _exception = exception;
            Extensions["StackTrace"] = _exception.StackTrace;
        }

        public override Uri Type => new Uri("http://getglimpse.com/Docs/Troubleshooting/Exception");
        public override string Title => "Server Exception";
        public override string Details => _exception.Message;
        public override int StatusCode => 500;
    }
}
