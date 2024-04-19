using System.Collections;
using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    [SerializeField] private int damage;
    [SerializeField] private float spread, range, reloadTime, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
    private int bulletsLeft, bulletsShot;
    public float timeBetweenShooting, recoil;

    //bools 
    bool readyToShoot, reloading;

    //bullet tracers
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private Transform trailSpawnPoint;
    [SerializeField]
    private float trailSpeed;

    //HUD
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI reloadingText;

    //Reference
    [SerializeField] private Camera fpsCam;
    [SerializeField] private RaycastHit rayHit;
    [SerializeField] private LayerMask whatIsEnemy;
    private Recoil recoilSystem;
    private GunRecoil gunRecoilSystem;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        recoilSystem = transform.Find("CameraRot/CameraRecoil").GetComponent<Recoil>();
        gunRecoilSystem = transform.Find("CameraRot/CameraRecoil/Main Camera/RevolverModel2").GetComponent<GunRecoil>();
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
                StartCoroutine(SpawnTrail(bulletTrail, rayHit.point, trailSpeed));
            }
            else
            {
                StartCoroutine(SpawnTrail(bulletTrail, trailSpawnPoint.position + fpsCam.transform.forward * 100, trailSpeed));
            }

            bulletsLeft--;
            bulletsShot--;

            ammoText.text = NormalizeText($"Ammo: {bulletsLeft}/{magazineSize}");

            recoilSystem.HandleRecoil();
            gunRecoilSystem.HandleRecoil();

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
    private void ResetShot()
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
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        reloadingText.text = "";
        ammoText.text = NormalizeText($"Ammo: {bulletsLeft}/{magazineSize}");
    }

    private IEnumerator SpawnTrail(TrailRenderer bulletTrail, Vector3 hit, float trailSpeed)
    {
        Vector3 startPosition = bulletTrail.transform.position;
        float distance = Vector3.Distance(bulletTrail.transform.position, hit);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            bulletTrail.transform.position = Vector3.Lerp(startPosition, hit, (1 - remainingDistance / distance) * trailSpeed);
            remainingDistance -= trailSpeed * Time.deltaTime;
            yield return null;
        }

        bulletTrail.transform.position = hit;

        Destroy(bulletTrail.gameObject, bulletTrail.time);
    }

    private string NormalizeText(string input)
    {
        string output = "";

        for(int i = 0; i < input.Length; i++)
        {
            output += input[^(i+1)];
        }

        return output;
    }
}