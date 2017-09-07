using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POSApplication
{
    public partial class MiniKeyboard : UserControl
    {
        //功能键的文字内容
        public string Backspace { get; set; }
        public string Cancel { get; set; }
        public string KeyEnter { get; set; }
        public string Dot { get; set; }
        public string X { get; set; }

        public delegate void MiniKeyboardHandler(object sender, MiniKeyboardArgs e);
        public event MiniKeyboardHandler Press;

        public MiniKeyboard()
        {
            InitializeComponent();
            BindEnvent();

            Backspace = btnBackspace.Text;
            Cancel = btnCancel.Text;
            KeyEnter = btnEnter.Text;
            Dot = btnDot.Text;
            X = btnX.Text;
        }

        private void BindEnvent()
        {
            foreach (Control ctl in this.Controls)
                if (ctl is Button)
                    ctl.Click += MiniKeyboardPress;
        }

        private void MiniKeyboardPress(Object sender, EventArgs e)
        {
            OnMiniKeyboardPress(new MiniKeyboardArgs(((Button)sender).Text));
        }

        private void OnMiniKeyboardPress(MiniKeyboardArgs e)
        {
            MiniKeyboardHandler temp = Press;
            if (temp != null)
                temp(this, e);
        }
    }

    public class MiniKeyboardArgs : EventArgs
    {
        public string KeyCode { get; private set; }
        public MiniKeyboardArgs(string code)
        {
            KeyCode = code;
        }
    }
}
