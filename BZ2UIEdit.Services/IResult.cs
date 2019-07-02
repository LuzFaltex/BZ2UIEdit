using System;
using System.Collections.Generic;
using System.Text;

namespace BZ2UIEdit.Services
{
    public interface IResult
    {
        State State { get; }
        string Reason { get; }
        Exception Exception { get; }
    }

    public enum State
    {
        Success,
        Failure
    }
}
