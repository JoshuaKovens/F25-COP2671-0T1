using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CropManager : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap croppableTilemap;

    [Header("CropBlock Prefab")]
    public GameObject cropBlockPrefab;

    [Header("Time Manager")]
    public TimeManager timeManager;

    private List<CropBlock> plantedCrops = new List<CropBlock>();
    private Dictionary<Vector2Int, CropBlock> cropGrid = new Dictionary<Vector2Int, CropBlock>();

    private void Start()
    {
        if (timeManager == null)
            timeManager = FindObjectOfType<TimeManager>();

        if(timeManager != null)
            timeManager.OnTimeChanged += OnTimeChanged;

        if (croppableTilemap == null || cropBlockPrefab == null)
        {
            Debug.LogError("Assign croppableTilemap and cropBlockPrefab in inspector.");
            return;
        }

        CreateGridUsingTilemap(croppableTilemap);
    }

    void CreateGridUsingTilemap(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                if (!tilemap.HasTile(cellPos)) continue;

                Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
                worldPos.z = 0f;

                GameObject blockObj = Instantiate(cropBlockPrefab, worldPos, Quaternion.identity, transform);
                CropBlock cropBlock = blockObj.GetComponent<CropBlock>();

                Vector2Int gridPos = new Vector2Int(x - bounds.xMin, y - bounds.yMin);
                cropBlock.Initialize(tilemap.name, gridPos, this);

                cropGrid.Add(gridPos, cropBlock);
            }
        }
    }

    void OnTimeChanged(int hour, int minute)
    {
        if (hour == 6 && minute == 0)
        {
            foreach (var crop in plantedCrops)
                crop.AdvanceGrowth();
        }
    }

    public void AddToPlantedCrops(CropBlock cropBlock)
    {
        if (!plantedCrops.Contains(cropBlock))
            plantedCrops.Add(cropBlock);
    }

    public void RemoveFromPlantedCrops(CropBlock cropBlock)
    {
        if (plantedCrops.Contains(cropBlock))
            plantedCrops.Remove(cropBlock);
    }

    public CropBlock GetBlockAt(Vector2Int pos)
    {
        cropGrid.TryGetValue(pos, out CropBlock block);
        return block;
    }
}
