using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpos : MonoBehaviour
{
    private GameObject BoxCastRayTest;

    // Start is called before the first frame update
    void Start()
    {
        BoxCastRayTest = GameObject.Find("fps_camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Conductor"|| other.gameObject.tag == "ColorOutput")
        {
            Debug.Log("Ç¢Ç¢ó¨ÇÍÇ™èoóàÇƒÇÈÇÊÅI");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().Existence_Check = true;
        }
    }
}
