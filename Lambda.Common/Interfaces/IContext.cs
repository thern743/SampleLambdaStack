using Amazon.Lambda.Core;

namespace Lambda.Common.Interfaces
{
    public interface IContext
    {
        string RequestId { get; set; }
    }
}
