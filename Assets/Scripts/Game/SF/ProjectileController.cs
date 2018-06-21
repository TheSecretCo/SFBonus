using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour 
{
    public ProjectileConfig configData { get; private set; }
    bool isAttacking;
    public bool isFromPlayer { get; private set; }

    CharacterController character;
    public WeaponController weapon { get; private set; }

	private void OnDisable()
	{
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.onLateUpdate -= Instance_OnLateUpdate;
        }
	}
	// Use this for initialization
	void Start () {
		
	}
	
    void Instance_OnLateUpdate()
    {
        if (!isAttacking)
        {
            return;
        }

        if (character.targetObject == null)
        {
            Free();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, character.targetObject.transform.position, configData.speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, character.targetObject.transform.position, 0.1f* Time.deltaTime);

    }

    public void Attack (ProjectileConfig _config, WeaponController _parent, CharacterController _character, bool _isFromPlayer = false)
    {
        TimeManager.Instance.onLateUpdate += Instance_OnLateUpdate;
        character = _character;
        weapon = _parent;
        configData = _config;
        isFromPlayer = _isFromPlayer;
        transform.position = weapon.transform.position;
        transform.localScale = Vector3.one;

        //transform.forward = character.transform.forward;
        isAttacking = true;
    }

    public void Free ()
    {
        weapon.FreeProjectile(this);
    }
}
