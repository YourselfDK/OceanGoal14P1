using UnityEngine;
using UnityEngine.UI;

public class Encyclopediamanager : MonoBehaviour
{
     
    public GameObject FishPanel; 
    public GameObject HazardsPanel; 
    public GameObject SolutionPanels;   

    public void ChangeThat (string nameOfPanel)
    {
        FishPanel.SetActive(nameOfPanel == "FishPanel");
        HazardsPanel.SetActive(nameOfPanel == "HazardsPanel");
        SolutionPanels.SetActive(nameOfPanel == "SolutionsPanel");
    }
}