using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject battleManagerPrefab;
    public static List<Soldier> legion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject bmObj = Instantiate(battleManagerPrefab);
        BattleManager bm = bmObj.GetComponent<BattleManager>();

        legion = createStarterArmy();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<Soldier> createStarterArmy()
    {
        List<Soldier> army = new List<Soldier>();

        for (int i = 0; i < 5; i ++)
        {
            army.Add(new Soldier());
        }

        army.Add(Soldier.createEvocatus());

        return army;
    }


}