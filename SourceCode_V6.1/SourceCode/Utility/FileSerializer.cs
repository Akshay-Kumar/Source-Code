using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using SourceCodeBL;
using Logger;

namespace SourceCode
{

    /// <summary>
    /// Serializes and Deserializes sourcecode data
    /// to persist the data, and to recover and redisplay
    /// the data stored in a scd type file
    /// </summary>
    class FileSerializer
    {

        /// <summary>
        /// Use a binary formatter to save the sourcecode data
        /// to a custom file
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="myFile"></param>
        public static void Serialize(string strPath, ProgramCode myCode)
        {
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, myCode);
                fs.Close();
            }
            catch (SerializationException ex)
            {
                ErrorLog.ErrorRoutine(ex); 
            }
        }


        /// <summary>
        /// Deserialize an existing file back into a
        /// list of type SourceCode Data
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static ProgramCode Deserialize(string strPath)
        {
            FileStream fs = new FileStream(strPath, FileMode.Open);
            ProgramCode myCode = new ProgramCode();

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                myCode = ((ProgramCode)(formatter.Deserialize(fs)));
                fs.Close();
                return myCode;
            }
            catch (SerializationException ex)
            {
                ErrorLog.ErrorRoutine(ex);
                return myCode;
            }
        }


    }
}