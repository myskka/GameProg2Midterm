using UnityEngine;

public interface IDamageable
{
    void TakeDamage(GameColor bulletColor);
}

public class Enemy : EnemyBase, IDamageable
{
    [SerializeField] private GameColor enemyColor;
    [SerializeField] private Renderer enemyRenderer;

    protected override void Start()
    {
        base.Start();
        if (!enemyRenderer) enemyRenderer = GetComponent<Renderer>();
        ApplyColor();
    }

    public void SetColor(GameColor color)
    {
        enemyColor = color;
        if (!enemyRenderer) enemyRenderer = GetComponent<Renderer>();
        ApplyColor();
    }

    private void ApplyColor()
    {
        if (enemyRenderer)
            enemyRenderer.material.color = ColorUtility.GetColor(enemyColor);
    }

    public void TakeDamage(GameColor bulletColor)
    {
        if (GameManager.IsGameOver) return;

        if (bulletColor == enemyColor)
            Destroy(gameObject);
    }
}