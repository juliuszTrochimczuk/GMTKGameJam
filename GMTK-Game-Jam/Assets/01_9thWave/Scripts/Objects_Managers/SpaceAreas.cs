using UnityEngine;

namespace ObjectsManagers
{
    public class SpaceAreas : MonoBehaviour
    {
        [SerializeField] private Vector2 size;

        [Header("Gizmos settings")]
        [SerializeField] private Color gizmosColor;
        [SerializeField] private bool showGizmos;

        public Vector2 Size => size;

        public Vector2 GetRandomPosition() => new(
            transform.position.x + Random.Range(-size.x, size.x),
            transform.position.y + Random.Range(-size.y, size.y)
        );

        public void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Gizmos.color = gizmosColor;
            Gizmos.DrawCube(transform.position, size);
        }
    }
}