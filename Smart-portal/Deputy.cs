using System;

namespace SmartPortal.Core
{
    // Клас депутата — представляє члена ради
    public class Deputy
    {
        private string id;
        private string name;
        private string faction;     // фракція
        private string quote;       // відома фраза

        public string Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Faction
        {
            get { return faction; }
        }

        public string Quote
        {
            get { return quote; }
        }

        public Deputy(string id, string name, string faction, string quote)
        {
            this.id = id;
            this.name = name;
            this.faction = faction;
            this.quote = quote;
        }

        public override string ToString()
        {
            return $"{Name} | {Faction} | \"{Quote}\"";
        }
    }
}