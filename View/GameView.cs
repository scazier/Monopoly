﻿using System;

public class GameView
{
    public void displayGame()
    {
        String game =
        "_____________________________________________________________________________________________________________________________________________________________________________________________\n" +
        "|                |                |                |                |                |                |                |                |                |                |                 |\n" +
        "|      Parc      |     Avenue     |     Chance     |   Boulevard    |     Avenue     |     Gare du    |    Faubourg    |    Place de    | Compagnie des  |     Rue la     | Allez en Prison |\n" +
        "|     Gratuit    |    Matignon    |                |  MalesHerbes   |  Henri-Martin  |      Nord      |  Saint-Honoré  |    la Bourse   |      eaux      |     Fayette    |                 |\n" +
        "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n" +
        "|                |                |                |                |                |                |                |                |                |                |                 |\n" +
        "|                |                |                |                |                |                |                |                |                |                |                 |\n" +
        "|                |      220 €     |                |      220 €     |      240 €     |      200 €     |      260 €     |      260 €     |      150 €     |      280 €     |                 |\n" +
        "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n" +
        "|                |____________________________                                                                                              ______________________________|                 |\n" +
        "|                |        Place Pigalle       |                                                                                             |     Avenue de Breteuil      |                 |\n" +
        "|      200 €     |____________________________|                                                                                             |_____________________________|       300 €     |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |   Boulevard Saint-Michel   |                                                                                             |         Avenue Foch         |                 |\n" +
        "|      180 €     |____________________________|                                                                                             |_____________________________|       300 €     |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |     Caisse de communauté   |                                                                                             |     Caisse de communauté    |                 |\n" +
        "|                |____________________________|                                                                                             |_____________________________|                 |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |        Avenue Mozart       |                                                                                             |   Boulevard des capucines   |                 |\n" +
        "|      180 €     |____________________________|                                                                                             |_____________________________|       300 €     |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |          Gare Lyon         |                                                                                             |      Gare Saint-Lazare      |                 |\n" +
        "|      200 €     |____________________________|                                                                                             |_____________________________|       200 €     |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |       Rue de Paradis       |                                                                                             |           Chance            |                 |\n" +
        "|      160 €     |____________________________|                                                                                             |_____________________________|                 |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |      Avenue de Neuilly     |                                                                                             |  Avenue des Champs-Elysées  |                 |\n" +
        "|      140 €     |____________________________|                                                                                             |_____________________________|       350 €     |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |   Compagnie d'électricité  |                                                                                             |        Taxe de Luxe         |                 |\n" +
        "|      150 €     |____________________________|                                                                                             |_____________________________|       100 €     |\n" +
        "|________________|                                                                                                                                                        |_________________|\n" +
        "|                |____________________________                                                                                               _____________________________|                 |\n" +
        "|                |  Boulevard de la Villette  |                                                                                             |       Rue de la Paix        |                 |\n" +
        "|      140 €     |____________________________|                                                                                             |_____________________________|       400 €     |\n" +
        "|________________|________________________________________________________________________________________________________________________________________________________|_________________|\n" +
        "|                |                |                |                |                |                |                |                |                |                |                 |\n" +
        "|      Prison    |   Avenue de    |    Rue des     |     Chance     |     Rue de     |      Gare      |      Impôt     |       Rue      |     Caisse     |  Boulevard de  |      Départ     |\n" +
        "|                | la République  |   Courcelles   |                |    Vaugirard   |  Montparnasse  |  sur le revenu |     LeCourbe   |  de communauté |   BelleVille   |       <--       |\n" +
        "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n" +
        "|                |                |                |                |                |                |                |                |                |                |                 |\n" +
        "|                |                |                |                |                |                |                |                |                |                |                 |\n" +
        "|                |      120 €     |      100 €     |                |      100 €     |      200 €     |      200 €     |       60 €     |                |      60 €      |                 |\n" +
        "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n";
        Console.WriteLine(game);
        // String game =
        // "_______________________________________________________________________________________________________________________________________________________________________________________________________________________________________\n"+
        // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
        // "|        Parc        |       Avenue       |       Chance       |     Boulevard      |       Avenue       |       Gare du      |      Faubourg      |      Place de      |   Compagnie des    |       Rue la       |   Allez en Prison  |\n"+
        // "|       Gratuit      |      Matignon      |                    |    MalesHerbes     |    Henri-Martin    |        Nord        |    Saint-Honoré    |      la Bourse     |        eaux        |       Fayette      |                    |\n"+
        // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n"+
        // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
        // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
        // "|                    |        220 €       |                    |        220 €       |        240 €       |        200 €       |        260 €       |        260 €       |        150 €       |        280 €       |                    |\n"+
        // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n"+
        // "|                    |____________________________                                                                                                                                  ______________________________|                    |\n"+
        // "|                    |        Place Pigalle       |                                                                                                                                 |     Avenue de Breteuil      |                    |\n"+
        // "|        200 €       |____________________________|                                                                                                                                 |_____________________________|        300 €       |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |   Boulevard Saint-Michel   |                                                                                                                                 |         Avenue Foch         |                    |\n"+
        // "|        180 €       |____________________________|                                                                                                                                 |_____________________________|        300 €       |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |     Caisse de communauté   |                                                                                                                                 |     Caisse de communauté    |                    |\n"+
        // "|                    |____________________________|                                                                                                                                 |_____________________________|                    |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |        Avenue Mozart       |                                                                                                                                 |   Boulevard des capucines   |                    |\n"+
        // "|        180 €       |____________________________|                                                                                                                                 |_____________________________|        300 €       |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |          Gare Lyon         |                                                                                                                                 |      Gare Saint-Lazare      |                    |\n"+
        // "|        200 €       |____________________________|                                                                                                                                 |_____________________________|        200 €       |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |       Rue de Paradis       |                                                                                                                                 |           Chance            |                    |\n"+
        // "|        160 €       |____________________________|                                                                                                                                 |_____________________________|                    |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |      Avenue de Neuilly     |                                                                                                                                 |  Avenue des Champs-Elysées  |                    |\n"+
        // "|        140 €       |____________________________|                                                                                                                                 |_____________________________|        350 €       |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |   Compagnie d'électricité  |                                                                                                                                 |        Taxe de Luxe         |                    |\n"+
        // "|        150 €       |____________________________|                                                                                                                                 |_____________________________|        100 €       |\n"+
        // "|____________________|                                                                                                                                                                                            |____________________|\n"+
        // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
        // "|                    |  Boulevard de la Villette  |                                                                                                                                 |       Rue de la Paix        |                    |\n"+
        // "|        140 €       |____________________________|                                                                                                                                 |_____________________________|        400 €       |\n"+
        // "|____________________|____________________________________________________________________________________________________________________________________________________________________________________________|____________________|\n"+
        // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
        // "|        Prison      |     Avenue de      |      Rue des       |       Chance       |       Rue de       |        Gare        |        Impôt       |         Rue        |       Caisse       |    Boulevard de    |       Départ       |\n"+
        // "|                    |   la République    |     Courcelles     |                    |      Vaugirard     |    Montparnasse    |    sur le revenu   |       LeCourbe     |    de communauté   |     BelleVille     |         <--        |\n"+
        // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n"+
        // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
        // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
        // "|                    |        120 €       |        100 €       |                    |        100 €       |        200 €       |        200 €       |         60 €       |                    |         60 €       |                    |\n"+
        // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n";
        //
    }
}