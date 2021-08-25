using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crewmates
{
    public class UI : MonoBehaviour
    {
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }
    }
}
