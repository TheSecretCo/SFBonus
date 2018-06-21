using UnityEngine;

public class TitleCanvasScript : MonoBehaviour
{
    [SerializeField]
    string mainMenuSceneName;

    // Use this for initialization
    void Start ()
    {
        ConfigDataManager.Instance.Init();
    }

    public void OnPlayButtonPressed ()
    {
        StartCoroutine(GameUtility.LoadSceneRoutine(mainMenuSceneName));
    }
}
