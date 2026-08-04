using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup monsterHeaderGroup;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider healthSlider;

    private Enemy currentEnemy;

    public void SetEnemy(Enemy enemy)
    {
        currentEnemy = enemy;

        if (currentEnemy != null)
        {
            nameText.text = currentEnemy.enemyName;
            monsterHeaderGroup.alpha = 1f;
        }
        else
        {
            monsterHeaderGroup.alpha = 0f;
        }
    }

    void Update()
    {
        if (currentEnemy != null)
        {
            healthSlider.value = currentEnemy.GetHealthPercent();
        }
    }
}
