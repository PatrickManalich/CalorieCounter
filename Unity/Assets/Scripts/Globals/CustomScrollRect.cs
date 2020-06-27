using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    public class CustomScrollRect : ScrollRect
    {
        private const float MinScrollbarSize = 0.3f;

        public override void Rebuild(CanvasUpdate executing)
        {
            base.Rebuild(executing);
            if (horizontalScrollbar)
            {
                horizontalScrollbar.size = Mathf.Max(MinScrollbarSize, horizontalScrollbar.size);
            }
            if (verticalScrollbar)
            {
                verticalScrollbar.size = Mathf.Max(MinScrollbarSize, verticalScrollbar.size);
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (horizontalScrollbar)
            {
                horizontalScrollbar.size = Mathf.Max(MinScrollbarSize, horizontalScrollbar.size);
            }
            if (verticalScrollbar)
            {
                verticalScrollbar.size = Mathf.Max(MinScrollbarSize, verticalScrollbar.size);
            }
        }
    }
}