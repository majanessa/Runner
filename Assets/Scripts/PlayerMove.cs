using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float gravity;
    private CharacterController _controller;
    private Vector3 _startMousePosition;
    private Vector3 _moveDirection = Vector3.zero;
    private StorageBoard _storageBoard;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _storageBoard = GetComponent<StorageBoard>();
    }

    private void Update()
    {
        _moveDirection.z = speed;
        if (transform.position.y < -1)
            GameService.Instance.GameOver();
    }

    private void FixedUpdate()
    {
        _moveDirection.y += gravity * Time.fixedDeltaTime;
        _controller.Move(_moveDirection * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        Board board = other.gameObject.GetComponent<Board>();
        if (obstacle != null)
            GameService.Instance.GameOver();

        if (board != null)
        {
            Destroy(board.gameObject);
            _storageBoard.AddItem();
        }
    }
}
