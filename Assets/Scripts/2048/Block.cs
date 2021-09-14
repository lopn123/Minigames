using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.TwoZeroFourEight
{
    public class Block : MonoBehaviour
    {
        public int num { get; private set; }

        public void SetNum(int Num)
        {
            num = Num;
            GetComponent<Text>().text = "" + num;
        }
    }
}