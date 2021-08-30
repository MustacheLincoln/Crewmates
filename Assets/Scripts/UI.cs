using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Crewmates
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject statusPanel;
        [SerializeField] private GameObject contextPanel;
        [SerializeField] private TMP_Text statusText;
        [SerializeField] private TMP_Text contextText;

        public static UI Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            statusPanel.SetActive(GameManager.Instance.selected);
            if (GameManager.Instance.selected)
            {
                var selected = GameManager.Instance.selected.GetComponent<Crewmate>();
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
            if (GameManager.Instance.rightClicking)
            {
                if (Vector3.Distance(Input.mousePosition, contextPanel.transform.position) > 150)
                {
                    contextPanel.SetActive(false);
                    GameManager.Instance.rightClicking = null;
                }
            }
            else
                contextPanel.SetActive(false);
        }

        public void ContextMenu(Vector3 mousePos, GameObject obj)
        {
            contextPanel.transform.position = mousePos;
            contextPanel.SetActive(true);
            contextText.text = obj.name;
        }
    }
}
