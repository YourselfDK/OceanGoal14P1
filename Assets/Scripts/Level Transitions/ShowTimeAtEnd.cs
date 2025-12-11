using TMPro;
using UnityEngine;

public class ShowTimeAtEnd : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerReveal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerReveal.text = "THANK YOU FOR PLAYING\r\n\r\nYour time was " + MainManager.Instance.TimerInSeconds.ToString() + " seconds!\r\nIf you wish to learn more about the creatures and dangers you faced, you can find the encyclopedia in the main menu";
    }
}
