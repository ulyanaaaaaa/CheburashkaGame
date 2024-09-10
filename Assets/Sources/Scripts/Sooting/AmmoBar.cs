using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    private Image _ammoBar;
    [SerializeField] private Gun _gun;

    private void Start()
    {
        _ammoBar = GetComponent<Image>();
        _gun.OnAmmoChanged += DisplayAmmo;
    }

    private void DisplayAmmo(float ammo, float maxAmmo)
    {
        _ammoBar.fillAmount = ammo / maxAmmo;
    }
}
