namespace Pixel2Image
{
    partial class ConverterForm
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
            choiceButton = new Button();
            reultText = new TextBox();
            SuspendLayout();
            // 
            // choiceButton
            // 
            choiceButton.Location = new Point(60, 120);
            choiceButton.Name = "choiceButton";
            choiceButton.Size = new Size(112, 34);
            choiceButton.TabIndex = 0;
            choiceButton.Text = "Choice";
            choiceButton.UseVisualStyleBackColor = false;
            choiceButton.Click += choiceButton_Click;
            // 
            // reultText
            // 
            reultText.BorderStyle = BorderStyle.FixedSingle;
            reultText.Location = new Point(60, 180);
            reultText.Name = "reultText";
            reultText.ReadOnly = true;
            reultText.Size = new Size(600, 30);
            reultText.TabIndex = 2;
            // 
            // Converter
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(reultText);
            Controls.Add(choiceButton);
            Name = "Converter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pixel Converter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button choiceButton;
        private TextBox reultText;
    }
}