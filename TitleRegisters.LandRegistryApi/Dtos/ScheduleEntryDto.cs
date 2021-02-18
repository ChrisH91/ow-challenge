namespace TitleRegisters.LandRegistryApi.Dtos
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ScheduleEntryDto
    {
        [JsonPropertyName("entryNumber")]
        public string EntryNumber { get; set; } = default!;

        [JsonPropertyName("entryDate")]
        public string EntryDate { get; set; } = default!;

        [JsonPropertyName("entryType")]
        public string EntryType { get; set; } = default!;

        [JsonPropertyName("entryText")]
        public IEnumerable<string> EntryText { get; set; } = default!;
    }
}
