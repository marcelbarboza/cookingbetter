using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    float moveInputZ;
    float moveInputX;
    float rotateInput;

    PlayerController playerController;
    GameObject playerManager;

    GameObject[] ovens;


    GameObject[] Ingredients;

    float lastInputTime = 0f;
    float intervalAction = 0.5f;

    bool isRunning = false;

    void Awake()
    {
        playerManager = GameObject.Find("Player Manager");
        playerController = playerManager.GetComponent<PlayerController>();     
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


    }

    private void Update()
    {     
          
        if (Input.GetButtonDown("Fire1"))
        {
      //  Debug.Log("click");
        playerController.GrabIngr();


            /*
            foreach (GameObject ingredient in Ingredients)
                {           
                        
                        IngredientController ingredientController = ingredient.GetComponent<IngredientController>();
                    
                        if(isRunning == false)
                        {
                            Debug.Log("attach");
                            isRunning = true;
                            ingredientController.Attach();
                        }
                }

                lastInputTime = Time.time;

            }*/

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
    }
}
