using UnityEngine;

[CreateAssetMenu(fileName = "NewSeedPacket", menuName = "Farming/SeedPacket")]
public class SeedPacket : ScriptableObject
{
    [Header("Crop Info")]
    public string cropName;

    [Header("Growth Sprites (4 stages)")]
    public Sprite[] growthSprites;

    [Header("Harvest Prefab")]
    public GameObject harvestPrefab;

    public Sprite GetIconForStage(int stage)
    {
        if (growthSprites == null || growthSprites.Length == 0) return null;
        stage = Mathf.Clamp(stage, 0, growthSprites.Length - 1);
        return growthSprites[stage];
    }
}
