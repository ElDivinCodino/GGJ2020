using UnityEngine;
using UnityEngine.AI;

public class RoombaAI : MonoBehaviour
{
    //[SerializeField]
    public Transform destination;
    public Transform body;
    public float speedRotation;

    NavMeshAgent navMeshAgent;
    bool isMoving = false;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        SetDestination();
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
        if (destination != null) {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
            navMeshAgent.enabled = true;
            isMoving = true;
        }
    }
}
