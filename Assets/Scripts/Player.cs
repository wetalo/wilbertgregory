using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController myController;
    public enum CharacterState
    {
        isIdle,
        isMoving,
        isJumping,
        isDoubleJumping,
        isDropping
    }
    

    [Space]
    [Header("Gravity")]
    //Gravity
    public float gravity = -9.8f; //meters per second per second
    private float terminalYVelocity = -53f; //meters per second

    //State Machine
    private CharacterState currentState = CharacterState.isIdle;

    //movement
    private float myXVelocity = 0.0f;
    private float myZVelocity = 0.0f;
    private float myYVelocity = 0.0f;

    //walk
    public float movementSpeed = 20.0f; // 20 unit per sec
    private Vector3 moveDirection = Vector3.zero;


    [Header("Jumping")]
    //jump
    public float jumpYVelocity = 10f;
    public float jumpCancelYVelocity = 2f; //the up to which the jump can be cancel

    [Space]
    [Header("Player Input")]
    public int controllerNumber;

    private string jumpButton;
    public string horizontalAxis;
    public string verticalAxis;

    public string jumpButtonKeyboard;
    public string horizontalAxisKeyboard;
    public string verticalAxisKeyboard;
    //private string leftButton;
    //private string rightButton;

    public DebugText debugText;

    public GameManager gameManager;

    public int mistakesMade = 0;
    public int objectsGotten = 0;

    public float rotateSpeed = 20f;

    public GameObject model;

    //Setup the input strings
    private void Awake()
    {
        jumpButton = "joystick " + controllerNumber + " button 0";
    }
    // Use this for initialization
    void Start () {
        myController = GetComponent<CharacterController>();
        

    }
	
	// Update is called once per frame
	void Update () {
        Gravity();
        RunStates();
        UpdateText();

    }

    void LateUpdate()
    {
        //Set direction
        moveDirection = transform.TransformDirection(new Vector3(myXVelocity, myYVelocity, myZVelocity));

        //moveDirection = new Vector3(myXVelocity, myYVelocity, 0);

        //apply movement
        myController.Move(moveDirection * Time.deltaTime);
        
    }

    void Gravity()
    {
        if (!myController.isGrounded)
        {

            if (myYVelocity > terminalYVelocity)
            {
                myYVelocity += gravity * Time.deltaTime;
            }
            else
            {
                myYVelocity = terminalYVelocity;
            }

        }
        else
        {
            myYVelocity = -1;
        }
    }

    void RunStates()
    {

        //Start new state
        if ((Input.GetKeyDown(jumpButton) || Input.GetKeyDown()) && (currentState == CharacterState.isIdle || currentState == CharacterState.isMoving || currentState == CharacterState.isJumping))
        {
            if(currentState != CharacterState.isJumping)
            {
                StartJump();
            } else if(currentState == CharacterState.isJumping)
            {
                StartDoubleJump();
            }

        }
        else if ((Input.GetAxis(verticalAxis) < 0 || Input.GetAxis(verticalAxisKeyboard) < 0) && (currentState == CharacterState.isIdle || currentState == CharacterState.isMoving || currentState == CharacterState.isJumping || currentState == CharacterState.isDoubleJumping))
        {
            StartDrop();
        }



        else if (currentState == CharacterState.isIdle && (Input.GetAxis(horizontalAxis) != 0 || Input.GetAxis(horizontalAxisKeyboard) != 0))
        {
            StartMove();
        }

        Jump();
        Move();
        Drop();
    }

    void ResetStates()
    {

    }

    //Jump
    void StartJump()
    {
        ResetStates();
        currentState = CharacterState.isJumping;
        myYVelocity = jumpYVelocity;
        //myXVelocity = 0f;
    }

    void StartDoubleJump()
    {
        ResetStates();
        currentState = CharacterState.isDoubleJumping;
        myYVelocity = jumpYVelocity;
    }

    void Jump()
    {
        if (currentState == CharacterState.isJumping || currentState == CharacterState.isDoubleJumping)
        {
            //jump cancel
            if (!(Input.GetKey(jumpButton) || Input.GetKey(jumpButtonKeyboard) ) && myYVelocity > jumpCancelYVelocity)
            {
                myYVelocity = jumpCancelYVelocity;
            }

            //air control
            //Change X velocity
            if(Input.GetAxis(horizontalAxis) != 0)
            {
                myZVelocity = Input.GetAxis(horizontalAxis) * movementSpeed;
            } else if(Input.GetAxis(horizontalAxisKeyboard) != 0)
            {
                myZVelocity = Input.GetAxis(horizontalAxisKeyboard) * movementSpeed;
            } else
            {
                myZVelocity = 0;
            }


            RotateModel();

            //Exit condition
            if (myYVelocity < 0 && myController.isGrounded)
            {
                StopJump();
            }
        }
    }

    void StopJump()
    {
        currentState = CharacterState.isIdle;
        myYVelocity = 0;
    }

    //Walk
    void StartMove()
    {
        ResetStates();
        currentState = CharacterState.isMoving;
    }

    void Move()
    {

        if (currentState == CharacterState.isMoving)
        {
            //Walk Animation

            //Change X velocity
            if (Input.GetAxis(horizontalAxis) != 0)
            {
                myZVelocity = Input.GetAxis(horizontalAxis) * movementSpeed;
            }
            else if (Input.GetAxis(horizontalAxisKeyboard) != 0)
            {
                myZVelocity = Input.GetAxis(horizontalAxisKeyboard) * movementSpeed;
            }
            else
            {
                myZVelocity = 0;
            }
            RotateModel();

            

            //Exit condition
            if (Input.GetAxis(horizontalAxis) == 0 && Input.GetAxis(horizontalAxisKeyboard) == 0)
            {
                StopMove();
            }
        }
    }

    void StopMove()
    {
        currentState = CharacterState.isIdle;
    }

    void StartDrop()
    {
        ResetStates();
        currentState = CharacterState.isDropping;
    }

    void Drop()
    {
        if(currentState == CharacterState.isDropping)
        {
            if (!myController.isGrounded)
            {
                myYVelocity = terminalYVelocity;
                if (Input.GetAxis(horizontalAxis) != 0)
                {
                    myZVelocity = Input.GetAxis(horizontalAxis) * movementSpeed;
                }
                else if (Input.GetAxis(horizontalAxisKeyboard) != 0)
                {
                    myZVelocity = Input.GetAxis(horizontalAxisKeyboard) * movementSpeed;
                }
                else
                {
                    myZVelocity = 0;
                }
            }
            else
            {
                StopDrop();
            }
        }
    }

    void StopDrop()
    {
        myZVelocity = 0;
        currentState = CharacterState.isIdle;
    }

    

    void TouchedSameType(Enemy enemy) {
        //debugText.ChangeText("Touched same type");
        enemy.Defeated();
        gameManager.GotObject();
        objectsGotten++;

        UpdateText();
    }

    void TouchedOtherType(Enemy enemy)
    {
        //debugText.ChangeText("Touched other type");
        enemy.Defeated();
        gameManager.MistakeMade();
        mistakesMade++;

        UpdateText();
    }
    

    void RotateModel()
    {
        int rotateDirection = 1;


        if(Input.GetAxis(horizontalAxis) < 0 || Input.GetAxis(horizontalAxisKeyboard) < 0)
        {
            rotateDirection = -1;
        }

        model.transform.RotateAround(model.transform.position, Vector3.right, rotateSpeed * rotateDirection * Time.deltaTime);
        //model.transform.RotateAround(model.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void UpdateText()
    {
        if(debugText)
        debugText.ChangeText("Objects Gotten: " + objectsGotten + "\n Mistakes made: " + mistakesMade);
    }

}
