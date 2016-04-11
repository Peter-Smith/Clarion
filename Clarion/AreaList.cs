using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clarion
{
    public class AreaList
    {
        List<AreaTag>[] areas;

        public AreaList() { 
        areas = new List<AreaTag>[5];
        for (int i = 0; i < 5; i++) {
            areas[i] = new List<AreaTag>();

        }

        }

    }
}