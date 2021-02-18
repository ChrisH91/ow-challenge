namespace TitleRegisters.LandRegistryApi
{
    using System.Collections.Generic;

    public class LeaseNoticeSchedule
    {
        public IEnumerable<LeaseNotice> Schedule { get; set; } = new List<LeaseNotice>();
    }
}
