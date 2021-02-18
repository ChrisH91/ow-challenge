namespace TitleRegisters.LandRegistryApi
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Threading.Tasks;
    using TitleRegisters.LandRegistryApi.Dtos;

    /// <summary>
    /// Class for parsing a lease notice schedule from Land Registry API input.
    /// </summary>
    /// <remarks>
    /// This class takes its input in the form of a stream to allow for various different input sources (HTTP response messages, files etc). The stream must be
    /// an UTF-8 encoded JSON string.
    /// </remarks>
    public class LeaseNoticeScheduleParser
    {
        private readonly Stream input;

        public LeaseNoticeScheduleParser(Stream input) => this.input = input;

        public ILeaseNoticeScheduleParsingStrategy Strategy { get; set; } = new TabulationLeaseNoticeScheduleParsingStrategy();

        public async Task<IEnumerable<LeaseNoticeSchedule>> ParseAsync()
        {
            var content = await JsonSerializer.DeserializeAsync<IEnumerable<LeasesScheduleNoticeDto>>(this.input);

            if (content == null)
            {
                throw new SerializationException("Unable to deserialize input");
            }

            return await this.Strategy.ParseAsync(content);
        }
    }
}
