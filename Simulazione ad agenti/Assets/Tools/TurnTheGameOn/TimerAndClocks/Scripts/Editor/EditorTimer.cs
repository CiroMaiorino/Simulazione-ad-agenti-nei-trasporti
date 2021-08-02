namespace TurnTheGameOn.Timer
{
	using UnityEngine;
	using UnityEditor;

	[CustomEditor(typeof(Timer))]
	public class GameManagerEditor : Editor 
	{
		static bool showTimesUpOptions;

		private void OnEnable()
		{
			EditorApplication.update += UpdateTimerUIText;
		}

		private void OnDisable()
		{
			EditorApplication.update -= UpdateTimerUIText;
		}

		public void UpdateTimerUIText()
		{
			if (Application.isPlaying == false)
			{
				Timer timer = (Timer)target;
				timer.ClampGameTime();
				if (timer.timerType == TimerType.CountUp || timer.timerType == TimerType.CountDown)
				{
					timer.SetTimerValue(timer.startTime);
				}
				timer.UpdateUIText();
			}
		}

		public override void OnInspectorGUI()
		{
			GUI.enabled = false; EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((Timer)target), typeof(Timer), false); GUI.enabled = true;

			serializedObject.Update();
			Timer timer = (Timer)target;
		
			EditorGUILayout.LabelField ("Timer Options", EditorStyles.boldLabel);

			EditorGUILayout.BeginVertical("Box");
			//Timer State
			SerializedProperty timerState = serializedObject.FindProperty ("timerState");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(timerState, true);
			if(EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			//Timer Type
			SerializedProperty timerType = serializedObject.FindProperty("timerType");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(timerType, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			// Start Time
			SerializedProperty startTime = serializedObject.FindProperty("startTime");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(startTime, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			if (timer.timerType == TimerType.CountUp) 
			{
				SerializedProperty finishTime = serializedObject.FindProperty("finishTime");
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(finishTime, true);
				if (EditorGUI.EndChangeCheck())
					serializedObject.ApplyModifiedProperties();
			}

			SerializedProperty timerSpeed = serializedObject.FindProperty("timerSpeed");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(timerSpeed, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			if (timer.timerType != TimerType.CountUpInfinite)
			{
				SerializedProperty loop = serializedObject.FindProperty("loop");
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(loop, true);
				if (EditorGUI.EndChangeCheck())
					serializedObject.ApplyModifiedProperties();
			}

			if (timer.timerType == TimerType.CountUpInfinite)
			{				
				//Use System Time
				SerializedProperty useSystemTime = serializedObject.FindProperty ("useSystemTime");
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(useSystemTime, true);
				if(EditorGUI.EndChangeCheck())
					serializedObject.ApplyModifiedProperties();
			}
			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginVertical("Box");
			SerializedProperty timeEvents = serializedObject.FindProperty("timeEvents");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(timeEvents, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();

			if (timer.timerType != TimerType.CountUpInfinite)
			{
				EditorGUILayout.BeginVertical("Box");
				showTimesUpOptions = EditorGUI.Foldout(EditorGUILayout.GetControlRect(), showTimesUpOptions, "Time's Up Event", true);
				if (showTimesUpOptions)
				{
					//Set Zero Timescale
					SerializedProperty setZeroTimescale = serializedObject.FindProperty("setZeroTimescale");
					EditorGUI.BeginChangeCheck();
					EditorGUILayout.PropertyField(setZeroTimescale, true);
					if (EditorGUI.EndChangeCheck())
						serializedObject.ApplyModifiedProperties();
					//Times Up Event
					SerializedProperty timesUpEvent = serializedObject.FindProperty("timesUpEvent");
					EditorGUI.BeginChangeCheck();
					EditorGUILayout.PropertyField(timesUpEvent, true);
					if (EditorGUI.EndChangeCheck())
						serializedObject.ApplyModifiedProperties();
				}
				EditorGUILayout.EndVertical ();
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField ("UI Text Output", EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical("Box");
			
			// Timer Text
			SerializedProperty textType = serializedObject.FindProperty ("textType");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(textType, true);
			if(EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			if (timer.textType == TimerTextType.DefaultText)
			{
				SerializedProperty timerTextDefault = serializedObject.FindProperty ("timerTextDefault");
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(timerTextDefault, true);
				if(EditorGUI.EndChangeCheck())
					serializedObject.ApplyModifiedProperties();
			}
			else if (timer.textType == TimerTextType.TextMeshProUGUI)
			{
				SerializedProperty timerTextTMPUGUI = serializedObject.FindProperty ("timerTextTMPUGUI");
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(timerTextTMPUGUI, true);
				if(EditorGUI.EndChangeCheck())
					serializedObject.ApplyModifiedProperties();
			}

			//Display Options
			SerializedProperty displayOptions = serializedObject.FindProperty("displayOptions");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(displayOptions, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			if (timer.timerType != TimerType.CountUpInfinite) 
			{
				SerializedProperty leadingZero = serializedObject.FindProperty ("leadingZero");
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(leadingZero, true);
				if(EditorGUI.EndChangeCheck())
					serializedObject.ApplyModifiedProperties();
			}

			EditorGUILayout.EndVertical ();
			if (GUI.changed) EditorUtility.SetDirty (target);
		}

	}
}