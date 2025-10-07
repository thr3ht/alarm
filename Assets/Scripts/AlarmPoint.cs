using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlarmPoint : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;
    [SerializeField] private float _volumeSpeed = 1f;
    
    private Color _defaultColor;
    private Color _alarmColor;
    private bool _thiefInside = false;  
    private Material _alarmMaterial;

    private void Start()
    {
        _alarmMaterial = GetComponent<Renderer>().material;
        _defaultColor = _alarmMaterial.color;
        _alarmColor = new Color(1f, 0f, 0f, _defaultColor.a);
        _alarm.volume = 0f;
    }

    private void Update()
    {
        float targetVolume;
        
        if (_thiefInside)
        {
            targetVolume = 1f;
        }
        else
        {
            targetVolume = 0;
        }
        
        _alarm.volume = Mathf.MoveTowards(_alarm.volume, targetVolume, _volumeSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief _))
        {
            _alarmMaterial.color = _alarmColor;
            _thiefInside = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief _))
        {
            _alarmMaterial.color = _defaultColor;
            _thiefInside = false;
        }
    }
}
