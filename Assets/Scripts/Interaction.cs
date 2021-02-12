using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool isClickable;
    public bool isRotatable;


    float xrot = 0f;
    float yrot = 0f;

    public float rotationSpeed = 3f;
    private Color defaultColor;
    private Color currentColor;

    private Color emissionColor;

    private float baseAngle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = gameObject.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
        //emissionColor = new Color(defaultColor.r *2, defaultColor.g * 2, defaultColor.b * 2);
        emissionColor = new Color(70, 70, 70);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        //defaultColor = gameObject.GetComponent<MeshRenderer>().material.color;
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",emissionColor);
        
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultColor);
    }

    private void OnMouseDown()
    {
        if (isClickable)
        {
            //Debug.Log("Click");
            ScenarioManager.instance.MakeAction(gameObject);
        }
            
        var dir = Camera.main.WorldToScreenPoint(transform.position);
        dir = Input.mousePosition - dir;
        baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
    }

    private void OnMouseDrag()
    {
        if (isRotatable)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                xrot -= rotationSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.E))
            {
                xrot += rotationSpeed * Time.deltaTime;
            }

            transform.localRotation = Quaternion.Euler(xrot, 0, 0);
            //Debug.Log("Rotation");
            //ScenarioManager.instance.MakeAction(gameObject);
        }
        

        
        

        //var dir = Camera.main.WorldToScreenPoint(transform.position);
        //dir = Input.mousePosition - dir;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - baseAngle;
        //transform.localRotation = Quaternion.AngleAxis(angle, Vector3.left);

        //float xRotation = Input.GetAxis("Mouse X") * Time.deltaTime;
        //float yRotation = Input.GetAxis("Mouse Y") * Time.deltaTime;
        //xrot -= xRotation;
        //yrot -= yRotation;

        //gameObject.transform.localRotation = Quaternion.Euler(xrot, yrot, 0);
    }

    private void OnMouseUp()
    {
        if(isRotatable)
            ScenarioManager.instance.MakeAction(gameObject);
    }
}
