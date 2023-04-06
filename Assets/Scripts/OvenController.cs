using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    GameObject ingredient;

    public bool onOven;

    private bool itChanged;
   
    private void Update()
    {
        if (onOven != itChanged)
        {
            Debug.Log("onOven" + onOven);
            itChanged = onOven;
        }

        if (ingredient != null) 
        { 
        CookingIngredient(onOven, ingredient);
        }

        //Debug.Log(IngredientCooking);
    }

    public void AttachIngredient()
    {

        if (ingredient != null) 
        { 
            if(ingredient.GetComponent<FixedJoint>() == null)
            {
                ingredient.AddComponent<FixedJoint>();
            }
            Rigidbody ingredientRb = ingredient.GetComponent<Rigidbody>();
            ingredientRb.isKinematic = true;

            if(ingredient.name == "cucumber") 
            {
                ingredient.transform.rotation = Quaternion.Euler(0,0,90);
                ingredient.transform.position = gameObject.transform.position + Vector3.up;
                
            }
            if (ingredient.name == "tomato")
            {
                ingredient.transform.position = gameObject.transform.position + Vector3.up;
            }

            FixedJoint ingredientFj = ingredient.GetComponent<FixedJoint>();        
            ingredientFj.connectedBody = gameObject.GetComponent<Rigidbody>();

            //ingredientRb.isKinematic = false;
            onOven = true;

        }

        if(ingredient == null)
        {
            onOven = false;
        }
    }

    public GameObject IngredientCooking
    {
        get { return ingredient; }
        set { ingredient = value;
            //you can set the joint here
        }
    }

    public void CookingIngredient(bool isCooking, GameObject ingredientToCook)
    {
        //if(ingredient = null)
        //{
        //    return;
        //}

        
        IngredientController ingredientControl = ingredientToCook.GetComponent<IngredientController>();
        Color colorStart = ingredientControl.colorStart;
        Material material = ingredientToCook.GetComponent<MeshRenderer>().material;
        
        //Material ingredientMaterial = ingredientControl.Material;

      /*  if (isCooking && ingredient.name == "tomato")
        {          
            

            timeElapsed += Time.deltaTime;
            float duration = 15f;
            float t = Mathf.SmoothStep(0.0f, 1.0f, timeElapsed / duration);
            

            material.color = Color.Lerp(colorStart, Color.black, t);
        }



        if (isCooking && ingredient.name == "cucumber")
        {
            timeElapsed += Time.deltaTime;
            float duration = 10f;
            float t = Mathf.SmoothStep(0.0f, 1.0f, timeElapsed / duration);
            //float t = timeElapsed / duration;

            material.color = Color.Lerp(colorStart, Color.black, t);
        }*/

        if (isCooking)
        {
            ingredientControl.timeElapsed += Time.deltaTime;
            
            float t = Mathf.SmoothStep(0.0f, 1.0f, ingredientControl.timeElapsed / ingredientControl.cookingDuration);
            //float t = timeElapsed / duration;
            ingredientControl.cookedTime = t;

            
            Debug.Log(ingredientControl.howCooked);

            material.color = Color.Lerp(colorStart, Color.black, t);
        }
    }

   

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ingredients"))
        {
            IngredientCooking = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ingredient = null;
    }

}
