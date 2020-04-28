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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.SpellCheckUtility;
using VPKSoft.SpellCheckUtility.WinForms;

namespace TestApp
{
    /// <summary>
    /// The main for of the test application.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // get the path for the application..
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

            // the case which shouldn't ever happen..
            if (path == null)
            {
                Close();
                return;
            }

            // set the dictionary path..
            DictionaryPath = Path.Combine(path, "en_US_dictionary");

            // except the Hunspell affix and dictionary files to exists and load them..
            SpellChecker.LoadDictionary(Path.Combine(DictionaryPath, "en_US.dic"),
                Path.Combine(DictionaryPath, "en_US.aff"));


            // subscribe to the event which is called by the SpellChecker class on progressing with the spell checking;
            // there is no reason to subscribe this event if the checking isn't visualized or done in real-time..
            SpellChecker.SpellCheckLocationChanged += SpellChecker_SpellCheckLocationChanged;
        }

        /// <summary>
        /// Gets the dictionary path for the en-US spell checking.
        /// </summary>
        private string DictionaryPath { get; }

        /// <summary>
        /// Gets or sets the text offset correction. This is used in the real time spell checking..
        /// </summary>
        /// <value>The text offset correction.</value>
        private int TextOffsetCorrection { get; set; }

        // the real-time spell checking event..
        private void SpellChecker_SpellCheckLocationChanged(object sender, VPKSoft.SpellCheckUtility.UtilityClasses.SpellCheckLocationChangeEventArgs e)
        {
            // set the selection for the misspelled word..
            tbMain.SelectionStart = e.SpellingError.StartLocation + TextOffsetCorrection;
            tbMain.SelectionLength = e.SpellingError.Length;

            // if a correction for the spelling error was made..
            if (e.AfterSpellCheck) 
            {
                // ..fix the error in real time..
                tbMain.SelectedText = e.SpellingError.CorrectedWord;

                // ..set the offset, so this doesn't get messy..
                TextOffsetCorrection += e.SpellingError.CorrectedWord.Length - e.SpellingError.Length;
            }

            // scroll the "view" into visibility..
            tbMain.ScrollToCaret();
        }

        // start a real-time spell checking for the text..
        private void mnuSpellCheck_Click(object sender, EventArgs e)
        {
            TextOffsetCorrection = 0; // re-set the offset..
            // create a new instance of the spell checker class..
            var spellChecker = new SpellChecker();

            // create a new spell checking dialog (implementing the ISpellCheck) interface..
            FormDialogSpellCheck.OwnerWindow = this;

            // don't set to locale to Finnish as some one might have difficulties in understanding the dialog,
            // which can be localized;
            // FormDialogSpellCheck.Locale = "fi-FI"; 
            var spellingDialog = new FormDialogSpellCheck();

            // run the spell check..
            spellChecker.RunSpellCheckInterface(spellingDialog, tbMain.Text, false);
        }

        // a user wishes to open a text file..
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            // ..so let the user do so..
            if (odText.ShowDialog() == DialogResult.OK)
            {
                // read the file contents to the text box..
                tbMain.Text = File.ReadAllText(odText.FileName);
            }
        }
    }
}
