using UnityEngine;

public class Rogue : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    }
}