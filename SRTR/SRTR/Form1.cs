using System;
using System.Windows.Forms;

namespace SRTR {
    public partial class Form1 : Form {

        SRTR s = new SRTR();

        public Form1() {
            InitializeComponent();
            //s.make('ア');
        }

        private void button1_Click(object sender, EventArgs e) {
            comboBox1.Items.Clear();
            Words.reset();
            for (int i = 0; i < textBox1.Lines.Length; i++) {
                //listBox1.Items.Add(textBox1.Lines[i]);
                new Words(textBox1.Lines[i]);
            }
            s.make(textBox2.Text[0], textBox3.Text[0], (int)numericUpDown1.Value, false);
            if (s.sList.Count > 0) {
                for (int j = 0; j < s.sList.Count; j++) {
                    if (s.sList[j].Count > 1) {
                        comboBox1.Items.Add(s.sList[j].Count + ":" + Words.wList[s.sList[j][0]].word + "->" + Words.wList[s.sList[j][s.sList[j].Count - 1]].word);
                    } else {
                        comboBox1.Items.Add(s.sList[j].Count + ":" + Words.wList[s.sList[j][0]].word);
                    }
                }
            } else {
                comboBox1.Items.Add("None.");
            }

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            listBox2.Items.Clear();
            if (s.sList.Count > 0) {
                for (int i = 0; i < s.sList[comboBox1.SelectedIndex].Count; i++) {
                    listBox2.Items.Add(i + ":" + Words.wList[s.sList[comboBox1.SelectedIndex][i]].word);
                }
            }
        }
    }
}
