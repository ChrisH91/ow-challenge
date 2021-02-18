namespace TitleRegisters
{
    using System.Diagnostics;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using TitleRegisters.LandRegistryApi;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var input = File.OpenRead(args[1]);
            var parser = new LeaseNoticeScheduleParser(input);
            var result = await parser.ParseAsync();

            var output = JsonSerializer.Serialize(result, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });

            await File.WriteAllTextAsync(args[2], output);
        }
    }
}
