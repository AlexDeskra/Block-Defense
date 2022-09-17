using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    protected int Length; // Spaces on grid
    protected int Width; // Spaces on grid
    protected string Name;

    public Entity()
    {
        Length = 0;
        Width = 0;
        Name = "";
    }

    public Entity(int length, int width, string name)
    {
        Length = length;
        Width = width;
        Name = name;
    }
    public string getName() { return Name; }


}
