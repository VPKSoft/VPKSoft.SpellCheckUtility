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
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.SpellCheckUtility.UtilityClasses;
using TabDeliLocalization = VPKSoft.SpellCheckUtility.WinForms.UtilityClasses.TabDeliLocalization;

namespace VPKSoft.SpellCheckUtility.WinForms
{
    /// <summary>
    /// A Windows Forms dialog to run a spell check for text.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// Implements the <see cref="VPKSoft.SpellCheckUtility.ISpellCheck" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    /// <seealso cref="VPKSoft.SpellCheckUtility.ISpellCheck" />
    public partial class FormDialogSpellCheck : Form, ISpellCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSpellCheck"/> class.
        /// </summary>
        public FormDialogSpellCheck()
        {
            InitializeComponent();

            if (LocalizeOnCreate)
            {
                Localize();
            }

            SetGuiTexts();

            try
            {
                var assemblyLocation = Assembly.GetEntryAssembly()?.Location;
                Icon = Icon.ExtractAssociatedIcon(assemblyLocation ?? Assembly.GetExecutingAssembly().Location);
            }
            catch
            {
                // ignored..
            }
        }

        private Point PreviousLocation { get; set; }

        /// <summary>
        /// Gets or sets the any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. 
        /// </summary>
        public static IWin32Window OwnerWindow { get; set; }

        private void FormDialogSpellCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            PreviousLocation = Location;
        }

        private void FormDialogSpellCheck_Shown(object sender, EventArgs e)
        {
            if (PreviousLocation != default)
            {
                Location = PreviousLocation;
            }

            tbReplaceMissSpell.Text = SpellingError.Word;
            lbMissSpelledWordValue.Text = SpellingError.Word;
        }

        /// <summary>
        /// Localizes the dialog with given culture name in the format languagecode2-country/regioncode2. The culture 'en-US' is used as fall-back.
        /// </summary>
        /// <param name="cultureString">The culture string.</param>
        [SuppressMessage("ReSharper", "CommentTypo")]
        public static void Localize(string cultureString)
        {
            try
            {
                Localize(new CultureInfo(cultureString));
            }
            catch
            {
                Localize(CultureInfo.CurrentUICulture);
            }
        }

        #region Localization
        /// <summary>
        /// The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually consists of two uppercase letters, or a BCP-47 language tag.
        /// </summary>
        /// <value>The locale name to used with the dialog. <seealso cref="LocalizeOnCreate"/></value>
        [SuppressMessage("ReSharper", "CommentTypo")]
        public static string Locale { get; set; } = CultureInfo.CurrentUICulture.Name;

        /// <summary>
        /// Gets or sets the localized title text for the dialog.
        /// </summary>
        public static string TextTitle { get; set; } = "Spell checking";

        /// <summary>
        /// Gets or sets the localized text for a label indicating a misspelled word.
        /// </summary>
        public static string TextLabelMisspelledWord { get; set; } = "Misspelled word:";

        /// <summary>
        /// Gets or sets the localized text for a label indicating a replacement for a misspelled word.
        /// </summary>
        public static string TextLabelReplaceMisspelledWord { get; set; } = "Replace &with:";

        /// <summary>
        /// Gets or sets a localized text for a button to get suggestion for a user-given word.
        /// </summary>
        // ReSharper disable once StringLiteralTypo (mnemonic)
        public static string TextButtonSuggestWords { get; set; } = "Chec&k word";

        /// <summary>
        /// Gets or sets the text for a label indicating a word suggestion list box near the label.
        /// </summary>
        // ReSharper disable once StringLiteralTypo (mnemonic)
        public static string TextListSuggestionsLabel { get; set; } = "S&uggestions:";

        /// <summary>
        /// Gets or sets the text for a replace button.
        /// </summary>
        public static string TextButtonReplace { get; set; } = "&Replace";

        /// <summary>
        /// Gets or sets the text for an ignore button.
        /// </summary>
        public static string TextButtonIgnore { get; set; } = "&Ignore";

        /// <summary>
        /// Gets or sets the text for a replace all button.
        /// </summary>
        public static string TextButtonReplaceAll { get; set; } = "Replace &all";        

        /// <summary>
        /// Gets or sets the text for an ignore all button.
        /// </summary>
        // ReSharper disable once StringLiteralTypo (mnemonic)
        public static string TextButtonIgnoreAll { get; set; } = "Ig&nore all";

        /// <summary>
        /// Gets or sets the text label to indicate user dictionary functions.
        /// </summary>
        public static string TextLabelUserDictionary { get; set; } = "User dictionary:";

        /// <summary>
        /// Gets or sets the text for a button to add word to the user dictionary.
        /// </summary>
        public static string TextButtonAddWordToUserDictionary { get; set; } = "A&dd word";

        /// <summary>
        /// Gets or sets the text for a button for to add a word to the user dictionary ignore list.
        /// </summary>
        // ReSharper disable once StringLiteralTypo (mnemonic)
        public static string TextButtonUserDictionaryAddToIgnoreList { get; set; } = "I&gnore always";

        /// <summary>
        /// Gets or sets the text for a label to indicate the location of the spell checking process.
        /// </summary>
        public static string TextLabelWord { get; set; } = "Word:";

        /// <summary>
        /// Gets or sets the text for a close button.
        /// </summary>
        public static string TextButtonClose { get; set; } = "&Close";

        /// <summary>
        /// Gets or sets the text label indicating words checked versus the total word check count.
        /// </summary>
        public static string TextLabelWordOfWord { get; set; } = "{0} / {1}";

        /// <summary>
        /// Localizes the dialog with a given culture. The culture 'en-US' is used as fall-back.
        /// </summary>
        /// <param name="culture">The culture to use for localization.</param>
        public static void Localize(CultureInfo culture)
        {
            var localization = new TabDeliLocalization();
            localization.GetLocalizedTexts(Properties.Resources.Localization);
            TextTitle = localization.GetMessage("txtTitle", 
                "Spell checking", culture.Name);

            TextLabelMisspelledWord = localization.GetMessage("txtMisspelledWord", 
                "Misspelled word:", culture.Name);

            TextLabelReplaceMisspelledWord = localization.GetMessage("txtReplaceWith",
                "Replace &with:", culture.Name);

            TextButtonSuggestWords = localization.GetMessage("txtCheckWord",
                // ReSharper disable once StringLiteralTypo (mnemonic)
                "Chec&k word", culture.Name);

            TextListSuggestionsLabel = localization.GetMessage("txtSuggestions",
                // ReSharper disable once StringLiteralTypo (mnemonic)
                "S&uggestions", culture.Name);

            TextButtonReplace = localization.GetMessage("txtReplace",
                "&Replace", culture.Name);

            TextButtonIgnore = localization.GetMessage("txtIgnore",
                "&Ignore", culture.Name);

            TextButtonReplaceAll = localization.GetMessage("txtReplaceAll",
                "Replace &all", culture.Name);

            TextButtonIgnoreAll = localization.GetMessage("txtIgnoreAll",
                // ReSharper disable once StringLiteralTypo (mnemonic)
                "Ig&nore all", culture.Name);

            TextLabelUserDictionary = localization.GetMessage("txtUserDictionary",
                "User dictionary:", culture.Name);

            TextButtonAddWordToUserDictionary = localization.GetMessage("txtAddWord",
                "A&dd word", culture.Name);

            TextButtonUserDictionaryAddToIgnoreList = localization.GetMessage("txtIgnoreAlways",
                // ReSharper disable once StringLiteralTypo (mnemonic)
                "I&gnore always", culture.Name);

            TextLabelWord = localization.GetMessage("txtWord",
                "Word:", culture.Name);

            TextButtonClose = localization.GetMessage("txtClose",
                "&Close", culture.Name);

            TextLabelWordOfWord = localization.GetMessage("txtWordOfWordFormat",
                "{0} / {1}", culture.Name);
        }

        /// <summary>
        /// Sets the localized GUI texts for the dialog.
        /// </summary>
        public void SetGuiTexts()
        {
            Text = TextTitle;
            lbMissSpelledWord.Text = TextLabelMisspelledWord;
            lbReplaceMissSpell.Text = TextButtonReplace;
            btCheckWord.Text = TextButtonSuggestWords;
            lbSuggestions.Text = TextListSuggestionsLabel;
            btReplace.Text = TextButtonReplace;
            btIgnore.Text = TextButtonIgnore;
            btReplaceAll.Text = TextButtonReplaceAll;
            btIgnoreAll.Text = TextButtonIgnoreAll;
            lbUserDictionary.Text = TextLabelUserDictionary;
            btAddToUserDictionary.Text = TextButtonAddWordToUserDictionary;
            btAddToIgnoreList.Text = TextButtonUserDictionaryAddToIgnoreList;
            lbWordCountDescription.Text = TextLabelWord;
            btClose.Text = TextButtonClose;
            lbWordCountValue.Text = string.Format(TextLabelWordOfWord, 0, 0);
        }

        /// <summary>
        /// Localizes the dialog using the <see cref="CultureInfo.CurrentUICulture" />. The culture 'en-US' is used as fall-back.
        /// </summary>
        public static void Localize()
        {
            if (!string.IsNullOrWhiteSpace(Locale))
            {
                try
                {
                    Localize(new CultureInfo(Locale));
                }
                catch
                {
                    Localize(CultureInfo.CurrentUICulture);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the dialog should be localized in the constructor.
        /// </summary>
        /// <value><c>true</c> if the dialog should be localized in the constructor; otherwise, <c>false</c>.</value>
        public static bool LocalizeOnCreate { get; set; } = true;
        #endregion

        /// <summary>
        /// Runs the spell checking for a miss-spelled word.
        /// </summary>
        /// <param name="spellingError">The current spelling error.</param>
        /// <param name="suggestions">The suggestions for the miss-spelled word.</param>
        /// <param name="location">The location where the spell checking is going at.</param>
        /// <param name="count">The count of total spell checks.</param>
        /// <returns><c>true</c> if the spell check loop should continue, <c>false</c> otherwise.</returns>
        public bool RunSpellCheck(SpellingError spellingError, List<string> suggestions, int location, int count)
        {
            listSuggestions.Items.Clear();
            listSuggestions.Items.AddRange(suggestions.ToArray().ToArray<object>());
            SpellingError = spellingError;

            lbWordCountValue.Text = $@"{location} / {count}";

            var result = ShowDialog(OwnerWindow) != DialogResult.Cancel;

            return result;
        }

        /// <summary>
        /// Gets or sets the action to add word to the user ignore word list.
        /// </summary>
        /// <value>The action to add word to the user ignore word list.</value>
        public Action<string> AddIgnoreWordAction { get; set; }

        /// <summary>
        /// Gets or sets the action to add a word to the user dictionary.
        /// </summary>
        /// <value>The action to add a word to the user dictionary.</value>
        public Action<string> AddUserDictionaryWord { get; set; }

        /// <summary>
        /// Gets or sets the Func to request suggestions for a word.
        /// </summary>
        /// <value>The the Func to request suggestions for a word.</value>
        public Func<string, List<string>> RequestWordSuggestion { get; set; }

        /// <summary>
        /// Gets or sets the function to detect whether the given word can be added to the user dictionary.
        /// </summary>
        /// <value>The function to detect whether the given word can be added to the user dictionary.</value>
        public Func<string, bool> CanAddToUserDictionary { get; set; }

        /// <summary>
        /// Gets or sets the function to detect whether the given word can be added to the user ignore list.
        /// </summary>
        /// <value>The function to detect whether the given word can be added to the user ignore list.</value>
        public Func<string, bool> CanAddToUserIgnoreList { get; set; }

        /// <summary>
        /// Gets or sets the current spelling error to handle.
        /// </summary>
        /// <value>The spelling error to handle.</value>
        public SpellingError SpellingError { get; set; }

        private void btAddToUserDictionary_Click(object sender, EventArgs e)
        {
            AddUserDictionaryWord?.Invoke(tbReplaceMissSpell.Text);
            SpellingError.CorrectedWord = tbReplaceMissSpell.Text;
            DialogResult = DialogResult.OK;
        }

        private void btAddToIgnoreList_Click(object sender, EventArgs e)
        {
            AddIgnoreWordAction?.Invoke(tbReplaceMissSpell.Text);
            SpellingError.CorrectedWord = SpellingError.Word;
            SpellingError.Ignore = true;
            DialogResult = DialogResult.OK;
        }

        private void btReplace_Click(object sender, EventArgs e)
        {
            SpellingError.CorrectedWord = tbReplaceMissSpell.Text;
            DialogResult = DialogResult.OK;
        }

        private void tbReplaceMissSpell_TextChanged(object sender, EventArgs e)
        {
            SetButtonsEnabled();
        }

        private bool SuggestionContains(string word)
        {
            foreach (var item in listSuggestions.Items)
            {
                if (string.Compare(item.ToString(), word, StringComparison.Ordinal) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void SetButtonsEnabled()
        {
            var conditionText = !string.IsNullOrWhiteSpace(tbReplaceMissSpell.Text) &&
                                 (tbReplaceMissSpell.Text != SpellingError.Word ||
                                 SuggestionContains(tbReplaceMissSpell.Text));

            btReplace.Enabled = conditionText;
            btReplaceAll.Enabled = conditionText;
            btCheckWord.Enabled = conditionText;

            btAddToUserDictionary.Enabled = !string.IsNullOrWhiteSpace(tbReplaceMissSpell.Text) &&
                                            CanAddToUserDictionary?.Invoke(tbReplaceMissSpell.Text) == true &&
                                            !SuggestionContains(tbReplaceMissSpell.Text);

            btAddToIgnoreList.Enabled = !string.IsNullOrWhiteSpace(tbReplaceMissSpell.Text) &&
                                        CanAddToUserIgnoreList?.Invoke(tbReplaceMissSpell.Text) == true;
        }

        private void listSuggestions_SelectedValueChanged(object sender, EventArgs e)
        {
            var listBox = (ListBox) sender;
            if (listBox.SelectedItem != null)
            {
                tbReplaceMissSpell.Text = listBox.SelectedItem.ToString();
            }
        }

        private void btIgnore_Click(object sender, EventArgs e)
        {
            SpellingError.CorrectedWord = SpellingError.Word;
            SpellingError.Ignore = true;
            DialogResult = DialogResult.OK;
        }

        private void btReplaceAll_Click(object sender, EventArgs e)
        {
            SpellingError.CorrectedWord = tbReplaceMissSpell.Text;
            SpellingError.ReplaceAll = true;
            DialogResult = DialogResult.OK;
        }

        private void btIgnoreAll_Click(object sender, EventArgs e)
        {
            SpellingError.IgnoreAll = true;
            DialogResult = DialogResult.OK;
        }

        private void btCheckWord_Click(object sender, EventArgs e)
        {
            var suggestions = RequestWordSuggestion?.Invoke(tbReplaceMissSpell.Text);
            if (suggestions != null)
            {
                listSuggestions.Items.Clear();
                listSuggestions.Items.AddRange(suggestions.ToArray().ToArray<object>());
            }
        }
    }
}
