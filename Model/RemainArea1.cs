﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model
{
    public class RemainArea1
    {
        private Decimal _consarea;
        private Decimal _agriarea;
        private Decimal _arabarea;

        public Decimal consArea
        {
            set { _consarea = value; }
            get { return _consarea; }
        }
        public Decimal agriArea
        {
            set { _agriarea = value; }
            get { return _agriarea; }
        }
        public Decimal arabArea
        {
            set { _arabarea = value; }
            get { return _arabarea; }
        }
    }
}
