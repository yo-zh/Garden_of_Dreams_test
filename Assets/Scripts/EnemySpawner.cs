using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnAmount = 3;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Camera mainCamera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            mainCamera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

        float padding = 0.9f; // 10% от краев, но стоит проверять еще и объекты на тайл-карте
        float x = Random.Range(
            bounds.min.x * padding,
            bounds.max.x * padding);
        float y = Random.Range(
            bounds.min.y * padding,
            bounds.max.y * padding);

        return new Vector2(x, y);
    }
}