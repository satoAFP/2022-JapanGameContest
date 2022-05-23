using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_clear : MonoBehaviour
{
    //ステージクリア状況
    [System.NonSerialized] public bool[] Stage_clear = new bool[5];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
            Stage_clear[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
}
