using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableObjectsByTags : MonoBehaviour
{
    [Header("Cleanup Levels (0=None, 1=Light, 2=Tough)")]
    [Range(0, 2)] public int FisherCleanup = 0;
    [Range(0, 2)] public int OilCleanup = 0;
    [Range(0, 2)] public int MPlasticCleanup = 0;
    [Range(0, 2)] public int ODeprivedCleanup = 0;
    [Range(0, 2)] public int ToxicWasteCleanup = 0;

    [Tooltip("Disable objects on Start. Otherwise, objects are disabled when scene changes.")]
    public bool disableOnStart = true;

    // List of all tags this script depends on
    private readonly string[] requiredTags = new string[]
    {
        "FisherLight", "FisherTough",
        "OilspillLight", "OilspillTough",
        "MicroplasticLight", "MicroplasticTough",
        "OxygenDeprivedLight", "OxygenDeprivedTough",
        "ToxicWasteLight", "ToxicWasteTough"
    };

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        ValidateTags(); // 🔍 sanity check before doing anything

        if (disableOnStart)
        {
            SyncFromMainManager();
            DisableObjects();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ValidateTags(); // 🔍 sanity check on scene load
        SyncFromMainManager();
        DisableObjects();
    }

    /// <summary>
    /// Sync inspector values with MainManager.Instance values.
    /// </summary>
    private void SyncFromMainManager()
    {
        if (MainManager.Instance == null)
        {
            Debug.LogWarning("MainManager.Instance is null!");
            return;
        }

        FisherCleanup = MainManager.Instance.FisherCleanup;
        OilCleanup = MainManager.Instance.OilCleanup;
        MPlasticCleanup = MainManager.Instance.MPlasticCleanup;
        ODeprivedCleanup = MainManager.Instance.ODeprivedCleanup;
        ToxicWasteCleanup = MainManager.Instance.ToxicWasteCleanup;
    }

    /// <summary>
    /// Disables objects based on cleanup values (0=None, 1=Light, 2=Tough).
    /// </summary>
    public void DisableObjects()
    {
        int totalDisabled = 0;

        totalDisabled += HandleCleanup(FisherCleanup, "FisherLight", "FisherTough");
        totalDisabled += HandleCleanup(OilCleanup, "OilspillLight", "OilspillTough");
        totalDisabled += HandleCleanup(MPlasticCleanup, "MicroplasticLight", "MicroplasticTough");
        totalDisabled += HandleCleanup(ODeprivedCleanup, "OxygenDeprivedLight", "OxygenDeprivedTough");
        totalDisabled += HandleCleanup(ToxicWasteCleanup, "ToxicWasteLight", "ToxicWasteTough");

        Debug.Log($"Total disabled objects: {totalDisabled}");
    }

    private int HandleCleanup(int cleanupValue, string lightTag, string toughTag)
    {
        int disabledCount = 0;

        if (cleanupValue == 1)
        {
            disabledCount += DisableByTag(lightTag);
        }
        else if (cleanupValue == 2)
        {
            disabledCount += DisableByTag(lightTag);
            disabledCount += DisableByTag(toughTag);
        }

        return disabledCount;
    }

    private int DisableByTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return 0;

        GameObject[] objectsWithTag;
        try
        {
            objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        }
        catch
        {
            Debug.LogError($"[DisableObjectsByTags] Tag '{tag}' is not defined in the project!");
            return 0;
        }

        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(false);
        }

        if (objectsWithTag.Length > 0)
            Debug.Log($"{objectsWithTag.Length} GameObjects with tag '{tag}' were disabled.");

        return objectsWithTag.Length;
    }

    /// <summary>
    /// Sanity check: verify all required tags exist in the project.
    /// </summary>
    private void ValidateTags()
    {
        foreach (string tag in requiredTags)
        {
            try
            {
                // Unity throws if CompareTag is called with an undefined tag
                new GameObject().CompareTag(tag);
            }
            catch
            {
                Debug.LogError($"[DisableObjectsByTags] Missing tag definition: '{tag}'. " +
                               $"Add it in Edit > Project Settings > Tags and Layers.");
            }
        }
    }
}