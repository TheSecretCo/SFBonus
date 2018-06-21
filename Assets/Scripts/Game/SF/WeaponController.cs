using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    WeaponConfig configData;
    ProjectileConfig projectileConfig;

    public CharacterController parent { get; private set; }

    ObjectPool projectileObjectPool;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void SetupWeapon (WeaponConfig _config, CharacterController _parent)
    {
        parent = _parent;
        configData = _config;
        projectileConfig = ConfigDataManager.Instance.GetProjectile(configData.projectile);
    }

    public void Attack (bool _isFromPlayer = false)
    {
        if (projectileObjectPool == null)
        {
            projectileObjectPool = new ObjectPool(Resources.Load<GameObject>(projectileConfig.prefab), 3);
        }

        GameObject projectileObject = projectileObjectPool.Get();
        ProjectileController projectileController = projectileObject.GetComponent<ProjectileController>();

        projectileController.Attack(projectileConfig, this, parent, _isFromPlayer);
    }

    public void FreeProjectile (ProjectileController _projectile)
    {
        projectileObjectPool.Free(_projectile.gameObject);
    }

	private void OnDestroy()
	{
        if (projectileObjectPool != null)
        {
            projectileObjectPool.CleanPool();
        }
	}
}
