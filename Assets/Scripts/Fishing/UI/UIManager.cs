using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private UIFishingEndManager uiFishingEndManager;

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
            uiFishingEndManager.SetText_FishName(fishName);
            uiFishingEndManager.SetImageByFishLevel(fishLevel);
            uiFishingEndManager.SetText_Money(fishPrice);
            uiFishingEndManager.SetImage_Fish(fishImage);
        }
    }
}