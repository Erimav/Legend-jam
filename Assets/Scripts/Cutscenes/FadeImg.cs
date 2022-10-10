using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImg : MonoBehaviour
{
    [SerializeField] private Image[] TargetImgs;
    private List<Color> DesiredColour = new();
    private List<Color> StartColour = new();
    public bool IsFading;

    public void Setup()
    {
        //Preload list data
        foreach(Image img in TargetImgs)
        {
            DesiredColour.Add(img.color);
            StartColour.Add(img.color);
        }
    }

    public void FadeIn()
    {
        //aim for the start colour, set to 0 alpha
        for(int i = 0; i < DesiredColour.Count; i++)
        {
            DesiredColour[i] = StartColour[i];
            TargetImgs[i].color = new Color(StartColour[i][0], StartColour[i][1], StartColour[i][2], 0);
        }

        IsFading = true;
        StartCoroutine(nameof(Fade), 1);
    }

    public void FadeOut()
    {
        //aim for 0 alpha, set to start colour
        for (int i = 0; i < DesiredColour.Count; i++)
        {
            DesiredColour[i] = StartColour[i];
            DesiredColour[i] = new Color(StartColour[i][0], StartColour[i][1], StartColour[i][2], 0);
            TargetImgs[i].color = StartColour[i];
        }

        IsFading = true;
        StartCoroutine(nameof(Fade), 1);
    }

    private IEnumerator Fade(float FadeDuration)
    {
        for (int i = 0; i < TargetImgs.Length; i++)
        {
            Color initialColor = TargetImgs[i].color;

            float elapsedTime = 0f;

            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;
                TargetImgs[i].color = Color.Lerp(initialColor, DesiredColour[i], elapsedTime / FadeDuration);
                yield return null;
            }
        }

        IsFading = false;
    }
}
