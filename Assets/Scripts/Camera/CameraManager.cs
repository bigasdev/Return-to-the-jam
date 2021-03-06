using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<CameraManager>();
            }
            return instance;
        }
    }
    [SerializeField] float offset;
    [SerializeField] float damping;
    public void killShake() => OnCameraShake(.05f, .25f);
    public void critShake() => OnCameraShake(.025f, .125f);
    public static void CritShake() => CameraManager.Instance.critShake();
    public static void KillShake() => CameraManager.Instance.killShake();
    //Parameter to set an action to be thrown at the end of the zoom
    public event Action OnEndZoom = delegate{};
    public Camera mainCamera;
    public Transform obj;
    private Vector2 p = Vector2.zero;
    private float originalSize;
    Coroutine shakeRoutine, zoomRoutine;
    private void Start() {
        this.transform.parent = null;
        this.transform.position = TagQuery.FindObject("Jelly").transform.position;
        obj = TagQuery.FindObject("Jelly").transform;
        originalSize = mainCamera.orthographicSize;
    }
    private void FixedUpdate() {
        if(obj != null)this.transform.position = Vector2.MoveTowards(this.transform.position, obj.position, damping * Time.deltaTime);
    }
    public void OnCameraShake(float duration, float magnitude) {
        if (shakeRoutine != null) {
            return;
        }
        shakeRoutine = StartCoroutine(Shake(duration, magnitude));
    }
    private IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos3 = mainCamera.transform.localPosition;
        Vector2 originalPos = mainCamera.transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            mainCamera.transform.localPosition = new Vector3(originalPos.x + Random.insideUnitCircle.x * magnitude, originalPos.y + Random.insideUnitCircle.y * magnitude, -10f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos3;
        //DefaultPosition();
        shakeRoutine = null;
    }
    //Methods to manage the camera zoom
    //We need a void function to control in a global way the ienumerator
    public void OnCameraZoom(float amount, float speed = .25f){
        if(zoomRoutine != null)return;
        zoomRoutine = StartCoroutine(Zoom(amount, speed));
    }
    public void OnCameraZoomReturn(float speed = .25f, bool stopZoom = false){
        if(stopZoom){
            StopCoroutine(zoomRoutine);
            zoomRoutine = null;
        }
        if(zoomRoutine != null)return;
        zoomRoutine = StartCoroutine(OriginalZoom(speed));
    }

    private IEnumerator Zoom(float amount, float speed = .25f){
        float size = mainCamera.orthographicSize - amount;
        while(mainCamera.orthographicSize > size){
            mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, size, speed * Time.deltaTime);
            yield return null;
        }
        OnEndZoom();
        zoomRoutine = null;
    }
    private IEnumerator OriginalZoom(float speed){
        while(mainCamera.orthographicSize < originalSize){
            mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, originalSize, speed * Time.deltaTime);
            yield return null;
        }
        zoomRoutine = null;
    }
}
