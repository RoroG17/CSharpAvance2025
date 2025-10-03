using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    public class Enclos
    {
        private int numero;
        private string type;

        // Composition : Un enclos contient plusieurs animaux
        public List<Animal> animaux;

        public Enclos(int numero, string type)
        {
            this.numero = numero;
            this.type = type;
            this.animaux = new List<Animal>();
        }

        public void AjouterAnimal(Animal a)
        {
            this.animaux.Add(a);
        }
        public string AfficherAnimaux()
        {
            string info = "";
            foreach (var animal in animaux)
            {
                info += $"\t - {animal.afficherInfo()} \n";
            }
            return info;
        }
    }
}
