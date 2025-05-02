using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    [SerializeField] private List<GameObject> _enemyPrefabs; // ������ �� ������ ����Ʈ
    [SerializeField] private List<Rect> _spawnAreas; // ���� ������ ���� ����Ʈ
    [SerializeField] private Color _gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����
    [SerializeField] private List<EnemyController> _activeEnemies = new List<EnemyController>(); // ���� Ȱ��ȭ�� ����

    public bool EnemySpawnComplite;

    [SerializeField] private float _timeBetweenSpawns = 0.2f;
    [SerializeField] private float _timeBetweenWaves = 1f;

    public void StartWave(int waveCount)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        EnemySpawnComplite = false;
        yield return new WaitForSeconds(_timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(_timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        EnemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (_enemyPrefabs.Count == 0 || _spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs �Ǵ� Spawn Areas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ������ �� ������ ����
        GameObject randomPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

        // ������ ���� ����
        Rect randomArea = _spawnAreas[Random.Range(0, _spawnAreas.Count)];

        // Rect ���� ������ ���� ��ġ ���
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // �� ���� �� ����Ʈ�� �߰�
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
      
        enemyController.SetRoundManager(this); // RoundManager ����
        _activeEnemies.Add(enemyController);
    }

    // ����� �׷� ������ �ð�ȭ (���õ� ��쿡�� ǥ��)
    private void OnDrawGizmosSelected()
    {
        if (_spawnAreas == null) return;

        Gizmos.color = _gizmoColor;
        foreach (var area in _spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    public bool CheckRoundEnd()
    {
        return (_activeEnemies.Count == 0 && EnemySpawnComplite);
    }
    public void RemoveEnemy(EnemyController enemy)
    {
        if (_activeEnemies.Contains(enemy))
            _activeEnemies.Remove(enemy);
    }

    public  void EndGame()
    {
        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
        {
            if (_activeEnemies[i] != null)
                Destroy(_activeEnemies[i].gameObject);
            _activeEnemies.RemoveAt(i);
        }
    }

}
