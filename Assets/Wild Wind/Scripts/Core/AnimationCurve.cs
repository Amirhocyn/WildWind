using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Core
{

    [CreateAssetMenu(fileName = "New Animation Curve", menuName = "Animation Curve",order = 0)]
    public class AnimationCurve : ScriptableObject
    {

        [SerializeField]UnityEngine.AnimationCurve animationCurve;
        Keyframe[] keys
        {

            get
            {

                return animationCurve.keys;

            }
            set
            {

                animationCurve.keys = value;

            }

        }

        int Lenght
        {

            get
            {

                return animationCurve.length;

            }

        }
        WrapMode preWrapMode
        {

            get
            {

                return animationCurve.preWrapMode;

            }
            set
            {

                animationCurve.preWrapMode = value;

            }

        }
        WrapMode postWrapMode
        {

            get
            {

                return animationCurve.postWrapMode;

            }
            set
            {

                animationCurve.postWrapMode = value;

            }

        }

        public float Evaluate(float time)
        {

            return animationCurve.Evaluate(time);

        }

    }

}
