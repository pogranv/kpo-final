namespace Rest.Services.Exceptions;

[Serializable]
public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException() { }
    public UserAlreadyExistException(string message) : base(message) { }
    public UserAlreadyExistException(string message, Exception inner) : base(message, inner) { }
    protected UserAlreadyExistException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
