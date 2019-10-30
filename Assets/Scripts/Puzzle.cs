using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Puzzle : MonoBehaviour
{
    public Texture2D preview;

    private List<Piece> pieces;
    private Vector3 rootOffset;

    private void Awake()
    {
        Transform child;

        pieces = new List<Piece>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Piece piece;

            child = transform.GetChild(i);
            piece = new Piece();
            piece.transform = child;
            piece.rootPosition = child.position;
            piece.rootRotation = child.rotation;
            pieces.Add(piece);
        }
        rootOffset = pieces[0].rootPosition;
    }

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
            return (10.0f);
        }
    }

    public bool Correct
    {
        get
        {
            Piece piece;
            Quaternion rotation;
            Vector3 position;
            Vector3 offset;

            offset = pieces[0].transform.position - rootOffset;
            for (int i = 0; i < pieces.Count; i++)
            {
                piece = pieces[i];
                position = piece.transform.position - piece.rootPosition - offset;
                rotation = piece.transform.rotation * Quaternion.Inverse(piece.rootRotation);
                if (
                    Mathf.Abs(position.x) > Puzzle.PositionDeadzone * piece.transform.localScale.x ||
                    Mathf.Abs(position.y) > Puzzle.PositionDeadzone * piece.transform.localScale.y ||
                    Mathf.Abs(rotation.x) * Mathf.Rad2Deg > Puzzle.RotationDeadzone ||
                    Mathf.Abs(rotation.y) * Mathf.Rad2Deg > Puzzle.RotationDeadzone ||
                    Mathf.Abs(rotation.z) * Mathf.Rad2Deg > Puzzle.RotationDeadzone
                    )
                {
                    return (false);
                }
            }
            return (true);
        }
    }

    public void DrawPreview()
    {
        GUI.DrawTexture(new Rect(0, 0, 100, 100), preview);
    }

    public void DrawGizmos()
    {
        Vector3 offset;

        offset = pieces[0].transform.position - rootOffset;
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].Draw(offset);
        }
    }

    public void Init()
    {
        Transform child;

        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            child.transform.position = new Vector3(
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f)
                );
            child.transform.rotation = new Quaternion(
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f),
                1
                );
        }
    }

    public void Solve()
    {
        StartCoroutine(SolveProcess());
    }

    private IEnumerator SolveProcess()
    {
        Piece piece;
        Quaternion rotation;
        Vector3 position;
        float speed;
        bool busy;

        speed = 0.05f;
        busy = true;
        while (busy)
        {
            bool done;

            done = true;
            for (int i = 0; i < pieces.Count; i++)
            {
                piece = pieces[i];
                position = piece.rootPosition;
                rotation = piece.rootRotation;
                piece.transform.localPosition = Vector3.Lerp(piece.transform.position, position, speed);
                piece.transform.rotation = Quaternion.Lerp(piece.transform.rotation, rotation, speed);
                position = piece.transform.position - piece.rootPosition;
                rotation = piece.transform.rotation * Quaternion.Inverse(piece.rootRotation);
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
        Debug.Log("Solved");
        yield return null;
    }
}
