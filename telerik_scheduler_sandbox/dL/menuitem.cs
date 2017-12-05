using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace telerik_scheduler_sandbox.dL
{
    public class menuitem
    {
        private string _text;
        private string _url;
        private int _id;
        private int? _parentId;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? ParentID
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public menuitem(int id, int? parentId, string text, string url)
        {
            _id = id;
            _parentId = parentId;
            _text = text;
            _url = url;
        }
    }
}