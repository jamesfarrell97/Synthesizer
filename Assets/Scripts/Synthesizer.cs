using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Synthesizer : MonoBehaviour
{
    private readonly int baseToggle = -1;

    private readonly float baseFreq = 100;
    private readonly float baseAmpl = 0;

    private readonly float baseAtt = 0.1f;
    private readonly float baseDec = 0.3f;
    private readonly float baseSus = 0.4f;
    private readonly float baseRel = 0.2f;

    private CsoundUnity csoundUnity;

    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();

        #region Channels
        csoundUnity.setChannel("temp", 90);

        csoundUnity.setChannel("sin", baseToggle);
        csoundUnity.setChannel("sqr", baseToggle);
        csoundUnity.setChannel("saw", baseToggle);
        csoundUnity.setChannel("tri", baseToggle);
        csoundUnity.setChannel("env", baseToggle);
        csoundUnity.setChannel("act", baseToggle);

        csoundUnity.setChannel("sinFreq", baseFreq);
        csoundUnity.setChannel("sqrFreq", baseFreq);
        csoundUnity.setChannel("sawFreq", baseFreq);
        csoundUnity.setChannel("triFreq", baseFreq);

        csoundUnity.setChannel("sinAmpl", baseAmpl);
        csoundUnity.setChannel("sqrAmpl", baseAmpl);
        csoundUnity.setChannel("sawAmpl", baseAmpl);
        csoundUnity.setChannel("triAmpl", baseAmpl);

        csoundUnity.setChannel("att", baseAtt);
        csoundUnity.setChannel("dec", baseDec);
        csoundUnity.setChannel("sus", baseSus);
        csoundUnity.setChannel("rel", baseRel);
        #endregion

        #region Sine
        sine = baseToggle;
        sineFrequency = baseFreq;
        sineAmplitude = baseAmpl;
        #endregion

        #region Square
        square = baseToggle;
        squareFrequency = baseFreq;
        squareAmplitude = baseAmpl;
        #endregion

        #region Saw
        saw = baseToggle;
        sawFrequency = baseFreq;
        sawAmplitude = baseAmpl;
        #endregion

        #region Triangle
        triangle = baseToggle;
        triangleFrequency = baseFreq;
        triangleAmplitude = baseAmpl;
        #endregion

        #region Envelope
        envelope = baseToggle;
        attack = baseAtt;
        decay = baseDec;
        sustain = baseSus;
        release = baseRel;
        #endregion

        #region Active
        active = baseToggle;
        #endregion
    }

    #region Sine
    private float sineAmplitude;
    private float sineFrequency;
    private int sine;

    public void SetSineAmplitude(Slider slider)
    {
        sineAmplitude = slider.value;
        csoundUnity.setChannel("sinAmpl", sineAmplitude);
        Debug.Log(sineAmplitude);
    }

    public void SetSineFrequency(Slider slider)
    {
        sineFrequency = slider.value;
        csoundUnity.setChannel("sinFreq", sineFrequency);
        Debug.Log(sineFrequency);
    }

    public void ToggleSine()
    {
        sine *= -1;
        csoundUnity.setChannel("sin", sine);
        Debug.Log(sine);

    }
    #endregion

    #region Square
    private float squareAmplitude;
    private float squareFrequency;
    private int square;

    public void SetSquareAmplitude(Slider slider)
    {
        squareAmplitude = slider.value;
        csoundUnity.setChannel("sqrAmpl", squareAmplitude);
    }

    public void SetSquareFrequency(Slider slider)
    {
        squareFrequency = slider.value;
        csoundUnity.setChannel("sqrFreq", squareFrequency);
    }

    public void ToggleSquare()
    {
        square *= -1;
        csoundUnity.setChannel("sqr", square);
    }
    #endregion

    #region Saw
    private float sawAmplitude;
    private float sawFrequency;
    private int saw;

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
    private int triangle;

    public void SetTriangleAmplitude(Slider slider)
    {
        triangleAmplitude = slider.value;
        csoundUnity.setChannel("triAmpl", triangleAmplitude);
    }

    public void SetTriangleFrequency(Slider slider)
    {
        triangleFrequency = slider.value;
        csoundUnity.setChannel("triFreq", triangleFrequency);
    }

    public void ToggleTriangle()
    {
        triangle *= -1;
        csoundUnity.setChannel("tri", triangle);
    }
    #endregion

    #region Envelope
    private float attack;
    private float decay;
    private float sustain;
    private float release;
    private int envelope;

    public void SetAttack(Slider slider)
    {
        attack = slider.value;
        csoundUnity.setChannel("att", attack);
    }

    public void SetDecay(Slider slider)
    {
        decay = slider.value;
        csoundUnity.setChannel("dec", decay);
    }

    public void SetSustain(Slider slider)
    {
        sustain = slider.value;
        csoundUnity.setChannel("sus", sustain);
    }

    public void SetRelease(Slider slider)
    {
        release = slider.value;
        csoundUnity.setChannel("rel", release);
    }

    public void ToggleEnvelope()
    {
        envelope *= -1;
        csoundUnity.setChannel("env", envelope);
    }
    #endregion

    #region Active
    private int active;

    public void ToggleActive()
    {
        active *= -1;
        Debug.Log(active);
        csoundUnity.setChannel("act", active);
    }
    #endregion
}
