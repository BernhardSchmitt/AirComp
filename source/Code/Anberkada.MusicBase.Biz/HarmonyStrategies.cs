
namespace Anberkada.MusicBase.Biz
{
    using Contracts;

    /// <summary>
    /// Strategies for chord recognition.
    /// </summary>
    class HarmonyStrategies : IHarmonyStrategies
    {
        /// <summary>
        /// Gets the list of matching chords.
        /// </summary>
        /// <param name="pitchList">The list of pitches.</param>
        /// <param name="basePitch">The pitch of the base tone.</param>
        /// <param name="style">The style of music.</param>
        /// <returns>
        /// The list of matching chords; the first element is best match.
        /// </returns>
        public IChord[] GetMatchingChords(Pitch[] pitchList, Pitch basePitch = null, MusicStyles style = MusicStyles.Unspecified)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the best matching chord.
        /// </summary>
        /// <param name="pitchList">The list of pitches.</param>
        /// <param name="basePitch">The pitch of the base tone.</param>
        /// <param name="style">The style of music.</param>
        /// <returns>
        /// The best matching chord.
        /// </returns>
        public IChord GetBestMatchingChord(Pitch[] pitchList, Pitch basePitch = null, MusicStyles style = MusicStyles.Unspecified)
        {
            throw new System.NotImplementedException();
        }
    }
}