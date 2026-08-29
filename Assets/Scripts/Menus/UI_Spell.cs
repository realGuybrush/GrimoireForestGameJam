using UnityEngine;

public class UI_Spell : MonoBehaviour
{
    [SerializeField]
    private RectTransform cdImage;

    [SerializeField]
    private GameObject spellIcon, selectionGlow;

    private float cd, timer, portion;

    private Vector2 defaultSize;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            cdImage.sizeDelta = new Vector2(cdImage.sizeDelta.x, cdImage.sizeDelta.y - Time.deltaTime * portion);
        }
    }

    public void PickUpSpell(float CD)
    {
        spellIcon.SetActive(true);
        cdImage.gameObject.SetActive(true);
        cd = CD;
        defaultSize = cdImage.sizeDelta;
        portion = defaultSize.y / cd;
        cdImage.sizeDelta = new Vector2(defaultSize.x, 0f);
    }

    public void SetSpellActive(bool value)
    {
        selectionGlow.SetActive(value);
    }

    public void Cast()
    {
        timer = cd;
        cdImage.sizeDelta = defaultSize;
    }

}
