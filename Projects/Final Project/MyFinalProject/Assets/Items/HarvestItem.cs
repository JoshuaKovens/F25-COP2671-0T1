using UnityEngine;

public class HarvestItem : MonoBehaviour
{
    [Header("Item Data")]
    public ItemData itemData;  
    public int quantity;       

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Debug.Log($"Picked up {itemData.itemName} x{quantity}");

           

            Destroy(gameObject); 
        }
    }
}
