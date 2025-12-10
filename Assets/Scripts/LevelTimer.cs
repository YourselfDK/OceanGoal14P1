using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{

    public static LevelTimer Instance;

    [SerializeField] TextMeshProUGUI SecondsCounter;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.Instance.InShop == false)
        {
            MainManager.Instance.Timer += Time.deltaTime;
            SecondsCounter.text = MainManager.Instance.Timer.ToString();
        }
    }
}
