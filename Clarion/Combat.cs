using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clarion
{
    public class Combat
    {
        private Fate f;
        public List<Combatant> allCombatants;

        private int turns;
        private int id_tally;

        public int fate() { return f.fate; }
        public int rising() { return f.rising; }

        public void turn() {
            f.Turn();
            turns++;
        }

        public Combat() {
            f = new Fate();
            turns = 0;
            id_tally = 0;
            allCombatants = new List<Combatant>();
            NewCombatant("New Pawn", 2);
            NewCombatant("New Pawn", 3);
            NewCombatant("New Pawn", 4);
        }

        public Combatant NewCombatant(String name, int area){
            Combatant c;
            String id = (id_tally++).ToString();
            String areaId = "area" + area;
            c = new Combatant(name, id, areaId);
            allCombatants.Add(c);
            return c;
        }

        public Combatant getCombatant(String targetId) {
            Combatant cmb;
            cmb = allCombatants.Find(x => x.id.Equals(targetId));
            return cmb;
        }

        public void moveCombatant(string targetId, String area) {
            Combatant cmb = allCombatants.Find(x => x.id.Equals(targetId));
            Console.WriteLine(cmb.areaID+"-->"+area);
            cmb.areaID = area;
        }

        public void removeCombatant(String targetId) {
            Combatant cmb = allCombatants.Find(x => x.id.Equals(targetId));
            allCombatants.Remove(cmb);
        }
    }
}