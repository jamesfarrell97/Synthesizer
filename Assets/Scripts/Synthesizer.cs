using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Synthesizer : MonoBehaviour
{
    private CsoundUnity csoundUnity;

    #region Sine
    private float sineAmplitude;
    private float sineFrequency;
    private int sine = -1;

    public void SetSineAmplitude(Slider slider)
    {
        sineAmplitude = slider.value;
        csoundUnity.setChannel("sineAmpl", sineAmplitude);
        Debug.Log(sineAmplitude);
    }

    public void SetSineFrequency(Slider slider)
    {
        sineFrequency = slider.value;
        csoundUnity.setChannel("sineFreq", sineFrequency);
        Debug.Log(sineFrequency);
    }

    public void ToggleSine()
    {
        sine *= -1;
        csoundUnity.setChannel("sine", sine);
        Debug.Log(sine);

    }
    #endregion

    #region Square
    private float squareAmplitude;
    private float squareFrequency;
    private int square = -1;

    public void SetSquareAmplitude(Slider slider)
    {
        squareAmplitude = slider.value;
        csoundUnity.setChannel("squareAmpl", squareAmplitude);
    }

    public void SetSquareFrequency(Slider slider)
    {
        squareFrequency = slider.value;
        csoundUnity.setChannel("squareFreq", squareFrequency);
    }

    public void ToggleSquare()
    {
        square *= -1;
        csoundUnity.setChannel("square", square);
    }
    #endregion

    #region Saw
    private float sawAmplitude;
    private float sawFrequency;
    private int saw = -1;

    public void SetSawAmplitude(Slider slider)
    {
        sawAmplitude = slider.value;
        csoundUnity.setChannel("sawAmpl", sawAmplitude);
    }

    public void SetSawFrequency(Slider slider)
    {
        sawFrequency = slider.value;
        csoundUnity.setChannel("sawFreq", sawFrequency);
    }

    public void ToggleSaw()
    {
        saw *= -1;
        csoundUnity.setChannel("saw", saw);
    }
    #endregion

    #region Triangle
    private float triangleAmplitude;
    private float triangleFrequency;
    private int triangle = -1;

    public void SetTriangleAmplitude(Slider slider)
    {
        triangleAmplitude = slider.value;
        csoundUnity.setChannel("triangleAmpl", triangleAmplitude);
    }

    public void SetTriangleFrequency(Slider slider)
    {
        triangleFrequency = slider.value;
        csoundUnity.setChannel("triangleFreq", triangleFrequency);
    }

    public void ToggleTriangle()
    {
        triangle *= -1;
        csoundUnity.setChannel("triangle", triangle);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();

        Debug.Log(sine);

        csoundUnity.setChannel("sine", sine);
        csoundUnity.setChannel("square", square);
        csoundUnity.setChannel("saw", saw);
        csoundUnity.setChannel("triangle", triangle);
    }

    // Update is called once per frame
    void Update()
    {
        //if (sine)
        //{
        //    Debug.Log("F: " + sineFrequency);
        //    Debug.Log("A: " + sineAmplitude);
        //    csoundUnity.setChannel("sineFreq", sineFrequency);
        //    csoundUnity.setChannel("sineAmpl", sineAmplitude);
        //}

        //if (square)
        //{
        //    csoundUnity.setChannel("squareFreq", squareFrequency);
        //    csoundUnity.setChannel("squareAmpl", squareAmplitude);
        //}

        //if (saw)
        //{
        //    csoundUnity.setChannel("sawFreq", sawFrequency);
        //    csoundUnity.setChannel("sawAmpl", sawAmplitude);
        //}

        //if (triangle)
        //{
        //    csoundUnity.setChannel("triangleFreq", triangleFrequency);
        //    csoundUnity.setChannel("triangleAmpl", triangleAmplitude);
        //}
    }
}
