using UnityEngine;

public class EnemyView : MonoBehaviour
{

    public Enemy enemy;
    public Renderer rend;
    public Color red = Color.red;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
