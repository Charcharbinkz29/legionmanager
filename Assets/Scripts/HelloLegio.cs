using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class HelloLegio : MonoBehaviour
{
    public string message = "";
    public int number = 0;
    public GameObject soldierPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        Debug.Log("Cheese");
        List<Soldier> soldiers = new List<Soldier>();
        soldiers.Add(new Soldier(10));
        soldiers.Add(new Soldier());
        soldiers.Add(Soldier.createEvocatus());

        Debug.Log($"{message}{number}");

        int r = 0;

        foreach (Soldier s in soldiers)
        {
            r++;
            Soldier.checkPromotion(s);

            if (s.rank == Soldier.Rank.Evocatus)
            {
                Soldier.addFabulaEntry(s, new FabulaEntry(0, "Led a cohort in the defense of Roma"));
            }

            Debug.Log($"{s.rank} {s.name} {s.age} {string.Join(',', s.fabula)}");

            GameObject obj = Instantiate(soldierPrefab, new Vector3(r * 2, 0, 0), Quaternion.identity);
            obj.GetComponent<SoldierView>().soldier = s;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
