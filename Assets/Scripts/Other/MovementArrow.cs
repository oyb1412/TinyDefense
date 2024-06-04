using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MovementArrow : MonoBehaviour {
    public static MovementArrow Instance;
    private LineRenderer lineRenderer;
    private GameObject arrowHeadPrefab;
    private Cell currentSelect;
    private bool isPress;
    private float pressTimer;
    private Coroutine drawCoroutine;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();

        if (Instance == null) {
            Instance = this;
        } else
            Destroy(gameObject);
    }
    void Start() {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        arrowHeadPrefab = Managers.Resources.Instantiate(Define.ARROWHEAD_PATH);
        arrowHeadPrefab.transform.localScale = Vector3.zero;
    }

    public void DrawArrow(Vector3 startPos, TowerBase tower) {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(drawCoroutine != null)
            StopCoroutine(drawCoroutine);
        drawCoroutine = StartCoroutine(Co_DrawArrow(startPos, tower));
    }

    public IEnumerator Co_DrawArrow(Vector3 startPos, TowerBase tower) {
        isPress = false;
        pressTimer = 0f;
        while (true) {
            if (Input.GetMouseButton(0)) {
                pressTimer += Time.deltaTime;
                if (pressTimer > Define.PRESS_TIME)
                    isPress = true;
                else
                    isPress = false;

                if(isPress) {
                    lineRenderer.SetPosition(0, startPos);
                    lineRenderer.enabled = true;
                    arrowHeadPrefab.transform.localScale = Define.ARROWHEAD_SCALE;

                    Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    lineRenderer.SetPosition(1, currentMousePosition);

                    arrowHeadPrefab.transform.position = currentMousePosition;
                    Vector2 direction = (currentMousePosition - (Vector2)startPos).normalized;
                    arrowHeadPrefab.transform.right = direction;

                    if (currentSelect != null)
                        currentSelect.MovementDeSelect();

                    currentSelect = null;
                    currentSelect = Managers.Select.SelectCell();

                    if (currentSelect != null)
                        currentSelect.MovementSelect();
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                if(currentSelect != null && isPress
                    && !currentSelect.IsSelected) {
                    if(currentSelect.IsUse()) {
                        tower.ChangeTower(currentSelect.Tower);
                    }
                    else {
                        tower.ChangeCell(currentSelect);
                    }
                    if (currentSelect != null)
                        currentSelect.MovementDeSelect();
                    UI_TowerDescription.Instance.DeActivation();
                }

                lineRenderer.enabled = false;
                arrowHeadPrefab.transform.localScale = Vector3.zero;
                isPress = false;
                pressTimer = 0f;
                break;
            }
            yield return null;
        }
    }
    

    
}
