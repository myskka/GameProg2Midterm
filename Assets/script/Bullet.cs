
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Renderer bulletRenderer;
    [SerializeField] private float lifetime = 4f;

    private GameColor bulletColor;
    private float speed;

    public void Initialize(GameColor color, float bulletSpeed)
    {
        bulletColor = color;
        speed = bulletSpeed;

        if (!bulletRenderer) bulletRenderer = GetComponent<Renderer>();
        bulletRenderer.material.color = ColorUtility.GetColor(color);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (GameManager.IsGameOver) return;

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.IsGameOver) return;

        IDamageable damageable = other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(bulletColor);
            Destroy(gameObject);
        }
    }
}
