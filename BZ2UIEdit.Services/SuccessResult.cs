using System;
using System.Collections.Generic;
using System.Text;

namespace BZ2UIEdit.Services
{
    public class SuccessResult : IResult
    {
        public State State => State.Success;

        public string Reason { get; }

        public Exception Exception => null;
        
        public SuccessResult(string reason = "Operation Successful")
        {
            Reason = reason;
        }
    }
}
