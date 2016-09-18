using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SourceCodeBL
{
    public class Tab
    {
        string name = string.Empty;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        List<ControlId> controlId = null;

        public List<ControlId> ControlId
        {
            get { return controlId; }
            set { controlId = value; }
        }

       
        public Tab()
        {
            controlId = new List<ControlId>();
        }
        public void Add(ControlId controlId)
        {
            this.controlId.Add(controlId);
        }
    }
    public class ControlId
    {
        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        bool invisible;

        public bool Invisible
        {
            get { return invisible; }
            set { invisible = value; }
        }
        bool disabled;

        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }
        public ControlId(string ID, bool invisible, bool disabled)
        {
            this.id = ID;
            this.invisible = invisible;
            this.disabled = disabled;
        }
    }
}
