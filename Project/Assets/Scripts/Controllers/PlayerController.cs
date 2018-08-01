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

    public bool GoToPosition(Vector3 destination)
    {
        bool pathValid = false;

        if (agent.isActiveAndEnabled)
        {
            agent.CalculatePath(destination, currentPath);
            pathValid = currentPath.status == NavMeshPathStatus.PathComplete;
        }
        
        return pathValid;
    }

    public void StopNavAgent()
    {
        agent.enabled = false;
        //Debug.Log("Turn off agent");
    }

    public void StartNavAgent()
    {
        agent.enabled = true;
        //Debug.Log("Turn on agent");
    }

    public void StopAtPlace()
    {
        GoToPosition(transform.position);
    }
}
