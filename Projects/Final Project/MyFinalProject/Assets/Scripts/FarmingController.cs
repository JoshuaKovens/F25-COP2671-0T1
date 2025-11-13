using UnityEngine;

public class FarmingController : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float interactDistance = 1.5f;

    [Header("Input Keys")]
    public KeyCode tillKey = KeyCode.Alpha1;
    public KeyCode waterKey = KeyCode.Alpha2;
    public KeyCode plantKey = KeyCode.Alpha3;
    public KeyCode harvestKey = KeyCode.Alpha4;

    [Header("Seeds")]
    public SeedPacket currentSeed; 

    private CropBlock selectedBlock;

    void Update()
    {
        DetectCurrentBlock();
        HandleInput();
    }

    void DetectCurrentBlock()
    {
        // Turn off previous highlight
        if (selectedBlock != null)
        {
            selectedBlock.Highlight(false);
        }

        selectedBlock = null;

        // Find nearest CropBlock within interactDistance
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.position, interactDistance);
        float closestDist = float.MaxValue;

        foreach (var hit in hits)
        {
            CropBlock block = hit.GetComponent<CropBlock>();
            if (block != null)
            {
                float dist = Vector2.Distance(player.position, block.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    selectedBlock = block;
                }
            }
        }

        
        if (selectedBlock != null)
        {
            selectedBlock.Highlight(true);
        }
    }

    void HandleInput()
    {
        if (selectedBlock == null) return;

        if (Input.GetKeyDown(tillKey))
        {
            TillSelected();
        }
        else if (Input.GetKeyDown(waterKey))
        {
            WaterSelected();
        }
        else if (Input.GetKeyDown(plantKey))
        {
            PlantSelected();
        }
        else if (Input.GetKeyDown(harvestKey))
        {
            HarvestSelected();
        }
    }

    // --- Toolbar methods ---
    public void TillSelected()
    {
        if (selectedBlock != null)
            selectedBlock.PlowSoil();
    }

    public void WaterSelected()
    {
        if (selectedBlock != null)
            selectedBlock.WaterSoil();
    }

    public void PlantSelected()
    {
        if (selectedBlock != null && currentSeed != null)
            selectedBlock.PlantSeed(currentSeed);
    }

    public void HarvestSelected()
    {
        if (selectedBlock != null)
            selectedBlock.HarvestPlants();
    }

    
    public void SetCurrentSeed(SeedPacket seed)
    {
        currentSeed = seed;
        Debug.Log($"Selected seed: {seed.cropName}");
    }
}
