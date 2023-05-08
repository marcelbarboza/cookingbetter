using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class InputManager : MonoBehaviour
{
    
    float moveInputZ;
    float moveInputX;
    float rotateInput;

    PlayerController playerController;
    GameObject playerManager;

    public VariableJoystick variableJoystick;


    GameObject[] ovens;
    GameObject buttonObject;

    GameObject[] Ingredients;

    float lastInputTime = 0f;
    float intervalAction = 0.5f;

    bool isRunning = false;

    void Awake()
    {
        

        playerManager = GameObject.Find("Player Manager");
        playerController = playerManager.GetComponent<PlayerController>();        
        
    }

    private void Start()
    {
        Scene sceneUI = SceneManager.GetSceneByName("UI");

        if (sceneUI.isLoaded)
        {
            variableJoystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
            buttonObject = GameObject.Find("ActionButton");
        }

        Button button = buttonObject.GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }



    private void FixedUpdate()
    {
        moveInputZ = Input.GetAxis("Vertical");
        moveInputX = Input.GetAxis("Horizontal");
        rotateInput = Input.GetAxis("Turning");
        playerController.Move(moveInputZ, moveInputX);
        playerController.Turn(rotateInput);

        Ingredients = GameObject.FindGameObjectsWithTag("Ingredients");
        ovens = GameObject.FindGameObjectsWithTag("Oven");


        if (Input.GetKeyDown(KeyCode.M))
        {
            playerController.PushBackTest();
        }

         
        playerController.TouchMove(variableJoystick.Horizontal, variableJoystick.Vertical);
        
        if(variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0)
        {
       //     Debug.Log(variableJoystick.Horizontal + " " + variableJoystick.Vertical);
        }

         if(variableJoystick == null)
        {
            Debug.Log("variable joystick is null");
        }

    }

    private void Update()
    {     
          
        if (buttonObject == null)
        {
            Debug.Log("button null");
        }

        /*
        if (Input.GetButtonDown("Fire1"))
        {
      //  Debug.Log("click");
        playerController.GrabIngr();

            foreach (GameObject oven in ovens)
            {

                OvenController ovenController = oven.GetComponent<OvenController>();

                if (isRunning == false)
                {
                   // Debug.Log("attach");
                    isRunning = true;
                    ovenController.AttachIngredient();
                }
            }

        lastInputTime = Time.time;

        }


        if (Time.time - lastInputTime > intervalAction)
        {
            isRunning = false;
        }
        */
    }

    private void OnButtonClick()
    {
        playerController.GrabIngr();

        foreach (GameObject oven in ovens)
        {

            OvenController ovenController = oven.GetComponent<OvenController>();

            
                // Debug.Log("attach");
                
                ovenController.AttachIngredient();
            
        }


    }

}


