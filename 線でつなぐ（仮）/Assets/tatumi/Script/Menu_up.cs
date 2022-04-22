using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu_up : MonoBehaviour
{
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           if(Menu.gameObject.activeSelf == true)
           {
                Menu.gameObject.SetActive(false);
               
           }
           else
           {
                Menu.gameObject.SetActive(true);
           }
        }
    }
}
