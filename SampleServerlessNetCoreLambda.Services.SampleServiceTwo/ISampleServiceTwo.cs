using System.Threading.Tasks;

namespace SampleServerlessNetCoreLambda.Services.SampleServiceTwo
{
    public interface ISampleServiceTwo
    {
        Task DoStuff(string msg);
    }
}