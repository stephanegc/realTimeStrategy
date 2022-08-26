using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool canMove = true;
    public float panSpeed = 30f;
    public float panBorder = 10f;
    public float scrollSpeed = 5f;
    private int minY = 20;
    private int maxY = 80;

    // Update is called once per frame
    void Update()
    {
        // Vérification on/off
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canMove = !canMove;
        }

        if (!canMove)
        {
            return;
        }

        // Déplacement orthogonaux
        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= (Screen.height - panBorder))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorder)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= panBorder)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= (Screen.width - panBorder))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0f)
        {
            return;
        }

        Vector3 pos = transform.position;
        pos.y -= Mathf.Round(scroll * 1000 * scrollSpeed * Time.deltaTime);
        Debug.Log("position:" + pos.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        Debug.Log("position clamped:" + pos.y);
        transform.position = pos;
    }
}
