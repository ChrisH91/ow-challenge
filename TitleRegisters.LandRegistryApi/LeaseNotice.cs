namespace TitleRegisters.LandRegistryApi
{
    using System.Collections.Generic;

    public class LeaseNotice
    {
        public int EntryNumber { get; set; }

        public string RegistrationDateAndPlanRef { get; set; } = string.Empty;

        public string PropertyDescription { get; set; } = string.Empty;

        public string DateOfLeaseAndTerm { get; set; } = string.Empty;

        public string LesseesTitle { get; set; } = string.Empty;

        public ICollection<string> Notes { get; set; } = new List<string>();
    }
}
