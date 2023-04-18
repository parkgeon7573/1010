using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    AnimationCurve curveMovement;
    [SerializeField]
    AnimationCurve curveScale;

    BlockArrangeSystem blockArrangeSystem;

    float appearTime = 0.5f;
    float returntime = 0.1f;

    [field:SerializeField]
    public Vector2Int BlockCount { private set; get; }

    public Color Color { get; private set; }
    public Vector3[] ChildBlocks { get; private set; }

    public void Setup(BlockArrangeSystem blockArrangeSystem, Vector3 parentPosition)
    {
        this.blockArrangeSystem = blockArrangeSystem;

        Color = GetComponentInChildren<SpriteRenderer>().color;

        ChildBlocks = new Vector3[transform.childCount];

        for(int i = 0; i < ChildBlocks.Length; ++i)
        {
            ChildBlocks[i] = transform.GetChild(i).localPosition; 
        }
        StartCoroutine(OnMoveTo(parentPosition, appearTime));
    }

    private void OnMouseDown()
    {
        StopCoroutine("OnScaleTo");
        StartCoroutine("OnScaleTo", Vector3.one);
    }

    private void OnMouseDrag()
    {
        //���� ��� ����� Pivot�� ��ϼ��� ���߾����� �����Ǿ� �ֱ� ������ x ��ġ�� �״�� ����ϰ�,

        // y ��ġ�� y�� ��� ������ ����(BlockCount.y * 0.5f)�� gap��ŭ �߰��� ��ġ�� ���

        //Camera.main.ScreenToWorldPoint()�� Vector3 ��ǥ�� ���ϸ� z ���� ī�޶��� ��ġ�� -10�� ������ ������
        //gap ���� z���� +10����� ���� ������Ʈ���� ��ġ�Ǿ� �ִ� z=0 ���� �����ȴ�.
        Vector3 gap = new Vector3(0, BlockCount.y * 0.3f + 0.5f, 10);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + gap;
    }

    private void OnMouseUp()
    {
        float x = Mathf.RoundToInt(transform.position.x - BlockCount.x % 2 * 0.5f) + BlockCount.x % 2 * 0.5f;
        float y = Mathf.RoundToInt(transform.position.y - BlockCount.y % 2 * 0.5f) + BlockCount.y % 2 * 0.5f;

        transform.position = new Vector3(x, y, 0);

        bool isSuccess = blockArrangeSystem.TryArrangementBlock(this);

        if(isSuccess == false)
        {
            StopCoroutine("OnScaleTO");
            StartCoroutine("OnScaleTo", Vector3.one * 0.5f);

            StartCoroutine(OnMoveTo(transform.parent.position, returntime));
        }
    }

    IEnumerator OnMoveTo(Vector3 end, float time)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.position = Vector3.Lerp(start, end, curveMovement.Evaluate(percent));

            yield return null;
        }
    }

    IEnumerator OnScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / returntime;

            transform.localScale = Vector3.Lerp(start, end, curveScale.Evaluate(percent));

            yield return null;
        }
    }
}
