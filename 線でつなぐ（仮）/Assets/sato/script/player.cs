using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //インスペクター設定
    [SerializeField, Header("主人公の移動量")] float move_power;
    [SerializeField, Header("マウス感度")]     float mouse_power;

    //プライベート変数
    private Vector3 move_vec = new Vector3(0, 0, 0);
    private Vector3 velocity;
    private Vector3 mousePos;
    private Vector3 befor_mouse_pos;

    // Start is called before the first frame update
    void Start()
    {
        befor_mouse_pos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        if(mousePos.x>befor_mouse_pos.x)
        {
            this.gameObject.transform.Rotate(new Vector3(0, (mousePos.x - befor_mouse_pos.x)* mouse_power, 0));
        }
        if (mousePos.x < befor_mouse_pos.x)
        {
            this.gameObject.transform.Rotate(new Vector3(0, (mousePos.x - befor_mouse_pos.x) * mouse_power, 0));
        }


        if (Input.GetKey(KeyCode.W))
        {
            velocity = gameObject.transform.rotation * new Vector3(0, 0, move_power);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity = gameObject.transform.rotation * new Vector3(-move_power, 0, 0);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity = gameObject.transform.rotation * new Vector3(0, 0, -move_power);
            Move(velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity = gameObject.transform.rotation * new Vector3(move_power, 0, 0);
            Move(velocity * Time.deltaTime);
        }

        befor_mouse_pos = Input.mousePosition;
    }

    private void Move(Vector3 vec)
    {
        this.gameObject.transform.position += vec;
    }
}
