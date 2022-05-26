using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class stage_clear : MonoBehaviour
{
    //ステージクリア状況
    [System.NonSerialized] public bool[] Stage_clear = new bool[6];

    //クリアした判定
    [System.NonSerialized] public bool clear;

    //クリア時のテキスト
    [System.NonSerialized] public string text_mem;

    //タイトルに戻るとカーソルをセットする
    private bool title_cursol_set = false;
    //カーソルの移動設定
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        //クリア状況初期化
        Stage_clear[0] = true;
        for (int i = 1; i < 6; i++)
            Stage_clear[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        //クリアした時
        if (clear)
        {
            //それぞれのステージをクリアしているか判断
            for (int i = 1; i < 6; i++)
            {
                if (SceneManager.GetActiveScene().name == "Stage" + i)
                {
                    Stage_clear[i] = true;

                    text_mem = i + "/5";
                }
            }
        }

        //ステージセレクトに戻るとクリアフラグ戻す
        if (SceneManager.GetActiveScene().name == "STAGE_SELECT")
        {
            clear = false;
            title_cursol_set = true;
        }

        //タイトルに戻るとカーソルを出すようにする
        if(SceneManager.GetActiveScene().name == "TITLE")
        {
            if(title_cursol_set)
            {
                SetCursorPos(960, 570);
                Cursor.visible = true;
                title_cursol_set = false;
            }
        }
    }

}
