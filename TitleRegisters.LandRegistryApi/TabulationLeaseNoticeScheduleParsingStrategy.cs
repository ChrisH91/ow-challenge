namespace TitleRegisters.LandRegistryApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TitleRegisters.LandRegistryApi.Dtos;

    /// <summary>
    /// Strategy for parsing schedule of notices of leases document API responses from the Land Registry API and returning them as
    /// <see cref="LeaseNoticeSchedule"/>.
    /// </summary>
    /// <remarks>
    /// This strategy for the most part uses the structure of the data returned from the API to deliminate columns in the tabular JSON returned and uses that
    /// to transform the text into a structured format. The exception to this is notes, which are identified by their content.
    ///
    /// This strategy works by first normalizing each entry that is not identified as a note so that it matches the length of the first entry. This is done by
    /// padding either the left or right of the string with space characters using the following logic. If the string ends in whitespace pad the left, if it
    /// does not pad the right.
    ///
    /// There are a few key assumptions made about the structure of the data:
    ///
    ///  * The first entry in the list of text entries is a row which contains data in all the columns in the table.
    ///  * All notes can be idenitified by the 4 characters "NOTE" being at the start of the text entry.
    ///  * All fully padded entries will end with a sequence of spaces.
    /// </remarks>
    public class TabulationLeaseNoticeScheduleParsingStrategy : ILeaseNoticeScheduleParsingStrategy
    {
        public async Task<IEnumerable<LeaseNoticeSchedule>> ParseAsync(IEnumerable<LeasesScheduleNoticeDto> leaseScheduleNoticeDto) => await Task.FromResult(
            leaseScheduleNoticeDto.Select(leaseScheduleNoticeDtoItem => new LeaseNoticeSchedule()
            {
                Schedule = leaseScheduleNoticeDtoItem.LeasesSchedule.ScheduleEntry.Select(j => this.ParseSingleScheduleEntry(j)),
            }));

        private LeaseNotice ParseSingleScheduleEntry(ScheduleEntryDto scheduleEntryDto)
        {
            // Parse the first entry in the list of entries to determine the start index of each of its columns. Once an entry is fully padded the start
            // index of each columns will be the same across all rows.
            var topRow = scheduleEntryDto.EntryText.First();
            var columnLength = topRow.Length;
            var columnStartIndices = new List<int>() { 0 };

            for (var i = 2; i < topRow.Length - 2; ++i)
            {
                if (topRow[i - 2] == ' ' && topRow[i - 1] == ' ' && topRow[i] != ' ')
                {
                    columnStartIndices.Add(i);
                }
            }

            var notes = new List<string>();
            var reorderedTable = new List<string>(columnLength);

            for (var i = 0; i < columnStartIndices.Count; i += 1)
            {
               reorderedTable.Add(string.Empty);
            }

            foreach (var textRow in scheduleEntryDto.EntryText)
            {
                if (string.IsNullOrEmpty(textRow))
                {
                    continue;
                }

                if (this.IsNote(textRow))
                {
                    notes.Add(textRow);
                    continue;
                }

                var normalizedTextRow = textRow.Last() == ' '
                    ? textRow.PadLeft(columnLength)
                    : textRow.PadRight(columnLength);

                for (var i = 0; i < columnStartIndices.Count; i += 1)
                {
                    reorderedTable[i] = i == columnStartIndices.Count - 1
                        ? reorderedTable[i].TrimAndAppendWithSpace(normalizedTextRow.Substring(columnStartIndices[i]))
                        : reorderedTable[i].TrimAndAppendWithSpace(
                            normalizedTextRow.Substring(
                                columnStartIndices[i],
                                columnStartIndices[i + 1] - columnStartIndices[i]));
                }
            }

            return new LeaseNotice()
            {
                RegistrationDateAndPlanRef = reorderedTable.ElementAtOrDefault(0),
                PropertyDescription = reorderedTable.ElementAtOrDefault(1),
                DateOfLeaseAndTerm = reorderedTable.ElementAtOrDefault(2),
                LesseesTitle = reorderedTable.ElementAtOrDefault(3),
                Notes = notes,
            };
        }

        private bool IsNote(string row) => row.StartsWith("NOTE");
    }
}
