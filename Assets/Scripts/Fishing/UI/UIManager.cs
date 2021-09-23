using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Fishing
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public UIReadyTurn uiReadyTurn;
        public UICommon uiCommon;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetInfo_FishingEndUI(string fishName, int fishLevel, int fishPrice, Sprite fishImage)
        {
            uiReadyTurn.uiFishingEnd.SetText_FishName(fishName);
            uiReadyTurn.uiFishingEnd.SetImageByFishLevel(fishLevel);
            uiReadyTurn.uiFishingEnd.SetText_Money(fishPrice);
            uiReadyTurn.uiFishingEnd.SetImage_Fish(fishImage);

            uiReadyTurn.SetActive_FishingEndPanel(true);
        }
    }
}