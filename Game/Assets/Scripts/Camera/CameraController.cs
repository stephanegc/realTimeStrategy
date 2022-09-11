using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
        startPosition = Input.mousePosition;
        endPosition = Input.mousePosition;
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
            startPosition = Input.mousePosition;
            endPosition = Input.mousePosition;
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
                Debug.Log("Clickable object hit by raycast !");
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null && unit.isSelectable)
                {
                    Mover mover = hit.collider.GetComponent<Mover>();
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
                List<Vector3> targetPositionList = GetPositionList(hit.point, 3f, UnitSelections.Instance.unitsSelected);
                int targetPositionListIndex = 0;
                Debug.Log("targetPositionList " + targetPositionList.Count);
                foreach (var unit in UnitSelections.Instance.unitsSelected)
                {
                    if (unit.GetComponent<Mover>() != null)
                    {
                        Mover mover = unit.GetComponent<Mover>();
                        mover.targetPosition = targetPositionList[targetPositionListIndex];
                        mover.aimingForTargetUnit = false;
                        targetPositionListIndex += 1;
                        Debug.Log(mover.targetPosition);
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
                        if (unit.GetComponent<Mover>() != null)
                        {
                            Mover mover = unit.GetComponent<Mover>();
                            mover.targetPosition = hitUnit.transform.position;
                            mover.aimingForTargetUnit = true;
                        }
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
            Debug.Log("Checking unit : " + unit);
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)) && unit.isSelectable)
            {
                Debug.Log("Sending the unit to the DragSelect");
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }

    private List<Vector3> GetPositionList(Vector3 hitPosition, float distance, List<Unit> units)
    {
        // Aim is to keep center relative to mouse both when odd (middle unit at center) and even (one unit on at half distance of center on each side)
        // Example ODD (1 unit : 0); (3 units: -1 0 +1); (5 units: -2 -1 0 +1 +2) %%% EVEN (2 units : -0.5 +0.5); (4 units : -1.5 -0.5 +0.5 +1.5)
        List<Vector3> positionList = new List<Vector3>();
        int positionCount = units.Count;
        var isEven = (positionCount % 2) == 0;
        int startIndex;
        float modifier;
        //float angle = Vector3.SignedAngle(units[0].transform.position, hitPosition, ); // angle needs to be computed only once for now

        var toHitPosition = hitPosition - units[0].transform.position;
        var toXaxis = Vector3.left - units[0].transform.position;
        var angle = Vector3.Angle(toHitPosition, toXaxis);
        Debug.Log("!!! ANGLE !!! : " + angle);

        if (isEven)
        {
            startIndex = positionCount / 2 * -1;  // cannot make it float ! ...
        }
        else
        {
            startIndex = (positionCount - 1) / 2 * -1;
        }
        IEnumerable<int> positionIndexes = Enumerable.Range(startIndex, positionCount);

        for (int i = 0; i < positionCount; i++)
        {
            Vector3 position = hitPosition;
            modifier = (float)positionIndexes.ElementAt(i);
            if (isEven)
            {
                modifier += 0.5f; // ... so we make it float after the fact !
            }
            //position.x += distance * modifier; // this is always making a line on the X axis, but we want the axis to be rotated according to the angle

            // How to make it rotate ?
            var x = Mathf.Cos(angle) * distance * modifier + hitPosition.x;
            var y = hitPosition.y;
            var z = Mathf.Sin(angle) * distance * modifier + hitPosition.z;
            var pos = new Vector3(x,y,z);

            positionList.Add(pos);
        }
        return positionList;
    }
}
