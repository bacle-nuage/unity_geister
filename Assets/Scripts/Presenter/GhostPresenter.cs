using System;
using Models;
using UnityEngine;

namespace Presenter
{
    public class GhostPresenter : MonoBehaviour
    {
        private GhostModel _ghostModel;

        public Action OnMouseDragListener;
        public Action OnMouseDownListener;
        public Action OnMouseUpListener;

        public GhostModel GhostModel
        {
            get => _ghostModel;
        }

        void Awake()
        {
            _ghostModel = new GhostModel(this);
        }

        void Start()
        {
            
        }

        void OnMouseDrag()
        {
            if (OnMouseDragListener != null)
            {
                OnMouseDragListener();
            }
            // _ghostModel.MoveGhost();
        }
        
        private void OnMouseDown()
        {
            if (OnMouseDownListener != null)
            {
                OnMouseDownListener();
            }
            // _ghostModel.OnMouseDownLocalPosition();
        }

        private void OnMouseUp()
        {
            if (OnMouseUpListener != null)
            {
                OnMouseUpListener();
            }
            // _ghostModel.JustFitGhost();
            // _ghostModel.DropNotArea();
        }
    }
}