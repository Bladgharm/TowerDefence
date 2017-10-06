using UnityEngine;

namespace Assets.Code
{
    public class ResourceItem : MonoBehaviour
    {
        [SerializeField]
        private ResourceType _itemResource;

        private Animation _animation;

        public ResourceType ItemResource
        {
            get { return _itemResource; }
        }

        private void Start()
        {
            _animation = GetComponent<Animation>();
        }

        public void HitItem()
        {
            if (_animation != null)
            {
                _animation.Play(PlayMode.StopAll);
            }
        }
    }

    public enum ResourceType
    {
        Tree,
        Iron,
        Stone
    }
}