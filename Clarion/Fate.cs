using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clarion
{
    public class Fate
    {
        private int _Fate;
        private int _Rising;

        public int fate {
            set { this._Fate = value;
            if (this._Fate < 0) { this._Fate = 0; }
            }
            get { return this._Fate; }

        }

        public int rising
        {
            set
            {
                this._Rising = value;
                if (this._Rising < 0) { this._Rising = 0; }
            }
            get { return this._Rising; }

        }

        private Random rand;
        public bool locked;

        public Fate() {
            rand = new Random();
            fate = rand.Next(1, 13);
            rising = rand.Next(1, 13);
        }

        public void Turn()
        {
            if (!locked)
            {
                fate = rising;
                rising = rand.Next(1, 13);
            }
            else { locked = false; }
        }

        public void Lock() {
            locked = true;
        }

    }
}