using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRTR {
    public partial class Form1 : Form {

        SRTR s = new SRTR();

        public Form1() {
            InitializeComponent();
            //s.make('ア');

        }

        private void button1_Click(object sender, EventArgs e) {
            Words.reset();
            for (int i = 0; i < textBox1.Lines.Length; i++) {
                //listBox1.Items.Add(textBox1.Lines[i]);
                new Words(textBox1.Lines[i]);
            }
            s.make(textBox2.Text[0], textBox3.Text[0], (int)numericUpDown1.Value);
        }
    }
}
