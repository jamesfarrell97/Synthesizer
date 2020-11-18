<CsoundSynthesizer>
<CsOptions>
-n -d -m0d
</CsOptions>

<CsInstruments>
sr = 44100 
ksmps = 32
nchnls = 2
0dbfs = 0

// Sine
giSine  ftgen   0, 0, 2^10, 10, 1

instr SYNTH
    kTempo      chnget "temp"

    kSin        chnget "sin"
    kSqr        chnget "sqr"
    kSaw        chnget "saw"
    kTri        chnget "tri"
    kEnv        chnget "env"
    kAct        chnget "act"

    kSinFreq    chnget "sinFreq"
    kSqrFreq    chnget "sqrFreq"
    kSawFreq    chnget "sawFreq"
    kTriFreq    chnget "triFreq"

    kSinAmpl    chnget "sinAmpl"
    kSqrAmpl    chnget "sqrAmpl"
    kSawAmpl    chnget "sawAmpl"
    kTriAmpl    chnget "triAmpl"

    kAtt        chnget "att"
    kDec        chnget "dec"
    kSus        chnget "sus"
    kRel        chnget "rel"

    if (metro(kTempo) == 1) then
        ; Sin Wave
        if (changed(kSin) == 1) then
            if (kSin == 1 && kAct == 1) then
                event "i",  1, 0, -10, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
            else
                event "i", -1, 0,   0, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
            endif
        endif

        if (changed(kSinFreq) == 1 && kSin == 1 && kAct == 1) then
            event "i", -1, 0,   0, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
            event "i",  1, 0, -10, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
        endif

        if (changed(kSinAmpl) == 1 && kSin == 1 && kAct == 1) then
            event "i", -1, 0,   0, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
            event "i",  1, 0, -10, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
        endif

        ; Sqaure Wave
        if (changed(kSqr) == 1) then
            if (kSqr == 1) then
                event "i",  2, 0, -10, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
            else
                event "i", -2, 0,   0, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
            endif
        endif

        if (changed(kSqrFreq) == 1 && kSqr == 1 && kAct == 1) then
            event "i", -2, 0,   0, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
            event "i",  2, 0, -10, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
        endif

        if (changed(kSqrAmpl) == 1 && kSqr == 1 && kAct == 1) then
            event "i", -2, 0,   0, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
            event "i",  2, 0, -10, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
        endif

        ; Saw Wave
        if (changed(kSaw) == 1) then
            if (kSaw == 1) then
                event "i",  3, 0, -10, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
            else
                event "i", -3, 0,   0, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
            endif
        endif

        if (changed(kSawFreq) == 1 && kSaw == 1 && kAct == 1) then
            event "i", -3, 0,   0, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
            event "i",  3, 0, -10, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
        endif

        if (changed(kSawAmpl) == 1 && kSaw == 1 && kAct == 1) then
            event "i", -3, 0,   0, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
            event "i",  3, 0, -10, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
        endif

        ; Triangle Wave
        if (changed(kTri) == 1) then
            if (kTri == 1 && kAct == 1) then
                event "i",  4, 0, -10, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
            else
                event "i", -4, 0,   0, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
            endif
        endif

        if (changed(kTriFreq) == 1 && kTri == 1 && kAct == 1) then
            event "i", -4, 0,   0, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
            event "i",  4, 0, -10, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
        endif

        if (changed(kTriAmpl) == 1 && kTri == 1 && kAct == 1) then
            event "i", -4, 0,   0, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
            event "i",  4, 0, -10, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
        endif
            
        ; Active
        if (changed(kAct) == 1) then
            if (kAct == 1) then
                if (kSin == 1) then
                    event "i",  1, 0, -10, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
                endif

                if (kSqr == 1) then
                    event "i",  2, 0, -10, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
                endif

                if (kSaw == 1) then
                    event "i",  3, 0, -10, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
                endif
                
                if (kTri == 1) then
                    event "i",  4, 0, -10, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
                endif
            else
                event "i", -1, 0, 0, kSinFreq, kSinAmpl, kAtt, kDec, kSus, kRel
                event "i", -2, 0, 0, kSqrFreq, kSqrAmpl, kAtt, kDec, kSus, kRel
                event "i", -3, 0, 0, kSawFreq, kSawAmpl, kAtt, kDec, kSus, kRel
                event "i", -4, 0, 0, kTriFreq, kTriAmpl, kAtt, kDec, kSus, kRel
            endif
        endif
    endif
endin

instr 1
    kFreq = p4
    kAmpl = p5

    iAtt = p6
    iDec = p7
    iSus = p8
    iRel = p9
    
    kEnv   madsr iAtt, iDec, iSus, iRel

    aOut   poscil kAmpl, kFreq, giSine
    outs   aOut * kEnv, aOut * kEnv
endin

instr 2
    kFreq = p4
    kAmpl = p5

    iAtt = p6
    iDec = p7
    iSus = p8
    iRel = p9
    
    kEnv   madsr iAtt, iDec, iSus, iRel

    aOsc1   poscil  kAmpl / 1, kFreq * 1, giSine
    aOsc3   poscil  kAmpl / 3, kFreq * 3, giSine
    aOsc5   poscil  kAmpl / 5, kFreq * 5, giSine
    aOsc7   poscil  kAmpl / 7, kFreq * 7, giSine
    
    aOut = aOsc1 + aOsc3 + aOsc5 + aOsc7
    outs    aOut * kEnv, aOut * kEnv
endin

instr 3
    kFreq = p4
    kAmpl = p5

    iAtt = p6
    iDec = p7
    iSus = p8
    iRel = p9
    
    kEnv   madsr iAtt, iDec, iSus, iRel

    aOsc1   poscil  kAmpl / 1, kFreq * 1, giSine
    aOsc2   poscil  kAmpl / 2, kFreq * 2, giSine
    aOsc3   poscil  kAmpl / 3, kFreq * 3, giSine
    aOsc4   poscil  kAmpl / 4, kFreq * 4, giSine
    aOsc5   poscil  kAmpl / 5, kFreq * 5, giSine
    aOsc6   poscil  kAmpl / 6, kFreq * 6, giSine
    aOsc7   poscil  kAmpl / 7, kFreq * 7, giSine
    aOsc8   poscil  kAmpl / 8, kFreq * 8, giSine
    
    aOut = aOsc1 - aOsc2 + aOsc3 - aOsc4 + aOsc5 - aOsc6 + aOsc7 - aOsc8
    outs    aOut * kEnv, aOut * kEnv
endin

instr 4
    kFreq = p4
    kAmpl = p5

    iAtt = p6
    iDec = p7
    iSus = p8
    iRel = p9
    
    kEnv   madsr iAtt, iDec, iSus, iRel

    aOsc1   poscil  kAmpl / 1^2, kFreq * 1, giSine
    aOsc3   poscil  kAmpl / 3^2, kFreq * 3, giSine
    aOsc5   poscil  kAmpl / 5^2, kFreq * 5, giSine
    aOsc7   poscil  kAmpl / 7^2, kFreq * 7, giSine
    aOsc9   poscil  kAmpl / 9^2, kFreq * 9, giSine
    
    aOut = aOsc1 + aOsc3 + aOsc5 + aOsc7 + aOsc9
    outs    aOut * kEnv, aOut * kEnv
endin
</CsInstruments>

<CsScore>
i"SYNTH"   0  [3600 * 12]
</CsScore>
</CsoundSynthesizer>