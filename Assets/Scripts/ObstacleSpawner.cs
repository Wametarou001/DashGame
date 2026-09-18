using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs; // 0: 低い障害物, 1: 高い障害物
    [SerializeField] private float moveSpeed = 6f;         // 全体の移動スピード（一括管理）
    [SerializeField] private float spawnInterval = 2.0f;     // 生成間隔
    [SerializeField] private float spawnX = 10f;             // 生成位置のX座標
    [SerializeField] private float groundY = -3.5f;          // 地面のY座標

    private float timer = 0f;

    // 外部（障害物など）からスピードを取得できるようにプロパティかメソッドを用意
    public float MoveSpeed => moveSpeed;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;

            // ランダムな間隔にする（1.5秒〜3秒）
            spawnInterval = Random.Range(1.5f, 3.0f);
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0) return;

        int index = Random.Range(0, obstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(spawnX, groundY, 0f);

        GameObject obstacle = Instantiate(obstaclePrefabs[index], spawnPos, Quaternion.identity);

        // 生成された障害物に「スポナーの存在」と「スピード」を教えてあげる
        Obstacle obsComponent = obstacle.GetComponent<Obstacle>();
        if (obsComponent != null)
        {
            obsComponent.Initialize(this);
        }
    }
}