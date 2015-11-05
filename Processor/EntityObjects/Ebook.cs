using eBdb.EpubReader;
using Processor.ControlObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using VersFx.Formats.Text.Epub;
using VersFx.Formats.Text.Epub.Entities;

/// <summary>
/// Summary description for Ebook
/// </summary>
public class Ebook
{
    public string AppUserId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string EbookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public string FilePath { get; set; }
    private int _currentChapterIndex;
    public string PathToCoverImage = @"F:\";
    public List<EbookChapter> Chapters = new List<EbookChapter>();

    public Ebook() 
    {
        AppUserId = "";
        EbookId = "-1";
        Title = "";
        Author = "";
        Publisher = "";
        FilePath = "";
        _currentChapterIndex = 0;
    }

    public int CurrentChapterIndex
    {
        get { return _currentChapterIndex; } 
        set {
              if (value < 0) //shouldnt drop below 0 or u get index out of range exception
                { _currentChapterIndex = 0; } 
              else
                { _currentChapterIndex = value; }
             }
    }


    public Ebook(string filePath)
    {
        AppUserId = "";
        EbookId = "-1";
        Title = "";
        Author = "";
        Publisher = "";
        FilePath = "";
        _currentChapterIndex = 1;
        this.FilePath = filePath;
        string fileExtension = Path.GetExtension(filePath);
        OpenEbookAndReadContent(fileExtension);
    }

    private void OpenEbookAndReadContent(string bookType) 
    {
        switch (bookType.ToUpper()) 
        {
            case ".EPUB":
                OpenEpub();
                break;
            default:
                break;
        }
    }

    private void OpenEpub()
    {
        EpubBook epub = EpubReader.OpenBook(FilePath);
        this.Title=epub.Title;
        this.Author = epub.Author;
        this.PathToCoverImage= Path.GetFileName(FilePath).Split('.')[0] + ".png";
        this.PathToCoverImage = PathToCoverImage.Replace(' ', '_').Replace('-', '_');
        this.PathToCoverImage = System.Web.HttpContext.Current.Server.MapPath("~/Images/" +PathToCoverImage);

        File.WriteAllText(PathToCoverImage, "");
        File.Delete(PathToCoverImage);

        Bitmap bmp = new Bitmap(epub.CoverImage);
        bmp.Save(this.PathToCoverImage);

        this.PathToCoverImage = Path.GetFileName(PathToCoverImage);
        //epub.CoverImage.Save(PathToCoverImage);
      

        foreach (EpubChapter chapter in epub.Chapters) 
        {
            EbookChapter chptr = new EbookChapter();
            chptr.Title = chapter.Title;
            chptr.ChapterContent = chapter.HtmlContent;
            this.Chapters.Add(chptr);
        }
    }

    public void Save() 
    {
        DatabaseHandler dh = new DatabaseHandler();
        string ebookId=dh.SaveEbook(this);
        this.EbookId = ebookId;
    }

    public static List<Ebook> GetAll() 
    {
        List<Ebook> allEbooks=new List<Ebook>();
        DatabaseHandler dh = new DatabaseHandler();
        DataTable dt = dh.GetAllEbooks();

        foreach (DataRow dr in dt.Rows) 
        {
            string filePath=dr["EbookFilePath"].ToString();
            Ebook ebook = new Ebook(filePath);
            ebook.AppUserId = dr["AppUserId"].ToString();
            allEbooks.Add(ebook);
        }
        return allEbooks;
    }

}