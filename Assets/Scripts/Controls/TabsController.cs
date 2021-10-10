using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client.Core
{
    public class TabsController : TabsController_Base
    {
        public bool SortTabs;
        public Color NormalColor;
        public Color SelectedColor;

        public Action<int, TabItem> OnSwitchTab;

        public override void SelectItem (int inIndex)
        {
            if (selectedIndex == inIndex)
                return;
            if (inIndex < 0 || inIndex >= tabs.Count)
                return;

            if (selectedIndex < tabs.Count)
            {
                if (tabs[selectedIndex].Tab)
                    tabs[selectedIndex].Tab.color = NormalColor;
                if (tabs[selectedIndex].Item != null)
                    tabs[selectedIndex].Item.gameObject.SetActive(false);
                tabs[selectedIndex].SelectedTabBackground?.gameObject.SetActive(false);
            }

            if (tabs[inIndex].Tab)
                tabs[inIndex].Tab.color = SelectedColor;
            tabs[inIndex].SelectedTabBackground?.gameObject.SetActive(true);
            if (SortTabs)
                tabs[inIndex].Tab.transform.SetAsLastSibling();
            if (tabs[inIndex].Item != null)
                tabs[inIndex].Item.gameObject.SetActive(true);
            selectedIndex = inIndex;

            OnSwitchTab?.Invoke(selectedIndex, tabs[inIndex]);
        }

        public bool SelectItemSilent(int inIndex)
        {
            if (selectedIndex != inIndex)
            {
                selectedIndex = inIndex;
                return true;
            }
            
            return false;
        }

        public override TabItem GetSelectedTab()
        {
            return tabs[selectedIndex];
        }
    }    
}
