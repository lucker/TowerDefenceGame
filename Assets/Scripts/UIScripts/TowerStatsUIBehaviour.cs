using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerStatsUIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rangeText;
    [SerializeField] private TextMeshProUGUI _atackText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private GameObject _upgradeBlock;
    [SerializeField] private Button _upgradeButton;
    

    private BaseTowerBehaviour _currentTower;
    private static TowerStatsUIBehaviour _instance;

    private void Awake()
    {
        _instance = this;

        gameObject.SetActive(false);
        //_upgradeBlock.SetActive(false);
        //_upgradeCostText.text = "0";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
        }
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonBehaviour);
    }

    public static TowerStatsUIBehaviour GetInstance()
    {
        return _instance;
    }

    public void SetRange(float range)
    {
        _rangeText.text = range.ToString("0.0");
    }

    public void SetAtack(float atack)
    {
        _atackText.text = atack.ToString("0.0");
    }

    public void SetTower(GameObject tower)
    {
        _currentTower = tower.GetComponent<BaseTowerBehaviour>();
        SetRange(_currentTower.GetRange());
        SetAtack(_currentTower.GetAtack());
        SetUpgradeBlockTexts();
        gameObject.SetActive(true);
    }

    public void OnUpgradeButtonBehaviour()
    {
        Debug.Log("UPgrade");
        _currentTower = _currentTower.UpgradeTower();
        SetTower(_currentTower.gameObject);
    }

    private void SetUpgradeBlockTexts()
    {
        GameManager gameManager = GameManager.GetInstance();

        if (_currentTower.CanBeUpgraded())
        {
            _upgradeBlock.SetActive(true);
            _upgradeCostText.text = _currentTower.GetCostForUpgrade().ToString("0");
        }
        else
        {
            _upgradeBlock.SetActive(false);
        }
    }
}