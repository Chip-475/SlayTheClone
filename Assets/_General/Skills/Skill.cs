using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using static SkillDataSO;

public class Skill : MonoBehaviour
{
    #region Declarations
    protected CombatManager Manager => CombatManager.instance;

    [SerializeField] SkillDataSO data;

    [Header("Info")]
    public int id;
    public bool unlocked;
    public string skillName;
    public SkillType skillType;
    public int damagePercentage;
    public List<DamageTypes> damageTypes;
    [Space]

    [Header("Meta")]
    public Image image;
    public Button button;
    public TMP_Text nameText;
    public bool isSelected;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        id = data.id;
        unlocked = data.unlocked;
        skillName = data.skillName;
        skillType = data.skillType;
        damagePercentage = data.damagePercentage;
        damageTypes = new List<DamageTypes>(data.damagePercentage);

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        nameText = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        nameText.text = skillName;
    }
    #endregion

    #region Methods
    public virtual void Effect(Enemy target) { return; }
    public virtual void Effect(Player target) { return; }

    public virtual void OnPointerEnter()
    {
        transform.DOScale(1.05f, 0.1f);
    }
    public void OnPointerExit()
    {
        transform.DOScale(1f, 0.1f);
    }
    public virtual void OnPointerClick()
    {
        isSelected = true;
        Manager.selectedSkill = this;
    }
    #endregion
}
