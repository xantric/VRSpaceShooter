using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolShoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private float BulletSpeed = 100;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private AudioClip FireSound;
    [SerializeField]
    private AudioClip ChargingGun;

    [SerializeField]
    private XRBaseController leftController;
    [SerializeField]
    private XRBaseController rightController;
    [SerializeField]
    private float hapticAmplitude = 0.5f;
    [SerializeField]
    private float hapticDuration = 0.1f;

    [SerializeField]
    private XRDirectInteractor interactor;

    private bool isFiring = false;
    private Coroutine fireCoroutine;
    private Coroutine delayCoroutine;
    public float delayBeforeFiring = 1.0f;
    private AudioSource audioSource;
    [SerializeField] AudioSource grabSound;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(StartShooting);
        grabInteractable.deactivated.AddListener(StopShooting);
        grabInteractable.selectExited.AddListener(StopShootingOnRelease);
        grabInteractable.selectExited.AddListener(GrabGunOnLeaving);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartShooting(ActivateEventArgs args)
    {
        if (!isFiring)
        {
            isFiring = true;
            fireCoroutine = StartCoroutine(StartFiringAfterDelay());
        }
    }
    void StopShootingOnRelease(SelectExitEventArgs args)
    {
        if (isFiring)
        {
            isFiring = false;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
                delayCoroutine = null;
            }
        }
    }
    void GrabGunOnLeaving(SelectExitEventArgs args)
    {
        Invoke("AutoGrabGun", 2.0f);
    }
    void AutoGrabGun()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        interactor.interactionManager.SelectEnter(interactor, grabInteractable);
        grabSound.Play();
    }
    void StopShooting(DeactivateEventArgs args)
    {
        if (isFiring)
        {
            isFiring = false;
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
                delayCoroutine = null;
            }
        }
    }

    private IEnumerator StartFiringAfterDelay()
    {
        if (ChargingGun != null && audioSource != null)
        {
            audioSource.PlayOneShot(ChargingGun);
        }
        yield return new WaitForSeconds(delayBeforeFiring);
        fireCoroutine = StartCoroutine(FireContinuously());
    }

    private IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;

        ShootingSystem.Play();
        if (FireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(FireSound);
        }

        Vector3 direction = GetDirection();
        TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

        StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

        if (Physics.Raycast(BulletSpawnPoint.position, direction, out hit, 100f))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.Burst();
            }
        }

        TriggerHaptics();
    }

    private void TriggerHaptics()
    {
        if (leftController != null)
        {
            leftController.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }
        if (rightController != null)
        {
            rightController.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));
            remainingDistance -= BulletSpeed * Time.deltaTime;
            yield return null;
        }

        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
