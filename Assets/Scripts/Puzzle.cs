using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Puzzle : MonoBehaviour
{
    public string puzzleName;

    public bool yRotation;
    public bool xRotation;
    public bool moveable;

    public Piece[] pieces;
    
    private Vector3 rootOffset;

    public bool solving;

    public static float PositionDeadzone
    {
        get
        {
            return (0.5f);
        }
    }

    public static float RotationDeadzone
    {
        get
        {
            return (7.0f);
        }
    }

    private void Awake()
    {
        rootOffset = pieces[0].transform.position;
        solving = false;
    }

    public bool Correct
    {
        get
        {
            Vector3 offset;

            offset = pieces[0].transform.position - rootOffset;
            for (int i = 0; i < pieces.Length; i++)
            {
                if (!pieces[i].Correct(offset))
                {
                    return (false);
                }
            }
            return (true);
        }
    }

    public void DrawGizmos()
    {
        Vector3 offset;

        offset = pieces[0].transform.position - rootOffset;
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].Draw(offset);
        }
    }

    public void Init()
    {
        Transform piece;

        for (int i = 0; i < pieces.Length; i++)
        {
            piece = pieces[i].transform;
            if (moveable)
            {
                piece.transform.position = new Vector3(
                    Random.Range(-1.0f, 1.0f),
                    Random.Range(-1.0f, 1.0f),
                    0.0f//Random.Range(-1.0f, 1.0f)
                    );
            }
            if (yRotation)
            {
                piece.Rotate(0, Random.Range(-90.0f, 90.0f), 0);
            }
            if (xRotation)
            {
                piece.Rotate(Random.Range(-90.0f, 90.0f), 0, 0);
            }
        }
    }

    public void Solve()
    {
        if (!solving)
        {
            StartCoroutine(SolveProcess());
        }
    }

    private IEnumerator SolveProcess()
    {
        Piece piece;
        Quaternion rotation;
        Vector3 position;
        Vector3 angles;
        float speed;
        bool busy;

        speed = 0.05f;
        busy = true;
        solving = true;
        while (busy)
        {
            bool done;

            done = true;
            for (int i = 0; i < pieces.Length; i++)
            {
                piece = pieces[i];
                position = piece.rootPosition;
                rotation = piece.rootRotation;
                if (piece.flipY && piece.transform.eulerAngles.y > 90.0f && piece.transform.eulerAngles.y < 270.0f)
                {
                    angles = rotation.eulerAngles;
                    angles.y += 180;
                    rotation = Quaternion.Euler(angles);
                }
                if (piece.flipX && piece.transform.eulerAngles.x > 90.0f && piece.transform.eulerAngles.x < 270.0f)
                {
                    angles = rotation.eulerAngles;
                    angles.x += 180;
                    rotation = Quaternion.Euler(angles);
                }
                piece.transform.localPosition = Vector3.Lerp(piece.transform.localPosition, position, speed);
                piece.transform.rotation = Quaternion.Lerp(piece.transform.rotation, rotation, speed);
                position = piece.transform.position - position;
                rotation = piece.transform.rotation * Quaternion.Inverse(rotation);
                if (position.magnitude > 0.1f)
                {
                    //done = false;
                }
                if (rotation.eulerAngles.magnitude > 0.1f)
                {
                    done = false;
                }
                yield return null;
            }
            if (done)
            {
                busy = false;
            }
        }
        Debug.Log(puzzleName + " solved");
        PlayerPrefs.SetInt(puzzleName, 1);
        yield return new WaitForSeconds(3.0f);
        Level.LoadMenu();
    }
}
