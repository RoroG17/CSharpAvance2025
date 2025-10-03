// See https://aka.ms/new-console-template for more information

using POO;

Carnivore lion1 = new Carnivore("Simba", 5, "Viande", 3);
Carnivore lion2 = new Carnivore("Nala", 4, "Viande", 2);

Vegetarien girafe1 = new Vegetarien("Melman", 7, "Feuilles");
Vegetarien girafe2 = new Vegetarien("Geoffrey", 6, "Feuilles");
Vegetarien girafe3 = new Vegetarien("Sophie", 8, "Feuilles");

Enclos enclosCarnivores = new Enclos(1, "savane");
enclosCarnivores.AjouterAnimal(lion1);
enclosCarnivores.AjouterAnimal(lion2);

Enclos enclosHerbivores = new Enclos(2, "forêt tropical");
enclosHerbivores.AjouterAnimal(girafe1);
enclosHerbivores.AjouterAnimal(girafe2);
enclosHerbivores.AjouterAnimal(girafe3);

Zoo zoo = new Zoo("Mon Zoo");
zoo.AjouterEnclos(enclosCarnivores);
zoo.AjouterEnclos(enclosHerbivores);

zoo.AfficherEnclos();
