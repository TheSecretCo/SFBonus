using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour 
{
    [SerializeField]
    Camera mCamera;

    [SerializeField]
    Transform destroyableObject;

    bool isRotating;
    bool isSwiping;
    Vector2 startingTouchPosition;

	// Use this for initialization
	void Start () 
    {
        //mCamera.transform.LookAt(vehicle);

	}

	private void OnEnable()
	{
        TimeManager.Instance.onUpdate += Instance_OnUpdate;
	}
	private void OnDisable()
	{
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.onUpdate -= Instance_OnUpdate;
        }
	}
    //// Update is called once per frame
    //void Update () 
    //   {

    //}

    void Instance_OnUpdate(float _deltaTime)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown("q"))
        {
            Rotate(Vector3.up);
        }
        if (Input.GetKeyDown("w"))
        {
            Rotate(Vector3.down);
        }
#endif
#if UNITY_IOS || UNITY_ANDROID
        SwipeControl();
#endif
    }

    void SwipeControl()
    {
        if (Input.touchCount == 2)
        {
            if (isSwiping)
            {
                Vector2 diff = ((Input.GetTouch(0).position + Input.GetTouch(1).position) / 2) - startingTouchPosition;

                // Put difference in Screen ratio, but using only width, so the ratio is the same on both
                // axes (otherwise we would have to swipe more vertically...)
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {
                            //Slide();
                        }
                        else
                        {
                            //forwardTargetPosition = new Vector3(0.0f, 0.0f, transform.position.z + runDistance);
                            //Jump();
                        }
                    }
                    else
                    {
                        if (diff.x < 0)
                        {
                            Rotate(Vector3.up);
                        }
                        else
                        {
                            Rotate(Vector3.down);
                        }
                    }

                    isSwiping = false;
                }
            }

            // Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
            // a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    //startingTouchPosition = Vector3. Input.GetTouch(0).position;
                    startingTouchPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;
                    isSwiping = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    isSwiping = false;
                }
            }
        }
    }

    void Rotate (Vector3 _rotateAxis)
    {
        if (isRotating)
        {
            return;
        }
        isRotating = true;
        StartCoroutine(GameUtility.RotateAround(transform, destroyableObject, _rotateAxis, 90.0f, 0.3f, delegate {
            isRotating = false;
        }));
    }

    public void RegisterDestroyableObject (Transform _transform)
    {
        destroyableObject = _transform;
    }


}
