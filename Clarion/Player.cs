using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace Clarion
{
    public class Player
    {
        private const int HPMULTIPLIER = 7; // the value that describes the relation between Toughness and Max Health
        private const int LIFEMULTIPLIER = 2; // the value that describes the relation between Spirit and Life

        public string name;
        public string status;

        private int _Life, _Health, _Magic, _Bravery;

        public bool act;

        public int Bravery
        {
            get { return _Bravery; }
            set { _Bravery = Math.Max(value,0); }
        }

        public int Magic
        {
            get { return _Magic; }
            set { _Magic = Math.Min(spirit.val(),Math.Max(value,0));}
        }

        public int MaxHealth {
            get { return toughness.val() * HPMULTIPLIER; }
        }

        public int Health
        {
            get { return _Health; }
            set { _Health = Math.Min(MaxHealth, Math.Max(value, 0)); }
        }

        public int MaxLife
        {
            get { return spirit.val() * LIFEMULTIPLIER; }
        }

        public int Life
        {
            get { return _Life; }
            set { _Life = Math.Min(MaxLife, Math.Max(value, 0));}
        }

        public Attribute strength, dexterity, intellect, toughness, spirit, speed;

        public Player(string name) {
            this.name = name;
            strength = new Attribute();
            dexterity = new Attribute();
            intellect = new Attribute();
            toughness = new Attribute();
            spirit = new Attribute();
            speed = new Attribute();

            Life = spirit.val();
            Magic = spirit.val();
            Health = MaxHealth;
        }

        public String payload() {
            PlayerPayload pl = new PlayerPayload(this);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(pl);
            return json;

        }

        private class PlayerPayload {
            public string name;
            public int health, magic, life, strength, dexterity, intellect, toughness, spirit, speed;
            public string status;
            public bool act;
            public PlayerPayload(Player p) {
                name = p.name;
                status = p.status;
                act = p.act;
                health = p.Health; magic = p.Magic; life = p.Life;
                strength = p.strength.val(); dexterity = p.dexterity.val(); intellect = p.intellect.val(); toughness = p.toughness.val(); spirit = p.spirit.val(); speed = p.speed.val();


            }
        
        }
    }
}