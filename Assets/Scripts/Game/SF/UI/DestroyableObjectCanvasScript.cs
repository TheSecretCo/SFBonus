using System;
using UnityEngine;

[PrefabAttribute("UI/DestroyableObjectCanvas")]
public class DestroyableObjectCanvasScript : Singleton<DestroyableObjectCanvasScript>
{
    [SerializeField]
    RectTransform panel;
    [SerializeField]
    GameObject numberTextPrefab;

    DestroyableObject targetObj;
    ProgressBarScript progressBar;
    ObjectPool numberObjectPool;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void SetupUI (DestroyableObject _targetObj)
    {
        if (targetObj != null)
        {
            targetObj.onHPUpdate -= TargetObj_OnHPUpdate;
        }
        targetObj = _targetObj;

        if (progressBar == null)
        {
            GameObject progressBarPrefab = GameUtility.GetProgressBarPrefab();
            GameObject progressBarGo = Instantiate(progressBarPrefab);
            progressBarGo.transform.SetParent(panel);

            progressBarGo.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
            progressBarGo.transform.localScale = Vector3.one;

            progressBar = progressBarGo.GetComponent<ProgressBarScript>();
        }

        progressBar.SetupProgressBar(null, null, targetObj.hp, targetObj.hp);

        CreateNumberTextObjectPool();

        targetObj.onHPUpdate += TargetObj_OnHPUpdate;
    }

    void TargetObj_OnHPUpdate(float _hp, float _power)
    {
        if (progressBar == null)
        {
            return;
        }

        HandleNumberText(_power);
        progressBar.UpdateProgress(_hp);
    }

    void CreateNumberTextObjectPool ()
    {
        if (numberObjectPool == null)
        {
            numberObjectPool = new ObjectPool(numberTextPrefab, 3);
        }
    }

    void HandleNumberText (float _hp)
    {
        GameObject numberObject = numberObjectPool.Get();
        numberObject.transform.SetParent(panel);
        NumberTextScript script = numberObject.GetComponent<NumberTextScript>();
        script.SetupNumberText(_hp.ToString(), numberObjectPool);
    }

	private void OnDisable()
	{
        targetObj.onHPUpdate -= TargetObj_OnHPUpdate;
	}
}
