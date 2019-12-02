using System;

public class Case
{
  protected int id;
  protected String name;

  // public Case next { get; set; }

  public int ID
  {
      get { return this.id; }
      set { this.id = value; }
  }

  public String Name
  {
      get { return this.name; }
      set { this.name = value; }
  }

  public Case(int id, String name)
  {
      this.id = id;
      this.name = name;
      //this.next = null;
  }

  public virtual String toString()
  {
      String output = "Case [id: "+this.id+", name: "+this.name+"]";
      return output;
  }
}
