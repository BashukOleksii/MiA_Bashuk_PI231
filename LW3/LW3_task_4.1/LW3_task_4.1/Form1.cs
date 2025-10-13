using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LW3_task_4._1
{

        public partial class Form1 : Form
        {
            private static readonly HttpClient _http = new HttpClient
            {
                BaseAddress = new Uri("https://dog.ceo"),
                Timeout = TimeSpan.FromSeconds(30)
            };
            public Form1()
            {
                InitializeComponent();
            }
            private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
            {
                e.Handled = true;
            }
            private async void button1_Click(object sender, EventArgs e)
            {
                try
                {
                    textBox2.Clear();
                    button1.Enabled = false;
                    var response = await _http.GetAsync("/api/breeds/image/random");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var Dog = JsonSerializer.Deserialize<Dog>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (Dog != null)
                    {
                        pictureBox1.LoadAsync(Dog.message);
                        textBox2.Text += "{" + Environment.NewLine + $"message: {Dog.message}," + Environment.NewLine + $"status: {Dog.status}" + Environment.NewLine + "}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Stop); ;
                }
                finally
                {
                    button1.Enabled = true;
                }
            }
           
        }

    }
