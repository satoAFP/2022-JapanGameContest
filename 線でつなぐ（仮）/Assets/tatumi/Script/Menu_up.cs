using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu_up : MonoBehaviour
{
    [SerializeField, Header("メニュー")] GameObject Menu;
    [SerializeField, Header("設定メニュー")] GameObject setting_menu;


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
                setting_menu.gameObject.SetActive(false);

            }
           else
           {
                Menu.gameObject.SetActive(true);
           }
        }
    }

    public void setting_open()
    {
        setting_menu.gameObject.SetActive(true);
    }

    public void setting_close()
    {
        setting_menu.gameObject.SetActive(false);
    }
}
