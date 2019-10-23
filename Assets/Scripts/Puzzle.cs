using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Puzzle : MonoBehaviour
{
    public Texture2D preview;

    public static float PositionDeadzone
    {
        get
        {
            return (0.2f);
        }
    }

    public static float RotationDeadzone
    {
        get
        {
            return (3.0f);
        }
    }

    public bool Correct
    {
        get
        {
            Transform child;
            Quaternion rotation;
            Vector3 anchor;
            
            anchor = transform.GetChild(0).position;
            for (int i = 0; i < transform.childCount; i++)
            {
                child = transform.GetChild(i);
                rotation = child.rotation;
                if (
                    Mathf.Abs(anchor.x - child.position.x) > Puzzle.PositionDeadzone ||
                    Mathf.Abs(anchor.y - child.position.y) > Puzzle.PositionDeadzone ||
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

    public void Init()
    {
        Transform child;

        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            child.transform.localPosition = new Vector3(
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
        float speed;
        bool busy;

        speed = 0.02f;
        busy = true;
        while (busy)
        {
            Transform child;
            bool done;

            done = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                child = transform.GetChild(i);
                child.transform.localPosition = Vector3.Lerp(child.transform.position, Vector3.zero, speed);
                child.transform.rotation = Quaternion.Lerp(child.transform.rotation, Quaternion.identity, speed);
                if (child.transform.localPosition.magnitude > 0.1f)
                {
                    done = false;
                }
                if (child.transform.rotation.eulerAngles.magnitude > 0.01f)
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
