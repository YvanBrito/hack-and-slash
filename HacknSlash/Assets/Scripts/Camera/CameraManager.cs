using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;
    public Camera current;
    public Camera cameraPlayerOutside;
    public PlayerMovement player;
    public bool isOutside;
    //public static Camera next;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        isOutside = true;
    }

    private void Update()
    {
        if (isOutside)
        {
            current = cameraPlayerOutside;
            cameraPlayerOutside.enabled = true;
            player.cam = cameraPlayerOutside;
        }
        else
        {

        }
    }

    public void switchCamera( Camera next )
    {
        //current.enabled = false;
        //next.enabled = true;
        current = next;
        print(next);
        //player.cam = next;
    }
}