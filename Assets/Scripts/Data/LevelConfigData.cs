using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LevelConfigData : IConfigData
{
    public List<LevelConfig> levelData { get; set; }
}

[Serializable]
public class LevelConfig
{
    public int level { get; set; }
    public List<ObjectConfig> objects { get; set; }
}

[Serializable]
public class ObjectConfig
{
    public int number { get; set; }
    public string prefab { get; set; }
    //public float hp { get; set; }
    public List<AttackPointConfig> attackPoints { get; set; }
}

[Serializable]
public class AttackPointConfig
{
    public string name { get; set; }
    public float hp { get; set; }
    public AttackPointType type { get; set; }
}