using LW3_task_5.Service;
using System.Data;
using LW3_task_5.Models;
using System.Diagnostics.Metrics;

namespace LW3_task_5
{
    public partial class Form1 : Form
    {
        DataTable dt;
        List<Country> countries;
        APIClient aPIClient;
        public Form1()
        {
            InitializeComponent();
            aPIClient = new APIClient(15);
            LoadTable();
            Load("all?fields=name,capital,region,subregion,area,population,flags");
        }
        private void LoadTable()
        {
            dt = new DataTable();
            dt.Columns.Add("Поширена назва", typeof(string));
            dt.Columns.Add("Офіційна назва", typeof(string));
            dt.Columns.Add("Столиця", typeof(string));
            dt.Columns.Add("Регіон", typeof(string));
            dt.Columns.Add("Площа", typeof(double));
            dt.Columns.Add("Населення", typeof(long));
        }
        private async Task Load(string url)
        {
            Loading(false, "Заванаження");
            countries = await aPIClient.Request(url);
            if (countries != null)
            {
                ListToTable();
                UploadDataGrid();
            }
            else
                MessageBox.Show(aPIClient.ErorMessage, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Loading(true, "Робота");
        }
        private void ListToTable()
        {
            dt.Rows.Clear();
            foreach (Country country in countries)
                dt.Rows.Add(country.name.common, country.name.official, returnCapital(country.capital), country.region, country.area, country.population);
        }
        private string returnCapital(List<string> c)
        {
            if (c == null)
                return "Немає";
            string capitals = "";
            foreach (string captital in c)
                capitals += captital + " ";
            return capitals;
        }
        private void UploadDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
        }
        private void Loading(bool status, string text)
        {
            StatusLabel.Text = text;
            button1.Enabled = status;
            button3.Enabled = status;
            findBox.Enabled = status;
            dataGridView1.Enabled = status;
            if (!status)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            await Load("all?fields=name,capital,region,subregion,area,population,flags");
            findBox.Clear();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(findBox.Text))
                return;
            await Load($"name/{findBox.Text}");
            findBox.Clear();
        }
        private async Task Information(string name)
        {
            Loading(false, "Відображення");
            List<Country> c = await aPIClient.Request($"name/{name}");
            if (c == null)
            {
                MessageBox.Show($"Помилка: {aPIClient.ErorMessage}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Common.Text = c[0].name.common;
            Official.Text = c[0].name.official;
            Capital.Text = returnCapital(c[0].capital);
            Sub.Text = c[0].subregion;
            Region.Text = c[0].region;
            Area.Text = c[0].area.ToString();
            Population.Text = c[0].population.ToString();
            pictureBox1.LoadAsync(c[0].flags.png);
            Flag.Text = c[0].flags.alt;
            Loading(true, "Показ");
        }
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string cName = string.Empty;
            if (e.RowIndex < 0)
                return;
            cName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            await Information(cName);
        }
    }
}


