using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    TowerControll towerObject;
    [SerializeField]
    Sprite dragSprite;
    [SerializeField]
    int towerPrice;

    public TowerControll TowerObject
    {
        get
        {
            return towerObject; 
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return dragSprite;
        }
    }

    public int TowerPrice
    {
        get
        {
            return towerPrice;
        }
    }
}
