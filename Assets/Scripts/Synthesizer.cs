using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class Synthesizer : MonoBehaviour
{
    [SerializeField] Toggle masterToggle;
    [SerializeField] Image instrumentActiveIndicator;

    [SerializeField] GameObject instrumentMessage;

    private CsoundUnity csoundUnity;

    #region Constant Parameters
    private readonly int baseToggle = -1;

    private readonly int baseTemp = 60;
    private readonly int baseOctave = 4;

    private readonly float baseFreq = 35;
    private readonly float baseAmpl = 0;

    private readonly float minFreq = 0;
    private readonly float maxFreq = 70;

    private readonly int minOct = 0;
    private readonly int maxOct = 8;

    private readonly int minTemp = -1;
    private readonly int maxTemp = 120;

    private readonly float baseAtt = 0.1f;
    private readonly float baseDec = 0.1f;
    private readonly float baseSus = 0.1f;
    private readonly float baseRel = 0.1f;

    private readonly float c = 16.35f;
    private readonly float cs = 17.32f;
    private readonly float d = 18.35f;
    private readonly float ef = 19.45f;
    private readonly float e = 20.60f;
    private readonly float f = 21.83f;
    private readonly float fs = 23.12f;
    private readonly float g = 24.50f;
    private readonly float gs = 25.96f;
    private readonly float a = 27.50f;
    private readonly float bf = 29.14f;
    private readonly float b = 30.87f;
    #endregion

    #region Variable Parameters
    private float deltaCC;
    private float deltaCSC;
    private float deltaDC;
    private float deltaEFC;
    private float deltaEC;
    private float deltaFC;
    private float deltaFSC;
    private float deltaGC;
    private float deltaGSC;
    private float deltaAC;
    private float deltaBFC;
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
    private int masterActive;

    private float attack;
    private float decay;
    private float sustain;
    private float release;

    private int octave;
    private int tempo;

    private bool instrumentActive;
    #endregion
    
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();

        #region Master
        tempo = baseTemp;
        octave = baseOctave;
        #endregion

        #region Delta Frequencies
        deltaCC = (c - c);
        deltaCSC = (cs - c);
        deltaDC = (d - c);
        deltaEFC = (ef - c);
        deltaEC = (e - c);
        deltaFC = (f - c);
        deltaFSC = (fs - c);
        deltaGC = (g - c);
        deltaGSC = (gs - c);
        deltaAC = (a - c);
        deltaBFC = (bf - c);
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

        #region Master
        masterActive = baseToggle;
        instrumentActive = true;
        #endregion
    }

    private void Update()
    {
        #region Press Key
        if (Input.GetKeyDown(KeyCode.A)) // C
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaCC);
        }

        if (Input.GetKeyDown(KeyCode.W)) // C#
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaCSC);
        }

        if (Input.GetKeyDown(KeyCode.S)) // D
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaDC);
        }

        if (Input.GetKeyDown(KeyCode.E)) // Eb
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaEFC);
        }

        if (Input.GetKeyDown(KeyCode.D)) // E
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaEC);
        }

        if (Input.GetKeyDown(KeyCode.F)) // F
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaFC);
        }

        if (Input.GetKeyDown(KeyCode.T)) // F#
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaFSC);
        }

        if (Input.GetKeyDown(KeyCode.G)) // G
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaGC);
        }

        if (Input.GetKeyDown(KeyCode.Y)) // G#
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaGSC);
        }

        if (Input.GetKeyDown(KeyCode.H)) // A
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaAC);
        }

        if (Input.GetKeyDown(KeyCode.U)) // Bb
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaBFC);
        }

        if (Input.GetKeyDown(KeyCode.J)) // B
        {
            if (!CheckInstrumentActive()) return;

            SetActive();
            UpdateFrequencies(deltaBC);
        }
        #endregion

        #region Release Key
        if (Input.GetKeyUp(KeyCode.A) && !Input.anyKey) // C
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.W) && !Input.anyKey) // C#
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.S) && !Input.anyKey) // D
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.E) && !Input.anyKey) // Eb
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.D) && !Input.anyKey) // E
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.F) && !Input.anyKey) // F
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.T) && !Input.anyKey) // F#
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.G) && !Input.anyKey) // G
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Y) && !Input.anyKey) // G#
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.H) && !Input.anyKey) // A
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.U) && !Input.anyKey) // Bb
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.J) && !Input.anyKey) // B
        {
            if (!CheckInstrumentActive()) return;

            SetInactive();
        }
        #endregion
    }

    #region Methods
    private bool CheckInstrumentActive()
    {
        if (!instrumentActive)
        {
            DisplayInstrumentMessage();
            return false;
        }

        return true;
    }

    private void UpdateFrequencies(float note = 0)
    {
        csoundUnity.setChannel("sin1Freq", (sin1Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin2Freq", (sin2Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin3Freq", (sin3Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin4Freq", (sin4Frequency + note) * Mathf.Pow(2, octave));
    }

    private void TurnInstrumentOn()
    {
        instrumentActiveIndicator.color = Color.green;
        instrumentActive = true;
    }

    private void TurnInstrumentOff()
    {
        instrumentActiveIndicator.color = Color.red;
        instrumentActive = false;
    }

    private void DisplayInstrumentMessage()
    {
        instrumentMessage.SetActive(true);
    }
    #endregion

    #region Sine Osc 1
    public void SetSin1Amplitude(Slider slider)
    {
        sin1Amplitude = slider.value;

        csoundUnity.setChannel("sin1Freq", sin1Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin1Ampl", sin1Amplitude);
    }

    public void SetSin1Frequency(Slider slider)
    {
        sin1Frequency = slider.value;
        csoundUnity.setChannel("sin1Freq", sin1Frequency * Mathf.Pow(2, octave));
    }

    public void ToggleSin1()
    {
        sin1Active *= -1;

        csoundUnity.setChannel("sin1Freq", sin1Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin1", sin1Active);
    }
    #endregion

    #region Sine Osc 2
    public void SetSin2Amplitude(Slider slider)
    {
        sin2Amplitude = slider.value;

        csoundUnity.setChannel("sin2Freq", sin2Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin2Ampl", sin2Amplitude);
    }

    public void SetSin2Frequency(Slider slider)
    {
        sin2Frequency = slider.value;
        csoundUnity.setChannel("sin2Freq", sin2Frequency * Mathf.Pow(2, octave));
    }

    public void ToggleSin2()
    {
        sin2Active *= -1;

        csoundUnity.setChannel("sin2Freq", sin2Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin2", sin2Active);
    }
    #endregion

    #region Sine Osc 3
    public void SetSin3Amplitude(Slider slider)
    {
        sin3Amplitude = slider.value;

        csoundUnity.setChannel("sin3Freq", sin3Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin3Ampl", sin3Amplitude);
    }

    public void SetSin3Frequency(Slider slider)
    {
        sin3Frequency = slider.value;
        csoundUnity.setChannel("sin3Freq", sin3Frequency * Mathf.Pow(2, octave));
    }

    public void ToggleSin3()
    {
        sin3Active *= -1;

        csoundUnity.setChannel("sin3Freq", sin3Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin3", sin3Active);
    }
    #endregion

    #region Sine Osc 4
    public void SetSin4Amplitude(Slider slider)
    {
        sin4Amplitude = slider.value;

        csoundUnity.setChannel("sin4Freq", sin4Frequency * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin4Ampl", sin4Amplitude);
    }

    public void SetSin4Frequency(Slider slider)
    {
        sin4Frequency = slider.value;
        csoundUnity.setChannel("sin4Freq", sin4Frequency * Mathf.Pow(2, octave));
    }

    public void ToggleSin4()
    {
        sin4Active *= -1;

        csoundUnity.setChannel("sin4Freq", sin4Frequency * Mathf.Pow(2, octave));
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

        if (masterActive == 1)
        {
            TurnInstrumentOff();
        }
        else
        {
            TurnInstrumentOn();
        }
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

        UpdateFrequencies();
    }
    #endregion

    #region Tempo
    public void SetTempo(TMP_InputField inputField)
    {
        int originalTempo = tempo;

        if (!int.TryParse(inputField.text, out tempo))
        {
            tempo = originalTempo;
            return;
        }

        if (tempo < minTemp || tempo > maxTemp)
        {
            tempo = originalTempo;
            return;
        }

        csoundUnity.setChannel("temp", tempo);
    }
    #endregion
}
