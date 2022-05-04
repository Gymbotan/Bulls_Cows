using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bulls_Cows
{
    /// <summary>
    /// Form where you are playing the game.
    /// </summary>
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button1; //обработка клавиши Enter
            count = 1; 
            while (true)
            {
                rnd = new Random().Next(0123, 9876);
                if (rnd < 1000)
                {
                    char[] mas = rnd.ToString().ToCharArray();
                    ideal[0] = '0';
                    for (int i = 0; i < 3; i++)
                    {
                        ideal[i + 1] = mas[0];
                    }
                }
                else
                {
                    ideal = rnd.ToString().ToCharArray();
                }

                if (isGood(ideal))
                {
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] mas = textBox2.Text.ToCharArray();
            if (mas.Length == 3)
            {
                char[] tempMas = new char[4];
                for (int i = 2; i >= 0; i--)
                {
                    tempMas[i + 1] = mas[i];
                    tempMas[0] = '0';
                }

                mas = tempMas;
            }

            if (mas.Length != 4 || !isGood (mas))
            {
                MessageBox.Show("Неверный ввод.\nНеобходимо ввести четырёхзначное число без повторений цифр.");
            }
            else
            {
                dataGridView1.Rows.Add();
                for (int i = 0; i <= 3; i++)
                {
                    dataGridView1.Rows[count-1].Cells[i].Value = mas[i];
                }
                howManyBullsAndCows(mas);
                count++;
            }
            
            textBox2.Clear();
            textBox2.Focus();
        }

        public void howManyBullsAndCows(char[] mas)
        {
            byte bulls = 0;
            byte cows = 0;
            // Calculating of bulls
            for (int i = 0; i <= 3; i++)
            {
                if (mas[i] == ideal[i])
                    bulls++;
            }

            // Calculating of cows
            for (int i = 0; i <= 3; i++)
            {
                if (mas[i] == ideal[0] || mas[i] == ideal[1] || mas[i] == ideal[2] || mas[i] == ideal[3])
                    cows++;
            }
            cows -= bulls;

            dataGridView1.Rows[count-1].Cells[4].Value = bulls;
            dataGridView1.Rows[count-1].Cells[5].Value = cows;

            if (bulls == 4)
            {
                MessageBox.Show("Поздравляем! Вы угадали загаданное число с " + count + " попыток");
                Close();
            }
        }

        public static bool isGood(char[] mas)
        {
            if (mas.All(c => char.IsDigit(c)))
            {
                if (mas[0] == mas[1] || mas[0] == mas[2] || mas[0] == mas[3] || mas[1] == mas[2] || mas[1] == mas[3] || mas[2] == mas[3])
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
