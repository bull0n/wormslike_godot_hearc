---
title: BArc
subtitle: Worms like
lang: fr
author:
- Bulloni Lucas <lucas.bulloni@he-arc.ch>
- Fleury Malik <malik.fleury@he-arc.ch>
date: \today
pagesize: A4
numbersections: true
documentclass: scrartcl
geometry: margin=2.5cm
header-includes: |
      \usepackage{fancyhdr}
      \pagestyle{fancy}
      \fancyhead[R]{Lucas Bulloni, Malik Fleury}
      \usepackage{float}
      \floatplacement{figure}{H}
---

# Description

BArc (Bomb Arc) est un jeu similaire à Worms. Deux équipe s'affrontent et doivent s'entruer. Chaque équipe comporte un certain nombre de personnages. Une fois que tous les personnages d'une équipe sont morts, la partie est terminée.
Les personnages on accès à 3 armes différentes

![Barc](./Barc.png "Barc"){width=50%}

# Fonctionnalités

- Trois armes différentes
- Destruction simpliste du terrain
- Gestion des dégâts sur les personnages
- Gestion de la mort (tombe)
- Gestion des tours
- Gestion de la caméra

## Points forts du projet

### Découpage de tous les éléments en scènes

Toute l'application est découpée en plusieurs scènes. Chaque scène comporte un élément du jeu, par exemple une arme ou l'effet d'explosion.
L'avantage à cela est d'avoir une structure de projet très propre et de pouvoir réutiliser facilement chaque élément du jeu en chargeant la scène correspondante.

### Chargement des éléments du jeu en mémoire

Avant le lancement d'une partie, le jeu charge toutes les scènes nécessaires afin de pas ressentir des ralentissements lorsque l'utilisateur joue.

### Trois armes avec différents comportement

Les armes du jeu comportent des comportements différents. Voici l'ensemble des armes et leur comportement :

- Les grenades : elles peuvent être lancées par le joueur et explose après un délai de quelques secondes.
- Le lance roquette : il peut lancer des roquettes faisant de gros dégâts lors de la collision avec les joueurs ou le terrain.
- Le fusil : il permet de tirer en ligne droite (léger effet de cloche avec la distance) avec des dégâts modérés sur les personnages.

### Apparition de tombes

Apparition des tombes lors de la mort des personnages. Les tombes sont affectées par la gravité mais pas par les collisions des personnages.

### Déplacement de la caméra

Il est possible de déplacer ainsi que d'effecuter un zoom sur la caméra afin d'avoir une meilleure vision sur le champs de bataille.

# Améliorations

- Régler les problèmes de ralentissements du terrain
  - Choisir une autre technique de gestion de destruction du terrain (marching cube, utilisations de masques)
- Appliquer des forces sur les personnages afin qu'ils éjectent lorsqu'ils sont proches d'une explosion
- Créer de grandes maps

# Outils utilisés

- Godot 3.1 (moteur de jeux open source)
- Blender 2.79 (logiciel de modélisation open source)
