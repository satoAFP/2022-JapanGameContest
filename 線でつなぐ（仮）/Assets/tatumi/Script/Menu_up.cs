using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu_up : MonoBehaviour
{
    [SerializeField, Header("���j���[")] GameObject Menu;
    [SerializeField, Header("�ݒ胁�j���[")] GameObject setting_menu;
    [SerializeField, Header("�S��map")] GameObject All_map;
    [SerializeField, Header("�����_")] GameObject center_point;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //E�L�[�𐄂��ꂽ�������j���[��ʂ�S����\��(�^�񒆂̏\���͍ĕ\��)
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

    //�ݒ�A�S��map�̕\���E��\�����{�^���ň�����悤�֐���
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
