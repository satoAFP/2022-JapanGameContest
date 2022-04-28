using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®ƒV[ƒ“–¼")] string scene_name;

    public void SceneMove()
    {
        SceneManager.LoadScene(scene_name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            SceneManager.LoadScene(scene_name);
        }
    }
}
