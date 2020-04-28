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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VPKSoft.SpellCheckUtility.UtilityClasses;
using WeCantSpell.Hunspell;

namespace VPKSoft.SpellCheckUtility
{
    /// <summary>
    /// A spell checking class using the <see cref="WeCantSpell.Hunspell"/> for custom spell checking via an <see cref="ISpellCheck"/> interface.
    /// </summary>
    public class SpellChecker
    {
        /// <summary>
        /// Gets or sets the dictionary that the <see cref="WeCantSpell.Hunspell"/> class library has created.
        /// </summary>
        private static WordList Dictionary { get; set; }

        /// <summary>
        /// Gets or sets the optional <see cref="IExternalDictionarySource"/> external dictionary interface to replace the Hunspell dictionary.
        /// </summary>
        public static IExternalDictionarySource ExternalDictionary { get; set; }

        /// <summary>
        /// Gets or sets the user dictionary that the <see cref="WeCantSpell.Hunspell"/> class library has created.
        /// </summary>
        private static WordList UserDictionary { get; set; }

        /// <summary>
        /// Gets or sets the list containing the user dictionary words.
        /// </summary>
        private static List<string> UserDictionaryWords { get; set; } = new List<string>();

        /// <summary>
        /// A list of ignored words.
        /// </summary>
        private static List<string> IgnoreList { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the word boundary regex used with the <see cref="SpellCheckFast"/> method.
        /// </summary>
        public Regex WordBoundaryRegex { get; set; } = new Regex( "\\b\\w+\\b", RegexOptions.Compiled);

        /// <summary>
        /// Gets word suggestions from either the <see cref="ExternalDictionary"/> or the given Hunspell dictionary set via the <see cref="LoadDictionary"/> method.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>A IEnumerable&lt;System.String&gt; containing the suggestions.</returns>
        private IEnumerable<string> DictionarySuggest(string word)
        {
            return Dictionary == null ? ExternalDictionary?.Suggest(word) : Dictionary.Suggest(word);
        }

        /// <summary>
        /// Checks if the word is spelled correctly using either the <see cref="ExternalDictionary"/> or the given Hunspell dictionary set via the <see cref="LoadDictionary"/> method.
        /// </summary>
        /// <param name="word">The word to check for.</param>
        /// <returns><c>true</c> if the word is spelled correctly, <c>false</c> otherwise.</returns>
        private bool DictionaryCheck(string word)
        {
            return (Dictionary?.Check(word) ?? ExternalDictionary?.Check(word)) == true;
        }

        /// <summary>
        /// Gets a word single word matching regular expression in case of on the fly error correction.
        /// </summary>
        /// <param name="word">The word to create the regular expression for.</param>
        /// <returns>The generated regular expression if successful; otherwise false.</returns>
        public static Regex GetWordMatchRegex(string word)
        {
            try
            {
                return new Regex("\\b" + word + "\\b");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Adds word to user's ignore word list.
        /// </summary>
        /// <param name="word">The word to add to the user ignore word list.</param>
        public void AddIgnoreWord(string word)
        {
            if (!IgnoreList.Contains(word))
            {
                IgnoreList.Add(word);
            }
        }

        /// <summary>
        /// Adds a word to the user dictionary.
        /// </summary>
        /// <param name="word">The word to add to the user dictionary.</param>
        public void AddUserDictionaryWord(string word)
        {
            AddToUserDictionary(word);
        }

        /// <summary>
        /// Add a word to the user dictionary.
        /// </summary>
        /// <param name="word">A word to add to the user dictionary.</param>
        /// <returns>True if the word was successfully added to the user dictionary; otherwise false.</returns>
        public bool AddToUserDictionary(string word)
        {
            if (!CanAddToUserDictionary(word))
            {
                return false;
            }

            UserDictionaryWords.Add(word);
            LoadUserDictionary(UserDictionaryWords.ToArray());
            return true;
        }


        /// <summary>
        /// Determines whether the given word can be added to the user dictionary.
        /// </summary>
        /// <param name="word">The word to check for.</param>
        /// <returns> <c>true</c> if the given word can be added to the user dictionary; otherwise, <c>false</c>.</returns>
        public bool CanAddToUserDictionary(string word)
        {
            return !UserDictionaryWords.Exists(f =>
                string.Compare(f, word, StringComparison.Ordinal) == 0);
        }

        /// <summary>
        /// Determines whether the given word can be added to the user ignore word list.
        /// </summary>
        /// <param name="word">The word to check for.</param>
        /// <returns> <c>true</c> if the given word can be added to the user ignore word list; otherwise, <c>false</c>.</returns>
        public bool CanAddToUserIgnoreList(string word)
        {
            return !IgnoreList.Exists(f =>
                string.Compare(f, word, StringComparison.Ordinal) == 0);
        }

        /// <summary>
        /// Gets suggestions to correct the given word.
        /// </summary>
        /// <param name="word">To word to get correction suggestions for.</param>
        /// <returns>A list of strings containing the suggestions of an empty list if no suggestions were found.</returns>
        public List<string> SuggestWords(string word)
        {
            // initialize the word suggestion list..
            List<string> suggestions = new List<string>();

            if (word == null)
            {
                return suggestions;
            }

            // get the dictionary suggestions..
            try
            {
                suggestions = DictionarySuggest(word).ToList();
            }
            catch
            {
                // ignored..
            }

            if (UserDictionary != null)
            {
                try
                {
                    var userSuggestions = UserDictionary.Suggest(word).ToList();
                    for (int i = 0; i < userSuggestions.Count; i++)
                    {
                        if (!suggestions.Exists(f => string.Compare(f, userSuggestions[i], StringComparison.Ordinal) == 0))
                        {
                            suggestions.Add(userSuggestions[i]);
                        }
                    }

                    // sort the list as there are possible two word sets in the suggestion list..
                    suggestions.Sort((x, y) => string.Compare(x, y, StringComparison.InvariantCultureIgnoreCase));
                }
                catch
                {
                    // ignored..
                }
            }

            return suggestions;
        }

        /// <summary>
        /// Delegate OnSpellCheckLocationChanged
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:VPKSoft.SpellCheckUtility.UtilityClasses.SpellCheckLocationChangeEventArgs"/> instance containing the event data.</param>
        public delegate void OnSpellCheckLocationChanged(object sender, SpellCheckLocationChangeEventArgs e);

        /// <summary>
        /// Occurs when spell check location within the text changed.
        /// </summary>
        public static event OnSpellCheckLocationChanged SpellCheckLocationChanged;

        /// <summary>
        /// Loops the given text through spell check <see cref="ISpellCheck.RunSpellCheck"/> interface
        /// method implementation and returns the user checked text.
        /// </summary>
        /// <param name="spellCheck">A class instance which implements the <see cref="ISpellCheck"/> interface.</param>
        /// <param name="text">The text to be spell-checked by the user.</param>
        /// <param name="nullOnClose">A value indicating whether to return null if the spell checking was not completed by a user.</param>
        /// <returns>The text spell-checked by user.</returns>
        public string RunSpellCheckInterfaceOnText(ISpellCheck spellCheck, string text, bool nullOnClose)
        {
            // get the spell checking correction list..
            var corrections = RunSpellCheckInterface(spellCheck, text, nullOnClose);

            // the spell checking was interrupted, so do return null..
            if (corrections == null)
            {
                return null;
            }

            // correct the text and return the value..
            foreach (var correction in corrections)
            {
                text = text.Remove(correction.StartLocation, correction.Length);
                text = text.Insert(correction.StartLocation, correction.CorrectedWord);
            }

            return text;
        }

        /// <summary>
        /// Gets or sets a value indicating whether any calls to the <see cref="ISpellCheck.RunSpellCheck"/> method
        /// calls were made on the previous call to the <see cref="RunSpellCheckInterface"/> or to the <see cref="RunSpellCheckInterfaceOnText"/> methods.
        /// This equals that no spelling errors were found.
        /// </summary>
        public static bool ChecksDoneOnPreviousRun { get; set; }

        /// <summary>
        /// Loops the given text through spell check <see cref="ISpellCheck.RunSpellCheck"/> interface
        /// method implementation amd returns the user given corrections for the given.
        /// </summary>
        /// <param name="spellCheck">A class instance which implements the <see cref="ISpellCheck"/> interface.</param>
        /// <param name="text">The text to be spell-checked by the user.</param>
        /// <param name="nullOnClose">A value indicating whether to return null if the spell checking was not completed by a user.</param>
        /// <returns>A list of corrections made to the text spell-checked by user.</returns>
        public List<SpellingCorrection> RunSpellCheckInterface(ISpellCheck spellCheck, string text, bool nullOnClose)
        {
            ChecksDoneOnPreviousRun = false;
            var result = new List<SpellingCorrection>(); // create the result instance..

            // attach the methods to the interface-implementing class..
            spellCheck.AddIgnoreWordAction = AddIgnoreWord;
            spellCheck.AddUserDictionaryWord = AddUserDictionaryWord;
            spellCheck.RequestWordSuggestion = SuggestWords;
            spellCheck.CanAddToUserDictionary = CanAddToUserDictionary;
            spellCheck.CanAddToUserIgnoreList = CanAddToUserIgnoreList;

            // run the spell checking for the text..
            var missSpelledWords = SpellCheckFast(text);

            // a value indicating whether the spell checking was created successfully..
            var interrupted = false;

            // ReSharper disable once ForCanBeConvertedToForeach, the collection will change..
            for (int i = 0; i < missSpelledWords.Count; i++)
            {
                // the misspelled word..
                var word = missSpelledWords[i];

                // if the user's personal ignore list contains the word, the skip it..
                if (IgnoreList.Contains(word.Word))
                {
                    continue;
                }

                // check if the word is set to be replaced with the same word at all times..
                var replaceAll = missSpelledWords.FirstOrDefault(f => f.ReplaceAll && f.Word ==
                    word.Word && f.CorrectedWord != null);

                if (replaceAll != null) 
                {
                    word.ReplaceAll = true; // set the replace all flag to true..

                    // multiply the replace all to the result for easier implementation..
                    result.Add(new SpellingCorrection
                    {
                        Word = word.Word, 
                        CorrectedWord = replaceAll.CorrectedWord,
                        StartLocation = word.StartLocation, 
                        EndLocation = word.StartLocation + word.Length,
                        ReplaceAll = word.ReplaceAll,
                    });

                    // clone the found instance of this replace all word..
                    var newWord = replaceAll.Clone();
                    newWord.StartLocation = word.StartLocation; // change the locations..
                    newWord.EndLocation = word.EndLocation;
                    newWord.CorrectedWord = replaceAll.CorrectedWord; // set the word correction..

                    // raise the event in case the the spell checking is done in real-time..
                    SpellCheckLocationChanged?.Invoke(this, new SpellCheckLocationChangeEventArgs {SpellingError = newWord, AfterSpellCheck = true});
                    continue; // this case there is no need to go on further..
                }

                // the ignore all case..
                if (missSpelledWords.Any(f => f.IgnoreAll && f.Word == word.Word))
                {
                    // ..the user has made choice to ignore all instances of this misspelled word..
                    continue;
                }

                // raise the event in case the the spell checking is done in real-time so
                // that the whatever-GUI can mark the word before the user makes a choice
                // how to handle the word..
                SpellCheckLocationChanged?.Invoke(this, new SpellCheckLocationChangeEventArgs {SpellingError = word, AfterSpellCheck = false});

                // call the ISpellCheck implementation instance to choose the action for a
                // new misspelled word..
                ChecksDoneOnPreviousRun = true;
                if (!spellCheck.RunSpellCheck(word, SuggestWords(word.Word), i + 1, missSpelledWords.Count))
                {
                    // set the interrupted flag to true so the result of this
                    // method can be set accordingly and break the lood..
                    interrupted = true; 
                    break;
                }

                // if the word is supposed to be ignored or to be ignored at all times
                // the loop doesn't need to continue any further..
                if (word.IgnoreAll || word.Ignore)
                {
                    continue;
                }

                // if for some reason the corrected word would be null
                // the loop doesn't need to continue any further..
                if (word.CorrectedWord == null)
                {
                    continue;
                }

                // raise the event in case the the spell checking is done in real-time so
                // the corrected word can be applied to the text..
                SpellCheckLocationChanged?.Invoke(this, new SpellCheckLocationChangeEventArgs {SpellingError = word, AfterSpellCheck = true});

                // add the corrected word to the result..
                result.Add(new SpellingCorrection
                {
                    Word = word.Word, 
                    CorrectedWord = word.CorrectedWord,
                    StartLocation = word.StartLocation, 
                    EndLocation = word.StartLocation + word.Length,
                    ReplaceAll = word.ReplaceAll,
                });
            }

            // if the spell checking was interrupted and
            // a null result was requested on interruption, return null..
            if (interrupted && nullOnClose)
            {
                return null;
            }

            // order the result backwards..
            result = 
                result.OrderByDescending(f => f.StartLocation).
                    ThenByDescending(f => f.Length).ToList();

            // return the result..
            return result;
        }

        /// <summary>
        /// Fetches the miss-spelled words and their locations of the given text using compiled regular expression to match the words.
        /// </summary>
        public List<SpellingError> SpellCheckFast(string text)
        {
            var result = new List<SpellingError>();

            var words = WordBoundaryRegex.Matches(text);


            // initialize a list of failed words to speed up the spell checking..
            List<string> failedWords = new List<string>();

            // initialize a list of passed words to speed up the spell checking..
            List<string> wordOkList = new List<string>();


            for (int i = 0; i < words.Count; i++)
            {
                if (wordOkList.Contains(words[i].Value))
                {
                    // already passed the check..
                    continue;
                }

                // check if the word is already in the list of failed words..
                bool inFailedWordList = failedWords.Contains(words[i].Value);

                if (inFailedWordList)
                {
                    // ..add the location to the result..
                    result.Add(new SpellingError
                    {
                        StartLocation = words[i].Index, EndLocation = words[i].Length + words[i].Index,
                        Word = words[i].Value,
                    });
                    continue;
                }

                // check the ignore list..
                if (IgnoreList.Exists(f =>
                        string.Equals(f, words[i].Value, StringComparison.InvariantCultureIgnoreCase)))
                {
                    // flag the word as ok to prevent re-checking the validity..
                    wordOkList.Add(words[i].Value);

                    // the word is valid via user dictionary or user ignore list..
                    continue;
                }

                // validate the possible user dictionary..
                bool userDictionaryOk = UserDictionary?.Check(words[i].Value) ?? false;

                if (userDictionaryOk)
                {
                    // flag the word as ok to prevent re-checking the validity..
                    wordOkList.Add(words[i].Value);

                    // the word is valid via user dictionary or user ignore list..
                    continue;
                }

                // validate the word with the loaded dictionary..
                if (!DictionaryCheck(words[i].Value))
                {
                    // flag the word as invalid to prevent re-checking the validity..
                    failedWords.Add(words[i].Value);

                    // ..add the location to the result..
                    result.Add(new SpellingError
                    {
                        StartLocation = words[i].Index, EndLocation = words[i].Length + words[i].Index,
                        Word = words[i].Value,
                    });
                }
                else
                {
                    // flag the word as ok to prevent re-checking the validity..
                    wordOkList.Add(words[i].Value);
                }
            }

            return result;
        }

        #region LoadSave
        /// <summary>
        /// Loads the dictionary in to the <see cref="Dictionary"/> property.
        /// </summary>
        /// <param name="dictionaryFile">The dictionary file (*.dic).</param>
        /// <param name="affixFile">The affix file (*.aff).</param>
        public static void LoadDictionary(string dictionaryFile, string affixFile)
        {
            // read the data from the streams and dispose of them..
            using (var dictionaryStream = File.OpenRead(dictionaryFile))
            {
                using (var affixStream = File.OpenRead(affixFile))
                {
                    // create a new dictionary..
                    Dictionary = WordList.CreateFromStreams(dictionaryStream, affixStream);
                }
            }
        }

        /// <summary>
        /// Gets the user dictionary as a tab delimited list. The standard save format.
        /// </summary>
        /// <returns>The user dictionary as a tab delimited string value.</returns>
        public static string GetUserDictionarySaveValue()
        {
            if (UserDictionaryWords.Count > 0)
            {
                return string.Join("\t", UserDictionaryWords);
            }

            return "";
        }

        /// <summary>
        /// Gets the words ignored by the users as a tab delimited list. The standard save format.
        /// </summary>
        /// <returns>The words ignored by the users as a tab delimited string value.</returns>
        public static string GetUserDictionaryIgnoreWordsValue()
        {
            if (IgnoreList.Count > 0)
            {
                return string.Join("\t", IgnoreList);
            }

            return "";
        }

        /// <summary>
        /// Sets the user dictionary from a given tab delimited string containing the words.
        /// </summary>
        /// <param name="saveValue">The tab delimited string containing the words.</param>
        public static void SetUserDictionarySaveValue(string saveValue)
        {
            if (string.IsNullOrWhiteSpace(saveValue))
            {
                return;
            }

            var split = saveValue.Split('\t');
            LoadUserDictionary(split);
        }

        /// <summary>
        /// Sets the user word ignore list from a given tab delimited string containing the words.
        /// </summary>
        /// <param name="saveValue">The tab delimited string containing the user word ignore list.</param>
        public static void SetUserDictionaryIgnoreWordsValue(string saveValue)
        {
            if (string.IsNullOrWhiteSpace(saveValue))
            {
                return;
            }

            var split = saveValue.Split('\t');
            IgnoreList = new List<string>(split);
        }

        /// <summary>
        /// Saves the user dictionary and the word ignore list to a given stream.
        /// </summary>
        /// <param name="stream">The stream to save the word list to.</param>
        /// <returns>True if the user dictionary was successfully saved; otherwise false.</returns>
        public static bool SaveUserDictionary(Stream stream)
        {
            try
            {
                using (TextWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(GetUserDictionarySaveValue());
                    writer.WriteLine(GetUserDictionaryIgnoreWordsValue());
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the user dictionary and the word ignore list to a given file name.
        /// </summary>
        /// <param name="fileName">The file name to save the user dictionary settings to.</param>
        /// <returns>True if the user dictionary was successfully saved; otherwise false.</returns>
        public static bool SaveUserDictionary(string fileName)
        {
            try
            {
                using (var fileStream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    return SaveUserDictionary(fileStream);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a user dictionary.
        /// </summary>
        /// <param name="wordStrings">A list of words to use within a user dictionary.</param>
        public static void LoadUserDictionary(params string[] wordStrings)
        {
            // create the user dictionary..
            UserDictionary = WordList.CreateFromWords(wordStrings);

            // save the words to the list, so the user dictionary can be
            // reconstructed..
            UserDictionaryWords = new List<string>(wordStrings);
        }

        /// <summary>
        /// Clears to user dictionary.
        /// </summary>
        public static void ClearUserDictionary()
        {
            UserDictionary = null;
            UserDictionaryWords.Clear();
            IgnoreList.Clear();
        }

        /// <summary>
        /// Loads the user dictionary and the word ignore list from a given stream.
        /// </summary>
        /// <param name="stream">The stream load the the word list from.</param>
        /// <returns>True if the user dictionary was successfully loaded; otherwise false.</returns>
        public static bool LoadUserDictionary(Stream stream)
        {
            try
            {
                
                using (var reader = new StreamReader(stream))
                {
                    SetUserDictionarySaveValue(reader.ReadLine());
                    SetUserDictionaryIgnoreWordsValue(reader.ReadLine());
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads the user dictionary and the word ignore list from a given file name.
        /// </summary>
        /// <param name="fileName">The file name to load the user dictionary settings from.</param>
        /// <returns>True if the user dictionary was successfully loaded; otherwise false.</returns>
        public static bool LoadUserDictionary(string fileName)
        {
            try
            {
                using (var fileStream = File.OpenRead(fileName))
                {
                    return LoadUserDictionary(fileStream);
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

