using Lambda.Common.Interfaces;

namespace Lambda.Common
{
    public class Context : IContext
    {
        public string RequestId { get; set; }
    }
}
