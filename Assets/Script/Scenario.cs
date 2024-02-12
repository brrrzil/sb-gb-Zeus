using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scenario : MonoBehaviour
{
    [Header ("Objects")]
    [SerializeField] GameObject Zeus;
    [SerializeField] GameObject // Появляющиеся надписи
        NotEnoughFood, N1, GameZone, WTF, WhoIs, Goose, Haha, LetsMake, MaybeTen,
        TheyEat, ICanFeed, Nice, WantMore, TwenyNoThirty, CreateThirty, CanDoBest,
        WorkHarder, ThatsBetter, LetsDoSmth, IFeelBetter, KeepItUp, ILikeIt, Move,
        MakeItBigger, DamnCool, WhoIsDaddy, ImDaddy, HellYes, GoodGoose, TakeYourTime;


    [Header("Smiles")]
    [SerializeField] SpriteRenderer SmileGO;
    [SerializeField] Sprite SunglassSmile, BadSurpriseSmile, AngryFckSmile, HahaSmile, ScarySmile, ExcitedSmile, VeryExcitedSmile; // Спрайты смайликов

    [Header("UI")]
    [SerializeField] TMP_Text CreateText;
    [SerializeField] GameObject CreateButton, FoodButton, PunishButton, TroodButton;
    [SerializeField] GameObject PopulationBar, FoodBar, MoodBar, TroodBar;

    Variables variables;

    // Булевы переменные для продвижения по сценарию
    private bool isFirstDweller = true;

    private bool isFeed = false;
    public bool IsFeed { get { return isFeed; } set { isFeed = value; } }

    private static bool isPressedFeed = false;
    public bool IsPressedFeed { get { return isPressedFeed; } set { isPressedFeed = value; } }

    private static bool isPunished = false;

    private static bool isPressedPunish = false;
    public bool IsPressedPunish { get { return isPressedPunish; } set { isPressedPunish = value; } }

    private bool isPressedPunishComplete = false;

    private bool isFirstBuild = false;
    public bool IsFirstBuild { get { return isFirstBuild; } set { isFirstBuild = value; } }

    private bool building1, building2, building3, building4, building5;

    private static bool isWin = false;
    public bool IsWin { get { return isWin; } set { isWin = value; } }

    void Start()
    {
        StartCoroutine(ZeusAppearance());
        variables = new Variables();
    }

    private void Update()
    {
        if (isFirstDweller && variables.Population == 1)
        {
            StartCoroutine(WhoIsCoroutine());
            isFirstDweller = false;
        }

        if (variables.Population >= 10 && !isFeed)
        {
            StartCoroutine(NeedFood());
            isFeed = true;
        }

        if (isPressedFeed)
        {
            StartCoroutine(LetsMakeMore());
            isPressedFeed = false;
        }

        if (variables.Population >= 30 && !isPunished)
        {
            StartCoroutine(TooLazy());
        }

        if (variables.Food < variables.MaxFood / 10) NotEnoughFood.SetActive(true);
        else NotEnoughFood.SetActive(false);

        if (isPressedPunish && !isPressedPunishComplete ) { StartCoroutine(ReadyToBuild()); isPressedPunishComplete = true; }

        if (variables.Energy == variables.MaxEnergy && !isFirstBuild) { StartCoroutine(FirstBuild()); }

        if (variables.Building == 1 && !building1) StartCoroutine(BuildingOne());
        if (variables.Building == 2 && !building2) StartCoroutine(BuildingTwo());
        if (variables.Building == 3 && !building3) StartCoroutine(BuildingThree());
        if (variables.Building == 4 && !building4) StartCoroutine(BuildingFour());
        if (variables.Building == 5 && !building5) StartCoroutine(BuildingFive());
    }

    private IEnumerator ZeusAppearance()
    {
        yield return new WaitForSeconds(4);
        Zeus.SetActive(true);
        yield return new WaitForSeconds(2);
        N1.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = SunglassSmile;
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;
        yield return new WaitForSeconds(1);
        N1.SetActive(false);
        StartCoroutine(GameZoneAppearance());
    }

    private IEnumerator GameZoneAppearance()
    {
        yield return new WaitForSeconds(3);
        GameZone.SetActive(true);
        yield return new WaitForSeconds(1);
        WTF.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = BadSurpriseSmile;
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;
        yield return new WaitForSeconds(1);
        WTF.SetActive(false);
        yield return new WaitForSeconds(2);
        CreateButton.SetActive(true);
    }    

    // Запускается при выполнении условия
    private IEnumerator WhoIsCoroutine()
    {
        yield return new WaitForSeconds(1);
        WhoIs.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = AngryFckSmile;
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;
        yield return new WaitForSeconds(1);
        WhoIs.SetActive(false);
        yield return new WaitForSeconds(1);
        Goose.SetActive(true);
        yield return new WaitForSeconds(2);
        CreateText.text = "Create гусь";
        PopulationBar.SetActive(true);
        yield return new WaitForSeconds(1);
        Goose.SetActive(false);
        StartCoroutine(MakeSomeMore());
    }   

    private IEnumerator MakeSomeMore()
    {
        yield return new WaitForSeconds(2);
        Haha.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = HahaSmile;
        yield return new WaitForSeconds(1);
        Haha.SetActive(false);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;

        yield return new WaitForSeconds(2);
        LetsMake.SetActive(true);
        yield return new WaitForSeconds(3);
        LetsMake.SetActive(false);

        yield return new WaitForSeconds(2);
        MaybeTen.SetActive(true);
        yield return new WaitForSeconds(3);
        MaybeTen.SetActive(false);
    }

    // Запускается при выполнении условия
    private IEnumerator NeedFood()
    {
        yield return new WaitForSeconds(1);
        FoodBar.SetActive(true);
        yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(1);
        TheyEat.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = ScarySmile;
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;
        yield return new WaitForSeconds(1);
        TheyEat.SetActive(false);

        yield return new WaitForSeconds(1);
        ICanFeed.SetActive(true);
        yield return new WaitForSeconds(3);
        ICanFeed.SetActive(false);
        FoodButton.SetActive(true);
    }

    // Запускается при выполнении условия
    private IEnumerator LetsMakeMore()
    {
        yield return new WaitForSeconds(2);
        Nice.SetActive(true);
        yield return new WaitForSeconds(3);
        Nice.SetActive(false);
        yield return new WaitForSeconds(1);
        WantMore.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = ExcitedSmile;
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;
        yield return new WaitForSeconds(1);
        WantMore.SetActive(false);
        yield return new WaitForSeconds(1);
        TwenyNoThirty.SetActive(true);
        yield return new WaitForSeconds(1);
        SmileGO.sprite = VeryExcitedSmile;
        yield return new WaitForSeconds(1);
        SmileGO.sprite = null;
        yield return new WaitForSeconds(1);
        TwenyNoThirty.SetActive(false);
        yield return new WaitForSeconds(1);
        CreateThirty.SetActive(true);
    }

    private IEnumerator TooLazy()
    {
        isPunished = true;
        yield return new WaitForSeconds(2);
        CreateThirty.SetActive(false);
        yield return new WaitForSeconds(1);
        TroodBar.SetActive(true);
        yield return new WaitForSeconds(1);
        CanDoBest.SetActive(true);
        yield return new WaitForSeconds(3);
        CanDoBest.SetActive(false);
        yield return new WaitForSeconds(1);
        WorkHarder.SetActive(true);
        yield return new WaitForSeconds(3);
        PunishButton.SetActive(true);
        yield return new WaitForSeconds(1);
        MoodBar.SetActive(true);
    }

    // Выполняется при первом нажатии кнопки наказания
    private IEnumerator ReadyToBuild()
    {
        WorkHarder.SetActive(false);
        yield return new WaitForSeconds(1);
        ThatsBetter.SetActive(true);
        yield return new WaitForSeconds(3);
        ThatsBetter.SetActive(false);
        yield return new WaitForSeconds(1);
        TroodButton.GetComponent<Button>().interactable = false;
        TroodButton.SetActive(true);
    }

    private IEnumerator FirstBuild() 
    { 
        isFirstBuild = true;
        yield return new WaitForSeconds(1);
        TroodButton.GetComponent<Button>().interactable = true;
        yield return new WaitForSeconds(1);
        LetsDoSmth.SetActive(true);
    }

    private IEnumerator BuildingOne()
    {
        building1 = true;
        LetsDoSmth.SetActive(false);
        IFeelBetter.SetActive(true);
        yield return new WaitForSeconds(3);
        IFeelBetter.SetActive(false);
        yield return new WaitForSeconds(1);
        KeepItUp.SetActive(true);
        yield return new WaitForSeconds(3);
        KeepItUp.SetActive(false);
    }

    private IEnumerator BuildingTwo()
    {
        building2 = true;
        ILikeIt.SetActive(true);
        yield return new WaitForSeconds(3);
        ILikeIt.SetActive(false);
        yield return new WaitForSeconds(15);
        Move.SetActive(true);
        yield return new WaitForSeconds(3);
        Move.SetActive(false);
    }

    private IEnumerator BuildingThree()
    {
        building3 = true;
        DamnCool.SetActive(true);
        yield return new WaitForSeconds(3);
        DamnCool.SetActive(false);
        yield return new WaitForSeconds(15);
        MakeItBigger.SetActive(true);
        yield return new WaitForSeconds(3);
        MakeItBigger.SetActive(false);
    }


    private IEnumerator BuildingFour()
    {
        building4 = true;
        WhoIsDaddy.SetActive(true);
        yield return new WaitForSeconds(3);
        WhoIsDaddy.SetActive(false);
        yield return new WaitForSeconds(15);
        ImDaddy.SetActive(true);
        yield return new WaitForSeconds(3);
        ImDaddy.SetActive(false);
    }

    private IEnumerator BuildingFive()
    {
        isWin = true;
        building5 = true;
        HellYes.SetActive(true);
        yield return new WaitForSeconds(3);
        HellYes.SetActive(false);
        yield return new WaitForSeconds(5);
        GoodGoose.SetActive(true);
        yield return new WaitForSeconds(3);
        GoodGoose.SetActive(false);
        yield return new WaitForSeconds(1);
        TakeYourTime.SetActive(true);
        yield return new WaitForSeconds(3);
        TakeYourTime.SetActive(false);
        yield return new WaitForSeconds(1);
        TroodBar.SetActive(false);
        yield return new WaitForSeconds(1);
        TroodButton.SetActive(false);
    }
}