using System.Text.Json;
using System.Xml.Serialization;

public class GUSDataResponse
{
    public int Length { get; set; }
    public string ProvinceName { get; set; }
    public double[] Values { get; set; }
    public int[] Years { get; set; }
}
