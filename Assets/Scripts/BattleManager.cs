using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject soldierPrefab;
    public static Soldier selectedSoldier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createGrid(7,18);
        Debug.Log("BATTLE START");
        undeployArmy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createGrid(int gx, int gy)
    {
        for (int j = 0; j < gy * 1; j+=1)
        {
            for (int i = 0; i < gx * 1; i+=1)
            {
                GameObject t = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                t.GetComponent<TileView>().position = new GridPos(i, j);
            }
        }
    }

    public void deploySolider(Soldier s, TileView t)
    {
        GameObject sObj = Instantiate(soldierPrefab, gridToWorld(t.position), Quaternion.identity);
        sObj.GetComponent<SoldierView>().soldier = s;
        s.deployed = true;
    }

    public void undeployArmy()
    {
        int x = -4;
        int y = 0;
        int z = 0;

        foreach (Soldier s in GameManager.legion)
        {
            GameObject sObj = Instantiate(soldierPrefab, new Vector3(x, y, z), Quaternion.identity);
            sObj.GetComponent<SoldierView>().soldier = s;
            s.deployed = false;
            z++;
        }
    }



    public static Vector3 gridToWorld(GridPos p) => new Vector3(p.x, 1/5, p.y);
}
