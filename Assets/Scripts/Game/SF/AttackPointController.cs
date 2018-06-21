using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackPointType
{
    Normal,
    Weak
}

public class AttackPointController : MonoBehaviour 
{
    AttackPointConfig configData;
    public float hp { get; set; }
    DestroyableObject parent;

    [SerializeField]
    AttackPointType attackPointType;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void SetupAttackPoint (AttackPointConfig _configData, DestroyableObject _parent)
    {
        parent = _parent;
        configData = _configData;
        attackPointType = configData.type;
        hp = configData.hp;
    }

    private void OnTriggerEnter(Collider _other)
	{
        ProjectileController projectileController = _other.GetComponent<ProjectileController>();
        projectileController.Free();
        float power = projectileController.configData.power;

        if (projectileController.isFromPlayer && attackPointType.Equals(AttackPointType.Weak))
        {
            power *= 1.5f;
        }

        if (attackPointType.Equals(AttackPointType.Weak))
        {
            UpdateHP(power);
        }
        parent.GotAttack(this, power);
	}

    public float UpdateHP (float _power)
    {
        return hp -= _power;
    }
}
