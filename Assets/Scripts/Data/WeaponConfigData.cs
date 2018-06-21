using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class WeaponConfigData : IConfigData
{
    public List<WeaponConfig> weapons { get; set; }
    public List<ProjectileConfig> projectiles { get; set; }
}

[Serializable]
public class WeaponConfig
{
    public string id { get; set; }
    public string prefab { get; set; }
    public string projectile { get; set; }
}

[Serializable]
public class ProjectileConfig
{
    public string id { get; set; }
    public string prefab { get; set; }
    public float power { get; set; }
    public float speed { get; set; }
}