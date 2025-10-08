Exercice - Optimisation traitement d'image

Ce programme permet de tester 2 types d'algorithme : 

	- Algorithme séquenciel
	- Algorithme optimiser en utilisant de l'asynchronisme et du parallèlisme
	
Ce programme va dans un premier temps télécharger 10 images sur le site Lorem Picsum et les stocker dans le projet.
Ensuite, pour chaque image, il va les redimensionner en 3 résolution différentes (1080p, 720p, 480p) et stocker les images.
Pour tester les deux algorithme, le code a été excuté une dizaine de fois

Résultats :

===============================================================
| Essai |  Code non optimisé (ms)  |  Code optimisé (ms)       |
===============================================================
|   1   |          6147            |          1002             |
|   2   |          5255            |          1047             |
|   3   |          5757            |           938             |
|   4   |          5796            |           836             |
|   5   |          5821            |           887             |
|   6   |          5422            |           911             |
|   7   |          5652            |           890             |
|   8   |          5562            |           797             |
|   9   |          5538            |           801             |
|  10   |          5587            |          1017             |
---------------------------------------------------------------
| Moyenne         |      5653,7 ms       |       912,6 ms      |
| Gain de perf.   |      ≈ 6,2× plus rapide                    |
===============================================================


Conclusion :

Les résultats montrent une amélioration des performances grâce à l’optimisation du code.
En moyenne, le code optimisé s’exécute en 912,6 ms, contre 5653,7 ms pour la version non optimisée.
Cela représente un gain de vitesse d’environ 6,2 fois.