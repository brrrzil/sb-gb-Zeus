using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Variables : MonoBehaviour
{
    private static float buttonSpeed = 1;
    private static int population = 0, maxPopulation = 50; 
    private static float food = 50, maxFood = 100;
    private static float mood = 0.5f, maxMood = 1;
    private static float vitality = 0.5f;
    private static float movementSpeed = 1;
    private static float laziness = 0.5f;
    private static float energy = 0, maxEnergy = 100;
    private static float building = 0; // Доделать этапы строительства

    public float ButtonSpeed { get { return buttonSpeed; } set { buttonSpeed = value; } }
    public int Population { get { return population; } set { population = value; } }
    public int MaxPopulation { get { return maxPopulation; } set { maxPopulation = value; } }
    public float Food { get { return food; } set { food = value; } }
    public float MaxFood { get { return maxFood; } set { maxFood = value; } }
    public float Mood { get { return mood; } set { mood = value; } }
    public float Laziness { get { return laziness; } set { laziness = value; } }
    public float Energy { get { return energy; } set { energy = value; } }
    public float MaxEnergy { get { return maxEnergy; } set { maxEnergy = value; } }
    public float Building { get { return building; } set { building = value; } }
    public float Vitality { get { return vitality; } set { } }

    public void IncreasePopulation() { population++; }
    public void DecreasePopulation() { population--; }
    
    public void IncreaseFood(float value) { food += maxFood / 5; if (food >= maxFood) food = maxFood; }
    public void DecreaseFood(float value) { food -= value; if (food <= 0) food = 0; }

    //[Header("Debug")]
    [SerializeField] TMP_Text PopulationText, FoodText, MoodText, LazinessText, VitalityText, MovingSpeedText, EnergyText, ButtonSpeedText, BuildingStatusText;
    private void Update()
    {
        PopulationText.text = population.ToString() + " / " + maxPopulation;
        FoodText.text = food.ToString() + " / " + maxFood;
        MoodText.text = mood.ToString() + " / " + maxMood;
        LazinessText.text = laziness.ToString();
        VitalityText.text = vitality.ToString();
        MovingSpeedText.text = movementSpeed.ToString();
        EnergyText.text = energy.ToString() + " / " + maxEnergy;
        ButtonSpeedText.text = buttonSpeed.ToString();
        BuildingStatusText.text = building.ToString();
    }

    public void MoodGrowth() // Запускается из контроллера
    {
        mood += 0.1f * vitality * (food / maxFood);
        if (mood >= maxMood) mood = maxMood;
    }

    public void VitalityValue()
    {
        vitality = food / maxFood * mood *2;
    }

    public void LazinessGrowth() // Запускается из контроллера
    {
        laziness += 0.02f * mood;
        if (laziness >= 1) laziness = 1;
    }

    public void EnergyGrowth() // Запускается из контроллера
    {
        energy += 2.1f * laziness * population / maxPopulation;
        if (energy >= maxEnergy) energy = maxEnergy;
    }

    public float MovementSpeed() // Используется в Dweller
    {
        movementSpeed = mood / laziness;
        return movementSpeed;
    }

    public void Upgrade(float value)
    {
        buttonSpeed *= value;
        maxPopulation = (int)(maxPopulation * value);
        maxFood *= value;
        maxEnergy *= value;
        mood *= value * 2;
        movementSpeed *= value;
        energy = 0;
    }    

    public int DeathRandom()
    {
        return Random.Range(0, (int)(vitality * 70));
    }

    public int RestRandom()
    {
        return Random.Range(0, (int)(2 / laziness));
    }
}