using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;


namespace LW3_task_4._2
{
    public partial class Form1 : Form
    {
        int index = 0;
        int max = 0;
        List<string> photos;

        private static readonly HttpClient _http = new HttpClient
        {
            BaseAddress = new Uri("https://dog.ceo"),
            Timeout = TimeSpan.FromSeconds(30)
        };
        public Form1()
        {
            InitializeComponent();
            photos = new List<string>();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
                return;
            int N = int.Parse(textBox3.Text);
            if (N > 50)
            {
                MessageBox.Show("Максимальна кількість 50 зображень", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string request = $"/api/breeds/image/random/{N}";
            try
            {
                index = 0;
                max = N;
                textBox2.Clear();
                button1.Enabled = false;
                var response = await _http.GetAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var Dogs = JsonSerializer.Deserialize<Dog>(json);
                if (Dogs != null)
                {
                    photos = Dogs.message;
                    pictureBox1.LoadAsync(Dogs.message[0]);
                    textBox2.Text += "{" + Environment.NewLine + $"message: ";
                    foreach (string item in Dogs.message)
                        textBox2.Text += item + "," + Environment.NewLine;
                    textBox2.Text += $"status: {Dogs.status}" + Environment.NewLine + "}";
                }
                else
                    photos.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                max = 0;
                photos.Clear();
            }
            finally
            {
                button1.Enabled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                return;
            e.Handled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.Text;
            if (text == ">" && photos.Count > 0 && index + 1 < max)
                index++;
            else if (text == "<" && index > 0 && photos.Count > 0)
                index--;
            if (photos.Count > 0)
                pictureBox1.LoadAsync(photos[index]);
        }
    }


}
