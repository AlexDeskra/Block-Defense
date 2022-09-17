using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    public bool Pierce;

    public ArrowTower(ArrowTowerScriptableObject Obj) : base(Obj)
    {
        Pierce = Obj.Pierce;
    }

}
