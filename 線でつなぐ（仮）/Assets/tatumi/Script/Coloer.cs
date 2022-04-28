using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coloer : MonoBehaviour
{
<<<<<<< HEAD
    private int a,time=0;
    private bool A;

=======
    public int a=0;
    private bool A;

    [SerializeField]
    Image image;

>>>>>>> 34a85c4f09f09f045e4ebb3c45aa3e74109ee66f
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
<<<<<<< HEAD
    void Update()
=======
    void FixedUpdate()
>>>>>>> 34a85c4f09f09f045e4ebb3c45aa3e74109ee66f
    {
        //time++;
        if (a < 1)
        {
            A = true;
        }
        else if (a > 255)
        {
            A = false;
        }
        

        if (A == true)
            a++;
        else
            a--;

<<<<<<< HEAD


        this.gameObject.GetComponent<Image>().color = new Color(255.0f,255.0f, 255.0f, a);
=======
      

        image.color = new Color32(255,255,255,(byte) a);
        
>>>>>>> 34a85c4f09f09f045e4ebb3c45aa3e74109ee66f
    }
}
