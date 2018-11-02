namespace Lambda.Common.Interfaces
{
    public interface IQueueConfig : IConfig
    {
        string Url { get; set; }
    }
}