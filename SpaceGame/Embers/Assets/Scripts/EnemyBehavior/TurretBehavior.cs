using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject turretBarrel;
    [SerializeField] float targetingDistance = 10f;
    [SerializeField] float fireRate = 5f;
    private float nextFire = 0f;

    GameObject playerShip;
    BehaviorTree tree;
    WaitForSeconds waitForSeconds;

    bool isInRange = true;

    Node.Status treeStatus = Node.Status.Running;

    void Start()
    {
        playerShip = GameObject.Find("PlayerShip");

        tree = new BehaviorTree("Turret Behavior");
        Sequence turretProtocol = new Sequence("Turret Protocol");
        Leaf searchForTarget = new Leaf($"Search For Target", SearchForTarget);
        Leaf fireAtTarget = new Leaf("Fire At Target", FireAtTarget);

        turretProtocol.AddChild(searchForTarget);
        turretProtocol.AddChild(fireAtTarget);
        tree.AddChild(turretProtocol);

        waitForSeconds = new WaitForSeconds(0.1f);

        StartCoroutine("ExecuteBehavior");
    }
    private void Update()
    {
        if (targetingDistance > Vector2.Distance(playerShip.transform.position, transform.position))
        {
            isInRange = true;
            transform.right = playerShip.transform.position - transform.position; //should only follow when is in targeting range - should lerp when leaves and returns to range
        }
    }

    IEnumerator ExecuteBehavior()
    {
        while (true)
        {
            if(treeStatus != Node.Status.Failure)
            {
                treeStatus = tree.Process();
                yield return waitForSeconds;
            }
        }
    }

    public Node.Status SearchForTarget()
    {
        if(isInRange) 
            return Node.Status.Success;
        return Node.Status.Failure;
    }

    public Node.Status FireAtTarget()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(projectile, new Vector2(turretBarrel.transform.position.x, turretBarrel.transform.position.y), transform.rotation);
        }
        return Node.Status.Success;
    }
}
