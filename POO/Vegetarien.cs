using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    // Héritage : Vegetarien hérite de Animal
    public class Vegetarien : Animal
    {
        private string vegetal;
        public Vegetarien(string nom, int age, string vegetal) : base(nom, age)
        {
            this.vegetal = vegetal;
        }
    }
}
