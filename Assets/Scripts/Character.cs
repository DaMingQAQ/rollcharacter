using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : MonoBehaviour
{
    public static Character instance;


    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
            
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public string characterName;
    public string race;
    public string charaClass;
    public string alignment;
    public int xp;
    public int hp;
    public int armorClass;
    public int speed;
    public List<string> items = new List<string>();
    public int abilityStrenght = 2;
    public int abilityDexterity = 2;
    public int abilityConstitution = 2;
    public int abilityIntelligence = 2;
    public int abilityWisdon = 2;
    public int abilityCharisma = 2;

}
