namespace VPKSoft.SpellCheckUtility.WinForms
{
    partial class FormDialogSpellCheck
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbMissSpelledWord = new System.Windows.Forms.Label();
            this.lbReplaceMissSpell = new System.Windows.Forms.Label();
            this.tbReplaceMissSpell = new System.Windows.Forms.TextBox();
            this.btCheckWord = new System.Windows.Forms.Button();
            this.lbSuggestions = new System.Windows.Forms.Label();
            this.listSuggestions = new System.Windows.Forms.ListBox();
            this.btReplace = new System.Windows.Forms.Button();
            this.btIgnore = new System.Windows.Forms.Button();
            this.btIgnoreAll = new System.Windows.Forms.Button();
            this.btReplaceAll = new System.Windows.Forms.Button();
            this.lbUserDictionary = new System.Windows.Forms.Label();
            this.btAddToIgnoreList = new System.Windows.Forms.Button();
            this.btAddToUserDictionary = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lbMissSpelledWordValue = new System.Windows.Forms.Label();
            this.lbWordCountDescription = new System.Windows.Forms.Label();
            this.lbWordCountValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbMissSpelledWord
            // 
            this.lbMissSpelledWord.AutoSize = true;
            this.lbMissSpelledWord.Location = new System.Drawing.Point(12, 15);
            this.lbMissSpelledWord.Name = "lbMissSpelledWord";
            this.lbMissSpelledWord.Size = new System.Drawing.Size(85, 13);
            this.lbMissSpelledWord.TabIndex = 0;
            this.lbMissSpelledWord.Text = "Misspelled word:";
            // 
            // lbReplaceMissSpell
            // 
            this.lbReplaceMissSpell.AutoSize = true;
            this.lbReplaceMissSpell.Location = new System.Drawing.Point(12, 41);
            this.lbReplaceMissSpell.Name = "lbReplaceMissSpell";
            this.lbReplaceMissSpell.Size = new System.Drawing.Size(72, 13);
            this.lbReplaceMissSpell.TabIndex = 1;
            this.lbReplaceMissSpell.Text = "Replace with:";
            // 
            // tbReplaceMissSpell
            // 
            this.tbReplaceMissSpell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReplaceMissSpell.Location = new System.Drawing.Point(153, 38);
            this.tbReplaceMissSpell.Name = "tbReplaceMissSpell";
            this.tbReplaceMissSpell.Size = new System.Drawing.Size(213, 20);
            this.tbReplaceMissSpell.TabIndex = 3;
            this.tbReplaceMissSpell.TextChanged += new System.EventHandler(this.tbReplaceMissSpell_TextChanged);
            // 
            // btCheckWord
            // 
            this.btCheckWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCheckWord.Location = new System.Drawing.Point(381, 36);
            this.btCheckWord.Name = "btCheckWord";
            this.btCheckWord.Size = new System.Drawing.Size(130, 23);
            this.btCheckWord.TabIndex = 4;
            this.btCheckWord.Text = "Check word";
            this.btCheckWord.UseVisualStyleBackColor = true;
            this.btCheckWord.Click += new System.EventHandler(this.btCheckWord_Click);
            // 
            // lbSuggestions
            // 
            this.lbSuggestions.AutoSize = true;
            this.lbSuggestions.Location = new System.Drawing.Point(12, 67);
            this.lbSuggestions.Name = "lbSuggestions";
            this.lbSuggestions.Size = new System.Drawing.Size(68, 13);
            this.lbSuggestions.TabIndex = 5;
            this.lbSuggestions.Text = "Suggestions:";
            // 
            // listSuggestions
            // 
            this.listSuggestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSuggestions.FormattingEnabled = true;
            this.listSuggestions.IntegralHeight = false;
            this.listSuggestions.Location = new System.Drawing.Point(15, 90);
            this.listSuggestions.Name = "listSuggestions";
            this.listSuggestions.Size = new System.Drawing.Size(272, 150);
            this.listSuggestions.TabIndex = 6;
            this.listSuggestions.SelectedValueChanged += new System.EventHandler(this.listSuggestions_SelectedValueChanged);
            // 
            // btReplace
            // 
            this.btReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReplace.Location = new System.Drawing.Point(293, 90);
            this.btReplace.Name = "btReplace";
            this.btReplace.Size = new System.Drawing.Size(106, 23);
            this.btReplace.TabIndex = 8;
            this.btReplace.Text = "Replace";
            this.btReplace.UseVisualStyleBackColor = true;
            this.btReplace.Click += new System.EventHandler(this.btReplace_Click);
            // 
            // btIgnore
            // 
            this.btIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btIgnore.Location = new System.Drawing.Point(405, 90);
            this.btIgnore.Name = "btIgnore";
            this.btIgnore.Size = new System.Drawing.Size(106, 23);
            this.btIgnore.TabIndex = 9;
            this.btIgnore.Text = "Ignore";
            this.btIgnore.UseVisualStyleBackColor = true;
            this.btIgnore.Click += new System.EventHandler(this.btIgnore_Click);
            // 
            // btIgnoreAll
            // 
            this.btIgnoreAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btIgnoreAll.Location = new System.Drawing.Point(405, 119);
            this.btIgnoreAll.Name = "btIgnoreAll";
            this.btIgnoreAll.Size = new System.Drawing.Size(106, 23);
            this.btIgnoreAll.TabIndex = 11;
            this.btIgnoreAll.Text = "Ignore all";
            this.btIgnoreAll.UseVisualStyleBackColor = true;
            this.btIgnoreAll.Click += new System.EventHandler(this.btIgnoreAll_Click);
            // 
            // btReplaceAll
            // 
            this.btReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReplaceAll.Location = new System.Drawing.Point(293, 119);
            this.btReplaceAll.Name = "btReplaceAll";
            this.btReplaceAll.Size = new System.Drawing.Size(106, 23);
            this.btReplaceAll.TabIndex = 10;
            this.btReplaceAll.Text = "Replace all";
            this.btReplaceAll.UseVisualStyleBackColor = true;
            this.btReplaceAll.Click += new System.EventHandler(this.btReplaceAll_Click);
            // 
            // lbUserDictionary
            // 
            this.lbUserDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUserDictionary.AutoSize = true;
            this.lbUserDictionary.Location = new System.Drawing.Point(293, 160);
            this.lbUserDictionary.Name = "lbUserDictionary";
            this.lbUserDictionary.Size = new System.Drawing.Size(80, 13);
            this.lbUserDictionary.TabIndex = 12;
            this.lbUserDictionary.Text = "User dictionary:";
            // 
            // btAddToIgnoreList
            // 
            this.btAddToIgnoreList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddToIgnoreList.Location = new System.Drawing.Point(405, 176);
            this.btAddToIgnoreList.Name = "btAddToIgnoreList";
            this.btAddToIgnoreList.Size = new System.Drawing.Size(106, 23);
            this.btAddToIgnoreList.TabIndex = 14;
            this.btAddToIgnoreList.Text = "Ignore always";
            this.btAddToIgnoreList.UseVisualStyleBackColor = true;
            this.btAddToIgnoreList.Click += new System.EventHandler(this.btAddToIgnoreList_Click);
            // 
            // btAddToUserDictionary
            // 
            this.btAddToUserDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddToUserDictionary.Location = new System.Drawing.Point(293, 176);
            this.btAddToUserDictionary.Name = "btAddToUserDictionary";
            this.btAddToUserDictionary.Size = new System.Drawing.Size(106, 23);
            this.btAddToUserDictionary.TabIndex = 13;
            this.btAddToUserDictionary.Text = "Add word";
            this.btAddToUserDictionary.UseVisualStyleBackColor = true;
            this.btAddToUserDictionary.Click += new System.EventHandler(this.btAddToUserDictionary_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(405, 217);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(106, 23);
            this.btClose.TabIndex = 15;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // lbMissSpelledWordValue
            // 
            this.lbMissSpelledWordValue.AutoSize = true;
            this.lbMissSpelledWordValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMissSpelledWordValue.Location = new System.Drawing.Point(150, 15);
            this.lbMissSpelledWordValue.Name = "lbMissSpelledWordValue";
            this.lbMissSpelledWordValue.Size = new System.Drawing.Size(11, 13);
            this.lbMissSpelledWordValue.TabIndex = 16;
            this.lbMissSpelledWordValue.Text = "-";
            // 
            // lbWordCountDescription
            // 
            this.lbWordCountDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbWordCountDescription.AutoSize = true;
            this.lbWordCountDescription.Location = new System.Drawing.Point(293, 214);
            this.lbWordCountDescription.Name = "lbWordCountDescription";
            this.lbWordCountDescription.Size = new System.Drawing.Size(36, 13);
            this.lbWordCountDescription.TabIndex = 17;
            this.lbWordCountDescription.Text = "Word:";
            // 
            // lbWordCountValue
            // 
            this.lbWordCountValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbWordCountValue.AutoSize = true;
            this.lbWordCountValue.Location = new System.Drawing.Point(293, 227);
            this.lbWordCountValue.Name = "lbWordCountValue";
            this.lbWordCountValue.Size = new System.Drawing.Size(46, 13);
            this.lbWordCountValue.TabIndex = 18;
            this.lbWordCountValue.Text = "{0} / {1}";
            // 
            // FormDialogSpellCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(523, 252);
            this.Controls.Add(this.lbWordCountValue);
            this.Controls.Add(this.lbWordCountDescription);
            this.Controls.Add(this.lbMissSpelledWordValue);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btAddToIgnoreList);
            this.Controls.Add(this.btAddToUserDictionary);
            this.Controls.Add(this.lbUserDictionary);
            this.Controls.Add(this.btIgnoreAll);
            this.Controls.Add(this.btReplaceAll);
            this.Controls.Add(this.btIgnore);
            this.Controls.Add(this.btReplace);
            this.Controls.Add(this.listSuggestions);
            this.Controls.Add(this.lbSuggestions);
            this.Controls.Add(this.btCheckWord);
            this.Controls.Add(this.tbReplaceMissSpell);
            this.Controls.Add(this.lbReplaceMissSpell);
            this.Controls.Add(this.lbMissSpelledWord);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogSpellCheck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spell checking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDialogSpellCheck_FormClosing);
            this.Shown += new System.EventHandler(this.FormDialogSpellCheck_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMissSpelledWord;
        private System.Windows.Forms.Label lbReplaceMissSpell;
        private System.Windows.Forms.TextBox tbReplaceMissSpell;
        private System.Windows.Forms.Button btCheckWord;
        private System.Windows.Forms.Label lbSuggestions;
        private System.Windows.Forms.ListBox listSuggestions;
        private System.Windows.Forms.Button btReplace;
        private System.Windows.Forms.Button btIgnore;
        private System.Windows.Forms.Button btIgnoreAll;
        private System.Windows.Forms.Button btReplaceAll;
        private System.Windows.Forms.Label lbUserDictionary;
        private System.Windows.Forms.Button btAddToIgnoreList;
        private System.Windows.Forms.Button btAddToUserDictionary;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lbMissSpelledWordValue;
        private System.Windows.Forms.Label lbWordCountDescription;
        private System.Windows.Forms.Label lbWordCountValue;
    }
}