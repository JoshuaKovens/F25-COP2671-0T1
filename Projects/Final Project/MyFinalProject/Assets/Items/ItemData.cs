using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Farming/Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;      
    public Sprite icon;          
    public int startingQuantity = 1;  
}
