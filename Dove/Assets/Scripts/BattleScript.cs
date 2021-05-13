using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BattleScript : NetworkBehaviour
{
    int AttackerHP;
    int DefenderHP;
    int AttackerHPLeft;
    int DefenderHPLeft;
    List<int> result = new List<int>();

    public int Battle(UnitInfoScript attacker, UnitInfoScript defender)
    {
        AttackerHP = attacker.GetUnitOPower();
        DefenderHP = defender.GetUnitOPower();
        DefenderHPLeft = DefenderHP - ((int)Mathf.Round(AttackerHP/2)+1);
        Debug.Log("Defender HP Left = " + DefenderHPLeft + " Round is " + ((int)Mathf.Round(AttackerHP/2)+1));
        AttackerHPLeft = AttackerHP - ((int)Mathf.Round(DefenderHP/2)+1);
        Debug.Log("Attacker HP  Left = " + AttackerHPLeft + " Round is " + ((int)Mathf.Round(DefenderHP/2)+1));
        if (AttackerHPLeft <= 0 && DefenderHPLeft <= 0)
        {
            attacker.SetHP(1);
            defender.DestroyUnit();
            return 0;
        }
        if (AttackerHPLeft <= 0 && DefenderHPLeft > 0)
        {
            attacker.DestroyUnit();
            defender.SetHP(DefenderHPLeft);
            return 1;
        }
        if (AttackerHPLeft > 0 && DefenderHPLeft <= 0)
        {
            attacker.SetHP(AttackerHPLeft);
            defender.DestroyUnit();
            return 2;
        }
        if (AttackerHPLeft > 0 && DefenderHPLeft > 0)
        {
            attacker.SetHP(AttackerHPLeft);
            defender.SetHP(DefenderHPLeft);
            return 3;
        }
        else return 3;
    }
}
