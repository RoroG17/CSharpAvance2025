using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO
{
    public class Zoo
    {
        private string nom;

        // Composition : Un zoo contient plusieurs enclos
        public List<Enclos> enclos;

        public Zoo(string nom)
        {
            this.nom = nom;
            this.enclos = new List<Enclos>();
        }

        public void AjouterEnclos(Enclos e)
        {
            this.enclos.Add(e);
        }

        public void AfficherEnclos()
        {
            Console.WriteLine($"Enclos dans le zoo {nom}:");
            foreach (var e in enclos)
            {
                Console.WriteLine($"Enclos {e.GetType().Name} de type {e.type} avec {e.animaux.Count} animaux : \n {e.AfficherAnimaux()}");
            }
        }

    }
}
