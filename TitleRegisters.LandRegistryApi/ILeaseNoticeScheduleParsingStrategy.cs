namespace TitleRegisters.LandRegistryApi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TitleRegisters.LandRegistryApi.Dtos;

    /// <summary>
    /// Abstraction that allows for easily switching out different methods of serializing the input from the Land Registry API.
    /// </summary>
    public interface ILeaseNoticeScheduleParsingStrategy
    {
        Task<IEnumerable<LeaseNoticeSchedule>> ParseAsync(IEnumerable<LeasesScheduleNoticeDto> leaseScheduleNoticeDto);
    }
}
