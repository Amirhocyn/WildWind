using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace WildWind.Systems.Spawn
{

    [Serializable]
    public class SpawnControlClip : PlayableAsset, ITimelineClipAsset
    {

        public ClipCaps clipCaps
        {

            get
            {

                return ClipCaps.Blending;

            }

        }

        [SerializeField]
        private SpawnControlBehaviour spawnControlBehaviour = new SpawnControlBehaviour();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {

            return ScriptPlayable<SpawnControlBehaviour>.Create(graph, spawnControlBehaviour);

        }

    }

}
