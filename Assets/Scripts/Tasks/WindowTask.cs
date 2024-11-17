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
    public bool taskActive = false;
    
    [SerializeField] private Material defaultWindowMaterial; // Reference to the initial dirty window material
    private Material originalMaterial; // Store the original material

    public DialogueSequence tooEarlySequence;

    void Awake()
    {
        taskPanel.SetActive(false);
        // Store reference to original window material
        windowImage = taskPanel.transform.GetChild(0).GetComponent<Image>();
        originalMaterial = windowImage.material;
    }

    private void CleanupResources()
    {
        if (alphaTexture != null)
        {
            alphaTexture.Release();
            Destroy(alphaTexture);
            alphaTexture = null;
        }
        if (material != null)
        {
            Destroy(material);
            material = null;
        }
    }

    private void ResetWindow()
    {
        // Reset to the dirty window material
        if (windowImage != null)
        {
            if (defaultWindowMaterial != null)
            {
                windowImage.material = new Material(defaultWindowMaterial);
            }
            else
            {
                // Fallback to original material if default dirty material isn't set
                windowImage.material = new Material(originalMaterial);
            }
        }
    }

    private void InitializeResources()
    {
        windowImage = taskPanel.transform.GetChild(0).GetComponent<Image>();
        rectTransform = windowImage.rectTransform;

        // Create new material instance
        material = new Material(windowImage.material);
        windowImage.material = material;

        // Create and initialize alpha texture
        alphaTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
        alphaTexture.Create();

        material.SetTexture("_AlphaTex", alphaTexture);

        // Clear the texture to black (dirty window)
        RenderTexture.active = alphaTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;
    }

    public void StartTask()
    {
        if (TaskManager.Instance.windowCooldown < 0)
        {
            PlayerController.Instance.canMove = false;
            
            // Clean up any existing resources first
            CleanupResources();
            
            // Reset the window to dirty state
            ResetWindow();
            
            taskPanel.SetActive(true);
            taskActive = true;
            
            // Initialize fresh resources
            InitializeResources();
        }
        else 
        {
            DialogueManager.Instance.StartSequence(tooEarlySequence, null);
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

        // Add escape key to cancel task
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelTask();
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

        int brushTexSize = 64; 
        Texture2D brushTex = new Texture2D(brushTexSize, brushTexSize);
        Color[] colors = new Color[brushTexSize * brushTexSize];
        
        Vector2 center = new Vector2(brushTexSize / 2, brushTexSize / 2);
        float radius = brushTexSize / 2f;
        
        for (int y = 0; y < brushTexSize; y++)
        {
            for (int x = 0; x < brushTexSize; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                float alpha = Mathf.Max(0, 1 - (distance / radius));

                alpha = Mathf.SmoothStep(0, 1, alpha);
                colors[y * brushTexSize + x] = new Color(1, 1, 1, alpha);
            }
        }
        
        brushTex.SetPixels(colors);
        brushTex.Apply();

        RenderTexture previousRT = RenderTexture.active;
        RenderTexture.active = alphaTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, alphaTexture.height, alphaTexture.width, 0);

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

        if (progress > 0.999f)
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
        
        // Clean up resources
        CleanupResources();
        
        // Reset window to original material
        if (windowImage != null)
        {
            windowImage.material = new Material(originalMaterial);
        }
        
        PlayerController.Instance.canMove = true;
    }

    public void CancelTask()
    {
        taskActive = false;
        taskPanel.SetActive(false);
        
        // Clean up resources
        CleanupResources();
        
        // Reset window to original material
        if (windowImage != null)
        {
            windowImage.material = new Material(originalMaterial);
        }
        
        PlayerController.Instance.canMove = true;
    }

    void OnDisable()
    {
        if (taskActive)
        {
            CancelTask();
        }
    }

    void OnDestroy()
    {
        CleanupResources();
    }
}