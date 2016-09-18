using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceCodeBL
{
    [Serializable]
    public class ProgramCode
    {
        string filepath;

        public string Filepath
        {
            get { return filepath; }
            set { filepath = value; }
        }
        string progCode;

        public string ProgCode
        {
            get { return progCode; }
            set { progCode = value; }
        }
        string programId;

        public string ProgramId
        {
            get { return programId; }
            set { programId = value; }
        }
        string langId;

        public string LangId
        {
            get { return langId; }
            set { langId = value; }
        }
        string langName;

        public string LangName
        {
            get { return langName; }
            set { langName = value; }
        }
        string progName;

        public string ProgName
        {
            get { return progName; }
            set { progName = value; }
        }
        string lastModified;

        public string LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }
        string createdDate;

        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public ProgramCode Clone()
        {
            ProgramCode copyCode = new ProgramCode();
            copyCode.createdDate = this.createdDate;
            copyCode.filepath = this.filepath;
            copyCode.langId = this.langId;
            copyCode.langName = this.langName;
            copyCode.lastModified = this.lastModified;
            copyCode.progCode = this.progCode;
            copyCode.progName = this.progName;
            copyCode.programId = this.programId;
            return copyCode;
        }
    }
}
