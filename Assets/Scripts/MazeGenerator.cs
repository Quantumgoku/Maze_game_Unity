using System;
using System.Collections.Generic;
using UnityEngine;

public enum WallState
{
    LEFT = 1,
    RIGHT = 2,
    UP = 4,
    DOWN = 8,
    VISITED = 128,
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    private static readonly System.Random rng = new System.Random();

    private static WallState GetOppositeWall(WallState wall)
    {
        return wall switch
        {
            WallState.LEFT => WallState.RIGHT,
            WallState.UP => WallState.DOWN,
            WallState.DOWN => WallState.UP,
            WallState.RIGHT => WallState.LEFT,
            _ => WallState.LEFT,
        };
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        var positionStack = new Stack<Position>();
        var startPosition = new Position { X = rng.Next(width), Y = rng.Next(height) };

        maze[startPosition.X, startPosition.Y] |= WallState.VISITED;
        positionStack.Push(startPosition);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randomNeighbour = neighbours[rng.Next(neighbours.Count)];
                var nPosition = randomNeighbour.Position;

                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;
                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0 && !maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
        {
            list.Add(new Neighbour
            {
                Position = new Position { X = p.X - 1, Y = p.Y },
                SharedWall = WallState.LEFT
            });
        }

        if (p.Y > 0 && !maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
        {
            list.Add(new Neighbour
            {
                Position = new Position { X = p.X, Y = p.Y - 1 },
                SharedWall = WallState.DOWN
            });
        }

        if (p.Y < height - 1 && !maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
        {
            list.Add(new Neighbour
            {
                Position = new Position { X = p.X, Y = p.Y + 1 },
                SharedWall = WallState.UP
            });
        }

        if (p.X < width - 1 && !maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
        {
            list.Add(new Neighbour
            {
                Position = new Position { X = p.X + 1, Y = p.Y },
                SharedWall = WallState.RIGHT
            });
        }

        return list;
    }

    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.LEFT | WallState.RIGHT | WallState.UP | WallState.DOWN;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = initial;
            }
        }

        return ApplyRecursiveBacktracker(maze, width, height);
    }
}
