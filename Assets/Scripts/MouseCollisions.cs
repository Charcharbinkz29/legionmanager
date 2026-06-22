using UnityEngine;

public class MouseCollisions : MonoBehaviour
{
    public GameObject selectedSoldierObject;
    public SoldierView currentlyHovered = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hoverhit))
        {

            SoldierView hoverView = hoverhit.collider.GetComponent<SoldierView>();
           

            if (hoverView != null)
            {
                if (hoverView != currentlyHovered)
                {
                    if (currentlyHovered != null) currentlyHovered.SetHovered(false);
                    hoverView.SetHovered(true);
                    currentlyHovered = hoverView;
                }
            }


        } else
        {
            if (currentlyHovered != null) { currentlyHovered.SetHovered(false); }
            currentlyHovered = null;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"{BattleManager.selectedSoldier}");
                SoldierView view = hit.collider.GetComponent<SoldierView>();
                if (view != null)
                {
                    Debug.Log($"{view.soldier.name}");
                    BattleManager.selectedSoldier = view.soldier;
                    selectedSoldierObject = hit.collider.gameObject;
                }

                TileView tView = hit.collider.GetComponent<TileView>();
                if (tView != null)
                {
                    Debug.Log($"{tView.position.x} {tView.position.y}");
                    if (BattleManager.selectedSoldier != null)
                    {
                        BattleManager.selectedSoldier.position = tView.position;
                        selectedSoldierObject.transform.position = new Vector3(tView.position.x, (float)0.5, tView.position.y);
                    }
                }
            }
            else
            {
                BattleManager.selectedSoldier = null;
                selectedSoldierObject = null;
            }
        }
    }
}
