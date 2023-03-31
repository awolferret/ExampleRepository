using InteractiveObjects;
using InteractiveObjects.Build;
using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaitingSystem : MonoBehaviour , IDataPersistence
{
    private const string KEY = "RaitingSystem";

    [SerializeField] private RaitingView _raitingView;
    [SerializeField] private List<BuildPlace> _buildPlaces;
    [SerializeField] private List<BuildingContainer> _buildContainers;
    [SerializeField] private List<Production> _productions;
    [SerializeField] private WaiterContainer _waiterContainer;

    private float _currentRaiting = 0;
    private int _currentLevel = 0;
    private float _raitingPerLevel = 0.2f;
    private int _maxLevel = 5;

    private IEnumerator Start()
    {
        float time = 0.5f;
        var wait = new WaitForSeconds(time);
        yield return wait;
        _raitingView.ChangeView(_currentRaiting);
    }

    private void OnEnable()
    {
        foreach (var item in _buildPlaces)
        {
            item.OnBuilt += AddRaiting;
        }
        foreach (var item in _buildContainers)
        {
            item.OnBuilt += AddRaiting;
        }
        _waiterContainer.OnBuilt += AddRaiting;
    }

    private void OnDisable()
    {
        foreach (var item in _buildPlaces)
        {
            item.OnBuilt -= AddRaiting;
        }
        foreach (var item in _buildContainers)
        {
            item.OnBuilt -= AddRaiting;
        }
        _waiterContainer.OnBuilt -= AddRaiting;
    }

    public int GetCurrentRaiting()
    {
        switch (_currentLevel)
        {
            case 0:
                return 1;
            case 1:
                return 4;
            case 2:
                return 7;
            case 3:
                return 10;
            case 4:
                return 12;
            case 5:
                return 14;
            default:
                return 1;
        }
    }

    private void AddRaiting(float raitingAmount)
    {
        _currentRaiting += raitingAmount;
        _raitingView.ChangeView(_currentRaiting);

        if (_currentRaiting % _raitingPerLevel <= 0.01)
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                _raitingView.PlayAddStarAnimation();
            }
        }
    }

    public void Save()
    {
        SaverLoader.SaveData(KEY, GetSaveSnapshot());
    }

    public void Load()
    {
        var data = SaverLoader.LoadData<SaveSystem.RaitingData>(KEY);
        _currentRaiting = data.CurrentRaiting;
        _currentLevel = data.Level;
        _raitingView.ChangeView(_currentRaiting);
    }

    public DataToSave GetSaveSnapshot()
    {
        var data = new SaveSystem.RaitingData()
        {
            CurrentRaiting = _currentRaiting,
            Level = _currentLevel,
        };

        return data;
    }
}
