namespace HadisIelts.Client.Features.Payment.Models
{
    public class PaymentFileModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FileSuffix { get; set; }
        public string Data { get; set; }
        public DateTime DateTime { get; set; }
        public static string ViewImageData(string imageData)
        {
            return String.Format("data:image/gif;base64,{0}", imageData);
        }

    }
}
