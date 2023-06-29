Console.WriteLine("***** Fun with Lazy Instantiation *****\n");
// No allocation of AllTracks object here!
MediaPlayer myPlayer = new MediaPlayer();
myPlayer.Play();
// Allocation of AllTracks happens when you call GetAllTracks().
MediaPlayer yourPlayer = new MediaPlayer();
AllTracks yourMusic = yourPlayer.GetAllTracks();
Console.ReadLine();

// Represents a single song.
class Song
{
    public string Artist { get; set; }
    public string TrackName { get; set; }
    public double TrackLength { get; set; }
}

// Represents all songs on a player.
class AllTracks
{
    // Our media player can have a maximum of 10,000 songs.
    private Song[] _allSongs = new Song[10000];
    public AllTracks()
    {
        // Assume we fill up the array of Song objects here.
        Console.WriteLine("Filling up the songs!");
    }
}

// The MediaPlayer has-an AllTracks object.
class MediaPlayer
{
    // Assume these methods do something useful.
    public void Play() { /* Play a song */ }
    public void Pause() { /* Pause the song */ }
    public void Stop() { /* Stop playback */ }

    private Lazy<AllTracks> _allSongs = new Lazy<AllTracks>(() =>
                 {
                     Console.WriteLine("Creating AllTracks object!");
                     return new AllTracks();
                 });
    public AllTracks GetAllTracks()
    {
        // Return all of the songs.
        return _allSongs.Value;
    }
}
