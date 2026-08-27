using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestControls : MonoBehaviour
{
    [SerializeField]
    private SpeechBubble speechBubble;
    
    [SerializeField]
    private PlayerSeekerTrigger talk, take;

    [SerializeField]
    private List<Quest> quests;

    private int questStep;

    [SerializeField]
    private Transform prizeSpawn;
    
    public event Action OnAllDone = delegate { };

    private void Awake()
    {
        talk.OnPlayerEnter += Talk;
        take.OnPlayerEnter += TakeItem;
    }

    private void Talk(PlayerMovement player)
    {
        speechBubble.ShowMessage(quests[questStep].GetText());
        if (quests[questStep].ItemName == "")
        {
            questStep++;
            if(questStep < quests.Count)
                speechBubble.ShowMessage(quests[questStep].GetText());
        }
    }
    
    private void TakeItem(PlayerMovement player)
    {
        if (quests[questStep].IsCorrectItemThere(player.Items))
        {
            if(questStep < quests.Count)
                speechBubble.ShowMessage(quests[questStep].Gratitude + " " + quests[questStep + 1].GetText(), true);
            else
                speechBubble.ShowMessage(quests[questStep].Gratitude, true);
            if (quests[questStep].Prize != null)
                Instantiate(quests[questStep].Prize, prizeSpawn.position, new Quaternion());
            questStep++;
        }
        if (questStep == quests.Count-1)
        {
            OnAllDone?.Invoke();
            TurnOffTriggers();
        }
    }

    public void TurnOffTriggers()
    {
        talk.gameObject.SetActive(false);
        take.gameObject.SetActive(false);
    }

    public void TellHimISaidF___YOU()
    {
        speechBubble.ShowMessage("Che fai? Fuck you!", true);
    }

    private void OnDestroy()
    {
        talk.OnPlayerEnter -= Talk;
        take.OnPlayerEnter -= TakeItem;
    }
}
