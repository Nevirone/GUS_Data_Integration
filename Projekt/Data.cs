using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

public class Data
{
    [JsonIgnore]
    private int _id;
    public int Length { get; set; }
    public string Province { get; set; }
    public string FirstValueName { get; set; }
    public string SecondValueName { get; set; }
    public double[] FirstValues { get; set; }
    public double[] SecondValues { get; set; }
    public int[] Years { get; set; }

    public Data()
    {
    }

    public Data(string province, string firstValueName, string secondValueName, double[] firstValues, double[] secondValues, int[] years)
    {
        if (firstValues.Length != secondValues.Length)
        {
            throw new ArgumentException("Unequal data array length");
        }

        Length = firstValues.Length;
        Province = province;
        FirstValueName = firstValueName;
        SecondValueName = secondValueName;
        FirstValues = firstValues;
        SecondValues = secondValues;
        Years = years;
    }

    public override string ToString()
    {
        string output = FirstValueName + ", " + SecondValueName + "\n";
        for (int i = 0; i < Length; i++)
        {
            output += FirstValues[i] + ", " + SecondValues[i] + " - " + Years[i] + "\n";
        }
        return output;
    }

    public string GetInfo()
    {
        return FirstValueName + " - " + SecondValueName + " dla " + Province;
    }

    public async Task saveToDatabase(string token)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/api/data");

            var jsonData = new
            {
                Length = Length,
                Province = Province,
                FirstValueName = FirstValueName,
                SecondValueName = SecondValueName,
                FirstValues = JsonSerializer.Serialize(FirstValues),
                SecondValues = JsonSerializer.Serialize(SecondValues),
                Years = JsonSerializer.Serialize(Years)
            };

            var data = JsonSerializer.Serialize(jsonData);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
                JsonElement root = jsonDocument.RootElement;

                _id = root.GetProperty("id").GetInt32();
                MessageBox.Show("Data saved to database with id: " + _id.ToString());
            }
            else
            {
                // Request failed
                Console.WriteLine("Error: " + response.ToString());
                Console.WriteLine(data);
            }
        }
    }

    public async Task readFromDatabase(string token, int id)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/data/" + id.ToString());

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
                JsonElement root = jsonDocument.RootElement;

                _id = root.GetProperty("id").GetInt32();
                Length = root.GetProperty("length").GetInt32();
                Province = root.GetProperty("province").GetString();
                FirstValueName = root.GetProperty("firstValueName").GetString();
                SecondValueName = root.GetProperty("secondValueName").GetString();
                FirstValues = JsonSerializer.Deserialize<double[]>(root.GetProperty("firstValues").GetString());
                SecondValues = JsonSerializer.Deserialize<double[]>(root.GetProperty("secondValues").GetString());
                Years = JsonSerializer.Deserialize<int[]>(root.GetProperty("years").GetString());

                MessageBox.Show("Data read to database with id: " + _id.ToString());
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    MessageBox.Show("Nie znaleziono danych o podanym id");
                else
                {
                    MessageBox.Show("Błąd wczytywania danych");
                    Console.WriteLine("Error: " + response.ToString());
                }
            }
        }
    }

    public void SerializeToJson(string filename)
    {
        string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, jsonString);
    }

    public static Data DeserializeFromJson(string filename)
    {
        string jsonString = File.ReadAllText(filename);
        return JsonSerializer.Deserialize<Data>(jsonString);
    }

    public void SerializeToXml(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Data));

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;  // Enable indentation

        using (XmlWriter writer = XmlWriter.Create(filename, settings))
        {
            serializer.Serialize(writer, this);
        }
    }

    public static Data DeserializeFromXml(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        using (StreamReader reader = new StreamReader(filename))
        {
            return (Data)serializer.Deserialize(reader);
        }
    }
}
