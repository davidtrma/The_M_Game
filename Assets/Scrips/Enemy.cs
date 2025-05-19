using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  Rigidbody2D Rigidbody2D;
    public int moveSpeed , damage , vida, tiempoMuerte;
    private bool recibiendoDanio;
    private Transform target;
    public Animator animator;
    public float rangoAtaque = 1.5f;
    public float tiempoEntreAtaques = 1f;
    private float tiempoUltimoAtaque;


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector2.Distance(transform.position, target.position);

        if (distancia > rangoAtaque)
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
            animator.SetBool("IsAttack", false);
        }
        else
        {
            Rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("IsMoving", false);

            if (Time.time >= tiempoUltimoAtaque)
            {
                animator.SetBool("IsAttack", true);
                AplicarDanio();
                tiempoUltimoAtaque = Time.time + tiempoEntreAtaques;
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
        }
    }

    void AplicarDanio()
    {
        if (target != null)
        {
            PlayerController jugador = target.GetComponent<PlayerController>();
            if (jugador != null)
            {
                jugador.RecibeDanio(damage);
            }
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().RecibeDanio(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            PlayerController player = collision.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                RecibeDanio(player.danio);
            }
        }
    }

    public void RecibeDanio(int danio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            animator.SetBool("IsHurt", true);
            TomarDaño(danio);
            Invoke(nameof(DesactivaDanio), 0.3f);
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        animator.SetBool("IsHurt", false);
    }


    public void TomarDaño(int daño)
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
