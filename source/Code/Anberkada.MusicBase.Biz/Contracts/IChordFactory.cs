
namespace Anberkada.MusicBase.Biz.Contracts
{
    /// <summary>
    /// Interface to the chord factory.
    /// </summary>
    public interface IChordFactory
    {
        /// <summary>
        /// Creates the specified chord.
        /// </summary>
        /// <param name="baseTone">The base tone.</param>
        /// <param name="chord">The chord identifier.</param>
        /// <returns>
        /// The created object.
        /// </returns>
        IChord CreateChord(Tone baseTone, Chords chord);
    }
}