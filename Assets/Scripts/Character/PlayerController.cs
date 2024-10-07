using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    public static PlayerController instance;

    public float health = 100;
    public float moveSpeed = 5f;

    private Vector2 attackDirection = new Vector2(1,0);
    public float attackForce;
    public float attackDistance;

    public float attackRange = 2f; // The length of the raycast
    public LayerMask layerMask;    // The layers that the raycast can hit
    public Color raycastColor = Color.red; // The color of the raycast in the editor


    [SerializeField] private float originalMoveSpeed;

    private bool isAttacking;
    private bool isRunning;
    private bool isGuarding = false;
    private bool isHurt;

    private Vector2 originalVelocity;

    private Rigidbody2D rb;
    private Animator anim;
    public AnimationState currentState;


    // Define animation states
    public enum AnimationState
    {
        Idle, Run, Attack, Guard, Hurt
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalMoveSpeed = moveSpeed;
        currentState = AnimationState.Run;

        StartCoroutine(PlayerStates());
    }

    private IEnumerator PlayerStates()
    {
        while (true)
        {
            if (currentState == AnimationState.Run)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                anim.Play("Player_Run");
            }
            else if (currentState == AnimationState.Attack)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    AttackWithRaycast();
                    rb.AddForce(attackDirection * attackForce, ForceMode2D.Impulse);
                    anim.Play("Player_Attack1");
                    yield return new WaitForSeconds(attackDistance);
                    isAttacking = false;
                    currentState = AnimationState.Run;
                }
            }
            else if (currentState == AnimationState.Guard)
            {
                if (!isHurt)
                {
                    anim.Play("Player_Guard");

                    if (!isGuarding && Mathf.Abs(rb.velocity.x) < 0.1f)
                    {
                        currentState = AnimationState.Run;
                    }
                }

            }
            else if (currentState == AnimationState.Hurt)
            {
                anim.Play("Player_Hurt");

                if (Mathf.Abs(rb.velocity.x) < 0.1f)
                {
                    isHurt = false;
                    currentState = AnimationState.Run;
                }
                else
                {
                    isHurt = true;
                }
            }

            yield return null;
        }
    }

    public void PointerDown()
    {
        isGuarding = true;
        currentState = AnimationState.Guard;
    }

    public void PointerUp()
    {
        isGuarding = false;
        currentState = AnimationState.Run;
    }

    public void Attack()
    {
        currentState = AnimationState.Attack;
    }

    private void AttackWithRaycast()
    {
        // Raycast in the direction of attack
        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackRange, layerMask);

        // Visualize the raycast in the editor
        Debug.DrawRay(transform.position, attackDirection * attackRange, raycastColor, 0.5f);

        // Check if the ray hit anything
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            MushroomEnemy enemy = hit.collider.GetComponent<MushroomEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1, 30f, new Vector2(1, 0));
                Debug.Log("Dealt damage to " + hit.collider.name);
            }
        }
    }


    public void TakeDamage(float amount, float knockBackForce, Vector2 damageSourcePosition)
    {
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (isGuarding)
            {
                currentState = AnimationState.Guard;
                rb.AddForce(damageSourcePosition * knockBackForce, ForceMode2D.Impulse);
            }
            else
            {
                health -= amount;
                currentState = AnimationState.Hurt;
                rb.AddForce(damageSourcePosition * knockBackForce, ForceMode2D.Impulse);
            }

        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
    }

}
