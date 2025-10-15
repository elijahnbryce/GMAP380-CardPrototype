using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    private CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] float distance = 9;

    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;
        switch (direction)
        {
            case Direction.Up:
                newPos.y += distance;
                break;
            case Direction.Down:
                newPos.y -= distance;
                break;
            case Direction.Left:
                newPos.x += distance;
                break;
            case Direction.Right:
                newPos.x -= distance;
                break;
        }

        //player.GetComponent<PlayerController>().rb.linearVelocity = Vector3.zero;
        player.transform.position = newPos;
    }
}
