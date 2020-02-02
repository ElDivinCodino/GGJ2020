using UnityEngine;
using UnityEngine.AI;

public class RoombaAI : MonoBehaviour
{
    public Transform[] destinations;
    public Transform body;
    public float speedRotation;
    public float indecisionSeconds;

    NavMeshAgent navMeshAgent;
    bool isMoving = false;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 0f, indecisionSeconds);
    }

    private void Update()
    {
        if (isMoving)
        {
            body.Rotate(Vector3.up, speedRotation * Time.deltaTime);
        }
        if (navMeshAgent.remainingDistance < 0.5f)
            SetDestination();
    }

    private void SetDestination()
    {
        Transform destination = destinations[Random.Range(0, destinations.Length)];
        if (destination != null) {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
            navMeshAgent.enabled = true;
            isMoving = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collisione");
        if (col.gameObject.name != "Roomba Agent" && col.gameObject.name != "Floor" && col.gameObject.name != "Floor2")
        {
            //Destroy(col.gameObject);
            if (col.gameObject.GetComponent<Rigidbody>() != null)
            {
                col.gameObject.GetComponent<Rigidbody>().AddForce(col.gameObject.transform.up * 300);
            }
        }
        if (col.gameObject.tag == "Sock")
        {
            Debug.Log("COLLIDE!");
        }
    }

}
