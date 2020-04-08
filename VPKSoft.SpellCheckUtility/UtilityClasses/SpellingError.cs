#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

namespace VPKSoft.SpellCheckUtility.UtilityClasses
{
    /// <summary>
    /// A class to hold locations for spelling errors.
    /// Implements the <see cref="VPKSoft.SpellCheckUtility.UtilityClasses.ISpellingResult" />
    /// </summary>
    /// <seealso cref="VPKSoft.SpellCheckUtility.UtilityClasses.ISpellingResult" />
    public class SpellingError: ISpellingResult
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new instance with the same data as the instance which was cloned.</returns>
        public SpellingError Clone()
        {
            return new SpellingError
            {
                StartLocation = StartLocation,
                ReplaceAll = ReplaceAll,
                CorrectedWord = CorrectedWord,
                EndLocation = EndLocation,
                Ignore = Ignore,
                IgnoreAll = IgnoreAll,
                Word = Word,
            };
        }

        /// <summary>
        /// Gets or sets the start location of the spelling error.
        /// </summary>
        public int StartLocation { get; set; }

        /// <summary>
        /// Gets or sets the end location of the spelling error.
        /// </summary>
        public int EndLocation { get; set; }

        /// <summary>
        /// Gets or sets the miss-spelled word.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the corrected word for the miss-spelled word.
        /// </summary>
        public string CorrectedWord { get; set; }

        /// <summary>
        /// Gets the length of the spelling error.
        /// </summary>
        /// <value>The length of the spelling error.</value>
        public int Length => EndLocation - StartLocation;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SpellingError"/> is corrected.
        /// </summary>
        public bool Corrected => !string.IsNullOrWhiteSpace(CorrectedWord);

        /// <summary>
        /// Gets or sets a value indicating whether all occurrences of the <see cref="Word"/> should be replaced with the <see cref="CorrectedWord"/> property value.
        /// </summary>
        public bool ReplaceAll { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all occurrences of the <see cref="Word"/> should be ignored further in the spell checking process.
        /// </summary>
        public bool IgnoreAll { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this occurrence of the <see cref="Word"/> should be ignored.
        /// </summary>
        public bool Ignore { get; set; }
    }
}
