# GAMAGORA-Virtual_World-Project
Projet final du cours Mondes Virtuels de l'année 2024/2025. Les étudiants sont répartis en 4 groupes qui abordent différent aspect de la génération procédurale dans le monde du jeux vidéo. L'objectif est de créer une map complète à la GTA VI qui peut être utilisée dans un futur jeu. 2022.3.45f

---
## Génération de terrain :mount_fuji:
### Introduction
Génération procédurale de terrain en utilisant le bruit de perlin : [Ressources](https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3&index=1). Le terrain doit être le plus réalistique possible en laissant une zone plane pour la génération procédurale de ville.

### Contributeurs
- **Git master** : Arthur PRADIER
- **Developpers** : Arthur PRADIER - [Najib EL KHADIR](https://najibxy.github.io/) - [Enzo MARTINEZ](https://dev-portfolio-enzo-martinezs-projects.vercel.app/) - Evan DUBOIS
- **Supports** : Augustin GUYOT - Adjamé TELLIER-ROZEN

---
## Génération de routes :car:
### Introduction
Génération procédurale de routes en utilisant les algorithmes de plus court chemin (A* ou Dijsktra): [Ressources](https://www.youtube.com/watch?v=-L-WgKMFuhE). Générez à la fois des grands axes routiers et des chemins de montagnes. Les axes routiers auront comme contraintes de passer que sur des zones ou le terrain n'a pas une forte courbure. 

### Contributeurs
- **Git master** : Flavien GONIN
- **Developpers** : Flavien GONIN - Alex CLORENNEC - Marwan AIT ADDI - Adjamé TELLIER-ROZEN - Anis HAMMOUCHE

---
## Génération d'arbres :deciduous_tree:
### Introduction
Génération procédurale d'arbre en utilisant le L-system : [Ressources](https://www.youtube.com/watch?v=E1B4UoSQMFw&list=PLQu8TxiWYLxID80vTywVWjaloCyytE2gB&index=2). Proposez 3 types d'arbres différents pour ajouter de la diversité. Ces arbres devront être ensuite placés de manière aléatoire sur le terrain pour créer des forêts. 
### Contributeurs
- **Git master** : Augustin GUYOT
- **Developpers** : Augustin GUYOT - Enzo CAUSSE - Saïd AIT AHMAD - Doha BITAQA - Salma SABBAR
- **Supports** : [Najib EL KHADIR](https://najibxy.github.io/)
  
---
## Génération d'une ville :city_sunrise:
### Introduction
Génération procédurale d'une ville en utilisant le L-system : [Ressources](https://cgl.ethz.ch/Downloads/Publications/Papers/2001/p_Par01.pdf). Générez dans un premier temps les routes de votre ville puis les cadastres des bâtiments pour ensuite générer procéduralement le bâtiment.
### Contributeurs
- **Git master** : [Renaud MACHECOURT-BOURGEOIS](https://itschocapic.github.io/)
- **Developpers** : [Renaud MACHECOURT-BOURGEOIS](https://itschocapic.github.io/) - Anna SARBIEWSKI - Ivan ROCQUE - Reyane REDJEM
- **Supports** : Enzo MARTINEZ
Root Sentence : [X]-[X]-[X]-[X]
Rules :
X -> F[+X]F[-X]FX
F -> FF[+F][-F]
  -> F[+FF][-FF]
![alt text](https://github.com/corentinGaut/GAMAGORA-Virtual-World-project/blob/city/image.jpg?raw=true)
