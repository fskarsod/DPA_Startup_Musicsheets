% Lily was here -- automatically converted by D:/Program/LilyPond/usr/bin\midi2ly.py from D:/Documents/GitHub/DPA_Startup_Musicsheets/Alle-eendjes-zwemmen-in-het-water.mid
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

  \time 4/4

  \tempo 4 = 120

}

trackA = <<
  \context Voice = voiceA \trackAchannelA
>>


trackBchannelB = \relative c {
  g''4 g a a
  | % 2
  d8. e16 d8 c b4 d
  | % 3
  g8 a b g a d, d'4
  | % 4
  g,2 r2
  | % 5
  g,4 g a a
  | % 6
  d8 e d c b4 a
  | % 7
  g8 a g fis e4 d
  | % 8
  g8 a g fis e4 d
  | % 9
  g g a a
  | % 10
  d8 e d c b4 a
  | % 11
  e2 fis
  | % 12
  g8 b d b g2
  | % 13
  g4 g a a
  | % 14
  d8 e d c b4 a
  | % 15
  g8 a g fis e4 d
  | % 16
  g8 a g fis e4 d
  | % 17
  g g a a
  | % 18
  d8 e d c b4 a
  | % 19
  e2 fis
  | % 20
  g8 b d b g2
  | % 21
  g8 b d fis g2
  | % 22

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
