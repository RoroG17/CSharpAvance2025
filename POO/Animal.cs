using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    public class Animal
    {
        private string nom;
        private int age;

        public Animal(string nom, int age)
        {
            this.nom = nom;
            this.age = age;
        }

        public string afficherInfo()
        {
            return $"{nom} ({age} ans)";
        }

    }
}
