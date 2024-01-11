using System.Collections;
using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, recoil;
    public int magazineSize, bulletsPerTap;
    public float bulletSpeed;
    public bool allowButtonHold;
    private int bulletsLeft, bulletsShot;

    //bools 
    bool readyToShoot, reloading;

    //bullet tracers
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private Transform trailSpawnPoint;

    //HUD
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadingText;

    //Reference
    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    private Recoil recoilSystem;

    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        recoilSystem = transform.Find("CameraRot/CameraRecoil").GetComponent<Recoil>();
        ammoText.text = NormalizeText($"Ammo: {bulletsLeft}/{magazineSize}");
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

            //muzzle flash
            //gonna add

            //RayCast
            TrailRenderer bulletTrail = Instantiate(trail, trailSpawnPoint.position, Quaternion.identity);
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
            {
                StartCoroutine(SpawnTrail(bulletTrail, rayHit.point));
            }
            else
            {
                StartCoroutine(SpawnTrail(bulletTrail, trailSpawnPoint.position + fpsCam.transform.forward * 100));
            }

            bulletsLeft--;
            bulletsShot--;

            ammoText.text = NormalizeText($"Ammo: {bulletsLeft}/{magazineSize}");

            recoilSystem.HandleRecoil();

            Invoke("ResetShot", timeBetweenShooting);

            if (bulletsShot > 0 && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }

            if(bulletsLeft == 0)
            {
                Reload();
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
            reloadingText.text = NormalizeText($"Reloading...");
            ammoText.text = NormalizeText($"Ammo: {bulletsLeft}/{magazineSize}");
            Invoke("ReloadFinished", reloadTime);
        }
    }
    public void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        reloadingText.text = "";
        ammoText.text = NormalizeText($"Ammo: {bulletsLeft}/{magazineSize}");
    }

    private IEnumerator SpawnTrail(TrailRenderer bulletTrail, Vector3 hit)
    {
        Vector3 startPosition = bulletTrail.transform.position;
        float distance = Vector3.Distance(bulletTrail.transform.position, hit);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            bulletTrail.transform.position = Vector3.Lerp(startPosition, hit, 1 - remainingDistance / distance);
            remainingDistance -= bulletSpeed * Time.deltaTime;
            yield return null;
        }

        bulletTrail.transform.position = hit;

        Destroy(bulletTrail.gameObject, bulletTrail.time);
    }

    string NormalizeText(string input)
    {
        string output = "";

        for(int i = 0; i < input.Length; i++)
        {
            output += input[^(i+1)];
        }

        return output;
    }
}