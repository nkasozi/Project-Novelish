using eBdb.EpubReader;
using EbookReaderLib;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\DELL\Pictures\Epubs\The Drop - Michael Connelly.epub";//=@"C:\Users\DELL\Pictures\Epubs\Lord of the Rings - 01 - The Fellowship of the Ring - J. R. R. Tolkien.epub";
            string pwd="";
            string outFolder=@"C:\Users\DELL\Pictures\Epubs\Output";
            SomaEpub epub = new SomaEpub(fileName);
            
        }

       

        
    }
}
