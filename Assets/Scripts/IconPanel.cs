using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IconPanel : MonoBehaviour {

    List<Icon> icons;
    List<Icon> reversedIcons = new List<Icon>();
    //public bool On = false;
    //public bool Off = false;

    //private void Update()
    //{
        //    if (Off)
        //    {
        //        IconDisable(stars);
        //        Off = false;
        //    }

        //    if (On)
        //    {
        //        IconEnable(stars);
        //        On = false;
        //    }
    //}

    void Awake ()
    {
        Icon[] tempIcons = GetComponentsInChildren<Icon>();
        icons = tempIcons.OrderBy(o => o.iconIndex).ToList();  // sorted list by index
        foreach (Icon icon in icons)
        {
            reversedIcons.Insert(0,icon); //reversed list by index
        }
    }

    public void IconDisable(int iconsToDisable)
    {
        int i = 1;
        foreach (Icon icon in icons)
        {
            if (icon.isEnabled)
            {
                if (i > iconsToDisable)
                {
                    return;
                }
                i++;
                icon.ChangeStatus();
            }
        }
    }

    public void IconDisable()
    {
        IconDisable(1);
    }

    public void IconEnable(int iconsToEnable)
    {
        int i = 1;
        foreach (Icon icon in icons)
        {
            if (!icon.isEnabled)
            {
                if (i > iconsToEnable)
                {
                    return;
                }
                i++;
                icon.ChangeStatus();
            }
        }
    }

    public void IconEnable()
    {
        IconEnable(1);
    }
}
