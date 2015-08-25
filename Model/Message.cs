using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model
{
    public class Message
    {
        private bool _flag;
        private String _msg;

        public bool flag
        {
            set { _flag = value; }
            get { return _flag; }
        }

        public String msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
    }
}
