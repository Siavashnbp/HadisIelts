using HadisIelts.Client.Features.Teacher.Models;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class ProcessedWritingFileModel
    {
        public WritingFileModel WritingFileModel { get; set; }
        public int WordCount { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string Message { get; set; }
        public ProcessedWritingFileModel()
        {
            WritingFileModel = new WritingFileModel();
        }
        public static List<ProcessedWritingFileModel> ConvertFilesToProcessedWritingFile(List<ProcessedWritingFile> files
            , List<WritingTypeModel> writingTypes)
        {
            int id = 0;
            var convertedFiles = new List<ProcessedWritingFileModel>();
            foreach (var item in files)
            {
                convertedFiles.Add(new ProcessedWritingFileModel
                {
                    PriceGroup = item.PriceGroup is not null ? new PriceGroup
                    {
                        Price = item.PriceGroup.Price,
                        PriceName = item.PriceGroup.PriceName
                    } : new PriceGroup(),
                    WordCount = item.WritingFile.WordCount ?? 0,
                    WritingFileModel = new WritingFileModel
                    {
                        Name = item.WritingFile.Name,
                        FileData = item.WritingFile.Data,
                        WritingType = writingTypes.FirstOrDefault(x => x.ID == item.WritingFile.WritingTypeID),
                        ID = id++,
                    },
                    Message = item.Message,
                });
            }
            return convertedFiles;
        }
    }
}
