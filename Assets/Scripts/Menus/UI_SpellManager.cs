using System.Collections.Generic;
using UnityEngine;

public class UI_SpellManager : MonoBehaviour
{
    [SerializeField]
    private List<UI_Spell> spells;
    
    private int curSpell;

    public void ActivateSpell(int index, float cd)
    {
        spells[index].PickUpSpell(cd);
    }

    public void ChooseSpell(int index)
    {
        spells[curSpell].SetSpellActive(false);
        curSpell = index;
        spells[curSpell].SetSpellActive(true);
    }

    public void CastSpell(int index)
    {
        spells[curSpell].Cast();
    }
}
