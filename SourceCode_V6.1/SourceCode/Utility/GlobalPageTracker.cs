using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveLogViewer;

namespace SourceCode
{
    public class GlobalPageTracker
    {
        public static Login loginObj;
        public static SourceCode sourceCodeObj;
        public static UserOperation operationPageObj;
        public static RoleOperation roleOperationPageObj;
        public static ColorSyntax colorSyntexPageObj;
        public static MainForm loggerFormObj;
        public static SearchBox searchBoxObj;
        public static LogTabPage logTabPageObj;
        public static DiffMergeSample diffMergeMapPageObj;
        public static DocumentMapSample documentMapPageObj;
        public static MarkerToolSample markerToolPageObj;
    }
    public class USER_ICONS
    {
        public static string locked_user_icon = "Oxygen-Icons.org-Oxygen-Actions-document-encrypt.ico";
        public static string unlocked_user_icon = "Oxygen-Icons.org-Oxygen-Actions-document-decrypt.ico";
        public static string deleted_user_icon = "Gakuseisean-Ivista-2-Alarm-Error.ico";
    }
}
