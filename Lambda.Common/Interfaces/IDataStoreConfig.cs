namespace Lambda.Common.Interfaces
{
    public interface IDataStoreConfig : IConfig
    {
        string TableName { get; set; }
    }
}