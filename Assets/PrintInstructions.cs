using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintInstructions : MonoBehaviour
{
    public Text _instructions;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        //_instructions.text = "Hello, trusty firewall!!!\nYou are in charge of the security of our website.\nBlock malicious traffic (red cubes) from getting to our network.\nBe careful with the green cubes though--we still want those to get through!\nPress the Space Bar to begin.";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (count != 0)
        {
            _instructions.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown("space"))
        {
            count = 5;
        }
    }
}