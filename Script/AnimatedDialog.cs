using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �_�C�A���O�̃A�j���[�V����
/// </summary>
public class AnimatedDialog : MonoBehaviour
{
    // �A�j���[�^�[
    [SerializeField] private Animator _animator;

    // �A�j���[�^�[�R���g���[���[�̃��C���[(�ʏ��0)
    [SerializeField] private int _layer;

    // IsOpen�t���O(�A�j���[�^�[�R���g���[���[���Œ�`�����t���O)
    private static readonly int paramIsOpen = Animator.StringToHash("IsOpen");

    // �_�C�A���O�͊J���Ă��邩�ǂ���
    public bool isOpen => gameObject.activeSelf;

    // �A�j���[�V���������ǂ���
    public bool isTransition { get; private set; }

    // �_�C�A���O���J��
    public void Open()
    {
        // �s������h�~
        if (isOpen || isTransition) return;

        // �p�l�����̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(true);

        // IsOpen�t���O���Z�b�g
        _animator.SetBool(paramIsOpen, true);

        // �A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
    }

    // �_�C�A���O�����
    public void Close()
    {
        // �s������h�~
        if (!isOpen || isTransition) return;

        // IsOpen�t���O���N���A
        _animator.SetBool(paramIsOpen, false);

        // �A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    // �J�A�j���[�V�����̑ҋ@�R���[�`��
    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        isTransition = true;

        yield return new WaitUntil(() =>
        {
            // �X�e�[�g���ω����A�A�j���[�V�������I������܂Ń��[�v
            var state = _animator.GetCurrentAnimatorStateInfo(_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        isTransition = false;

        onCompleted?.Invoke();
    }
}
