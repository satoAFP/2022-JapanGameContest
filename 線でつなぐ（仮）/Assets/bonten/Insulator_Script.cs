using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insulator_Script : MonoBehaviour
{
    public
    static Insulator_Script instance;
    bool insulation = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
