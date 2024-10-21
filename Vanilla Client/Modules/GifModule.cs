using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace Vanilla.Modules
{
    /*
    internal class GifModule : VanillaModule
    {
        public string imagePath = "Assets/Textures/spritesheet.png"; // Path to the sprite sheet
        public int columns = 4; // Number of columns in the sprite sheet
        public int rows = 4;    // Number of rows in the sprite sheet
        public float frameDelay = 0.1f; // Delay between frames

        private List<Sprite> frames = new List<Sprite>();
        private Image imageComponent; // UI Image component to display the sprite
        private int currentFrame;
        private float timer;
        internal override void Start()
        {
            Texture2D spriteSheet = LoadTexture(imagePath);
            if (spriteSheet != null)
            {
                // Slice the sprite sheet into frames
                frames = SliceSpriteSheet(spriteSheet, columns, rows);

                // Find the Image component to display the frames
                imageComponent = GameObject.Find("Player Nameplate/Canvas/Nameplate/Contents/Icon/User Image").GetComponent<Image>();
            }
        }

        internal override void Update()
        {
            if (frames.Count == 0 || imageComponent == null) return;

            // Timer to control frame playback
            timer += Time.deltaTime;
            if (timer >= frameDelay)
            {
                currentFrame = (currentFrame + 1) % frames.Count; // Loop through the frames
                imageComponent.sprite = frames[currentFrame]; // Update the sprite
                timer = 0f;
            }
        }

        private Texture2D LoadTexture(string path)
        {
            byte[] imageData = File.ReadAllBytes(path); // Load file into byte array
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData); // Load byte array into texture
            return texture;
        }

        private List<Sprite> SliceSpriteSheet(Texture2D spriteSheet, int columns, int rows)
        {
            List<Sprite> spriteList = new List<Sprite>();
            int frameWidth = spriteSheet.width / columns;
            int frameHeight = spriteSheet.height / rows;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    // Create a new sprite for each frame
                    Rect frameRect = new Rect(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                    Sprite newSprite = Sprite.Create(spriteSheet, frameRect, new Vector2(0.5f, 0.5f));
                    spriteList.Add(newSprite);
                }
            }

            return spriteList;
        }

    }*/
}
