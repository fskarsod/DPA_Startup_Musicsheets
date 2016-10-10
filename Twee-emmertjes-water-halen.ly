% Lily was here -- automatically converted by D:/Program/LilyPond/usr/bin\midi2ly.py from D:/Documents/GitHub/DPA_Startup_Musicsheets/Twee-emmertjes-water-halen.mid
\version "2.14.0"

\layout {
  \context {
    \Voice
    \remove "Note_heads_engraver"
    \consists "Completion_heads_engraver"
    \remove "Rest_engraver"
    \consists "Completion_rest_engraver"
  }
}

trackAchannelA = {

  % [SEQUENCE_TRACK_NAME] control track

  % [TEXT_EVENT] creator:

  % [TEXT_EVENT] GNU LilyPond 2.18.2

  \time 6/8

  \tempo 4 = 120
  \skip 2*21
  \time 2/4

}

trackA = <<
  \context Voice = voiceA \trackAchannelA
>>


trackBchannelB = \relative c {
  g'''4. a8
  | % 2
  a a g4
  | % 3
  g8 e4 c8
  | % 4
  f4 g8 a4 b8 c g
  | % 6
  e c r4
  | % 7
  g4. a8
  | % 8
  a a g4
  | % 9
  g8 e4 c8
  | % 10
  g'4. a8
  | % 11
  a a g4. e
  | % 13
  g4 g8 a4 a8 g4. e
  | % 16
  f4 f8 f4 d8 f4
  | % 18
  f8 f4.
  | % 19
  a4 g8 f4 e8 d4
  | % 21
  d8 c2 r8 c e
  | % 23
  g4 g
  | % 24
  g a8. g16
  | % 25
  g8 f f e
  | % 26
  f4 d8 e
  | % 27
  f4 f
  | % 28
  f g8. f16
  | % 29
  f8 e e dis
  | % 30
  e4 c8 e
  | % 31
  g4 g
  | % 32
  g c8. b16
  | % 33
  b8 a a gis
  | % 34
  a4 a8 a
  | % 35
  g4 r4
  | % 36
  b r4
  | % 37
  c2. c,8 e
  | % 39
  g4 g
  | % 40
  g a8. g16
  | % 41
  g8 f f e
  | % 42
  f4 d8 e
  | % 43
  f4 f
  | % 44
  f g8. f16
  | % 45
  f8 e e dis
  | % 46
  e4 c8 e
  | % 47
  g4 g
  | % 48
  g c8. b16
  | % 49
  b8 a a gis
  | % 50
  a4 a8 a
  | % 51
  g4 r4
  | % 52
  b r4
  | % 53
  c2.
}

trackB = <<
  \context Voice = voiceA \trackBchannelB
>>


\score {
  <<
    \context Staff=trackB \trackA
    \context Staff=trackB \trackB
  >>
  \layout {}
  \midi {}
}
