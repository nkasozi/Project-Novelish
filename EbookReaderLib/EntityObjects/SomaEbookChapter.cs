using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookReaderLib
{
    public class SomaEpubChapter
    {
        public string EbooKChapterId { get; set; }
        public string Title { get; set; }
        public string ChapterContent { get; set; }
        public string FullFileName { get; set; }
        public int IndexOfChapter { get; set; }

        public SomaEpubChapter(string filePath, int indexOfChapter)
        {
            FullFileName = filePath;
            IndexOfChapter = indexOfChapter;
            ReadChapterContent(filePath);
        }

        private void ReadChapterContent(string filePath)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.Load(filePath, Encoding.UTF8);

                string bodyText = doc.DocumentNode.SelectSingleNode("//body").InnerHtml;
                string sanitizedHtml = bodyText.Replace("<image", "<img").Replace("xlink:href", "src").Replace("</image>", "</img>");
                ChapterContent = sanitizedHtml;
                Title = "";
            }
            catch (Exception ex)
            {

            }
        }

        private string GetImageName(string imageNameString)
        {
            string result = "";
            try
            {
                string[] part = imageNameString.Split('/');
                result = part[(part.Length - 1)];
            }
            catch (Exception)
            {
                result = imageNameString;
            }
            return result;
        }


    }

}
