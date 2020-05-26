using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float changeInterval;

    private float _changeTime;
    private int _direction = 1;

    private void Update()
    {
        transform.Translate(Vector2.up * (_direction * speed * Time.deltaTime));
        
        if (Time.time < _changeTime)
        {
            return;
        }

        _direction = -_direction;
        _changeTime = Time.time + changeInterval;
    }
}
