using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  Rigidbody2D Rigidbody2D;
    public Collider2D Collider2D;
    public int moveSpeed , damage , vida, tiempoMuerte;
    private bool recibiendoDanio;
    private Transform target;
    public Animator animator;
    public float rangoAtaque = 1.5f;
    public float tiempoEntreAtaques = 1f;
    private float tiempoUltimoAtaque;
    public GameObject dropObjeto;


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
       
    }

   // Update is called once per frame
       void Update()
        {
        float distancia = Vector2.Distance(transform.position, target.position);

        Vector2 direccion = (target.position - transform.position).normalized;
        Rigidbody2D.velocity = direccion * moveSpeed;

        if (direccion.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (direccion.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        animator.SetBool("IsMoving", true);
       }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            animator.SetBool("IsAttack", true);
            collision.gameObject.GetComponent<PlayerController>().RecibeDanio(damage);
        }
        Invoke(nameof(DesactivaAtaque), 1f);
    }

    public void DesactivaAtaque()
    {
        animator.SetBool("IsAttack", false);
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
        Instantiate(dropObjeto, transform.position, Quaternion.identity);
        animator.SetTrigger("Muerte");
        moveSpeed = 0;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, tiempoMuerte);
    }
}
