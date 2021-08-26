using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Crewmates
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameManager gm;
        [SerializeField] private MouseRaycast mouseRaycast;
        [SerializeField] private GameObject statusPanel;
        [SerializeField] private TMP_Text statusText;

        private void Update()
        {
            statusPanel.SetActive(gm.selected);
            if (gm.selected)
            {
                var selected = gm.selected.GetComponent<Crewmate>();
                statusText.text = (selected.name
                    + "\nMood: "
                    + selected.mood
                    + "\nModified by:\n"
                    + selected.statuses
                    );
            }
        }
    }
}
