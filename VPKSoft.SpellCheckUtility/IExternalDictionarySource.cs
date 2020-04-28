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

using System.Collections.Generic;

namespace VPKSoft.SpellCheckUtility
{
    /// <summary>
    /// An interface to provide an external dictionary to the library.
    /// </summary>
    public interface IExternalDictionarySource
    {
        /// <summary>
        /// Gets spelling correction suggestions for a specified miss-spelled word.
        /// </summary>
        /// <param name="word">The miss-spelled word to get correction suggestions for.</param>
        /// <returns>A IEnumerable&lt;System.String&gt; of correction suggestions for the word.</returns>
        IEnumerable<string> Suggest(string word);

        /// <summary>
        /// Checks whether the specified word is spelled correctly.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns><c>true</c> if the word is spelled correctly, <c>false</c> otherwise.</returns>
        bool Check(string word);
    }
}
