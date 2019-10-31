using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool debug;
    [Space(5)]
    public float moveSpeed;
    public float rotateSpeed;
    public Puzzle puzzle;

    private GameObject selectedObject;
    
    private int idleIndex;
    public bool Idle
    {
        get
        {
            return (idleIndex <= 0);
        }
    }

    private void Start()
    {
        selectedObject = null;
        puzzle.Init();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectObject();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                RotateObject();
            }
            else
            {
                MoveObject();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            puzzle.Solve();
        }
    }
    
    private void LateUpdate() 
    {
        if (idleIndex > 0)
        {
            idleIndex--;
        }
    }

    private void OnGUI()
    {
        Texture2D t;

        if (Idle)
        {
            t = new Texture2D(1, 1);
            if (puzzle.Correct)
            {
                t.SetPixel(0, 0, Color.green);
            }
            else
            {
                t.SetPixel(0, 0, Color.red);
            }
            t.Apply();
            GUI.DrawTexture(new Rect(0, Screen.height - 100, 100, 100), t);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && debug)
        {
            puzzle.DrawGizmos();
        }
    }

    private void SelectObject()
    {
        Ray ray;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
        {
            selectedObject = hit.collider.gameObject;
        }
        Busy();
    }

    private void RotateObject()
    {
        Transform cam;
        float x;
        float y;

        if (selectedObject == null)
        {
            return;
        }
        cam = Camera.main.transform;
        x = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        y = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            selectedObject.transform.RotateAround(selectedObject.transform.position, Vector3.up, -x);
        }
        else
        {
            selectedObject.transform.RotateAround(selectedObject.transform.position, Vector3.right, y);
        }
        Busy();
    }

    private void MoveObject()
    {
        Vector3 move;
        float x;
        float y;

        if (selectedObject == null)
        {
            return;
        }
        x = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        y = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
        move = new Vector3(x, y, 0.0f);
        selectedObject.transform.position = selectedObject.transform.position + move;
        Busy();
    }

    private void Busy()
    {
        idleIndex = 5;
    }
}