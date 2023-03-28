using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    public GameObject body;
    Rigidbody rb;

    private bool isGrounded;
    private bool touchIngr;
    public bool holdingItem = false;
    public bool ovenOnReach = false;

    public GameObject targetIngredient;
    public GameObject targetOven;

    public Transform cam;

    

    void Awake()
    {        
        rb = body.GetComponent<Rigidbody>();

    }

  

    // Update is called once per frame
    void FixedUpdate()
    {
        
       
        isGrounded = Physics.Raycast(body.transform.position, Vector3.down, 1f);
       
        CheckRaycast();

        //transform.position = body.transform.position;
       
    }



    public void UpdateItemsHeld()
    {
        bool ovenInReach = true;
        if (TryGetGameItems(ovenInReach, out bool collidingWithIngredients))
        {

        }
        else
        {

        }
    }

    public bool IsOvenInReach()
    {
        return true;
    }

    public bool TryGetGameItems(bool ovenInReach, out bool collidingWithIngredients)
    {
        bool foundIngredients = true;
        if (foundIngredients)
        {
            ovenInReach = true;
            collidingWithIngredients = true;
            return true;
        }
        else
        {
            ovenInReach = false;
            collidingWithIngredients = false;

            return false;
        }
    }

    public GameObject CheckRaycast()
    {
        
        // make a method to reset variables 

        ovenOnReach = false;
        touchIngr = false;
        targetIngredient = null;
        targetOven = null;

        RaycastHit hitIngredient, hitOven;

        Debug.DrawRay(body.transform.position - new Vector3(0, 0.4f,0), body.transform.forward, Color.blue);
        

        if(Physics.Raycast((body.transform.position), body.transform.forward, out hitOven, 5f, LayerMask.GetMask("Stuff")))
        {            
            if (hitOven.collider.CompareTag("Oven"))
            {
                //Debug.Log("inside");
                ovenOnReach = true;
                return targetOven;
            }
        }
        

        if (Physics.Raycast(body.transform.position, body.transform.forward, out hitIngredient, 1f))
        {
            if (hitIngredient.collider.CompareTag("Ingredients"))
            {
                touchIngr = true;
                targetIngredient = hitIngredient.collider.gameObject;    
                return targetIngredient;
            }     

        }

       return null;
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(targetIngredient);
        }

       
    }

    public void Move(float moveInputZ, float moveInputX)
    {

        if (isGrounded) {
                   
            if(moveInputZ != 0) 
            {
                   
                Vector3 movingFoward = moveInputZ * ( body.transform.forward) * movementSpeed;

                Vector3 newMovement = new Vector3(movingFoward.x, rb.velocity.y, movingFoward.z);

                rb.velocity = newMovement;

            }
            else if(moveInputX != 0)
            {
                Vector3 newMovement = new Vector3(moveInputX * movementSpeed, rb.velocity.y, rb.velocity.z);

                rb.velocity = newMovement;

            }

            void LocalFunction() { }

            

        }
    }

    public void Turn(float rotateInput)
    {
        if (rotateInput == 0)
        {                       
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            
            rb.angularVelocity = new Vector3(0f, rotateInput * rotationSpeed, 0f);
        }
    }

    public void GrabIngr()
    {

        if (holdingItem == true && targetIngredient != null)
        {
           
            Destroy(targetIngredient.GetComponent<FixedJoint>());
            
            holdingItem = false;
            
        }


        
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(body.transform.position, body.transform.forward * 50f);
    }
        
}

