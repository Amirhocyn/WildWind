using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{

    NavMeshAgent AI;

    private void Start()
    {

        AI = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
                AI.SetDestination(hit.point);

        }

    }

    private void LateUpdate()
    {

        if (AI.pathStatus == NavMeshPathStatus.PathComplete)
            AI.velocity = AI.speed * AI.transform.forward;

        AI.updateRotation = true;

        print(AI.steeringTarget);

    }

}
