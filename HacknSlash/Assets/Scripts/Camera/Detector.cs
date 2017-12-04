using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Camera cam;

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.transform.name == "Player")
        {
            //print(transform.parent.Find("Camera"));
            CameraManager.instance.isOutside = false;
            cam.enabled = true;
            CameraManager.instance.cameraPlayerOutside.enabled = false;
            CameraManager.instance.player.cam = cam;
            //CameraManager.instance.switchCamera(camera);
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.transform.name == "Player")
        {
            CameraManager.instance.isOutside = false;
            cam.enabled = true;
            CameraManager.instance.cameraPlayerOutside.enabled = false;
            CameraManager.instance.player.cam = cam;
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.transform.name == "Player")
        {
            CameraManager.instance.isOutside = true;
            cam.enabled = false;
            CameraManager.instance.cameraPlayerOutside.enabled = true;
        }
    }
}
