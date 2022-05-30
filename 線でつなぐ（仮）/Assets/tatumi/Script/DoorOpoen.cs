using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    //ギミックノクリア状況管理用flag
    [SerializeField, Header("現在の通電状況")]
    public bool[] ClearTaskflag;

    [SerializeField, Header("飛ぶ先のステージ番号")] 
    public int stage_num;

    [SerializeField, Header("ドアのイラスト")]
    public GameObject[] door_illustration;

    //ギミックの詳細判別（flag種類,num=true,falseが同居してないか）
    private bool taskflag = false;
    private int num;

    [SerializeField, Header("ワープゲートとして使うか(true)")] public bool warp_door;
    [SerializeField, Header("ゴール時のワープゲートとして使うか(true)")] public bool goal_warp_door;
    [SerializeField, Header("ドアのアニメーション")] public Animator door;
    [SerializeField, Header("ドアノブのアニメーション")] public Animator doorknob;
    [SerializeField, Header("シーン移動用のパネルの出現")] public GameObject scene_move_panel;

    private bool[] stage_clear_check = new bool[6];
    private bool SoundClear = false;

    public Material[] mat = new Material[1];//変更したいマテリアルをセット
    [SerializeField, Header("0=強調,1=普通")]
    Material[] mats;

    //音源取得
    [SerializeField]
    private AudioClip soundopen,soundclose,soundlock;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        audioSource = GetComponent<AudioSource>();
        
        //ステージのクリア状況取得
        stage_clear_check = GameObject.Find("stage_clear_check").GetComponent<stage_clear>().Stage_clear;

        //出来るステージの扉のイラスト光らせる
        if (stage_clear_check[stage_num])
        {
            for (int i = 0; i < door_illustration.Length; i++)
            {
                door_illustration[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //全要素一致か判断
        IEnumerable<bool> results = (IEnumerable<bool>)ClearTaskflag.Distinct();
       
        //配列にtrue,falseが同居してるか数化（true or false のみなら1,どっちもあるなら2）
        num = results.Count();

        //bool型で判定できるように変換
        foreach (bool Check in results)
        {
            //代入（中身判別）
            taskflag = Check;

            //カギ閉め、開け
            if(taskflag!=Check)
            {
                audioSource.PlayOneShot(soundlock);
            }
        }
       
        mats[0] = mat[1];
        //rayあたってないなら元に戻す
        GetComponent<Renderer>().materials = mats;
    }

    public void RayOpenDoor()
    {
        if (!warp_door)
        {
            //ドアが開けるかどうか見てアニメーションを再生
            if (num == 1 && taskflag == true)
            {
                door.SetBool("open", true);
                doorknob.SetBool("open", true);

                //一度のみ
                if (SoundClear == false)
                audioSource.PlayOneShot(soundopen);
                SoundClear = true;

                //最後の部屋でゴール時ワープする
                if (goal_warp_door)
                {
                    door.SetBool("open", true);
                    doorknob.SetBool("open", true);
                    scene_move_panel.SetActive(true);
                    
                }
            }
            else
            {
                door.SetBool("open", false);
                doorknob.SetBool("open", false);
                audioSource.PlayOneShot(soundclose);
            }

            
        }
    }

    public void RayOpenWarpDoor()
    {
        if (warp_door)
        {
            if (stage_clear_check[stage_num])
            {
                //ステージ入るための扉開く処理
                door.SetBool("open", true);
                doorknob.SetBool("open", true);
                scene_move_panel.SetActive(true);
                if (!SoundClear)
                    audioSource.PlayOneShot(soundopen);
                SoundClear = true;

            }
        }
    }

    public void RayTargetDoor()
    {
        //マテリアル変更(1=強調0=普通)

        mats[0] = mat[0];
        //光るよ！
        GetComponent<Renderer>().materials = mats;

    }
}
