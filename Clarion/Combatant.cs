using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clarion
{
    public class Combatant
    {
        public String name { get; set; }
        public String id { get; private set; }
        public String areaID {get; set;}
        public int hp, life, bravery, mp;
        // enforce uniqueness on ids, areaID is area1 - area5

        public Combatant(String name, String id, String areaID) {
            this.name = name;
            this.id = id;
            this.areaID = areaID;
            this.hp = 49;
            this.life = 14;
            this.bravery = 0;
            this.mp = 7;
        }

        public String updateSquawk() {
            return this.name + "- Life: " + life + " HP:" + this.hp + " MP: " + this.mp + " Bravery: " + this.bravery;
        }
    }
}