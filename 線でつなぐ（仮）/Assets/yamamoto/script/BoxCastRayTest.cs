using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;
    //�@�^�[�Q�b�g�Ƃ̋���
    private float distanceFromTargetObj;

    public GameObject Target;//���C���Փ˂��Ă���I�u�W�F�N�g������ 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�@�^�[�Q�b�g�Ƃ̋���
        distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;
        //�@Cube�̃��C���΂��^�[�Q�b�g�ƐڐG���Ă��邩����
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Target")))
        {
            Debug.Log(hit.transform.name);

            //�ڐG�����I�u�W�F�N�g�̃X�N���v�g���擾���A�t���O��ύX
            hit.collider.GetComponent<ClickObj>().move=true;

            //���N���b�N���ꂽ�Ƃ��Ƀ��C�ƐڐG���Ă���I�u�W�F�N�g�̍��W��Target�ɓ����
            if (Input.GetMouseButtonDown(0))
            {
                Target = hit.collider.gameObject;
            }

        }

        //�}�b�v�`�b�v�Ƀ��C���ڐG���Ă��邩����(ray����ɕύX�j
        else if(Physics.BoxCast(transform.position, Vector3.one * 0.000005f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Mapcip")))
        {

            Vector3 worldPos = hit.collider.gameObject.transform.position;//�}�b�v�`�b�v�̍��W���擾����

           
            //���N���b�N���ꂽ�Ƃ��Ƀ}�b�v�`�b�v�̍��W��Target�ɏ㏑������
            if (Input.GetMouseButtonDown(0))
            {
                worldPos.y = 0.5f;//Y�����Œ肷��
                Target.transform.position = worldPos;
                Target = new GameObject();
            }

            Debug.Log(hit.transform.name);
           
        }
    }

    void OnDrawGizmos()
    {
        //�@Cube�̃��C���^���I�Ɏ��o��
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }

}