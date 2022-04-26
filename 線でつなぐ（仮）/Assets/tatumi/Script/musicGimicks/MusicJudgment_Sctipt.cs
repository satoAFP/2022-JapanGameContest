using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJudgment_Sctipt : MonoBehaviour
{
    //ゲームクリアとなるカラー
   
    [SerializeField]
    private int clearMusic,muisc_num;

    //消える対象（ぴかぴか処理用）
    [SerializeField]
    private GameObject[] objs;

    // Update is called once per frame
    void Update()
    {
        
        
        objs[0].SetActive(true);
        objs[1].SetActive(false);

        if (clearMusic==muisc_num)
        {
            //ここにクリアの証的なコード
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }

    public void now_music(int a)
    {
        muisc_num = a;
    }
}
