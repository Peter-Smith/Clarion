using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Represents one of the six attributes.

namespace Clarion
{
    public class Attribute
    {
        public int trueVal;
        public int boost;
        public int intact; // this is 0 when the stat is broken
        static Random r = new Random();

        public int val(){
            return Math.Max(((trueVal + boost) * intact),0);
        }

        public Attribute() {
            roll();
            boost = 0;
            intact = 1;
        }

        public void roll(){
            int a, b;
            a = r.Next(1,7);
            b = r.Next(1,7);
            trueVal = a + b;
        }

    }
}