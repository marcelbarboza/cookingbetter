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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ingredients"))
        {
            ingredient = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ingredient = null;
    }

}
