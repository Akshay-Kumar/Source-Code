using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Logger;
using System.Data;
using MarlaLibraries.Cryptography;

namespace SourceCode
{
    public class FileBackUp
    {
        ArrayList lstFilesFound = null;
        ArrayList lstDirectoriesFound = null;
        static string rootDirectory = string.Empty;

        public static string RootDirectory
        {
            get 
            {
                if (string.IsNullOrEmpty(rootDirectory))
                {
                    DirectoryInfo dir = new DirectoryInfo(DIRECTORY.GetApplicationPath());
                    if (!dir.Exists)
                        dir.Create();
                    rootDirectory = dir.FullName;
                }
                return rootDirectory;
            }
            set 
            {
                DirectoryInfo dir = new DirectoryInfo(value);
                if(!dir.Exists)
                    dir.Create();
                rootDirectory = dir.FullName; 
            }
        }
        ClsEncryption encryptor = null;
        public FileBackUp()
        {
            lstFilesFound = new ArrayList();
            encryptor = new ClsEncryption();
            lstDirectoriesFound = new ArrayList();
        }

        public ArrayList GetDirectory(string sDir, bool clearList)
        {
            try
            {
                if (clearList)
                    lstDirectoriesFound.Clear();
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    lstDirectoriesFound.Add(d);
                }
            }
            catch (Exception excpt)
            {
                ErrorLog.ErrorRoutine(excpt);
            }
            return lstDirectoriesFound;
        }

        public ArrayList GetFiles(string sDir, bool clearList)
        {
            try
            {
                if (clearList)
                    lstFilesFound.Clear();
                foreach (string f in Directory.GetFiles(sDir))
                {
                    lstFilesFound.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        lstFilesFound.Add(f);
                    }
                    GetFiles(d, false);
                }
            } 
            catch (Exception excpt)
            {
                ErrorLog.ErrorRoutine(excpt);
            }
            return lstFilesFound;
        }

        public ArrayList DirSearch(string sDir, string txtFile,bool clearList)
        {
            try
            {
                if (clearList)
                    lstFilesFound.Clear();
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, txtFile))
                    {
                        lstFilesFound.Add(f);
                    }
                    DirSearch(d,txtFile,false);
                }
            }
            catch (Exception excpt)
            {
                ErrorLog.ErrorRoutine(excpt);
            }
            return lstFilesFound;
        }

        public bool CreateDirectory(string subDirectory)
        {
            string subdir = string.Empty;
            try
            {
                // If directory does not exist, create it. 
                if (!Directory.Exists(rootDirectory))
                {
                    Directory.CreateDirectory(rootDirectory);
                }
                // Create sub directory
                subdir = rootDirectory + "\\" + subDirectory;

                if (!Directory.Exists(subdir))
                {
                    Directory.CreateDirectory(subdir);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return Directory.Exists(subdir);
        }

        public bool CreateFile(string fileName,string subDirectory,string text,out string filePath)
        {
            FileStream fs = null;
            string path = rootDirectory + "\\" + subDirectory + "\\" + fileName;
            filePath = string.Empty;
            try
            {
                if (!File.Exists(path) && text != string.Empty)
                {
                    fs = File.Create(path);
                    WriteToFile(fs,path,text);
                    filePath = path;
                }
                else if (text != string.Empty)
                {
                    if (File.Exists(path))
                    {
                        WriteToFile(path,text);
                        filePath = path;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return File.Exists(filePath);
        }
        /// <summary>
        /// Serializes an object into a physical file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subDirectory"></param>
        /// <param name="code"></param>
        /// <param name="filePath"></param>
        /// <returns>bool</returns>
        public bool SerializeFile(string fileName, string subDirectory,SourceCodeBL.ProgramCode code, out string filePath)
        {
            string path = rootDirectory + "\\" + subDirectory + "\\" + fileName;
            SourceCodeBL.ProgramCode encCode = code.Clone();
            encCode.ProgCode = encryptor.Encrypt(encCode.ProgCode.ToString());
            filePath = string.Empty;
            try
            {
                if (encCode.ProgCode != string.Empty)
                {
                    FileSerializer.Serialize(path, encCode);
                    filePath = path;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return File.Exists(filePath);
        }
        /// <summary>
        /// Deserializes a physical file into an object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string</returns>
        public string DeSerializeFile(string filePath)
        {
            SourceCodeBL.ProgramCode decCode = null;
            string fileText = string.Empty;
            try
            {
                if (File.Exists(filePath))
                {
                    decCode = FileSerializer.Deserialize(filePath);
                }
                decCode.ProgCode = encryptor.Decrypt(decCode.ProgCode.ToString());
                fileText = decCode.ProgCode;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return fileText;
        }
        public bool WriteToFile(string filePath, string fileText)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(fileText);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return File.Exists(filePath);
        }

        public bool WriteToFile(FileStream fs, string filePath, string fileText)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(fileText);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return File.Exists(filePath);
        }
        // Read From a Text File
        public string ReadFile(string path)
        {
            StreamReader sr = null;
            string fileText = string.Empty;
            try
            {
                if (File.Exists(path))
                {
                    sr = new StreamReader(path);
                    fileText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            finally
            {
                sr.Close();
            }
            return fileText;
        }
        // Replace a Text File
        public bool ReplaceFile(string fileName, string directory, string text, out string filePath)
        {
            filePath = string.Empty;
            try
            {
                // If file already exists in destination, delete it.   
                ArrayList lst = DirSearch(directory, fileName, true);
                if (lst.Count > 0)
                {
                    foreach (string f in lst)
                    {
                        filePath = f;
                    }
                }
                else
                {
                    return false;
                }
                File.Delete(filePath);
                CreateFile(fileName, directory, text, out filePath);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return File.Exists(filePath);
        }
    }
    #region Directory
    public class DIRECTORY
    {
        public const string icons = "icons";
        public static string GetApplicationPath()
        {
            string strAppPath = string.Empty;
            try
            {
                string strBaseDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                int nFirstSlashPos = strBaseDirectory.LastIndexOf("\\");
                string strTemp = string.Empty;

                if (0 < nFirstSlashPos)
                    strTemp = strBaseDirectory.Substring(0, nFirstSlashPos);

                int nSecondSlashPos = strTemp.LastIndexOf("\\");
                string strTempAppPath = string.Empty;
                if (0 < nSecondSlashPos)
                    strTempAppPath = strTemp.Substring(0, nSecondSlashPos);

                strAppPath = strTempAppPath.Replace("bin", "");
            }
            catch (Exception)
            {
                return string.Empty;
            }
            DirectoryInfo dInfo = new DirectoryInfo(strAppPath + "DATA");
            if (!dInfo.Exists)
                dInfo.Create();
            return dInfo.FullName;
        }
        public static string GetLoggerFilePath()
        {
            if (ErrorLog.LogFilePath == string.Empty)
            {
                DirectoryInfo dInfo = new DirectoryInfo(GetApplicationPath().Replace("DATA", "bin\\Debug\\log"));
                if (!dInfo.Exists)
                    dInfo.Create();
                return dInfo.FullName;
            }
            else
            {
                return ErrorLog.LogFilePath;
            }
        }
        public static string GetConfigFilePath()
        {
            DirectoryInfo dInfo = new DirectoryInfo(GetApplicationPath().Replace("DATA", "bin\\Debug\\config"));
            if (!dInfo.Exists)
                dInfo.Create();
            return dInfo.FullName;
        }
        public static string GetConfigFileName()
        {
            if(!File.Exists(GetConfigFilePath() + "\\" + "config.ini"))
                File.Create(GetConfigFilePath() + "\\" + "config.ini").Close();
            return GetConfigFilePath() + "\\" + "config.ini";
        }
    }
    #endregion
}
