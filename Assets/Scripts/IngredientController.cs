using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public Material material;
    public Color colorStart;


    private bool IsCooking;
    private bool isTouchingOven;
    private bool isTouchingPlayer;
    

    GameObject player;
    GameObject playerBody;
    PlayerController playerController;


    GameObject touchingWhat;

    float intervalAction = 3f;
    float lastInputTime = 0f;

    public delegate void CookingEvent(object sender, EventArgs e);
    
    public float cookingDuration { get; set; }
    public float howCooked { get; set; }
    public float cookedTime { get; set; }
    public float timeElapsed { get; set; }



    private void Awake()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerBody = GameObject.Find("PlayerBody");
    }

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        colorStart = material.color;

        IsCooking = false;
        
    }

    private void Update()
    {
        //  CookingIngredient(IsCooking, gameObject);
        // AddOrRemoveParent();
        HowCooked();
        
    }

    public float HowCooked()
    {
        return howCooked = Mathf.Lerp(0, cookedTime, cookingDuration);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Oven"))
        {

            //  Debug.Log("colliding with oven");
            //  IsCooking = true;
            isTouchingOven = true;
            touchingWhat = collision.gameObject;
        }


        if (collision.collider.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    public Material Material
    {
        get { return GetComponent<MeshRenderer>().material; }
        set { GetComponent<MeshRenderer>().material = value; }
    }

    

    private void OnCollisionExit(Collision collision)
    {
        IsCooking = false;
        isTouchingOven = false;
        isTouchingPlayer = false;
        touchingWhat = null;
       
    }



    //public void CookingIngredient(bool isCooking, GameObject Ingredient) 
    //{
    //    if (isCooking && Ingredient.name == "tomato") {
    //        timeElapsed += Time.deltaTime;
    //        float duration = 15f;
    //        float t = timeElapsed / duration;            

    //        material.color = Color.Lerp(colorStart, Color.black, t);
    //    }

    //    if (isCooking && Ingredient.name == "cucumber")
    //    {
    //        timeElapsed += Time.deltaTime;
    //        float duration = 10f;
    //        float t = timeElapsed / duration;

    //        material.color = Color.Lerp(colorStart, Color.black, t);
    //    }
    //}

    private void AddJoint()
    {
        if (gameObject.GetComponent<FixedJoint>() == null)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.breakForce = 10f;
        }
    }

    /*public float CookingDuration
    {
        get { return CookingDuration; }
        set { CookingDuration; }
    }*/


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
