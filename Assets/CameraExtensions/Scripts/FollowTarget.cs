using UnityEngine;

namespace Camera2D.CameraExtensions
{
    public class FollowTarget : MonoBehaviour
    {
        public Vector2 offset;
        public Vector2 overshoot;
        public float speed;
        public Vector2 Position { get; private set; }
        public Vector2 OvershootPosition { get; private set; }
        public Vector2 OffsetPosition { get; private set; }
        public Vector2 Offset { get; private set; }
        public Vector2 Overshoot { get; private set; }

        public void Center()
        {
            Overshoot = Vector2.zero;
            Offset = Vector2.zero;
        }
        public void UpdateTarget(Vector2 position, Vector2 direction)
        {
            Position = position;
            if (direction.y != 0 || direction.x != 0)
            {
                Offset = direction;
            }
            Overshoot = Offset + direction * overshoot;

            OffsetPosition = GetOffsetPosition();
            OvershootPosition = OffsetPosition + direction * overshoot;
        }

        private Vector2 GetOffsetPosition()
        {
            return (Offset * offset) + Position;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(OffsetPosition, Vector3.one * 0.25f);
            Gizmos.DrawLine(Position, OffsetPosition);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(OvershootPosition, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(ScreenToWorld(Vector2.zero), 1f);
        }
        Vector3 ScreenToWorld(Vector2 v)
        {
            Camera camera = Camera.current;
            Vector3 s = camera.WorldToScreenPoint(transform.position);
            return camera.ScreenToWorldPoint(new Vector3(v.x, camera.pixelHeight - v.y, s.z));
        }
    }
}
