using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                RotateObject();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SelectObject();
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

        cam = Camera.main.transform;
        x = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        y = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime * -1;
        selectedObject.transform.RotateAround(selectedObject.transform.position, cam.right, x);
        selectedObject.transform.RotateAround(selectedObject.transform.position, cam.up, y);
        Busy();
    }

    private void Busy()
    {
        idleIndex = 5;
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
        puzzle.DrawPreview();
    }
}