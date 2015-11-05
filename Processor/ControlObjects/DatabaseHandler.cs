using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor.ControlObjects
{
    public class DatabaseHandler
    {

        private Database SYSTEM_DB;
        //private Database INTERNETDB;
        private DbCommand db_command;


        public DatabaseHandler()
        {
            try
            {

                SYSTEM_DB = DatabaseFactory.CreateDatabase("TestConnectionString");

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public string SaveEbook(Ebook ebook)
        {
            try
            {
                db_command = SYSTEM_DB.GetStoredProcCommand("SaveEbook", ebook.EbookId, ebook.AppUserId, ebook.Title, ebook.Author, ebook.FilePath);
                DataTable dt=SYSTEM_DB.ExecuteDataSet(db_command).Tables[0];
                return dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }


        internal DataTable GetAllEbooks()
        {
            try
            {
                db_command = SYSTEM_DB.GetStoredProcCommand("GetAllEbooks");
                DataTable dt = SYSTEM_DB.ExecuteDataSet(db_command).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal DataTable GetAllAppUsers()
        {
            try
            {
                db_command = SYSTEM_DB.GetStoredProcCommand("GetAllAppUsers");
                DataTable dt = SYSTEM_DB.ExecuteDataSet(db_command).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                throw ;
            }
        }

        internal string SaveAppUser(AppUser appUser)
        {
            try
            {
                db_command = SYSTEM_DB.GetStoredProcCommand("SaveAppUser",appUser.AppUserId,appUser.AppUserPassword,appUser.Name,"");
                DataTable dt = SYSTEM_DB.ExecuteDataSet(db_command).Tables[0];
                return dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                throw ;
            }
        }
    }
}
