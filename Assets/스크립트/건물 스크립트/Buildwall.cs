using UnityEngine;
using UnityEngine.UI;

public class Buildwall : MonoBehaviour
{
    public GameObject buildingPrefab; // �ǹ� �������� ������ ����
    public Text woodCountText; // ���� ������ ǥ���ϴ� Text ������Ʈ ������ ����
    public int WoodDown = 5;
    public GameObject Incastle;
    private float YourFixedYPosition = -0.65f;
    private bool isBuildingMode = false; // �ǹ��� ���� ������� ����
    private GameObject previewBuilding = null;
    private float height;
    private Vector3 initialPosition;
    public GameObject objectToDisable;

    private void Awake()
    {
        // ���� ��ü(Buildwall)�� RectTransform ������Ʈ ��������
        RectTransform walltransform = GetComponent<RectTransform>();
        // ���� ���� height ������ ����
        height = walltransform.sizeDelta.y;
        initialPosition = transform.position;
       
    }

    void Update()
    {
        int woodCount = CurrencyManager.Instance.Wood;
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();

        if (woodCount <= 4)
        {
            ChangeWoodCountTextColorToRed();
        }
        else
        {
            ChangeWoodCountTextColorToBlack();
        }

        if (isBuildingMode)
        {
            if (woodCount >= 5)
            {
                // ���콺�� X ��ǥ�� ������ Y ��ǥ�� ���������� ����
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // 2D ���ӿ����� z ���� 0���� ����
                mousePosition.y = (float)YourFixedYPosition; // ���ϴ� Y ��ǥ�� ����

                // �̸������ �ǹ� ������Ʈ�� X ��ǥ�� ������Ʈ
                if (previewBuilding != null)
                {
                    previewBuilding.transform.position = new Vector3(mousePosition.x, (float)YourFixedYPosition, 0);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    // "wood" ��ȭ�� 5 �̻��� ��쿡�� �ǹ� ��ġ

                    // Ŭ���� ��ġ�� �ǹ��� ��ġ
                    Instantiate(buildingPrefab, mousePosition, Quaternion.identity);
                    isBuildingMode = !isBuildingMode;
                    Destroy(previewBuilding);
                    // �ǹ��� ���� �� "wood" ������ ���ҽ�Ŵ
                    if (currencyManager != null)
                    {
                        currencyManager.SpendCurrency(Currency.CurrencyType.Wood, WoodDown);
                    }
                }
            }
        }
    }

    public void ToggleBuildingMode()
    {
        isBuildingMode = !isBuildingMode; // �ǹ� ���� ��带 ���
        int woodCount = CurrencyManager.Instance.Wood;

        if (isBuildingMode && woodCount >= 5)
        {
            Incastle.SetActive(false);
            objectToDisable.SetActive(false);

            // ���콺�� X ��ǥ�� ������ Y ��ǥ�� ���������� ����
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D ���ӿ����� z ���� 0���� ����
            mousePosition.y = (float)YourFixedYPosition; // ���ϴ� Y ��ǥ�� ����

            // �̸������ �ǹ� ������Ʈ ����
            previewBuilding = Instantiate(buildingPrefab, new Vector3(mousePosition.x, (float)YourFixedYPosition, 0), Quaternion.identity);
            // �̸������ �ǹ� ������Ʈ�� ó������ ��Ȱ��ȭ
            if (previewBuilding != null)
            {
                previewBuilding.SetActive(true);
            }

            // ���ο� ��ġ ��� �� ����
            Vector2 offset = new Vector2(0, height * 30f);
            transform.position = (Vector2)transform.position + offset;

        }
    }

    // "wood" ������ 5 �̸��� �� Text ������ ���������� �����ϴ� �Լ�
    void ChangeWoodCountTextColorToRed()
    {
        // Text ������Ʈ�� ������ ���������� ����
        woodCountText.color = Color.red;
    }

    void ChangeWoodCountTextColorToBlack()
    {
        // Text ������Ʈ�� ������ ���������� ����
        woodCountText.color = Color.black;
    }

   public  void Comeback()
    {
        transform.position = initialPosition;
    }
}
