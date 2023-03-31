using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    public void ShowMoney(int money)
    {
        _moneyText.text = money.ToString() + "$";
    }
}