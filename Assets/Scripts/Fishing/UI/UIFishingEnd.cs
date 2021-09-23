using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Fishing
{
    public class UIFishingEnd : MonoBehaviour
    {
        [SerializeField]
        private UIReadyTurn uiReadyTurn;
        [SerializeField]
        private Text text_money;
        [SerializeField]
        private Text text_fishName;
        [SerializeField]
        private Image image_fish;
        [SerializeField]
        private GameObject[] fishLevelImages;
        [SerializeField]
        private FishingFloat fishingFloat;

        private int fishLevel;

        public void SetText_Money(int amount)
        {
            text_money.text = "" + amount;
        }

        public void SetText_FishName(string name)
        {
            text_fishName.text = name;
        }

        public void SetImage_Fish(Sprite fishImage)
        {
            image_fish.sprite = fishImage;
        }

        public void SetImageByFishLevel(int level)
        {
            for (int i = 0; i < fishLevelImages.Length; i++)
            {
                if (i < level) fishLevelImages[i].SetActive(true);
                else fishLevelImages[i].SetActive(false);
            }

            fishLevel = level + 1;
        }

        public void ClickedButton_SellFish()
        {
            fishingFloat.level = 1;
            GameManager.instance.GetMoney(int.Parse(text_money.text));
            uiReadyTurn.SetActive_FishingEndPanel(false);
        }

        public void ClickedButton_UseBait()
        {
            fishingFloat.level = fishLevel;
            uiReadyTurn.SetActive_FishingEndPanel(false);
        }
    }
}