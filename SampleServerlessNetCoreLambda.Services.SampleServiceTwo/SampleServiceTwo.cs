using System.Threading.Tasks;

namespace SampleServerlessNetCoreLambda.Services.SampleServiceTwo
{
    public class SampleServiceTwo : ISampleServiceTwo
    {
        public async Task DoStuff(string msg)
        {
            await Task.Run(() => System.Console.WriteLine(msg));
        }
    }
}
