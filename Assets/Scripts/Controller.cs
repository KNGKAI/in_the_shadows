using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float rotateSpeed;

    public GameObject selectedObject;
    
    private void Start()
    {
        selectedObject = null;
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
    }

    void SelectObject()
    {
        Ray ray;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
        {
            selectedObject = hit.collider.gameObject;
        }
    }

    void RotateObject()
    {
        float x;
        float y;

        x = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        y = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        selectedObject.transform.rotation = Quaternion.Euler(selectedObject.transform.rotation.eulerAngles.x + x, selectedObject.transform.rotation.eulerAngles.y - y, 0.0f);
    }
}
