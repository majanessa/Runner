using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject obstaclePrefab;
    public GameObject boardPrefab;
    [SerializeField] private Transform player;
    private readonly List<GameObject> _activePlatforms = new List<GameObject>();
    private readonly List<GameObject> _activeObstacles = new List<GameObject>();
    private float _spawnPos = 0;
    private float _spawnPosObstacles = 0;
    private GameObject _nextPlatform;
    private const float PlatformLength = 50;
    private const int StartPlatforms = 3;

    private void Start()
    {
        for (int i = 0; i < StartPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        if (player.position.z - 60 > _spawnPos - (StartPlatforms * PlatformLength))
        {
            SpawnPlatform();
            DeletePlatform();
        }                             
    }

    private void SpawnPlatform()
    {
        _nextPlatform = Instantiate(platformPrefab, transform.forward * _spawnPos, transform.rotation);
        _activePlatforms.Add(_nextPlatform);
        SpawnObstacle(2);
        _spawnPos += PlatformLength + 50;
        SpawnObstacle(2, true);

        for (int i = 0; i < 10; i++)
        {
            Instantiate(boardPrefab, 
                new Vector3(_nextPlatform.transform.position.x, _nextPlatform.transform.position.y + 0.5f, _nextPlatform.transform.position.z - 10 + i + 1f),
                Quaternion.Euler(12.5f, 25.5f, 2f));
        }
    }

    private void SpawnObstacle(int count, bool up = false)
    {
        for (int i = 0; i < count; i++)
        {
            float posY = up ? 10f : 3f;
            GameObject nextObstacle = Instantiate(obstaclePrefab, 
                new Vector3(0f, posY, transform.position.z + _spawnPosObstacles), 
                Quaternion.Euler(0f, 0f, -90f));
            _activeObstacles.Add(nextObstacle);
            _spawnPosObstacles += 20;
        }
    }
    
    private void DeletePlatform()
    {
        Destroy(_activePlatforms[0]);
        _activePlatforms.RemoveAt(0);
        Destroy(_activeObstacles[0]);
        _activeObstacles.RemoveAt(0);
    }
}
