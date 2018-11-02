namespace Lambda.Common.Interfaces
{
    public interface ISqsEvent<T>
    {
        T[] Records { get; set; }
    }
}
