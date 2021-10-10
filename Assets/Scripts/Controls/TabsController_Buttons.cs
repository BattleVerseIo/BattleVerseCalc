using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client.Core
{
    public class TabsController_Buttons : TabsController_Base
    {
        public Sprite NormalSprite;
        public Sprite SelectedSprite;                        

        public override void SelectItem (int inIndex)
        {
            if (selectedIndex == inIndex)
                return;
            if (inIndex < 0 || inIndex >= tabs.Count)
                return;

            if (selectedIndex < tabs.Count)
            {
                tabs[selectedIndex].Tab.sprite = NormalSprite;
                tabs[selectedIndex].Item.gameObject.SetActive(false);
            }

            tabs[inIndex].Tab.sprite = SelectedSprite;
            tabs[inIndex].Item.gameObject.SetActive(true);
            selectedIndex = inIndex;
        }

        public override TabItem GetSelectedTab()
        {
            return tabs[selectedIndex];
        }
    }    
}
