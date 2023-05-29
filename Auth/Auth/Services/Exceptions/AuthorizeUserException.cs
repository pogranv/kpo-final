namespace Auth.Services.Exceptions;

[Serializable]
public class AuthorizeUserException : Exception
{
    public AuthorizeUserException() { }
    public AuthorizeUserException(string message) : base(message) { }
    public AuthorizeUserException(string message, Exception inner) : base(message, inner) { }
    protected AuthorizeUserException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}