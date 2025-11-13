using UnityEngine;
using UnityEngine.UI;

public class ToolBarController : MonoBehaviour
{
    [Header("Farming Controller Reference")]
    public FarmingController farmingController;

    [Header("Toolbar Buttons")]
    public Button hoeButton;
    public Button waterButton;
    public Button plantButton;
    public Button harvestButton;

    void Start()
    {
        if (farmingController == null)
        {
            Debug.LogError("FarmingController reference not set in ToolBarController!");
            return;
        }

        // Assign button click listeners
        if (hoeButton != null) hoeButton.onClick.AddListener(OnHoe);
        if (waterButton != null) waterButton.onClick.AddListener(OnWater);
        if (plantButton != null) plantButton.onClick.AddListener(OnPlant);
        if (harvestButton != null) harvestButton.onClick.AddListener(OnHarvest);
    }

    // Button callbacks
    public void OnHoe()
    {
        farmingController.TillSelected();
    }

    public void OnWater()
    {
        farmingController.WaterSelected();
    }

    public void OnPlant()
    {
        farmingController.PlantSelected();
    }

    public void OnHarvest()
    {
        farmingController.HarvestSelected();
    }
}
