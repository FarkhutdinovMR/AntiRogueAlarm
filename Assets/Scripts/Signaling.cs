using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private MotionSensor _enterSensor;
    [SerializeField] private MotionSensor _exitSensor;
    [SerializeField] private float _volumeSwitchDuration;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    private AudioSource _audioSource;
    private bool _isAlarm;
    private float _runningTime;

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

    private void Update()
    {
        if (_isAlarm && _runningTime <= _volumeSwitchDuration)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_minVolume, _maxVolume, _runningTime / _volumeSwitchDuration);
        }
        else if(_isAlarm == false && _runningTime >= 0f)
        {
            _runningTime -= Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_minVolume, _maxVolume, _runningTime / _volumeSwitchDuration);

            if (_audioSource.volume <= _minVolume)
                _audioSource.Stop();
        }
    }

    private void OnEntered()
    {
        if (_isAlarm == false)
        {
            _audioSource.Play();
            _isAlarm = true;
        }
    }

    private void OnExit()
    {
        _isAlarm = false;
    }
}