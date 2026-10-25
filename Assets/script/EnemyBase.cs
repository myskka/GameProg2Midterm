using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public static readonly List<EnemyBase> ActiveEnemies = new();

    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float reachDistance = 0.7f;

    protected Transform player;

    protected virtual void OnEnable() => ActiveEnemies.Add(this);
    protected virtual void OnDisable() => ActiveEnemies.Remove(this);

    protected virtual void Start()
    {
        Player p = FindFirstObjectByType<Player>();
        if (p) player = p.transform;
    }

    protected virtual void Update()
    {
        if (GameManager.IsGameOver || !player) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude <= reachDistance)
        {
            GameManager.Instance.GameOver();
            return;
        }

        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }
}