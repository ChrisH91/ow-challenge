namespace TitleRegisters.LandRegistryApi.Dtos
{
    using System.Text.Json.Serialization;

    public class LeasesScheduleNoticeDto
    {
        [JsonPropertyName("leaseschedule")]
        public LeasesScheduleDto LeasesSchedule { get; set; } = default!;
    }
}
