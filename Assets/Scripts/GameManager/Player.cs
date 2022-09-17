using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int Gold;
    private int Lives;
    private Manager ManagerEntity;
    public Player()
    {
        Gold = 50;
        Lives = 10;
        ManagerEntity = GameObject.Find("GameManager").GetComponent<Manager>();
    }
    public Player(int gold, int lives)
    {
        Gold = gold;
        Lives = lives;
        ManagerEntity = GameObject.Find("GameManager").GetComponent<Manager>();
    }
    public bool DecreaseLives(int Amount)
    {
        if (Lives >= Amount)
        {
            Lives -= Amount;
            return true;
        }
        return false;
    }
    public void AddGold(int gold)
    {
        Gold += gold;
    }
    public bool SubtractGold(int gold)
    {
        if (Gold >= gold)
        {
            Gold -= gold;
            return true;
        }
        return false;
    }
    public int getLives() { return Lives; }
    public int getGold() { return Gold; }

}
