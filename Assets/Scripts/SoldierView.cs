using System;
using UnityEngine;

public class SoldierView : MonoBehaviour
{
    public Soldier soldier;
    public Renderer rend;
    private Color originalColor;
    public Color hoverColor = Color.yellow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHovered(bool isHovered)
    {
        rend.material.color = isHovered ? hoverColor : originalColor;
    }
}
