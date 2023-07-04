using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{

    [SerializeField] private GameObject roomCorner;
    [SerializeField] private GameObject roomWall;
    [SerializeField] private GameObject roomFloor;

    public void Generate(Grid<int> grid)
    {
        for(int x = 0; x<grid.Width; x++)
        {
            for(int y = 0; y<grid.Heigth; y++)
            {
                GameObject prefab;
                Vector3 rotation;

                //Cuartos
                if (grid.GetValue(x,y) == 0)
                {

                    int[,] neighbour = new int[3, 3];
                    //Fila inferior
                    neighbour[0, 0] = grid.GetValue(x - 1, y - 1);
                    neighbour[1, 0] = grid.GetValue(x, y - 1);
                    neighbour[2, 0] = grid.GetValue(x + 1, y - 1);

                    //Fila central
                    neighbour[0, 1] = grid.GetValue(x - 1, y);
                    neighbour[1, 1] = grid.GetValue(x, y);
                    neighbour[2, 1] = grid.GetValue(x + 1, y);

                    //Fila superior
                    neighbour[0, 2] = grid.GetValue(x - 1, y + 1);
                    neighbour[1, 2] = grid.GetValue(x, y + 1);
                    neighbour[2, 2] = grid.GetValue(x + 1, y + 1);

                    int left = neighbour[0, 1];
                    int rigth = neighbour[2, 1];
                    int top = neighbour[1, 2];
                    int bottom = neighbour[1, 0];

                    

                    //Esquina superior izquierda
                    if (top == -1 && left == -1 && bottom == 0 && rigth == 0)
                    {
                        prefab = roomCorner;
                        rotation = new Vector3(0, 0, -90);
                    }

                    //Esquina superior derecha
                    else if (left == 0 && top == -1 && rigth == -1 && bottom == 0)
                    {
                        prefab = roomCorner;
                        rotation = new Vector3(0, 0, -180);
                    }

                    //Esquina inferior izquierda
                    else if (left == -1 && top == 0 && rigth == 0 && bottom == -1)
                    {
                        prefab = roomCorner;
                        rotation = new Vector3(0, 0, 0);
                    }
                    //Esquina inferior derecha
                    else if (left == 0 && top == 0 && rigth == -1 && bottom == -1)
                    {
                        prefab = roomCorner;
                        rotation = new Vector3(0, 0, 90);
                    }
                    //Parede izquierda
                    else if (left == -1 && top == 0 && rigth == 0 && bottom == 0)
                    {
                        prefab = roomWall;
                        rotation = Vector3.zero;
                    }
                    //Pared superior
                    else if (left == 0 && top == -1 && rigth == 0 && bottom == 0)
                    {
                        prefab = roomWall;
                        rotation = new Vector3(0, 0, -90);
                    }
                    //Pared derecha
                    else if (left == 0 && top == 0 && rigth == -1 && bottom == 0)
                    {
                        prefab = roomWall;
                        rotation = new Vector3(0, 0, -180);
                    }
                    //Pared inferior
                    else if (left == 0 && top == 0 && rigth == 0 && bottom == -1)
                    {
                        prefab = roomWall;
                        rotation = new Vector3(0, 0, 90);
                    }
                    //Parte central cuerto
                    else
                    {
                        prefab = roomFloor;
                        rotation = Vector3.zero;
                    }

                    grid.IndexToPoint(x, y, out Vector2 point);
                    Vector3 center = point + (Vector2.one * grid.Size) / 2.0f;

                    Instantiate(prefab, center , Quaternion.Euler(rotation));
                    
                }

            }
        }
    }
}
