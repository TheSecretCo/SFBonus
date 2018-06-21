using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NumberTextScript : MonoBehaviour 
{
    Color originalTextColor;
    Color originalOutlineColor;
    [SerializeField]
    Text text;
    [SerializeField]
    Outline outline;

    [SerializeField]
    float flySpeed;

    ObjectPool objectPool;
    Coroutine textAnimationCoroutine;

	private void Awake()
	{
        originalTextColor = text.color;
        originalOutlineColor = outline.effectColor;
	}

	// Use this for initialization
	void Start () 
    {
        
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void SetupNumberText (string _text, ObjectPool _objectPool)
    {
        text.text = _text;
        objectPool = _objectPool;
        float randomX = UnityEngine.Random.Range(-10.0f, 10.0f);
        float randomY = UnityEngine.Random.Range(0.0f, 50.0f);
        text.rectTransform.anchoredPosition = new Vector2(randomX, 50.0f);
        text.color = originalTextColor;
        outline.effectColor = originalOutlineColor;
        textAnimationCoroutine = StartCoroutine(TextAnimation());
    }

    IEnumerator TextAnimation ()
    {
        float defaultY = text.rectTransform.anchoredPosition.y;
        float randomY = defaultY + 50.0f;//Random.Range(defaultY + 70.0f, defaultY + 80.0f);
        Vector2 TargetPosition = new Vector2(text.rectTransform.anchoredPosition.x, randomY);

        while (text.rectTransform.anchoredPosition.y < randomY - 0.1f)
        {
            text.rectTransform.anchoredPosition = Vector2.Lerp(text.rectTransform.anchoredPosition, TargetPosition, flySpeed * Time.deltaTime);

            if (text.rectTransform.anchoredPosition.y > randomY - 10.0f)
            {
                Color tColor = text.color;
                tColor.a = tColor.a - (Time.deltaTime);
                text.color = tColor;

                Color oColor = outline.effectColor;
                oColor.a = oColor.a - (Time.deltaTime);
                outline.effectColor = oColor;

                if (text.color.a < 0.0f)
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        if (objectPool != null)
        {
            objectPool.Free(gameObject);
        }
    }
}
