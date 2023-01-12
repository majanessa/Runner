using System.Collections;
using UnityEngine;

public class RoadOfBoards : MonoBehaviour
{
    [SerializeField] private GameObject roadOfBoardsPrefab;
    [SerializeField] private GameObject boardForRoad;
    [SerializeField] private PlayerMove player;
    private GameObject _startRoad;
    private bool _isStartRoad;
    private Vector3 _startMousePosition;
    private bool _isBuildRoad;
    private Vector3 _mouseDragDir;
    private StorageBoard _storageBoard;

    private void Start()
    {
        _storageBoard = player.GetComponent<StorageBoard>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Input.mousePosition;
            if (player.GetComponent<CharacterController>().isGrounded)
                StartRoad(player.transform);
        }
        else if (Input.GetMouseButton(0))
        {		
            _mouseDragDir = (Input.mousePosition - _startMousePosition).normalized;
            if (_isStartRoad && !_isBuildRoad)
                StartCoroutine(BuildRoad());
        } else if (Input.GetMouseButtonUp(0))
        {
            _isStartRoad = false;
            _isBuildRoad = false;
            StopAllCoroutines();
        }
    }

    private void StartRoad(Transform player)
    {
        _startRoad = Instantiate(roadOfBoardsPrefab,
            new Vector3(player.position.x, player.position.y - 1f, player.position.z),
            Quaternion.identity
        );
        _isStartRoad = true;
    }

    private IEnumerator BuildRoad()
    {
        _isBuildRoad = true;
        while (_isBuildRoad)
        {
            for (int i = 0; i < 200; i++)
            {
                GameObject road;
                Transform lastChild = _startRoad.transform.GetChild(_startRoad.transform.childCount - 1);
                if (_mouseDragDir.y > 0)
                {
                    road = Instantiate(boardForRoad, 
                        new Vector3(lastChild.position.x, lastChild.position.y * _mouseDragDir.y + 0.3f, lastChild.position.z + 0.8f),
                        Quaternion.Euler(_mouseDragDir.y * 10 * 2 * -1, 0, 0)
                    );
                }
                else
                {
                    road = Instantiate(boardForRoad, 
                        new Vector3(lastChild.position.x, lastChild.position.y * _mouseDragDir.y - 0.3f, lastChild.position.z + 0.8f),
                        Quaternion.Euler(_mouseDragDir.y * 10 * 2 * -1, 0, 0)
                    );
                }
                _storageBoard.RemoveItem();
                
                road.transform.parent = _startRoad.transform;

                yield return new WaitForSeconds(0.09f);
            }
            _isStartRoad = false;
            _isBuildRoad = false;
        }
    }
}
