using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Numerics;

namespace protect_inf_LR1
{
    public partial class Form1 : Form
    {
        char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Ґ', 'Д', 'Е', 'Є', 'Ж', 'З', 'И',
                                                        'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь',
                                                        'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };


        public Form1()
        {
            InitializeComponent();
        }

        //зашифровати
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            if ((textBox_p.Text.Length > 0) && (textBox_q.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox_p.Text);
                long q = Convert.ToInt64(textBox_q.Text);

                if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
                {
                    string s = "";

                    StreamReader sr = new StreamReader("in.txt");

                    while (!sr.EndOfStream)
                    {
                        s += sr.ReadLine();
                    }

                    sr.Close();

                    s = s.ToUpper();

                    long n = p * q;
                    long m = (p - 1) * (q - 1); 
                    long d = Calculate_d(m);
                    long e_ = Calculate_e(d, m); 

                    List<string> result = RSA_Endoce(s, e_, n);

                    StreamWriter sw = new StreamWriter("out1.txt");
                    foreach (string item in result)
                        sw.WriteLine(item);
                    sw.Close();

                    textBox_d.Text = d.ToString();
                    textBox_n.Text = n.ToString();

                    Process.Start("out1.txt");
                }
                else
                    MessageBox.Show("p или q - не прості числа!");
            }
            else
                MessageBox.Show("Введіть p и q!");
        }

        //дешифрувати
        private void buttonDecipher_Click(object sender, EventArgs e)
        {
            if ((textBox_d.Text.Length > 0) && (textBox_n.Text.Length > 0))
            {
                long d = Convert.ToInt64(textBox_d.Text);
                long n = Convert.ToInt64(textBox_n.Text);

                List<string> input = new List<string>();

                StreamReader sr = new StreamReader("out1.txt");

                while (!sr.EndOfStream)
                {
                    input.Add(sr.ReadLine());
                }

                sr.Close();

                string result = RSA_Dedoce(input, d, n);

                StreamWriter sw = new StreamWriter("out2.txt");
                sw.WriteLine(result);
                sw.Close();

                Process.Start("out2.txt");
            }
            else
                MessageBox.Show("Введіть секретний ключ!");
        }

        //перевірка: простое число?
        private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        //зашифровати
        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        //дешифрувати
        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        //обрахунок параметра d. d повинно бути взаємно простим з m
        private long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) //якщо мають спільні дільники
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        //обчислення параметра e
        private long Calculate_e(long d, long m)
        {
            long e = 23;

           while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }
         
            return e; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
