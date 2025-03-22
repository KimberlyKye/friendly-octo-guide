namespace Domain.Defaults;

public abstract class ValueObject<T> where T : ValueObject<T>
{
    private T _value;

    public T Value
    {
        get { return _value; }
        protected set { _value = value; }
    }
    protected ValueObject(T value)
    {
        Value = value;
    }

    public T GetValue()
    {
        return Value;
    }
}
