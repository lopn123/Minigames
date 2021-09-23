using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Fishing
{
    public class UICommon : MonoBehaviour
    {
        [SerializeField]
        private Text text_money;

        public void SetText_Money(int value)
        {
            int beforeMoney = int.Parse(text_money.text);
            string text = (beforeMoney + value).ToString();
            text_money.text = text;
        }
    }
}