namespace Rest.Services.Exceptions;

[Serializable]
public class DishNotFoundException : Exception
{
    public DishNotFoundException() { }
    public DishNotFoundException(string message) : base(message) { }
    public DishNotFoundException(string message, Exception inner) : base(message, inner) { }
    protected DishNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}