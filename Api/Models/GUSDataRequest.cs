using Org.BouncyCastle.Math;

namespace Api.Models
{
    public class GUSDataRequest
    {
        public string DataId { get; set; }
        public string ProvinceName { get; set; }
        public int[] Years { get; set; }
    }
}
