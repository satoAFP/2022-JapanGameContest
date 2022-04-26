using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_or_Script : Base_Enegization
{

    //musicには合成がないため不要
    //private GameObject child;
    //[SerializeField]
    //private List<GameObject> obj_list = new List<GameObject>();
    [SerializeField]
    private int music_num=-1;

    private GameObject ResetObj;
    
    //脱色処理(合成以下略)
    //public void Decolorization(int decolor,GameObject gameObject)
    //{
    //    Debug.Log(decolor);
    //    //子オブジェクトに音番号を渡す指令を出す
    //    child.GetComponent<Music_or_Child_Script>().SetColCulation(music_num);

    //    //混ぜるはないみたい

    //}

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトを取得
       // child = transform.GetChild(0).gameObject;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (energization == true)
            GetComponent<Renderer>().material.color = new Color32(71, 214, 255, 1);
        else
            GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);
    }

    public void OnCollisionStay(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、その色に変更
        if (collision.gameObject.tag == "MusicOutput")
        {
            //ColorOutoputのenergizationがtrueならここに入る
            if (collision.gameObject.GetComponent<OutputMusic_Script>().GetEnergization() == true)
            {
                music_num = collision.gameObject.GetComponent<OutputMusic_Script>().Remusic_num();
                energization = true;
            }
            else
            {
                music_num = -1;
                energization = false;
            }
        }
        else if(music_num!=-1&&collision.gameObject.tag == "Power_Supply")
        {
            ResetObj = collision.gameObject;
            collision.gameObject.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //タグColorOutputオブジェクトから色を取得し、設定された値をこのオブジェクトの変数から引くことで脱色
        if (collision.gameObject.tag == "MusicOutput")
        {

            music_num = -1;
            energization = false;
            ResetObj.GetComponent<MusicJudgment_Sctipt>().now_music(music_num);
            //子オブジェクトに色を消す指令を出す(子供はつか(ry)
            // child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }

   
}
