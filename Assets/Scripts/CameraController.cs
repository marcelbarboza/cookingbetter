using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject playerBody;
    Vector3 distance;
    Vector3 playerDir;
    Vector3 playerDirectionWorld;
    Vector3 cameraForward;
    Vector3 cameraRight;

    GameObject playerTarget;

    private void Awake()
    {
        playerBody = GameObject.Find("PlayerBack");
        distance = new Vector3(0, 10, -10);
        playerTarget = GameObject.Find("PlayerTarget");

    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (playerBody == null)
        {
            return;
        }

        Vector3 playerPos = playerBody.transform.localPosition;

        Vector3 offset = new Vector3(0f, 10f, 10f); 

        Vector3 LocalToWorld(Vector3 objectPos)
        {
            Vector3 cameraForward = transform.forward;
            Vector3 cameraRight = transform.right;

            Vector3 playerOffSet = cameraRight * objectPos.x + cameraForward * objectPos.z;



            return  playerOffSet;
        }

        Vector3 playerPosWorld = LocalToWorld(playerPos);

        
        Vector3 playerDir = playerBody.transform.forward;
        Vector3 downAxis = transform.position - playerBody.transform.position;


        //transform.right = (transform.position - playerPos).normalized;

        //  transform.right = downAxis;
        transform.forward = playerDir;

        //ddd transform.position = playerPosWorld - () ;

        */

        transform.position = playerBody.transform.position + new Vector3(0,10f,0);

        

        transform.LookAt(playerTarget.transform);

    }

    

    private void OnDrawGizmos()
    {
        if (playerBody == null)
        {
            return;
        }

        Vector3 playerPos = playerBody.transform.localPosition;

        Vector3 LocalToWorld(Vector3 objectPos)
        {
            Vector3 cameraForward = transform.forward;
            Vector3 cameraRight = transform.right;

            Vector3 playerOffSet = cameraRight * objectPos.x + cameraForward * objectPos.z;



            return transform.position + playerOffSet;
        }

        Vector3 playerPosWorld = LocalToWorld(playerPos);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(playerPosWorld, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(playerPos, 1f);

       
    }


}
