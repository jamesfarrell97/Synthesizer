using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Synthesizer : MonoBehaviour
{
    private readonly int baseToggle = -1;

    private readonly float baseTemp = 60;
    private readonly float baseFreq = 440;
    private readonly float baseAmpl = 0;

    private readonly float minFreq = 0;
    private readonly float maxFreq = 20000;

    private readonly float minOct = 0;
    private readonly float maxOct = 8;

    private readonly float baseAtt = 0.1f;
    private readonly float baseDec = 0.3f;
    private readonly float baseSus = 0.4f;
    private readonly float baseRel = 0.2f;

    private readonly float c = 16.3518f;
    private readonly float d = 18.35f;
    private readonly float e = 20.60f;
    private readonly float f = 21.83f;
    private readonly float g = 24.50f;
    private readonly float a = 27.50f;
    private readonly float b = 30.87f;

    private float squareAmplitude;
    private float sineAmplitude;
    private float sawAmplitude;
    private float triangleAmplitude;

    private float sineFrequency;
    private float squareFrequency;
    private float sawFrequency;
    private float triangleFrequency;

    private float originalSineFrequency;
    private float originalSquareFrequency;
    private float originalSawFrequency;
    private float originalTriangleFrequency;

    private int sineActive;
    private int squareActive;
    private int sawActive;
    private int triangleActive;
    private int envelopeActive;
    private int masterActive;

    private float attack;
    private float decay;
    private float sustain;
    private float release;

    private int octave;
    private int tempo;

    private CsoundUnity csoundUnity;

    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();

        #region Channels
        csoundUnity.setChannel("temp", baseTemp);

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
        sineActive = baseToggle;
        sineFrequency = baseFreq;
        sineAmplitude = baseAmpl;
        #endregion

        #region Square
        squareActive = baseToggle;
        squareFrequency = baseFreq;
        squareAmplitude = baseAmpl;
        #endregion

        #region Saw
        sawActive = baseToggle;
        sawFrequency = baseFreq;
        sawAmplitude = baseAmpl;
        #endregion

        #region Triangle
        triangleActive = baseToggle;
        triangleFrequency = baseFreq;
        triangleAmplitude = baseAmpl;
        #endregion

        #region Envelope
        envelopeActive = baseToggle;
        attack = baseAtt;
        decay = baseDec;
        sustain = baseSus;
        release = baseRel;
        #endregion

        #region Active
        masterActive = baseToggle;
        #endregion
    }

    private void Update()
    {
        #region Press Key
        if (Input.GetKeyDown(KeyCode.Alpha1)) // C
        {
            SetActive();
            UpdateFrequencies(c);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // D
        {
            SetActive();
            UpdateFrequencies(d);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // E
        {
            SetActive();
            UpdateFrequencies(e);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) // F
        {
            SetActive();
            UpdateFrequencies(f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) // G
        {
            SetActive();
            UpdateFrequencies(g);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) // A
        {
            SetActive();
            UpdateFrequencies(a);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) // B
        {
            SetActive();
            UpdateFrequencies(b);
        }
        #endregion

        #region Release Key
        if (Input.GetKeyUp(KeyCode.Alpha1)) // C
        {
            ResetFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2)) // D
        {
            ResetFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Alpha3)) // E
        {
            ResetFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Alpha4)) // F
        {
            ResetFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Alpha5)) // G
        {
            ResetFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Alpha6)) // A
        {
            ResetFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Alpha7)) // B
        {
            ResetFrequencies();
            SetInactive();
        }
        #endregion
    }

    private void UpdateFrequencies(float note)
    {
        originalSineFrequency = (float) csoundUnity.getChannel("sinFreq");
        originalSquareFrequency = (float) csoundUnity.getChannel("squareFreq");
        originalSawFrequency = (float) csoundUnity.getChannel("sawFreq");
        originalTriangleFrequency = (float) csoundUnity.getChannel("triangleFreq");

        float frequency = note * Mathf.Pow(2, octave);

        csoundUnity.setChannel("sinFreq", frequency);
        csoundUnity.setChannel("sqrFreq", frequency);
        csoundUnity.setChannel("sawFreq", frequency);
        csoundUnity.setChannel("triFreq", frequency);
    }

    private void ResetFrequencies()
    {
        csoundUnity.setChannel("sinFreq", originalSineFrequency);
        csoundUnity.setChannel("sqrFreq", originalSquareFrequency);
        csoundUnity.setChannel("sawFreq", originalSawFrequency);
        csoundUnity.setChannel("triFreq", originalTriangleFrequency);
    }

    #region Sine
    public void SetSineAmplitude(Slider slider)
    {
        sineAmplitude = slider.value;
        csoundUnity.setChannel("sinAmpl", sineAmplitude);
    }

    public void SetSineFrequency(TMP_InputField inputField)
    {
        if (!float.TryParse(inputField.text, out sineFrequency)) return;
        if (sineFrequency < minFreq || sineFrequency > maxFreq) return;

        csoundUnity.setChannel("sinFreq", sineFrequency);
    }

    public void ToggleSine()
    {
        sineActive *= -1;
        csoundUnity.setChannel("sin", sineActive);
    }
    #endregion

    #region Square
    public void SetSquareAmplitude(Slider slider)
    {
        squareAmplitude = slider.value;
        csoundUnity.setChannel("sqrAmpl", squareAmplitude);
    }

    public void SetSquareFrequency(TMP_InputField inputField)
    {
        if (!float.TryParse(inputField.text, out squareFrequency)) return;
        if (squareFrequency < minFreq || squareFrequency > maxFreq) return;

        csoundUnity.setChannel("sqrFreq", squareFrequency);
    }

    public void ToggleSquare()
    {
        squareActive *= -1;
        csoundUnity.setChannel("sqr", squareActive);
    }
    #endregion

    #region Saw
    public void SetSawAmplitude(Slider slider)
    {
        sawAmplitude = slider.value;
        csoundUnity.setChannel("sawAmpl", sawAmplitude);
    }

    public void SetSawFrequency(TMP_InputField inputField)
    {
        if (!float.TryParse(inputField.text, out sawFrequency)) return;
        if (sawFrequency < minFreq || sawFrequency > maxFreq) return;

        csoundUnity.setChannel("sawFreq", sawFrequency);
    }

    public void ToggleSaw()
    {
        sawActive *= -1;
        csoundUnity.setChannel("saw", sawActive);
    }
    #endregion

    #region Triangle
    public void SetTriangleAmplitude(Slider slider)
    {
        triangleAmplitude = slider.value;
        csoundUnity.setChannel("triAmpl", triangleAmplitude);
    }

    public void SetTriangleFrequency(TMP_InputField inputField)
    {
        if (!float.TryParse(inputField.text, out triangleFrequency)) return;
        if (triangleFrequency < minFreq || triangleFrequency > maxFreq) return;

        csoundUnity.setChannel("triFreq", triangleFrequency);
    }

    public void ToggleTriangle()
    {
        triangleActive *= -1;
        csoundUnity.setChannel("tri", triangleActive);
    }
    #endregion

    #region Envelope
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
    #endregion

    #region Master
    public void ToggleActive()
    {
        masterActive *= -1;
        csoundUnity.setChannel("mst", masterActive);
    }

    public void SetActive()
    {
        masterActive = 1;
        csoundUnity.setChannel("mst", masterActive);
    }

    public void SetInactive()
    {
        masterActive = -1;
        csoundUnity.setChannel("mst", masterActive);
    }
    #endregion

    #region Octave
    public void SetOctave(TMP_InputField inputField)
    {
        int originalOctave = octave;

        if (!int.TryParse(inputField.text, out octave))
        {
            octave = originalOctave;
            return;
        }

        if (octave < minOct || octave > maxOct)
        {
            octave = originalOctave;
            return;
        }
    }
    #endregion

    #region Tempo
    public void SetTempo(TMP_InputField inputField)
    {
        if (!int.TryParse(inputField.text, out tempo)) return;
        csoundUnity.setChannel("temp", tempo);
    }
    #endregion
}
