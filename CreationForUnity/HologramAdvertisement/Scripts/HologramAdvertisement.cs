using System;
using System.Collections;
using UnityEngine;

namespace Creation.HologramAdvertisement
{
    public class HologramAdvertisement : MonoBehaviour
    {
        [SerializeField] private GameObject cubeObject;
        
        [SerializeField] private GameObject planeObject;

        [SerializeField] private AudioSource audioSource;
        
        private GameObject displayObject;
        private Renderer renderer;

        private float defaultAlpha = 1.0f;
        private Vector3 adjustedObjectSize = Vector3.one;

        private void Awake()
        {
            defaultAlpha = planeObject.GetComponent<Renderer>().material.GetFloat("_BaseAlpha");
        }

        public Vector3 Setup(
            Advertisement advertisement,
            float adjustSize,
            SizeAdjustType sizeAdjustType,
            bool isPlane,
            Color hologramColor,
            float hologramNoiseAmount,
            float hologramNoiseStrength
            )
        {
            setupModel(isPlane);
            adjustObjectSize(advertisement.Texture, adjustSize, sizeAdjustType);
            updateMaterialSetting(advertisement.Texture,hologramColor,hologramNoiseAmount,hologramNoiseStrength);
            audioSource.clip = advertisement.AudioClip;
            return adjustedObjectSize;
        }

        public void UpdateAlpha(float alpha)
        {
            renderer.material.SetFloat("_BaseAlpha", alpha * defaultAlpha);
        }

        public void UpdateSize(Vector3 rate)
        {
            Vector3 objectSize = adjustedObjectSize;
            objectSize.x *= rate.x;
            objectSize.y *= rate.y;
            objectSize.z *= rate.z;
            displayObject.transform.localScale = objectSize;
        }

        public void PlayAudio(Action onFinished)
        {
            if (audioSource.clip == null)
            {
                onFinished?.Invoke();
                return;
            }

            StartCoroutine(playAudioCoroutine(onFinished));
            audioSource.Play();
        }

        private IEnumerator playAudioCoroutine(Action onFinished)
        {
            while (audioSource.isPlaying)
            {
                yield return null;
            }
            onFinished?.Invoke();
        }
        
        private void setupModel(bool isPlane)
        {
            cubeObject.gameObject.SetActive(!isPlane);
            planeObject.gameObject.SetActive(isPlane);
            displayObject = isPlane ? planeObject : cubeObject;
            renderer = displayObject.GetComponent<Renderer>();
        }

        private void adjustObjectSize(Texture textureAsset, float adjustSize, SizeAdjustType sizeAdjustType)
        {
            Vector3 objectSize = Vector3.one * adjustSize;
            float heightPerWidth = (float) textureAsset.height / textureAsset.width;
            switch (sizeAdjustType)
            {
                case SizeAdjustType.ShortSide:
                    if (heightPerWidth > 1.0f)
                    {
                        objectSize.y = objectSize.x * heightPerWidth;
                    }
                    else
                    {
                        objectSize.x = objectSize.y / heightPerWidth;
                    }
                    break;
                case SizeAdjustType.LongSide:
                    if (heightPerWidth > 1.0f)
                    {
                        objectSize.x = objectSize.y / heightPerWidth;
                    }
                    else
                    {
                        objectSize.y = objectSize.x * heightPerWidth;
                    }
                    break;
                case SizeAdjustType.Width:
                    objectSize.y = objectSize.x * heightPerWidth;
                    break;
                case SizeAdjustType.Height:
                    objectSize.x = objectSize.y / heightPerWidth;
                    break;
            }

            displayObject.transform.localScale = objectSize;
            adjustedObjectSize = objectSize;
        }

        private void updateMaterialSetting(
            Texture texture,
            Color hologramColor,
            float hologramNoiseAmount,
            float hologramNoiseStrength)
        {
            var material = renderer.material;
            material.SetTexture("_DisplayTexture", texture);
            material.SetColor("_HologramColor", hologramColor);
            material.SetFloat("_HologramNoiseAmount", hologramNoiseAmount);
            material.SetFloat("_HologramNoiseStrength", hologramNoiseStrength);
        }
    }
}