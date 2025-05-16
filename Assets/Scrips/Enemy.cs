using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  Rigidbody2D Rigidbody2D;
    public float moveSpeed , damage , vida;
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

        float horizontal = Rigidbody2D.velocity.x;

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("IsMoving", true);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerHealth.instance.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        if (vida <= 0) 
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        animator.SetTrigger("Muerte");
    }
}
