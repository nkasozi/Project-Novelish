using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AppDbContext
/// </summary>
public class AppDbContext:DbContext
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Ebook> Ebooks { get; set; }

	public AppDbContext()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}