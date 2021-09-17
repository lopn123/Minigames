using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Fishing
{
    public class UIFishingEnd : MonoBehaviour
    {
        [SerializeField]
        private Text text_money;
        [SerializeField]
        private Text text_fishName;
        [SerializeField]
        private Image image_fish;
        [SerializeField]
        private GameObject[] fishLevelImages;

        // Start is called before the first frame update
        void Start()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        public void SetText_Money(int amount, Color color)
        {
            text_money.text = "" + amount;

            if (color != null) text_money.color = color;
        }

        public void SetText_FishName(string name, Color color)
        {
            text_fishName.text = name;
            if (color != null) text_money.color = color;
        }

        public void SetImage_Fish(Sprite fishImage)
        {
            image_fish.sprite = fishImage;
        }

        public void SetImageByFishLevel(int fishLevel)
        {
            for (int i = 0; i < fishLevelImages.Length; i++)
            {
                if (i < fishLevel) fishLevelImages[i].SetActive(true);
                else fishLevelImages[i].SetActive(false);
            }
        }

        public void ClickedButton_GoNet()
        {

        }

        public void ClickedButton_UseBait()
        {

        }
    }
}