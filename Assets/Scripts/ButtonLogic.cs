using NewgroundsUnityAPIHelper.Helper.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLogic : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    [SerializeField] private GameObject _soundPrefab;
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioClip _clickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        var soundPre = GameObject.Instantiate(_soundPrefab);

        soundPre.GetComponent<AudioSource>().clip = _clickSound;

        soundPre.GetComponent<AudioSource>().Play();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var soundPre = Instantiate(_soundPrefab);

        soundPre.GetComponent<AudioSource>().clip = _hoverSound;

        soundPre.GetComponent<AudioSource>().Play();
    }

    public void getCreditMedal()
    {
        NewgroundsAPIHelper.Instance.IsUserLoggedIn(isLoggedIn =>
        {
            if (!NewgroundsAPIHelper.Instance.IsMedalUnlocked((int) NGMedalsEnum.Developers))
                NewgroundsAPIHelper.Instance.UnlockMedal((int) NGMedalsEnum.Developers);
        });
    }
}
