
namespace Anberkada.MusicBase.Biz.Contracts
{
    /// <summary>
    /// Interface to strategies of chord recognition.
    /// </summary>
    public interface IHarmonyStrategies
    {
        /// <summary>
        /// Gets the list of matching chords.
        /// </summary>
        /// <param name="pitchList">The list of pitches.</param>
        /// <param name="basePitch">The pitch of the base tone.</param>
        /// <param name="style">The style of music.</param>
        /// <returns>The list of matching chords; the first element is best match.</returns>
        IChord[] GetMatchingChords(Pitch[] pitchList, Pitch basePitch = null, MusicStyles style = MusicStyles.Unspecified);

        /// <summary>
        /// Gets the best matching chord.
        /// </summary>
        /// <param name="pitchList">The list of pitches.</param>
        /// <param name="basePitch">The pitch of the base tone.</param>
        /// <param name="style">The style of music.</param>
        /// <returns>The best matching chord.</returns>
        IChord GetBestMatchingChord(Pitch[] pitchList, Pitch basePitch = null, MusicStyles style = MusicStyles.Unspecified);
    }
}