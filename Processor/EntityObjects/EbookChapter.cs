using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EbookChapter
/// </summary>
public class EbookChapter
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string EbooKChapterId { get; set; }
    public string Title { get; set; }
    public string ChapterContent { get; set; }

	public EbookChapter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}