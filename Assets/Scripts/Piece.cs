using UnityEngine;

public struct Piece
{
    public Vector3 rootPosition;
    public Quaternion rootRotation;
    public Transform transform;

    public void Draw(Vector3 offset)
    {
        Vector3 root;
        Vector3 position;

        root = offset - rootPosition;
        position = transform.position - offset;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(root, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(position, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(position, position + transform.forward);
        Gizmos.DrawLine(position, position + transform.up);
        Gizmos.DrawLine(position, position + transform.right);
    }
}
