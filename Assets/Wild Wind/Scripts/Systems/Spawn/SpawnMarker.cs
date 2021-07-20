using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SpawnMarker : Marker, INotification
{
    public PropertyName id { get; }

    void INotification()
    {



    }

}
