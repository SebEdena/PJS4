using UnityEngine;
using System.Collections;

public class CameraFixer : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    private Quaternion init_rota;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        init_rota = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = init_rota;
        transform.position = player.transform.position + offset;
    }
}
