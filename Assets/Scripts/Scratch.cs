using UnityEngine;

public class Scratch : MonoBehaviour
{
    [SerializeField] private SpriteMask spriteMask;
    [SerializeField] private Camera spriteCam;

    private RenderTexture _renderBuffer;
    private Texture2D _staticTexture;

    void Start()
    {
        // Creamos los buffers una sola vez al inicio
        _renderBuffer = new RenderTexture(Screen.width, Screen.height, 1, RenderTextureFormat.ARGB32);
        _staticTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        
        // Asignamos la textura al render de la cámara de forma permanente
        spriteCam.targetTexture = _renderBuffer;
    }

    public void AssignScreenAsMask() 
    {
        // 1. La cámara ya renderiza a _renderBuffer automáticamente. Solo forzamos el Render:
        spriteCam.Render();

        // 2. Solo si REALMENTE necesitas un Sprite (SpriteMask lo requiere), 
        // actualizamos la textura existente en lugar de crear una nueva.
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = _renderBuffer;
        
        _staticTexture.ReadPixels(new Rect(0, 0, _renderBuffer.width, _renderBuffer.height), 0, 0);
        _staticTexture.Apply();
        
        RenderTexture.active = prev;

        // 3. NO crees un nuevo Sprite. Si es la primera vez, créalo. 
        // Después, el Sprite ya apunta a '_staticTexture', la cual acabamos de actualizar.
        if (spriteMask.sprite == null)
        {
            Rect rect = new Rect(0, 0, _staticTexture.width, _staticTexture.height);
            spriteMask.sprite = Sprite.Create(_staticTexture, rect, new Vector2(0.5f, 0.5f), Screen.height / 10);
        }
    }

    private void OnDestroy()
    {
        // Limpieza manual obligatoria para objetos que no hereda de MonoBehaviour
        if (_renderBuffer != null) _renderBuffer.Release();
        if (_staticTexture != null) Destroy(_staticTexture);
    }
}
