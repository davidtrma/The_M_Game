using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Velocidad del personaje
    public int danio = 15;
    public float currentHealth;
    public float maxHealth;

    public Collider2D ControladorAtaque;
    private Rigidbody2D rigidbody;
    private Vector2 moveInput;
    private Animator animator;
    public Transform attackPoint;

    private int defaultLayer;
    private bool invulnerable = false;
    private bool isAttacking = false;  // Flag para verificar si el personaje está atacando
    private Vector2 attackDirection = Vector2.up;  // Dirección de ataque predeterminada (por defecto hacia arriba)

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defaultLayer = gameObject.layer;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAttackInput();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        if (!isAttacking)  // Solo mueve al personaje si no está atacando
        {
            rigidbody.velocity = moveInput * speed;
        }
    }

    void HandleMovement()
    {
        // Movimiento con WASD o teclas de dirección
        float moveX = Input.GetAxisRaw("Horizontal");  // Usamos Horizontal para movimiento
        float moveY = Input.GetAxisRaw("Vertical");    // Usamos Vertical para movimiento

        moveInput = new Vector2(moveX, moveY).normalized;  // Normalizamos para mantener la velocidad constante

    }

    void HandleAttackInput()
    {
        // Ataques con las flechas (↑ ↓ ← →) usando Horizontal2 y Vertical2
        float attackX = Input.GetAxisRaw("Horizontal2");  // Se puede configurar en el Input Manager
        float attackY = Input.GetAxisRaw("Vertical2");    // Se puede configurar en el Input Manager

        // Si se detecta entrada de flecha, actualizamos la dirección de ataque
        if (attackX != 0 || attackY != 0)
        {
            attackDirection = new Vector2(attackX, attackY).normalized;
            Attack(attackDirection);  // Llama al método de ataque con la dirección actual
        }
    }

    void Attack(Vector2 direction)
    {
        if (isAttacking) return;  // Si ya está atacando, no hace nada

        isAttacking = true;  // Marca que el personaje está atacando
        animator.SetBool("IsAttacking", true);  // Inicia la animación de ataque
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        PositionControladorAtaque(direction);
        Invoke(nameof(ResetAttack), 0.3f);  // Tiempo de duración del ataque
    }

    void ResetAttack()
    {
        isAttacking = false;  // Marca que el personaje ya no está atacando
        animator.SetBool("IsAttacking", false);  // Termina la animación de ataque
    }

    void UpdateAnimator()
    {
        // Si no está atacando, usa la dirección de movimiento para la animación
        if (!animator.GetBool("IsAttacking"))
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetFloat("Speed", moveInput.magnitude);
        }
    }

    public void RecibeDanio(int danio)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            currentHealth -= danio;
            animator.SetBool("Hit",true); 
            StartCoroutine(InvulnerabilidadTemporal(1f));
        }
    }


    private IEnumerator InvulnerabilidadTemporal(float duracion)
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerInvulnerable");

        float elapsed = 0f;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        while (elapsed < duracion)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }
        sprite.enabled = true;

        gameObject.layer = defaultLayer;
        invulnerable = false;
        animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TomarDaño(danio);
            }
        }
    }

    public void PositionControladorAtaque(Vector2 direction)
    {
        Vector3 offset = Vector3.zero;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            offset = direction.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            offset = direction.y > 0 ? Vector3.up : Vector3.down;
        }

        attackPoint.localPosition = offset; 
    }
}
