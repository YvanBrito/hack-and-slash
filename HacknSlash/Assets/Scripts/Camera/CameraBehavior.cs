using UnityEngine;
using System.Collections;
//https://www.youtube.com/watch?v=ZiAdnLK7SF8
//https://www.youtube.com/watch?v=FoZwgRE5LYI
public class CameraBehavior : MonoBehaviour {

    public Transform m_Target;
    public float turnSpeed = 4;
    public float followSpeed = 3;
    public bool onlyLooking;
    Vector3 offset;

    private Collider[] col;
    
	void Start () {
        offset = transform.position - m_Target.position;
        col = GetComponentsInChildren<Collider>();
    }
	
	void LateUpdate () {
        if (!onlyLooking)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("RotateHor") * turnSpeed, Vector3.up) * offset;
            transform.position = m_Target.position + offset;
        }
        else
        {
            
        }

        transform.LookAt(m_Target);
    }
}
