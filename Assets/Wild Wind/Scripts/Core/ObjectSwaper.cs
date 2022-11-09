using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using WildWind.Movement;

namespace WildWind.Core
{

    public class ObjectSwaper<T>
    {

        public delegate ref T ReturnObjectByRef();
        ReturnObjectByRef originalObject;
        ReturnObjectByRef alternativeObject;

        private CancellationTokenSource CancellationTokenSource;

        public ObjectSwaper(ReturnObjectByRef original, ReturnObjectByRef alternative, Func<bool> violationCondition, CancellationTokenSource cancellationTokenSource, ref Task task)
        {

            originalObject = original;
            alternativeObject = alternative;
            CancellationTokenSource = cancellationTokenSource;

            SwapObjects();

            task = ConditionViolation(violationCondition, cancellationTokenSource.Token);

        }

        private async Task ConditionViolation(Func<bool> violationCondition, CancellationToken token)
        {

            try
            {
                await Task.Run(() =>
                {
                    while (!(violationCondition() || token.IsCancellationRequested))
                    {
                        //if (token.IsCancellationRequested) break;
                    }
                });
            }
            finally
            {
                SwapObjects();
            }

        }

        private void SwapObjects()
        {

            Debug.LogWarning("Swaping : " + (originalObject() as MoverData).speed + "    " + (alternativeObject() as MoverData).speed);
            T originalHelper = (T)((object)originalObject());
            originalObject() = (T)((object)alternativeObject());
            alternativeObject() = originalHelper;

        }

    }

}