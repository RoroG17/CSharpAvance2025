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
|   1   |          2068            |           362             |
|   2   |          1107            |           340             |
|   3   |           996            |           346             |
|   4   |          1064            |           365             |
|   5   |           994            |           341             |
|   6   |           944            |           362             |
|   7   |          1041            |           355             |
|   8   |           960            |           381             |
|   9   |          1018            |           348             |
|  10   |          1003            |           330             |
---------------------------------------------------------------
| Moyenne         |      1119,5 ms       |       353 ms        |
| Gain de perf.   |      ≈ 3,1× plus rapide                    |
===============================================================


Conclusion :

Les résultats montrent une amélioration des performances grâce à l’optimisation du code.
En moyenne, le code optimisé s’exécute en 912,6 ms, contre 5653,7 ms pour la version non optimisée.
Cela représente un gain de vitesse d’environ 6,2 fois.