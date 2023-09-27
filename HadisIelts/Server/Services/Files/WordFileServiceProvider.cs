using DocumentFormat.OpenXml.Packaging;
using System.Xml;

namespace HadisIelts.Server.Services.Files
{
    public class WordFileServiceProvider : IWordFileServices
    {

        public int CountFileWords(string data)
        {
            byte[] byteData = Convert.FromBase64String(data);
            int wordCount = 0;
            XmlDocument xmlDoc = new XmlDocument();
            using (var docxMs = new MemoryStream(byteData))
            {
                using (var wordDoc = WordprocessingDocument.Open(docxMs, true))
                {
                    var body = wordDoc.MainDocumentPart.Document.InnerXml;
                    xmlDoc.LoadXml(body);

                    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                    namespaceManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

                    XmlNode bodyNode = xmlDoc.SelectSingleNode("//w:body", namespaceManager);
                    if (bodyNode != null)
                    {
                        bool isAnswerFound = false;
                        XmlNodeList paragraphNodes = bodyNode.SelectNodes(".//w:p", namespaceManager);
                        foreach (XmlNode paragraphNode in paragraphNodes)
                        {
                            string paragraphText = paragraphNode.InnerText.Trim();
                            string[] words = paragraphText.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            if (isAnswerFound)
                            {
                                wordCount += words.Length;
                            }
                            else
                            {
                                int answerIndex = Array.IndexOf(words, "#Answer:");
                                if (answerIndex != -1)
                                {
                                    wordCount += words.Length - (answerIndex + 1);
                                    isAnswerFound = true;
                                }
                            }
                        }
                    }
                    return wordCount;
                }
            }
        }
    }
}

