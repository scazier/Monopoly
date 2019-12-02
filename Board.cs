using System;

public class Board
{
    public List<Case> cases;
    public List<Joueur> joueurs;
    /* affichage temporraire
    public void afficher()
    {
        int j = 40 ;
        for(int i=0;i<j;i++)
        {
            foreach(Joueur player in joueurs)
            {
                if(player.Position==i)
                {
                    Console.Write("O");
                }

            }Console.Write("|  ");
        }
    }*/
    // initalisation
    public void initialisation()
    {
        joueurs = new List<Joueur> { };
        cases = new List<Case> { };
    }

    private static Board board;
    private Board() { }
    public static Board getInstance()
    {
        if (plateau == null)
        {
            board = new Board();
        }
        return board;
    }


}
