using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Ÿ�� �̵� ��� ǥ�� Ŭ����
/// </summary>
public class MovementArrow : MonoBehaviour {
    public static MovementArrow Instance;
    //�̵� ��� ǥ�� ���� ������
    private LineRenderer lineRenderer;
    //�̵� ��� ��� ȭ��ǥ ������Ʈ
    private GameObject arrowHeadPrefab;
    //���� �������� ��
    private Cell currentSelect;
    //press���ΰ�?
    private bool isPress;
    //press�� �ð�
    private float pressTimer;
    //�̵� ��� ��ο� �ڷ�ƾ
    private Coroutine drawCoroutine;
    //ī�޶� ĳ��
    private Camera mainCamera;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();

        if (Instance == null) {
            Instance = this;
        } else
            Destroy(gameObject);

        mainCamera = Camera.main;
    }

    void Start() {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        arrowHeadPrefab = Managers.Resources.Activation(Managers.Data.DefineData.ARROWHEAD_PATH);
        arrowHeadPrefab.transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// Ÿ�� ���ý� ��� ��ο� ����
    /// </summary>
    /// <param name="startPos">���� ������</param>
    public void DrawArrow(Vector3 startPos, TowerBase tower) {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(drawCoroutine != null)
            StopCoroutine(drawCoroutine);

        drawCoroutine = StartCoroutine(Co_DrawArrow(startPos, tower));
    }

    /// <summary>
    /// Ÿ�� ��� ��ο� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    public IEnumerator Co_DrawArrow(Vector3 startPos, TowerBase tower) {
        isPress = false;
        pressTimer = 0f;
        while (true) {
            bool isTouching = false;
            Vector2 currentTouchPosition = Vector2.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
            // ���콺 �Է� ó��
            if (Input.GetMouseButton(0)) {
                isTouching = true;
                currentTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            } else if (Input.GetMouseButtonUp(0)) {
                if (currentSelect != null && isPress && !currentSelect.IsSelected) {
                    if (currentSelect.IsUse()) {
                        tower.ChangeTower(currentSelect.Tower);
                    } else {
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
#elif UNITY_ANDROID
        // ��ġ �Է� ó��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                isTouching = true;
                currentTouchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (currentSelect != null && isPress && !currentSelect.IsSelected)
                {
                    if (currentSelect.IsUse())
                    {
                        tower.ChangeTower(currentSelect.Tower);
                    }
                    else
                    {
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
        }
#endif

            if (isTouching) {
                pressTimer += Time.deltaTime;
                if (pressTimer > Managers.Data.DefineData.PRESS_TIME)
                    isPress = true;
                else
                    isPress = false;

                //��ġ�� ��� �ؼ� press���°� �Ǹ� ��ο� ����
                if (isPress) {
                    lineRenderer.SetPosition(0, startPos);
                    lineRenderer.enabled = true;
                    arrowHeadPrefab.transform.localScale = Managers.Data.DefineData.ARROWHEAD_SCALE;

                    lineRenderer.SetPosition(1, currentTouchPosition);

                    arrowHeadPrefab.transform.position = currentTouchPosition;
                    Vector2 direction = (currentTouchPosition - (Vector2)startPos).normalized;
                    arrowHeadPrefab.transform.right = direction;

                    if (currentSelect != null)
                        currentSelect.MovementDeSelect();

                    currentSelect = null;
                    currentSelect = Managers.Select.SelectCell();

                    if (currentSelect != null)
                        currentSelect.MovementSelect();
                }
            }

            yield return null;
        }
    }
}
