using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NumGates;

public class UIAllyStatCustomize : MonoBehaviour
{
    [Header("Custom")]
    [SerializeField] private string stat;
    [SerializeField] private int maxPoint;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button plusButton;
    [SerializeField] private TextMeshProUGUI statPointText;
    [SerializeField] private TextMeshProUGUI maxText;

    public float StatPoint => statPoint;
    private float statPoint;

    private void OnEnable()
    {
        minusButton.onClick.AddListener(OnClickMinus);
        plusButton.onClick.AddListener(OnClickPlus);
    }

    private void OnDisable()
    {
        minusButton.onClick.RemoveListener(OnClickMinus);
        plusButton.onClick.RemoveListener(OnClickPlus);
    }

    public void InitUI()
    {
        statPoint = 0;
        SetStatText();
        SetPointText();
        SetMaxText("");
        //EnableMaxText(true);
        EnableMinusButton(false);
        EnablePlusButton(false);
    }

    public void EnableCustomize(bool isEnable)
    {
        EnableMinusButton(isEnable);
        EnablePlusButton(isEnable);

        if (isEnable == true)
        {
            EnableMinusButton(IsMinPoint(statPoint));
            EnablePlusButton(IsMaxPoint(statPoint));
        }
    }

    public void LoadStatPoint(float point)
    {
        statPoint = point;
        SetStatText();
    }

    private void SetStatText()
    {
        TheBoxUI.SetTextByString(statText, stat);
    }

    private void SetPointText()
    {
        TheBoxUI.SetTextByValue(statPointText, statPoint);
    }

    private void SetMaxText(string text)
    {
        TheBoxUI.SetTextByString(maxText, text);
    }

    private int GetStatPoint()
    {
        return TheBoxUI.GetTextValueInt(statPointText);
    }

    private void EnableMinusButton(bool isEnable)
    {
        TheBoxUI.EnableButton(minusButton, isEnable);
    }

    private void EnablePlusButton(bool isEnable)
    {
        TheBoxUI.EnableButton(plusButton, isEnable);
    }

    private void OnClickMinus()
    {
        int tempPoint = GetStatPoint();

        if (!IsMinPoint(tempPoint - 1))
        {
            statPoint -= 1;
            SetPointText();

            if (IsMaxPoint(statPoint))
            {
                SetMaxText("");
                EnablePlusButton(true);
            }

            EnableMinusButton(IsMinPoint(statPoint));
        }
    }

    private void OnClickPlus()
    {
        int tempPoint = GetStatPoint();

        if (IsMaxPoint(tempPoint + 1))
        {
            statPoint += 1;
            SetPointText();

            if (IsMaxPoint(statPoint))
            {
                SetMaxText("M");
                EnablePlusButton(false);
            }

            EnableMinusButton(IsMinPoint(tempPoint));
        }
    }

    private bool IsMaxPoint(float point)
    {
        return point == maxPoint;
    }

    private bool IsMinPoint(float point)
    {
        return point == 0;
    }
}
