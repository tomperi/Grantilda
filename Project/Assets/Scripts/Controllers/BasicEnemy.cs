using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public float lookRadius = 5f;

    public Transform target;
    private NavMeshAgent agent;
    private NavMeshPath path;
	
    // Use this for initialization
	void Start ()
	{
	    agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    float distance = Vector3.Distance(target.position, transform.position);

	    if (distance <= lookRadius)
	    {
	        agent.SetDestination(target.position);

	        if (distance <= agent.stoppingDistance)
	        {
	            // FaceTarget();
            }
	    }
	}

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void GoToPosition(Vector3 destination)
    {
        agent.CalculatePath(destination, path);
    }

    public void StopNavAgent()
    {
        agent.enabled = false;
    }

    public void StartNavAgent()
    {
        agent.enabled = true;
    }

    public void StopAtPlace()
    {
        GoToPosition(transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
