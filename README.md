# Monopoly

This game is a final project of a design pattern course.

* About the game
* Design Pattern view
* Run

### About the game

This project is based on this model of Monopoly:
![](http://www.monopolypedia.fr/editions/france/classique2014/monopoly-classique-plateau.jpg)

The result shown to the user is the following:
![](output.png)

Every player is represented by a pawn whith an emoji which is easier to see on the terminal.
On every round the user can see which player he is and the amount of money he have. 
The bought properties will be displayed on the screen with the number of houses and if there is a hotel on it.

### Design Pattern view

This project contain 3 different design patterns.
* Factory
* Singleton
* Mode Viewer Controller (MVC)

The factory pattern is used to create each box of the game. With this pattern, we no longer need to create new instances of a class, just call a function that getCase () from the class CaseFactory by specifying the type of box we want. The function enters the necessary parameters according to this type.

The singleton pattern is used for the banker and the deck of community and chance cards. With this one, we create only one instance of the class because there is no more than one banker or deck of cards.  We call a function to create the banker or the deck and if it is already created it return the existing one.

The MVC pattern is classical. There is 3 classes, the model which contains the players and the board. The controller to affect any changes on the instance model. The viewer which display the changes of the game to the user. 

### Run
To run the game just exectue the following line:
```
dotnet run
```

But before running the game launch a test to know if the code works with:
```
dotnet run --test
```
A verbose mode is available with the option -v
```
dotnet run --test --v
```
