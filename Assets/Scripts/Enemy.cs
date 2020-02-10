using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform exit;
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    public float navigation;
    [SerializeField]
    int health;

    private Transform enemy;
    Collider2D enemyCollider;
    private float navigationTime = 0;
    private int target = 0;
    bool isDead = false;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        Manager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        if (wayPoints != null && isDead == false)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigation)
            {
                if (target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MoveingPoint")
        {
            target += 1;
        }
        else if (collision.tag == "Finish")
        {
            Manager.Instance.UnregisterEnemy(this);
        }
        else if (collision.tag == "Projectile")
        {
            Projectile newP = collision.gameObject.GetComponent<Projectile>();
            EnemyHit(newP.AttackDamage);
            Destroy(collision.gameObject);
        }
    }

    public void EnemyHit(int hitPoints)
    {
        if (health - hitPoints > 0)
        {
            //hurt
            health -= hitPoints;
        }
        else
        {
            //die
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        Manager.Instance.UnregisterEnemy(this);
    }
}
