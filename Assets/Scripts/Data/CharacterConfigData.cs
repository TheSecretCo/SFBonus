using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CharacterConfigData : IConfigData
{
    public List<CharacterConfig> characters { get; set; }
}

[Serializable]
public class CharacterConfig
{
    public string id { get; set; }
    public string name { get; set; }
    public string prefab { get; set; }
    public float power { get; set; }
    public string defaultWeapon { get; set; }
}
