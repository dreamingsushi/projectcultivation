using System.Collections;
using System.Data;
using UnityEngine;

public class MushroomEnemy : MonoBehaviour, IDamagable, IEnemy
{
    public float health = 5f;
    public float damageAmount = 1f;

    public float moveSpeed;
    public Vector2 moveDirection;

    public float attackWidth = 5f;
    public float attackHeight = 5f;

    public float dashDistance = 2f;  // Distance to dash forward
    public float dashDuration = 0.2f;  // Duration of the dash

    public float knockbackDistance = 5f;

    private bool isDashing;
    private bool isKnockedBack;

    private Collider2D damageCollider;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AnimationState currentState;
    public enum AnimationState
    {
        Run, Attack, Hurt
    }

    private void Start()
    {
        damageCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(EnemyStates());
    }

    private IEnumerator EnemyStates()
    {
        while (true)
        {
            if (currentState == AnimationState.Run)
            {
                MoveTowardPlayer(moveSpeed, moveDirection);
                anim.Play("Enemy_Mushroom_Run");
            }
            else if (currentState == AnimationState.Attack)
            {

            }
            else if (currentState == AnimationState.Hurt)
            {
                anim.Play("Enemy_Mushroom_Hurt");

                if (Mathf.Abs(rb.velocity.x) < 0.1f)
                {
                    currentState = AnimationState.Run;
                }
            }

            yield return null;
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
            health -= amount;
            rb.AddForce(damageSourcePosition * knockBackForce, ForceMode2D.Impulse);
            currentState = AnimationState.Hurt;
        }
    }

    public void MoveTowardPlayer(float moveSpeed, Vector2 moveDirection)
    {
        rb.velocity = moveSpeed * moveDirection;
    }
    private void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.instance.TakeDamage(1f, 25f, new Vector2(-1, 0));
            Debug.Log("Dealt " + 1 + " damage to " + gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector2 detectionCenter = (Vector2)transform.position;
        Vector2 detectionSize = new Vector2(attackWidth, attackHeight);

        Gizmos.DrawWireCube(detectionCenter, detectionSize);
    }
}
