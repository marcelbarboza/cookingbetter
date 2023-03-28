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
    GameObject player;
    

    GameObject[] Ingredients;

    float lastInputTime = 0f;
    float intervalAction = 0.5f;

    bool isRunning = false;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
               
        
        
    }

    

    private void FixedUpdate()
    {
        moveInputZ = Input.GetAxis("Vertical");
        moveInputX = Input.GetAxis("Horizontal");
        rotateInput = Input.GetAxis("Turning");
        playerController.Move(moveInputZ, moveInputX);
        playerController.Turn(rotateInput);

        Ingredients = GameObject.FindGameObjectsWithTag("Ingredients");

    }

    private void Update()
    {
        
            
            if (Input.GetButtonDown("Fire1"))
            {
            Debug.Log("click");
            // playerController.GrabIngr();



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

            }

        if (Time.time - lastInputTime > intervalAction)
        {
            isRunning = false;
        }
    }
}
