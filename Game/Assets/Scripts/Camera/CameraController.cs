using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Click
    Camera myCam;
    public LayerMask clickable;
    public LayerMask ground;
    RaycastHit hit;

    // Move + Zoom
    private bool canMove = true;
    public float panSpeed = 30f;
    public float panBorder = 10f;
    public float scrollSpeed = 5f;
    private int minY = 20;
    private int maxY = 80;

    // Drag
    [SerializeField]
    private RectTransform boxVisual;
    private Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
        DrawSelection();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Zoom();

        // when clicked
        if (Input.GetMouseButtonDown(0))
        {
            CastRay("left");
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CastRay("right");
        }

        // when dragging
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
        // when released click
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Left mouse button released ! Adding selected units via drag");
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    private void Move()
    {
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
    }

    private void Zoom()
    {
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

    private void CastRay(string mouseButton)
    {
        
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (mouseButton == "left")
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                Debug.Log("CLickable object hit by raycast !");
                if (hit.collider.GetComponent<Unit>() != null)
                {
                    Unit unit = hit.collider.GetComponent<Mover>();
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        UnitSelections.Instance.ShiftClickSelect(unit);
                    }
                    else
                    {
                        UnitSelections.Instance.ClickSelect(unit);
                    }
                }
                
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }

        if (mouseButton == "right")
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                foreach (var unit in UnitSelections.Instance.unitsSelected)
                {
                    if (unit.GetComponent<Mover>() != null)
                    {
                        Mover mover = unit.GetComponent<Mover>();
                        mover.targetPosition = hit.point;
                        mover.Move();
                    }
                }
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (hit.collider.GetComponent<Unit>() != null)
                {
                    var hitUnit = hit.collider.GetComponent<Unit>();
                    foreach (var unit in UnitSelections.Instance.unitsSelected)
                    {
                        Unit unitTemp = unit.GetComponent<Unit>();
                        unitTemp.targetUnit = hitUnit;
                    }
                }
            }
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;
        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        // Check X direction
        if (Input.mousePosition.x < startPosition.x)
        {
            // Dragging left 
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            // Dragging right
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        // Check Y direction
        if (Input.mousePosition.y < startPosition.y)
        {
            // Dragging down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            // Dragging up
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                Debug.Log("Sending the unit to the DragSelect");
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}
