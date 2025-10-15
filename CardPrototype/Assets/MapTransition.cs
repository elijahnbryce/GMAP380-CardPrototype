using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    private CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;

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
                newPos.y += 7;
                break;
            case Direction.Down:
                newPos.y -= 7;
                break;
            case Direction.Left:
                newPos.x += 7;
                break;
            case Direction.Right:
                newPos.x -= 7;
                break;
        }

        player.transform.position = newPos;
    }
}
