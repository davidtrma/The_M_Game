using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  Rigidbody2D Rigidbody2D;
    public float moveSpeed , damage;
    private Transform target;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        Rigidbody2D.velocity = (target.position - transform.position).normalized * moveSpeed;
        
        

        if (Rigidbody2D.velocity.x > 0) 
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("IsMoving", true);
        }
        if (Rigidbody2D.velocity.x < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("IsMoving", true);
        }

        if (Rigidbody2D.velocity.x == 0) { 
            animator.SetBool("IsMoving",false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerHealth.instance.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }
}
