using UnityEngine;

public class CropBlock : MonoBehaviour
{
    private enum CropState { Empty, Plowed, Planted, ReadyToHarvest }

    public Vector2Int Location { get; protected set; }

    [Header("Tilled Soil")]
    public SpriteRenderer _plowedSoilSR;
    public Sprite _plowedSoilIcon;

    [Header("Watered Soil")]
    public SpriteRenderer _wateredSoilSR;
    public Sprite _wateredSoilIcon;

    [Header("Crop Sprites")]
    public SpriteRenderer _cropGrowthSR; 
    public SpriteRenderer _cropSR;       

    private int _currentStage;
    private SeedPacket _currentSeedPacket;

    private bool _isWatered = false;
    private bool _preventUse = false;

    private string _cropName = string.Empty;
    private string _tilemapName = string.Empty;

    public Validator validator;
    private CropManager _cropController;
    private CropState _currentState = CropState.Empty;

    private void Awake()
    {
        validator = GetComponent<Validator>();
    }

    public void Initialize(string tilemapName, Vector2Int location, CropManager cropController)
    {
        Location = location;
        _tilemapName = tilemapName;
        _cropController = cropController;
        _currentState = CropState.Empty;
        name = FormatName();
    }

    public void PlowSoil()
    {
        if (_plowedSoilSR == null || _plowedSoilIcon == null) return;
        if (_currentState != CropState.Empty) return;
        if (_preventUse) return;

        _currentState = CropState.Plowed;
        _plowedSoilSR.enabled = true;
        _plowedSoilSR.sprite = _plowedSoilIcon;
    }

    public void WaterSoil()
    {
        if (_wateredSoilSR == null || _wateredSoilIcon == null) return;
        if (_currentState == CropState.Empty) return;
        if (_preventUse) return;
        if (_isWatered) return;

        _wateredSoilSR.enabled = true;
        _wateredSoilSR.sprite = _wateredSoilIcon;
        _isWatered = true;
    }

    public void PlantSeed(SeedPacket seed)
    {
        if (_currentState != CropState.Plowed) return;
        if (seed == null) return;

        _currentSeedPacket = seed;
        _currentStage = 0;
        _currentState = CropState.Planted;
        _cropName = seed.cropName;

        if (_cropGrowthSR != null)
        {
            _cropGrowthSR.enabled = true;
            _cropGrowthSR.sprite = seed.GetIconForStage(_currentStage);
        }

        _cropController.AddToPlantedCrops(this);
        Debug.Log($"Planted {seed.cropName} at {Location.x},{Location.y}");
    }

    public void AdvanceGrowth()
    {
        if (_currentState != CropState.Planted) return;
        _currentStage++;

        if (_currentSeedPacket != null && _currentStage >= _currentSeedPacket.growthSprites.Length)
        {
            _currentStage = _currentSeedPacket.growthSprites.Length - 1;
            _currentState = CropState.ReadyToHarvest;
        }

        if (_cropGrowthSR != null && _currentSeedPacket != null)
            _cropGrowthSR.sprite = _currentSeedPacket.GetIconForStage(_currentStage);

        Debug.Log($"{_cropName} at {Location.x},{Location.y} advanced to stage {_currentStage}");
    }

    
    public void HarvestPlants()
    {
        if (_currentState != CropState.ReadyToHarvest) return;

        if (_currentSeedPacket != null && _currentSeedPacket.harvestPrefab != null)
        {
            GameObject obj = Instantiate(_currentSeedPacket.harvestPrefab, transform.position, Quaternion.identity);

            // Assign harvested item data
            HarvestItem harvestItem = obj.GetComponent<HarvestItem>();
            if (harvestItem != null && _currentSeedPacket.harvestItemData != null)
            {
                harvestItem.itemData = _currentSeedPacket.harvestItemData;
                harvestItem.quantity = harvestItem.itemData.startingQuantity;
            }
        }

        ResetSoil();
        _cropController.RemoveFromPlantedCrops(this);
    }

    private void ResetSoil()
    {
        _currentState = CropState.Empty;
        _isWatered = false;
        _cropName = string.Empty;

        if (_plowedSoilSR != null) { _plowedSoilSR.enabled = false; _plowedSoilSR.sprite = null; }
        if (_wateredSoilSR != null) { _wateredSoilSR.enabled = false; _wateredSoilSR.sprite = null; }
        if (_cropGrowthSR != null) { _cropGrowthSR.enabled = false; _cropGrowthSR.sprite = null; }
        if (_cropSR != null) _cropSR.enabled = false;

        name = FormatName();
    }

    public void Highlight(bool on)
    {
        if (_cropSR != null)
            _cropSR.enabled = on;
    }

    private string FormatName()
    {
        return _currentState == CropState.Planted
            ? $"{_tilemapName}-{_cropName} [{Location.x},{Location.y}]"
            : $"{_tilemapName} [{Location.x},{Location.y}]";
    }
}
