using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_fade : MonoBehaviour
{

    [SerializeField, Header("fadeさせるオブジェクト")] GameObject fade_obj;
    [SerializeField, Header("フェードの速度"), Range(0.1f, 0.01f)] float fade_speed;

    // Start is called before the first frame update
    void Start()
    {
        fade_obj.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        fade_obj.gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, fade_speed);

        if (fade_obj.gameObject.GetComponent<Image>().color.a <= 0)
            fade_obj.gameObject.SetActive(false);

    }
}
