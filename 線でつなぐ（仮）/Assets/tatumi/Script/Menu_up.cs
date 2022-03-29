using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu_up : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if(this.gameObject.activeSelf == true)
           {
                this.gameObject.SetActive(false);
           }
           else
           {
                this.gameObject.SetActive(true);
           }
        }
    }
}
