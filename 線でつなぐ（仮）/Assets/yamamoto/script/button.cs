using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class button : MonoBehaviour
{

    public Animator anim;

    public Material[] mat = new Material[1];//変更したいマテリアルをセット
    Material[] mats;

    [SerializeField]
    private float waittime;//ボタンクリック後の待機時間

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        //めた

        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
    }

    public void RayPushButton()
    {
        anim.SetBool("Push", true);
        StartCoroutine("ButtonPushed");
    }

    IEnumerator ButtonPushed()
    {
        Debug.Log("はい");
        //1秒停止
        yield return new WaitForSeconds(waittime);
        anim.SetBool("Push", false);
    }

    public void RayTargetButton()
    {
        //マテリアル変更(1=強調0=普通)

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

        Debug.Log("ミスターk");

    }
}
