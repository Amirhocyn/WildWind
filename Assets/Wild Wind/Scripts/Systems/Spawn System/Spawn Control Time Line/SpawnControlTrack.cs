using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System;
using UnityEngine.Playables;

namespace WildWind.Systems.Spawn
{

    [Serializable]
    [TrackColor(23/255f, 165 / 255f, 137 / 255f)]
    [TrackBindingType(typeof(SpawnerSystem))]
    [TrackClipType(typeof(SpawnControlClip))]
    public class SpawnControlTrack : TrackAsset
    {

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {

            return ScriptPlayable<SpawnControlMixer>.Create(graph, inputCount);

        }

    }

}
