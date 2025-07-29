using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.UIModule.Scripts
{
    [AddComponentMenu("Layout/Responsive Grid View", 152)]
    public class ResponsiveGridView : LayoutGroup
    {
        public enum Constraint
        {
            Fixed = 0,
            Flexible = 1
        }
        
        [SerializeField] protected Constraint _cellSizeConstraint = Constraint.Fixed;
        [SerializeField] protected Vector2 _minCellSize = new Vector2(100, 100);
        [SerializeField] protected Vector2 _maxCellSize = new Vector2(100, 100);
        [SerializeField] protected Vector2 _cellSize = new Vector2(100, 100);
        [SerializeField] protected Constraint _spacingSizeConstraint = Constraint.Fixed;
        [SerializeField] protected Vector2 _minSpacingSize = Vector2.zero;
        [SerializeField] protected Vector2 _maxSpacingSize = Vector2.zero;
        [SerializeField] protected Vector2 _spacingSize = Vector2.zero;
        [SerializeField] protected int _columnConstraintCount = 2;
        [SerializeField] protected int _minColumnConstraintCount = 2;
        [SerializeField] protected int _maxColumnConstraintCount = 2;
        [SerializeField] protected Constraint _columnConstraint = Constraint.Fixed;
        
        private int _currentColumnConstraintCount;
        private Vector2 _currentCellSize;
        private Vector2 _currentSpacingSize;
        
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            RecalculateResponsiveValues();
            float minSpace = padding.horizontal + (_currentCellSize.x + _currentSpacingSize.x) * _currentColumnConstraintCount - _currentSpacingSize.x;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 0);
        }
        
        public override void CalculateLayoutInputVertical()
        {
            RecalculateResponsiveValues();
            int minRows = Mathf.CeilToInt(rectChildren.Count / (float)_currentColumnConstraintCount - 0.001f);
            float minSpace = padding.vertical + (_currentCellSize.y + _currentSpacingSize.y) * minRows - _currentSpacingSize.y;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 1);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minSpace);
        }
        
        public override void SetLayoutHorizontal()
        {
            SetCellsAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetCellsAlongAxis(1);
        }

        private void RecalculateResponsiveValues()
        {
            float rectWidth = rectTransform.rect.size.x;
            
            if (_columnConstraint == Constraint.Fixed)
            {
                _currentColumnConstraintCount = _columnConstraintCount;
            }
            else
            {
                RecalculateColumnConstraintCount(rectWidth);
            }

            if (_cellSizeConstraint == Constraint.Fixed)
            {
                _currentCellSize = _cellSize;
            }
            else
            {
                _spacingSizeConstraint = Constraint.Fixed;
                _currentSpacingSize = _spacingSize;
                RecalculateCellSize(rectWidth);
            }

            if (_spacingSizeConstraint == Constraint.Fixed)
            {
                _currentSpacingSize = _spacingSize;
            }
            else
            {
                _cellSizeConstraint = Constraint.Fixed;
                _currentCellSize = _cellSize;
                RecalculateSpacingSize(rectWidth);
            }
        }

        private void RecalculateColumnConstraintCount(float rectWidth)
        {
            float maxCellWidth = _cellSize.x;
            float minCellWidth = _cellSize.x;
            float maxSpacingWidth = _spacingSize.x;
            float minSpacingWidth = _spacingSize.x;
            if (_cellSizeConstraint == Constraint.Flexible)
            {
                maxCellWidth = _maxCellSize.x;
                minCellWidth = _minCellSize.x;
            }

            if (_spacingSizeConstraint == Constraint.Fixed)
            {
                maxSpacingWidth = _maxSpacingSize.x;
                minSpacingWidth = _minSpacingSize.x;
            }

            int minColumnConstraintCount = Mathf.Clamp(Mathf.FloorToInt((rectWidth - padding.horizontal) / (minCellWidth + minSpacingWidth)), _minColumnConstraintCount, _maxColumnConstraintCount);
            int maxColumnConstraintCount = Mathf.Clamp(Mathf.FloorToInt((rectWidth - padding.horizontal) / (maxCellWidth + maxSpacingWidth)), _minColumnConstraintCount, _maxColumnConstraintCount);
            _currentColumnConstraintCount = Mathf.Min(minColumnConstraintCount, maxColumnConstraintCount);
        }

        private void RecalculateCellSize(float rectWidth)
        {
            float cellWidth = (rectWidth - padding.horizontal - (_currentColumnConstraintCount - 1) * _currentSpacingSize.x) /
                              _currentColumnConstraintCount;
            float relativeSize = (cellWidth - _minCellSize.x) / (_maxCellSize.x - _minCellSize.x);
            _currentCellSize = Vector2.Lerp(_minCellSize, _maxCellSize, relativeSize);
        }

        private void RecalculateSpacingSize(float rectWidth)
        {
            float spacingWidth = (rectWidth - padding.horizontal - _currentColumnConstraintCount * _currentCellSize.x) /
                                 (_currentColumnConstraintCount - 1);
            float relativeSize = (spacingWidth - _minSpacingSize.x) / (_maxSpacingSize.x - _minSpacingSize.x);
            _currentSpacingSize = Vector2.Lerp(_minSpacingSize, _maxSpacingSize, relativeSize);
        }
        
        private void SetCellsAlongAxis(int axis)
        {
            RecalculateResponsiveValues();

            var rectChildrenCount = rectChildren.Count;
            if (axis == 0)
            {
                // Only set the sizes when invoked for horizontal axis, not the positions.
            
                for (int i = 0; i < rectChildrenCount; i++)
                {
                    RectTransform rect = rectChildren[i];
            
                    m_Tracker.Add(this, rect,
                        DrivenTransformProperties.Anchors |
                        DrivenTransformProperties.AnchoredPosition |
                        DrivenTransformProperties.SizeDelta);
            
                    rect.anchorMin = Vector2.up;
                    rect.anchorMax = Vector2.up;
                    rect.sizeDelta = _currentCellSize;
                }
                return;
            }
            
            int cellCountX = _currentColumnConstraintCount;
            int cellCountY =  Mathf.CeilToInt(rectChildrenCount / (float) cellCountX - 0.001f);
            
            Vector2 requiredSpace = new Vector2(
                cellCountX * _currentCellSize.x + (cellCountX - 1) * _currentSpacingSize.x,
                cellCountY * _currentCellSize.y + (cellCountY - 1) * _currentSpacingSize.y
            );
            Vector2 startOffset = new Vector2(
                GetStartOffset(0, requiredSpace.x),
                GetStartOffset(1, requiredSpace.y)
            );

            for (int column = 0; column < cellCountX; column++)
            {
                for (int row = 0; row < cellCountY; row++)
                {
                    int childIndex = row * cellCountX + column;
                    if (childIndex < rectChildrenCount)
                    {
                        SetChildAlongAxis(rectChildren[childIndex], 0, startOffset.x + (_currentCellSize[0] + _currentSpacingSize[0]) * column, _currentCellSize[0]);
                        SetChildAlongAxis(rectChildren[childIndex], 1, startOffset.y + (_currentCellSize[1] + _currentSpacingSize[1]) * row, _currentCellSize[1]);
                    }
                }
            }
        }
    }
}