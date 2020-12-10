using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Synthesizer : MonoBehaviour
{
    [SerializeField] Toggle masterToggle;

    private readonly int baseToggle = -1;

    private readonly float baseTemp = 60;
    private readonly float baseFreq = 0;
    private readonly float baseAmpl = 0;

    private readonly float minFreq = 0;
    private readonly float maxFreq = 70;

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

    private float deltaCC;
    private float deltaDC;
    private float deltaEC;
    private float deltaFC;
    private float deltaGC;
    private float deltaAC;
    private float deltaBC;

    private float sin1Amplitude;
    private float sin2Amplitude;
    private float sin3Amplitude;
    private float sin4Amplitude;

    private float sin1Frequency;
    private float sin2Frequency;
    private float sin3Frequency;
    private float sin4Frequency;

    private int sin1Active;
    private int sin2Active;
    private int sin3Active;
    private int sin4Active;
    private int envelopeActive;
    private int masterActive;

    private float attack;
    private float decay;
    private float sustain;
    private float release;

    private int octave;
    private int tempo;

    private CsoundUnity csoundUnity;
    
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();

        #region Delta Frequencies
        deltaCC = (c - c);
        deltaDC = (d - c);
        deltaEC = (e - c);
        deltaFC = (f - c);
        deltaGC = (g - c);
        deltaAC = (a - c);
        deltaBC = (b - c);
        #endregion

        #region Channels
        csoundUnity.setChannel("temp", baseTemp);
        csoundUnity.setChannel("env", baseToggle);
        csoundUnity.setChannel("act", baseToggle);

        csoundUnity.setChannel("sin1", baseToggle);
        csoundUnity.setChannel("sin2", baseToggle);
        csoundUnity.setChannel("sin3", baseToggle);
        csoundUnity.setChannel("sin4", baseToggle);

        csoundUnity.setChannel("sin1Ampl", baseAmpl);
        csoundUnity.setChannel("sin2Ampl", baseAmpl);
        csoundUnity.setChannel("sin3Ampl", baseAmpl);
        csoundUnity.setChannel("sin4Ampl", baseAmpl);

        csoundUnity.setChannel("sin1Freq", baseFreq);
        csoundUnity.setChannel("sin2Freq", baseFreq);
        csoundUnity.setChannel("sin3Freq", baseFreq);
        csoundUnity.setChannel("sin4Freq", baseFreq);

        csoundUnity.setChannel("att", baseAtt);
        csoundUnity.setChannel("dec", baseDec);
        csoundUnity.setChannel("sus", baseSus);
        csoundUnity.setChannel("rel", baseRel);
        #endregion

        #region Sinw Osc 1
        sin1Active = baseToggle;
        sin1Frequency = baseFreq;
        sin1Amplitude = baseAmpl;
        #endregion

        #region Sine Osc 2
        sin2Active = baseToggle;
        sin2Frequency = baseFreq;
        sin2Amplitude = baseAmpl;
        #endregion

        #region Sine Osc 3
        sin3Active = baseToggle;
        sin3Frequency = baseFreq;
        sin3Amplitude = baseAmpl;
        #endregion

        #region Sine Osc 4
        sin4Active = baseToggle;
        sin4Frequency = baseFreq;
        sin4Amplitude = baseAmpl;
        #endregion

        #region Envelope
        attack = baseAtt;
        decay = baseDec;
        sustain = baseSus;
        release = baseRel;
        #endregion

        #region Active
        envelopeActive = baseToggle;
        masterActive = baseToggle;
        #endregion
    }

    private void Update()
    {
        #region Press Key
        if (Input.GetKeyDown(KeyCode.Q)) // C
        {
            SetActive();
            UpdateFrequencies(deltaCC);
        }

        if (Input.GetKeyDown(KeyCode.W)) // D
        {
            SetActive();
            UpdateFrequencies(deltaDC);
        }

        if (Input.GetKeyDown(KeyCode.E)) // E
        {
            SetActive();
            UpdateFrequencies(deltaEC);
        }

        if (Input.GetKeyDown(KeyCode.R)) // F
        {
            SetActive();
            UpdateFrequencies(deltaFC);
        }

        if (Input.GetKeyDown(KeyCode.T)) // G
        {
            SetActive();
            UpdateFrequencies(deltaGC);
        }

        if (Input.GetKeyDown(KeyCode.Y)) // A
        {
            SetActive();
            UpdateFrequencies(deltaAC);
        }

        if (Input.GetKeyDown(KeyCode.U)) // B
        {
            SetActive();
            UpdateFrequencies(deltaBC);
        }
        #endregion

        #region Release Key
        if (Input.GetKeyUp(KeyCode.Q) && !Input.anyKey) // C
        {
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.W) && !Input.anyKey) // D
        {
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.E) && !Input.anyKey) // E
        {
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.R) && !Input.anyKey) // F
        {
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.T) && !Input.anyKey) // G
        {
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Y) && !Input.anyKey) // A
        {
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.U) && !Input.anyKey) // B
        {
            SetInactive();
        }
        #endregion
    }

    private void UpdateFrequencies(float note = 0)
    {
        csoundUnity.setChannel("sin1Freq", (sin1Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin2Freq", (sin2Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin3Freq", (sin3Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin4Freq", (sin4Frequency + note) * Mathf.Pow(2, octave));
    }

    #region Sine Osc 1
    public void SetSin1Amplitude(Slider slider)
    {
        sin1Amplitude = slider.value;
        csoundUnity.setChannel("sin1Ampl", sin1Amplitude);
    }

    public void SetSin1Frequency(TMP_InputField inputField)
    {
        float originalFrequency = sin1Frequency;

        if (float.TryParse(inputField.text, out sin1Frequency))
        {
            if (sin1Frequency < minFreq || sin1Frequency > maxFreq)
            {
                sin1Frequency = originalFrequency;
                return;
            }
        
            csoundUnity.setChannel("sin1Freq", sin1Frequency * Mathf.Pow(2, octave));
        }
    }

    public void ToggleSin1()
    {
        sin1Active *= -1;
        csoundUnity.setChannel("sin1", sin1Active);
    }
    #endregion

    #region Sine Osc 2
    public void SetSin2Amplitude(Slider slider)
    {
        sin2Amplitude = slider.value;
        csoundUnity.setChannel("sin2Ampl", sin2Amplitude);
    }

    public void SetSin2Frequency(TMP_InputField inputField)
    {
        float originalFrequency = sin2Frequency;

        if (float.TryParse(inputField.text, out sin2Frequency))
        {
            if (sin2Frequency < minFreq || sin2Frequency > maxFreq)
            {
                sin2Frequency = originalFrequency;
                return;
            }

            csoundUnity.setChannel("sin2Freq", sin2Frequency * Mathf.Pow(2, octave));
        }
    }

    public void ToggleSin2()
    {
        sin2Active *= -1;
        csoundUnity.setChannel("sin2", sin2Active);
    }
    #endregion

    #region Sine Osc 3
    public void SetSin3Amplitude(Slider slider)
    {
        sin3Amplitude = slider.value;
        csoundUnity.setChannel("sin3Ampl", sin3Amplitude);
    }

    public void SetSin3Frequency(TMP_InputField inputField)
    {
        float originalFrequency = sin3Frequency;

        if (float.TryParse(inputField.text, out sin3Frequency))
        {
            if (sin3Frequency < minFreq || sin3Frequency > maxFreq)
            {
                sin3Frequency = originalFrequency;
                return;
            }

            csoundUnity.setChannel("sin3Freq", sin3Frequency * Mathf.Pow(2, octave));
        }
    }

    public void ToggleSin3()
    {
        sin3Active *= -1;
        csoundUnity.setChannel("sin3", sin3Active);
    }
    #endregion

    #region Sine Osc 4
    public void SetSin4Amplitude(Slider slider)
    {
        sin4Amplitude = slider.value;
        csoundUnity.setChannel("sin4Ampl", sin4Amplitude);
    }

    public void SetSin4Frequency(TMP_InputField inputField)
    {
        float originalFrequency = sin4Frequency;

        if (float.TryParse(inputField.text, out sin4Frequency))
        {
            if (sin4Frequency < minFreq || sin4Frequency > maxFreq)
            {
                sin4Frequency = originalFrequency;
                return;
            }
        }

        csoundUnity.setChannel("sin4Freq", sin4Frequency * Mathf.Pow(2, octave));
    }

    public void ToggleSin4()
    {
        sin4Active *= -1;
        csoundUnity.setChannel("sin4", sin4Active);
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
        masterToggle.gameObject.SetActive(false);
        csoundUnity.setChannel("mst", masterActive);
    }

    public void SetInactive()
    {
        masterActive = -1;
        masterToggle.gameObject.SetActive(true);
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

        UpdateFrequencies();
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
