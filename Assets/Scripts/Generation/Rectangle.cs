using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle
{
    private Vector2 origin;
    private Vector2 size;

    public Vector2 Origin => origin;

    public Vector2 Size => size;

    public Vector2 Center => origin + (size / 2.0f);

    public Rectangle(Vector2 origin, Vector2 size)
    {
        this.origin = origin;
        this.size = size;
    }

    
    public void Draw(Color color, float duration)
    {
        Vector2 bottomLeftCorner = origin;
        Vector2 bottomRightCorner = origin + new Vector2(size.x, 0);
        Vector2 topLeftCorner = origin + new Vector2(0,size.y);
        Vector2 topRightCorner = origin + size;

        Debug.DrawLine(bottomLeftCorner, topLeftCorner, color, duration);
        Debug.DrawLine(topLeftCorner, topRightCorner, color, duration);
        Debug.DrawLine(topRightCorner, bottomRightCorner, color, duration);
        Debug.DrawLine(bottomRightCorner, bottomLeftCorner, color, duration);

    }
}



