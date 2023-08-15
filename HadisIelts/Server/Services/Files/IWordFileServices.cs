namespace HadisIelts.Server.Services.Files
{
    public interface IWordFileServices
    {
        public int CountFileWords(string data);
        public string ConvertDocxtoXml(string docxData);
    }
}
