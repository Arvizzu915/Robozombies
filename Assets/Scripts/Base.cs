using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Slider baseHealthSlider;
    private float currentBaseHealth = 100, maxBaseHealth = 100;

    private void Start()
    {
        currentBaseHealth = maxBaseHealth;
        baseHealthSlider.value = maxBaseHealth;
    }

    public void TakeDamage(float damage)
    {
        currentBaseHealth -= damage;
        baseHealthSlider.value = currentBaseHealth;

        if (currentBaseHealth <= 0)
        {
            //lose
        }
    }
}
