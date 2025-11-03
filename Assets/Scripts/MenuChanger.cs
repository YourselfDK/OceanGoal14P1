using UnityEngine;

public class menuChanger : MonoBehaviour
{
    public GameObject videoPanel;
    public GameObject mathPanel;
    public GameObject madPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeThat(string nameOfPanel)
    {
        videoPanel.SetActive(nameOfPanel == "video");
        mathPanel.SetActive(nameOfPanel == "math");
        madPanel.SetActive(nameOfPanel == "mad");
    }
}