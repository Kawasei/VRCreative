#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Creation.HologramAdvertisement
{
    [CustomEditor(typeof(HologramAdvertisementController))]
    public class HologramAdvertisementControllerEditor : Editor
    {
        private GUIStyle boxStyle;
        private GUIStyle titleStyle;
        private GUIStyle innerTitleStyle;
        private ReorderableList reorderableList;

        public override void OnInspectorGUI()
        {
            initializeStyle();
            serializedObject.Update();
            showAdvertisementSetting();
            EditorGUILayout.Space();
            showModelSetting();
            EditorGUILayout.Space();
            showLineupSetting();
            EditorGUILayout.Space();
            showAnimationSetting();
            EditorGUILayout.Space();
            showHologramSetting();
            serializedObject.ApplyModifiedProperties();
        }

        private void initializeStyle()
        {
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.stretchWidth = true;
            
            titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.normal.background = makeTex(2, 2, Color.black);
            
            innerTitleStyle = new GUIStyle(EditorStyles.boldLabel);
            innerTitleStyle.normal.background = makeTex(2, 2, new Color(0.1f, 0.1f, 0.1f ,1.0f));
        }


        private void showAdvertisementSetting()
        {
            EditorGUILayout.BeginVertical(boxStyle);
            EditorGUILayout.LabelField("Advertisement Setting", titleStyle);
            EditorGUI.indentLevel++;
            // テクスチャ設定
            var advertisementsProperty = serializedObject.FindProperty("advertisements");
            if (reorderableList == null)
            {
                reorderableList = new ReorderableList(serializedObject, advertisementsProperty);
                reorderableList.draggable = true;
                reorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Advertisement List");
                reorderableList.elementHeightCallback = index => EditorGUI.GetPropertyHeight(advertisementsProperty.GetArrayElementAtIndex(index));
                reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var elementProperty = advertisementsProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, elementProperty, new GUIContent("Advertisement" + index));
                };
                }

            reorderableList.DoLayoutList();

            EditorGUI.indentLevel--;　
            EditorGUILayout.EndVertical();
        }

        private void showModelSetting()
        {
            EditorGUILayout.BeginVertical(boxStyle);
            EditorGUILayout.LabelField("Model Setting", titleStyle);
            EditorGUI.indentLevel++;

            // 使用するmodelが2Dかの設定
            var isPlaneProperty = serializedObject.FindProperty("isPlane");
            isPlaneProperty.boolValue =
                EditorGUILayout.Toggle("2D Model", isPlaneProperty.boolValue);
            var adjustSizeProperty = serializedObject.FindProperty("adjustSize");
            // アンカーポイントの設定
            var anchorPointProperty = serializedObject.FindProperty("anchorPoint");
            anchorPointProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Anchor Point",
                (AnchorPoint) anchorPointProperty.enumValueIndex));
            EditorGUILayout.BeginVertical(boxStyle);
            {
                EditorGUILayout.LabelField("Size Setting", innerTitleStyle);
                EditorGUI.indentLevel++;
                // サイズ設定
                adjustSizeProperty.floatValue =
                    EditorGUILayout.FloatField("Size", adjustSizeProperty.floatValue);
                // サイズ調整のタイプ
                var sizeAdjustTypeProperty = serializedObject.FindProperty("sizeAdjustType");
                sizeAdjustTypeProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Adjust Type",
                    (SizeAdjustType) sizeAdjustTypeProperty.enumValueIndex));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(boxStyle);
            {
                EditorGUILayout.LabelField("Direction Setting", innerTitleStyle);
                EditorGUI.indentLevel++;
                var advertisementDirectionProperty = serializedObject.FindProperty("advertisementDirection");
                advertisementDirectionProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup(
                    "Direction Type",
                    (AdvertisementDirection) advertisementDirectionProperty.enumValueIndex));
                // 回転角度の設定
                EditorGUI.BeginDisabledGroup((AdvertisementDirection) advertisementDirectionProperty.enumValueIndex !=
                                             AdvertisementDirection.Rotate);
                var rotateAnglePerSecondProperty = serializedObject.FindProperty("rotateAnglePerSecond");
                rotateAnglePerSecondProperty.floatValue =
                    EditorGUILayout.FloatField("Rotate Euler Angle Per Speed", rotateAnglePerSecondProperty.floatValue);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        private void showLineupSetting()
        {
            var advertisementProperty = serializedObject.FindProperty("advertisements");
            EditorGUILayout.BeginVertical(boxStyle);
            {
                EditorGUI.BeginDisabledGroup(advertisementProperty.arraySize <= 1);
                EditorGUILayout.LabelField("Lineup Setting", titleStyle);
                EditorGUI.indentLevel++;

                // 並べるモデルの数の設定
                var lineupCountProperty = serializedObject.FindProperty("lineupCount");
                lineupCountProperty.intValue = Mathf.Max(1,
                    EditorGUILayout.IntField("Lineup Count", lineupCountProperty.intValue));
                {
                    EditorGUI.BeginDisabledGroup(lineupCountProperty.intValue <= 1);

                    // 並べる方向についての設定
                    var lineupDirectionProperty = serializedObject.FindProperty("lineupDirection");
                    lineupDirectionProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Direction",
                        (LineupDirection) lineupDirectionProperty.enumValueIndex));
                    //　並べる際の間のサイズの設定
                    var lineupPaddingProperty = serializedObject.FindProperty("lineupPadding");
                    lineupPaddingProperty.floatValue =
                        EditorGUILayout.FloatField("Padding Size", lineupPaddingProperty.floatValue);

                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndVertical();
        }

        private void showAnimationSetting()
        {
            var advertisementsCount = serializedObject.FindProperty("advertisements").arraySize;
            var lineUpCount = serializedObject.FindProperty("lineupCount").intValue;
            EditorGUILayout.BeginVertical(boxStyle);
            {
                EditorGUI.BeginDisabledGroup(advertisementsCount <= lineUpCount);
                EditorGUILayout.LabelField("Animation Setting", titleStyle);
                EditorGUI.indentLevel++;
                var displayTimeProperty = serializedObject.FindProperty("displayTime");
                displayTimeProperty.floatValue =
                    EditorGUILayout.FloatField("Display Time", displayTimeProperty.floatValue);
                var switchingTimeProperty = serializedObject.FindProperty("switchingTime");
                switchingTimeProperty.floatValue =
                    EditorGUILayout.FloatField("Switching Time", switchingTimeProperty.floatValue);
                
                // 表示アニメーション
                EditorGUILayout.BeginVertical(boxStyle);
                {
                    EditorGUILayout.LabelField("Show Animation", innerTitleStyle);
                    EditorGUI.indentLevel++;
                    var animationTypeProperty = serializedObject.FindProperty("showAnimationType");
                    animationTypeProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Animation Type",
                        (ShowAnimationType) animationTypeProperty.enumValueIndex));
                    var easeTypeProperty = serializedObject.FindProperty("showAnimationEaseType");
                    easeTypeProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Ease Type",
                        (EaseType) easeTypeProperty.enumValueIndex));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();
                // 非表示アニメーション
                EditorGUILayout.BeginVertical(boxStyle);
                {
                    EditorGUILayout.LabelField("Hide Animation", innerTitleStyle);
                    EditorGUI.indentLevel++;
                    var animationTypeProperty = serializedObject.FindProperty("hideAnimationType");
                    animationTypeProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Animation Type",
                        (HideAnimationType) animationTypeProperty.enumValueIndex));
                    var easeTypeProperty = serializedObject.FindProperty("hideAnimationEaseType");
                    easeTypeProperty.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup("Ease Type",
                        (EaseType) easeTypeProperty.enumValueIndex));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndVertical();
        }
        
        private void showHologramSetting()
        {
            EditorGUILayout.BeginVertical(boxStyle);
            {
                EditorGUILayout.LabelField("Hologram Setting", titleStyle);
                EditorGUI.indentLevel++;
                var hologramColorProperty = serializedObject.FindProperty("hologramColor");
                hologramColorProperty.colorValue =
                    EditorGUILayout.ColorField("Hologram Color", hologramColorProperty.colorValue);
                var hologramNoiseAmountProperty = serializedObject.FindProperty("hologramNoiseAmount");
                hologramNoiseAmountProperty.floatValue =
                    EditorGUILayout.FloatField("Noise Amount", hologramNoiseAmountProperty.floatValue);
                var hologramNoiseStrengthProperty = serializedObject.FindProperty("hologramNoiseStrength");
                hologramNoiseStrengthProperty.floatValue =
                    EditorGUILayout.FloatField("Noise Strength", hologramNoiseStrengthProperty.floatValue);

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
        }

        private Texture2D makeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
#endif