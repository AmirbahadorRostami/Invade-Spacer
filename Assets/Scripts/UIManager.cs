using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceInvader
{
    
    public enum UIView
    {
        MainMenu,
        GameView,
        PauseMenu,
    }

    public class UIManager : MonoBehaviour
    {

        public List<GameObject> UIViews;
        private PlayerShip player;
        
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private TextMeshProUGUI Health;
        
        
        public void setHealth(int amount)
        {
            Health.text = amount.ToString();
        }

        public void setScore(int amount)
        {
            Score.text = amount.ToString();
        }
        
        public void ChangeUI(UIView type)
        {
            ClearViews();
            switch (type)
            {
                case UIView.MainMenu:
                    UIViews[0].SetActive(true);
                    break;
                case UIView.PauseMenu:
                    UIViews[1].SetActive(true);
                    break;
                case UIView.GameView:
                    UIViews[2].SetActive(true);
                    break;
            }
        }

        private void ClearViews()
        {
            foreach (var view in UIViews)
            {
                view.SetActive(false);
            }
        }
        
    }

}
