using System;

namespace SlotEase.Application.Exceptions;

[Serializable]
public class UserFriendlyException : Exception
{
    /// <summary>
    /// Additional information about the exception.
    /// </summary>
    public string Details { get; private set; }

    public UserFriendlyException(string message) : base(message)
    {

    }

    public UserFriendlyException(string message, Exception inner)
   : base(message, inner) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="details">Additional information about the exception</param>
    public UserFriendlyException(string message, string details)
        : this(message)
    {
        Details = details;
    }
}
