using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{    
    private int ingredientsCount;
    public int maxIngredients;

    GameObject boxTomato;
    GameObject boxCucumber;

    List<GameObject> ingredients;

    public Mesh[] meshs;
    public Material[] materials;

    void Awake()
    {
        boxTomato = GameObject.Find("boxTomato");
        boxCucumber = GameObject.Find("boxCucumber");

        ingredients = new List<GameObject>();
    }

    private void Start()
    {
        
        ingredientsCount = 0;
      //  StartCoroutine(SpawnIngredient("tomato", maxIngredients));
       // StartCoroutine(SpawnIngredient("cucumber", maxIngredients));
        StartCoroutine(SpawnIngredientPROPERLY_DONE_M8("cucumber", meshs[1], materials[1], maxIngredients, 10f, boxCucumber));
        StartCoroutine(SpawnIngredientPROPERLY_DONE_M8("tomato", meshs[0], materials[0], maxIngredients, 15f, boxTomato));
    }

    // Update is called once per frame
    void Update()
    {
        if((boxTomato != null) && (ingredientsCount < maxIngredients))
        {
           /* for(int i = 0; i < maxIngredients; i++) {                            
                newIngredient[ingredientsCount] = Instantiate(ingredients[0]);
                newIngredient[ingredientsCount].transform.parent = box.transform;
                newIngredient[ingredientsCount].transform.position = box.transform.position + Vector3.up;
                ingredientsCount++;
            }*/

           /* for (int i = 0; i < maxIngredients; i++)
            {
               
                GameObject ingredient = new GameObject("banana " + i, typeof(IngredientChooser));
                // with you remove typeof, then you need to add ingredient.AddComponent<IngredientChooser>();
                IngredientChooser ingredientChooser = ingredient.GetComponent<IngredientChooser>();
                ingredientChooser.SetIngredientType("tomato");

                // ingredient.transform.parent = box.transform;
                //newIngredient[ingredientsCount] = ingredient;

                ingredientsCount++;

            }*/
            for (int i = 0; i < maxIngredients; i++)
            {

                //SpawnIngredient("tomato", i);
                ingredientsCount++;
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach(GameObject go in newIngredients)
            {
                Debug.Log(go.name);
            }
        }
        */
        

    }

    void SpawnIngredients(string ingredient, int amount)
    {
        if(ingredient == "tomato")
        {                      
            GameObject newIngr = new GameObject("tomato " + amount);
            
            newIngr.AddComponent<MeshFilter>().mesh = meshs[0];
            newIngr.AddComponent<MeshRenderer>().material = materials[0];
            
            newIngr.AddComponent<BoxCollider>();
            newIngr.transform.position = (boxTomato.transform.position + Vector3.up) + (UnityEngine.Random.onUnitSphere);
            Rigidbody rb = newIngr.AddComponent<Rigidbody>();
            rb.mass = 0.3f;
            newIngr.tag = "Ingredients";
           
            //if you parent you will deform the mesh , or to keep it parented i have to keep the scale uniform on the parent (1,1,1) or (4,4,4), not (1,3,1)
           // newIngr.transform.parent = box.transform;                    
           
            newIngr.AddComponent<IngredientController>();

            ingredients.Add(newIngr);
        }
    }

    IEnumerator SpawnIngredient(string ingredient, int amount) {

        yield return new WaitForSeconds(1f);

        if (ingredient == "tomato")
        {
            for (int i = 0; i < amount; i++)
            {

                GameObject newIngr = new GameObject("tomato");
                newIngr.AddComponent<MeshFilter>().mesh = meshs[0];
                newIngr.AddComponent<MeshRenderer>().material = materials[0];
                newIngr.AddComponent<BoxCollider>();

                newIngr.transform.position = (boxTomato.transform.position + new Vector3(0f, 2f, 0f)) + (UnityEngine.Random.onUnitSphere);

                Rigidbody rb = newIngr.AddComponent<Rigidbody>();
                rb.mass = 0.01f;
                newIngr.tag = "Ingredients";

                //if you parent you will deform the mesh , or to keep it parented i have to keep the scale uniform on the parent (1,1,1) or (4,4,4), not (1,3,1)
                // newIngr.transform.parent = box.transform;

                newIngr.AddComponent<IngredientController>();

                ingredients.Add(newIngr);

                yield return new WaitForSeconds(1f);

            }            
        }

        if (ingredient == "cucumber")
        {
            for (int i = 0; i < amount; i++)
            {

                GameObject newIngr = new GameObject("cucumber");
                newIngr.AddComponent<MeshFilter>().mesh = meshs[1];
                newIngr.AddComponent<MeshRenderer>().material = materials[1];
                newIngr.AddComponent<BoxCollider>();

                newIngr.transform.position = (boxCucumber.transform.position + new Vector3(0f, 2f, 0f)) + (UnityEngine.Random.onUnitSphere);

                Rigidbody rb = newIngr.AddComponent<Rigidbody>();
                rb.mass = 0.01f;
                newIngr.tag = "Ingredients";
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                //if you parent you will deform the mesh , or to keep it parented i have to keep the scale uniform on the parent (1,1,1) or (4,4,4), not (1,3,1)
                // newIngr.transform.parent = box.transform;

                newIngr.AddComponent<IngredientController>();

                ingredients.Add(newIngr);

                yield return new WaitForSeconds(1f);

            }
        }
    }

    IEnumerator SpawnIngredientPROPERLY_DONE_M8(string name, Mesh mesh, Material material, int amount, float cookingDuration, GameObject spawnObject)
    {

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < amount; i++)
        {

            GameObject ingredient = new GameObject(name);
            MeshFilter meshFilter = ingredient.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = ingredient.AddComponent<MeshRenderer>();
            Rigidbody rb = ingredient.AddComponent<Rigidbody>();
            IngredientController ingredientController = ingredient.AddComponent<IngredientController>();


            ingredient.AddComponent<BoxCollider>();

            ingredientController.cookingDuration = cookingDuration;


            meshFilter.mesh = mesh;    
            meshRenderer.material = material;
            
            ingredient.transform.position = spawnObject.transform.position + new Vector3(0f, 2f, 0f) + UnityEngine.Random.onUnitSphere;

            rb.mass = 0.01f;

            ingredient.tag = "Ingredients";

            ingredients.Add(ingredient);

            yield return new WaitForSeconds(1f);

        }
    }
}
