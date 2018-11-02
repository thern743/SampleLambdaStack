using Lambda.Common.Interfaces;
namespace Lambda.Common.AWS.SQS
{
    public class SqsEvent<T> : ISqsEvent<T>
    {
        public T[] Records { get; set; }
    }
}
