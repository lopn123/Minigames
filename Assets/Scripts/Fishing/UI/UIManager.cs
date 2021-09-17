using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Fishing
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject fishingEndPanel;
        private UIFishingEnd uiFishingEnd;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetInfo_FishingEndUI(string fishName, int fishLevel, int fishPrice, Sprite fishImage, Color? nameColor = null, Color? moneyColor= null)
        {
            uiFishingEnd.SetText_FishName(fishName, (Color)nameColor);
            uiFishingEnd.SetImageByFishLevel(fishLevel);
            uiFishingEnd.SetText_Money(fishPrice, (Color)moneyColor);
            uiFishingEnd.SetImage_Fish(fishImage);

            fishingEndPanel.SetActive(true);
        }
    }
}