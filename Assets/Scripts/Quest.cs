using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    [SerializeField]
    private string firstText, secondText, gratitude, item;

    [SerializeField]
    private ItemEnum questItem;

    [SerializeField]
    private bool finished;

    [SerializeField]
    private Item prize;
    
    private bool firstTime = true;

    public string GetText()
    {
        if (firstTime)
        {
            firstTime = false;
            return firstText;
        }
        return secondText;
    }

    public bool IsCorrectItemThere(List<ItemEnum> checkedItems)
    {
        if (checkedItems.Contains(questItem))
            finished = true;
        return finished;
    }

    public string ItemName => item;
    
    public string Gratitude => gratitude;

    public Item Prize => prize;

}
