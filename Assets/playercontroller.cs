using UnityEngine;
using UnityEngine.InputSystem;//使用新输入系统

public class Playercontroller : MonoBehaviour
{
    private float hp = 100.0f;
    private float maxHp = 100f;

    public float speed = 5f;
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float damageRange = 1.0f;
    private float damage = 10;
    private float facing = 1f;
    private float attackCoolDown = 0.4f;
    private float lastAttackTime = -999f;
    private bool isDead = false;
    private Vector3 startPoint;
    //设置移动速度


    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //TestValueVsReference();
        startPoint = transform.position;
    }
    void Update()
    {
        float move = 0f;


        //左右方向控制
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1f;
            facing = -1f;

        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            move = 1f;
            facing = 1f;
        }

        //每帧移动一点：方向*速度*时间
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);


        //跳跃
        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame
            || Keyboard.current.wKey.wasPressedThisFrame
            || Keyboard.current.upArrowKey.wasPressedThisFrame;
        if (jumpPressed && isGrounded)
        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);


        }
        
        
        if(Keyboard.current.jKey.isPressed&&Time.time - lastAttackTime >= attackCoolDown)
        {
            Attack();
        }//攻击

        //掉落时返回起点
        if (transform.position.y < -10f)
        {
            transform.position = startPoint;
            rb.linearVelocity = Vector2.zero;
        
        
        }
    }
    
    
    void OnCollisionStay2D(Collision2D collision)
    {

        foreach (ContactPoint2D c in collision.contacts)
        {
            
            if (c.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }


        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    //攻击方法
    void Attack()
    {
        lastAttackTime = Time.time;
        Vector2 center = new Vector2(transform.position.x + (facing * 0.8f), transform.position.y);
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, damageRange);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public bool IsDead { get { return isDead; } }

    public void TakeDamage(float amount)
    {
        if (isDead == true) return;
        hp -= amount;
        if (hp <= 0)
        {
            hp = 0.0f;
            Die();
        }
        Debug.Log("玩家剩余血量"+hp);
    }
    void Die()
    {
        Debug.Log("玩家死了");
        enabled=false;
        isDead = true;

    }
    //攻击范围显示
    void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector2 center = new Vector2(transform.position.x + facing * 0.8f, transform.position.y);
            Gizmos.DrawWireSphere(center, damageRange);
        }
    /*public void TestValueVsReference()
    {
        Debug.Log("当前血量" + hp);
        TakeDamage(10f);
        Debug.Log("当前血量" + hp);
        float local = hp;
        local -= 10;
        Debug.Log("" + hp);
    }
    */
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 400, 40), "HP: " + hp + " / " + maxHp);
    }

}
