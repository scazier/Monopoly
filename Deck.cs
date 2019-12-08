using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Content
{
    private String request;
    private String action;

    public String Request
    {
        get { return this.request; }
        set { this.request = value; }
    }

    public String Action
    {
        get { return this.action; }
        set { this.action = value; }
    }

    public Content(String request, String action)
    {
        this.request = request;
        this.action = action;
    }

    public String toString()
    {
        String output = "[ Request: "+this.request+"\t => \tAction: "+this.action+"]";
        return output;
    }
}


public class Deck
{
    private static Deck _object;
    private Stack<Content> community;
    private Stack<Content> chance;

    public Stack<Content> Community
    {
        get { return this.community; }
        set { this.community = value; }
    }

    public Stack<Content> Chance
    {
        get { return this.chance; }
        set { this.chance = value; }
    }

    public int ID { get; set; }


    private static Deck ReadCards(Deck deck, bool readCommunity=false, bool readChance=false)
    {
        Stack<Content> communityStack = deck.community;
        Stack<Content> chanceStack = deck.chance;
        try
        {
            String[] lines = File.ReadAllLines(@"cards.txt");

            bool communityVal = false;
            bool chanceVal = false;

            String line;
            for (int id_line=0; id_line<lines.Length; id_line++)
            {
                line = lines[id_line];

                if (line.Equals("Community") && readCommunity == true) { communityVal = true; }
                if (line.Equals("Chance") && readChance == true) { chanceVal = true; communityVal = false; }
                if (!line[0].Equals('#') && line[0].Equals('\t'))
                {
                    if (line[0].Equals('\t') &&  communityVal)
                    {
                        String[] data = line.Split('|');
                        communityStack.Push(new Content(data[0],data[1]));
                    }
                    if (line[0].Equals('\t') &&  chanceVal)
                    {
                        String[] data = line.Split('|');
                        chanceStack.Push(new Content(data[0], data[1]));
                    }
                }
            }

            List<Content> communityList = new List<Content>(communityStack.ToArray());
            List<Content> chanceList = new List<Content>(chanceStack.ToArray());

            var rnd = new Random();
            communityList = communityList.OrderBy(item => rnd.Next()).ToList();
            chanceList = chanceList.OrderBy(item => rnd.Next()).ToList();

            int lowestLength = Math.Min(communityList.Count, chanceList.Count);
            int biggestLength = Math.Max(communityList.Count, chanceList.Count);
            for (int i=0; i<biggestLength; i++)
            {
                if (communityList.Count != biggestLength)
                {
                    if (i < lowestLength) { communityStack.Push(communityList[i]); }
                    chanceStack.Push(chanceList[i]);
                }
                else
                {
                    if (i < lowestLength) { chanceStack.Push(chanceList[i]); }
                    communityStack.Push(chanceList[i]);
                }

            }
            deck.chance = chanceStack;
            deck.community = communityStack;
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read: ");
            Console.WriteLine(e.Message);
        }
        return deck;
    }

    private Deck()
    {
        ID += 1;
        this.community = new Stack<Content>();
        this.chance = new Stack<Content>();

    }

    public static Deck ReadDeck()
    {
        if (_object == null)
        {
            _object = new Deck();
            _object = ReadCards(_object, true, true);
        }
        else
        {
            if (_object.community.Count == 0) { _object = ReadCards(_object, true, false); }
            if (_object.chance.Count == 0) { _object = ReadCards(_object, false, true); }

        }
        return _object;
    }

    public String toString()
    {
        String output = "Deck[\nCommunity cards:\n";

        foreach (Content obj in this.community)
        {
            output += "\t"+obj.toString()+"\n";
        }

        output += "\nChance cards:\n";

        foreach (Content obj in this.chance)
        {
            output += "\t"+obj.toString()+"\n";
        }

        return output;
    }

}
