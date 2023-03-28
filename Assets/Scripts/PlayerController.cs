using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    public GameObject player;
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
        rb = player.GetComponent<Rigidbody>();
    }

  

    // Update is called once per frame
    void FixedUpdate()
    {
        
       
        isGrounded = Physics.Raycast(player.transform.position, Vector3.down, 1f);
       
        CheckRaycast();

        //transform.position = body.transform.position;
       
    }



    public void UpdateItemsHeld()
    {
        
        if (TryGetGameItems(out bool ovenInReach, out bool collidingWithIngredients))
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

    public bool TryGetGameItems(out bool ovenInReach, out bool collidingWithIngredients)
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

        RaycastHit hitIngredient1, hitIngredient2, hitOven;

        Debug.DrawRay(player.transform.position - new Vector3(0, 0.4f,0), player.transform.forward, Color.blue);
        

        
        

        if (Physics.Raycast(player.transform.position, player.transform.forward, out hitIngredient1, 2f))
        {
            if (hitIngredient1.collider.CompareTag("Ingredients"))
            {
                touchIngr = true;
                targetIngredient = hitIngredient1.collider.gameObject;
                Debug.Log("on target");
                return targetIngredient;
            }     
        }
        if (Physics.Raycast(player.transform.position + new Vector3(0f, 0.5f, 0f), player.transform.forward, out hitIngredient2, 2f))
        {
            if (hitIngredient2.collider.CompareTag("Ingredients"))
            {
                touchIngr = true;
                targetIngredient = hitIngredient2.collider.gameObject;
                Debug.Log("on target");
                return targetIngredient;
            }
        }

        if (Physics.Raycast((player.transform.position), player.transform.forward, out hitOven, 5f, LayerMask.GetMask("Stuff")))
        {
            if (hitOven.collider.CompareTag("Oven"))
            {
                //Debug.Log("inside");
                ovenOnReach = true;
                return targetOven;
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
                   
                Vector3 movingFoward = moveInputZ * ( player.transform.forward) * movementSpeed;

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

        if (holdingItem == false && targetIngredient != null)
        {
            if (targetIngredient.GetComponent<FixedJoint>())
            {
                Destroy(targetIngredient.GetComponent<FixedJoint>());
            }
            
            targetIngredient.transform.position = player.transform.position + player.transform.forward;
            targetIngredient.transform.rotation = player.transform.rotation;

            // transform.position = playerBody.transform.position + playerBody.transform.forward;
            //  transform.rotation = player.transform.rotation;

            
            Rigidbody ingredientRb = targetIngredient.GetComponent<Rigidbody>();
            ingredientRb.isKinematic = false;

            if (targetIngredient.GetComponent<FixedJoint>() == null)
            {
                targetIngredient.AddComponent<FixedJoint>();
            }

            FixedJoint ingredientfj = targetIngredient.GetComponent<FixedJoint>();
            ingredientfj.connectedBody = rb.GetComponent<Rigidbody>();

            holdingItem = true;
        }


        


        
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(player.transform.position, player.transform.forward * 50f);
    }
        
}

