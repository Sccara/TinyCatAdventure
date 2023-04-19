using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : ObjectStats
{
    #region Singleton

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one PlayerStats instances");
            return;
        }
        instance = this;
    }

    #endregion

    public SceneFader sceneFader;
    public Animator animator;

    [Header("Player Stats")]
    public float cold;
    public float stamina;
    public float money;
    public bool isFrozen;
    public bool questIsCompleted = false;
    public bool isBusy = false;
    public Quest quest;

    [Header("Player Attack")]
    public float attackRange = 1f;
    public float attackForce = 5f;
    public float attackRate = 1f;
    public bool isAttack = false;

    [Header("Positive/Negative Effects Rate")]
    public float coldDecreaseRate = 2f;
    public float staminaRecoveryRate = 5f;
    public float healthDecreaseRate = 1f;

    [HideInInspector]
    public float maxCold = 100;
    [HideInInspector]
    public float maxStamina = 100;

    [Header("Stat Bars")]
    public Image healthBar;
    public Image staminaBar;
    public Image coldBar;

    [Header("Stat Texts")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        questIsCompleted = false;

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        UpdateHealth(maxHealth);
        UpdateStamina(maxStamina);
        UpdateCold(maxCold);
    }

    private void Update()
    {
        if (!isAttack && !isFrozen && !GetComponent<PlayerMovement>().isRunning)
        {
            IncreaseStaminaOverTime(staminaRecoveryRate * Time.deltaTime);
        }

        if (Health <= 0)
        {
            Die();
        }

        FrozenCheck();

        UpdateUIStats();
    }

    public override void Die()
    {
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        if (animator.GetBool("IsFlip"))
        {
            animator.Play("IdleSittingFlip");
        }
        else
        {
            animator.Play("IdleSitting");
        }
       
        GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(2);

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void UpdateHealth(float h)
    {
        Health += h;

        if (Health >= maxHealth)
            Health = maxHealth;
    }

    public void UpdateStamina(float s)
    {
        stamina += s;

        if (stamina >= maxStamina)
            stamina = maxStamina;
    }

    public void UpdateCold(float c)
    {
        cold += c;

        if (cold >= maxCold)
            cold = maxCold;
    }

    void UpdateUIStats()
    {
        attackText.text = "Attack: " + damage.GetValue().ToString();
        armorText.text = "Armor: " + armor.GetValue().ToString();
        moneyText.text = "     " + money;

        healthBar.fillAmount = Health / maxHealth;
        staminaBar.fillAmount = stamina / maxStamina;
        coldBar.fillAmount = cold / maxCold;
    }

    public void IncreaseStaminaOverTime(float rate)
    {
        UpdateStamina(rate);
    }

    public void FrozenCheck()
    {
        if (cold <= 0)
        {
            cold = 0;
            isFrozen = true;
        }
        else
        {
            isFrozen = false;
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }
}
