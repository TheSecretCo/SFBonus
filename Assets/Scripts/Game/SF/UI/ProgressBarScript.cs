using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour 
{
    [SerializeField]
    Image background;
    [SerializeField]
    Image foreground;
    [SerializeField]
    Text text;

    float max;
    float currentProgress;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void SetupProgressBar (Sprite _bgSprite, Sprite _fgSprite, float _max, float _currentProgress, bool _shouldShowText = true)
    {
        if (_bgSprite != null)
        {
            background.overrideSprite = _bgSprite;
        }
        if (_fgSprite != null)
        {
            foreground.overrideSprite = _fgSprite;
        }
        text.gameObject.SetActive(_shouldShowText);
        max = _max;
        currentProgress = _currentProgress;

        UpdateProgress(currentProgress);
    }

    public void SetProgressBarSizeAndPosition (Vector2 _newSize, Vector2 _newPosition)
    {
        background.rectTransform.sizeDelta = _newSize;
        background.rectTransform.anchoredPosition = _newPosition;
    }

    public void UpdateProgress (float _currentProgress)
    {
        currentProgress = _currentProgress;
        foreground.fillAmount = currentProgress / max;
        text.text = (foreground.fillAmount * 100.0f).ToString() + "%";
    }
}
