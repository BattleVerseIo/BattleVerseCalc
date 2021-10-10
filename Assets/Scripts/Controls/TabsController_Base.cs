using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client.Core
{
    public abstract class TabsController_Base : MonoBehaviour
    {      
        public List<TabItem> tabs;

        protected int selectedIndex;

        private void Start()
        {            
            SelectItem(0);
        }

        public abstract void SelectItem(int inIndex);
        public abstract TabItem GetSelectedTab();
    }

    [Serializable]
    public struct TabItem
    {
        [SerializeField]
        public Image Tab;
        [SerializeField]
        public Transform Item;
        [SerializeField]
        public Button Button;
        [SerializeField]
        public Image SelectedTabBackground;
        [SerializeField]
        public AlertController Alert;
        [SerializeField]
        public int ContextValue;
        [SerializeField]
        public int ContextParent;
    }
}
