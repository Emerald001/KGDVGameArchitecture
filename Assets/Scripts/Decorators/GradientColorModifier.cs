using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GradientColorModifier", order = 1)]

public class GradientColorModifier : GunModifier
{
    public Gradient bulletGradient;
    public float gradientStep;
    private float gradientEvaluator;

    public override void OnGunShoot()
    {
        tempGun.bulletColor = bulletGradient.Evaluate(gradientEvaluator);
        gradientEvaluator += gradientStep;
        base.OnGunShoot();

        if(gradientEvaluator > 1) { gradientEvaluator = 0; }
    }
}
