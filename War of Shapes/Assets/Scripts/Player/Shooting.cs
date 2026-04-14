using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletForce = 5f;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ObjectPooler pooler;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = pooler.SpawnFromPools("Bullet",firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletForce * transform.up, ForceMode2D.Impulse);
    }
}
