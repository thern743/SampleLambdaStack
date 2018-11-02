using System.Threading.Tasks;

namespace SampleServerlessNetCoreLambda.Services.SampleServiceOne
{
    public interface ISampleServiceOne
    {
        Task DoStuff(string msg);
    }
}