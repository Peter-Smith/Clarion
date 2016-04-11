using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Clarion;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
        static private Combat c = new Combat();
        private const String systemMessageName = "SYS";


        public void turn()
        {
            c.turn();
            Clients.All.updateFates(c.rising());
            Clients.All.broadcastMessage(Squawks.Mfates(c.fate(),c.rising() ));
            // TODO wake up non-stunned characters by calling c

            // TODO squawk the new turn
        }

        public void send(string message)
        {
            // Call the broadcastMessage method to update clients.

            Clients.All.broadcastMessage(message);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            foreach (Combatant cmb in c.allCombatants) {
                Clients.Caller.addCombatant(cmb.id, cmb.name, cmb.areaID);
                Clients.Caller.setFates(c.fate(),c.rising());
            }
            return base.OnConnected();
        }

        public void addCombatant(String name, int area) {
            Clients.All.broadcastMessage("New Combatant: "+name);
            Combatant cmb = c.NewCombatant(name, area);
            Clients.All.addCombatant(cmb.id, name, cmb.areaID);
        }

        public void removeCombatant(String id) {
            c.removeCombatant(id);
            Clients.Others.deleteById(id);
        }

        public void move(String id, String area){
            c.moveCombatant(id, area);
            Clients.Others.moveById(id, area);
        }

        public void fetchStats(String id) {
            Combatant cmb = c.getCombatant(id);
            if (cmb != null) {
                Clients.Caller.updateStats(cmb.life, cmb.hp, cmb.mp, cmb.bravery);
            }
        }

        public void pushStats(String id, int life, int hp, int mp, int bravery) {
            Combatant cmb = c.getCombatant(id);
            if (cmb != null) {
                cmb.life = life;
                cmb.hp = hp;
                cmb.mp = mp;
                cmb.bravery = bravery;
                Clients.All.broadcastMessage(cmb.updateSquawk());
            }
        }

    }
}