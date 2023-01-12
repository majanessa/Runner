using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBoard : MonoBehaviour
{
    
    private readonly Stack<GameObject> _boards = new Stack<GameObject>();
    [SerializeField] private List<Transform> holdPoints;
    [SerializeField] private GameObject boardPrefab;
    private const float Velocity = 12;
    private bool IsHoldItem() => 0 < _boards.Count;

    public void AddItem()
    {
        GameObject board = Instantiate(boardPrefab);
        _boards.Push(board);
        _boards.Peek().transform.rotation = holdPoints[_boards.Count - 1].rotation;
        StartCoroutine(PickUpAnimation(_boards.Peek().transform, holdPoints[_boards.Count - 1]));
    }
    
    public void RemoveItem()
    {
        if (IsHoldItem())
        {
            _boards.Pop();
        }
    }
    
    private IEnumerator PickUpAnimation(Transform objTransform, Transform holdPoint)
    {
        while (objTransform.position != holdPoint.position)
        {
            yield return null;
            objTransform.position = Vector3.MoveTowards(
                objTransform.position, 
                holdPoint.position, 
                Time.deltaTime * Velocity);
        }
        objTransform.SetParent(holdPoint.parent);
    }
}
