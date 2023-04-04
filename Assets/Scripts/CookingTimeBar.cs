using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingTimeBar : MonoBehaviour
{
    public Slider slider;

    GameObject oven;
    OvenController ovenController;
    // Start is called before the first frame update
    void Awake()
    {
        slider = gameObject.GetComponentInChildren<Slider>();
        oven = GameObject.Find("Oven");
        ovenController = oven.GetComponent<OvenController>();

        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
