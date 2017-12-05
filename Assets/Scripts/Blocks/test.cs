using UnityEngine;
using System.Collections;

public class Snap : MonoBehaviour {

  public  string partnerTag;
  public  float closeVPDist = 0.05f;
  public float farVPDist = 1;
  public  float moveSpeed = 40.0f;
  public  float rotateSpeed = 90.0f;

  private Vector3 screenPoint;
  private Vector3 offset;
  private bool isSnaped;
    Color color = new Color(1, 0, 0);

    float dist = Mathf.Infinity;
    Color normalColor;
    GameObject partnerGO;
    // Use this for initialization
    void Start () {
        normalColor = GetComponent<Renderer>().material.color;
        partnerGO = GameObject.FindGameObjectWithTag(partnerTag);
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Cursor.visible = false;
    }
    void OnMouseDrag()
    {
        //transform.SetParent(null);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        Vector3 partnerPos = Camera.main.WorldToViewportPoint(partnerGO.transform.position);
        Vector3 myPos = Camera.main.WorldToViewportPoint(transform.position);
        dist = Vector2.Distance(partnerPos, myPos);
        GetComponent<Renderer>().material.color = (dist < closeVPDist) ? color : normalColor;
    }
    void OnMouseUp()
    {
        Cursor.visible = true;
        if (dist < closeVPDist)
        {
            transform.SetParent(partnerGO.transform);
            StartCoroutine(InstallPart());
            isSnaped = true;
        }
        if( dist > farVPDist)
        {
          //  transform.SetParent(null);
        }
    }
IEnumerator InstallPart()
    {
        while (transform.localPosition != Vector3.right || transform.localRotation != Quaternion.identity)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.right, Time.deltaTime * moveSpeed);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, Time.deltaTime * rotateSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
