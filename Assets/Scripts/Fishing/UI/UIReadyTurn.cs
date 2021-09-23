using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Fishing
{
    public class UIReadyTurn : MonoBehaviour
    {
        [SerializeField]
        private Button button_Casting;
        [SerializeField]
        private GameObject fishingEndPanel;
        [HideInInspector]
        public UIFishingEnd uiFishingEnd;

        private void Awake()
        {
            uiFishingEnd = fishingEndPanel.GetComponent<UIFishingEnd>();
        }

        public void SetActive_FishingEndPanel(bool isActive)
        {
            fishingEndPanel.gameObject.SetActive(isActive);

            if (!isActive)
            {
                SetActive_CastingButton(true);
            }
        }

        public void SetActive_CastingButton(bool isActive)
        {
            button_Casting.gameObject.SetActive(isActive);
        }
    }
}