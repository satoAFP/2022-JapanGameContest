using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time_count : MonoBehaviour
{
    [SerializeField, Header("èIóπéûä‘")] float total_time;

    private int minute;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        minute = (int)total_time / 60;
        seconds = (int)total_time % 60;
        total_time = (int)total_time % 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (total_time < 0)
        {
            minute--;
            total_time = 60;
        }
        total_time -= Time.deltaTime;
        seconds = (int)total_time;

        gameObject.GetComponent<TextMesh>().text = minute.ToString("d2") + ":" + seconds.ToString("d2");
    }
}
