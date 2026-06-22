using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Soldier;

public class Combatant
{
    public string name;
    public int age;
    public int attack;
    public int defense;
    public int health;
    public int speed;
    public Rank rank;
    public GridPos position;

}


public class Soldier : Combatant
{

    public enum Rank
    {
        Legionarius,
        Signifer,
        Velixarius,
        Legatus,
        Evocatus
    }

    public static Dictionary<Rank, int> xpProgression = new Dictionary<Rank, int>
    {
        {Rank.Legionarius, 0},
        {Rank.Signifer, 10},
        {Rank.Velixarius, 30},
        {Rank.Legatus, 50}
    };

    public static string[] names = { "Marcus", "Brutus", "Lucius", "Titus", "Cassius" };
    
    public int xp;
    public List<FabulaEntry> fabula = new List<FabulaEntry>();
    public Boolean deployed = false;


    public Soldier(int xp = 0)
    {
        name = names[UnityEngine.Random.Range(0, names.Length)];
        age = UnityEngine.Random.Range(18, 40);
        attack = UnityEngine.Random.Range(1, 10);
        defense = UnityEngine.Random.Range(1, 10);
        health = UnityEngine.Random.Range(5, 10);
        speed = UnityEngine.Random.Range(1, 3);
        rank = Rank.Legionarius;
        fabula = new List<FabulaEntry>();
        this.xp = xp;
    }

    public static Soldier createEvocatus()
    {
        Soldier s = new Soldier();
        s.rank = Rank.Evocatus;
        s.attack += 5;
        s.defense += 5;
        s.health += 3;
        s.age = UnityEngine.Random.Range(40, 50);

        return s;

    }

    public static Boolean checkPromotion(Soldier s)
    {
        if (s.rank == Rank.Evocatus)
        {
            return false;
        }

        foreach (KeyValuePair<Rank, int> pair in xpProgression)
        {
            if (pair.Key > s.rank && s.xp >= pair.Value)
            {
                s.rank = pair.Key;
                return true;
            }
        }
        return false;
    }

    public static void addFabulaEntry(Soldier s, FabulaEntry f)
    {
        s.fabula.Add(f);
    }
}

public class Enemy : Combatant
{
    public enum Type
    {
        Barbarian,
        Chief
    }

    Type type;

    public Enemy()
    {
        attack = UnityEngine.Random.Range(1, 10);
        defense = UnityEngine.Random.Range(1, 10);
        health = UnityEngine.Random.Range(5, 10);
        speed = UnityEngine.Random.Range(1, 3);
        type = Type.Barbarian;
    }

    
}
