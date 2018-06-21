
using System;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterPositionType
{
    Center,
    Front,
    Right,
    Left,
    Back
}

public class DestroyableObject : MonoBehaviour
{
    [SerializeField]
    public Transform front;
    [SerializeField]
    public Transform right;
    [SerializeField]
    public Transform left;
    [SerializeField]
    public Transform back;

    int currentWeakPointIndex = 1;
    AttackPointController mainAttackPoint;
    List<AttackPointController> attackPointList = new List<AttackPointController>();
    ObjectConfig configData;

    public float hp { get; private set; }
    public event Action<float, float> onHPUpdate;

    bool willDestroy;

    // Use this for initialization
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update () {

    //}

    public void SetupObject(ObjectConfig _configData)
    {
        configData = _configData;
        //hp = configData.hp;
        FindWeakPoints();
        SetupUI();
    }

    void FindWeakPoints()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < configData.attackPoints.Count; i++)
        {
            AttackPointConfig attackPointConfig = configData.attackPoints[i];

            foreach (Transform child in allChildren)
            {
                if (child.name.Equals(attackPointConfig.name))
                {
                    AttackPointController attackPointController = child.gameObject.AddComponent<AttackPointController>();
                    if (i > 1)
                    {
                        child.gameObject.SetActive(false);
                    }
                    attackPointController.SetupAttackPoint(attackPointConfig, this);
                    attackPointList.Add(attackPointController);
                    break;
                }
            }
        }

        hp = attackPointList[0].hp;
    }

    void SetupUI ()
    {
        DestroyableObjectCanvasScript.Instance.SetupUI(this);
    }

    public void GotAttack (AttackPointController _attackFrom, float _power)
    {
        if (attackPointList.Count <= 0 || willDestroy)
        {
            return;
        }
        //Debug.LogError(_attackFrom.hp + " " + currentWeakPointIndex);
        if (_attackFrom.hp <= 0.0f)
        {
            attackPointList[currentWeakPointIndex].gameObject.SetActive(false);
            currentWeakPointIndex += 1;
            if (attackPointList.Count > currentWeakPointIndex)
            {
                AttackPointController attackPointController = attackPointList[currentWeakPointIndex];
                attackPointController.gameObject.SetActive(true);
            }
        }

        hp = attackPointList[0].UpdateHP(_power);
        if (onHPUpdate != null)
        {
            onHPUpdate(hp, _power);
        }
            
        if (hp <= 0.0f)
        {
            GameController.Instance.LoadNextObject();
            willDestroy = true;
        }
    }
}
