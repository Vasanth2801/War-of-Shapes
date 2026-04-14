using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 0.025f;
    [SerializeField] private float bulletForce = 7f;

    [Header("References")]
    private Rigidbody2D rb;
    [SerializeField] private ObjectPooler pooler;

    [Header("Distance for the Enemy to Shoot")]
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToStop = 2f;

    [Header("Firing Rate for the Enemy")]
    [SerializeField] private float fireRate;
    [SerializeField] private float timer = 0;

    [Header("Reference for the FirePoint")]
    [SerializeField] private Transform firePoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = fireRate;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pooler = FindAnyObjectByType<ObjectPooler>();
    }

    void Update()
    {
        if(target != null)
        {
            RotateTowardsTarget();
        }

        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(timer <= 0)
        {
            GameObject bullet = pooler.SpawnFromPools("EnemyBullet", firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            timer = fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position,target.position) >= distanceToStop)
        {
            rb.linearVelocity = transform.up * enemySpeed * Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotationSpeed);
    }
}
