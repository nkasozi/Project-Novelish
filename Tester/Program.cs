using eBdb.EpubReader;
using EbookReaderLib;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.DBApi;

namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            //TestReadEpub();

            TestReadPdf();

            
        }

        private static void TestReadPdf()
        {
            DateTime t1 = DateTime.Now;

            ParsingPDF example = new ParsingPDF();
            string src = @"D:\Ebooks\Harry Potter\Book 1 - Harry Potter and the Sorce (208)\Book 1 - Harry Potter and the S - Harry Potter.pdf";
            string[] text = example.ParsePdfToText(src);

            DateTime t2 = DateTime.Now;
            TimeSpan ts = t2 - t1;
            Console.WriteLine("Parsing completed in {0:0.00} seconds.", ts.TotalSeconds);
        }

        private static void TestReadEpub()
        {
            string fileName = @"C:\Users\DELL\Pictures\Epubs\Lord of the Rings - 01 - The Fellowship of the Ring - J. R. R. Tolkien.epub";
            string pwd = "";
            string outFolder = @"C:\Users\DELL\Pictures\Epubs\Output";
            string imagesFolder = @"C:\Users\DELL\Documents\Visual Studio 2013\Projects\TestAspNet_1\TestAspNet\Images\";
            SomaEpub epub = new SomaEpub(fileName, imagesFolder);
            //"SaveEbook", ebook.EbookId, ebook.AppUserId, ebook.Title, ebook.Author, ebook.FilePath,ebook.CurrentChapterIndex
            //DBApi.ArrayOfAnyType array = new DBApi.ArrayOfAnyType();
            string[] param = { "-1", "1234", "Test", "Test", "Test", "12" };
            string constring = "TestConStriing";

            //array.AddRange(param);
            //DBApi.ServiceSoapClient req = new DBApi.ExecuteInsertRequest();
            //DBApi.ExecuteInsertRequestBody body=new DBApi.ExecuteInsertRequestBody();
            //body.Parameters=array;
            //body.storedProcedureName="SaveEbook";
            //req.Body = body;

            // ServiceSoapClient client=new ServiceSoapClient();

            //Result result = client.ExecuteInsert(constring,"SaveEbook",param);
            //DataTable dt = client.ExecuteSelect(constring,"GetAllEbooks",param);
            //client.c
        }




    }
}
