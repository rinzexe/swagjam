using UnityEngine;
using UnityEngine.UI;

public class WindowTask : Trigger
{
    public GameObject taskPanel;
    private Material material;
    public float brushSize = 20f;
    private RectTransform rectTransform;
    private RenderTexture alphaTexture;
    private Image windowImage;
    private bool taskActive = false;

    void Awake()
    {
        taskPanel.SetActive(false);
    }

    public void StartTask()
    {
        if (TaskManager.Instance.windowCooldown < 0)
        {
            PlayerController.Instance.canMove = false;
            taskPanel.SetActive(true);
            taskActive = true;

            windowImage = taskPanel.transform.GetChild(0).GetComponent<Image>();
            rectTransform = windowImage.rectTransform;

            material = new Material(windowImage.material);
            windowImage.material = material;

            alphaTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
            alphaTexture.Create();

            material.SetTexture("_AlphaTex", alphaTexture);

            RenderTexture.active = alphaTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
    }

    void LateUpdate()
    {
        if (!taskActive) return;

        if (Input.GetMouseButton(0))
        {
            DrawAtPosition(Input.mousePosition);
            CheckProgress();
        }
    }

void DrawAtPosition(Vector2 screenPosition)
{
    Vector2 localPoint;
    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
        rectTransform,
        screenPosition,
        null,
        out localPoint))
    {
        return;
    }

    Vector2 normalizedPoint = Rect.PointToNormalized(
        rectTransform.rect,
        localPoint
    );

    float pixelX = normalizedPoint.x * alphaTexture.width;
    float pixelY = (1 - normalizedPoint.y) * alphaTexture.height;

    // Create a circular brush texture
    int brushTexSize = 64; // Make it a power of 2 for better performance
    Texture2D brushTex = new Texture2D(brushTexSize, brushTexSize);
    Color[] colors = new Color[brushTexSize * brushTexSize];
    
    // Create a soft circular brush
    Vector2 center = new Vector2(brushTexSize / 2, brushTexSize / 2);
    float radius = brushTexSize / 2f;
    
    for (int y = 0; y < brushTexSize; y++)
    {
        for (int x = 0; x < brushTexSize; x++)
        {
            float distance = Vector2.Distance(new Vector2(x, y), center);
            float alpha = Mathf.Max(0, 1 - (distance / radius));
            // Smooth falloff
            alpha = Mathf.SmoothStep(0, 1, alpha);
            colors[y * brushTexSize + x] = new Color(1, 1, 1, alpha);
        }
    }
    
    brushTex.SetPixels(colors);
    brushTex.Apply();

    // Set up drawing state
    RenderTexture previousRT = RenderTexture.active;
    RenderTexture.active = alphaTexture;

    GL.PushMatrix();
    GL.LoadPixelMatrix(0, alphaTexture.height, alphaTexture.width, 0);

    // Draw the circular brush
    Graphics.DrawTexture(
        new Rect(pixelX - brushSize/2, pixelY - brushSize/2, brushSize, brushSize),
        brushTex,
        new Rect(0, 0, 1, 1),
        0, 0, 0, 0,
        Color.white
    );

    GL.PopMatrix();
    RenderTexture.active = previousRT;
    Destroy(brushTex);
}

    void CheckProgress()
    {
        if (Time.frameCount % 10 != 0) return;

        Texture2D temp = new Texture2D(alphaTexture.width, alphaTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = alphaTexture;
        temp.ReadPixels(new Rect(0, 0, alphaTexture.width, alphaTexture.height), 0, 0);
        temp.Apply();
        RenderTexture.active = null;

        Color[] pixels = temp.GetPixels();
        int whitePixels = 0;
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].r > 0.5f) // Consider pixels above 0.5 as cleaned
            {
                whitePixels++;
            }
        }

        float progress = (float)whitePixels / pixels.Length;

        if (progress > 0.99f)
        {
            CompleteTask();
        }

        Destroy(temp);
    }

    public void CompleteTask()
    {
        taskActive = false;
        taskPanel.SetActive(false);
        TaskManager.Instance.CompleteWindows();

        if (alphaTexture != null)
        {
            alphaTexture.Release();
            Destroy(alphaTexture);
        }
        if (material != null)
        {
            Destroy(material);
        }
    }

    void OnDestroy()
    {
        if (alphaTexture != null)
        {
            alphaTexture.Release();
            Destroy(alphaTexture);
        }
        if (material != null)
        {
            Destroy(material);
        }
    }
}