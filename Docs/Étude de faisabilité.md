# Étude de faisabilité

## Introduction

Cette étude a pour but d'adresser le risque du framework inadapté. En effet, pour ce projet, nous souhaitons utiliser le moteur de jeu Unity. La crainte concernant ce moteur est qu'il ne permette pas de développer en orienté objet (OOP). 

## Unity

Unity est pas mal orienté sur l'utilisation des outils disponible dans l'UI et nous ne faisons que scripter le comportement des GameObjects. Même si nous avons déjà utilisé Unity pour faire des projets personnels, il s'avère que cet outil ne soit pas adapté pour les demandes du module (l'orienté objet). On peut faire du MVC et faire du code orienté objet (avec la fabrication des GameObjects via les scripts) mais cela rend le projet trop brouillon. L'avantage qu'a Unity est l'interface qui simplifie tout le côté UX car on a pas besoin de calculer tout ça, on a juste à déplacer les éléments à l'endroit souhaité. 

## Monogame

Si le projet sous Unity paraît impossible du point de vue du PO, nous pourrions dériver sur le framework MonoGame qui est un framework de jeu pour du C#. En effet, ce framework est utilisé pour développer des jeux et pour notre projet, ce framework convient parfaitement avec nos attentes et ceux du Scrum Master/Product Owner. La seule difficulté sera de se mettre dans le framework car nous ne l'avons jamais utilisé. La documentation paraît assez complète malgré que le site soit un peu brouillon et différents tutoriels sont disponible.

Pour avoir une idée de ce que Monogame est capable de faire, nous avons essayé de reproduire un [jeu test](https://docs.monogame.net/articles/getting_started/2_creating_a_new_project_vs.html) (afficher une balle et le faire se déplacer). Nous avons remarqué une certaine similitude avec le framework LibGDX.

## Conclusion

Faire un projet sur Unity en orienté objet peut devenir rapidement compliqué. Malgré nos différentes recherches, nous n'avons rien trouvé de concluant pour faire le tout en orienté objet. Pour éviter le risque du framework inadapté, notre équipe a fait le choix de partir sur le framework Monogame. Ce framework est comme le framework LibGDX que nous avons pu voir dans le module JAVA. Cependant, il y a moins de tutoriels que pour Unity et la documentation n'est pas si complète que ça.

