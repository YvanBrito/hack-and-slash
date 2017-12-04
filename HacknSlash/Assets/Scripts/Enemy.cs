using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour {

    private GameObject target;
    private Rigidbody m_rb;
    private Vector3 nextPos;
    private Vector3 facingDirection;
    private bool isThinking;
    private float timeTarget;
    private float nextAttack;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float attackSpeed;
	private NavMeshAgent pathfinder;

    public Vector3[] limitArea;
    public LayerMask collisionMask;

    void Start() {
        m_rb = GetComponent<Rigidbody>();
        nextPos = transform.position;
        isThinking = false;
    }

    void Update() {
        Debug.DrawRay(transform.position, transform.forward.normalized * 17, Color.yellow);

        if (target == null) {
            Patrolling();
        }
        else {
            Chasing();
        }
    }

    IEnumerator thinkOtherPos() {
        if (!isThinking) {
            isThinking = true;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            nextPos = new Vector3(Random.Range(limitArea[0].x, limitArea[1].x), transform.position.y, Random.Range(limitArea[0].z, limitArea[2].z));
            isThinking = false;
        }
    }

    void Patrolling() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 17, collisionMask)) {
            target = hit.transform.gameObject;
            timeTarget = Time.time;
        }
        else {
            if (Vector3.Distance(transform.position, nextPos) <= 1) {
                StartCoroutine(thinkOtherPos());
            }
            else {
                transform.LookAt(nextPos);
                transform.Translate(new Vector3(0, 0, 5 * Time.deltaTime));
            }
        }
    }

    void Chasing() {
        Vector3 posTarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        var targetRotation = Quaternion.LookRotation(posTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);

        if (Vector3.Distance(posTarget, transform.position) > attackRange) {
            transform.Translate(new Vector3(0, 0, 7 * Time.deltaTime));
            nextAttack = Time.time + attackRate;
        }
        else {
            Attacking();
        }

        Debug.Log(Time.time - timeTarget);
        if (Time.time - timeTarget >= 30) {
            target = null;
        }
    }

    void Attacking() {
        if(Time.time > nextAttack) {
            nextAttack = Time.time + attackRate;
            //StartCoroutine(animAttacking());
        }
    }

    IEnumerator animAttacking() {
        Vector3 originalPos = transform.position;
        Vector3 attackPosition = target.transform.position;// - new Vector3(0,0,5);

        float percent = 0;

        while (percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Slerp(originalPos, attackPosition, interpolation);

            yield return null;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (Vector3 o in limitArea) {
            Gizmos.DrawSphere(o, 0.5f);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(nextPos, 0.5f);
    }
}
