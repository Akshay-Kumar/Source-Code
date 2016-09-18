using System;
using System.IO;
using System.Collections;

namespace SourceCode
{
	/// <summary>
	/// Summary description for SyntaxReader.
    /// </summary>
    #region SyntaxReader
    public class SyntaxReader
    {
        #region Declaration
        private string TheFile;
		private  ArrayList Keywords = new ArrayList();
        private ArrayList Functions = new ArrayList();
		private ArrayList ClassKeywords = new ArrayList();
		private ArrayList Comments = new ArrayList();
        private ArrayList String = new ArrayList();
        #endregion

        #region Methods
        public SyntaxReader(string file)
		{
			//
			// TODO: Add constructor logic here
			//
			FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);
			TheFile = sr.ReadToEnd();
			sr.Close();
			fs.Close();
			FillArrays();
		}

		public void FillArrays()
		{
			StringReader sr = new StringReader(TheFile);
			string nextLine;

			nextLine = sr.ReadLine();
			nextLine = nextLine.Trim();

			// find functions header
			while (nextLine != null)
			{

                if (nextLine == "[KEYWORDS]")
                {
                    // read all of the functions into the arraylist
                    nextLine = sr.ReadLine();
                    if (nextLine != null)
                        nextLine = nextLine.Trim();
                    while (nextLine != null && nextLine[0] != '[')
                    {
                        Keywords.Add(nextLine);

                        nextLine = "";
                        while (nextLine != null && nextLine == "")
                        {
                            nextLine = sr.ReadLine();
                            if (nextLine != null)
                                nextLine = nextLine.Trim();
                        }

                    }
                }

                if (nextLine == "[FUNCTIONS]")
                {
					// read all of the functions into the arraylist
					nextLine = sr.ReadLine();
					if (nextLine != null)
						nextLine = nextLine.Trim();
					while (nextLine != null && nextLine[0] != '[')
					{
						Functions.Add(nextLine);

						nextLine = "";
						while (nextLine != null && nextLine == "")
						{
							nextLine = sr.ReadLine();
							if (nextLine != null)
								nextLine = nextLine.Trim();
						}
					}
				}

                if (nextLine == "[COMMENTS]")
                {
                    // read all of the comments into the arraylist
                    nextLine = sr.ReadLine();
                    if (nextLine != null)
                        nextLine = nextLine.Trim();
                    while (nextLine != null && nextLine[0] != '[')
                    {
                        Comments.Add(nextLine);

                        nextLine = "";
                        while (nextLine != null && nextLine == "")
                        {
                            nextLine = sr.ReadLine();
                            if (nextLine != null)
                                nextLine = nextLine.Trim();
                        }

                    }
                }
                if (nextLine == "[STRING]")
                {
                    // read all of the functions into the arraylist
                    nextLine = sr.ReadLine();
                    if (nextLine != null)
                        nextLine = nextLine.Trim();
                    while (nextLine != null && nextLine[0] != '[')
                    {
                        String.Add(nextLine);

                        nextLine = "";
                        while (nextLine != null && nextLine == "")
                        {
                            nextLine = sr.ReadLine();
                            if (nextLine != null)
                                nextLine = nextLine.Trim();
                        }

                    }
                }
                if (nextLine == "[CLASSKEYWORDS]")
                {
                    // read all of the class keywords into the arraylist
                    nextLine = sr.ReadLine();
                    if (nextLine != null)
                        nextLine = nextLine.Trim();
                    while (nextLine != null && nextLine[0] != '[')
                    {
                        ClassKeywords.Add(nextLine);

                        nextLine = "";
                        while (nextLine != null && nextLine == "")
                        {
                            nextLine = sr.ReadLine();
                            if (nextLine != null)
                                nextLine = nextLine.Trim();
                        }

                    }
                }
				if (nextLine != null && nextLine.Length > 0 && nextLine[0] == '[')
				{
				}
				else
				{
					nextLine = sr.ReadLine();
					if (nextLine != null)
						nextLine = nextLine.Trim();
				}
			}

			Keywords.Sort();
            ClassKeywords.Sort();
			Functions.Sort();
			Comments.Sort();
            String.Sort();
		}

		public bool IsKeyword(string s)
		{
			int index = Keywords.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}

		public bool IsClassKeyword(string s)
		{
			int index = ClassKeywords.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}
        
        public bool IsString(string s)
        {
            int index = String.BinarySearch(s);
            if (index >= 0)
                return true;

            return false;
        }

        public bool IsFunction(string s)
        {
            int index = Functions.BinarySearch(s);
            if (index >= 0)
                return true;

            return false;
        }
        public bool IsComment(string s)
		{
			int index = Comments.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
        }
        #endregion
    }
    #endregion
}
