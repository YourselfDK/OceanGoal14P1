using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{

    public static LevelTimer Instance;

    [SerializeField] TextMeshProUGUI SecondsCounter;

    UnityEngine.SceneManagement.Scene finalLevel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SecondsCounter.text = MainManager.Instance.TimerInSeconds.ToString() + " Sec";
        UnityEngine.SceneManagement.Scene finalLevel = SceneManager.GetSceneByName("Level 6");
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.SceneManagement.Scene whatScene = SceneManager.GetActiveScene();
        if (MainManager.Instance.InShop == false)
        {
            MainManager.Instance.Timer += Time.deltaTime;
            if(MainManager.Instance.Timer >= 1)
            {
                MainManager.Instance.TimerInSeconds++;
                SecondsCounter.text = MainManager.Instance.TimerInSeconds.ToString() + " Sec";
                MainManager.Instance.Timer = 0;
            }
        }
        if (whatScene == finalLevel)
        {
            Destroy(this);
        }
    }
}
