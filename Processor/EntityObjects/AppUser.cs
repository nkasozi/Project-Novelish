using Processor.ControlObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AppUser
/// </summary>
public class AppUser
{
    public string AppUserId { get; set; }
    public string AppUserPassword { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string StatusDescription { get; set; }


    public AppUser()
    {
        AppUserId = "-1";
        AppUserPassword = "";
        Name = "";
        Email = "";
    }

    public void Save()
    {
        DatabaseHandler dh = new DatabaseHandler();
        string userId = dh.SaveAppUser(this);
        AppUserId = userId;
        this.StatusDescription = "SUCCESS!! Your account has been created";
    }

    public bool UserCredentialsAreValid()
    {
        AppUser dbUser = GetAll().Find(AppUser => ((AppUser.AppUserId == this.AppUserId) && (AppUser.AppUserPassword == AppUserPassword)));
        if (dbUser != null)
        {
            this.Name = dbUser.Name;
            this.StatusDescription = "SUCCESS";
            return true;
        }
        else
        {
            StatusDescription = "Invalid Username or Password";
            return false;
        }
    }

    public List<AppUser> GetAll()
    {
        List<AppUser> allUsers = new List<AppUser>();
        DatabaseHandler dh = new DatabaseHandler();
        DataTable dt = dh.GetAllAppUsers();
        foreach (DataRow dr in dt.Rows)
        {
            AppUser user = new AppUser();
            user.AppUserId = dr["AppUserID"].ToString();
            user.AppUserPassword = dr["Password"].ToString();
            user.Email = "";
            user.Name = dr["Name"].ToString();
            allUsers.Add(user);
        }
        return allUsers;
    }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(AppUserId)) 
        {
            StatusDescription = "PLEASE SUPPLY A VALID USERNAME";
            return false;
        }
        else if (string.IsNullOrEmpty(AppUserPassword)) 
        {
            StatusDescription = "PLEASE SUPPLY A VALID PASSWORD";
            return false;
        }
        else if (string.IsNullOrEmpty(Name))
        {
            StatusDescription = "PLEASE SUPPLY YOUR NAME";
            return false;
        }
        return true;
    }
}