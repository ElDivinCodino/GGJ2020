using UnityEngine;
using UnityEngine.AI;

public class RoombaAI : MonoBehaviour
{
    public Transform[] destinations;
    public Transform body;
    public float speedRotation;

    NavMeshAgent navMeshAgent;
    bool isMoving = false;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 0f, 1.7f);
    }

    private void Update()
    {
        if (isMoving)
        {
            body.Rotate(Vector3.up, speedRotation * Time.deltaTime);
        }
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
}
