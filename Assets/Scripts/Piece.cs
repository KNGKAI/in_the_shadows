using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool ignoreZRotation;
    public bool flipX;
    public bool flipY;

    [HideInInspector]
    public Vector3 rootPosition;
    [HideInInspector]
    public Quaternion rootRotation;
    
    private void Awake()
    {
        rootPosition = transform.position;
        rootRotation = transform.rotation;
    }

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

    public bool Correct(Vector3 offset)
    {
        Quaternion rotation;
        Vector3 position;
        float x, y, a, b, c;

        position = transform.position - rootPosition - offset;
        rotation = transform.rotation * Quaternion.Inverse(rootRotation);

        x = Mathf.Abs(position.x);
        y = Mathf.Abs(position.y);
        a = rotation.eulerAngles.x;
        b = rotation.eulerAngles.y;
        c = rotation.eulerAngles.z;

        if (a > 180)
        {
            a = a - 180;
        }
        if (b > 180)
        {
            b = b - 180;
        }
        if (c > 180)
        {
            c = c - 180;
        }

        if (x > Puzzle.PositionDeadzone * transform.localScale.x || y > Puzzle.PositionDeadzone * transform.localScale.y)
        {
            return (false);
        }

        if(a > Puzzle.RotationDeadzone)
        {
            if (flipX)
            {
                a = 180.0f - a;
                if (a > Puzzle.RotationDeadzone)
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }
        }
        if(b > Puzzle.RotationDeadzone)
        {
            if (flipY)
            {
                b = 180.0f - b;
                if (b > Puzzle.RotationDeadzone)
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }
        }
        if (c > Puzzle.RotationDeadzone && !ignoreZRotation)
        {
            return (false);
        }
        return (true);
    }
}
