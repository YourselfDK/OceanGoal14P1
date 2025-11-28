using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button FishButton;
    public Button HazardsButton;

    [Header("Panels")]
    public GameObject DisplayPanelFish;
    public GameObject DisplayPanelHazards;

    void Start()
    {
        // Hide panels at start
        DisplayPanelFish.SetActive(false);
        DisplayPanelHazards.SetActive(false);

        // Add listeners
        FishButton.onClick.AddListener(ShowFishPanel);
        HazardsButton.onClick.AddListener(ShowHazardsPanel);
    }

    void ShowFishPanel()
    {
        DisplayPanelFish.SetActive(true);
    }

    void ShowHazardsPanel()
    {
        DisplayPanelHazards.SetActive(true);
    }
}

