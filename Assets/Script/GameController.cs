using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private float FoodValue;

    [SerializeField] Dweller dweller;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Image dwellerCountBar, foodBar, moodBar, troodBar;
    [SerializeField] SpriteRenderer Statue;
    [SerializeField] Sprite Statue0, Statue1, Statue2, Statue3, Statue4, Statue5;
    [SerializeField] Button CreateButton, FoodButton, PunishButton, TroodButton;
    [SerializeField] Button PlayPauseButt, MuteButt, RestartButt, FastButt;
    [SerializeField] Sprite Play, Pause, MuteOn, MuteOff;
    [SerializeField] GameObject PunishEffect;
    [SerializeField] GameObject RestartPanelGO;

    Scenario scenario;    
    Variables variables;

    float x, z;

    private bool isFoodPressed = false;

    private bool isPause = false, isFast = false, isMute = false;

    void Start()
    {
        scenario = new Scenario();
        variables = new Variables();
        StartCoroutine(UpdateVariables());
    }

    void Update() 
    {
        dwellerCountBar.fillAmount = (float)variables.Population / variables.MaxPopulation;
        foodBar.fillAmount = variables.Food / variables.MaxFood;
        moodBar.fillAmount = variables.Mood;
        troodBar.fillAmount = variables.Energy / variables.MaxEnergy;

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CreateButton.interactable && CreateButton.isActiveAndEnabled) CreateDweller();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FoodButton.interactable && FoodButton.isActiveAndEnabled) AddFood();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PunishButton.interactable && PunishButton.isActiveAndEnabled) Punish();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (TroodButton.interactable && TroodButton.isActiveAndEnabled) Build();
        }
    }

    public void CreateDweller()
    {
        if (variables.Population < variables.MaxPopulation)
        {
            x = (float)Random.Range(dweller.LeftBorder, dweller.RightBorder + 1);
            z = (float)Random.Range(dweller.BottomBorder, dweller.TopBorder + 1);

            Instantiate(dweller, new Vector3(x, transform.position.y, z), new Quaternion());
            variables.IncreasePopulation();
            StartCoroutine(ButtonDelay(CreateButton, variables.ButtonSpeed));
            CreateButton.GetComponent<AudioSource>().Play();
        }
    }

    public void AddFood()
    {
        variables.IncreaseFood(FoodValue);
        StartCoroutine(ButtonDelay(FoodButton, variables.ButtonSpeed * 5));
        if (!isFoodPressed) { scenario.IsPressedFeed = true; isFoodPressed = true; }
        FoodButton.GetComponent<AudioSource>().Play();
    }

    public void Punish()
    {
        variables.Laziness = variables.Laziness * 0.5f;
        variables.Mood = variables.Mood * 0.5f;

        if (!scenario.IsPressedPunish) scenario.IsPressedPunish = true;

        PunishButton.GetComponent<AudioSource>().Play();
        StartCoroutine(PunishEffectCoroutine());
        StartCoroutine(ButtonDelay(PunishButton, variables.ButtonSpeed * 10));
    }

    public void Build() // Строительство статуи / Апгрейд Зевса
    {
        //if (!scenario.IsFirstBuild) scenario.IsFirstBuild = true;
        variables.Energy = 0;
        TroodButton.interactable = false;
        variables.Building++;
        TroodButton.GetComponent<AudioSource>().Play();

        switch (variables.Building)
        {
            case 1:
                variables.Upgrade(1.2f);
                Statue.sprite = Statue1;
                break;
            case 2:
                variables.Upgrade(1.2f);
                Statue.sprite = Statue2;
                break;
            case 3:
                variables.Upgrade(1.15f);
                Statue.sprite = Statue3;
                break;
            case 4:
                variables.Upgrade(1.15f);
                Statue.sprite = Statue4;
                break;
            case 5:
                variables.Upgrade(1.1f);
                Statue.sprite = Statue5;
                break;
        }
    }

    private IEnumerator ButtonDelay(Button button, float time)
    {
        button.interactable = false;
        yield return new WaitForSeconds(time * 2f);
        button.interactable = true;
    }

    private IEnumerator UpdateVariables()
    {
        yield return new WaitForSeconds(1);
        variables.MoodGrowth();
        variables.LazinessGrowth();
        variables.EnergyGrowth();
        variables.VitalityValue();
        variables.MovementSpeed();
        StartCoroutine(UpdateVariables());
        if (variables.Energy == variables.MaxEnergy) TroodButton.interactable = true;        
    }

    private IEnumerator PunishEffectCoroutine()
    {
        PunishEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        PunishEffect.SetActive(false);
    }

    public void RestartButton()
    {
        RestartButt.GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioSource.pitch = -1;
        Time.timeScale = 1;
    }

    public void PlayPauseButton()
    {
        PlayPauseButt.GetComponent<AudioSource>().Play();

        if (!isPause)
        {
            Time.timeScale = 0;
            isPause = true;
            isFast = false;
            PlayPauseButt.GetComponent<Image>().sprite = Play;
            audioSource.Pause();
            audioSource.pitch = -1;            
        }

        else
        {
            Time.timeScale = 1;
            isPause = false;
            isFast = false;
            PlayPauseButt.GetComponent<Image>().sprite = Pause;
            audioSource.Play();
            audioSource.pitch = -1;
        }
    }

    public void FastButton()
    {
        FastButt.GetComponent<AudioSource>().Play();

        if (!isFast)
        {
            Time.timeScale = 2;
            audioSource.Play();
            audioSource.pitch = -2;
            isFast = true;
        }

        else
        {
            Time.timeScale = 1;
            audioSource.Play();
            audioSource.pitch = -1;
            isFast = false;
        }
    }

    public void Mute()
    {
        MuteButt.GetComponent<AudioSource>().Play();

        if (!isMute)
        {
            audioSource.Pause();
            MuteButt.GetComponent<Image>().sprite = MuteOff;
            isMute = true;
        }

        else
        {
            audioSource.Play();
            MuteButt.GetComponent<Image>().sprite = MuteOn;
            isMute = false;
        }
    }
}