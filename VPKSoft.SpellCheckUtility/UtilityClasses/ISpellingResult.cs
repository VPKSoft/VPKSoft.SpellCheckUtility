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
    /// An interface for the <see cref="SpellingError"/> and for the <see cref="SpellingCorrection"/> classes.
    /// </summary>
    internal interface ISpellingResult
    {
        /// <summary>
        /// Gets or sets the start location of the spelling error.
        /// </summary>
        /// <value>The start location of the spelling error.</value>
        int StartLocation { get; set; }

        /// <summary>
        /// Gets or sets the end location of the spelling error.
        /// </summary>
        /// <value>The end location of the spelling error.</value>
        int EndLocation { get; set; }

        /// <summary>
        /// Gets or sets the miss-spelled word.
        /// </summary>
        /// <value>The miss-spelled word.</value>
        string Word { get; set; }

        /// <summary>
        /// Gets or sets the corrected word for the miss-spelled word.
        /// </summary>
        /// <value>The corrected word for the miss-spelled word.</value>
        string CorrectedWord { get; set; }

        /// <summary>
        /// Gets the length of the spelling error.
        /// </summary>
        /// <value>The length of the spelling error.</value>
        int Length { get; }

        /// <summary>
        /// Gets or sets a value indicating whether all occurrences of the <see cref="Word"/> should be replaced with the <see cref="CorrectedWord"/> property value.
        /// </summary>
        /// <value><c>true</c> if all occurrences of the <see cref="Word"/> should be replace; otherwise, <c>false</c>.</value>
        bool ReplaceAll { get; set; }
    }
}
