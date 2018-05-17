﻿namespace Projectile.ObjectPooling.Bullets
{
    using UnityEngine;

    public abstract class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private int damage = 1;
        [SerializeField]
        private float lifeTime = 1;
        [SerializeField]
        private BulletPool.BulletTypes type = BulletPool.BulletTypes.Player;

        public BulletPool.BulletTypes Type { get { return this.type; } }

        private int referenceIndex = 0;
        private float currentLifeTime = 0;

        public bool hasExplosion;

        private void Update()
        {
            LocalUpdate();
            if ((this.currentLifeTime -= Time.deltaTime) <= 0)
            {
                if (!hasExplosion)
                {
                    ReturnBullet();
                }
                
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collider)
        {
            if (!hasExplosion)
            {
                this.currentLifeTime = 0;
                /*
                if (collider.gameObject.tag != "Bullet")
                {
                    this.currentLifeTime = 0;
                }
                */
                    
            }
           
        }

        public IPoolable SpawnCopy(int referenceIndex)
        {
            Bullet bullet = Instantiate<Bullet>(this);
            bullet.referenceIndex = referenceIndex;
            return bullet;
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public int GetReferenceIndex()
        {
            return this.referenceIndex;
        }

        public void Initialize()
        {
            LocalInitialize();
        }

        public void ReInitialize()
        {
            LocalReInitialize();
            this.currentLifeTime = this.lifeTime;
            this.gameObject.SetActive(true);
        }

        public void Deallocate()
        {
            LocalDeallocate();
            this.gameObject.SetActive(false);
        }

        public void Delete()
        {
            LocalDelete();
            Destroy(this.gameObject);
        }

        public int GetDamage()
        {
            return this.damage;
        }

        protected void ReturnBullet()
        {
            BulletPool.Instance.ReturnBullet(this.type, this.gameObject);
        }

        /// <summary> Local Update for subclasses. </summary>
        protected abstract void LocalUpdate();
        /// <summary> Local Initialize for subclasses. </summary>
        protected abstract void LocalInitialize();
        /// <summary> Local ReInitialize for subclasses. </summary>
        protected abstract void LocalReInitialize();
        /// <summary> Local Deallocate for subclasses. </summary>
        protected abstract void LocalDeallocate();
        /// <summary> Local Delete for subclasses. </summary>
        protected abstract void LocalDelete();
    }
}