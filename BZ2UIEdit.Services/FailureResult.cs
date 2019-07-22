using System;
using System.Collections.Generic;
using System.Text;

namespace BZ2UIEdit.Services
{
    public class FailureResult : IResult
    {
        public FailureResult(string reason, Exception exception = null)
        {
            Reason = reason;
            Exception = exception;
        }

        public FailureResult(Exception exception)
        {
            Reason = exception.Message;
            Exception = exception;
        }

        public State State => State.Failure;

        public string Reason { get; }

        public Exception Exception { get; }
    }
}
