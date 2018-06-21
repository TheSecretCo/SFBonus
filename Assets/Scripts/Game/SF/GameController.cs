using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[PrefabAttribute("Singleton/GameController")]
public class GameController : Singleton<GameController> 
{
    LevelConfig levelConfig;
    int currentObjectIndex = 0;

    DestroyableObject currentObject;
    CharacterController characterController;

    public CameraController cameraController { get; private set; }

	private void Awake()
	{
        ConfigDataManager.Instance.Init();
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
	}

	// Use this for initialization
	void Start () 
    {
        LoadLevel(1);
        LoadNextObject(true);
        CreateCharacter();
	}
	
    void LoadLevel (int _level)
    {
        levelConfig = ConfigDataManager.Instance.GetLevel(_level);
    }

    public void LoadNextObject (bool _immediate = false)
    {
        if (currentObjectIndex >= levelConfig.objects.Count)
        {
            // Next level
            Debug.LogError("Next level is not ready yet!!!!!!");
            return;
        }

        DestroyPreviousObject();

        if (_immediate)
        {
            LoadNextObjectHere();
            return;
        }

        StartCoroutine(LoadNextObjectRoutine());
    }

    IEnumerator LoadNextObjectRoutine ()
    {
        yield return new WaitForSeconds(0.3f);
        LoadNextObjectHere();
    }

    void LoadNextObjectHere ()
    {
        CreateObject(currentObjectIndex);
        currentObjectIndex++;
    }

    void DestroyPreviousObject ()
    {
        if (currentObject != null)
        {
            Destroy(currentObject.gameObject);
        }
    }

    void CreateObject (int _objectIndex)
    {
        ObjectConfig objectConfig = levelConfig.objects[_objectIndex];
        GameObject objectGO = Instantiate(Resources.Load<GameObject>(objectConfig.prefab));

        currentObject = objectGO.GetComponent<DestroyableObject>();
        currentObject.SetupObject(objectConfig);
        cameraController.RegisterDestroyableObject(currentObject.transform);

        if (characterController != null)
        {
            characterController.RegisterTargetObject(currentObject);
        }
    }

    void CreateCharacter ()
    {
        CharacterConfig characterConfig = ConfigDataManager.Instance.GetCharacter("1st");
        GameObject characterGO = Instantiate(Resources.Load<GameObject>(characterConfig.prefab));

        characterController = characterGO.GetComponent<CharacterController>();
        characterController.SetupCharacter(characterConfig, currentObject);
    }

}
