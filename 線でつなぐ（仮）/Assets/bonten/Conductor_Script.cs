using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected
        bool energization = false;
    //List<GameObject> colList = new List<GameObject>();//コライダーリスト



    public

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //ここでゲームオブジェクトリストの中に特定のタグ名を見つけて、それに応じた処理をさせたい
    //    foreach (GameObject Obj in colList)
    //    {
    //        if (Obj.Find("Power_Supply"))
    //        {
    //            energization = true;
    //            GetComponent<Renderer>().material.color = Color.cyan;
    //        }
    //        else if (Obj.Find("Conductor"))
    //        {
    //            energization = true;
    //            GetComponent<Renderer>().material.color = Color.cyan;
    //        }
    //        else if (Obj.Find("Insulator"))
    //        {
    //            energization = false;
    //            GetComponent<Renderer>().material.color = Color.gray;
    //        }

    //    }

    //}

    ////ここで触れてるオブジェクトをリストにぶち込む
    //void OnCollisionEnter(Collision c)
    //{
    //    colList.Add(c.gameObject);
    //}

    ////離れたら離れたオブジェクトのリストを削除
    //void OnCollisionExit(Collision c)
    //{
    //    colList.Remove(c.gameObject);
    //}


    void OnCollisionStay(Collision c)
    {
        //電源と接触してるとき
        if (c.gameObject.tag == "Power_Supply")
        {
            //通電変数をtrueにし、色を水色に変更
            energization = true;
            GetComponent<Renderer>().material.color = Color.cyan;

            Debug.Log("true");
        }
        //電源と接触せずに導体と接触してるとき!
        else if (c.gameObject.tag == "Conductor")
        {
            //絶縁体と接触してるとき
            if (c.gameObject.tag == "Insulator")
            {
                //通電変数をfalseにする
                energization = false;
                GetComponent<Renderer>().material.color = Color.gray;
            }
            else
            {
                //通電変数をtrueにし、色を水色に変更
                energization = true;
                GetComponent<Renderer>().material.color = Color.cyan;
            }
        }
    }
}
