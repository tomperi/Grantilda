using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    
	public NavMeshAgent agent;

    private NavMeshPath currentPath;

    private void Start()
    {
        currentPath = new NavMeshPath();

        // Prevents the agent from rotating 
        agent.updateRotation = false;  
    }

    void Update () 
	{
	    if (currentPath.status == NavMeshPathStatus.PathComplete && agent.isActiveAndEnabled)
	    {
	        agent.SetPath(currentPath);
	    }

        if (Input.GetKeyDown("z"))
        {
            // Stop the player in place
            StopAtPlace();
        }
	}

    public void GoToPosition(Vector3 destination)
    {
        agent.CalculatePath(destination, currentPath);
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
}
