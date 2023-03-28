using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    Material material;
    Color colorStart;
    Color colorEnd;

    private bool IsCooking;
    private bool isTouchingOven;
    private bool isTouchingPlayer;
    float timeElapsed;

    GameObject player;
    GameObject playerBody;
    PlayerController playerController;


    GameObject touchingWhat;

    float intervalAction = 3f;
    float lastInputTime = 0f;


    private void Awake()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerBody = GameObject.Find("PlayerBody");
    }

    private void Start()
    {        
        material = GetComponent <MeshRenderer>().material;
        
        colorStart = material.color;

        IsCooking = false;
        timeElapsed = 0f;
    }

    private void FixedUpdate()
    {
        CookingIngredient(IsCooking, gameObject.name);
       // AddOrRemoveParent();
    }

    private void OnCollisionEnter(Collision collision)
    {    
        if(collision.collider.CompareTag("Oven"))
        {

           Debug.Log("colliding with oven");
          //  IsCooking = true;
            isTouchingOven = true;
            touchingWhat = collision.gameObject;
        }
        

        if (collision.collider.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        IsCooking = false;
        isTouchingOven = false;
        isTouchingPlayer = false;
        touchingWhat = null;
       
    }



    private void CookingIngredient(bool isCooking, string Ingredient) 
    {
        if (isCooking && gameObject.name == "tomato") {
            timeElapsed += Time.deltaTime;
            float duration = 5f;
            float t = timeElapsed / duration;            

            material.color = Color.Lerp(colorStart, Color.black, t);
        }

        if (isCooking && gameObject.name == "cucumber")
        {
            timeElapsed += Time.deltaTime;
            float duration = 10f;
            float t = timeElapsed / duration;

            material.color = Color.Lerp(colorStart, Color.black, t);
        }
    }

    private void AddJoint()
    {
        if (gameObject.GetComponent<FixedJoint>() == null)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.breakForce = 10f;
        }
    }


    public void Attach()
    {

        Debug.Log("holding item " + playerController.holdingItem + ", is touching player " + isTouchingPlayer + ", player target " + playerController.targetIngredient + ", oven on reach " + playerController.ovenOnReach);

        if ((playerController.holdingItem == true && playerController.ovenOnReach == true))
        {
           

            FixedJoint fj = playerController.targetIngredient.GetComponent<FixedJoint>();

            fj.connectedBody = playerController.targetOven.GetComponent<Rigidbody>();

            playerController.targetIngredient.transform.position = touchingWhat.transform.position + touchingWhat.transform.up;
                         
            return;
        }

        else if (playerController.holdingItem == true && playerController.ovenOnReach == false)
        {
            Debug.Log("release");
            Destroy(playerController.targetIngredient.GetComponent<FixedJoint>());
            playerController.holdingItem = false;

            return;
        }


        else if (playerController.targetIngredient == gameObject.CompareTag("Ingredients") && playerController.holdingItem == false)
        {
            Debug.Log("pick");
            playerController.targetIngredient.transform.position = playerBody.transform.position + playerBody.transform.forward;
            playerController.targetIngredient.transform.rotation = playerBody.transform.rotation;

           // transform.position = playerBody.transform.position + playerBody.transform.forward;
          //  transform.rotation = player.transform.rotation;
            
            Rigidbody rbPlayer = playerBody.GetComponent<Rigidbody>();
            Rigidbody rb = playerController.targetIngredient.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            if (playerController.targetIngredient.GetComponent<FixedJoint>() == null)
            {
                playerController.targetIngredient.AddComponent<FixedJoint>();
            }
          
            FixedJoint fj = playerController.targetIngredient.GetComponent<FixedJoint>();
            fj.connectedBody = rbPlayer.GetComponent<Rigidbody>();

            playerController.holdingItem = true;
                
            return;
            }       

    }

    



    private void AddOrRemoveParent()
    {
        if(gameObject.GetComponent<FixedJoint>() == null)
        {
            transform.parent = null;
        }

        if (gameObject.GetComponent<FixedJoint>())
        {
           FixedJoint joint = gameObject.GetComponent<FixedJoint>();
           GameObject go = joint.connectedBody.gameObject;
           transform.parent = go.transform;
        }
    }
}
