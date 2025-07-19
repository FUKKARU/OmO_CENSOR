using UnityEngine;
using System;
using System.Collections.Generic;
using Scripts.Utilities; // ← シングルトン基盤

namespace SpeedControl
{
    public class Speed
    {
        private float _speed = 1f;
        public float CurrentSpeed => _speed;
        public event Action<float> OnValueChange;

        public void SetSpeed(float speed)
        {
            _speed = speed;
            OnValueChange?.Invoke(_speed);
        }
        public void Dispose()
        {
            OnValueChange = null;
        }
    }

    public class BattleSpeedController : ASingletonMonoBehaviour<BattleSpeedController>
    {
        private readonly List<Speed> _subscribers = new List<Speed>();
        private float _battleSpeed = 1f;
        private float _skillControlledSpeed = 1f;
        private bool _isPaused = false;

        public float CurrentBattleSpeed => _battleSpeed;
        public float CurrentSkillSpeed => _skillControlledSpeed;
        public bool IsPaused => _isPaused;

        public void Subscribe(Speed speed)
        {
            if (!_subscribers.Contains(speed))
                _subscribers.Add(speed);
        }
        public void Unsubscribe(Speed speed)
        {
            if (_subscribers.Contains(speed))
                _subscribers.Remove(speed);
        }

        public void SetDoubleSpeed(bool isDouble)
        {
            _battleSpeed = isDouble ? 2f : 1f;
            NotifyAll();
        }
        public void SetPause(bool isPause)
        {
            _isPaused = isPause;
            NotifyAll();
        }
        public void SetSkillControlledSpeed(float speed)
        {
            _skillControlledSpeed = speed;
            NotifyAll();
        }
        private void NotifyAll()
        {
            foreach (var speed in _subscribers.ToArray()) // イテレーション中のリスト操作対策
            {
                float calculated = CalculateSpeed(speed);
                speed.SetSpeed(calculated);
            }
        }
        private float CalculateSpeed(Speed instance)
        {
            if (_isPaused)
                return 0f;
            float result = _battleSpeed;
            if (instance is IFilterableSpeed filterable && !filterable.UseSkillControlledSpeed)
                return result;
            result *= _skillControlledSpeed;
            return result;
        }
    }
    
    /// <summary>
    /// スキル制御フィルターを持つSpeed拡張
    /// </summary>
    public interface IFilterableSpeed
    {
        bool UseSkillControlledSpeed { get; set; }
    }

    /// <summary>
    /// キャラクターの移動やアニメーションにSpeedを適用する例
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterSpeedController : MonoBehaviour, IFilterableSpeed
    {
        private Speed _speed = new Speed();
        private Animator _animator;

        public bool UseSkillControlledSpeed { get; set; } = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _speed.OnValueChange += UpdateAnimatorSpeed;
            BattleSpeedController.Instance.Subscribe(_speed);
        }

        private void OnDestroy()
        {
            BattleSpeedController.Instance.Unsubscribe(_speed);
            _speed.OnValueChange -= UpdateAnimatorSpeed;
        }

        private void UpdateAnimatorSpeed(float newSpeed)
        {
            _animator.speed = newSpeed;
        }
    }
}