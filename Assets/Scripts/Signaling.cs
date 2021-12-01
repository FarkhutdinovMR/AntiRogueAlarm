using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private MotionSensor _enterSensor;
    [SerializeField] private MotionSensor _exitSensor;
    [SerializeField] private float _volumeChangeSpeed;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    private AudioSource _audioSource;
    private Coroutine _changeVolume;
    private bool _isAlarm;

    private void OnEnable()
    {
        _enterSensor.Entered += OnEntered;
        _exitSensor.Entered += OnExit;
    }

    private void OnDisable()
    {
        _enterSensor.Entered -= OnEntered;
        _exitSensor.Entered -= OnExit;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEntered()
    {
        if (_isAlarm)
            return;

        _isAlarm = true;

        if (_changeVolume != null)
            StopCoroutine(_changeVolume);

        _changeVolume = StartCoroutine(nameof(IncreaseVolume));
    }

    private void OnExit()
    {
        if (_isAlarm == false)
            return;

        _isAlarm = false;

        if (_changeVolume != null)
            StopCoroutine(_changeVolume);

        _changeVolume = StartCoroutine(nameof(DecreaseVolume));
    }

    private IEnumerator IncreaseVolume()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }

        _audioSource.Stop();
    }
}