using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Action<float, float> OnAmmoChanged;

    [SerializeField] private protected Ball _ball;
    [SerializeField] private protected Transform _spawnPoint;
    [SerializeField] private protected float _delay;
    [SerializeField] private protected int _maxAmmo;
    [SerializeField] private protected float _reloadDelay;

    [field: SerializeField] public bool CanShoot { get; set; } = true;
    [field: SerializeField] public int Ammo { get; private protected set; } = 10;

    public virtual void Shoot()
    {
        CanShoot = false;
        Ball ballCreated = Instantiate(_ball, _spawnPoint.position, Quaternion.identity).GetComponent<Ball>();
        ballCreated.Fly(_spawnPoint.transform.forward, 50);
        Ammo--;
        OnAmmoChanged?.Invoke(Ammo, _maxAmmo);
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        if (Ammo < 0) CanShoot = false;
        else CanShoot = true;
    }

    public void Reload()
    {
        CanShoot = false;
        StartCoroutine(ReloadTick());
    }

    public IEnumerator ReloadTick()
    {
        yield return new WaitForSeconds(_reloadDelay);
        Ammo = _maxAmmo;
        CanShoot = true;
    }
}
