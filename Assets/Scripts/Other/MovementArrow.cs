using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// 타워 이동 경로 표시 클래스
/// </summary>
public class MovementArrow : MonoBehaviour {
    public static MovementArrow Instance;
    //이동 경로 표기 라인 렌더러
    private LineRenderer lineRenderer;
    //이동 경로 헤드 화살표 오브젝트
    private GameObject arrowHeadPrefab;
    //현재 선택중인 셀
    private Cell currentSelect;
    //press중인가?
    private bool isPress;
    //press한 시간
    private float pressTimer;
    //이동 경로 드로우 코루틴
    private Coroutine drawCoroutine;
    //카메라 캐싱
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
    /// 타워 선택시 경로 드로우 시작
    /// </summary>
    /// <param name="startPos">시작 포지션</param>
    public void DrawArrow(Vector3 startPos, TowerBase tower) {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(drawCoroutine != null)
            StopCoroutine(drawCoroutine);

        drawCoroutine = StartCoroutine(Co_DrawArrow(startPos, tower));
    }

    /// <summary>
    /// 타워 경로 드로우 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator Co_DrawArrow(Vector3 startPos, TowerBase tower) {
        isPress = false;
        pressTimer = 0f;
        while (true) {
            bool isTouching = false;
            Vector2 currentTouchPosition = Vector2.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
            // 마우스 입력 처리
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
        // 터치 입력 처리
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

                //터치를 길게 해서 press상태가 되면 드로우 시작
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
