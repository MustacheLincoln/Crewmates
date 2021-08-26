using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Crewmates
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private MouseRaycast mouseRaycast;
        [SerializeField] private GameObject statusPanel;
        [SerializeField] private TMP_Text statusText;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            mouseRaycast = FindObjectOfType<MouseRaycast>();
        }

        private void Update()
        {
            statusPanel.SetActive(gameManager.selected);
            if (gameManager.selected)
            {
                var selected = gameManager.selected.GetComponent<Crewmate>();
                string descriptions = "";
                foreach (string modifier in selected.modifierDescriptions)
                {
                    descriptions += modifier + "\n";
                }
                statusText.text = (selected.name
                    + "\nMood: "
                    + selected.mood
                    + "\nBase: 50\n"
                    + descriptions
                    ) ;
            }
        }
    }
}
