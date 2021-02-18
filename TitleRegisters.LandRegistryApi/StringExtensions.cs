namespace TitleRegisters.LandRegistryApi
{
    public static class StringExtensions
    {
        /// <summary>
        /// Helper string extension method to conditionally append to a string, this is used to concatenate string based tables which use whitespace to
        /// deliminate the columns.
        /// </summary>
        /// <remarks>
        /// If the original string being appended to is empty then a trimmed version of the input string to be appended will be returned, if not the trimmed
        /// version of the the input string will be appended to the original string separated by a single space.
        ///
        /// The string will be appended only if the string to be appended is not null or whitespace
        /// </remarks>
        /// <param name="original">The original string to append to.</param>
        /// <param name="toAppend">The string to append to the original.</param>
        /// <returns>The appended string.</returns>
        public static string TrimAndAppendWithSpace(this string original, string toAppend)
        {
            var trimmed = toAppend.Trim();

            if (string.IsNullOrEmpty(trimmed))
            {
                return original;
            }

            return original == string.Empty ? trimmed : $"{original} {trimmed}";
        }
    }
}
