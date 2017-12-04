using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {
    
    public GameObject player;
    public float speed = 3.0f;
    private Vector3 m_endPoint;

    private const float DISTANCE_THRESHOLD = 6.0F;

	void Start () {
	    if(this.player != null) {
			this.m_endPoint = new Vector3(player.transform.position.x, (player.transform.position.y + 1.23f), player.transform.position.z);
        }
	}

	void Update () {
	    if (this.player != null) {
            if(Vector3.Distance(this.player.transform.position, transform.position) > DISTANCE_THRESHOLD) {
                transform.position = Vector3.Slerp(transform.position, this.m_endPoint, Time.deltaTime);
            }

            if(transform.position != this.m_endPoint) {
                transform.position = Vector3.Slerp(transform.position, this.m_endPoint, speed * Time.deltaTime);
            }

			this.m_endPoint = new Vector3 (player.transform.position.x, (player.transform.position.y + 1.23f), player.transform.position.z);
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
