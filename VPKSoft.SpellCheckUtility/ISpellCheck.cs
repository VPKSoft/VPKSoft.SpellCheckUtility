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

using System;
using System.Collections.Generic;
using VPKSoft.SpellCheckUtility.UtilityClasses;

namespace VPKSoft.SpellCheckUtility
{
    /// <summary>
    /// Interface for the spell checking to abstract things a little.
    /// </summary>
    public interface ISpellCheck
    {
        /// <summary>
        /// Runs the spell checking for a miss-spelled word.
        /// </summary>
        /// <param name="spellingError">The current spelling error.</param>
        /// <param name="suggestions">The suggestions for the miss-spelled word.</param>
        /// <param name="location">The location where the spell checking is going at.</param>
        /// <param name="count">The count of total spell checks.</param>
        /// <returns><c>true</c> if the spell check loop should continue, <c>false</c> otherwise.</returns>
        bool RunSpellCheck(SpellingError spellingError, List<string> suggestions, int location, int count);

        /// <summary>
        /// Gets or sets the action to add word to the user ignore word list.
        /// </summary>
        /// <value>The action to add word to the user ignore word list.</value>
        Action<string> AddIgnoreWordAction { get; set; }

        /// <summary>
        /// Gets or sets the action to add a word to the user dictionary.
        /// </summary>
        /// <value>The action to add a word to the user dictionary.</value>
        Action<string> AddUserDictionaryWord { get; set; }

        /// <summary>
        /// Gets or sets the Func to request suggestions for a word.
        /// </summary>
        /// <value>The the Func to request suggestions for a word.</value>
        Func<string, List<string>> RequestWordSuggestion { get; set; }

        /// <summary>
        /// Gets or sets the function to detect whether the given word can be added to the user dictionary.
        /// </summary>
        /// <value>The function to detect whether the given word can be added to the user dictionary.</value>
        Func<string, bool> CanAddToUserDictionary { get; set; }

        /// <summary>
        /// Gets or sets the function to detect whether the given word can be added to the user ignore list.
        /// </summary>
        /// <value>The function to detect whether the given word can be added to the user ignore list.</value>
        Func<string, bool> CanAddToUserIgnoreList { get; set; }

        /// <summary>
        /// Gets or sets the current spelling error to handle.
        /// </summary>
        /// <value>The spelling error to handle.</value>
        SpellingError SpellingError { get; set; }
    }
}
