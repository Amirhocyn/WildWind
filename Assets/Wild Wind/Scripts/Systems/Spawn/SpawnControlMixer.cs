using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace WildWind.Systems.Spawn
{

    public class SpawnControlMixer : PlayableBehaviour
    {

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {

            int inputCount = playable.GetInputCount();

            int maxActiveMissiles = 0;
            int maxActivePowerups = 0;
            int maxActiveStars = 0;
            float totalWeight = 0;

            for (int j = 0; j < inputCount; j++)
            {

                float inputWeight = playable.GetInputWeight(j);
                ScriptPlayable<SpawnControlBehaviour> inputPlayable = (ScriptPlayable<SpawnControlBehaviour>)playable.GetInput(j);
                SpawnControlBehaviour behaviour = inputPlayable.GetBehaviour();

                maxActiveMissiles += (int)(behaviour.maxActiveMissiles * inputWeight);
                maxActivePowerups += (int)(behaviour.maxActivePowerups * inputWeight);
                maxActiveStars += (int)(behaviour.maxActiveStars * inputWeight);

                totalWeight += inputWeight;

            }

            SpawnerSystem.Instance.maxActiveMissiles = maxActiveMissiles;
            SpawnerSystem.Instance.maxActivePowerups = maxActivePowerups;
            SpawnerSystem.Instance.maxActiveStars = maxActiveStars;

        }

    }

}
