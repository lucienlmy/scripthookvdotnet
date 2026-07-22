//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum AudioFlag
    {
        ActivateSwitchWheelAudio,
        AllowCutsceneOverScreenFade,
        AllowForceRadioAfterRetune,
        AllowPainAndAmbientSpeechToPlayDuringCutscene,
        AllowPlayerAIOnMission,
        AllowPoliceScannerWhenPlayerHasNoControl,
        AllowRadioDuringSwitch,
        AllowRadioOverScreenFade,
        AllowScoreAndRadio,
        AllowScriptedSpeechInSlowMo,
        AvoidMissionCompleteDelay,
        DisableAbortConversationForDeathAndInjury,
        DisableAbortConversationForRagdoll,
        DisableBarks,
        DisableFlightMusic,
        DisableReplayScriptStreamRecording,
        EnableHeadsetBeep,
        ForceConversationInterrupt,
        ForceSeamlessRadioSwitch,
        ForceSniperAudio,
        FrontendRadioDisabled,
        HoldMissionCompleteWhenPrepared,
        IsDirectorModeActive,
        IsPlayerOnMissionForSpeech,
        ListenerReverbDisabled,
        LoadMPData,
        MobileRadioInGame,
        OnlyAllowScriptTriggerPoliceScanner,
        PlayMenuMusic,
        PoliceScannerDisabled,
        ScriptedConvListenerMaySpeak,
        SpeechDucksScore,
        SuppressPlayerScubaBreathing,
        WantedMusicDisabled,
        WantedMusicOnMission,
    }
}
