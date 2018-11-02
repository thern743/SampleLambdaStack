using System.Threading.Tasks;

namespace SampleServerlessNetCoreLambda.Services.SampleServiceOne
{
    public class SampleServiceOne : ISampleServiceOne
    {
        public async Task DoStuff(string msg)
        {
            await Task.Run(() => System.Console.WriteLine(msg));
        }
    }
}
