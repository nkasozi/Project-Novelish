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
            //string fileName = @"C:\Users\DELL\Pictures\Epubs\The Drop - Michael Connelly.epub";//=@"C:\Users\DELL\Pictures\Epubs\Lord of the Rings - 01 - The Fellowship of the Ring - J. R. R. Tolkien.epub";
            //string pwd="";
            //string outFolder=@"C:\Users\DELL\Pictures\Epubs\Output";
            //SomaEpub epub = new SomaEpub(fileName);
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
           
            ServiceSoapClient client=new ServiceSoapClient();
           
            Result result = client.ExecuteInsert(constring,"SaveEbook",param);
            DataTable dt = client.ExecuteSelect(constring,"GetAllEbooks",param);
            //client.c

            
        }




    }
}
