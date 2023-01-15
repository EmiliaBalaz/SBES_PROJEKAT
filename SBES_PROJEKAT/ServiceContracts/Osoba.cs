using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public enum Tip { STUDENT, PROFESOR, PREDMET}

    [DataContract]
    public class Osoba
    {
        Tip tipOsobe;
        string naziv;
        string id;

        public Osoba(Tip tipOsobe, string naziv, string id)
        {
            this.TipOsobe = tipOsobe;
            this.Naziv = naziv;
            this.Id = id;
        }

        [DataMember]
        public Tip TipOsobe { get => tipOsobe; set => tipOsobe = value; }
        [DataMember]
        public string Naziv { get => naziv; set => naziv = value; }
        [DataMember]
        public string Id { get => id; set => id = value; }


        public override string ToString()
        {
            return $"\nOsoba:\nTIP:{TipOsobe}\nNAZIV:{Naziv}\nID: {Id}\n";
        }
    }
}
