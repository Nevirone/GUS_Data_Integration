using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Projekt
{
    public partial class Form1 : Form
    {
        Data graphData;
        string token;

        const int FILE_TYPE_XML = 0;
        const int FILE_TYPE_JSON = 1;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async Task loadData()
        {
            const string projectValueType = "1616464";

            // inflation, unemployment, monthly wage
            string[] types = { "217230", "60270", "64541" };

            string[] provinces = {
                "DOLNOŒL¥SKIE",
                "KUJAWSKO-POMORSKIE",
                "LUBELSKIE",
                "LUBUSKIE",
                "£ÓDZKIE",
                "MA£OPOLSKIE",
                "MAZOWIECKIE",
                "OPOLSKIE",
                "PODKARPACKIE",
                "PODLASKIE",
                "POMORSKIE",
                "ŒL¥SKIE",
                "ŒWIÊTOKRZYSKIE",
                "WARMIÑSKO-MAZURSKIE",
                "WIELKOPOLSKIE",
                "ZACHODNIOPOMORSKIE"
            };

            int provinceId = province_input.SelectedIndex;
            int typeId = data_type_input.SelectedIndex;

            if (provinceId == -1)
            {
                MessageBox.Show("Nie wybrano województwa");
                return;
            }
            if (typeId == -1)
            {
                MessageBox.Show("Nie wybrano rodzaju danych");
                return;
            }

            string province = provinces[provinceId];
            string type = types[typeId];
            int[] years = { 2016, 2017, 2018, 2019, 2020, 2021, 2022 };

            GusDataResponse firstData = null, secondData = null;


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/api/gus/data");


                string data = "{ \"DataId\": \"1616464\", \"ProvinceName\": \"" + province + "\", \"Years\": [";
                data += string.Join(", ", years);
                data += "]}";

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    firstData = JsonConvert.DeserializeObject<GusDataResponse>(responseContent);
                }
                else
                {
                    // Request failed
                    Console.WriteLine("Error: " + response.ToString());
                }
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/api/gus/data");

                string data = "{ \"DataId\": \"" + type + "\", \"ProvinceName\": \"" + province + "\", \"Years\": [";
                data += string.Join(", ", years);
                data += "]}";

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    secondData = JsonConvert.DeserializeObject<GusDataResponse>(responseContent);
                }
                else
                {
                    // Request failed
                    Console.WriteLine("Error: " + response.ToString());
                }
            }

            if (firstData != null && secondData != null)
            {
                graphData = new Data(firstData.ProvinceName, "Wartoœæ projektów europejskich (MLN)",
                    data_type_input.Text, firstData.Values.Select(n => n / 1000000.0).ToArray(), secondData.Values, years);
            }
        }


        private async void load_data_button_click(object sender, EventArgs e)
        {
            if (token == null)
            {
                MessageBox.Show("Musisz siê zalogowaæ");
                return;
            }
            await loadData();

            data_info.Text = graphData.GetInfo();
            MessageBox.Show("Dane za³adowane");
        }
        private void generate_button_click(object sender, EventArgs e)
        {
            if (graphData == null)
            {
                MessageBox.Show("Brak danych");
                return;
            }

            GraphManager.GenerateGraph(graphData.FirstValueName, graphData.SecondValueName, graphData.FirstValues, graphData.SecondValues, graphData.Years, graph_box);
        }

        private void export_button_click(object sender, EventArgs e)
        {

            string filename = file_name_input.Text;
            if (filename == null || filename.Length == 0)
            {
                MessageBox.Show("Pusta nazwa pliku");
                return;
            }

            int file_type = file_format_input.SelectedIndex;
            if (file_type == -1)
            {
                MessageBox.Show("Nie wybrano formatu pliku");
                return;
            }

            if (graphData == null)
            {
                MessageBox.Show("Brak danych do eksportu");
                return;
            }

            if (file_type == FILE_TYPE_JSON)
                graphData.SerializeToJson(filename + ".json");
            else if (file_type == FILE_TYPE_XML)
                graphData.SerializeToXml(filename + ".xml");
            MessageBox.Show("Dane wyeksportowane");
        }

        private void import_button_click(object sender, EventArgs args)
        {
            string filename = file_name_input.Text;
            if (filename == null || filename.Length == 0)
            {
                MessageBox.Show("Pusta nazwa pliku");
                return;
            }

            int file_type = file_format_input.SelectedIndex;
            if (file_type == -1)
            {
                MessageBox.Show("Nie wybrano formatu pliku");
                return;
            }

            if (file_type == FILE_TYPE_JSON)
            {
                if (!File.Exists(filename + ".json"))
                    MessageBox.Show("Plik o podanej nazwie nie istnieje");
                else
                {
                    graphData = Data.DeserializeFromJson(filename + ".json");
                    MessageBox.Show("Dane zaimportowane");
                }
            }
            else if (file_type == FILE_TYPE_XML)
            {
                if (!File.Exists(filename + ".xml"))
                    MessageBox.Show("Plik o podanej nazwie nie istnieje");
                else
                {
                    graphData = Data.DeserializeFromXml(filename + ".xml");
                    MessageBox.Show("Dane zaimportowane");
                }
            }

        }
        private async void register_button_click(object sender, EventArgs e)
        {
            if (!inputValid())
                return;
            string email = email_input.Text;
            string password = password_input.Text;

            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/api/auth/register");

                string data = "{ \"Email\": \"" + email + "\", \"Password\": \"" + password + "\"}";

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Konto utworzone");
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        MessageBox.Show("Z³e dane");
                    else
                    {
                        MessageBox.Show("B³¹d");
                        Console.WriteLine("Error: " + response.ToString());
                    }
                }
            }
        }

        private async void login_button_click(object sender, EventArgs e)
        {
            if (!inputValid())
                return;
            string email = email_input.Text;
            string password = password_input.Text;

            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/api/auth/login");


                string data = "{ \"Email\": \"" + email + "\", \"Password\": \"" + password + "\"}";

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    token = await response.Content.ReadAsStringAsync();
                    user_info.Text = email;
                    MessageBox.Show("Zosta³eœ zalogowany");
                }
                else
                {
                    user_info.Text = "brak";
                    token = null;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        MessageBox.Show("Podane dane nie pasuj¹ do ¿adnego z u¿ytkowników");
                    else
                    {
                        MessageBox.Show("B³¹d");
                        Console.WriteLine("Error: " + response.ToString());
                    }
                }
            }
        }

        private bool inputValid()
        {
            string email = email_input.Text;
            string password = password_input.Text;

            if (email.Length == 0)
            {
                MessageBox.Show("Email is empty");
                return false;
            }
            if (password.Length == 0)
            {
                MessageBox.Show("Password is empty");
                return false;
            }
            if (!validEmail(email))
            {
                MessageBox.Show("Email is not valid");
                return false;
            }
            return true;
        }
        private static bool validEmail(string email)
        {
            var valid = true;
            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void database_save_button_click(object sender, EventArgs e)
        {
            if (token == null)
            {
                MessageBox.Show("Musisz siê zalogowaæ");
                return;
            }

            if (graphData == null)
                MessageBox.Show("Dane s¹ puste");
            else
                await graphData.saveToDatabase(token);
        }

        private async void database_read_button_click(object sender, EventArgs e)
        {
            if (token == null)
            {
                MessageBox.Show("Musisz siê zalogowaæ");
                return;
            }

            int? id = null;
            try
            {
                id = int.Parse(data_id.Text);
            }
            catch
            {
                MessageBox.Show("Id musi byæ liczb¹");
            }
            if (id == null) { }
            else if (id < 0)
                MessageBox.Show("Podaj poprawne id");
            else
            {
                try
                {
                    graphData = new Data();
                    await graphData.readFromDatabase(token, id.Value);
                    data_info.Text = graphData.GetInfo();
                }
                catch (Exception ex)
                {
                    graphData = null;
                    data_info.Text = "puste";
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void email_input_TextChanged(object sender, EventArgs e)
        {

        }
    }
}