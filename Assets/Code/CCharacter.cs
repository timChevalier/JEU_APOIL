using UnityEngine;
using System.Collections;

public class CCharacter : CElement 
{

	/// <summary>
	/// Initializes a new instance of the <see cref="CCharacter"/> class.
	/// </summary>
	public CCharacter()
	{

	}
	
	/// <summary>
	/// Init this instance.
	/// </summary>
	public new void Init()
	{	
		base.Init();
	}

	/// <summary>
	/// Reset this instance.
	/// </summary>
	public new void Reset()
	{
		base.Reset();
	}

	/// <summary>
	/// Process
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime. Time between 2 updates.
	/// </param>
	public new void Process(float fDeltatime)
	{
		base.Process(fDeltatime);
	}
}
