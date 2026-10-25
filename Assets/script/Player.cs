
using UnityEngine;

public interface ISwitchable
{
    void SwitchState();
}

public enum GameColor { Red, Blue, Green }

public class Player : MonoBehaviour, ISwitchable
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float detectionRange = 12f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float bulletSpeed = 12f;
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private GameColor currentColor;

    private float fireTimer;
    private EnemyBase target;

    private void Start()
    {
        if (!cubeRenderer) cubeRenderer = GetComponent<Renderer>();
        ApplyColor();
    }

    private void Update()
    {
        if (GameManager.IsGameOver) return;

        FindNearestEnemy();

        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, lookRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            if (target != null) Shoot();
        }
    }

    private void FindNearestEnemy()
    {
        target = null;
        float closestDistance = detectionRange * detectionRange;

        foreach (EnemyBase enemy in EnemyBase.ActiveEnemies)
        {
            if (enemy == null) continue;

            float distance = (enemy.transform.position - transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy;
            }
        }
    }

    private void Shoot()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 0.8f;
        Bullet bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
        bullet.Initialize(currentColor, bulletSpeed);
    }

    private void OnMouseDown()
    {
        Debug.Log("PLAYER CLICKED!");

        if (!GameManager.IsGameOver)
            SwitchState();
    }

    public void SwitchState()
    {
        currentColor = (GameColor)(((int)currentColor + 1) % 3);
        ApplyColor();
        Debug.Log($"[PLAYER] Switched to {currentColor}");
    }

    private void ApplyColor()
    {
        cubeRenderer.material.color = ColorUtility.GetColor(currentColor);
    }
}

public static class ColorUtility
{
    public static Color GetColor(GameColor color)
    {
        switch (color)
        {
            case GameColor.Red: return Color.red;
            case GameColor.Blue: return Color.blue;
            case GameColor.Green: return Color.green;
            default: return Color.white;
        }
    }
}
