using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    GameObject ingredient;

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
            ingredient.transform.position = gameObject.transform.position + Vector3.up;

            FixedJoint ingredientFj = ingredient.GetComponent<FixedJoint>();        
            ingredientFj.connectedBody = gameObject.GetComponent<Rigidbody>();

            ingredientRb.isKinematic = false;

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
        
    }

}
