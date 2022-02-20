using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creation.HologramAdvertisement
{
    public class HologramAdvertisementLineupRoot : MonoBehaviour
    {
        [SerializeField] private HologramAdvertisement baseAdvertisement;
        
        private float adjustSize;
        private SizeAdjustType sizeAdjustType;
        private AnchorPoint anchorPoint;
        private ShowAnimationType showAnimationType;
        private HideAnimationType hideAnimationType;
        private EaseType showAnimationEaseType;
        private EaseType hideAnimationEaseType;
        private bool isPlane;
        private LineupDirection lineupDirection;
        private int lineupTextureCount;
        private float lineupTexturePadding;
        private Color hologramColor;
        private float hologramNoiseAmount;
        private float hologramNoiseStrength;

        private GameObject advertisementComponentsParent;
        private Vector3 advertisementComponentsParentSize;
        private List<HologramAdvertisement> advertisementComponents = new List<HologramAdvertisement>();

        public void Setup(
            List<Advertisement> initialAdvertisements,
            float adjustSize,
            SizeAdjustType sizeAdjustType,
            AnchorPoint anchorPoint,
            ShowAnimationType showAnimationType,
            HideAnimationType hideAnimationType,
            EaseType showAnimationEaseType,
            EaseType hideAnimationEaseType,
            bool isPlane,
            int lineupTextureCount,
            float lineupTexturePadding,
            LineupDirection lineupDirection,
            Color hologramColor,
            float hologramNoiseAmount,
            float hologramNoiseStrength)
        {
            this.adjustSize = adjustSize;
            this.sizeAdjustType = sizeAdjustType;
            this.anchorPoint = anchorPoint;
            this.showAnimationType = showAnimationType;
            this.hideAnimationType = hideAnimationType;
            this.showAnimationEaseType = showAnimationEaseType;
            this.hideAnimationEaseType = hideAnimationEaseType;
            this.isPlane = isPlane;
            this.lineupTextureCount = lineupTextureCount;
            this.lineupTexturePadding = lineupTexturePadding;
            this.lineupDirection = lineupDirection;
            this.hologramColor = hologramColor;
            this.hologramNoiseAmount = hologramNoiseAmount;
            this.hologramNoiseStrength = hologramNoiseStrength;

            if (advertisementComponentsParent == null)
            {
                advertisementComponentsParent = new GameObject("advertisementComponentsParent");
                advertisementComponentsParent.transform.SetParent(this.transform);
            }

            initializeAdvertisementComponents();
            updateAdvertisementComponentsTexture(initialAdvertisements);
            adjustAnchorPoint();
        }

        public void PlayHideAnimation(float time, Action onFinished)
        {
            StartCoroutine(playHideAnimationCoroutine(time, onFinished));
        }

        public void PlayShowAnimation(float time, Action onFinished)
        {
            StartCoroutine(playShowAnimationCoroutine(time, onFinished));
        }

        public void UpdateNextAdvertisements(List<Advertisement> nextAdvertisements)
        {
            updateAdvertisementComponentsTexture(nextAdvertisements);
            adjustAnchorPoint();
        }

        private void initializeAdvertisementComponents()
        {
            advertisementComponents.ForEach(Destroy);
            advertisementComponents.Clear();

            for (int i = 0; i < lineupTextureCount; i++)
            {
                var newAdvertisement = GameObject.Instantiate<HologramAdvertisement>(baseAdvertisement);
                newAdvertisement.transform.SetParent(advertisementComponentsParent.transform);
                newAdvertisement.gameObject.SetActive(true);
                advertisementComponents.Add(newAdvertisement);
            }
        }

        private void updateAdvertisementComponentsTexture(List<Advertisement> advertisements)
        {
            float offset = 0.0f;
            Vector3 maxSize = Vector3.zero;
            for (int i = 0; i < advertisements.Count; i++)
            {
                var size = advertisementComponents[i].Setup(advertisements[i],
                    adjustSize,
                    sizeAdjustType,
                    isPlane,
                    hologramColor,
                    hologramNoiseAmount,
                    hologramNoiseStrength);
                maxSize.x = Mathf.Max(maxSize.x, size.x);
                maxSize.y = Mathf.Max(maxSize.y, size.y);
                maxSize.z = Mathf.Max(maxSize.z, size.z);

                Vector3 advertisementPosition = Vector3.zero;
                if (lineupDirection == LineupDirection.Horizontal)
                {
                    advertisementPosition.x = offset + size.x / 2.0f;
                    offset += size.x;
                }
                else
                {
                    advertisementPosition.y = offset + size.y / 2.0f;
                    offset += size.y;
                }

                if (i < advertisementComponents.Count - 1)
                {
                    offset += lineupTexturePadding;
                }

                advertisementComponents[i].transform.localPosition = advertisementPosition;
            }

            advertisementComponents.ForEach(advertisement =>
            {
                Vector3 position = advertisement.transform.localPosition;
                if (lineupDirection == LineupDirection.Horizontal)
                {
                    position.x -= offset / 2.0f;
                }
                else
                {
                    position.y -= offset / 2.0f;
                }

                advertisement.transform.localPosition = position;
            });

            advertisementComponentsParentSize = Vector2.one;
            if (lineupDirection == LineupDirection.Horizontal)
            {
                advertisementComponentsParentSize.x = offset;
                advertisementComponentsParentSize.y = maxSize.y;
                advertisementComponentsParentSize.z = offset;
            }
            else
            {
                advertisementComponentsParentSize.x = maxSize.x;
                advertisementComponentsParentSize.y = offset;
                advertisementComponentsParentSize.z = maxSize.z;
            }
        }

        private void adjustAnchorPoint()
        {
            Vector3 localPosition = Vector3.zero;
            if (anchorPoint == AnchorPoint.Left ||
                anchorPoint == AnchorPoint.TopLeft ||
                anchorPoint == AnchorPoint.BottomLeft)
            {
                localPosition.x = advertisementComponentsParentSize.x / 2.0f;
            }
            else if (anchorPoint == AnchorPoint.Right ||
                     anchorPoint == AnchorPoint.TopRight ||
                     anchorPoint == AnchorPoint.BottomRight)
            {
                localPosition.x = -1.0f * advertisementComponentsParentSize.x / 2.0f;
            }

            if (anchorPoint == AnchorPoint.Top ||
                anchorPoint == AnchorPoint.TopLeft ||
                anchorPoint == AnchorPoint.TopRight)
            {
                localPosition.y = -1.0f * advertisementComponentsParentSize.y / 2.0f;
            }
            else if (anchorPoint == AnchorPoint.Bottom ||
                     anchorPoint == AnchorPoint.BottomLeft ||
                     anchorPoint == AnchorPoint.BottomRight)
            {
                localPosition.y = advertisementComponentsParentSize.y / 2.0f;
            }

            advertisementComponentsParent.transform.localPosition = localPosition;
        }

        private IEnumerator playHideAnimationCoroutine(float time, Action onFinished)
        {
            float counter = 0.0f;
            while (counter < time)
            {
                counter = Mathf.Min(counter + Time.deltaTime, time);
                float value = Mathf.Max(EaseUtil.Ease(hideAnimationEaseType, counter, time, 1.0f, 0.0f), 0.0f);
                switch (hideAnimationType)
                {
                    case HideAnimationType.FadeOut:
                        advertisementComponents.ForEach(advertisement => advertisement.UpdateAlpha(value));
                        break;
                    case HideAnimationType.Shrink:
                        advertisementComponents.ForEach(advertisement => advertisement.UpdateSize(Vector3.one * value));
                        break;
                    case HideAnimationType.ShrinkHorizontal:
                        advertisementComponents.ForEach(advertisement =>
                            advertisement.UpdateSize(new Vector3(value, 1.0f, value)));
                        break;
                    case HideAnimationType.ShrinkVertical:
                        advertisementComponents.ForEach(
                            advertisement => advertisement.UpdateSize(new Vector3(1.0f, value, 1.0f)));
                        break;
                }

                yield return null;
            }

            onFinished?.Invoke();
        }

        private IEnumerator playShowAnimationCoroutine(float time, Action onFinished)
        {
            float counter = 0.0f;
            while (counter < time)
            {
                counter = Mathf.Min(counter + Time.deltaTime, time);
                float value = Mathf.Max(EaseUtil.Ease(showAnimationEaseType, counter, time, 0.0f, 1.0f), 0.0f);
                switch (showAnimationType)
                {
                    case ShowAnimationType.FadeIn:
                        advertisementComponents.ForEach(advertisement => advertisement.UpdateAlpha(value));
                        break;
                    case ShowAnimationType.Expansion:
                        advertisementComponents.ForEach(advertisement => advertisement.UpdateSize(Vector3.one * value));
                        break;
                    case ShowAnimationType.ExpansionHorizontal:
                        advertisementComponents.ForEach(advertisement =>
                            advertisement.UpdateSize(new Vector3(value, 1.0f, value)));
                        break;
                    case ShowAnimationType.ExpansionVertical:
                        advertisementComponents.ForEach(
                            advertisement => advertisement.UpdateSize(new Vector3(1.0f, value, 1.0f)));
                        break;
                }

                yield return null;
            }

            advertisementComponents.ForEach(advertisement => advertisement.UpdateAlpha(1.0f));
            onFinished?.Invoke();
        }
    }
}