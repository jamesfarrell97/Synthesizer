<CsoundSynthesizer>
<CsOptions>
-n -d -m0d
</CsOptions>

<CsInstruments>
sr = 44100 
ksmps = 32
nchnls = 2
0dbfs = 1 
 
instr INSTR
    kPlay   init 0

    kFreq   chnget "freq"                                                       ;chnget - Reads data from the software bus
    kPan    chnget "panR"                                                       ;chnget - Reads data from the software bus
    kPlay   chnget "play"                                                       ;chnget - Reads data from the software bus
    kStop   chnget "stop"                                                       ;chnget - Reads data from the software bus

    if (changed(kPlay) == 1) then
        event "i", "NOTE", 0, 1, kFreq, kPan
    endif

    if (changed(kStop) == 1) then
        event "i", "NOTE", 0, 0
    endif
endin

instr NOTE
    kFreq = p4
    kPan = p5

    iAtt = 0.1 ;Raise Sound
    iDec = 0.5 ;Lower Sound
    iSus = 0.3 ;Sustain Sound
    iRel = 0.2 ;Release Sound

    //Create envelope
    kEnv    madsr iAtt, iDec, iSus, iRel

    a1      inch 1                                                                  ;inch   - Reads from numbered channels in an external audio signal or stream
    a2      inch 2                                                                  ;inch   - Reads from numbered channels in an external audio signal or stream
    aComb   comb a1, 2, .2                                                          ;comb   - Reverberates an input signal with a “colored” frequency response
            outs oscil:a(1 - kPan, kFreq) * kEnv, oscil:a(1 - kPan, kFreq) * kEnv   ;oscil  - A simple oscillator ;outs - Writes audio data to an external device or stream
endin

</CsInstruments>

<CsScore>
;i        p2           p3
i"INSTR"   0  [3000 * 10]
</CsScore>
</CsoundSynthesizer>