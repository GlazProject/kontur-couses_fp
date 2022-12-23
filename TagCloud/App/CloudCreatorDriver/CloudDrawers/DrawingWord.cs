﻿using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers;

public class DrawingWord : IDrawingWord
{
    private readonly IWord word;

    public DrawingWord(IWord word, Font font, Color color, Rectangle rectangle)
    {
        this.word = word;
        Font = font;
        Color = color;
        Rectangle = rectangle;
    }

    public string Value => word.Value;

    public int Count
    {
        get => word.Count;
        set => word.Count = value;
    }

    public double Tf
    {
        get => word.Tf;
        set => word.Tf = value;
    }

    public Font Font { get; }
    public Color Color { get; }
    public Rectangle Rectangle { get; }

    public bool Equals(IWord? other)
    {
        return ((IWord)this).Equals(other);
    }
}