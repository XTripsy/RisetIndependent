using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    Color Pen_Colour;
    int Pen_Width;

    public LayerMask Drawing_Layers;

    bool Reset_Canvas_On_Play = true;
    Color Reset_Colour = new Color(1, 1, 1, 1);

    bool Reset_To_This_Texture_On_Play = false;
    public Texture2D reset_texture;

    Sprite drawable_sprite;
    Texture2D drawable_texture;

    Vector2 previous_drag_position;
    Color[] clean_colours_array;
    Color32[] cur_colors;
    bool mouse_was_previously_held_down = false;
    bool no_drawing_on_current_drag = false;

    public void PenBrush(Vector2 world_point)
    {
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

        cur_colors = drawable_texture.GetPixels32();

        if (previous_drag_position == Vector2.zero)
            MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Colour);
        else
            ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Colour);

        ApplyMarkedPixelChanges();
        previous_drag_position = pixel_pos;
    }

    void Update()
    {
        bool mouse_held_down = Input.GetMouseButton(0);
        if (mouse_held_down && !no_drawing_on_current_drag)
        {
            Debug.LogWarning("Hold");
            Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, Drawing_Layers.value);
            if (hit != null && hit.transform != null)
                PenBrush(mouse_world_position);
            else
            {
                Debug.LogWarning("Out");
                previous_drag_position = Vector2.zero;
                if (!mouse_was_previously_held_down)
                    no_drawing_on_current_drag = true;
            }
        }
        else if (!mouse_held_down)
        {
            Debug.LogWarning("Release");
            previous_drag_position = Vector2.zero;
            no_drawing_on_current_drag = false;
        }
        mouse_was_previously_held_down = mouse_held_down;
    }

    public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
    {
        float distance = Vector2.Distance(start_point, end_point);
        Vector2 direction = (start_point - end_point).normalized;

        Vector2 cur_position = start_point;
        float lerp_steps = 1 / distance;

        for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
        {
            cur_position = Vector2.Lerp(start_point, end_point, lerp);
            MarkPixelsToColour(cur_position, width, color);
        }
    }

    public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {
            if (x >= (int)drawable_sprite.rect.width || x < 0)
                continue;

            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                MarkPixelToChange(x, y, color_of_pen);
            }
        }
    }

    public void MarkPixelToChange(int x, int y, Color color)
    {
        int array_pos = y * (int)drawable_sprite.rect.width + x;
        if (array_pos > cur_colors.Length || array_pos < 0)
            return;

        cur_colors[array_pos] = color;
    }
    public void ApplyMarkedPixelChanges()
    {
        drawable_texture.SetPixels32(cur_colors);
        drawable_texture.Apply();
    }

    public void ColourPixels(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {
            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                drawable_texture.SetPixel(x, y, color_of_pen);
            }
        }

        drawable_texture.Apply();
    }


    public Vector2 WorldToPixelCoordinates(Vector2 world_position)
    {
        Vector3 local_pos = transform.InverseTransformPoint(world_position);
        float pixelWidth = drawable_sprite.rect.width;
        float pixelHeight = drawable_sprite.rect.height;
        float unitsToPixels = pixelWidth / drawable_sprite.bounds.size.x * transform.localScale.x;
        float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
        float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;
        Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));
        return pixel_pos;
    }

    public Vector3 PixelToWorldCoordinates(Vector2 pixel_pos)
    {
        float pixelWidth = drawable_sprite.rect.width;
        float pixelHeight = drawable_sprite.rect.height;
        float unitsToPixels = pixelWidth / drawable_sprite.bounds.size.x * transform.localScale.x;
        float uncentered_x = pixel_pos.x / unitsToPixels - pixelWidth / 2;
        float uncentered_y = pixel_pos.y / unitsToPixels - pixelHeight / 2;
        Vector3 world_pos = transform.TransformPoint(new Vector3(uncentered_x, uncentered_y, 0f));
        return world_pos;
    }

    public void ResetCanvas()
    {
        drawable_texture.SetPixels(clean_colours_array);
        drawable_texture.Apply();
    }

    void Awake()
    {
        Pen_Colour = new Color(255f, 255f, 255f, 0f);
        Pen_Width = 15;

        drawable_sprite = this.GetComponent<SpriteRenderer>().sprite;
        drawable_texture = drawable_sprite.texture;

        clean_colours_array = new Color[(int)drawable_sprite.rect.width * (int)drawable_sprite.rect.height];
        for (int x = 0; x < clean_colours_array.Length; x++)
            clean_colours_array[x] = Reset_Colour;

        if (Reset_Canvas_On_Play)
            ResetCanvas();
        else if (Reset_To_This_Texture_On_Play)
            Graphics.CopyTexture(reset_texture, drawable_texture);
    }
}
