using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using SFML.Game.Framework.Content;
using SFML.Game.Framework.Graphics;

#nullable disable
namespace SFML.Game.Framework.Components;

public class DebugComponent : IDrawableGameComponent
{

    private float[] graphPoints;      // fixed-size array
    private int graphIndex = 0;       // current write index
    private int maxGraphPoints = 300; // number of points = width of graph in pixels

    GraphicsDrawer drawer;
    private const int MaxFrameHistory = 300; // store last 300 frames (~5s at 60fps)
    private readonly Queue<float> frameTimes = new Queue<float>();

    private Vector2 graphPosition = new Vector2(10, 50);
    private Vector2 graphSize = new Vector2(300, 20);
    private float elapsedTime = 0f;   // accumulated time in seconds
    private int frameCounter = 0;     // frames counted in current second
    private float accumulatedFrameTime = 0f; // sum of frame times in ms
    private float currentFPS = 0f;    // FPS updated once per second

    public bool IsDisposed { get; private set; }
    public Game Game { get; }
    public Font Font { get; set; } = null;
    public int FontSize { get; set; } = 12;
    public Color FontColor { get; set; } = Color.White;
    public Color Background { get; set; } = new Color( 0, 0, 0, 255);
    public Color GraphColor { get; set; } = Color.White;

    /// <summary>Latest calculated FPS</summary>
    public float FPS { get; private set; }
    /// <summary>Average FPS over all recorded frames</summary>
    public float AverageFPS => frameTimes.Count > 0 ? 1f / ( frameTimes.Average() / 1000f ) : 0;
    /// <summary>1% low FPS (bottom 1% slowest frames)</summary>
    public float OnePercentLow
    {
        get
        {
            if ( frameTimes.Count == 0 ) return 0;
            int count = Math.Max(1, frameTimes.Count / 100);
            return 1000f / frameTimes.OrderByDescending( t => t ).Take( count ).Average();
        }
    }

    public DebugComponent( Game game )
    {
        Game = game;
    }

    public void Dispose()
    {
        IsDisposed = true;
    }

    public void Initialize() { drawer = new GraphicsDrawer( Game.GraphicsDevice as GraphicsDevice); InitializeGraph( 20 );  graphSize = new Vector2( 100, 20 + FontSize ); }
    public void LoadContent() { }

    public void Update( GameTime gameTime )
    {

        accumulatedFrameTime += gameTime.Delta; // in seconds

        if ( elapsedTime >= 1.0f ) // once per second
        {
            // Real FPS based on frames actually drawn
            currentFPS = frameCounter / elapsedTime;
            FPS = currentFPS;

            // Store average frame time in ms for graph
            float avgFrameTimeMs = (accumulatedFrameTime / frameCounter) * 1000f;
            frameTimes.Enqueue( avgFrameTimeMs );
            AddGraphPointWrapped( avgFrameTimeMs );

            if ( frameTimes.Count > MaxFrameHistory )
                frameTimes.Dequeue();

            // Reset for next second
            frameCounter = 0;
            accumulatedFrameTime = 0f;
            elapsedTime = 0f;
        }
    }
    public void Draw( GameTime gameTime )
    {

        if ( Font == null ) return;

        drawer.Begin( BlendState.Alpha );

        // Prepare text lines
        string fpsText = $"FPS: {FPS:0}";
        string avgText = $"Avg FPS: {AverageFPS:0}";
        string lowText = $"1% Low: {OnePercentLow:0}";
        string latencyText = $"Latency: {frameTimes.LastOrDefault():0.0} ms";

        List<string> lines = new List<string> { fpsText, avgText, lowText, latencyText };

        // Measure maximum text width
        float maxWidth = 0;
        float lineHeight = Font.MeasureString("Test", (uint)FontSize).Y;
        float lineSpacing = 10f; // Y spacing between lines

        foreach ( var line in lines )
        {
            var size = Font.MeasureString(line, (uint)FontSize);
            if ( size.X > maxWidth ) maxWidth = size.X;
        }

        float padding = 5f;
        float bgWidth = maxWidth + padding * 2;
        float bgHeight = lines.Count * (lineHeight + lineSpacing) + padding * 2 + graphSize.Y + 5;

        // Draw background
        drawer.DrawRoundedRectangle(new (bgWidth, bgHeight), new (5,5), 5, Background * 0.5f, Color.Transparent );

        // Draw text with spacing
        for ( int i = 0; i < lines.Count; i++ )
        {
            drawer.DrawString( lines[i], Font, FontSize,
                new Vector2( 5 + padding, 5 + padding + i * ( lineHeight + lineSpacing ) ),
                FontColor );
        }

        drawer.End();

        drawer.Begin( BlendState.Additive );

        // Draw graph below text
        Vector2 graphOrigin = new Vector2(5 + padding, 5 + padding + lines.Count * (lineHeight + lineSpacing) + 5);
   

        DrawGraphWrapped( graphOrigin, (5 + bgWidth)- (FontSize), FontSize, GraphColor);

        drawer.End();

        frameCounter++;
        elapsedTime += ( float )gameTime.Delta; // or stopwatch delta
    }

    /// <summary>
    /// Initializes the rolling graph buffer.
    /// </summary>
    private void InitializeGraph( int width )
    {
        maxGraphPoints = width;
        graphPoints = new float[maxGraphPoints];
        for ( int i = 0; i < maxGraphPoints; i++ )
            graphPoints[i] = 0f;
    }

    /// <summary>
    /// Adds a new frame time to the wrapped graph (overwrites oldest point).
    /// </summary>
    private void AddGraphPointWrapped( float frameTime )
    {
        graphPoints[graphIndex] = frameTime;
        graphIndex = ( graphIndex + 1 ) % maxGraphPoints; // wrap around
    }
    /// <summary>
    /// Draws the wrapped graph using angle-based lines for smoothness.
    /// </summary>
    private void DrawGraphWrapped( Vector2 position, float graphWidth, float graphHeight, Color color )
    {
        if ( graphPoints == null || graphPoints.Length < 2 ) return;

        float maxMs = Math.Max(16.0f, graphPoints.Max());
        float scaleX = graphWidth / (graphPoints.Length - 1);
        float scaleY = graphHeight / maxMs;

        int pointsCount = graphPoints.Length;
        int startIndex = graphIndex; // start from the oldest point

        Vector2 prev = new Vector2(position.X, position.Y + graphHeight - graphPoints[startIndex] * scaleY);

        for ( int i = 1; i <= pointsCount; i++ )
        {
            int idx = (startIndex + i) % pointsCount;
            Vector2 current = new Vector2(position.X + i * scaleX, position.Y + graphHeight - graphPoints[idx] * scaleY);

            // Calculate angle for smooth line
            float dx = current.X - prev.X;
            float dy = current.Y - prev.Y;
            float angle = MathF.Atan2(dy, dx) * 180f / MathF.PI;

            // Draw line using angle
            drawer.DrawLine( prev, current, color, 2f, angle );

            prev = current;
        }
    }

}