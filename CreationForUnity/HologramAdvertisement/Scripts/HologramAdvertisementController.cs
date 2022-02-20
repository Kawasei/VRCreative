using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Creation.HologramAdvertisement
{
    public class HologramAdvertisementController : MonoBehaviour
    {
        [SerializeField] private List<Advertisement> advertisements = new List<Advertisement>();

        [SerializeField] private float displayTime = 5.0f;

        [SerializeField] private float switchingTime = 1.0f;

        [SerializeField] private ShowAnimationType showAnimationType = ShowAnimationType.FadeIn;

        [SerializeField] private HideAnimationType hideAnimationType = HideAnimationType.FadeOut;

        [SerializeField] private EaseType showAnimationEaseType = EaseType.Linear;

        [SerializeField] private EaseType hideAnimationEaseType = EaseType.Linear;

        [SerializeField] private bool isPlane = true;

        [SerializeField] private AdvertisementDirection advertisementDirection = AdvertisementDirection.Fixed;

        [SerializeField] private float rotateAnglePerSecond = 5.0f;

        [SerializeField] private float adjustSize = 2.5f;

        [SerializeField] private SizeAdjustType sizeAdjustType = SizeAdjustType.ShortSide;

        [SerializeField] private AnchorPoint anchorPoint = AnchorPoint.Center;

        [SerializeField] private LineupDirection lineupDirection = LineupDirection.Vertical;

        [SerializeField] private int lineupCount = 1;

        [SerializeField] private float lineupPadding = 0.0f;
        
        [SerializeField] private Color hologramColor = Color.white;

        [SerializeField] private float hologramNoiseAmount = 96.0f;
        
        [SerializeField] private float hologramNoiseStrength = 0.5f;

        private HologramAdvertisementLineupRoot advertisementLineupRoot;
        private Camera mainCamera;
        private float counter = 0.0f;
        private bool isSwitching = false;
        private int nextDisplayTextureIndex = 0;
        private float useDisplayTime = 0.0f;

        public void Setup(
            List<Advertisement> advertisements,
            Color hologramColor,
            float displayTime = 5.0f,
            float switchingTime = 1.0f,
            ShowAnimationType showAnimationType = ShowAnimationType.FadeIn,
            HideAnimationType hideAnimationType = HideAnimationType.FadeOut,
            EaseType showAnimationEaseType = EaseType.Linear,
            EaseType hideAnimationEaseType = EaseType.Linear,
            bool isPlane = true,
            AdvertisementDirection advertisementDirection = AdvertisementDirection.Fixed,
            float rotateAnglePerSecond = 5.0f,
            float adjustSize = 2.5f,
            SizeAdjustType sizeAdjustType = SizeAdjustType.ShortSide,
            AnchorPoint anchorPoint = AnchorPoint.Center,
            int lineupCount = 1,
            float lineupPadding = 0.0f,
            LineupDirection lineupDirection = LineupDirection.Vertical,
            float hologramNoiseAmount = 96.0f,
            float hologramNoiseStrength = 0.5f)
        {
            this.advertisements = advertisements;
            this.displayTime = displayTime;
            this.switchingTime = switchingTime;
            this.showAnimationType = showAnimationType;
            this.hideAnimationType = hideAnimationType;
            this.showAnimationEaseType = showAnimationEaseType;
            this.hideAnimationEaseType = hideAnimationEaseType;
            this.isPlane = isPlane;
            this.advertisementDirection = advertisementDirection;
            this.rotateAnglePerSecond = rotateAnglePerSecond;
            this.adjustSize = adjustSize;
            this.sizeAdjustType = sizeAdjustType;
            this.anchorPoint = anchorPoint;
            this.lineupCount = lineupCount;
            this.lineupPadding = lineupPadding;
            this.lineupDirection = lineupDirection;
            this.hologramColor = hologramColor;
            this.hologramNoiseAmount = hologramNoiseAmount;
            this.hologramNoiseStrength = hologramNoiseStrength;
            
            advertisements.RemoveAll(item => item.Texture == null);
            updateAdvertisementView();
        }

        private void Awake()
        {
            advertisements.RemoveAll(item => item.Texture == null);
            updateAdvertisementView();
        }

        private void Update()
        {
            updateAdvertisementsTexture();
            updateAdvertisementDirection();
        }

        private void updateAdvertisementsTexture()
        {
            if (advertisements.Count <= lineupCount)
            {
                return;
            }

            if (isSwitching)
            {
                return;
            }

            counter += Time.deltaTime;
            if (counter >= useDisplayTime)
            {
                isSwitching = true;
                updateAdvertisementView();
            }
        }

        private void updateAdvertisementView()
        {
            var nextAdvertisements = getShouldShowAdvertisements();
            var maxAudioLength = 0.0f;
            if ((nextAdvertisements?.Count ?? 0) > 0)
            {
                maxAudioLength = nextAdvertisements
                    .Select(advertisement =>
                    {
                        if (advertisement?.AudioClip == null)
                        {
                            return 0.0f;
                        }

                        return advertisement.AudioClip.length;
                    })
                    .Max();
            }

            useDisplayTime = Mathf.Max(maxAudioLength, displayTime);
            setupAdvertisementList(nextAdvertisements);
            switchAdvertisement(nextAdvertisements,() =>
            {
                counter = 0.0f;
                isSwitching = false;
            });
        }

        private void updateAdvertisementDirection()
        {
            if (advertisementDirection == AdvertisementDirection.Fixed)
            {
                return;
            }

            switch (advertisementDirection)
            {
                case AdvertisementDirection.Rotate:
                    this.transform.Rotate(new Vector3(
                        0.0f,
                        Time.deltaTime * rotateAnglePerSecond,
                        0.0f));
                    break;
                case AdvertisementDirection.MainCamera:
                    if (mainCamera == null)
                    {
                        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
                    }

                    var direction = mainCamera.transform.position - this.transform.position;
                    direction.y = 0;
                    this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                    break;
                case AdvertisementDirection.Fixed:
                default:
                    break;
                    ;
            }
        }

        private void setupAdvertisementList(List<Advertisement> initialAdvertisements)
        {
            if (advertisementLineupRoot != null)
            {
                return;
            }
            advertisementLineupRoot = GetComponentInChildren<HologramAdvertisementLineupRoot>();
            advertisementLineupRoot.Setup(
                initialAdvertisements,
                adjustSize,
                sizeAdjustType,
                anchorPoint,
                showAnimationType,
                hideAnimationType,
                showAnimationEaseType,
                hideAnimationEaseType,
                isPlane,
                Mathf.Min(lineupCount, advertisements.Count),
                lineupPadding,
                lineupDirection,
                hologramColor,
                hologramNoiseAmount,
                hologramNoiseStrength);
        }

        private void switchAdvertisement(List<Advertisement> nextAdvertisements,Action onFinished)
        {
            float time = switchingTime / 2.0f;
            advertisementLineupRoot.PlayHideAnimation(time, () =>
            {
                advertisementLineupRoot.UpdateNextAdvertisements(nextAdvertisements);
                advertisementLineupRoot.PlayShowAnimation(time, onFinished);
            });
        }

        private List<Advertisement> getShouldShowAdvertisements()
        {
            List<Advertisement> result = new List<Advertisement>();
            if ((advertisements?.Count ?? 0) == 0)
            {
                return result;
            }
            for (int i = 0; i < lineupCount; i++)
            {
                result.Add(advertisements[nextDisplayTextureIndex]);
                nextDisplayTextureIndex = (nextDisplayTextureIndex + 1) % advertisements.Count;
            }

            return result;
        }
    }
}
