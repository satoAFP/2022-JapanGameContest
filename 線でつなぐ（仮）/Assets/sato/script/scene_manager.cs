using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    [SerializeField, Header("�ړ��V�[����")] string scene_name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SceneMove()
    {
        SceneManager.LoadScene(scene_name);
    }
}
