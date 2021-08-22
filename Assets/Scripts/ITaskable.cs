using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public interface ITaskable
    {
        public void Task(Crewmate crewmate);
    }
}
