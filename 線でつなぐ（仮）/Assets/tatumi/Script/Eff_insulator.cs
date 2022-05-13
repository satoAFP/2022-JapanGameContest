using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Eff_insulator : MonoBehaviour
{
    //現在あたってるObj保存
    [SerializeField]
    private GameObject[] HitCon;

    //上記の名前用
    [SerializeField]
    private string[] HitCon_name;

    //上記のObjが60FPS管理でどれだけ維持するかを保存するための変数
    [SerializeField]
    private int[] HitCon_tonumber;
    

    private int i,nowObj=1;

	void FixedUpdate()
    {
        //保存するいずれかのObjが既定時間経ってるか確認
        for (int i = 0; i != 10; i++)
        {
            HitCon_tonumber[i]++;
            //立ってるならExit判定＆値を初期化
            if (HitCon_tonumber[i] == 30)
            {
                //電源ON,OFFはコンダクター側で管理
                HitCon[i].GetComponent<Conductor_Script>().EffExit();
                HitCon[i] = null;
                HitCon_name[i] = null;
                //nowObj--;
            }
        }

       
        
    }

	private void OnParticleCollision(GameObject other)
	{
        //今のところNOMAL電柱のみ反応
        if (other.gameObject.tag == "Conductor")
        {
            //名前を取得現在の保存している名前に一致しなけば
            string result = HitCon_name.SingleOrDefault(value => value == other.gameObject.name);

            //ここに入る
            if (result == null)
            {
               // Debug.Log(result);
                //穴アイテルやつ探してぶっこむ
                for (int i = 0; i != 10; i++)
                {
                    if (HitCon[i] == null)
                    {
                        //新規なら入力
                        HitCon[i] = other.gameObject;
                        HitCon_name[i] = HitCon[i].name;
                        HitCon_tonumber[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                //Stay中numberを0にする
                for(int i=0;i!=10;i++)
                {
                    if (HitCon_name[i] == HitCon[i].name)
                    {
                        HitCon_tonumber[i] = 0;
                        break;
                    }
                }
            }

            // 当たった相手を強制電源OFF
            //電源ON,OFFはコンダクター側で管理
            other.gameObject.GetComponent<Conductor_Script>().PowerOff();
             
        }
	}

    
}
