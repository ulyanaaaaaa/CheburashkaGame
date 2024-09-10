using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Gun _gun;
    
    private void Awake()
    {
        _gun = GetComponentInChildren<Gun>();
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && _gun.CanShoot && _gun.Ammo > 0)
        {
            _gun.Shoot();
        }

        if (Input.GetKeyUp(KeyCode.R))
            _gun.Reload();
    }
}
