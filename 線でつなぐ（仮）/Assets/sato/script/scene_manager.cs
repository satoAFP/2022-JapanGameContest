using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    [SerializeField, Header("OnTriggerEnterでのシーン移動")] bool Trigger_move;
    [SerializeField, Header("OnCollisionEnterでのシーン移動")] bool Collision_move;
    [SerializeField, Header("移動シーン名")] string scene_name;

    public void SceneMove()
    {
        SceneManager.LoadScene(scene_name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Trigger_move)
        {
            if (other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(scene_name);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Collision_move)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObject.Find("stage_clear_check").GetComponent<stage_clear>().clear = true; Debug.Log("ddd");
                SceneManager.LoadScene(scene_name);
            }
        }
    }
}
