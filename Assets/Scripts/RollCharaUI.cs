using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RollCharaUI : MonoBehaviour
{
    public InputField nameInput;
    public InputField alignmentInput;
    public InputField expInput;
    public InputField hpInput;
    public InputField armorInput;
    public InputField speedInput;
    public Dropdown raceChoose;
    public Dropdown classChoose;

    public Text abilities;
    public Text info;
    public Text output;

    int abilityStrenght = 2;
    int abilityDexterity = 2;
    int abilityConstitution = 2;
    int abilityIntelligence = 2;
    int abilityWisdon = 2;
    int abilityCharisma = 2;

    Dictionary<string, string> raceDict = new Dictionary<string, string>();
    Dictionary<string, string> classDict = new Dictionary<string, string>();

    void Start()
    {
        raceDict.Add("Dragonborn", "Your draconic heritage manifests in a variety of traits you share with other dragonborn.");
        raceDict.Add("Dwarf", "Your dwarf character has an assortment of in abilities, part and parcel of dwarven nature.");
        raceDict.Add("Elf", "Your elf character has a variety of natural abilities, the result of thousands of years of elven refinement.");
        raceDict.Add("Gnome", "Your gnome character has certain characteristics in common with all other gnomes.");
        raceDict.Add("Half - Elf", "Your half-elf character has some qualities in common with elves and some that are unique to half-elves.");
        raceDict.Add("Half - Orc", "Your half-orc character has certain traits deriving from your orc ancestry.");
        raceDict.Add("Halfling", "Your halfling character has a number of traits in common with all other halflings.");
        raceDict.Add("Human", "It's hard to make generalizations about humans, but your human character has these traits.");
        raceDict.Add("Tiefling", "Tieflings share certain racial traits as a result of their infernal descent.");

        raceChoose.ClearOptions();
        raceChoose.AddOptions(raceDict.Keys.ToList());
        raceChoose.onValueChanged.AddListener((x) => { info.text = raceDict[raceChoose.captionText.text]; UpdateInfo(); });

        classDict.Add("Barbarian", "In battle, you fight with primal ferocity.For some barbarians, rage is a means to an end–that end being violence.");
        classDict.Add("Bard", "Whether singing folk ballads in taverns or elaborate compositions in royal courts, bards use their gifts to hold audiences spellbound.");
        classDict.Add("Cleric", "Clerics act as conduits of divine power.");
        classDict.Add("Druid", "Druids venerate the forces of nature themselves. Druids holds certain plants and animals to be sacred, and most druids are devoted to one of the many nature deities.");
        classDict.Add("Fighter", "Different fighters choose different approaches to perfecting their fighting prowess, but they all end up perfecting it.");
        classDict.Add("Monk", "Coming from monasteries, monks are masters of martial arts combat and meditators with the ki living forces.");
        classDict.Add("Paladin", "Paladins are the ideal of the knight in shining armor; they uphold ideals of justice, virtue, and order and use righteous might to meet their ends.");
        classDict.Add("Ranger", "Acting as a bulwark between civilization and the terrors of the wilderness, rangers study, track, and hunt their favored enemies.");
        classDict.Add("Rogue", "Rogues have many features in common, including their emphasis on perfecting their skills, their precise and deadly approach to combat, and their increasingly quick reflexes.");
        classDict.Add("Sorcerer", "An event in your past, or in the life of a parent or ancestor, left an indelible mark on you, infusing you with arcane magic.As a sorcerer the power of your magic relies on your ability to project your will into the world.");
        classDict.Add("Warlock", "You struck a bargain with an otherworldly being of your choice: the Archfey, the Fiend, or the Great Old One who has imbued you with mystical powers, granted you knowledge of occult lore, bestowed arcane research and magic on you and thus has given you facility with spells.");
        classDict.Add("Wizard", "The study of wizardry is ancient, stretching back to the earliest mortal discoveries of magic. As a student of arcane magic, you have a spellbook containing spells that show glimmerings of your true power which is a catalyst for your mastery over certain spells.");

        classChoose.ClearOptions();
        classChoose.AddOptions(classDict.Keys.ToList());
        classChoose.onValueChanged.AddListener((x) => { info.text = classDict[classChoose.captionText.text]; UpdateInfo(); });


        info.text = "";
        output.text = "";

        if(Character.instance.abilityDexterity==2)
        RollAbility();


        LoadInfo();
    }

    public void UpdateInfo()
    {
        Character.instance.characterName = nameInput.text;
        Character.instance.alignment= alignmentInput.text;

        if (expInput.text.Length == 0)
            Character.instance.xp = 0;
        else
            Character.instance.xp = int.Parse(expInput.text);

        if (hpInput.text.Length == 0)
            Character.instance.hp = 0;
        else
        Character.instance.hp = int.Parse(hpInput.text);

        if (armorInput.text.Length == 0)
            Character.instance.armorClass = 0;
        else
            Character.instance.armorClass = int.Parse(armorInput.text);

        if (speedInput.text.Length == 0)
            Character.instance.speed = 0;
        else
            Character.instance.speed = int.Parse(speedInput.text);

        Character.instance.charaClass = classChoose.captionText.text;
        Character.instance.race = raceChoose.captionText.text;

        Character.instance.abilityStrenght = abilityStrenght;
        Character.instance.abilityWisdon = abilityWisdon;
        Character.instance.abilityCharisma = abilityCharisma;
        Character.instance.abilityConstitution = abilityConstitution;
        Character.instance.abilityDexterity = abilityDexterity;
        Character.instance.abilityIntelligence = abilityIntelligence;

        RefreshJson();
    }


    void LoadInfo()
    {
        nameInput.text= Character.instance.characterName;
        alignmentInput.text= Character.instance.alignment ;
        expInput.text = Character.instance.xp.ToString();
        hpInput.text= Character.instance.hp.ToString();
        armorInput.text= Character.instance.armorClass.ToString();
        speedInput.text= Character.instance.speed.ToString();

        classChoose.captionText.text = Character.instance.charaClass;
        raceChoose.captionText.text = Character.instance.race;

        abilityStrenght = Character.instance.abilityStrenght;
        abilityWisdon = Character.instance.abilityWisdon;
        abilityCharisma = Character.instance.abilityCharisma;
        abilityConstitution = Character.instance.abilityConstitution;
        abilityDexterity = Character.instance.abilityDexterity;
        abilityIntelligence = Character.instance.abilityIntelligence;

    }


    int RollNum()
    {
        List<int> d6List = new List<int>();
        List<int> d4List = new List<int>();

        d6List.Add(UnityEngine.Random.Range(1, 7));
        d6List.Add(UnityEngine.Random.Range(1, 7));
        d6List.Add(UnityEngine.Random.Range(1, 7));
        d4List.Add(UnityEngine.Random.Range(1, 5));
        d4List.Add(UnityEngine.Random.Range(1, 5));
        d4List.Add(UnityEngine.Random.Range(1, 5));
        d6List.Sort();
        d4List.Sort();
        d6List.RemoveAt(0);
        d4List.RemoveAt(0);
        return d6List.Sum() + d4List.Sum() + 2;
    }


    public void RollAbility()
    {
        abilityStrenght = RollNum();
        abilityDexterity = RollNum();
        abilityConstitution = RollNum();
        abilityIntelligence = RollNum();
        abilityWisdon = RollNum();
        abilityCharisma = RollNum();

        abilities.text = "Abilities:\n" +
            $"Strength: {abilityStrenght}\n" +
            $"Dexterity: {abilityDexterity}\n" +
            $"Constitution: {abilityConstitution}\n" +
            $"Intelligence: {abilityIntelligence}\n" +
            $"Wisdon: {abilityWisdon}\n" +
            $"Charisma: {abilityCharisma}";
        UpdateInfo();
    }



    void RefreshJson()
    {
        string json = JsonUtility.ToJson(Character.instance);
        output.text = json;
    }




}
