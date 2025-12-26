
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clocks : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image clocksFill;
    [SerializeField] private TMP_Text clocksAmount;
    [SerializeField] private Button clocksButton;
    [Space(5)]
    [SerializeField] private Slider resourceSlider;
    [SerializeField] private TMP_Text resourceAmount;
    [Space(15)]
    [Header("Settings")]

    [SerializeField] private float firstTimeLimit = 5f;
    [SerializeField] private float secondTimeLimit = 10f;

    [Space(5)]

    [SerializeField] private int foodReward = 3;
    [SerializeField] private int foodPrice = 5;
    

    private float firstTimer = 0f;
    private float secondTimer = 0f;
    private bool isFirstTimerActive = true;
    private int firstLaunches = 0;

    private int foodAmount = 0;


    void Start()
    {
        clocksButton.onClick.AddListener(StartTimer);
        UpdateUI();
        
    }

    void Update()
    {
        UpdateClocksTimer();
        UpdateResourceTimer();

        clocksButton.interactable = !isFirstTimerActive && foodAmount >= foodPrice;
    }


    void UpdateClocksTimer()
    {
        if (isFirstTimerActive)
        {
            firstTimer -= Time.deltaTime;
            if (firstTimer <= 0)
            {
                firstTimer = firstTimeLimit;
                isFirstTimerActive = false;
                foodAmount += foodReward;
                UpdateUI();
            }
        }

        clocksFill.fillAmount = firstTimer / firstTimeLimit;
    }

    void UpdateResourceTimer()
    {
        secondTimer += Time.deltaTime;
        if (secondTimer >= secondTimeLimit)
        {
            secondTimer = 0f;
            foodAmount += foodReward;
            UpdateUI();
        }

        resourceSlider.value = secondTimer / secondTimeLimit;
    }

    void StartTimer()
    {
        if (isFirstTimerActive || foodAmount < foodPrice) return;
        firstTimer = firstTimeLimit;
        isFirstTimerActive = true;
        firstLaunches++;
        foodAmount -= foodPrice;
        UpdateUI();

    }

    void UpdateUI()
    {
        clocksAmount.text = $"Запуски: {firstLaunches}";
        resourceAmount.text = $"{foodAmount} еды";
    }
}
