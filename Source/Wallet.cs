using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SaveSystem;

public class Wallet : MonoBehaviour , IDataPersistence
{
    [SerializeField] private List<WalletUI> _walletUI;
    [SerializeField] private int _money = 0;

    public int Money => _money;

    public event UnityAction OnMoneyChanged;

    private void Start()
    {
        foreach (var wallet in _walletUI) 
        {
            wallet.ShowMoney(_money);
        }
        OnMoneyChanged?.Invoke();
    }

    public void AddMoney(int moneyAmount)
    {
        _money += moneyAmount;

        foreach (var wallet in _walletUI)
        {
            wallet.ShowMoney(_money);
        }
        OnMoneyChanged?.Invoke();
    }

    public bool TrySpendMoney(int moneyAmount)
    {
        if (moneyAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(moneyAmount));

        if (_money >= moneyAmount)
        {
            _money -= moneyAmount;

            foreach (var wallet in _walletUI)
            {
                wallet.ShowMoney(_money);
            }

            OnMoneyChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Save()
    {
        SaverLoader.SaveData(gameObject.name, GetSaveSnapshot());
    }

    public void Load()
    {
        var data = SaverLoader.LoadData<SaveSystem.WalletData>(gameObject.name);
        _money = data.CurrentMoney;
    }

    public DataToSave GetSaveSnapshot()
    {
        var data = new SaveSystem.WalletData()
        {
            CurrentMoney = _money,
        };

        return data;
    }
}
