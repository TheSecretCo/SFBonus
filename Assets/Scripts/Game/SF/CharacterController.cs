using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{

    bool isRotating;
    bool prepareSwiping;
    bool isSwiping;

    Vector2 startingTouchPosition;

    public DestroyableObject targetObject { get; private set; }
    //Transform destroyableObject;
    CharacterConfig characterConfig;

    Animator mAnimator;

    [SerializeField]
    Transform weaponContainer;
    WeaponController weaponController;



    // Use this for initialization
    void Start ()
    {
    }

    private void OnEnable ()
    {
        mAnimator = GetComponent<Animator>();
        TimeManager.Instance.onUpdate += Instance_OnUpdate;
    }

    private void OnDisable ()
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

    void Instance_OnUpdate (float _deltaTime)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Rotate(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Rotate(Vector3.down);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Attack(true);
        }
#endif
#if UNITY_IOS || UNITY_ANDROID
        SwipeControl();
        //TapControl();
#endif
    }

    void SwipeControl ()
    {
        if (Input.touchCount == 1)
        {
            if (prepareSwiping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouchPosition;

                // Put difference in Screen ratio, but using only width, so the ratio is the same on both
                // axes (otherwise we would have to swipe more vertically...)
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
                {
                    isSwiping = true;
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
                }
            }

            // Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
            // a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startingTouchPosition = Input.GetTouch(0).position;
                    prepareSwiping = true;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (!isSwiping)
                    {
                        TapControl();
                    }

                    isSwiping = false;
                    prepareSwiping = false;
                }
            }
        }
    }

    void TapControl ()
    {
        if (isRotating)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            //if (Input.GetTouch(0).phase == TouchPhase.Began)
            //{
                Attack(true);
            //}
        }

        //Attack(true);
    }

    void Attack (bool _isFromPlayer = false)
    {
        mAnimator.StopPlayback();
        mAnimator.SetTrigger("Punch");
        weaponController.Attack(_isFromPlayer);
    }

    void Rotate (Vector3 _rotateAxis)
    {
        if (isRotating)
        {
            return;
        }
        isRotating = true;
        StartCoroutine(GameUtility.RotateAround(transform, targetObject.transform, _rotateAxis, 90.0f, 0.3f, delegate
        {
            isRotating = false;
        }));
    }

    public void SetupCharacter (CharacterConfig _characterConfig, DestroyableObject _targetObject)
    {
        if (_characterConfig != null)
        {
            characterConfig = _characterConfig;
        }

        transform.position = _targetObject.front.position;

        RegisterTargetObject(_targetObject);
        CreateWeapon(characterConfig.defaultWeapon);
    }

    public void RegisterTargetObject (DestroyableObject _targetObject)
    {
        targetObject = _targetObject;
    }

    void CreateWeapon (string _weaponID)
    {
        WeaponConfig weaponConfig = ConfigDataManager.Instance.GetWeapon(_weaponID);
        GameObject weaponGO = Instantiate(Resources.Load<GameObject>(weaponConfig.prefab));
        weaponGO.transform.parent = weaponContainer;
        weaponGO.transform.localPosition = Vector3.zero;
        weaponGO.transform.localScale = Vector3.one;
        weaponController = weaponGO.GetComponent<WeaponController>();
        weaponController.SetupWeapon(weaponConfig, this);
    }
}
