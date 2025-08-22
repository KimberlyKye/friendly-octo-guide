namespace Common.Domain.ValueObjects.Base;

/// <summary>
/// Общий класс для терминов нашей экспертной области.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ValueObject<T> : IValueObject
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

    // equals - метод сравнения 
    // toString - метод преобразования в строку

}
