using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  Rigidbody2D Rigidbody2D;
    public float moveSpeed , damage , vida, tiempoMuerte;
    private bool recibiendoDanio;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().RecibeDanio(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            RecibeDanio(1);
        }
    }

    public void RecibeDanio(int danio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            animator.SetBool("IsHurt", true);
            Invoke(nameof(DesactivaDanio), 0.3f);
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        animator.SetBool("IsHurt", false);
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
        Destroy(gameObject, tiempoMuerte);
    }
}
