using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Moves projectile
    // Imparts damage onto object

    [SerializeField] float speed = 0.01f;
    //[SerializeField] float damage = 5f;

    public void Start()
    {
        Destroy(gameObject, 5);
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * transform.right.x, transform.position.y + speed * transform.right.y);
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name);
    }
}