using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[PrefabAttribute("Singleton/ConfigDataManager")]
public class ConfigDataManager : Singleton<ConfigDataManager>
{
    [SerializeField]
    string levelDataPath;
    [SerializeField]
    string characterDataPath;
    [SerializeField]
    string weaponDataPath;

    LevelConfigData levelConfigData;
    CharacterConfigData characterConfigData;
    WeaponConfigData weaponConfigData;

    public void Init ()
    {
    }

    private void Awake ()
    {
        levelConfigData = LoadConfigData<LevelConfigData>(levelDataPath);
        characterConfigData = LoadConfigData<CharacterConfigData>(characterDataPath);
        weaponConfigData = LoadConfigData<WeaponConfigData>(weaponDataPath);
    }

    // Use this for initialization
    void Start ()
    {
        
    }

    T LoadConfigData<T> (string _path)
    {
        TextAsset configTextAsset = Resources.Load<TextAsset>(_path);
        T configData = GameUtility.DeserializeFromByte<T>(configTextAsset.bytes);
        return configData;
    }

    void LoadLevelData()
    {
        TextAsset configTextAsset = Resources.Load<TextAsset>(levelDataPath);
        levelConfigData = GameUtility.DeserializeFromByte<LevelConfigData>(configTextAsset.bytes);

        //string path = GameUtility.GetStreamingassetPath(levelDataPath);
        //StartCoroutine(LoadLevelDataRoutine(path,(LevelConfigData _levelConfigData) => { 
        //    levelConfigData = _levelConfigData; 
        //}));

        //levelConfigData = GameUtility.DeserializeFromPath<LevelConfigData>(path);
        //Debug.LogError(levelConfigData);
    }


    //IEnumerator LoadLevelDataRoutine (string _path, Action<LevelConfigData> _callback)
    //{
    //    WWW request = new WWW("file://" + _path);
    //    while (!request.isDone)
    //    {
    //        yield return null;
    //    }

    //    LevelConfigData result = GameUtility.DeserializeFromByte<LevelConfigData>(request.bytes);

    //    if (_callback != null)
    //    {
    //        _callback(result);
    //    }
    //}

    //void OnLevelDataLoadDone (LevelConfigData _levelConfigData)
    //{
    //    levelConfigData = _levelConfigData;
    //}

    public LevelConfig GetLevel (int _level)
    {
        return levelConfigData.levelData.Find(item => item.level.Equals(_level));
    }

    public CharacterConfig GetCharacter(string _id)
    {
        return characterConfigData.characters.Find(item => item.id.Equals(_id));
    }

    public WeaponConfig GetWeapon (string _id)
    {
        return weaponConfigData.weapons.Find(item => item.id.Equals(_id));
    }

    public ProjectileConfig GetProjectile (string _id)
    {
        return weaponConfigData.projectiles.Find(item => item.id.Equals(_id));
    }
}
