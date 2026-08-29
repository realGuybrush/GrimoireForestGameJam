using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float disappearTimerPerSymbol;

    private float timer;

    private List<string> futureMessages = new List<string>();

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (futureMessages.Count > 0)
                {
                    UpdateMessage(futureMessages[0]);
                    futureMessages.RemoveAt(0);
                }
                else
                {
                    text.text = "";
                    gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void ShowMessage(string messageText, bool FORCE = false)
    {
        gameObject.SetActive(true);
        UpdateMessage(messageText, FORCE);
    }

    private void UpdateMessage(string messageText, bool FORCE = false)
    {
        if (FORCE)
        {
            timer = 0;
            futureMessages.Clear();
        }
        if (timer > 0)
        {
            futureMessages.Add(messageText);
        } else
        {
            text.text = messageText;
            timer = disappearTimerPerSymbol * messageText.Length;   
        }
    }

    public bool HasFutureText => futureMessages.Count > 0;
}
