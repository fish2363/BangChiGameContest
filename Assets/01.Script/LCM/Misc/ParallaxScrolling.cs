using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private Vector3 _startPos;
    private Camera _cam;
    public float parallaxFactor;

    void Start()
    {
        _cam = Camera.main;
        _startPos = transform.position;
    }

    void Update()
    {
        float distX = _cam.transform.position.x * parallaxFactor;
        float distY = _cam.transform.position.y * parallaxFactor;

        transform.position = new Vector3(_startPos.x + distX, _startPos.y + distY, transform.position.z);
    }
}
