using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float hp = 30.0f;
    private float maxHp = 30.0f;
    public float damage = 10.0f ;
    public float attackInterval = 1.0f;
    private float lastAttackTime = -999f;
    public float moveSpeed = 2.0f;
    private Rigidbody2D rb;
    private Transform playerTarget;
    public float decetRange = 12f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if(playerObj != null )
        {
            playerTarget = playerObj.transform;
        }
        if(playerObj == null )
            return;
    }

    void Update()
    {
        if (rb == null) return;
        if (playerTarget == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        float dist = Vector2.Distance(transform.position, playerTarget.position);
        if (dist > decetRange)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }
        Vector2 dir = (playerTarget.position-transform.position);
        dir = dir.normalized;
        rb.linearVelocity = new Vector2(dir.x * moveSpeed, rb.linearVelocity.y);

    }

    void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log("敌人被撞：撞到的是"+collision.gameObject.name);
        Playercontroller player = collision.gameObject.GetComponent<Playercontroller>();
        if (player == null)
        {
            return;
        }

        if (Time.time - lastAttackTime > attackInterval)
            {
                player.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
    }
    public float Hp
    { get { return hp; }
      set { hp = Mathf.Clamp(value,0,maxHp);}
     }
    public float MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp < 0)
        {
            hp = 0.0f;
            Destroy(gameObject);
        }
        Debug.Log("-" + amount);
    }
}
