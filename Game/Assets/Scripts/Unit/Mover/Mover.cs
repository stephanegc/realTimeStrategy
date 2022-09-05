using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : Unit
{
    public NavMeshAgent myAgent;
    public float distanceToTargetPosition;
    private float distanceToSetDestination;
    public bool aimingForTargetUnit;
    public bool isMoving = false;
    public Vector3 targetPosition;

    protected virtual void Awake()
    {
        canMove = true;
        canAttack = true;
        attackPower = 10f;
        myAgent.acceleration = 100f;
        myAgent.speed = 8f;
        myAgent.angularSpeed = 10000f;
        aimingForTargetUnit = false;
        if (targetPosition == null)
        {
            targetPosition = transform.position;
            myAgent.SetDestination(targetPosition);
            Debug.Log("Setting targetPosition to : " + targetPosition);
        }
    }

    protected override void Update()
    {
        base.Update();
        distanceToTargetPosition = Vector3.Distance(targetPosition, transform.position);

        AnimatorStateInfo asi = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        // This make it move to it but then need to find a way to make it stop moving when very close, and override its position when we right click on the ground (without removing target unit)
        if (targetUnit != null && aimingForTargetUnit)
        {
            targetPosition = targetUnit.transform.position;
        }


        if (HasDestinationChanged()) // need to be able to trigger this also when isMoving !!! Else can NOT change destination before the unit has finished moving !!!
        {
            Move();
        } 

        // We want to setIdle the unit only if it's moving AND either it's close to its targetPosition OR it's close to its targetUnit AND the unit is still aiming to go to the position of its targetUnit (else we want it to keep moving and not stop on its way !!!)
        if ( isMoving && (distanceToTargetPosition <= 0.5 || (distanceToTargetUnit <= 1.5 && targetPosition == targetUnit.transform.position)) )
        {
            SetIdle();
        }

        //if (isMoving && ( (distanceToTargetPosition <= 0.1 || distanceToTargetUnit <= 1.5) || (targetUnit == null || targetPosition == targetUnit.transform.position) )) 
        //{
        //    SetIdle();
        //}
    }

    public void Move()
    {
        myAgent.SetDestination(targetPosition);
        isMoving = true;
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.SetTrigger("Run");
        }
    }

    public void SetIdle()
    {
        Debug.Log("Setting to IDLE");
        isMoving = false;
        animator.SetTrigger("Idle");
    }

    public bool HasDestinationChanged()
    {
        var distance = Vector3.Distance(targetPosition, myAgent.destination);
        if (distance < 1)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

}

// UnitMovement.cs
//using UnityEngine;
//using UnityEngine.AI;

//public class UnitMovement : MonoBehaviour
//{
//    Camera myCam;
//    NavMeshAgent myAgent;
//    public LayerMask ground;
//    //public GameObject groundMarker;

//    // Start is called before the first frame update
//    void Start()
//    {
//        myCam = Camera.main;
//        myAgent = GetComponent<NavMeshAgent>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(1))
//        {
//            RaycastHit hit;
//            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
//            {
//                myAgent.SetDestination(hit.point);
//            }
//        }
//        //if (Input.GetMouseButtonDown(1))
//        //{
//        //    RaycastHit hit;
//        //    Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

//        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
//        //    {
//        //        groundMarker.transform.position = hit.point;
//        //        groundMarker.SetActive(false);
//        //        groundMarker.SetActive(true);
//        //    }
//        //}
//    }
//}

////If you want your units to form beautiful circles and not crash into each other, delete all your code from UnitMovement and paste this:

////using System.Collections.Generic;

////using UnityEngine;

////using UnityEngine.AI;



////public class UnitMovement : MonoBehaviour

////{

////    Camera cam;

////    NavMeshAgent myAgent;

////    public LayerMask ground;

////    public static List<NavMeshAgent> meshAgents = new List<NavMeshAgent>();



////    void Start()

////    {

////        cam = Camera.main;

////        myAgent = GetComponent<NavMeshAgent>();

////        meshAgents.Add(myAgent);

////    }



////    void Update()

////    {

////        if (meshAgents.Contains(myAgent))

////        {

////            //absolutely nothing   

////        }

////        else

////        {

////            meshAgents.Add(myAgent);

////        }

////        if (Input.GetMouseButtonDown(1) && meshAgents.Contains(myAgent))

////        {

////            if (meshAgents.IndexOf(myAgent) == 0)

////            {

////                RaycastHit hit;

////                Ray ray = cam.ScreenPointToRay(Input.mousePosition);



////                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))

////                {

////                    myAgent.SetDestination(hit.point);

////                }



////                float angle = 60; // angular step

////                int countOnCircle = (int)(360 / angle); // max number in one round

////                int count = meshAgents.Count; // number of agents

////                float step = 1; // circle number

////                int i = 1; // agent serial number

////                float randomizeAngle = Random.Range(0, angle);

////                while (count > 1)

////                {

////                    var vec = Vector3.forward;

////                    vec = Quaternion.Euler(0, angle * (countOnCircle - 1) + randomizeAngle, 0) * vec;

////                    meshAgents[i].SetDestination(myAgent.destination + vec * (myAgent.radius + meshAgents[i].radius + 0.5f) * step);

////                    countOnCircle--;

////                    count--;

////                    i++;

////                    if (countOnCircle == 0)

////                    {

////                        if (step != 3 && step != 4 && step < 6 || step == 10) { angle /= 2f; }



////                        countOnCircle = (int)(360 / angle);

////                        step++;

////                        randomizeAngle = Random.Range(0, angle);

////                    }

////                }



////            }

////        }

////    }



////    void OnDisable()

////    {

////        meshAgents.Clear();

////    }

////}