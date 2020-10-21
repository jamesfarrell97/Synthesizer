using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesizer : MonoBehaviour
{
    private CsoundUnity csoundUnity;
    private float frequency = 1;
    private float frequencyFactor = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(frequency);
            csoundUnity.setChannel("freq", frequency += frequencyFactor);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(frequency);
            csoundUnity.setChannel("play", Random.Range(0, 1000));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(frequency);
            csoundUnity.setChannel("stop", Random.Range(0, 1000));
        }
    }
}
