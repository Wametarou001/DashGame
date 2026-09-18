using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleSpawner spawner;

    // スポーナーから初期化時に呼ばれる
    public void Initialize(ObstacleSpawner obstacleSpawner)
    {
        spawner = obstacleSpawner;
    }

    private void Update()
    {
        // スポーナーが管理しているスピードを使って左に移動
        float currentSpeed = (spawner != null) ? spawner.MoveSpeed : 6f;
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }
}