<CsoundSynthesizer>
<CsOptions>
-n -d -m0d
</CsOptions>
<CsInstruments>
sr 	= 	44100 
ksmps 	= 	32
nchnls 	= 	2
0dbfs	=	1 

instr TEST
a1 inch 1
a2 inch 2
aComb comb a1, 2, .2
outs aComb, aComb
endin

</CsInstruments>
<CsScore>
i"TEST" 0 [3600*12]
</CsScore>
</CsoundSynthesizer>