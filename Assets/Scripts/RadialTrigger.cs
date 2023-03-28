using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialTrigger : MonoBehaviour
{
    public float radius = 1f;

    public Transform Obj;

    void Start()
    {
        
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Handles.color = Color.red;

        Vector3 objPost = Obj.position;

        Vector3 disp = objPost - origin;

        



        float distance = Mathf.Sqrt(disp.x * disp.x + disp.y * disp.y);


        
        

        if(distance < radius )
        {
            Handles.color = Color.green;
        }

        Handles.DrawWireDisc(origin, Vector3.forward, radius);
    }
#endif

}
