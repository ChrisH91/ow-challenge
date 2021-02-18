namespace TitleRegisters.LandRegistryApi.Dtos
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class LeasesScheduleDto
    {
        [JsonPropertyName("scheduleType")]
        public string ScheduleType { get; set; } = default!;

        [JsonPropertyName("scheduleEntry")]
        public IEnumerable<ScheduleEntryDto> ScheduleEntry { get; set; } = default!;
    }
}
