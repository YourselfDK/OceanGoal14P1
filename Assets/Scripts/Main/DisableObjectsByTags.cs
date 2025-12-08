using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableObjectsByTags : MonoBehaviour
{
    public enum CleanupLevel
    {
        None = 0,
        Light = 1,
        Tough = 2
    }

    [Header("Cleanup Levels (set in Inspector or via MainManager)")]
    public CleanupLevel FisherCleanup = CleanupLevel.None;
    public CleanupLevel OilCleanup = CleanupLevel.None;
    public CleanupLevel MPlasticCleanup = CleanupLevel.None;
    public CleanupLevel ODeprivedCleanup = CleanupLevel.None;
    public CleanupLevel ToxicWasteCleanup = CleanupLevel.None;

    [Tooltip("Disable objects on Start. Otherwise, objects are disabled when scene changes.")]
    public bool disableOnStart = true;

    void OnEnable()
    {
        // Subscribe to sceneLoaded event (newer Unity API)
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        if (disableOnStart)
        {
            DisableObjects();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Called automatically when a new scene is loaded
        DisableObjects();
    }

    /// <summary>
    /// Disables objects based on cleanup values (None, Light, Tough).
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

    private int HandleCleanup(CleanupLevel cleanupLevel, string lightTag, string toughTag)
    {
        int disabledCount = 0;

        if (cleanupLevel == CleanupLevel.Light)
        {
            disabledCount += DisableByTag(lightTag);
        }
        else if (cleanupLevel == CleanupLevel.Tough)
        {
            disabledCount += DisableByTag(lightTag);
            disabledCount += DisableByTag(toughTag);
        }

        return disabledCount;
    }

    private int DisableByTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return 0;

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(false);
        }

        if (objectsWithTag.Length > 0)
            Debug.Log($"{objectsWithTag.Length} GameObjects with tag '{tag}' were disabled.");

        return objectsWithTag.Length;
    }
}