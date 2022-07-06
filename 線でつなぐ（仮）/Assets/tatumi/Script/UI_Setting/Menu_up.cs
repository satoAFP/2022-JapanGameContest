using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu_up : MonoBehaviour
{
    [SerializeField, Header("メニュー")] GameObject Menu;
    [SerializeField, Header("設定メニュー")] GameObject setting_menu;
    [SerializeField, Header("全体map")] GameObject All_map;
    [SerializeField, Header("中央点")] GameObject center_point;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Eキーを推されたさいメニュー画面を全部非表示(真ん中の十字は再表示)
            if (Menu.gameObject.activeSelf == true)
            {
                Menu.gameObject.SetActive(false);
                setting_menu.gameObject.SetActive(false);
                All_map.gameObject.SetActive(false);
                center_point.gameObject.SetActive(true);
            }
            else
            {
                Menu.gameObject.SetActive(true);
                center_point.gameObject.SetActive(false);
            }
        }
    }

    //設定、全体mapの表示・非表示をボタンで扱えるよう関数化
    public void setting_open()
    {
        setting_menu.gameObject.SetActive(true);
    }

    public void setting_close()
    {
        setting_menu.gameObject.SetActive(false);
    }

    public void Allmap_open()
    {
        All_map.gameObject.SetActive(true);
    }

    public void Allmap_close()
    {
        All_map.gameObject.SetActive(false);
    }
}
