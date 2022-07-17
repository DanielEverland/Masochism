using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    /// <summary>
    /// The minimum amount of force of a collision before sound is played
    /// </summary>
    [SerializeField]
    private float _minMagnitude = 0.1f;

    /// <summary>
    /// Time between each time a sound is played
    /// </summary>
    [SerializeField]
    private float _minTimeBetweenSounds = 0.2f;

    /// <summary>
    /// Determines how loud a sound is based upon the amount of force of the collision
    /// </summary>
    [SerializeField]
    private AnimationCurve _volumeImpulse;

    [SerializeField] private float _minPitch = 0.8f;
    [SerializeField] private float _maxPitch = 1.2f;

    private float _lastTimeSoundPlayed;

    private void OnCollisionEnter(Collision collision)
    {
        TriggerCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        TriggerCollision(collision);
    }

    private void TriggerCollision(Collision collision)
    {
        if (Time.time - _lastTimeSoundPlayed < _minTimeBetweenSounds)
            return;

        float hitMagnitude = collision.impulse.magnitude;

        if (hitMagnitude < _minMagnitude)
            return;

        _lastTimeSoundPlayed = Time.time;

        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.volume = _volumeImpulse.Evaluate(hitMagnitude);
        _audioSource.Play();
    }

    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
