using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    public void Shoot()
    {
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            readyToShoot = false;

            //Spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            //Calculate Direction with Spread
            Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
            {
                Debug.Log(rayHit.collider.name);
            }

            bulletsLeft--;
            bulletsShot--;
            Debug.Log(bulletsLeft);

            Invoke("ResetShot", timeBetweenShooting);

            if (bulletsShot > 0 && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
        }
    }
    public void ResetShot()
    {
        readyToShoot = true;
    }
    public void Reload()
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;
            Invoke("ReloadFinished", reloadTime);
        }
    }
    public void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}