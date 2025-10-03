using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    // Héritage : Carnivore hérite de Animal
    public class Carnivore : Animal
    {
        private string regime;
        private int nbRepasParJour;

        public Carnivore(string nom, int age, string regime, int nbRepasParJour) : base(nom, age)
        {
            this.regime = regime;
            this.nbRepasParJour = nbRepasParJour;
        }
    }
}
