using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Kyo.Manager;

public class ContentService {
    public Dictionary<string, Texture2D> Textures { get; private set; }
    public Dictionary<string, SpriteFont> Fonts { get; private set; }
    public Dictionary<string, Song> MusicBank { get; private set; }
    public Dictionary<string, SoundEffect> SoundBank { get; private set; }


    private static ContentService _instance;
    private ContentManager _content;

    private ContentService() {
        Textures = new();
        Fonts = new();
        MusicBank = new();
        SoundBank = new();
    }

    public static ContentService Instance {
        get {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }
/* Permet d'empêcher un freeeze des tasks et améliore la fluidité du chargement */
    public Task LoadContent(ContentManager content, GraphicsDevice graphics) {
        _content = content;

        AddTexture("circle");
        
        // Chargement du theme 1
        AddTexture("Images/128/square brown light_png_shadow_128px", "square_brown_light_128");
        AddTexture("Images/128/square brown dark_png_shadow_128px", "square_brown_dark_128");
        
        // Chargement du theme 2
        AddTexture("Images/128/square gray light _png_shadow_128px", "square_gray_light_128");
        AddTexture("Images/128/square gray dark _png_shadow_128px", "square_gray_dark_128");

        // Chargement des pièces blanches
        AddTexture("Images/128/w_pawn_png_shadow_128px", "wpawn");
        AddTexture("Images/128/w_knight_png_shadow_128px", "wknight");
        AddTexture("Images/128/w_bishop_png_shadow_128px", "wbishop");
        AddTexture("Images/128/w_rook_png_shadow_128px", "wrook");
        AddTexture("Images/128/w_queen_png_shadow_128px", "wqueen");
        AddTexture("Images/128/w_king_png_shadow_128px", "wking");

        // Chargement des pièces noires
        AddTexture("Images/128/b_pawn_png_shadow_128px", "bpawn");
        AddTexture("Images/128/b_knight_png_shadow_128px", "bknight");
        AddTexture("Images/128/b_bishop_png_shadow_128px", "bbishop");
        AddTexture("Images/128/b_rook_png_shadow_128px", "brook");
        AddTexture("Images/128/b_queen_png_shadow_128px", "bqueen");
        AddTexture("Images/128/b_king_png_shadow_128px", "bking");

        // Chargement de la font
        AddFont("Fonts/Debug28", "debug28");

        return Task.CompletedTask;
    }

    public void AddTexture(Texture2D texture, string key) {
        Textures.Add(key, texture);
    }

    public void AddTexture(string path, string key = null) {
        if (key == null)
            Textures.Add(path, _content.Load<Texture2D>(path));
        else
            Textures.Add(key, _content.Load<Texture2D>(path));
    }

    public void AddFont(string path, string key = null) {
        if (key == null)
            Fonts.Add(path, _content.Load<SpriteFont>(path));
        else
            Fonts.Add(key, _content.Load<SpriteFont>(path));
    }

    public void AddMusic(string path, string key = null) {
        if (key == null)
            MusicBank.Add(path, _content.Load<Song>(path));
        else
            MusicBank.Add(key, _content.Load<Song>(path));
    }

    public void AddSoundEffect(string path, string key = null) {
        if (key == null)
            SoundBank.Add(path, _content.Load<SoundEffect>(path));
        else
            SoundBank.Add(key, _content.Load<SoundEffect>(path));
    }
}