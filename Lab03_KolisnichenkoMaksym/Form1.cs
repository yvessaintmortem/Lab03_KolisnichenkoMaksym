using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_KolisnichenkoMaksym
{
    public partial class Form1 : Form
    {
        byte[] arrAfterXOR;
        bool flag = false;
        public Form1()
        {
            InitializeComponent();
        }

        void myShowToolTip(TextBox tB, byte[] arr) // Створення підказки в двійковому форматі
        {
            string[] b = arr.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray();
            string hexValues = string.Join(" ", b);
            toolTip_HEX.SetToolTip(tB, hexValues);
        }

        //Функція XOR вхідного значення з ключем
        byte[] myXOR(byte[] arr_text, byte[] arr_key)
        {
            int len_text = arr_text.Length;
            int len_key = arr_key.Length;
            byte[] arr_cipher = new byte[len_text];
            for (int i = 0; i < len_text; i++)
            {
                byte p = arr_text[i];
                byte k = arr_key[i % len_key]; // mod
                byte c = (byte)(p ^ k); // XOR

                arr_cipher[i] = c;
            }
            arrAfterXOR = arr_cipher;
            return arr_cipher;
        }

        string myCipher(TextBox tb_text, TextBox tb_Key, TextBox tb_cipher)
        {
            string text = tb_text.Text;
            byte[] arr_text;
            if (flag == false)
            {
                arr_text = Encoding.UTF32.GetBytes(text);
                flag = true;
            }
            else 
            {
                arr_text = arrAfterXOR;
                flag = false;
            }
            myShowToolTip(tb_text, arr_text); // Створити підказку

            string key = tb_Key.Text;
            byte[] arr_key = Encoding.UTF32.GetBytes(key);
            myShowToolTip(tb_Key, arr_key); // Створити підказку

            byte[] arr_cipher = myXOR(arr_text, arr_key);

            string cipher = Encoding.UTF32.GetString(arr_cipher);
            tb_cipher.Text = cipher;
            myShowToolTip(tb_cipher, arr_cipher); // Створити підказку

            return cipher;
        }

        private void button_XOR_Click(object sender, EventArgs e)
        {
            if (textBox_Key_IN.Text == "")
            {
                textbox_C_IN.Text = textBox_P_IN.Text;
                textBox_P_OUT.Text = textBox_P_IN.Text;
                textBox_C_OUT.Text = textBox_P_IN.Text;
            }
            else
            {
                string cipher = myCipher(textBox_P_IN, textBox_Key_IN, textbox_C_IN); // зашифрування
                textBox_P_OUT.Text = textbox_C_IN.Text;
                textBox_Key_OUT.Text = textBox_Key_IN.Text;
                myCipher(textBox_P_OUT, textBox_Key_OUT, textBox_C_OUT); // розшифрування
            }
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_P_IN.Text = "";
            textBox_Key_IN.Text = "";
            textbox_C_IN.Text = "";

            textBox_P_OUT.Text = "";
            textBox_Key_OUT.Text = "";
            textBox_C_OUT.Text = "";
        }
    }
}
