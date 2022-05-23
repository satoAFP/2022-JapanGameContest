using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage_clear : MonoBehaviour
{
    //ステージクリア状況
    [System.NonSerialized] public bool[] Stage_clear = new bool[5];

    //クリアした判定
    [System.NonSerialized] public bool clear;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        Stage_clear[0] = true;
        for (int i = 1; i < 5; i++)
            Stage_clear[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clear)
        {
            Debug.Log("aaa");
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("bbb");
                if (SceneManager.GetActiveScene().name == "Stage" + i)
                {
                    Debug.Log("ccc");
                    Stage_clear[i + 1] = true;
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "STAGE_SELECT")
        {
            clear = false;
        }
    }
}
