using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EbookReaderLib
{
    public class SomaEpub
    {
        public string EbookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string PathToCoverImage { get; set; }

        private string Password { get; set; }

        public List<SomaEpubChapter> Chapters = new List<SomaEpubChapter>();

        public SomaEpub(string pathToEpub)
        {
            if (!pathToEpub.ToUpper().Contains(".EPUB"))
            {
                throw new Exception("INVALID FILE FORMAT. PLEASE GIVE PATH TO VALID EPUB");
            }

            FilePath = pathToEpub;
            FileExt = Path.GetExtension(FilePath);
            FileName = Path.GetFileNameWithoutExtension(FilePath);
            string FileDirectory = Path.GetDirectoryName(FilePath);
            Password = "";
            string TimeNow = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff");
            string outPutFilePath = FileDirectory + @"\" + FileName + @"_Unzipped_" + TimeNow + @"\";
            bool success = ExtractZipFile(FilePath, Password, outPutFilePath);

            if (success)
            {
                Chapters = ReadManifest(outPutFilePath);
                Chapters = OrderChapters(outPutFilePath, Chapters);
                PathToCoverImage = GetCoverImagePath(outPutFilePath);
                Title = GetEbookTitle(outPutFilePath);
                Author = GetEbookAurthor(outPutFilePath);
                Publisher = GetEbookPublisher(outPutFilePath);
                //SaveEpubImages(outPutFilePath);
            }
            else
            {
                throw new Exception("UNABLE TO EXTRACT EPUB CONTENTS. FILE MAYBE CORRUPTED");
            }

        }

        private void SaveEpubImages(string outPutFilePath)
        {
            try
            {
                string[] ImagesDirectories = Directory.GetDirectories(outPutFilePath, "*", SearchOption.AllDirectories);
                foreach (string ImagesDir in ImagesDirectories)
                {
                    if (ImagesDir.ToUpper().Contains("IMAGE"))
                    {
                        string[] imagesFiles = Directory.GetFiles(ImagesDir);
                        foreach (string image in imagesFiles)
                        {
                            Bitmap bitmap = new Bitmap(image);
                            string imageFileName = Path.GetFileName(image);
                            string savePath = System.Web.HttpContext.Current.Server.MapPath("~/Images/" + imageFileName);
                            bitmap.Save(savePath);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
            
            }
        }

        private string GetEbookTitle(string outPutFilePath)
        {
            string Title = "";
            try
            {
                //for epub..on extraction of zip file
                //there is a file called content.opf that has a manifest that describes location of content
                string[] filteredFiles = Directory.GetFiles(outPutFilePath, "*.opf", SearchOption.AllDirectories);

                if (filteredFiles.Length == 0)
                {
                    throw new Exception("NO EPUB MANIFEST AND SPINE FOUND. INVALID EPUB FORMAT. EPUB MAYBE CORRUPTED");
                }

                string manifestFileName = filteredFiles[0];

                //initialize reader to read manifest xml
                XmlReader reader = XmlReader.Create(manifestFileName);

                //go to manifest tag
                bool success = reader.ReadToFollowing("dc:title");

                //if its found
                if (success)
                {
                    Title = reader.ReadElementContentAsString();
                }

            }
            catch (Exception ex)
            {

            }
            return Title;
        }

        private string GetEbookAurthor(string outPutFilePath)
        {
            string Title = "";
            try
            {
                //for epub..on extraction of zip file
                //there is a file called content.opf that has a manifest that describes location of content
                string[] filteredFiles = Directory.GetFiles(outPutFilePath, "*.opf", SearchOption.AllDirectories);

                if (filteredFiles.Length == 0)
                {
                    throw new Exception("NO EPUB MANIFEST AND SPINE FOUND. INVALID EPUB FORMAT. EPUB MAYBE CORRUPTED");
                }

                string manifestFileName = filteredFiles[0];

                //initialize reader to read manifest xml
                XmlReader reader = XmlReader.Create(manifestFileName);

                //go to manifest tag
                bool success = reader.ReadToFollowing("dc:creator");

                //if its found
                if (success)
                {
                    Title = reader.ReadElementContentAsString();
                }

            }
            catch (Exception ex)
            {

            }
            return Title;
        }

        private string GetEbookPublisher(string outPutFilePath)
        {
            string Title = "";
            try
            {
                //for epub..on extraction of zip file
                //there is a file called content.opf that has a manifest that describes location of content
                string[] filteredFiles = Directory.GetFiles(outPutFilePath, "*.opf", SearchOption.AllDirectories);

                if (filteredFiles.Length == 0)
                {
                    throw new Exception("NO EPUB MANIFEST AND SPINE FOUND. INVALID EPUB FORMAT. EPUB MAYBE CORRUPTED");
                }

                string manifestFileName = filteredFiles[0];

                //initialize reader to read manifest xml
                XmlReader reader = XmlReader.Create(manifestFileName);

                //go to manifest tag
                bool success = reader.ReadToFollowing("dc:publisher");

                //if its found
                if (success)
                {
                    Title = reader.ReadElementContentAsString();
                }

            }
            catch (Exception ex)
            {

            }
            return Title;
        }


        private string GetCoverImagePath(string outPutFilePath)
        {
            string filepath = "";
            try
            {
                string[] filteredFiles = Directory.GetFiles(outPutFilePath, "cover.*", SearchOption.AllDirectories);
                if (filteredFiles.Length <= 0)
                {
                    filepath = "";
                    return filepath;
                }
                else
                {
                    filepath = filteredFiles[0];
                    return filepath;
                }
            }
            catch (Exception ex)
            {
                return filepath;
            }

        }

        private List<SomaEpubChapter> ReadManifest(string outPutFilePath)
        {
            List<SomaEpubChapter> allChapters = new List<SomaEpubChapter>();

            try
            {
                //for epub..on extraction of zip file
                //there is a file called content.opf that has a manifest that describes location of content
                string[] filteredFiles = Directory.GetFiles(outPutFilePath, "*.opf", SearchOption.AllDirectories);

                if (filteredFiles.Length == 0)
                {
                    throw new Exception("NO EPUB MANIFEST AND SPINE FOUND. INVALID EPUB FORMAT. EPUB MAYBE CORRUPTED");
                }

                string manifestFileName = filteredFiles[0];

                //initialize reader to read manifest xml
                XmlReader reader = XmlReader.Create(manifestFileName);

                //go to manifest tag
                bool success = reader.ReadToFollowing("manifest");

                //if its found
                if (success)
                {
                    int count = 1;
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "item") && (reader.HasAttributes))
                        {
                            //string FilePath to chapter
                            string fileName = reader.GetAttribute("href").Replace("/", @"\");

                            if (fileName.Contains(".html") || fileName.Contains(".xhtml"))
                            {
                                fileName = outPutFilePath + fileName;

                                //create chapter in Ebook
                                SomaEpubChapter chapter = new SomaEpubChapter(fileName, count);
                                chapter.Title = reader.GetAttribute("id");
                                allChapters.Add(chapter);

                                //increment chapter count
                                count++;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception("UNABLE TO READ EPUB: " + ex.Message);
            }
            return allChapters;
        }

        private List<SomaEpubChapter> OrderChapters(string outPutFilePath, List<SomaEpubChapter> Chapters)
        {
            List<SomaEpubChapter> allChapters = new List<SomaEpubChapter>();

            try
            {
                //for epub..on extraction of zip file
                //there is a .opf  file that has a spine tag that describes order of the content
                string[] filteredFiles = Directory.GetFiles(outPutFilePath, "*.opf", SearchOption.AllDirectories);

                if (filteredFiles.Length == 0)
                {
                    throw new Exception("NO EPUB MANIFEST AND SPINE FOUND. INVALID EPUB FORMAT. EPUB MAYBE CORRUPTED");
                }

                string manifestFileName = filteredFiles[0];

                //initialize reader to read spine xml
                XmlReader reader = XmlReader.Create(manifestFileName);

                //go to spine tag
                bool success = reader.ReadToFollowing("spine");

                //if its found
                if (success)
                {
                    int count = 1;
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "itemref") && (reader.HasAttributes))
                        {
                            //get id of chapter
                            string chapterId = reader.GetAttribute("idref");

                            //find that chapter from all the chapters
                            SomaEpubChapter chapter = Chapters.Find(i => i.Title == chapterId);

                            //if found
                            if (chapter != null)
                            {
                                //give thatchapter its right location in the ebook
                                chapter.IndexOfChapter = count;
                                allChapters.Add(chapter);
                                count++;
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UNABLE TO ORDER CHAPTERS IN EPUB: " + ex.Message);
            }
            return allChapters;
        }




        public bool ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    if (File.Exists(fullZipToPath))
                    {
                        FileStream streamWriter = new FileStream(fullZipToPath, FileMode.Open, FileAccess.Read);
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                    else
                    {
                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                    zipStream.Close();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }





    }
}
