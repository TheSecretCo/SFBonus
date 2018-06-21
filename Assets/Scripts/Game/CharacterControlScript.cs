﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ControlType
{
    Swipe,
    Tap
}
public class CharacterControlScript : MonoBehaviour, IGameObject
{
//    [SerializeField]
//    public ControlType characterControlType;

//    [SerializeField]
//    Transform characterTransform;
//    public CharacterColliderScript characterColliderScript { get; private set; }

    public float runDistance = 6.0F;
//    public float laneChangeSpeed = 1.0f;
//    //public float maxHeight = 2.0F;
//    //public float gravity = 20.0F;
//    private Vector3 moveDirection = Vector3.zero;

//    TrackSegment currentTrackSegment;

//    // Moving
//    [SerializeField]
//    public bool autoForward;
//    int currentLane = 1;
//    Vector3 laneChangeTargetPosition;
//    Vector3 forwardTargetPosition;

//    float totalWorldTravelDistance;

//    //Control
//    Vector2 startingTouchPosition;
//    bool isSwiping;

//    float jumpStart;
//    public bool isJumping { get; private set; }

    public GameObjectType gameObjectType { get; set; }

    public float speed
    {
        get
        {
            return runDistance;
        }
        set
        {
            runDistance = speed;
        }
    }

//    bool isGoingUp;
//    //public float jumpSpeed = 20.0F;
//    //public float jumpLength = 2.0f;     // Distance jumped
//    public float jumpHeight = 1.2f;

//    VehicleControlScript currentVehicleControlScript;

//    private void OnEnable ()
//    {
//        TimeManager.Instance.onUpdate += Instance_OnUpdate;
//        TimeManager.Instance.onLateUpdate += Instance_OnLateUpdate;
//    }

//    private void OnDisable ()
//    {
//        if (TimeManager.Instance != null)
//        {
//            TimeManager.Instance.onUpdate -= Instance_OnUpdate;
//            TimeManager.Instance.onLateUpdate -= Instance_OnLateUpdate;
//        }
//    }

//    // Use this for initialization
//    void Start ()
//    {
//        gameObjectType = GameObjectType.Character;
//        GameObject characterObject = Instantiate<GameObject>(Resources.Load<GameObject>("Player/Player"), transform);
//        characterTransform = characterObject.transform;
//        characterTransform.localScale = Vector3.one;
//        characterTransform.localPosition = Vector3.zero;
//        characterColliderScript = characterObject.GetComponent<CharacterColliderScript>();
//        RegisterCharacterToTrack();
//    }

//    void RegisterCharacterToTrack ()
//    {
//        TrackManager.Instance.RegisterCharacter(this);
//    }

//    public void RegisterVehicle (VehicleControlScript _newVehicleControlScript)
//    {
//        currentVehicleControlScript = _newVehicleControlScript;
//    }

//    void Instance_OnLateUpdate ()
//    {
//        if (currentVehicleControlScript != null)
//        {
//            transform.position = new Vector3(transform.position.x, currentVehicleControlScript.transform.position.y, currentVehicleControlScript.transform.position.z);
//            forwardTargetPosition = transform.position;
//        }
//    }


//    void Instance_OnUpdate (float _deltaTime)
//    {
//        if (!TrackManager.Instance.isTrackReady)
//        {
//            return;
//        }
//        //Vector3 newMoveDirection = Vector3.zero;
//        //if (characterTransform.isGrounded)
//        //{
//        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//        //    moveDirection = transform.TransformDirection(moveDirection);
//        //    moveDirection *= speed;
//        //    if (Input.GetButton("Jump"))
//        //    {
//        //        moveDirection.y = jumpSpeed;
//        //    }
//        //}

//        ////if (characterTransform.gameObject.transform.position.y >= maxHeight)
//        ////{
//        ////    moveDirection.y = maxHeight;
//        ////}
//        ////else
//        ////{
//        ////Debug.LogError(characterTransform.isGrounded);
//        //if (!characterTransform.isGrounded)
//        //{
//        //    moveDirection.y -= gravity * _deltaTime;
//        //    //}
//        //    //Debug.LogError(moveDirection.y);
//        //}
//        //Debug.LogError(moveDirection.y);
//        //CollisionFlags cFlags = characterTransform.Move(moveDirection * _deltaTime);

//        CheckInput();

//        float dTime = laneChangeSpeed * TrackManager.Instance.laneOffset * _deltaTime / runDistance;

//        // Move collider side to side
//        Vector3 newLaneChangeTargetPosition = laneChangeTargetPosition;
//        if (isJumping)
//        {
//            //// Same as with the sliding, we want a fixed jump LENGTH not fixed jump TIME. Also, just as with sliding,
//            //// we slightly modify length with speed to make it more playable.
//            ////newLaneChangeTargetPosition.x - Mathf.Abs(newLaneChangeTargetPosition.x) + TrackManager.Instance.laneOffset;
//            //float correctJumpLength = runDistance;//laneChangeSpeed / runDistance;// * (1.0f + trackManager.speedRatio);
//            //float ratio = (totalWorldTravelDistance - jumpStart) / correctJumpLength;
//            //Debug.LogError(totalWorldTravelDistance - jumpStart);
//            ////Debug.LogError(ratio);
//            //if (ratio >= 1.0f)
//            //{
//            //    isJumping = false;
//            //    //character.animator.SetBool(s_JumpingHash, false);
//            //}
//            //else
//            //{
//            //    newLaneChangeTargetPosition.y = ratio * 2.0f * jumpHeight * laneChangeSpeed;//Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
//            //    //if (newLaneChangeTargetPosition.y <= 0.0f)
//            //    //{
//            //    //    newLaneChangeTargetPosition.y = 0.0f;
//            //    //    isJumping = false;
//            //    //}

//            //}

//            //else if (!AudioListener.pause)//use AudioListener.pause as it is an easily accessible singleton & it is set when the app is in pause too
//            //{
//            //    verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, k_GroundingSpeed * Time.deltaTime);
//            //    if (Mathf.Approximately(verticalTargetPosition.y, 0f))
//            //    {
//            //        character.animator.SetBool(s_JumpingHash, false);
//            //        m_Jumping = false;
//            //    }
//            //}

//            float correctJumpLength = runDistance;
//            Vector3 newPos = characterTransform.localPosition;
//            float ratio = newPos.y / jumpHeight * 4.0f;

//            if (Mathf.Approximately(newPos.y, 0.0f))
//            {
//                isGoingUp = true;
//            }

//            if (isGoingUp)
//            {
//                newPos.y = characterTransform.localPosition.y + (jumpHeight * dTime);
//                if (newPos.y >= jumpHeight)
//                {
//                    newPos.y = jumpHeight;
//                    isGoingUp = false;

//                    currentVehicleControlScript = null;
//                    characterColliderScript.SetTrigger(true);
//                }
//            }
//            else
//            {
//                newPos.y = characterTransform.localPosition.y + (-jumpHeight * dTime);
//                if (newPos.y <= 0.0f)
//                {
//                    newPos.y = 0.0f;
//                    isJumping = false;
//                }
//            }

//            characterTransform.localPosition = newPos;
//        }

//        //float dTime = laneChangeSpeed * TrackManager.Instance.laneOffset * _deltaTime / runDistance;
//        newLaneChangeTargetPosition.y = characterTransform.localPosition.y;
//        characterTransform.localPosition = Vector3.MoveTowards(characterTransform.localPosition, newLaneChangeTargetPosition, dTime);

//        // Move pivot forward
//        float scaledSpeed = runDistance * _deltaTime;

//        Vector3 newForwardTargetPosition = forwardTargetPosition;
//        bool needRecenter = transform.position.sqrMagnitude > TrackManager.Instance.k_FloatingOriginThreshold;
//        if (needRecenter)
//        {
//            TrackManager.Instance.ReCenterTracks(transform.position);
//            forwardTargetPosition = newForwardTargetPosition -= transform.position;
//            transform.position = Vector3.zero;
//        }

//        if (autoForward)
//        {
//            Vector3 movingDirection = transform.forward;
//            movingDirection *= scaledSpeed;
//            transform.position = transform.position + movingDirection;
//            forwardTargetPosition = transform.position;
//        }
//        else
//        {
//            transform.position = Vector3.MoveTowards(transform.position, newForwardTargetPosition, laneChangeSpeed * _deltaTime);
//        }

//        totalWorldTravelDistance += scaledSpeed;

//        CheckPlayerPositionOnTrack();
//    }

//    void CheckInput ()
//    {
//        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
//        //if (moveDirection.x)
//#if UNITY_EDITOR || UNITY_STANDALONE
//        // Use key input in editor or standalone
//        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()))
//        {
//            ChangeLane(-1);
//        }
//        else if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()))
//        {
//            ChangeLane(1);
//        }
//        else if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            if (isJumping)
//            {
//                return;
//            }
//            forwardTargetPosition = new Vector3(0.0f, 0.0f, transform.position.z + runDistance);
//            Jump();
//        }
//        else if (Input.GetKeyDown(KeyCode.DownArrow))
//        {
//            //if (!m_Sliding)
//            //Slide();
//        }
//#else
//        switch(characterControlType)
//        {
//            case ControlType.Swipe:
//                SwipeControl();
//                break;
//            case ControlType.Tap:
//                TapControl();
//                break;
//        }

//#endif
    //}

    //void SwipeControl ()
    //{
    //    if (Input.touchCount == 1)
    //    {
    //        if (isSwiping)
    //        {
    //            Vector2 diff = Input.GetTouch(0).position - startingTouchPosition;

    //            // Put difference in Screen ratio, but using only width, so the ratio is the same on both
    //            // axes (otherwise we would have to swipe more vertically...)
    //            diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

    //            if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
    //            {
    //                if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
    //                {
    //                    if (diff.y < 0)
    //                    {
    //                        //Slide();
    //                    }
    //                    else
    //                    {
    //                        forwardTargetPosition = new Vector3(0.0f, 0.0f, transform.position.z + runDistance);
    //                        Jump();
    //                    }
    //                }
    //                else
    //                {
    //                    if (diff.x < 0)
    //                    {
    //                        ChangeLane(-1);
    //                    }
    //                    else
    //                    {
    //                        ChangeLane(1);
    //                    }
    //                }

    //                isSwiping = false;
    //            }
    //        }

    //        // Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
    //        // a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
    //        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
    //        {
    //            if (Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                startingTouchPosition = Input.GetTouch(0).position;
    //                isSwiping = true;
    //            }
    //            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
    //            {
    //                isSwiping = false;
    //            }
    //        }
    //    }
    //}

    //void TapControl ()
    //{
    //    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        float halfWidth = Screen.width * 0.5f;
    //        Debug.LogError(touch.position);
    //        if (touch.position.x < halfWidth)
    //        {
    //            ChangeLane(-1);
    //        }
    //        else
    //        {
    //            ChangeLane(1);
    //        }
    //    }
    //}

    //public void ChangeLane (int _direction)
    //{
    //    if (isJumping)
    //    {
    //        return;
    //    }

    //    int targetLane = currentLane + _direction;

    //    if (targetLane < 0)
    //    {
    //        targetLane = 0;
    //    }
    //    else if (targetLane > 2)
    //    {
    //        targetLane = 2;
    //    }

    //    currentLane = targetLane;
    //    laneChangeTargetPosition = new Vector3((currentLane - 1) * TrackManager.Instance.laneOffset, 0.0f, 0.0f);
    //    //forwardTargetPosition = new Vector3(0.0f, 0.0f, transform.position.z + runDistance);
    //    float speed = runDistance;
    //    if (currentVehicleControlScript != null)
    //    {
    //        speed = currentVehicleControlScript.speed * 0.5f;
    //    }
    //    forwardTargetPosition = new Vector3(0.0f, 0.0f, transform.position.z + speed);
    //    Jump();
    //}

    //public void Jump ()
    //{
    //    if (isJumping)
    //    {
    //        return;
    //    }
    //    //if (m_Sliding)
    //    //StopSliding();

    //    //float correctJumpLength = jumpLength;//jumpLength * (1.0f + trackManager.speedRatio);
    //    jumpStart = totalWorldTravelDistance;

    //    //float animSpeed = k_TrackSpeedToJumpAnimSpeedRatio * (trackManager.speed / correctJumpLength);
    //    //character.animator.SetFloat(s_JumpingSpeedHash, animSpeed);
    //    //character.animator.SetBool(s_JumpingHash, true);
    //    //m_Audio.PlayOneShot(character.jumpSound);
    //    isJumping = true;
    //}

    //public void StopJumping ()
    //{
    //    if (isJumping)
    //    {
    //        //character.animator.SetBool(s_JumpingHash, false);
    //        isJumping = false;
    //    }
    //}

    //void CheckPlayerPositionOnTrack ()
    //{
    //    TrackSegment trackSegment = TrackManager.Instance.FindCurrentSegmentAt(transform.position);
    //    if (trackSegment == null || trackSegment.Equals(currentTrackSegment))
    //    {
    //        return;
    //    }

    //    TrackManager.Instance.RemoveTrackSegement(trackSegment.segmentIndex - 2);
    //    currentTrackSegment = trackSegment;
    //}

    //void RemoveUnusedSegment ()
    //{

    //}
}
