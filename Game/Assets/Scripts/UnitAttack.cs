using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private Transform target;
    private float distanceToTarget;
    private Camera myCam;
    public LayerMask clickable;
    public LayerMask ground;
    public float attackSpeed = 2f;
    public float attackRange = 2f;
    private float attackCountDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTarget();
            Debug.Log("Target: " + target.name);
        }
        if (target != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        }
        if (target != null && distanceToTarget <= attackRange && attackCountDown == 0)
        {
            Attack(target);
        }
        if (attackCountDown == 0)
        {
            attackCountDown = attackSpeed;
        }
        attackCountDown -= Time.deltaTime;
    }

    void SelectTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            target = hit.collider.transform;
        }
    }

    void Attack(Transform target)
    {
        Debug.Log("Attacking: " + target.name);

    }

    void GoToResource()
    {

    }
}
