using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : Item
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private SpriteRenderer darkening;

    [SerializeField]
    private float darkeningTime;

    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                SceneManager.LoadScene(sceneName);
            else
                darkening.color = new Color (0f, 0f, 0f, 1f - timer / darkeningTime);
        }
    }

    protected override void TriggerAction(Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>()!= null)
            if(timer <= 0)
                timer = darkeningTime;
    }
}
