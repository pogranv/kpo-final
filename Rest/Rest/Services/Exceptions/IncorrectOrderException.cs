namespace Rest.Services.Exceptions;

[Serializable]
public class IncorrectOrderException : Exception
{
    public IncorrectOrderException() { }
    public IncorrectOrderException(string message) : base(message) { }
    public IncorrectOrderException(string message, Exception inner) : base(message, inner) { }
    protected IncorrectOrderException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}