using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FighterScript : PlayerController
{
    public override void UseAbility1(){
        Attack();
        nextAttack = Time.time + (1/attackSpeed);
        mana -= 10;
        manabar.SetValue(mana);
    }

    public override void UseAbility2(){
        
    }

    public override void UseAbility3(){
        mana=maxmana;
        manabar.SetValue(mana);
    }

    public override void UseAbility4(){
        health += 20;
        healthbar.SetValue(health);
        mana -= 20;
        manabar.SetValue(mana);
    }
}
