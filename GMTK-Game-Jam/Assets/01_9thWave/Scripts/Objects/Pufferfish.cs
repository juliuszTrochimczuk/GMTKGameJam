using EventsManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pufferfish : MonoBehaviour
{
    [SerializeField] private Transform _variantBig;
    [SerializeField] private Transform _variantSmall;
    [SerializeField] private float _durationInBig;
    [SerializeField] private float _activationDistance;

    private Transform MainVariant { get; set; }

    private Transform _playerTransform;
    private EventsCaller _caller;
    private Coroutine _inBigForm;

    private void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _caller = GameObject.FindWithTag("Manager").GetComponent<EventsCaller>();
        _variantBig.gameObject.SetActive(false);
        MainVariant = _variantSmall;
    }

    private void Start()
    {
        _caller.GetEvent(EventsManagers.EventType.Wave).AddListenerToGameEvent(GoBig);
    }

    void Update()
    {
        if (!_variantSmall.gameObject.activeInHierarchy)
            return;

        if (Vector2.Distance(_playerTransform.position, _variantSmall.position) > _activationDistance)
            return;

        GoBig();
    }

    private void GoBig()
    {
        if (_inBigForm != null)
            return;

        _inBigForm = StartCoroutine(BigFormHandler());
    }

    private IEnumerator BigFormHandler()
    {
        TransitionVariants(_variantBig);
        yield return new WaitForSeconds(_durationInBig);
        TransitionVariants(_variantSmall);
        _inBigForm = null;
    }

    private void TransitionVariants(Transform designedVariant)
    {
        MainVariant.gameObject.SetActive(false);
        designedVariant.position = MainVariant.position;
        designedVariant.gameObject.SetActive(true);
        MainVariant = designedVariant;
    }
}
