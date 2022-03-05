using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsRoom : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject projectile;
    private GameObject gameManager;
    private EventManager eventManager;

    private void Start()
    {
        gameManager = GameObject.Find("_GameManager");
        eventManager = gameManager.GetComponent<EventManager>();
        
    }
    void Update()
    {
        if (eventManager.componentsActive)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            {
                turret.transform.right = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0f);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(projectile, new Vector2(turret.transform.position.x, turret.transform.position.y), turret.transform.rotation);
            }
        }
    }
}
