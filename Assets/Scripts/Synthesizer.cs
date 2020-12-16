using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Synthesizer : MonoBehaviour
{
    #region Serialzed Fields

    // Exposed to Unity

    [SerializeField] GameObject instrumentMessage;
    [SerializeField] Image instrumentActiveIndicator;
    [SerializeField] Toggle masterToggle;

    #endregion

    #region Private Fields

    private CsoundUnity csoundUnity;

    #endregion

    #region Constant Parameters

    // Initial values

    private readonly int baseToggle = -1;

    private readonly int baseTemp = 60;
    private readonly int baseOctave = 4;

    private readonly float baseFreq = 35;
    private readonly float baseAmpl = 0;

    private readonly float baseAtt = 0.1f;
    private readonly float baseDec = 0.1f;
    private readonly float baseSus = 0.1f;
    private readonly float baseRel = 0.1f;

    // Value ranges

    private readonly float minFreq = 0;
    private readonly float maxFreq = 70;

    private readonly int minOct = 0;
    private readonly int maxOct = 8;

    private readonly int minTemp = -1;
    private readonly int maxTemp = 120;

    // Equal tempered scale frequency values
    // https://pages.mtu.edu/~suits/notefreqs.html

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

    // Delta note values (frequency of each note relative to c)

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

    // Oscillator values

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

    // Envelope values

    private float attack;
    private float decay;
    private float sustain;
    private float release;

    // Master values

    private int masterActive;
    private bool instrumentActive;

    private int octave;
    private int tempo;

    #endregion

    void Start()
    {
        // Retrieves and initialises the Csound component

        #region CSound Initialisation

        csoundUnity = GetComponent<CsoundUnity>();
        
        #endregion

        // Calculate frequencies relative to bottom c
        
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

        // Initialise Csound channels
        
        #region Channels

        csoundUnity.setChannel("temp", baseTemp);
        csoundUnity.setChannel("osc", baseToggle);

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

        // Initialise oscillator values

        #region Sine Osc 1

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

        // Initialise Envelope values

        #region Envelope

        attack = baseAtt;
        decay = baseDec;
        sustain = baseSus;
        release = baseRel;

        #endregion

        // Initialise master values

        #region Master

        masterActive = baseToggle;
        instrumentActive = true;

        tempo = baseTemp;
        octave = baseOctave;

        #endregion
    }

    private void Update()
    {
        // Checks for a key press. Each key is linked to a specific note For 
        // example, if the A key is pressed, the user is requesting that the 
        // C note is played
        //

        // Press Key
        //
        // Check if the instrument is active (checks if the oscillators are
        // on, the oscillators and the instrument cannot be played at the 
        // same time).
        //
        // If the instrument is not active, return. If the instrument is
        // active, update the frequencies of all oscillators and turn any 
        // active oscillator on.
        //

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

        // Release Key
        //
        // Check if the instrument is active (checks if the oscillators are
        // on, the oscillators and the instrument cannot be played at the 
        // same time).
        //
        // If the instrument is not active, return. If the instrument is
        // active, reset the frequencies of all oscillators and turn all
        // oscillators off.
        //

        #region Release Key

        if (Input.GetKeyUp(KeyCode.A) && !Input.anyKey) // C
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.W) && !Input.anyKey) // C#
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.S) && !Input.anyKey) // D
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.E) && !Input.anyKey) // Eb
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.D) && !Input.anyKey) // E
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.F) && !Input.anyKey) // F
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.T) && !Input.anyKey) // F#
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.G) && !Input.anyKey) // G
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.Y) && !Input.anyKey) // G#
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.H) && !Input.anyKey) // A
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.U) && !Input.anyKey) // Bb
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        if (Input.GetKeyUp(KeyCode.J) && !Input.anyKey) // B
        {
            if (!CheckInstrumentActive()) return;

            UpdateFrequencies();
            SetInactive();
        }

        #endregion
    }

    #region Methods

    /// <summary>
    /// Checks if the instrument is currently active. If the instrument
    /// is not active, display error message and return false. Otherwsie,
    /// return true.
    /// </summary>
    /// <returns>
    /// true if instrument active, false if instrument not active
    /// </returns>
    private bool CheckInstrumentActive()
    {
        if (!instrumentActive)
        {
            DisplayInstrumentMessage();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Updates the frequencies of all oscillators, based on an input
    /// note. Calculates the desired note frequency relative to the 
    /// user-defined frequency of each oscillator.
    /// </summary>
    /// <param name="note">
    /// delta frequency from c of the desired note
    /// </param>
    private void UpdateFrequencies(float note = 0)
    {
        csoundUnity.setChannel("sin1Freq", (sin1Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin2Freq", (sin2Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin3Freq", (sin3Frequency + note) * Mathf.Pow(2, octave));
        csoundUnity.setChannel("sin4Freq", (sin4Frequency + note) * Mathf.Pow(2, octave));
    }

    /// <summary>
    /// Marks the instrument as active and sets the indicator colour to
    /// green
    /// </summary>
    private void TurnInstrumentOn()
    {
        instrumentActiveIndicator.color = Color.green;
        instrumentActive = true;
    }

    /// <summary>
    /// Marks the instrument as inactive and sets the indicator colour to
    /// red
    /// </summary>
    private void TurnInstrumentOff()
    {
        instrumentActiveIndicator.color = Color.red;
        instrumentActive = false;
    }

    /// <summary>
    /// Displays an instrument error message to the user
    /// </summary>
    private void DisplayInstrumentMessage()
    {
        instrumentMessage.SetActive(true);
    }

    #endregion

    // Sine Oscillators
    //
    // Each oscillator contains three functions which control
    // a corresponding instrument within csound. The toggle function
    // toggles each instrument on or off. The set frequency and set
    // amplitude fucntions update the frequency and amplitude values
    // of the corresponding oscillator, respectively.
    //

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

    // Envelope
    //
    // The set attack, set decay, set sustain, and set release functions
    // set the attack, decay, sustain, and release values of all 
    // instruments, respectively.
    //

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

    // Master
    //
    // The toggle active function toggles the oscillators on or off.
    // The set active and set inactive functions turn the oscillators on or 
    // off, respectively. The set octave function sets the synthesizers
    // current octave. The set tempo function sets the synthesizers update
    // speed (tempo times per second).
    // 

    #region Master

    public void ToggleActive()
    {
        masterActive *= -1;
        csoundUnity.setChannel("osc", masterActive);

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
        csoundUnity.setChannel("osc", masterActive);
    }

    public void SetInactive()
    {
        masterActive = -1;
        csoundUnity.setChannel("osc", masterActive);
    }

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