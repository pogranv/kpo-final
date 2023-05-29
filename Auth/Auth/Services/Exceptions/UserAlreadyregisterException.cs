namespace Auth.Services.Exceptions;

[Serializable]
public class UserAlreadyregisterException : Exception
{
    public UserAlreadyregisterException() { }
    public UserAlreadyregisterException(string message) : base(message) { }
    public UserAlreadyregisterException(string message, Exception inner) : base(message, inner) { }
    protected UserAlreadyregisterException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
