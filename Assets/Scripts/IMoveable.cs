using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public interface IMoveable
    {
        void MoveTo(Vector3 position, Action onArrivedAtPosition = null);
        Vector3 GetPosition();
    }
}
