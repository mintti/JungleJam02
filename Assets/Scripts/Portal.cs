using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform warpTransform;
    
    [TextArea]
    public string infoText;
    public int time;
    
    private GameObject _player;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            StartCoroutine(nameof(WarpTimer));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(nameof(WarpTimer));
            UIManager.Instance.ShowQuestion();
        }
    }

    IEnumerator WarpTimer()
    {
        if (time == 0)
        {
            Warp();
        }
        else
        {
            int count = time;
            UIManager.Instance.ShowQuestion($"{count}초 후 {infoText}");
            while(count > 0)
            {
                yield return new WaitForSeconds(1f);
                count--;
                UIManager.Instance.ShowQuestion($"{count}초 후 {infoText}");
            }
            Warp();
        }
    }

    private void Warp()
    {
        // Character Controller의 Move 메서드에 새로운 위치 전달
        //_player.GetComponent<PlayerController>()._controller.Move(warpTransform.position - transform.position);
        _player.GetComponent<PlayerController>().warpTimer = .5f;
        _player.gameObject.transform.position = warpTransform.position;
        // _player.GetComponent<PlayerController>().gameObject.transform.position = warpTransform.position;
        UIManager.Instance.ShowQuestion();
    }
}
