public class ReactData<T> where T : struct
{
    private T data = default;
    public delegate void DataEventHandler(T oldData, T newData);
    public event DataEventHandler OnValueChanged;

    public ReactData (T data) => this.data = data;

    public T Value
    {
        get => data;
        set
        {
            var temp = data;
            data = value;
            OnValueChanged?.Invoke(temp, data);
        }
    }
}