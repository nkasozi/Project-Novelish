using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Tester
{

    public class ParsingPDF
    {

        static string PDF;
        static string TEXT2;

        /**
         * Parses the PDF using PRTokeniser
         * @param src  the path to the original PDF file
         * @param dest the path to the resulting text file
         */
        public string[] parsePdfToHtml(string src)
        {
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(src);
            //StreamWriter output = new StreamWriter();
            SimpleTextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
            List<string> text = new List<string>(); ;
            int pageCount = reader.NumberOfPages;
            for (int pg = 1; pg <= pageCount; pg++)
            {
                text.Add(PdfTextExtractor.GetTextFromPage(reader, pg, strategy));
            }
            return text.ToArray();
        }



        /**
         * Parses the PDF using PRTokeniser
         * @param src  the path to the original PDF file
         * @param dest the path to the resulting text file
         */
        public string[] ParsePdfToText(string src)
        {
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(src);
            //reader.t
            //StreamWriter output = new StreamWriter();
            LocationTextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
            List<string> text = new List<string>(); 
            int pageCount = reader.NumberOfPages;
            for (int pg = 1; pg <= pageCount; pg++)
            {
               
                string content=PdfTextExtractor.GetTextFromPage(reader, pg, strategy);
                text.Add(content);
            }
            return text.ToArray();
        }

        ///**
        // * Main method.
        // */
        //static void Main(string[] args)
        //{
        //    if (args.Length < 1 || args.Length > 2)
        //    {
        //        Console.WriteLine("USAGE: ParsePDF infile.pdf <outfile.txt>");
        //        return;
        //    }
        //    else if (args.Length == 1)
        //    {
        //        PDF = args[0];
        //        TEXT2 = System.IO.Path.GetFileNameWithoutExtension(PDF) + ".txt";
        //    }
        //    else
        //    {
        //        PDF = args[0];
        //        TEXT2 = args[1];
        //    }

        //    try
        //    {
        //        DateTime t1 = DateTime.Now;

        //        ParsingPDF example = new ParsingPDF();
        //        example.parsePdf(PDF, TEXT2);

        //        DateTime t2 = DateTime.Now;
        //        TimeSpan ts = t2 - t1;
        //        Console.WriteLine("Parsing completed in {0:0.00} seconds.", ts.TotalSeconds);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("ERROR: " + ex.Message);
        //    }
        //} // class

        public class MyTextRenderListener : IRenderListener
        {
            /** The print writer to which the information will be written. */
            protected StreamWriter output;

            /**
             * Creates a RenderListener that will look for text.
             */
            public MyTextRenderListener(StreamWriter output)
            {
                this.output = output;
            }

            public void BeginTextBlock()
            {
                output.Write("<");
            }

            public void EndTextBlock()
            {
                output.WriteLine(">");
            }

            public void RenderImage(ImageRenderInfo renderInfo)
            {
            }

            public void RenderText(TextRenderInfo renderInfo)
            {
                output.Write("<");
                output.Write(renderInfo.GetText());
                output.Write(">");
            }
        } // class
    } // namespace  
}

