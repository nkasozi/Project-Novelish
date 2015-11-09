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

        public SomaEpubChapter(string filePath,int indexOfChapter) 
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
                ChapterContent = bodyText;
                Title = "";
            }
            catch (Exception ex) 
            {
            
            }
        }


    }

}
